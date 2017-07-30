using System.Threading.Tasks;
using PopupPluginSample.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PopupPluginSample
{
    public partial class App : PrismApplication
    {
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.NavigateAsync( "MenuPage" );

            NavigationService.NavigateAsync("MainPage?message=Hello%20from%20the%20App");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterInstance(PopupNavigation.Instance);
            Container.RegisterTypeForNavigation<MenuPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<PopupView>();
            Container.RegisterTypeForNavigation<MDPRoot>();
            Container.RegisterTypeForNavigation<NavigationRoot>();
            Container.RegisterTypeForNavigation<TabbedRoot>();
            Container.RegisterPopupNavigationService();
        }
    }
}
