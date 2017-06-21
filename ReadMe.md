# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some friendly extensions for the INavigationService so that you can navigate in a Prism friendly manner. Note that this does not currently support deep linking.

![Current Build][buildStatus]

| Package | Version |
| ------- | ------- |
| [Prism.Plugin.Popups.Autofac][11] | [![21]][11] |
| [Prism.Plugin.Popups.DryIoc][12] | [![22]][12] |
| [Prism.Plugin.Popups.Ninject][13] | [![23]][13] |
| [Prism.Plugin.Popups.Unity][14] | [![24]][14] |

## Support

If this project helped you reduce time to develop and made your app better, please help support this project.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.me/dansiegel)

## Usage

### Version 2.X

Version 2.X deprecates the use of all but the `ClearPopupStackAsync` extension method. You can now register a `PopupNavigationService` which uses the base Prism PageNavigationService and adds support for Pushing and Popping PopupPage's. To use the `PopupNavigationService` see the registration example below for the DI container of your choice.

#### Autofac

```cs
protected override void RegisterTypes()
{
    var builder = new ContainerBuilder();
    builder.RegisterPopupNavigatioService();
    builder.UpdateContainer( Container );
}
```

#### All other containers

```cs
protected override void RegisterTypes()
{
    Container.RegisterPopupNavigationService();
}
```

It's worth noting that there is a generic overload for the registration method that accepts any type that inherits from `PopupPageNavigationServiceBase` in the event that you have custom logic you need to execute in the NavigationService.

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

[11]: https://www.nuget.org/packages/Prism.Plugin.Popups.Autofac
[12]: https://www.nuget.org/packages/Prism.Plugin.Popups.DryIoc
[13]: https://www.nuget.org/packages/Prism.Plugin.Popups.Ninject
[14]: https://www.nuget.org/packages/Prism.Plugin.Popups.Unity

[21]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Autofac.svg
[22]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.DryIoc.svg
[23]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Ninject.svg
[24]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.Unity.svg

[buildStatus]: https://avantipoint.visualstudio.com/_apis/public/build/definitions/9ae3c52d-a8d5-4184-b4fe-94f6625d7f93/10/badge
