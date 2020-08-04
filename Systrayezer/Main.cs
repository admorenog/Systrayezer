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
                    binding.apply(hook);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Collection<Config.Binding> bindings = UserConfig.config.bindings;
            dataGridBindings.AutoGenerateColumns = true;
            dataGridBindings.DataSource = bindings;
            dataGridBindings.AutoResizeColumns();

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "GetHotkey";
            column.Name = "HotKey";
            //dataGridBindings.Columns.Add(column);
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
                    binding.apply(hook);
                }
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
