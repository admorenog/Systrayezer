using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Systrayezer.Config;

namespace Systrayezer
{
    public partial class Main : Form
    {
        bool hidden = false;
        private KeyboardHook hook = new KeyboardHook();
        public Main()
        {
            InitializeComponent();
            UserConfig userConfig = new UserConfig();
            // If the user checks "apply hotkeys at start" we should apply the hotkeys
            // here, maybe we can add an startup access to start this minimized or hidden.
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_Dispose(bool disposing)
        {
            this.Visible = false;
        }
        
        private void Tray_doubleclick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void BtnApplyHotkeys_Click(object sender, EventArgs e)
        {
            Collection<Config.Binding> bindings = UserConfig.config.bindings;

            for(int idxBinding = 0; idxBinding < bindings.Count; idxBinding++)
            {
                Config.Binding binding = bindings.ElementAt(idxBinding);
                
                hook.KeyPressed += new EventHandler<KeyPressedEventArgs>((object eventSender, KeyPressedEventArgs ev) =>
                {
                    if (!binding.hidden)
                    {
                        ExternalWindowManager.hideWindow(binding.app);
                    }
                    else
                    {
                        ExternalWindowManager.showWindow(binding.app);
                    }
                    binding.hidden = !binding.hidden;
                });

                binding.id = hook.RegisterHotKey(binding.GetCombinationOfModifierKeys(), binding.key);
            }

        }
    }
}
