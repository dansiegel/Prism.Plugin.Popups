using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace PopupPluginSample.ViewModels
{
    public class PopupViewModel : BindableBase, INavigationAware, IConfirmNavigationAsync
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _pageDialogService { get; }

        public PopupViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            System.Diagnostics.Debug.WriteLine("Hello from the PopupViewViewModel");
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            NavigateBackCommand = new DelegateCommand(OnNavigateBackCommandExecuted);
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand NavigateBackCommand { get; }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} Navigating To");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} Navigated From");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{GetType().Name} Navigated To");

            if (parameters.ContainsKey("message"))
            {
                Message = parameters["message"].ToString();
            }
        }

        private async void OnNavigateBackCommandExecuted()
        {
            await _navigationService.GoBackAsync(new NavigationParameters{
                { "message", "Hello from the Popup View" }
            });
        }

        public Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return _pageDialogService.DisplayAlertAsync("Go Back", "You pressed the hardware back button. Are you sure you want to leave?", "Yes", "No");
        }
    }
}