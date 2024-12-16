using RebutanHutanFP.Properties;
using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace RebutanHutanFP
{
    // weapon utk player Lion
    public class WeaponBones
    {
        public string direction;
        public int bonesLeft;
        public int bonesTop;

        private int speed = 20;
        private PictureBox _bonesPictureBox = new PictureBox();
        private System.Windows.Forms.Timer _bonesTimer = new System.Windows.Forms.Timer();

        public void MakeWeaponBones(Form form)
        {
            Bitmap bonesBitmap = Properties.Resources.bones; // Ensure the resource is of type Bitmap
            _bonesPictureBox.Image = bonesBitmap;

            // Atur ukuran bones sesuai kebutuhan, misalnya skala 50%
            _bonesPictureBox.Size = new Size(bonesBitmap.Width / 3, bonesBitmap.Height / 3);

            _bonesPictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Mengatur agar gambar disesuaikan dalam ukuran PictureBox
            _bonesPictureBox.BackColor = Color.Transparent;
            _bonesPictureBox.Tag = "bones";
            _bonesPictureBox.Left = bonesLeft;
            _bonesPictureBox.Top = bonesTop;
            _bonesPictureBox.BringToFront();

            form.Controls.Add(_bonesPictureBox);

            _bonesTimer.Interval = speed;
            _bonesTimer.Tick += new EventHandler(BonesTimerEvent);
            _bonesTimer.Start();
        }

        private void BonesTimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
                _bonesPictureBox.Left -= speed;

            else if (direction == "right")
                _bonesPictureBox.Left += speed;

            else if (direction == "up")
                _bonesPictureBox.Top -= speed;

            else if (direction == "down")
                _bonesPictureBox.Top += speed;

            if (_bonesPictureBox.Left < 15 || _bonesPictureBox.Left > 1180 || _bonesPictureBox.Top < 40 || _bonesPictureBox.Top > 780)
            {
                _bonesTimer.Stop();
                _bonesTimer.Dispose();
                _bonesPictureBox.Dispose();
                _bonesTimer = null;
                _bonesPictureBox = null;
            }
        }
    }
}