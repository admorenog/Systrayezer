using System;
using System.Windows.Forms;
using Systrayezer.Forms;
using Systrayezer.Windows;

namespace Systrayezer
{

    public partial class Main : Form
    {
        
        private KeyboardHook hook = new KeyboardHook();

        public static DataGridView datagrid;
        public Main()
        {
            // TODO: check for another instance and try to kill it restoring all hidden windows.
            InitializeComponent();

            new UserConfig();

            UserConfig.config.applyBindings(hook);

            datagrid = dataGridBindings;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dataGridBindings.AutoGenerateColumns = false;
            dataGridBindings.DataSource = UserConfig.config.bindings;
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "HotKey";
            column.Name = "HotKey";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "application";
            column.Name = "Application";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "autostart";
            column.Name = "Auto Start";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "starthide";
            column.Name = "Start Hide";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "systray";
            column.Name = "Systray";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "applied";
            column.Name = "Running";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "hidden";
            column.Name = "Hidden";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "restorePosition";
            column.Name = "Restore position";
            column.ReadOnly = true;
            dataGridBindings.Columns.Add(column);
            dataGridBindings.AutoResizeColumns();
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
            UserConfig.config.applyBindings(hook);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((components != null))
            {
                components.Dispose();
            }
            base.Dispose();
            Application.Exit();
        }

        private void keypress(object sender, KeyEventArgs e)
        {
            var values = Enum.GetValues(typeof(Keys));
            Keys keyToSet = e.KeyCode;
            Console.WriteLine(keyToSet.ToString());
            Console.WriteLine("KeyValue " + e.KeyValue);
            Console.WriteLine("KeyCode " + e.KeyCode);
            Console.WriteLine("KeyData " + e.KeyData);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddBinding_Click(object sender, EventArgs e)
        {
            Form formEditBinding = new EditBinding();
            formEditBinding.ShowDialog();
        }
    }
}
