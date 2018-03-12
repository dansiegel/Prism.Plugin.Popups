using Prism.Navigation;

namespace Prism.Plugin.Popups.Tests.Mocks.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationService NavigationService { get; set; }
    }
}
