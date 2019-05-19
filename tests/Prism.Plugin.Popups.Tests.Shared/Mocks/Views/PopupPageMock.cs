using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups.Tests.Mocks.Views
{
    public class PopupPageMock : PopupPage
    {
        public PopupPageMock()
        {
            Content = new Label { Text = "Hello World" };
        }
    }
}
