using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;

namespace PopupPluginSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
