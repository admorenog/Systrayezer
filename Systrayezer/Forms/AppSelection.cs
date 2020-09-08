using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Systrayezer.Windows;

namespace Systrayezer.Forms
{
    public partial class AppSelection : Form
    {
        public AppSelection()
        {
            InitializeComponent();
        }

        public void InitPictureBoxes()
        {
            int x = 0, y = 0, w = 240, h = 135, margin = 12, itemsInRow = 4, spaceForText = 24;

            for (int idxWindow = 0; idxWindow < EditBinding.windows.Length; idxWindow++)
            {
                int idxRow = idxWindow / itemsInRow;
                y = margin * idxRow + h * idxRow + spaceForText * idxRow;
                x += w;
                bool isNewRow = idxWindow % itemsInRow == 0;
                if (isNewRow) x = 0;
                x += margin;
                y += margin;
                PictureBox pictureBox = AddPictureBox(idxWindow, x, y, w, h);
                pictureBox.Click += new EventHandler(EditBinding.pictureBox_Click);
                AddLabel(idxWindow, x, y + h, w, EditBinding.windows[idxWindow].Process.ProcessName, EditBinding.windows[idxWindow].Title);
                ExternalWindowManager.SyncThumbNailsTo(Handle, EditBinding.windows[idxWindow].Handle, pictureBox);
            }

            Height = margin * 2 + y + h + spaceForText + 20;
        }

        private PictureBox AddPictureBox(int idx, int x, int y, int w, int h)
        {
            PictureBox pictureBox = new PictureBox();
            ((ISupportInitialize)(pictureBox)).BeginInit();
            SuspendLayout();
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Name = idx.ToString();
            pictureBox.Size = new Size(w, h);
            pictureBox.Location = new Point(x, y);
            ((ISupportInitialize)(pictureBox)).EndInit();
            Controls.Add(pictureBox);
            return pictureBox;
        }

        private Label AddLabel(int idx, int x, int y, int width, string processName, string title)
        {
            Label label = new Label();
            label.Width = width;
            label.Text = processName + " - " + title;
            label.Name = "label" + idx;
            label.Location = new Point(x, y);
            Controls.Add(label);
            return label;
        }

        private void AppSelection_Load(object sender, EventArgs e)
        {
            InitPictureBoxes();
        }
    }
}
