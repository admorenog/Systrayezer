﻿using System;

namespace Systrayezer.Forms
{
    partial class EditBinding
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbKeyBinding = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            tbApp = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btnSelectApp = new System.Windows.Forms.Button();
            this.btnSaveBinding = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbKeyBinding
            // 
            this.tbKeyBinding.Location = new System.Drawing.Point(45, 6);
            this.tbKeyBinding.Name = "tbKeyBinding";
            this.tbKeyBinding.ReadOnly = true;
            this.tbKeyBinding.Size = new System.Drawing.Size(100, 20);
            this.tbKeyBinding.TabIndex = 7;
            this.tbKeyBinding.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keydown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Keys";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Application";
            // 
            // tbApp
            // 
            tbApp.Enabled = false;
            tbApp.Location = new System.Drawing.Point(231, 6);
            tbApp.Name = "tbApp";
            tbApp.ReadOnly = true;
            tbApp.Size = new System.Drawing.Size(100, 20);
            tbApp.TabIndex = 9;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "autostart";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(69, 17);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "start hide";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 78);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(58, 17);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "systray";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // btnSelectApp
            // 
            this.btnSelectApp.Location = new System.Drawing.Point(231, 32);
            this.btnSelectApp.Name = "btnSelectApp";
            this.btnSelectApp.Size = new System.Drawing.Size(100, 23);
            this.btnSelectApp.TabIndex = 14;
            this.btnSelectApp.Text = "Select App";
            this.btnSelectApp.UseVisualStyleBackColor = true;
            this.btnSelectApp.Click += new System.EventHandler(this.btnSelectApp_Click);
            // 
            // btnSaveBinding
            // 
            this.btnSaveBinding.Location = new System.Drawing.Point(231, 72);
            this.btnSaveBinding.Name = "btnSaveBinding";
            this.btnSaveBinding.Size = new System.Drawing.Size(100, 23);
            this.btnSaveBinding.TabIndex = 15;
            this.btnSaveBinding.Text = "save";
            this.btnSaveBinding.UseVisualStyleBackColor = true;
            // 
            // EditBinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 102);
            this.Controls.Add(this.btnSaveBinding);
            this.Controls.Add(this.btnSelectApp);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(tbApp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbKeyBinding);
            this.Name = "EditBinding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditBinding";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbKeyBinding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button btnSelectApp;
        private System.Windows.Forms.Button btnSaveBinding;
        private static System.Windows.Forms.TextBox tbApp;
    }
}