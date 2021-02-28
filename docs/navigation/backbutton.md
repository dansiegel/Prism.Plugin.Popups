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