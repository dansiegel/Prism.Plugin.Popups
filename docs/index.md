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
| [Prism.Plugin.Popups][PluginNuGet] | [![Latest NuGet][PluginNuGetShield]][PluginNuGet] | [![Latest CI Build][PluginMyGetShield]][PluginMyGet] |

[1]: https://github.com/rotorgames/Rg.Plugins.Popup
[2]: https://github.com/PrismLibrary/Prism

[PluginNuGet]: https://www.nuget.org/packages/Prism.Plugin.Popups
[PluginNuGetShield]: https://img.shields.io/nuget/vpre/Prism.Plugin.Popups.svg
[PluginMyGet]: https://www.myget.org/feed/prism-plugins/package/nuget/Prism.Plugin.Popups
[PluginMyGetShield]: https://img.shields.io/myget/prism-plugins/vpre/Prism.Plugin.Popups.svg
