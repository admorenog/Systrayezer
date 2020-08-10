using System;

namespace Systrayezer
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            this.Main_Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApplyHotkeys = new System.Windows.Forms.Button();
            this.dataGridBindings = new System.Windows.Forms.DataGridView();
            this.btnAddBinding = new System.Windows.Forms.Button();
            this.btnDelBinding = new System.Windows.Forms.Button();
            this.btnEditBinding = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBindings)).BeginInit();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Systrayezer";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.Tray_doubleclick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItem1.Text = "Close";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // btnApplyHotkeys
            // 
            this.btnApplyHotkeys.Location = new System.Drawing.Point(520, 259);
            this.btnApplyHotkeys.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApplyHotkeys.Name = "btnApplyHotkeys";
            this.btnApplyHotkeys.Size = new System.Drawing.Size(94, 24);
            this.btnApplyHotkeys.TabIndex = 0;
            this.btnApplyHotkeys.Text = "Apply hotkeys";
            this.btnApplyHotkeys.UseVisualStyleBackColor = true;
            this.btnApplyHotkeys.Click += new System.EventHandler(this.BtnApplyHotkeys_Click);
            // 
            // dataGridBindings
            // 
            this.dataGridBindings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBindings.Location = new System.Drawing.Point(13, 13);
            this.dataGridBindings.Name = "dataGridBindings";
            this.dataGridBindings.RowHeadersWidth = 51;
            this.dataGridBindings.Size = new System.Drawing.Size(601, 240);
            this.dataGridBindings.TabIndex = 2;
            // 
            // btnAddBinding
            // 
            this.btnAddBinding.Location = new System.Drawing.Point(13, 259);
            this.btnAddBinding.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddBinding.Name = "btnAddBinding";
            this.btnAddBinding.Size = new System.Drawing.Size(52, 24);
            this.btnAddBinding.TabIndex = 3;
            this.btnAddBinding.Text = "add";
            this.btnAddBinding.UseVisualStyleBackColor = true;
            // 
            // btnDelBinding
            // 
            this.btnDelBinding.Location = new System.Drawing.Point(70, 259);
            this.btnDelBinding.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDelBinding.Name = "btnDelBinding";
            this.btnDelBinding.Size = new System.Drawing.Size(52, 24);
            this.btnDelBinding.TabIndex = 4;
            this.btnDelBinding.Text = "remove";
            this.btnDelBinding.UseVisualStyleBackColor = true;
            // 
            // btnEditBinding
            // 
            this.btnEditBinding.Location = new System.Drawing.Point(127, 259);
            this.btnEditBinding.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditBinding.Name = "btnEditBinding";
            this.btnEditBinding.Size = new System.Drawing.Size(52, 24);
            this.btnEditBinding.TabIndex = 5;
            this.btnEditBinding.Text = "edit";
            this.btnEditBinding.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(199, 259);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 291);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnEditBinding);
            this.Controls.Add(this.btnDelBinding);
            this.Controls.Add(this.btnAddBinding);
            this.Controls.Add(this.dataGridBindings);
            this.Controls.Add(this.btnApplyHotkeys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Systrayezer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keypress);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBindings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Main_Disponse(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Button btnApplyHotkeys;
        private System.Windows.Forms.DataGridView dataGridBindings;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button btnAddBinding;
        private System.Windows.Forms.Button btnDelBinding;
        private System.Windows.Forms.Button btnEditBinding;
        private System.Windows.Forms.TextBox textBox1;
    }
}

