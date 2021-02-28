#if MONOANDROID
using System;
using System.Threading;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace Prism.Plugin.Popups
{
    /// <summary>
    /// Native helper class
    /// </summary>
    public static class PopupPlugin
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        /// <summary>
        /// Called when the Activity has detected the user's press of the back key
        /// </summary>
        public static async void OnBackPressed()
        {
            await semaphore.WaitAsync();
            try
            {
                var container = ContainerLocator.Container;
                var popupNavigation = container.Resolve<IPopupNavigation>();
                var appProvider = container.Resolve<IApplicationProvider>();

                var topPage = PopupUtilities.TopPage(popupNavigation, appProvider);
                var navService = container.Resolve<INavigationService>(PrismApplicationBase.NavigationServiceName);
                if (navService is IPageAware pa)
                {
                    pa.Page = topPage;
                }

                var parameters = new NavigationParameters();
                parameters.AddInternalParameter("_NavigationSource", "BackButton");
                await navService.GoBackAsync(parameters);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
#endif
