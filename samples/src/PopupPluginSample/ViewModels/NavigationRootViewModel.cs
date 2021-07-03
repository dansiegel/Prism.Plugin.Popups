using System;
using Prism.Navigation;

namespace PopupPluginSample.ViewModels
{
    public class NavigationRootViewModel : BaseViewModel
    {
        public NavigationRootViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}