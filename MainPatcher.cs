using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Harmony;

namespace SubnauticaMiniMap
{
    class MainPatcher
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.oldark.subnautica.acceleratedstart.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            string[] lines = { "First line", "Second line", "Third line" };
            System.IO.File.WriteAllLines(@"H:\SteamLibrary\steamapps\common\Subnautica\QMods\SubnauticaMap\Bars_dmp\WriteLines.txt", lines);
        }
    }
}
