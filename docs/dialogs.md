# Popup Dialog Service

The Dialog Service in Prism is simply fantastic, however it has one major limitation. Because Dialogs are pushed as content within the currently active ContentPage this results in a limitation in which the NavigationPage within a TabbedPage or NavigationPage will remain visible and accessible. Also if called from a MasterDetailPage's Master, it will be update the Detail but may not close the Master resulting in a situation where you can still access menu items within the Master page.

The Popup Plugin is perfectly situated to solve this problem by swapping out the implementation for the Dialog Service with one which uses a PopupPage to push. This solves the issues mentioned above because a PopupPage will overlay any Navigation Bar and will display even if a MasterDetailPage's Master is currently presented.

```c#
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterPopupDialogService();

    containerRegistry.RegisterDialog<SampleDialog, SampleDialogViewModel>();
}
```

!!! note "Note"
    Besides needing to register the Popup Dialog Service there are ZERO differences at this time for using the Dialog Service.

!!! warning "Warning"
    Popup Pages from Rg.Plugins.Popup are simply a Xamarin.Forms.ContentPage. If you fail to properly initialize Rg.Plugins.Poup or you fail to register the Popup Plugin, Prism will simply push the Popup Page as if it were a Content Page. This may result in the Popup Page being treated as a Modal page or it may be pushed into a Navigation Page. Be sure to see the [Initializing Rg.Plugin.Popup](rgplugins.md) topic for more information.