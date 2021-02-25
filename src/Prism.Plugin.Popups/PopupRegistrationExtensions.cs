using System;
using System.Reflection;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Prism.Services.Dialogs.Popups;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    /// <summary>
    /// Provides additional extensions to easily register Popup Service Extensions for Prism.
    /// </summary>
    public static class PopupRegistrationExtensions
    {
        /// <summary>
        /// Registers your custom <see cref="PopupPageNavigationService" /> to override the default <see cref="INavigationService" />
        /// provided by Prism. This adds support for navigating to / from <see cref="PopupPage" /> Views as easily as any
        /// other <see cref="Page" />.
        /// </summary>
        /// <typeparam name="TService">Your custom type of <see cref="PopupPageNavigationService " />.</typeparam>
        /// <param name="containerRegistry">The <see cref="IContainerRegistry" />.</param>
        /// <returns>The <see cref="IContainerRegistry" />.</returns>
        public static IContainerRegistry RegisterPopupNavigationService<TService>(this IContainerRegistry containerRegistry)
            where TService : PopupPageNavigationService
        {
            containerRegistry.RegisterPopupNavigation();
            containerRegistry.RegisterSingleton<IPageBehaviorFactory, PopupPageBehaviorFactory>();

            containerRegistry.RegisterScoped<INavigationService, TService>();
            return containerRegistry.Register<INavigationService, TService>(PrismApplicationBase.NavigationServiceName);
        }

        /// <summary>
        /// Registers the <see cref="PopupPageNavigationService" /> to override the default <see cref="INavigationService" />
        /// provided by Prism. This adds support for navigating to / from <see cref="PopupPage" /> Views as easily as any
        /// other <see cref="Page" />.
        /// </summary>
        /// <param name="containerRegistry">The <see cref="IContainerRegistry" />.</param>
        /// <returns>The <see cref="IContainerRegistry" />.</returns>
        public static IContainerRegistry RegisterPopupNavigationService(this IContainerRegistry containerRegistry) =>
            containerRegistry.RegisterPopupNavigationService<PopupPageNavigationService>();

        /// <summary>
        /// Updates the <see cref="IDialogService" /> registration to use a <see cref="PopupPage" /> as the root Page container.
        /// </summary>
        /// <param name="containerRegistry">The <see cref="IContainerRegistry" />.</param>
        /// <returns>The <see cref="IContainerRegistry" />.</returns>
        public static IContainerRegistry RegisterPopupDialogService(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigation();
            return containerRegistry.RegisterSingleton<IDialogService, PopupDialogService>();
        }

        private static void RegisterPopupNavigation(this IContainerRegistry containerRegistry)
        {
            if (!containerRegistry.IsRegistered<IPopupNavigation>())
            {
                containerRegistry.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            }
        }
    }
}
