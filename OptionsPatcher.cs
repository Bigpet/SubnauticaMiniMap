using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SubnauticaMiniMap
{
    //mostly copied from https://github.com/SMLHelper/SMLHelper/blob/2.x/SMLHelper/Patchers/OptionsPanelPatcher.cs
    //to avoid either a) adding an external depedency  b) conflicting with versions other mods use
    class OptionsPatcher
    {
        internal static List<ModOptions> modOptions = new List<ModOptions>();

        internal static void Patch(HarmonyInstance harmony)
        {
            var uGUI_OptionsPanelType = typeof(uGUI_OptionsPanel);
            var thisType = typeof(OptionsPatcher);
            var startMethod = uGUI_OptionsPanelType.GetMethod("AddTabs", BindingFlags.NonPublic | BindingFlags.Instance);

            harmony.Patch(startMethod, null, new HarmonyMethod(thisType.GetMethod("AddTabs_Postfix", BindingFlags.NonPublic | BindingFlags.Static)));
        }

        internal static void AddTabs_Postfix(uGUI_OptionsPanel __instance)
        {
            var optionsPanel = __instance;
            var modsTab = optionsPanel.AddTab("Mods");

            foreach (ModOptions modOption in modOptions)
            {
                optionsPanel.AddHeading(modsTab, modOption.Name);

                foreach (ModOption option in modOption.Options)
                {
                    if (option.Type == ModOptionType.Slider)
                    {
                        var slider = (ModSliderOption)option;

                        optionsPanel.AddSliderOption(modsTab, slider.Label, slider.Value, slider.MinValue, slider.MaxValue, slider.Value,
                            callback: new UnityEngine.Events.UnityAction<float>((float sliderVal) =>
                                modOption.OnSliderChange(slider.Id, sliderVal)));
                    }
                    else if (option.Type == ModOptionType.Toggle)
                    {
                        var toggle = (ModToggleOption)option;

                        optionsPanel.AddToggleOption(modsTab, toggle.Label, toggle.Value,
                            callback: new UnityEngine.Events.UnityAction<bool>((bool toggleVal) =>
                                modOption.OnToggleChange(toggle.Id, toggleVal)));
                    }
                    else if (option.Type == ModOptionType.Choice)
                    {
                        var choice = (ModChoiceOption)option;

                        optionsPanel.AddChoiceOption(modsTab, choice.Label, choice.Options, choice.Index,
                            callback: new UnityEngine.Events.UnityAction<int>((int index) =>
                                modOption.OnChoiceChange(choice.Id, index)));
                    }
                    else
                    {
                        //V2.Logger.Log($"Invalid ModOptionType detected for option: {option.Id}");
                    }
                }
            }
        }
    }
}
