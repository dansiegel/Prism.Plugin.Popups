# Initializing Rg.Plugin.Popup

Rg.Plugin.Popup must be initialized on each platform.

## Android

```c#
public class MainActivity : FormsAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        TabLayoutResource = Resource.Layout.Tabbar;
        ToolbarResource = Resource.Layout.Toolbar;

        base.OnCreate(savedInstanceState);

        global::Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

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
