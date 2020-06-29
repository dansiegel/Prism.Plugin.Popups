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
[PluginSponsorConnect]: https://www.sponsorconnect.dev/package/Prism.Plugin.Popups
[PluginSponsorConnectShield]: https://img.shields.io/endpoint?url=https%3A%2F%2Fsponsorconnect.dev%2Fshield%2FPrism.Plugin.Popups%2Fvpre&logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAFLElEQVQ4jTWUa2wcVx3FfzN3HjuzL9vxa20nrh3XThxn67SOHaUiNrgpKF9oJSoRIlTgE30IVeIbfEGgggIqQioFVUhtqURKaVQSQevQh1pKESFJ3aRkqe16cTZrx45jr5+zszuPHTST5i/dq6t7dc//3HN1jhRUk6BtEdbgYIIrV2IIsY7vezwwepgvjB8bfefsSz9SVf3y9cW1N4rLmx8GUKHm4bkOtVotuivLcrSWgmoCtO1oc2AgQS4XQ1HWo8Ndbc2PdLUl/jQ2shfH9dm2HUrrZbdccYvnPyn+cmFx6Tk+LyEEvu+joAXRzokTcXI5hVTKY3PTI27oPxja1/50X1czt9YsfL+GEDItOxKqrBndh3sTv37xzGbsarH8jKoqgBQBipGDMo9+S2ZiIoFheiTi6oG7Olq+uK+n5Se6rpqW7eHXahi6GgFWXZ+NssfBVpu7M8b4ucnV30qyXA7PQkDp9vs10VCf/LGmBE/EDT1dnzbpaK1H12RMTcXxAtyQoSxjGDFSesDdxk3u6Urz9OvFM29cmH9YVVVc10VIstrZWJ94vSmtfTMeE7EgCGirT9Ld2YXjSZRtG993qFYrWBWHays2Ttli5C6BoQt6MvE9E5fXP65UqtMhObGzveUbbY3xJ0I2yILGpM7xBw/RtbuX4ewe+nv3oukmLc2tmIpEvGZz4eoc+Zs2/bvS9Lfr5JfdzMy88wp4vvB9EnFTH880JdMe0KDByP4+Bu8dYiY/x4uvniFpaCQTSbL9e/jh1w/jbt/id+em+LhQobsRctfWlmZX5T9Qq7rCdZ1rVsU7VdpyZ/PXVwtpUx8e2NPN0a8dw761zObKIo0t7cRjMiP3ZdlhSEi+w/bGDRaXVlhxG6hrbLv8n5mF34OPkCQJv4aladpH9w92vvnkt48/rMq0Jj2b3p1NjB4eYWt7m66dGQb7Opm+cp5kMsXpdz/inp4mvvvYUxw5eKD3+uJSPF+48bYsyTK6ptCzq4mNso1bXst/5cgwa1WBg05NNXnw/nsZPrCf9UpA3a4B6gyZsiOR3j3CoX0dZHaYYcOF8FMk0zTRVIHr+dh2pWPm3PNFT8SIKwGtaY25WxZNTc34ngNBgKKbbOQv8tlUjjIpxkeHOfnyW9eee+29sdXV1YKsaTpC0bDKLn39fUO7Dw1hWxUmc5/xwmt/Y3auSF3KoC6dQhYqNd+j6rik4zFKyzf4X8nl9LuXng3BIoaGYUTGdj0Px/E6/vnKyaKhwtm/TrCxtsLwwftAGJFLjo6PU11bZP16DiHDM3/8cOb5iU+yUKve8bR8Jy00VYXAn3/1z3/5x/kLF2nYO8YjT/2C9XSWDz5dRpMDZAIURbBVtlFkqDj+cgiWTCQIpYsAQ2dEJUUuZL7kvP/Y49+hvU5jY3mB/S0aP/v+oxwZG2NppcSNkoXleFhb2+SKpSlkHVkINE27DRhOUUpIEpnmJt7519Vnp6cLft+OGlPvnSK7M0E6bnLh4iRbdgVRXaPDDPjN2UkuzSz8ShE1bNuORmQ9SZJxHIcvHcry0AMj1KUS5dn8nHjo2PhoV08frqwzNTXFbKHIvoxJ4b+XOXl6kpffnz4Kwb9DycIR/kOUNkIohI8eyvaSmylgWWGn2pGfPv7Vvw8N7MayLD649CmFmxuYsj//5sX8SyUr+LmEuxXU/M/VkgjTJiSGUBRkoTCU7SOm39YhrIZU/ES2u+VUpsF84cnjX/5eb2dmL6jIik5dOo2ihKF6R37ptobA/wFjXTUfqzHjCAAAAABJRU5ErkJggg==
