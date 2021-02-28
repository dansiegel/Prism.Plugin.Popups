# Popups

The Popups plugin provides an extremely lightweight framework for implementing Popup Pages using the [Rg.Plugin.Popup][1] package with [Prism.Forms][2]. To do this we simply provide you with some new implementations of both Prism's Navigation & Dialog Services.

!!! note "Note"
    Install the [Prism.Plugin.Popups][PluginNuGet] NuGet to your project. This has no dependency on a specific DI Container thus allowing your code to work with any container of your choice.

!!! danger "Critical Note"
    This plugin does not remove any platform initialization requirements that [Rg.Plugins.Popup][1] has. Be sure that you have followed all of the [Rg.Plugins.Popup][1] guidelines for initializing each Platform (iOS/Android,etc).

## Support

This project is maintained by Dan Siegel. If this project or others maintained by Dan have helped you please help support the project by [sponsoring Dan](https://xam.dev/sponsor-prism-popups) on GitHub!

[![GitHub Sponsors](https://github.blog/wp-content/uploads/2019/05/mona-heart-featured.png?fit=600%2C315)](https://xam.dev/sponsor-prism-popups)

## NuGet

| Package | NuGet | MyGet |
|-------|:-----:|:------:|
| [Prism.Plugin.Popups][PluginNuGet] | [![Latest Release][PluginNuGetShield]][PluginNuGet] | [![Latest CI Package][PluginSponsorConnectShield]][PluginSponsorConnect] |

Want to consume the CI packages? Sign up as a [GitHub sponsor](https://xam.dev/35) and you can access the Sponsor Connect private feed.

[1]: https://github.com/rotorgames/Rg.Plugins.Popup
[2]: https://github.com/PrismLibrary/Prism

[PluginNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups
[PluginNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.svg
[PluginSponsorConnect]: https://www.sponsorconnect.dev/nuget/package/Prism.Plugin.Popups
[PluginSponsorConnectShield]: https://img.shields.io/endpoint?url=https%3A%2F%2Fsponsorconnect.dev%2Fshield%2FPrism.Plugin.Popups%2Fvpre
