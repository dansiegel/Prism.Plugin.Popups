# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some friendly extensions for the INavigationService so that you can navigate in a Prism friendly manner. Note that this does not currently support deep linking.

| Platform | Build Status |
| -------- | ------ |
| VSTS Build | ![Current Build][buildStatus] |
| AppCenter iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/0a60407d-a075-41cd-a211-31c92d07ec86/branches/master/badge)](https://appcenter.ms) |

| Package | Version | MyGet |
| ------- | ------- | ----- |
| [Prism.Plugin.Popups.Autofac][AutofacNuGet] | [![AutofacNuGetShield]][AutofacNuGet] | [![AutofacMyGetShield]][AutofacMyGet] |
| [Prism.Plugin.Popups.DryIoc][DryIocNuGet] | [![DryIocNuGetShield]][DryIocNuGet] | [![DryIocMyGetShield]][DryIocMyGet] |
| [Prism.Plugin.Popups.Unity][UnityNuGet] | [![UnityNuGetShield]][UnityNuGet] | [![UnityMyGetShield]][UnityMyGet] |

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

#### Autofac

```cs
protected override void RegisterTypes()
{
    Builder.RegisterPopupNavigatioService();
}
```

#### All other containers

```cs
protected override void RegisterTypes()
{
    Container.RegisterPopupNavigationService();
}
```

It's worth noting that there is a generic overload for the registration method that accepts any type that inherits from `PopupPageNavigationServiceBase` in the event that you have custom logic you need to execute in the NavigationService. The `RegisterPopupNavigationService` method will also register `IPopupNavigation` if it has not yet been registered.

### Version 1.X

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

[AutofacNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups.Autofac
[DryIocNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups.DryIoc
[NinjectNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups.Ninject
[UnityNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups.Unity

[AutofacNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Autofac.svg
[DryIocNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.DryIoc.svg
[NinjectNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Ninject.svg
[UnityNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Unity.svg

[AutofacMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups.Autofac
[DryIocMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups.DryIoc
[NinjectMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups.Ninject
[UnityMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups.Unity

[AutofacMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.Autofac.svg
[DryIocMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.DryIoc.svg
[NinjectMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.Ninject.svg
[UnityMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.Unity.svg

[buildStatus]: https://avantipoint.visualstudio.com/_apis/public/build/definitions/9ae3c52d-a8d5-4184-b4fe-94f6625d7f93/27/badge
