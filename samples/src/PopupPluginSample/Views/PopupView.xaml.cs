using Xamarin.Forms;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Pages;

namespace PopupPluginSample.Views
{
    public partial class PopupView : PopupPage
    {
        public PopupView()
        {
            InitializeComponent();
            AnnounceBindingContext();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            AnnounceBindingContext();
        }

        private void AnnounceBindingContext()
        {
            System.Diagnostics.Debug.WriteLine(GetType().Name);
            System.Diagnostics.Debug.WriteLine($"BindingContext: {BindingContext?.GetType()?.Name}");
        }

    }
}

