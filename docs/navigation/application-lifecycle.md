Version 8 of the Popup Plugin has introduced support for Prism's IApplicationLifecycleAware interface on your PopupPage ViewModels. This means that if your app goes to sleep while you have a PopupPage displayed, it's ViewModel can now handle the OnSleep, and similarly the OnResume when the app resumes.

!!! important "Implementation Note"
    Support for IApplicationLifecycleAware traditionally comes from PrismApplicationBase's override of OnResume / OnSleep. Normally if you do not include a reference to the base method IApplicationLifecycleAware would be disabled. Using the PopupPlugin it is important that you do **NOT** reference the base method call as this will lead to bugs in your app.

```cs
using Prism.Plugin.Popups;

public partial class App : PrismApplication
{
    protected override void OnResume()
    {
        this.PopupPluginOnResume();
    }

    protected override void OnSleep()
    {
        this.PopupPluginOnSleep();
    }
}
```
