using System;
using System.Diagnostics;
using System.IO;
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
    public static class LinuxOpenBB8FolderFix {
        static bool Prefix() {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))  {
                return true;
            }
            Process.Start("xdg-open", $"\"{SaveSystem.savedBibitePath}\"");
            return false;
        }
    }

    [HarmonyPatch(typeof(ScenarioSelectorPanel), "OpenSaveFolder")]
    public static class LinuxOpenScenarioFolderFix {
        static bool Prefix() {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))  {
                return true;
            }
            string scenarioPath = Path.Combine(Application.persistentDataPath, ScenarioSelectorPanel.DefaultSimulationSettingsPath);
            Process.Start("xdg-open", $"\"{scenarioPath}\"");
            return false;
        }
    }

    [HarmonyPatch(typeof(LoadGamePanel), "OpenSaveFolder")]
    public static class LinuxOpenSavesFolderFix {
        static bool Prefix() {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))  {
                return true;
            }
            Process.Start("xdg-open", $"\"{SaveController.SavePath}\"");
            return false;
        }
    }
}
