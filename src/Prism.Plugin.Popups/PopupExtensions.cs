using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Prism.Navigation
{
    /// <summary>
    /// Extensions to better handle Popup Navigation.
    /// </summary>
    public static partial class PopupExtensions
    {

        static INavigation s_navigation => ContainerLocator.Container.Resolve<IApplicationProvider>().MainPage.Navigation;

        static IPopupNavigation s_popupNavigation => ContainerLocator.Container.Resolve<IPopupNavigation>();

        static IReadOnlyList<PopupPage> s_popupStack => s_popupNavigation.PopupStack;

        /// <summary>
        /// Clears the Popup Navigation Stack.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" />.</param>
        /// <param name="key">A single NavigationParameter key.</param>
        /// <param name="param">A single NavigationParameter value.</param>
        /// <param name="animated">A flag to indicate whether the Navigation should be animated.</param>
        /// <returns>The <see cref="INavigationResult" />.</returns>
        public static Task<INavigationResult> ClearPopupStackAsync(this INavigationService navigationService, string key, object param, bool animated = true) =>
            navigationService.ClearPopupStackAsync(GetNavigationParameters(key, param, NavigationMode.Back), animated);

        /// <summary>
        /// Clears the Popup Navigation Stack.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" />.</param>
        /// <param name="parameters">The <see cref="INavigationParameters" />.</param>
        /// <param name="animated">A flag to indicate whether the Navigation should be animated.</param>
        /// <returns>The <see cref="INavigationResult" />.</returns>
        public static async Task<INavigationResult> ClearPopupStackAsync(this INavigationService navigationService, INavigationParameters parameters = null, bool animated = true)
        {
            while (s_popupStack.Any())
            {
                var result = await navigationService.GoBackAsync(parameters, null, animated: animated);
                if (result.Exception != null)
                    return result;
            }

            return new NavigationResult { Success = true };
        }

        private static INavigationParameters GetNavigationParameters(string key, object param, NavigationMode mode) =>
            new NavigationParameters()
            {
                { key, param }
            }.AddNavigationMode(mode);
    }
}