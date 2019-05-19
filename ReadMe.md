# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some friendly extensions for the INavigationService so that you can navigate in a Prism friendly manner. Note that this does not currently support deep linking.

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

If this project helped you reduce time to develop and made your app better, please help support this project.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.me/dansiegel)

## Usage

### Version 2.X

Version 2.X deprecates the use of all but the `ClearPopupStackAsync` extension method. You can now register a `PopupNavigationService` which uses the base Prism PageNavigationService and adds support for Pushing and Popping PopupPage's. To use the `PopupNavigationService` see the registration example below for the DI container of your choice.

Because 2.X simply uses INavigationService one of the benefits you will get is support for Deep Linking like shown here:

```cs
NavigationService.NavigateAsync("MainPage/PopupPageA/PopupPageB");
```

**NOTE** All Container Specific packages have been deprecated due to Prism 7 offering an IOC Abstraction. You only need to install Prism.Plugin.Popups.

#### Getting Started

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

### Version 1.X

**NOTE** This version is DEPRECATED, please use the 2.0 preview. This is not compatible with Prism 7.

There are three primary extensions added for working with Navigation.

* ClearPopupStackAsync
* PopupGoBackAsync (Obsolete in v2.X)
* PushPopupPageAsync (Obsolete in v2.X)

 All three of these contain overloads so that you can pass in a NavigationParameters object, or if you have a single key value pair you can pass it in as shown below for the NavigateCommand.

```cs
public class MyPageViewModel : BindableBase
{
    INavigationService _navigationService { get; }

    public MyPageViewModel( INavigationService navigationService )
    {
        _navigationService = navigationService;
        NavigateCommand = new DelegateCommand( OnNavigateCommandExecuted );
        GoBackCommand = new DelegateCommand( OnGoBackCommandExecuted );
    }

    public DelegateCommand NavigateCommand { get; }

    public DelegateCommand GoBackCommand { get; }

    private async void OnNavigateCommandExecuted()
    {
        await _navigationService.PushPopupPageAsync( "SomePopupPage", "message", "hello from MyPage" );
    }

    private async void OnGoBackCommandExecuted()
    {
        await _navigationService.PopupGoBackAsync();
    }
}
```

[1]: https://github.com/rotorgames/Rg.Plugins.Popup
[2]: https://github.com/PrismLibrary/Prism

[PluginNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups
[PluginNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.svg
[PluginMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups
[PluginMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.svg
