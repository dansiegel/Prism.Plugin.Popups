Version 8 of the Popup Plugin has introduced new support for handling the Android Back Button. This cannot however be done seamlessly from within Xamarin.Forms. In order to support the Android Back Button you will need to update your MainActivity. Adding support is very simple as you will simply need to override the OnBackPressed method in your MainActivity and call `PopupPlugin.OnBackPressed()`.

!!! important "Implementation Note"
    Do not include a reference to the base.OnBackPressed

```csharp
using Prism.Plugin.Popups;

public class MainActivity : FormsAppCompatActivity
{
    public override void OnBackPressed()
    {
        PopupPlugin.OnBackPressed();
    }
}
```

!!! note
    This should only be used for version 8.0. Prism 8.1 introduced back button support which supports BOTH dialogs and Page navigation. This API will be deprecated when we bump to 8.1 as the Prism.Forms API should be used directly with 8.1.