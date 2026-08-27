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

namespace BibiteFixes
{
    public static class Utils {
        public static BibiteBody GetBody(GameObject obj) {
            return obj.GetComponent<BibitePart>().GetMainBody();
        }

        public static Vector3 GetPosition(BibiteBody body) {
            return body.transform.position;
        }

        public static Vector3 GetPosition(GameObject obj) {
            return obj.transform.position;
        }
    }
}
