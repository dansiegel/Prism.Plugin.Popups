using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Plugin.Popups
{
    public class PluginNavigationMock : IPopupNavigation
    {
        private static Lazy<IPopupNavigation> _lazyNavigation = new Lazy<IPopupNavigation>(() => new PluginNavigationMock());
        private static IPopupNavigation _instance;
        public static IPopupNavigation Instance => _instance ?? (_instance = _lazyNavigation.Value);

        private PluginNavigationMock() { }

        private List<PopupPage> _popupStack { get; } = new List<PopupPage>();
        public IReadOnlyList<PopupPage> PopupStack => _popupStack;

        public Task PopAllAsync(bool animate = true)
        {
            _popupStack.Clear();

#if NETSTANDARD1_0
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        public Task PopAsync(bool animate = true)
        {
            if (_popupStack.Any())
            {
                _popupStack.Remove(_popupStack.Last());
            }

#if NETSTANDARD1_0
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        public Task PushAsync(PopupPage page, bool animate = true)
        {
            _popupStack.Add(page);

#if NETSTANDARD1_0
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        public Task RemovePageAsync(PopupPage page, bool animate = true)
        {
            _popupStack.Remove(page);

#if NETSTANDARD1_0
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }
    }
}
