using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupPluginSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService { get; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty( ref _title, value ); }
        }

        public DelegateCommand LaunchPopupCommand { get; }

        public MainPageViewModel( INavigationService navigationService )
        {
            _navigationService = navigationService;
            LaunchPopupCommand = new DelegateCommand( OnLaunchPopupCommandExecuted );
        }

        public void OnNavigatingTo( NavigationParameters parameters )
        {
            System.Diagnostics.Debug.WriteLine( "NavigatingTo MainPageViewModel" );
        }

        public void OnNavigatedFrom( NavigationParameters parameters )
        {

        }

        public void OnNavigatedTo( NavigationParameters parameters )
        {
            if( parameters.ContainsKey( "title" ) )
                Title = ( string )parameters[ "title" ] + " and Prism";
        }

        private async void OnLaunchPopupCommandExecuted()
        {
            await _navigationService.NavigateAsync( $"PopupView?message=Hello%20from%20{GetType().Name}" );
        }
    }
}

