using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Systrayezer.Forms
{
    public partial class EditBinding : Form
    {
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
                keyPressedAsText += e.KeyCode;
            }

            tbKeyBinding.Text = keyPressedAsText;
        }
    }
}
