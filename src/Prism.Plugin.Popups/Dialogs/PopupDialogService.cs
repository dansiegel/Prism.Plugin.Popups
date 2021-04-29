using System;
using System.Diagnostics.CodeAnalysis;
using Prism.Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Plugin.Popups.Dialogs;
using Prism.Plugin.Popups.Dialogs.Xaml;
using Prism.Services.Dialogs.Xaml;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Services.Dialogs.Popups
{
    /// <summary>
    /// Provides an implementation of the <see cref="IDialogService" /> that uses <see cref="PopupPage"/>
    /// as the background page container.
    /// </summary>
    public class PopupDialogService : IDialogService
    {
        /// <summary>
        /// The Style name expected in the Application <see cref="ResourceDictionary" />
        /// </summary>
        public const string PopupOverlayStyle = "PrismDialogMaskStyle";

        private IPopupNavigation _popupNavigation { get; }
        private IContainerProvider _containerProvider { get; }

        /// <summary>
        /// Creates a new <see cref="PopupDialogService" />
        /// </summary>
        /// <param name="popupNavigation">The <see cref="IPopupNavigation" /> service to push and pop the <see cref="PopupPage" />.</param>
        /// <param name="containerProvider">The <see cref="IContainerProvider" /> to resolve the Dialog View.</param>
        public PopupDialogService(IPopupNavigation popupNavigation, IContainerProvider containerProvider)
        {
            _popupNavigation = popupNavigation;
            _containerProvider = containerProvider;
        }

        /// <inheritdoc />
        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            try
            {
                parameters = UriParsingHelper.GetSegmentParameters(name, parameters);

                var view = CreateViewFor(UriParsingHelper.GetSegmentName(name));
                var popupPage = CreatePopupPageForView(view);

                var dialogAware = InitializeDialog(view, parameters);

                if (!parameters.TryGetValue<bool>(KnownDialogParameters.CloseOnBackgroundTapped, out var closeOnBackgroundTapped))
                {
                    var dialogLayoutCloseOnBackgroundTapped = DialogLayout.GetCloseOnBackgroundTapped(view);
                    if (dialogLayoutCloseOnBackgroundTapped.HasValue)
                    {
                        closeOnBackgroundTapped = dialogLayoutCloseOnBackgroundTapped.Value;
                    }
                }

                if (!parameters.TryGetValue<bool>(KnownPopupDialogParameters.Animated, out var animated))
                    animated = true;

                var popupDialogLayoutIsAnimationEnabled = PopupDialogLayout.GetIsAnimationEnabled(view);
                popupPage.IsAnimationEnabled = popupDialogLayoutIsAnimationEnabled ?? true;

                dialogAware.RequestClose += DialogAware_RequestClose;

                void CloseOnBackgroundClicked(object sender, EventArgs args)
                {
                    DialogAware_RequestClose(new DialogParameters());
                }

                void DialogAware_RequestClose(IDialogParameters outParameters)
                {
                    try
                    {
                        var result = CloseDialog(outParameters ?? new DialogParameters(), popupPage, view);
                        if (result.Exception is DialogException de && de.Message == DialogException.CanCloseIsFalse)
                        {
                            return;
                        }

                        dialogAware.RequestClose -= DialogAware_RequestClose;
                        if (closeOnBackgroundTapped)
                        {
                            popupPage.BackgroundClicked -= CloseOnBackgroundClicked;
                        }
                        callback?.Invoke(result);
                        GC.Collect();
                    }
                    catch (DialogException dex)
                    {
                        if (dex.Message != DialogException.CanCloseIsFalse)
                        {
                            callback?.Invoke(new DialogResult
                            {
                                Exception = dex,
                                Parameters = parameters
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        callback?.Invoke(new DialogResult
                        {
                            Exception = ex,
                            Parameters = parameters
                        });
                    }
                }

                if (closeOnBackgroundTapped)
                {
                    popupPage.BackgroundClicked += CloseOnBackgroundClicked;
                }

                popupPage.RequestClose = () => DialogAware_RequestClose(null);

                Action<IDialogParameters> closeCallback = closeOnBackgroundTapped ? DialogAware_RequestClose : p => { };
                PushPopupPage(popupPage, view, closeCallback, animated);
            }
            catch (Exception ex)
            {
                callback?.Invoke(new DialogResult { Exception = ex });
            }
        }

        private static PopupDialogContainer CreatePopupPageForView(View view)
        {
            var popupPage = new PopupDialogContainer();

            var hasSystemPadding = view.GetValue(PopupDialogLayout.HasSystemPaddingProperty);
            if (hasSystemPadding != null) popupPage.HasSystemPadding = (bool)hasSystemPadding;

            var hasKeyboardOffset = view.GetValue(PopupDialogLayout.HasKeyboardOffsetProperty);
            if (hasKeyboardOffset != null) popupPage.HasKeyboardOffset = (bool)hasKeyboardOffset;

            return popupPage;
        }

        private View CreateViewFor(string name)
        {
            var view = (View)_containerProvider.Resolve<object>(name);

            if (ViewModelLocator.GetAutowireViewModel(view) is null)
            {
                ViewModelLocator.SetAutowireViewModel(view, true);
            }

            return view;
        }

        private IDialogAware GetDialogController(View view)
        {
            if (view is IDialogAware viewAsDialogAware)
            {
                return viewAsDialogAware;
            }
            else if (view.BindingContext is null)
            {
                throw new DialogException(DialogException.NoViewModel);
            }
            else if (view.BindingContext is IDialogAware dialogAware)
            {
                return dialogAware;
            }

            throw new DialogException(DialogException.ImplementIDialogAware);
        }

        private IDialogAware InitializeDialog(View view, IDialogParameters parameters)
        {
            var dialog = GetDialogController(view);

            dialog.OnDialogOpened(parameters);

            return dialog;
        }

        private IDialogResult CloseDialog(IDialogParameters parameters, PopupPage popupPage, View dialogView)
        {
            try
            {
                if (parameters is null)
                {
                    parameters = new DialogParameters();
                }

                if (!parameters.TryGetValue<bool>(KnownPopupDialogParameters.Animated, out var animated))
                    animated = true;

                var dialogAware = GetDialogController(dialogView);

                if (!dialogAware.CanCloseDialog())
                {
                    throw new DialogException(DialogException.CanCloseIsFalse);
                }

                dialogAware.OnDialogClosed();
                _popupNavigation.RemovePageAsync(popupPage, animated);

                return new DialogResult
                {
                    Parameters = parameters
                };
            }
            catch (DialogException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return new DialogResult
                {
                    Exception = ex,
                    Parameters = parameters
                };
            }
        }

        private async void PushPopupPage(PopupPage popupPage, View dialogView, Action<IDialogParameters> closeCallback, bool animated = true)
        {
            View mask = DialogLayout.GetMask(dialogView);
            var gesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command<IDialogParameters>(closeCallback),
                CommandParameter = new DialogParameters()
            };

            if (mask is null)
            {
                Style overlayStyle = GetOverlayStyle(dialogView);

                mask = new BoxView
                {
                    Style = overlayStyle
                };
            }

            mask.SetBinding(VisualElement.WidthRequestProperty, new Binding { Path = "Width", Source = popupPage });
            mask.SetBinding(VisualElement.HeightRequestProperty, new Binding { Path = "Height", Source = popupPage });
            mask.GestureRecognizers.Add(gesture);

            var overlay = new AbsoluteLayout();
            var relativeWidth = DialogLayout.GetRelativeWidthRequest(dialogView);
            if (relativeWidth != null)
            {
                dialogView.SetBinding(View.WidthRequestProperty,
                    new Binding("Width",
                                BindingMode.OneWay,
                                new RelativeContentSizeConverter { RelativeSize = relativeWidth.Value },
                                source: popupPage));
            }

            var relativeHeight = DialogLayout.GetRelativeHeightRequest(dialogView);
            if (relativeHeight != null)
            {
                dialogView.SetBinding(View.HeightRequestProperty,
                    new Binding("Height",
                                BindingMode.OneWay,
                                new RelativeContentSizeConverter { RelativeSize = relativeHeight.Value },
                                source: popupPage));
            }

            //AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
            //AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, popupPage.Width, popupPage.Height));
            AbsoluteLayout.SetLayoutFlags(dialogView, AbsoluteLayoutFlags.PositionProportional);
            var popupBounds = DialogLayout.GetLayoutBounds(dialogView);
            AbsoluteLayout.SetLayoutBounds(dialogView, popupBounds);
            //overlay.Children.Add(content);
            if (DialogLayout.GetUseMask(dialogView) ?? true)
            {
                overlay.Children.Add(mask);
            }
            else
            {
                overlay.GestureRecognizers.Add(gesture);
            }

            overlay.Children.Add(dialogView);
            popupPage.Content = overlay;
            await _popupNavigation.PushAsync(popupPage, animated);
        }

        private static Style GetOverlayStyle(View popupView)
        {
            var style = DialogLayout.GetMaskStyle(popupView);
            if (style != null)
            {
                return style;
            }

            if (Application.Current.Resources.ContainsKey(PopupOverlayStyle))
            {
                style = (Style)Application.Current.Resources[PopupOverlayStyle];
                if (style.TargetType == typeof(BoxView))
                {
                    return style;
                }
            }

            var overlayStyle = new Style(typeof(BoxView));
            overlayStyle.Setters.Add(new Setter { Property = VisualElement.OpacityProperty, Value = 0.75 });
            overlayStyle.Setters.Add(new Setter { Property = VisualElement.BackgroundColorProperty, Value = Color.Black });

            Application.Current.Resources[PopupOverlayStyle] = overlayStyle;
            return overlayStyle;
        }

        private class DialogResult : IDialogResult
        {
            public Exception Exception { get; set; }
            public IDialogParameters Parameters { get; set; }
        }
    }
}
