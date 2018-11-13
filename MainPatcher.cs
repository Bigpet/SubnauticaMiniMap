using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Harmony;
using UnityEngine.SceneManagement;
using System.IO;

namespace SubnauticaMiniMap
{
    class MainPatcher
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("de.petertissen.subnautica.minimap.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += MainPatcher.OnSceneLoaded;
            f = System.IO.File.Open(@"H:\SteamLibrary\steamapps\common\Subnautica\QMods\SubnauticaMap\Bars_dmp\modtest.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(f);
            sw.WriteLine("ModStart");
            sw.Flush();
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main")
            {
                MiniMap.Load();
            }
        }

        public static FileStream f;
        public static StreamWriter sw;
    }
}
