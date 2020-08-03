using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace Systrayezer
{
    public partial class Main : Form
    {
        private KeyboardHook hook = new KeyboardHook();
        public Main()
        {
            InitializeComponent();
            new UserConfig();

            Collection<Config.Binding> bindings = UserConfig.config.bindings;

            for (int idxBinding = 0; idxBinding < bindings.Count; idxBinding++)
            {
                Config.Binding binding = bindings.ElementAt(idxBinding);
                if (binding.autostart)
                {
                    applyBinding(binding);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e) {
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
                if(binding.eventKeyId == 0)
                {
                    applyBinding(binding);
                }
            }
        }

        void applyBinding(Config.Binding binding)
        {
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>((object eventSender, KeyPressedEventArgs ev) =>
            {
                if (!binding.hidden)
                {
                    ExternalWindowManager.hideWindows(binding.windowHandlers);
                }
                else
                {
                    ExternalWindowManager.showWindows(binding.windowHandlers);
                }
                binding.hidden = !binding.hidden;
            });

            binding.eventKeyId = hook.RegisterHotKey(binding.GetCombinationOfModifierKeys(), binding.key);

            if (binding.starthide)
            {
                ExternalWindowManager.hideWindows(binding.windowHandlers);
            }

            if (binding.systray)
            {
                binding.CreateSystray(hook);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((components != null))
            {
                components.Dispose();
            }
            base.Dispose();
        }
    }
}
