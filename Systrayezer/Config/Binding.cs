using System;
using System.Collections.ObjectModel;
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
        public int eventKeyId { get; set; } = 0;
        public bool autostart { get; set; } = false;
        public bool starthide { get; set; } = false;
        public bool systray { get; set; } = true;
        public Collection<IntPtr> windowHandlers { get; set; } = new Collection<IntPtr>();
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

            string assignedKey = configLine.Elements().Where(x => x.Name == "key").First().Value.ToUpper();
            var values = Enum.GetValues(typeof(Keys));
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
            autostart = bool.Parse(configLine.Elements().Where(x => x.Name == "autostart").First().Value);
            starthide = bool.Parse(configLine.Elements().Where(x => x.Name == "starthide").First().Value);
            systray = bool.Parse(configLine.Elements().Where(x => x.Name == "systray").First().Value);

            if (windowHandlers.Count() == 0)
            {
                switch (getAppBy)
                {
                    case "WindowName": windowHandlers = ExternalWindowManager.GetAllWindowByCaption(app); break;
                    case "ProcessName": windowHandlers = ExternalWindowManager.GetAllWindowsFromProcessName(app); break;
                }
            }
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
