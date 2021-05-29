using Prism.Navigation;
using Xamarin.Forms;

namespace PopupPluginSample.Views
{
    public partial class FLPRoot : FlyoutPage, IMasterDetailPageOptions
    {
        public FLPRoot()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}