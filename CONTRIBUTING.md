## Contributing

If there's a specific bug you'd like to see addressed, either in The Bibites itself or in this modpack, feel free to open a well-described issue on github. 

This is a volunteer effort, and I make no promises to address any issues in a timely manner, or ever.

### Developing BibiteFixes

To build and develop this project locally:

- Clone the project
- Open `BibiteFixes.csproj` in your editor of choice and edit the HintPath for `BibitesAssembly.dll` to match wherever the library is located on your system (typically `<BibitesInstall>/The Bibites_Data/Managed/BibitesAssembly.dll`).
- Open a terminal and run `dotnet build`.
- Copy `bin/Debug/net48/BibiteFixes.dll` into `<BibitesInstall>/BepInEx/plugins`. A script to automate this step exists at `script/copy_to_install.sh`.

Submit all pull requests against the `dev` branch.

### Contribution Notes

Please do not use generative AI/LLMs/Agents to contribute to this project. 

Pull requests from robots will be closed.

The maintainers of this project reserve the right to reject and/or remove any contributions, for any reason.
