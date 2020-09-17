using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using Systrayezer.Windows;

namespace Systrayezer.Config
{
    class Binding : IConfigSetting
    {
        int IConfigSetting.Type { get => ConfigSettings.TypeBinding; }

        private Systrayezer.Windows.ModifierKeys[] modifiers = new Systrayezer.Windows.ModifierKeys[0];
        public Keys key { get; set; }
        public string getAppBy { get; set; }
        public string app { get; set; }
        public bool hidden { get; set; } = false;
        public int eventKeyId { get; set; } = 0;
        public bool autostart { get; set; } = false;
        public bool starthide { get; set; } = false;
        public IntPtr _restorePosition { get; set; } = ExternalWindowManager.HWND_TOPMOST;
        public string restorePosition { get { return getRestorePosition(); } }
        public bool systray { get; set; } = true;
        public bool applied { get; set; } = false;
        public string Hotkey { get { return GetModifiers() + "+" + GetKeyUnicode(); } }
        public string application { get { return getAppBy + "(" + app + ")"; } }
        public string messageStatus { get; set; } = "";

        private KeyboardHook hook;

        public Collection<IntPtr> windowHandlers { get; set; } = new Collection<IntPtr>();

        public Binding(XElement configLine)
        {
            string[] modifiersAsString = configLine.Elements()
                .Where(x => x.Name == "modifiers").Elements()
                .Select(x => x.Value).ToArray();
            modifiers = new Systrayezer.Windows.ModifierKeys[modifiersAsString.Length];
            for (int idxModifier = 0; idxModifier < modifiersAsString.Length; idxModifier++)
            {
                switch(modifiersAsString[idxModifier])
                {
                    case "ctrl":  modifiers[idxModifier] = Systrayezer.Windows.ModifierKeys.Control; break;
                    case "alt":   modifiers[idxModifier] = Systrayezer.Windows.ModifierKeys.Alt; break;
                    case "shift": modifiers[idxModifier] = Systrayezer.Windows.ModifierKeys.Shift; break;
                    case "win":   modifiers[idxModifier] = Systrayezer.Windows.ModifierKeys.Win; break;
                    default: throw new Exception(
                            "modifier not found " + modifiers[idxModifier] +
                            ". Possible values are ctrl, alt, shift and win."
                        );
                }
            }


            string assignedKey = configLine.Elements().Where(x => x.Name == "key").First().Value.ToUpper();

            key = getKey(assignedKey);
            getAppBy = configLine.Elements().Where(x => x.Name == "app").First().Attribute("refBy").Value;
            app = configLine.Elements().Where(x => x.Name == "app").First().Value;
            autostart = bool.Parse(configLine.Elements().Where(x => x.Name == "autostart").First().Value);
            starthide = bool.Parse(configLine.Elements().Where(x => x.Name == "starthide").First().Value);
            systray = bool.Parse(configLine.Elements().Where(x => x.Name == "systray").First().Value);

            //TODO: make this async to search the window if not exists and updating the messageStatus property
            if (windowHandlers.Count() == 0)
            {
                switch (getAppBy)
                {
                    case "WindowName": windowHandlers = ExternalWindowManager.GetAllWindowByCaption(app); break;
                    case "ProcessName": windowHandlers = ExternalWindowManager.GetAllWindowsFromProcessName(app); break;
                }
            }
        }


        string GetKeyUnicode()
        {
            var buf = new StringBuilder(256);
            var keyStates = new byte[256];
            int vk = KeyInterop.VirtualKeyFromKey((Key)key);
            ExternalWindowManager.ToUnicodeEx(vk, 0, keyStates, buf, buf.Capacity, 0, InputLanguage.CurrentInputLanguage.Handle);
            return buf.ToString();
        }
        private Keys getKey(string assignedKey)
        {
            Keys keyToSet;
            try
            {
                if (assignedKey.Length == 1)
                {
                    char asignedChar = assignedKey[0];
                    var keyCode = ExternalWindowManager.VkKeyScanEx(asignedChar, InputLanguage.CurrentInputLanguage.Handle);
                    keyToSet = (Keys)(keyCode & 0xFF);
                }
                else
                {
                    Enum.TryParse(assignedKey, out keyToSet);
                }
            }
            catch (Exception)
            {
                throw new Exception("Cannot find the key " + assignedKey);
            }
            return keyToSet;
        }

