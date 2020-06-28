# Prism.Plugin.Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some new implementations of both Prism's Navigation & Dialog Services.

| Platform | Build Status |
| -------- | ------ |
| VSTS Build | [![Build Status](https://dev.azure.com/dansiegel/Prism.Plugins/_apis/build/status/dansiegel.Prism.Plugin.Popups?branchName=master)](https://dev.azure.com/dansiegel/Prism.Plugins/_build/latest?definitionId=43?branchName=master) |
<!-- | AppCenter Android | [![Build status](https://build.appcenter.ms/v0.1/apps/0c92b88f-fe1b-42cf-a714-240a0704d184/branches/master/badge)](https://appcenter.ms) |
| AppCenter iOS | [![Build status](https://build.appcenter.ms/v0.1/apps/0a60407d-a075-41cd-a211-31c92d07ec86/branches/master/badge)](https://appcenter.ms) | -->

| Package | NuGet | Sponsor Connect |
| ------- | ------- | ----- |
| [Prism.Plugin.Popups][PluginNuGet] | [![PluginNuGetShield]][PluginNuGet] | [![PluginSponsorConnectShield]][PluginSponsorConnect] |

Want to consume the CI packages? Sign up as a [GitHub sponsor](https://xam.dev/35) and you can access the Sponsor Connect private feed.

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
[PluginSponsorConnect]: https://www.sponsorconnect.dev/package/Prism.Plugin.Popups
[PluginSponsorConnectShield]: https://img.shields.io/endpoint?url=https%3A%2F%2Fsponsorconnect.dev%2Fshield%2FPrism.Plugin.Popups%2Fvpre&logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAFLElEQVQ4jTWUa2wcVx3FfzN3HjuzL9vxa20nrh3XThxn67SOHaUiNrgpKF9oJSoRIlTgE30IVeIbfEGgggIqQioFVUhtqURKaVQSQevQh1pKESFJ3aRkqe16cTZrx45jr5+zszuPHTST5i/dq6t7dc//3HN1jhRUk6BtEdbgYIIrV2IIsY7vezwwepgvjB8bfefsSz9SVf3y9cW1N4rLmx8GUKHm4bkOtVotuivLcrSWgmoCtO1oc2AgQS4XQ1HWo8Ndbc2PdLUl/jQ2shfH9dm2HUrrZbdccYvnPyn+cmFx6Tk+LyEEvu+joAXRzokTcXI5hVTKY3PTI27oPxja1/50X1czt9YsfL+GEDItOxKqrBndh3sTv37xzGbsarH8jKoqgBQBipGDMo9+S2ZiIoFheiTi6oG7Olq+uK+n5Se6rpqW7eHXahi6GgFWXZ+NssfBVpu7M8b4ucnV30qyXA7PQkDp9vs10VCf/LGmBE/EDT1dnzbpaK1H12RMTcXxAtyQoSxjGDFSesDdxk3u6Urz9OvFM29cmH9YVVVc10VIstrZWJ94vSmtfTMeE7EgCGirT9Ld2YXjSZRtG993qFYrWBWHays2Ttli5C6BoQt6MvE9E5fXP65UqtMhObGzveUbbY3xJ0I2yILGpM7xBw/RtbuX4ewe+nv3oukmLc2tmIpEvGZz4eoc+Zs2/bvS9Lfr5JfdzMy88wp4vvB9EnFTH880JdMe0KDByP4+Bu8dYiY/x4uvniFpaCQTSbL9e/jh1w/jbt/id+em+LhQobsRctfWlmZX5T9Qq7rCdZ1rVsU7VdpyZ/PXVwtpUx8e2NPN0a8dw761zObKIo0t7cRjMiP3ZdlhSEi+w/bGDRaXVlhxG6hrbLv8n5mF34OPkCQJv4aladpH9w92vvnkt48/rMq0Jj2b3p1NjB4eYWt7m66dGQb7Opm+cp5kMsXpdz/inp4mvvvYUxw5eKD3+uJSPF+48bYsyTK6ptCzq4mNso1bXst/5cgwa1WBg05NNXnw/nsZPrCf9UpA3a4B6gyZsiOR3j3CoX0dZHaYYcOF8FMk0zTRVIHr+dh2pWPm3PNFT8SIKwGtaY25WxZNTc34ngNBgKKbbOQv8tlUjjIpxkeHOfnyW9eee+29sdXV1YKsaTpC0bDKLn39fUO7Dw1hWxUmc5/xwmt/Y3auSF3KoC6dQhYqNd+j6rik4zFKyzf4X8nl9LuXng3BIoaGYUTGdj0Px/E6/vnKyaKhwtm/TrCxtsLwwftAGJFLjo6PU11bZP16DiHDM3/8cOb5iU+yUKve8bR8Jy00VYXAn3/1z3/5x/kLF2nYO8YjT/2C9XSWDz5dRpMDZAIURbBVtlFkqDj+cgiWTCQIpYsAQ2dEJUUuZL7kvP/Y49+hvU5jY3mB/S0aP/v+oxwZG2NppcSNkoXleFhb2+SKpSlkHVkINE27DRhOUUpIEpnmJt7519Vnp6cLft+OGlPvnSK7M0E6bnLh4iRbdgVRXaPDDPjN2UkuzSz8ShE1bNuORmQ9SZJxHIcvHcry0AMj1KUS5dn8nHjo2PhoV08frqwzNTXFbKHIvoxJ4b+XOXl6kpffnz4Kwb9DycIR/kOUNkIohI8eyvaSmylgWWGn2pGfPv7Vvw8N7MayLD649CmFmxuYsj//5sX8SyUr+LmEuxXU/M/VkgjTJiSGUBRkoTCU7SOm39YhrIZU/ES2u+VUpsF84cnjX/5eb2dmL6jIik5dOo2ihKF6R37ptobA/wFjXTUfqzHjCAAAAABJRU5ErkJggg==
