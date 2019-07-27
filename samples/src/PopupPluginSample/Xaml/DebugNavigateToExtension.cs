using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace PopupPluginSample.Xaml
{
    public class DebugNavigateToExtension : Prism.Navigation.Xaml.NavigateToExtension
    {
        protected override Task HandleNavigation(INavigationParameters parameters, INavigationService navigationService)
        {
            return base.HandleNavigation(parameters, navigationService);
        }
    }
}
