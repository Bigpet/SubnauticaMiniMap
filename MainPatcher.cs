using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Harmony;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

namespace SubnauticaMiniMap
{
    class MainPatcher
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("de.petertissen.subnautica.minimap.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += MainPatcher.OnSceneLoaded;
            f = System.IO.File.Open(string.Format("{0}\\QMods\\MiniMap\\modtest.txt", Environment.CurrentDirectory), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            //f = System.IO.File.Open(@"H:\SteamLibrary\steamapps\common\Subnautica\QMods\SubnauticaMap\Bars_dmp\modtest.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(f);
            sw.WriteLine("ModStart");
            //Debug.Log("MiniMapMod patcher start");
            sw.WriteLine("ModOptions");
            try
            {
                var options = new ModOptions();
                sw.WriteLine("ModOptions2");
                options.Name = "Mini Map Options";
                var slider = new ModSliderOption("sldtest", "Slider Option", 20, 100, 50);
                sw.WriteLine("ModOptions3");
                var checkbox = new ModToggleOption("tgltest", "Toggle Opt", false);
                sw.WriteLine("ModOptions4");
                var choice = new ModChoiceOption("choicetest", "Choice", new[] { "a option", "b option" }, 0);
                sw.WriteLine("ModOptions5");

                options.Options.Add(slider);
                sw.WriteLine("ModOptions6");

                options.Options.Add(checkbox);
                sw.WriteLine("ModOptions7");
                options.Options.Add(choice);
                sw.WriteLine("ModOptions8");
                OptionsPatcher.modOptions.Add(options);
                sw.WriteLine("ModOptions9");
                OptionsPatcher.Patch(harmony);
                sw.WriteLine("ModOptions10");

            }
            catch (Exception e)
            {
                sw.WriteLine(e.StackTrace);
            }
            sw.WriteLine("OptionsPatched");
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
