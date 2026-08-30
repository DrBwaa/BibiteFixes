#! /bin/bash

set -euo pipefail

# How to use this script:
#   Make a file called ".env" (the . is important) at the root of the project, right next to ".gitignore"
#   Copy the following line into the new file, replacing the path with the actual location of the installation on your system:
#       export BIBITES_DIR='/path/to/The/Bibites/Installation/The Bibites 0.6.3.1'
#     Remember, this location above should be wherever the game executable is. The same place you installed BepInEx.
#
#   Open a terminal in the project root
#   Run dotnet build
#   Run `scripts/copy_to_install.sh` to copy the newly built .dll from your project into the bepinex plugin folder.


if [ ! -f ./bin/Debug/net48/BibiteFixes.dll ]; then
  echo "BibiteFixes.dll not found. Did you forget to run dotnet build?"
  exit 1
fi

source .env

if [ -z "$BIBITES_DIR" ]; then
  echo "BIBITES_DIR is not set, make sure to export it correctly in .env"
  exit 1
fi


if [ ! -d "$BIBITES_DIR/BepInEx/plugins" ]; then
  echo "$BIBITES_DIR/BepInEx/plugins/ does not exist or is not a directory"
  exit 1
fi

cp ./bin/Debug/net48/BibiteFixes.dll "$BIBITES_DIR/BepInEx/plugins"

echo "BibiteFixes.dll copied successfully."
