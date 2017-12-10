using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PopupPluginSample.ViewModels
{
    public class PopupViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService { get; }

        public PopupViewModel( INavigationService navigationService )
        {
            System.Diagnostics.Debug.WriteLine( "Hello from the PopupViewViewModel" );
            _navigationService = navigationService;
            NavigateBackCommand = new DelegateCommand( OnNavigateBackCommandExecuted );
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty( ref _message, value ); }
        }

        public DelegateCommand NavigateBackCommand { get; }

        public void OnNavigatingTo( NavigationParameters parameters )
        {
            System.Diagnostics.Debug.WriteLine( $"{GetType().Name} Navigating To" );
        }

        public void OnNavigatedFrom( NavigationParameters parameters )
        {
            System.Diagnostics.Debug.WriteLine( $"{GetType().Name} Navigated From" );
        }

        public void OnNavigatedTo( NavigationParameters parameters )
        {
            System.Diagnostics.Debug.WriteLine( $"{GetType().Name} Navigated To" );

            if( parameters.ContainsKey( "message" ) )
            {
                Message = parameters[ "message" ].ToString();
            }
        }

        private async void OnNavigateBackCommandExecuted()
        {
            await _navigationService.GoBackAsync(new NavigationParameters{
                { "message", "Hello from the Popup View" }
            });
        }
    }
}
