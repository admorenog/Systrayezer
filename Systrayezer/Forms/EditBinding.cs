using System;
using System.Text;
using System.Windows.Forms;
using Systrayezer.Windows;

namespace Systrayezer.Forms
{
    public partial class EditBinding : Form
    {
        public static Window[] windows;
        private static Form formEditBinding = null;


        public EditBinding()
        {
            InitializeComponent();
        }
        private void keydown(object sender, KeyEventArgs e)
        {
            var values = Enum.GetValues(typeof(Keys));
            Keys keyToSet = e.KeyCode;
            Console.WriteLine(keyToSet.ToString());
            Console.WriteLine("KeyValue " + e.KeyValue);
            Console.WriteLine("KeyCode " + e.KeyCode);
            Console.WriteLine("KeyData " + e.KeyData);

            string keyPressedAsText = "";
            if (e.Control)
            {
                keyPressedAsText += "ctrl+";
            }
            if(e.Shift)
            {
                keyPressedAsText += "shift+";
            }
            if(e.Alt)
            {
                keyPressedAsText += "alt+";
            }
            if(e.KeyCode != Keys.ControlKey)
            {
                var buf = new StringBuilder(256);
                var keyStates = new byte[256];
                if (e.Shift)
                    keyStates[16] = 0x80;

                ExternalWindowManager.ToUnicodeEx(e.KeyValue, 0, keyStates, buf, buf.Capacity, 0, InputLanguage.CurrentInputLanguage.Handle);

                keyPressedAsText += buf.ToString();
            }

            tbKeyBinding.Text = keyPressedAsText;
        }

        private void btnSelectApp_Click(object sender, EventArgs e)
        {
            windows = ExternalWindowManager.GetAllWindows();
            formEditBinding = new AppSelection();
            formEditBinding.Show();
        }

        public static void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Window selectedWindow = windows[int.Parse(pictureBox.Name)];
            tbApp.Text = selectedWindow.Process.ProcessName;
            formEditBinding.Dispose();
            Console.WriteLine("title:{0}, pid:{1}, pname:{2}", selectedWindow.Title, selectedWindow.Process.Id, selectedWindow.Process.ProcessName);
        }
    }
}
