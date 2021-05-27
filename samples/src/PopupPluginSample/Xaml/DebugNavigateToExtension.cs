using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Navigation.Xaml;

namespace PopupPluginSample.Xaml
{
    public class DebugNavigateToExtension : NavigateToExtension
    {
        protected override Task HandleNavigation(INavigationParameters parameters, INavigationService navigationService)
        {
            return base.HandleNavigation(parameters, navigationService);
        }
    }
}