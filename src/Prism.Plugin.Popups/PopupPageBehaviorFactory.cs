using System;
using Prism.Behaviors;
using Prism.Common;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;

namespace Prism.Plugin.Popups
{
    /// <summary>
    /// Provides an implementation of the <see cref="IPageBehaviorFactory" /> to add the 
    /// <see cref="BackgroundPopupDismissalBehavior" /> on <see cref="PopupPage" />.
    /// </summary>
    public class PopupPageBehaviorFactory : PageBehaviorFactory
    {
        IPopupNavigation _popupNavigation { get; }
        IApplicationProvider _applicationProvider { get; }

        /// <summary>
        /// Creates a new <see cref="IPageBehaviorFactory" /> for use with <see cref="PopupPage" />.
        /// </summary>
        /// <param name="popupNavigation">The <see cref="IPopupNavigation" /> service.</param>
        /// <param name="applicationProvider">The <see cref="IApplicationProvider" />.</param>
        public PopupPageBehaviorFactory(IPopupNavigation popupNavigation, IApplicationProvider applicationProvider)
        {
            _popupNavigation = popupNavigation;
            _applicationProvider = applicationProvider;
        }

        /// <inheritdoc />
        protected override void ApplyPageBehaviors(Xamarin.Forms.Page page)
        {
            base.ApplyPageBehaviors(page);
            if (page is PopupPage popupPage)
            {
                popupPage.Behaviors.Add(new BackgroundPopupDismissalBehavior(_popupNavigation, _applicationProvider));
            }
        }
    }
}
