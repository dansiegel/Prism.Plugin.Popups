using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using static System.Diagnostics.Debug;

namespace PopupPluginSample.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = GetType().Name;
            NavigateCommand = new DelegateCommand<object>(OnNavigateCommandExecuted);
            GoBackCommand = new DelegateCommand(OnGoBackCommandExecuted);
            WriteLine($"Initialized {Title}");
            NavigateCommand.CanExecuteChanged += CanExecuteChanged;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand<object> NavigateCommand { get; }

        public DelegateCommand GoBackCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("message", $"Hello from {GetType().Name}");
            WriteLine($"{Title} OnNavigatedFrom");
            WriteLine($"Parameters: {parameters.GetValue<string>("message")}");
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            WriteLine($"{Title} OnNavigatedTo");
            Message = parameters.GetValue<string>("message");
            WriteLine($"Parameters: {Message}");
            WriteLine($"Can Navigate: {NavigateCommand.CanExecute("foobar")}");
            WriteLine($"NavigationService Page: {(_navigationService as IPageAware).Page.GetType().Name}");
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            WriteLine($"{Title} OnNavigatingTo");
            Message = parameters.GetValue<string>("message");
            WriteLine($"Parameters: {Message}");
        }

        protected virtual async void OnNavigateCommandExecuted(object path)
        {
            WriteLine($"{GetType().Name} Executing Navigate Command: {path}");
            await _navigationService.NavigateAsync(path.ToString());
        }

        protected virtual async void OnGoBackCommandExecuted()
        {
            WriteLine($"{GetType().Name} Executing GoBack Command");
            await _navigationService.GoBackAsync();
        }

        private void CanExecuteChanged(object sender, EventArgs args) =>
            WriteLine($"Can Execute Changed: {((ICommand)sender).CanExecute(string.Empty)}");
    }
}
