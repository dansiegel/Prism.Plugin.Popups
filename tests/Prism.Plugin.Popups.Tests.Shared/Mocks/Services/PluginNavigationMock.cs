using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Events;
using Rg.Plugins.Popup.Pages;

namespace Prism.Plugin.Popups.Tests.Mocks.Services
{
    public class PluginNavigationMock : IPopupNavigation
    {
        private static Lazy<IPopupNavigation> _lazyNavigation = new Lazy<IPopupNavigation>(() => new PluginNavigationMock());
        private static IPopupNavigation _instance;

        public event EventHandler<PopupNavigationEventArgs> Pushing;
        public event EventHandler<PopupNavigationEventArgs> Pushed;
        public event EventHandler<PopupNavigationEventArgs> Popping;
        public event EventHandler<PopupNavigationEventArgs> Popped;

        public static IPopupNavigation Instance => _instance ?? (_instance = _lazyNavigation.Value);

        internal PluginNavigationMock() { }

        private List<PopupPage> _popupStack { get; } = new List<PopupPage>();
        public IReadOnlyList<PopupPage> PopupStack => _popupStack;

        public async Task PopAllAsync(bool animate = true)
        {
            while(_popupStack.Any())
            {
                await PopAsync(animate);
            }
        }

        public Task PopAsync(bool animate = true)
        {
            if (_popupStack.Count > 0)
            {
                _popupStack.RemoveAt(_popupStack.Count - 1);
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
