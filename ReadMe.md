# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some friendly extensions for the INavigationService so that you can navigate in a Prism friendly manner. Note that this does not currently support deep linking.

![Current Build][buildStatus]

| Package | Version |
| ------- | ------- |
| [Prism.Plugin.Popups.Autofac][11] | [![21]][11] |
| [Prism.Plugin.Popups.DryIoc][12] | [![22]][12] |
| [Prism.Plugin.Popups.Ninject][13] | [![23]][13] |
| [Prism.Plugin.Popups.Unity][14] | [![24]][14] |


## Usage

There are three primary extensions added for working with Navigation.

   * ClearPopupStackAsync
   * PopupGoBackAsync
   * PushPopupPageAsync

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