        public Systrayezer.Windows.ModifierKeys GetCombinationOfModifierKeys()
        {
            Systrayezer.Windows.ModifierKeys combination = 0;
            foreach(Systrayezer.Windows.ModifierKeys modifier in modifiers)
            {
                combination |= modifier;
            }
            return combination;
        }

        private string GetModifiers()
        {
            string combination = "";
            foreach (Systrayezer.Windows.ModifierKeys modifier in modifiers)
            {
                string modifierName = "";

                switch(modifier)
                {
                    case Systrayezer.Windows.ModifierKeys.Alt: modifierName = "alt"; break;
                    case Systrayezer.Windows.ModifierKeys.Control: modifierName = "ctrl"; break;
                    case Systrayezer.Windows.ModifierKeys.Shift: modifierName = "shift"; break;
                    case Systrayezer.Windows.ModifierKeys.Win: modifierName = "win"; break;
                }
                if(combination != string.Empty)
                {
                    combination += "+";
                }
                combination += modifierName;
            }
            return combination;
        }

        public void CreateSystray()
        {
            NotifyIcon trayIcon = new NotifyIcon();

            try {
                Icon ico = ExternalWindowManager.GetAppIcon(windowHandlers.ElementAt(0));
                trayIcon.Icon = ico;
            }
            catch (Exception) { }

            trayIcon.Text = app;
            trayIcon.Visible = true;

            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItemRestore = new MenuItem("Restore", (object sender, EventArgs ev) => {
                if(hidden) {
                    hidden = false;
                    ExternalWindowManager.showWindows(windowHandlers);
                }
            });
            MenuItem menuItemHide = new MenuItem("Hide systray", (object sender, EventArgs ev) => {
                trayIcon.Visible = false;
            });
            MenuItem menuItemClose = new MenuItem("Close", (object sender, EventArgs ev) => {
                foreach (IntPtr windowHandler in windowHandlers)
                {
                    ExternalWindowManager.CloseWindow(windowHandler);
                }
                trayIcon.Dispose();
                hook.UnRegisterHotKey(eventKeyId);
            });

            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItemRestore, menuItemHide, menuItemClose });
            trayIcon.ContextMenu = contextMenu;
            trayIcon.Click += new EventHandler((object sender, EventArgs e) => {
                if (hidden) {
                    hidden = false;
                    ExternalWindowManager.showWindows(windowHandlers);
                }
            });
        }

        public void apply()
        {
            hook = new KeyboardHook();
            if (!applied)
            {
                try
                {
                    eventKeyId = hook.RegisterHotKey(GetCombinationOfModifierKeys(), key);
                    hook.KeyPressed += new EventHandler<KeyPressedEventArgs>((object eventSender, KeyPressedEventArgs ev) => {
                        if (!hidden)
                        {
                            ExternalWindowManager.hideWindows(windowHandlers);
                            messageStatus = "hidden";
                        }
                        else
                        {
                            ExternalWindowManager.showWindows(windowHandlers);
                            messageStatus = "show";
                        }
                        hidden = !hidden;
                        Main.datagrid.Refresh();
                    });

                    if (starthide)
                    {
                        hidden = true;
                        ExternalWindowManager.hideWindows(windowHandlers);
                    }

                    if (systray)
                    {
                        CreateSystray();
                    }
                    applied = true;

                    messageStatus = "ok";
                }
                catch(Exception exception)
                {
                    messageStatus = exception.Message;
                }
            }
        }

        private string getRestorePosition()
        {
            string positionAsText = "";
            if(_restorePosition == ExternalWindowManager.HWND_BOTTOM)
            {
                positionAsText = "bottom";
            }
            else if(_restorePosition == ExternalWindowManager.HWND_NOTOPMOST)
            {
                positionAsText = "no top most";
            }
            else if (_restorePosition == ExternalWindowManager.HWND_TOP)
            {
                positionAsText = "top";
            }
            else if (_restorePosition == ExternalWindowManager.HWND_TOPMOST)
            {
                positionAsText = "top most";
            }
            return positionAsText;
        }

    }
}
