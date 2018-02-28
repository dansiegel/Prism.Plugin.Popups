using System.Threading.Tasks;
using PopupPluginSample.Views;
using Prism;
using Prism.Ioc;
#if AUTOFAC
using Autofac;
using Prism.Autofac;
#elif DRYIOC
using DryIoc;
using Prism.DryIoc;
#elif UNITY
using Unity;
using Prism.Unity;
#endif
using Prism.Logging;
using Xamarin.Forms;
using Prism.Plugin.Popups;

namespace PopupPluginSample
{
    public class App : PrismApplication
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
            //InitializeComponent();
            LogUnobservedTaskExceptions();

            await NavigationService.NavigateAsync("MenuPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            //containerRegistry.GetContainer().Unregister<INavigationService>(NavigationServiceName);
            //containerRegistry.RegisterInstance(Rg.Plugins.Popup.Services.PopupNavigation.Instance);
            //containerRegistry.GetContainer().Register<INavigationService, PopupPageNavigationService>(reuse: Reuse.Transient,
            //                                                                                          //ifAlreadyRegistered: IfAlreadyRegistered.Replace,
            //                                                                                          serviceKey: NavigationServiceName);


            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<MDPRoot>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<NavigationRoot>();
            containerRegistry.RegisterForNavigation<PopupView>();
            containerRegistry.RegisterForNavigation<TabbedRoot>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Container.Resolve<ILoggerFacade>().Log($"{e.Exception}", Category.Exception, Priority.None);
            };
        }
    }
}
