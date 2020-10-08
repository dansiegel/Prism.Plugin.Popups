using System;
using Prism.Navigation;

namespace PopupPluginSample.ViewModels
{
    public class TabbedRootViewModel : BaseViewModel
    {
        public TabbedRootViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
