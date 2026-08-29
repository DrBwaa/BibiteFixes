# README

This project contains a variety of bug fixes and performance improvements for The Bibites.

## Getting Started

- Install The Bibites
- Install BepInEx 5.4 (see instructions [here](https://the-bibites.fandom.com/wiki/Modding_Guide_For_Beginners#How_to_install_BepInEx_mods?) or [here](https://docs.bepinex.dev/v5.4.21/articles/user_guide/installation/index.html))
- Download the plugin: `BibiteFixes.dll` from https://github.com/DrBwaa/BibiteFixes/releases.
  - Put the plugin file into `BepInEx/plugins` within your Bibites installation.
- Launch The Bibites with BepInEx (see [instructions](https://docs.bepinex.dev/v5.4.21/articles/user_guide/installation/index.html) for your specific operating system).

### Configuration

To configure the modpack, open `BepInEx/config/bibites.bibitefixes.cfg` (in the game installation directory). If this file doesn't exist, launch the game once (with BepInEx) to generate it.

Configuration options are described within the config file. After making changes, re-launch the game for them to take effect.

### Compatibility

This modpack is built against The Bibites version 0.6.3.1 and BepInEx version 5.4.23.5.

Compatibility with other versions is not guaranteed.

## Fixes Included

- DisableReaper: Reaper trait will no longer appear with Easter-eggs disabled.
- Phero3HeadingEvolvable: The `Phero3Heading` node can now be chosen as an evolved input.
- FullMouthFix: Holding ten objects no longer causes permanent inability to eat anything.
- PherosenseTick: Phero senses now update either every brain period, or every half second (configurable), rather than every single tick.
- PheromoneCost: Phero cost will no longer display a stale value in the UI.
- TPSFix: Low simulation speeds no longer gain extra TPS: TPS stays locked to the expected setting.

## Attributions

See ATTRIBUTIONS.md
