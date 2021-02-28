# Initializing Rg.Plugin.Popup

Rg.Plugin.Popup must be initialized on each platform. 

!!! danger "Critical Note"
    With the exception of the BackButton handler for Android there is NO platform specific code in this Plugin. Any platform specific errors that you may encounter are likely the result of a bug in either Rg.Plugins.Popup or Xamarin.Forms.

## Android

```c#
public class MainActivity : FormsAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        TabLayoutResource = Resource.Layout.Tabbar;
        ToolbarResource = Resource.Layout.Toolbar;

        base.OnCreate(savedInstanceState);

        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        global::Rg.Plugins.Popup.Popup.Init(this);

        LoadApplication(new App());
    }
}
```

## iOS

```c#
public partial class AppDelegate : FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
    {
        global::Rg.Plugins.Popup.Popup.Init();
        global::Xamarin.Forms.Forms.Init();

        LoadApplication(new App());

        return base.FinishedLaunching(uiApplication, launchOptions);
    }
}
```
