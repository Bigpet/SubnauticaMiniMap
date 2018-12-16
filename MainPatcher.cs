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
        /**
         * This function is the main entry point for QModManager.
         * It uses Harmony to make the main game call into the mods code.
         */
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("de.petertissen.subnautica.minimap.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += MainPatcher.OnSceneLoaded;

            dbg_log_file = System.IO.File.Open(string.Format("{0}\\QMods\\MiniMap\\modtest.txt", Environment.CurrentDirectory), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            //dbg_log_file = System.IO.File.Open(@"H:\SteamLibrary\steamapps\common\Subnautica\QMods\SubnauticaMap\Bars_dmp\modtest.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            dbg_log = new StreamWriter(dbg_log_file);
            dbg_log.WriteLine("ModStart");
            AddOptions(harmony);
            dbg_log.Flush();
        }

        /**
         * Add options in the "Mod" Tab, based on the SMLHelper
         */
        private static void AddOptions(HarmonyInstance harmony)
        {
            try
            {
                var options = new ModOptions();
                dbg_log.WriteLine("ModOptions");
                options.Name = "Mini Map Options";
                var slider = new ModSliderOption("sldtest", "Slider Option", 20, 100, 50);
                var checkbox = new ModToggleOption("tgltest", "Toggle Opt", false);
                var choice = new ModChoiceOption("choicetest", "Choice", new[] { "a option", "b option" }, 0);

                options.Options.Add(slider);

                options.Options.Add(checkbox);
                options.Options.Add(choice);
                OptionsPatcher.modOptions.Add(options);
                OptionsPatcher.Patch(harmony);
            }
            catch (Exception e)
            {
                dbg_log.WriteLine(e.StackTrace);
            }
            dbg_log.WriteLine("OptionsPatched");
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main")
            {
                MiniMap.Load();
            }
        }

        public static FileStream dbg_log_file;
        public static StreamWriter dbg_log;
    }
}
