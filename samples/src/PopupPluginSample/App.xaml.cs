using DryIoc;
using PopupPluginSample.Dialogs;
using PopupPluginSample.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Logging;
using Prism.Plugin.Popups;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PopupPluginSample
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLoggingListeners();

            await NavigationService.NavigateAsync("MenuPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();

            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<MDPRoot>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<NavigationRoot>();
            containerRegistry.RegisterForNavigation<PopupView>();
            containerRegistry.RegisterForNavigation<TabbedRoot>();

            containerRegistry.RegisterDialog<SampleDialog, SampleDialogViewModel>();
        }

        private void SetupLoggingListeners()
        {
            Log.Listeners.Add(new FormsLoggingListener());
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Container.Resolve<ILoggerFacade>().Log($"{e.Exception}", Category.Exception, Priority.None);
            };
        }

        private class FormsLoggingListener : LogListener
        {
            public override void Warning(string category, string message) =>
                Trace.WriteLine($"    {category}: {message}");
        }
    }
}
