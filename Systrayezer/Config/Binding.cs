﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Systrayezer.Config
{
    class Binding : IConfigSetting
    {
        int IConfigSetting.Type { get => ConfigSettings.TypeBinding; }

        public ModifierKeys[] modifiers = new ModifierKeys[0];
        public Keys key { get; set; }
        public string getAppBy { get; set; }
        public string app { get; set; }
        public bool hidden { get; set; } = false;
        public int id { get; set; }
        public Binding(XElement configLine)
        {
            string[] modifiersAsString = configLine.Elements()
                .Where(x => x.Name == "modifiers").Elements()
                .Select(x => x.Value).ToArray();
            modifiers = new ModifierKeys[modifiersAsString.Length];
            for (int idxModifier = 0; idxModifier < modifiersAsString.Length; idxModifier++)
            {
                switch(modifiersAsString[idxModifier])
                {
                    case "ctrl":  modifiers[idxModifier] = ModifierKeys.Control; break;
                    case "alt":   modifiers[idxModifier] = ModifierKeys.Alt; break;
                    case "shift": modifiers[idxModifier] = ModifierKeys.Shift; break;
                    case "win":   modifiers[idxModifier] = ModifierKeys.Win; break;
                    default: throw new Exception(
                            "modifier not found " + modifiers[idxModifier] +
                            ". Possible values are ctrl, alt, shift and win."
                        );
                }
            }

            string assignedKey = configLine.Elements().Where(x => x.Name == "key").First().Value;
            Keys keyToSet;
            try
            {
                Enum.TryParse(assignedKey, out keyToSet);
            }
            catch (Exception)
            {
                throw new Exception("Cannot find the key " + assignedKey);
            }

            key = keyToSet;
            getAppBy = configLine.Elements().Where(x => x.Name == "app").First().Attribute("refBy").Value;
            app = configLine.Elements().Where(x => x.Name == "app").First().Value;
        }

        public ModifierKeys GetCombinationOfModifierKeys()
        {
            ModifierKeys combination = 0;
            foreach(ModifierKeys modifier in modifiers)
            {
                combination |= modifier;
            }
            return combination;
        }
    }
}