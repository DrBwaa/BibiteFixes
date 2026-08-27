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

## Contributing

If there's a specific bug you'd like to see addressed, either in The Bibites itself or in this modpack, feel free to open a well-described issue on github. This is a volunteer effort, and we make no promises to address any issues in a timely manner, or ever.

### Developing BibiteFixes

To build and develop this project locally

- Clone the project
- Open `BibiteFixes.csproj` in your editor of choice and edit the HintPath for `BibitesAssembly.dll` to match wherever the library is located on your system (typically `<BibitesInstall>/The Bibites_Data/Managed/BibitesAssembly.dll`).
- Open a terminal and run `dotnet build`.
- Copy `bin/Debug/net48/BibiteFixes.dll` into `<BibitesInstall>/BepInEx/plugins`. (TODO: Script this)

### Contribution Notes

Please do not use generative AI/LLMs/Agents to contribute to this project. 
Pull requests from robots will be closed.

The maintainers of this project reserve the right to reject and/or remove any contributions, for any reason.

# Attributions

Thanks to overwatch_mercy for putting together the original version of this modpack and writing several fixes.
Thanks to melting_diamond for the BepInEx tutorial and guidance.
Thanks to rogerwrightshoe for `FullMouthFix`.
Thanks to carlosspicywiener for the original versions of several more fixes.
