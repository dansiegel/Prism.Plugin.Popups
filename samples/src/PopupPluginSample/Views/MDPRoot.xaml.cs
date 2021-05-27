using Prism.Navigation;
using Xamarin.Forms;

namespace PopupPluginSample.Views
{
    public partial class MDPRoot : IMasterDetailPageOptions
    {
        public MDPRoot()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}