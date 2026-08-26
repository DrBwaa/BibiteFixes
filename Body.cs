using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ManagementScripts;
using SettingScripts;
using SimulationScripts.BibiteScripts;
using UnityEngine;

namespace CommonFixes
{
    [HarmonyPatch(typeof(BibiteBody), nameof(BibiteBody.BecomeReaper))]
    public static class DisableReaper
    {
        static bool Prefix()
        {
            return UserSettings.AllowEasterEggs.val;
        }
    }
}
