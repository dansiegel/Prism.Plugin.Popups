using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Mocks.Events;

namespace Prism.Plugin.Popups.Tests.Mocks.ViewModels
{
    public class PopupPageMockViewModel : BindableBase, INavigatedAware, IDestructible
    {
        private IEventAggregator _eventAggregator { get; }

        public INavigationService NavigationService { get; }

        public int Destroyed { get; private set; }

        public PopupPageMockViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NavigationService = navigationService;
        }

        public bool NavigatedTo { get; set; }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _eventAggregator.GetEvent<NavigatedFromEvent>().Publish();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            NavigatedTo = true;
        }

        public void Destroy()
        {
            Destroyed++;
        }
    }
}