using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups.Dialogs.Xaml
{
    /// <summary>
    /// Provides additional attached properties for Dialogs
    /// </summary>
    public static class PopupDialogLayout
    {
        /// <summary>
        /// Sets the IsAnimationEnabled property on the <see cref="PopupPage"/>
        /// </summary>
        public static readonly BindableProperty IsAnimationEnabledProperty =
            BindableProperty.CreateAttached("IsAnimationEnabled", typeof(bool?), typeof(PopupDialogLayout), null);

        /// <summary>
        /// Gets the <see cref="IsAnimationEnabledProperty" />
        /// </summary>
        /// <param name="bindable">The Dialog View.</param>
        /// <returns>The value of IsAnimationEnabled.</returns>
        public static bool? GetIsAnimationEnabled(BindableObject bindable) =>
            (bool?)bindable.GetValue(IsAnimationEnabledProperty);

        /// <summary>
        /// Sets the <see cref="IsAnimationEnabledProperty" />
        /// </summary>
        /// <param name="bindable">The Dialog View.</param>
        /// <param name="value">The value of IsAnimationEnabled.</param>
        public static void SetIsAnimationEnabled(BindableObject bindable, bool? value) =>
            bindable.SetValue(IsAnimationEnabledProperty, value);

        /// <summary>
        /// Provides an attached property for Dialog Views to set the HasSystemPadding of the PopupPage.
        /// </summary>
        public static readonly BindableProperty HasSystemPaddingProperty = BindableProperty.CreateAttached("HasSystemPadding", typeof(bool), typeof(View), false);

        /// <summary>
        /// Gets the HasSystemPadding property from the Dialog View.
        /// </summary>
        /// <param name="view">The DialogView.</param>
        /// <returns>The value.</returns>
        public static bool GetHasSystemPadding(BindableObject view)
        {
            return (bool)view.GetValue(HasSystemPaddingProperty);
        }

        /// <summary>
        /// Sets the HasSystemPadding property of on the Dialog View.
        /// </summary>
        /// <param name="view">The Dialog View.</param>
        /// <param name="value">The value.</param>
        public static void SetHasSystemPadding(BindableObject view, bool value)
        {
            view.SetValue(HasSystemPaddingProperty, value);
        }

        /// <summary>
        /// Provides an attached property for Dialog Views to set the HasKeyboardOffset of the PopupPage.
        /// </summary>
        public static readonly BindableProperty HasKeyboardOffsetProperty = BindableProperty.CreateAttached("HasKeyboardOffset", typeof(bool), typeof(View), false);

        /// <summary>
        /// Gets the value for HasKeyboardOffset.
        /// </summary>
        /// <param name="view">The Dialog View.</param>
        /// <returns>The value.</returns>
        public static bool GetHasKeyboardOffset(BindableObject view)
        {
            return (bool)view.GetValue(HasKeyboardOffsetProperty);
        }

        /// <summary>
        /// Sets the value for the Popup HasKeyboardOffset
        /// </summary>
        /// <param name="view">The Dialog View.</param>
        /// <param name="value">The value.</param>
        public static void SetHasKeyboardOffset(BindableObject view, bool value)
        {
            view.SetValue(HasKeyboardOffsetProperty, value);
        }
    }
}