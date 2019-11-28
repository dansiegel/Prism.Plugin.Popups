# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some new implementations of both Prism's Navigation & Dialog Services.

| Platform | Build Status |
| -------- | ------ |
| VSTS Build | [![Build Status](https://dev.azure.com/dansiegel/Prism.Plugins/_apis/build/status/dansiegel.Prism.Plugin.Popups?branchName=master)](https://dev.azure.com/dansiegel/Prism.Plugins/_build/latest?definitionId=43?branchName=master) |
| AppCenter Android | [![Build status](https://build.appcenter.ms/v0.1/apps/0c92b88f-fe1b-42cf-a714-240a0704d184/branches/master/badge)](https://appcenter.ms) |
| AppCenter iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/0a60407d-a075-41cd-a211-31c92d07ec86/branches/master/badge)](https://appcenter.ms) |

| Package | Version | MyGet |
| ------- | ------- | ----- |
| [Prism.Plugin.Popups][PluginNuGet] | [![PluginNuGetShield]][PluginNuGet] | [![PluginMyGetShield]][PluginMyGet] |

## Symbols

Builds are now generated with a symbols package. This will allow users to better debug code when using our symbol server feed. Packages published to NuGet prior to (and including) 2.0.0-pre2 do not have symbols available. You can add the following Symbol Server for all Prism Plugins.

[https://www.myget.org/F/prism-plugins/symbols/](https://www.myget.org/F/prism-plugins/symbols/)

## Support

If this project helped you reduce time to develop and made your app better, please be sure to star the project help support Dan.

[![GitHub Sponsors](https://github.blog/wp-content/uploads/2019/05/mona-heart-featured.png?fit=600%2C315)](https://xam.dev/35)

## Usage

*NOTE: We have changed versioning to now follow the Major.Minor from the Prism version that the Popup Plugin is built against. This should help avoid confusion with compatibility particularly as many issues were being reported because developers were updating to a preview of Prism but remaining on the stable build from the Popups Plugin*

### Getting Started

Install the [Prism.Plugin.Popups][PluginNuGet] NuGet to your project. Notice that this has no dependency on a specific DI Container thus allowing your code to work with any container of your choice.

This plugin does not remove any platform initialization requirements that [Rg.Plugins.Popup][1] has. Be sure that you have followed all of the [Rg.Plugins.Popup][1] guidelines for initializing each Platform (iOS/Android,etc).

To use the plugin you will need to update the Registration for INavigationService. To do this you simply need to add the following:

```cs
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // This updates INavigationService and registers PopupNavigation.Instance
    containerRegistry.RegisterPopupNavigationService();
}
```

It's worth noting that there is a generic overload for the registration method that accepts any type that inherits from `PopupPageNavigationService` in the event that you have custom logic you need to execute in the NavigationService. The `RegisterPopupNavigationService` method will also register `IPopupNavigation` from `PopupNavigation.Instance` for you.

Prism's underlying Page Navigation Service has a dependency on `IPageBehaviorFactory`. In order to handle navigation events from tapping outside of (and closing) the PopupPage, this plugin relies on the `PopupPageBehaviorFactory` which is also registered by the registration method shown above. If you are registering your own custom implementation, be sure to either add the `BackgroundPopupDismissalBehavior` to PopupPages, or simply inherit from the `PopupPageBehaviorFactory`.

**NOTE**: All initializations for Rg.Plugins.Popup should be done in Platform code, the Registrations for the NavigationService should be done in your PrismApplication. No additional initialization is required inside of Prism Modules.

### Navigation

There is nothing different about navigating with Popup Pages than Navigating with normal pages. You can even deep link with Popup Pages. The only difference that you will encounter is that when Navigating from a Popup Page to a normal Xamarin.Forms Page, it will Pop any Popup Pages that are currently in the Popup Stack.

[1]: https://github.com/rotorgames/Rg.Plugins.Popup
[2]: https://github.com/PrismLibrary/Prism

[PluginNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups
[PluginNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.svg
[PluginMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups
[PluginMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.svg
