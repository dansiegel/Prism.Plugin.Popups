using System;
using System.Collections.Generic;
using System.Text;
using Prism.AppModel;
using Prism.Common;
using Prism.Ioc;
using Rg.Plugins.Popup.Contracts;

namespace Prism.Plugin.Popups
{
    /// <summary>
    /// Provides support extensions for <see cref="IApplicationLifecycleAware" />
    /// </summary>
    public static class ApplicationLifecycleExtensions
    {
        /// <summary>
        /// Adds support for <see cref="IApplicationLifecycleAware.OnResume" />
        /// </summary>
        /// <remarks>
        /// Do not invoke <c>base.OnResume()</c>.
        /// </remarks>
        /// <param name="app">The <see cref="PrismApplicationBase" /></param>
        public static void PopupPluginOnResume(this PrismApplicationBase app)
        {
            InvokeLifecyleEvent(app, x => x.OnResume());
        }

        /// <summary>
        /// Adds support for <see cref="IApplicationLifecycleAware.OnSleep" />
        /// </summary>
        /// <remarks>
        /// Do not invoke <c>base.OnResume()</c>.
        /// </remarks>
        /// <param name="app">The <see cref="PrismApplicationBase" /></param>
        public static void PopupPluginOnSleep(this PrismApplicationBase app)
        {
            InvokeLifecyleEvent(app, x => x.OnSleep());
        }

        private static void InvokeLifecyleEvent(PrismApplicationBase app, Action<IApplicationLifecycleAware> action)
        {
            if (app.MainPage != null)
            {
                var popupNavigation = app.Container.Resolve<IPopupNavigation>();
                var appProvider = app.Container.Resolve<IApplicationProvider>();

                var page = PopupUtilities.TopPage(popupNavigation, appProvider);
                PageUtilities.InvokeViewAndViewModelAction(page, action);
            }
        }
    }
}