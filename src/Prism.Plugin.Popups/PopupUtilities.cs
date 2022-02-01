using Prism.Common;
using Prism.Navigation;
using Prism.Plugin.Popups.Dialogs;
using Prism.Plugin.Popups.Extensions;
using Rg.Plugins.Popup.Contracts;
using System.Linq;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    internal static class PopupUtilities
    {
        public const string NavigationModeKey = "__NavigationMode";

        public static INavigationParameters CreateNewNavigationParameters() =>
            new NavigationParameters().AddNavigationMode(NavigationMode.New);

        public static INavigationParameters CreateBackNavigationParameters() =>
            new NavigationParameters().AddNavigationMode(NavigationMode.Back);

        public static INavigationParameters AddNavigationMode(this INavigationParameters parameters, NavigationMode mode)
        {
            return parameters.AddInternalParameter(NavigationModeKey, mode);
        }

        public static INavigationParameters AddInternalParameter(this INavigationParameters parameters, string key, object value)
        {
            ((INavigationParametersInternal)parameters).Add(key, value);
            return parameters;
        }

        public static Page TopPage(IPopupNavigation popupNavigation, Page mainPage)
        {
            Page page = null;
            var popupStack = popupNavigation.PopupStack.Where(x => !(x is PopupDialogContainer));
            if (popupStack.Count() > 0)
                page = popupStack.LastOrDefault();
            else if (mainPage.Navigation.ModalStack.Count > 0)
                page = mainPage.Navigation.ModalStack.LastOrDefault();
            else
                page = mainPage.Navigation.NavigationStack.LastOrDefault();

            if (page == null)
                page = mainPage;

            return page.GetDisplayedPage();
        }

        public static Page GetOnNavigatedToTarget(IPopupNavigation popupNavigation, IApplicationProvider applicationProvider)
        {
            Page page = null;
            if (popupNavigation.PopupStack.Count > 1)
                page = popupNavigation.PopupStack.ElementAt(popupNavigation.PopupStack.Count() - 2);
            else if (applicationProvider.MainPage.Navigation.ModalStack.Count > 0)
                page = applicationProvider.MainPage.Navigation.ModalStack.LastOrDefault();
            else
                page = applicationProvider.MainPage.Navigation.NavigationStack.LastOrDefault();

            if (page == null)
                page = applicationProvider.MainPage;

            return page.GetDisplayedPage();
        }
    }
}
