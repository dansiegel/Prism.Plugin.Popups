using Prism.Navigation;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    /// <summary>
    /// An extended <see cref="NavigationException" /> for use cases that specifically apply to <see cref="PopupPage" />
    /// </summary>
    public class PopupNavigationException : NavigationException
    {
        /// <summary>
        /// Gets the error message for when the Application.MainPage has not yet been set.
        /// </summary>
        public const string RootPageHasNotBeenSet = "Popup Pages cannot be set before the Application.MainPage has been set. You must have a valid NavigationStack prior to navigating.";

        /// <summary>
        /// Creates a new instance of the <see cref="PopupNavigationException" />
        /// </summary>
        /// <param name="page">The <see cref="PopupPage" /></param>
        public PopupNavigationException(Page page)
            : base(RootPageHasNotBeenSet, page)
        {

        }
    }
}
