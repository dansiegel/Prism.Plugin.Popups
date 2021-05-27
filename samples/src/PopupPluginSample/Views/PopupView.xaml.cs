using System.Diagnostics;

namespace PopupPluginSample.Views
{
    public partial class PopupView
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
            Debug.WriteLine(GetType().Name);
            Debug.WriteLine($"BindingContext: {BindingContext?.GetType()?.Name}");
        }
    }
}