The original purpose of the Popup Plugin was to provide support for Popup Pages within a Prism Application. This cannot be natively supported since Xamarin.Forms has no concept of a PopupPage, and Rg.Plugins.Popup uses it's own service for push and popping PopupPages. While the original implemenation largely consisted of extension methods, the current version of the Popup Plugin now replaces the NavigationService that is registered by Prism with it's own implementation that understands Popup Pages.

## Initializing the Popup Plugin

!!! warning "Warning"
    Popup Pages from Rg.Plugins.Popup are simply a Xamarin.Forms.ContentPage. If you fail to properly initialize Rg.Plugins.Poup or you fail to register the Popup Plugin, Prism will simply push the Popup Page as if it were a Content Page. This may result in the Popup Page being treated as a Modal page or it may be pushed into a Navigation Page. Be sure to see the [Initializing Rg.Plugin.Popup](rgplugins.md) topic for more information.

In your Prism Application you will need to be sure to specify that you want to register the Popup Plugin's Navigation Service from your RegisterTypes method. Failure to call this method will result in the Popup Navigation Service not being registered.

```c#
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterPopupNavigationService();
}
```

