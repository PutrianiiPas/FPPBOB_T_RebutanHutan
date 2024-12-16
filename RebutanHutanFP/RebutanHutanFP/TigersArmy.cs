using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace RebutanHutanFP;

// class lawan player 'Lion'
public class TigersArmy
{
    private readonly Form _gameForm;
    private readonly Random _randNum;
    private readonly List<PictureBox> _tigerAttacks;
    private readonly System.Windows.Forms.Timer _tigerArmyTimer;
    private readonly SoundPlayer _collisionSound;

    public TigersArmy(Form form)
    {
        _gameForm = form;
        _randNum = new Random();
        _tigerAttacks = new List<PictureBox>();

        // inisialisasi timer utk nambah tiger setiap 12 detik
        _tigerArmyTimer = new System.Windows.Forms.Timer()
        {
            Interval = 12000
        };
        _tigerArmyTimer.Tick += AddTiger;
        _tigerArmyTimer.Start();

        // inisialisasi SoundPlayer utk sound efek weapon, menjaga stream tetap hidup
        _collisionSound = new SoundPlayer(new MemoryStream(Properties.Resources.hit_sounds));
    }

    public void HandleTigerMovements(ref int lionPlayerHealth, ref int lionScore)
    {
        foreach (Control x in _gameForm.Controls)
        {
            if (x is PictureBox tiger && (string)tiger.Tag == "tigers")
            {
                var lionPlayer = _gameForm.Controls["lionPlayer"];

                // cek jika tiger bertabrakan dgn lion
                if (lionPlayer != null && lionPlayer.Bounds.IntersectsWith(tiger.Bounds))
                {
                    lionPlayerHealth -= 1;
                }

                var lionBounds = lionPlayer?.Bounds ?? Rectangle.Empty;

                // gerakan tiger menuju lion
                if (tiger.Left > lionBounds.Left)
                {
                    tiger.Left -= 3;
                    tiger.Image = Properties.Resources.tiger_attack_left;
                }
                if (tiger.Left < lionBounds.Left)
                {
                    tiger.Left += 3;
                    tiger.Image = Properties.Resources.tiger_attack_right;
                }
                if (tiger.Top > lionBounds.Top)
                {
                    tiger.Top -= 3;
                    tiger.Image = Properties.Resources.tiger_attack_up;
                }
                if (tiger.Top < lionBounds.Top)
                {
                    tiger.Top += 3;
                    tiger.Image = Properties.Resources.tiger_attack_down;
                }

                // cek tabrakan dgn bones
                foreach (Control bone in _gameForm.Controls)
                {
                    if (bone is PictureBox && (string)bone.Tag == "bones" && tiger.Bounds.IntersectsWith(bone.Bounds))
                    {
                        _collisionSound.Play();
                        lionScore++;

                        _gameForm.Controls.Remove(bone);
                        bone.Dispose();

                        _gameForm.Controls.Remove(tiger);
                        tiger.Dispose();

                        _tigerAttacks.Remove(tiger);
                        MakeTiger();
                        break;
                    }
                }
            }
        }
    }

    public void MakeTiger()
    {
        PictureBox tiger = new PictureBox
        {
            Tag = "tigers",
            Image = Properties.Resources.tiger_attack_down,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Size = new Size(80, 80),
            BackColor = Color.Transparent
        };

        bool isOverlapping;
        do
        {
            // posisi random: 0 = kiri, 1 = kanan, 2 = atas, 3 = bawah
            int edge = _randNum.Next(0, 4);

            switch (edge)
            {
                case 0: // kiri
                    tiger.Left = -tiger.Width;
                    tiger.Top = _randNum.Next(0, _gameForm.ClientSize.Height - tiger.Height);
                    break;
                case 1: // kanan
                    tiger.Left = _gameForm.ClientSize.Width;
                    tiger.Top = _randNum.Next(0, _gameForm.ClientSize.Height - tiger.Height);
                    break;
                case 2: // atas
                    tiger.Top = -tiger.Height;
                    tiger.Left = _randNum.Next(0, _gameForm.ClientSize.Width - tiger.Width);
                    break;
                case 3: // bawah
                    tiger.Top = _gameForm.ClientSize.Height;
                    tiger.Left = _randNum.Next(0, _gameForm.ClientSize.Width - tiger.Width);
                    break;
            }

            // check overlap dgn tiger lain
            isOverlapping = _tigerAttacks.Any(existingTiger => tiger.Bounds.IntersectsWith(existingTiger.Bounds));
        } while (isOverlapping);

        _tigerAttacks.Add(tiger);
        _gameForm.Controls.Add(tiger);
    }

    public void ResetTigersArmy()
    {
        foreach (PictureBox tiger in _tigerAttacks)
        {
            _gameForm.Controls.Remove(tiger);
            tiger.Dispose();
        }

        _tigerAttacks.Clear();

        // nambahin 3 tiger awal
        for (int i = 0; i < 3; i++)
        {
            MakeTiger();
        }
    }

    private void AddTiger(object sender, EventArgs e)
    {
        MakeTiger();
    }
}