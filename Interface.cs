using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UIScripts;
using ManagementScripts;
using UnityEngine;

namespace BibiteFixes
{
    [HarmonyPatch(typeof(BibiteTemplateSelectorPanel), "OpenSaveFolder")]
    public static class LinuxOpenTemplateFolderFix {
        static bool Prefix() {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))  {
                return true;
            }
            Process.Start("xdg-open", $"\"{SaveSystem.savedBibitePath}\"");
            return false;
        }
    }
}
