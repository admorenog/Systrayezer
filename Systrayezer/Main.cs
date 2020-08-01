using System;
using System.Windows.Forms;

namespace Systrayezer
{
    public partial class Main : Form
    {
        bool hidden = false;
        KeyboardHook hook = new KeyboardHook();
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

        private void btnApplyHotkeys_Click(object sender, EventArgs e)
        {
            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(Systrayezer.ModifierKeys.Control | Systrayezer.ModifierKeys.Alt, Keys.F12);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();

            // We should detect the window not only by name, because there are applications
            // that changes their name with the status of the process.
            if(!hidden)
            {
                ExternalWindowManager.hideWindow("Descargas");
            }
            else
            {
                ExternalWindowManager.showWindow("Descargas");
            }
            hidden = !hidden;
        }
    }
}
