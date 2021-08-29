using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using HarmonyLib;
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
            var harmony = new Harmony("de.petertissen.subnautica.minimap.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += MainPatcher.OnSceneLoaded;

            dbg_log_file = System.IO.File.Open(string.Format("{0}\\QMods\\MiniMap\\modtest.txt", Environment.CurrentDirectory), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            //dbg_log_file = System.IO.File.Open(@"H:\SteamLibrary\steamapps\common\Subnautica\QMods\SubnauticaMap\Bars_dmp\modtest.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            dbg_log = new StreamWriter(dbg_log_file);
            writeDebugLine("MiniMap ModStart");
            AddOptions(harmony);
        }

        public static void writeDebugLine(string input)
        {
            if(dbg_log != null)
            {
                dbg_log.WriteLine(input);
                dbg_log.Flush();
            }
        }

        /**
         * Add options in the "Mod" Tab, based on the SMLHelper
         */
        private static void AddOptions(Harmony harmony)
        {
            try
            {
                var options = new ModOptions();
                writeDebugLine("Adding ModOptions");
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
                writeDebugLine("exception during Adding ModOpting");
                writeDebugLine(e.StackTrace);
            }
            writeDebugLine("OptionsPatched");
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main")
            {
                MiniMap.Load();
            }
        }

        private static FileStream dbg_log_file = null;
        private static StreamWriter dbg_log = null;
    }
}
