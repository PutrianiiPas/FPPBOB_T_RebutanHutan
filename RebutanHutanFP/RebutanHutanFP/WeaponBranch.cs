using RebutanHutanFP.Properties;
using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace RebutanHutanFP
{
    public class WeaponBranch
    {
        public string direction;
        public int branchLeft;
        public int branchTop;

        private int speed = 20;
        private PictureBox _branchPictureBox = new PictureBox();
        private System.Windows.Forms.Timer _branchTimer = new System.Windows.Forms.Timer();

        public void MakeWeaponBranch(Form form)
        {
            Bitmap branchBitmap = Properties.Resources.branch; // Ensure the resource is of type Bitmap
            _branchPictureBox.Image = branchBitmap;

            _branchPictureBox.Size = new Size(branchBitmap.Width / 3, branchBitmap.Height / 3);

            _branchPictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Mengatur agar gambar disesuaikan dalam ukuran PictureBox
            _branchPictureBox.BackColor = Color.Transparent;
            _branchPictureBox.Tag = "branch";
            _branchPictureBox.Left = branchLeft;
            _branchPictureBox.Top = branchTop;
            _branchPictureBox.BringToFront();

            form.Controls.Add(_branchPictureBox);

            _branchTimer.Interval = speed;
            _branchTimer.Tick += new EventHandler(BranchTimerEvent);
            _branchTimer.Start();
        }

        private void BranchTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
                _branchPictureBox.Left -= speed;

            else if (direction == "right")
                _branchPictureBox.Left += speed;

            else if (direction == "up")
                _branchPictureBox.Top -= speed;

            else if (direction == "down")
                _branchPictureBox.Top += speed;

            if (_branchPictureBox.Left < 15 || _branchPictureBox.Left > 1180 || _branchPictureBox.Top < 40 || _branchPictureBox.Top > 780)
            {
                _branchTimer.Stop();
                _branchTimer.Dispose();
                _branchPictureBox.Dispose();
                _branchTimer = null;
                _branchPictureBox = null;
            }
        }
    }
}