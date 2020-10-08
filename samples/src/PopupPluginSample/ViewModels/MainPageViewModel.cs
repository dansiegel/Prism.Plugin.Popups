using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupPluginSample.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public DelegateCommand LaunchPopupCommand => new DelegateCommand(OnLaunchPopupCommandExecuted);

        private async void OnLaunchPopupCommandExecuted() =>
            await _navigationService.NavigateAsync("PopupView");
    }
}

