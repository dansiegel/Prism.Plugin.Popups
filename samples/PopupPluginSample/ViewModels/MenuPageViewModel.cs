using System;
using Prism.Navigation;

namespace PopupPluginSample.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public MenuPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
        }
    }
}
