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
            /*
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            */
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
            this.btnApplyHotkeys = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            // btnApplyHotkeys
            // 
            this.btnApplyHotkeys.Location = new System.Drawing.Point(451, 315);
            this.btnApplyHotkeys.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApplyHotkeys.Name = "btnApplyHotkeys";
            this.btnApplyHotkeys.Size = new System.Drawing.Size(125, 30);
            this.btnApplyHotkeys.TabIndex = 0;
            this.btnApplyHotkeys.Text = "Apply hotkeys";
            this.btnApplyHotkeys.UseVisualStyleBackColor = true;
            this.btnApplyHotkeys.Click += new System.EventHandler(this.BtnApplyHotkeys_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 16);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(547, 283);
            this.dataGridView1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 56);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem1.Text = "Close";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 353);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApplyHotkeys);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Systrayezer";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

