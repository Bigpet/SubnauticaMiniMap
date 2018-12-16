using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubnauticaMiniMap
{
    //mostly copied from https://github.com/SMLHelper/SMLHelper/blob/2.x/SMLHelper/
    //to avoid either a) adding an external depedency  b) conflicting with versions other mods use

    public enum ModOptionType
    {
        Slider,
        Toggle,
        Choice
    }
    public class ChoiceChangedEventArgs : EventArgs
    {
        public string Id { get; }
        public int Index { get; }
        public ChoiceChangedEventArgs(string id, int index)
        {
            Id = id;
            Index = index;
        }
    }

    public class SliderChangedEventArgs : EventArgs
    {
        public string Id { get; }
        public float Value { get; }
        public SliderChangedEventArgs(string id, float value)
        {
            Id = id;
            Value = value;
        }
    }

    public class ToggleChangedEventArgs : EventArgs
    {
        public string Id { get; }
        public bool Value { get; }
        public ToggleChangedEventArgs(string id, bool value)
        {
            Id = id;
            Value = value;
        }
    }

    public class ModOptions
    {
        public string Name = "";
        public List<ModOption> Options = new List<ModOption>();

        protected event EventHandler<SliderChangedEventArgs> SliderChanged;
        protected event EventHandler<ToggleChangedEventArgs> ToggleChanged;
        protected event EventHandler<ChoiceChangedEventArgs> ChoiceChanged;

        internal void OnSliderChange(string id, float value)
        {
            SliderChanged(this, new SliderChangedEventArgs(id, value));
        }

        internal void OnToggleChange(string id, bool value)
        {
            ToggleChanged(this, new ToggleChangedEventArgs(id, value));
        }

        internal void OnChoiceChange(string id, int indexValue)
        {
            ChoiceChanged(this, new ChoiceChangedEventArgs(id, indexValue));
        }

    }

    public class ModOption
    {
        public ModOptionType Type;
        public string Label;
        public string Id;

        internal ModOption(ModOptionType type, string label, string id)
        {
            Type = type;
            Label = label;
            Id = id;
        }
    }
    public class ModSliderOption : ModOption
    {
        public float MinValue { get; }
        public float MaxValue { get; }
        public float Value { get; }

        internal ModSliderOption(string id, string label, float minValue, float maxValue, float value) : base(ModOptionType.Slider, label, id)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }
    }

    public class ModToggleOption : ModOption
    {
        public bool Value { get; }
        internal ModToggleOption(string id, string label, bool value) : base(ModOptionType.Toggle, label, id)
        {
            Value = value;
        }
    }

    public class ModChoiceOption : ModOption
    {
        public string[] Options { get; }
        public int Index { get; }

        internal ModChoiceOption(string id, string label, string[] options, int index) : base(ModOptionType.Choice, label, id)
        {
            Options = options;
            Index = index;
        }
    }
}
