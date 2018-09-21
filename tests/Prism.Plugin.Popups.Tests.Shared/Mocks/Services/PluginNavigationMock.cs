using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;

namespace Prism.Plugin.Popups.Tests.Mocks.Services
{
    public class PluginNavigationMock : IPopupNavigation
    {
        private static Lazy<IPopupNavigation> _lazyNavigation = new Lazy<IPopupNavigation>(() => new PluginNavigationMock());
        private static IPopupNavigation _instance;
        public static IPopupNavigation Instance => _instance ?? (_instance = _lazyNavigation.Value);

        internal PluginNavigationMock() { }

        private List<PopupPage> _popupStack { get; } = new List<PopupPage>();
        public IReadOnlyList<PopupPage> PopupStack => _popupStack;

        public Task PopAllAsync(bool animate = true)
        {
            _popupStack.Clear();

            return Task.CompletedTask;
        }

        public Task PopAsync(bool animate = true)
        {
            if (_popupStack.Any())
            {
                _popupStack.Remove(_popupStack.Last());
            }

            return Task.CompletedTask;
        }

        public Task PushAsync(PopupPage page, bool animate = true)
        {
            _popupStack.Add(page);

            return Task.CompletedTask;
        }

        public Task RemovePageAsync(PopupPage page, bool animate = true)
        {
            _popupStack.Remove(page);

            return Task.CompletedTask;
        }
    }
}
