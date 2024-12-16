using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace RebutanHutanFP;

// class lawan player 'Tiger'
public class LionsArmy
{
    private readonly Form _gameForm;
    private readonly Random _randNum;
    private readonly List<PictureBox> _lionAttacks;
    private readonly System.Windows.Forms.Timer _lionArmyTimer;
    private readonly SoundPlayer _collisionSound;

    public LionsArmy(Form form)
    {
        _gameForm = form;
        _randNum = new Random();
        _lionAttacks = new List<PictureBox>();

        // inisialisasi timer utk nambah lion setiap 12 detik
        _lionArmyTimer = new System.Windows.Forms.Timer()
        {
            Interval = 12000
        };
        _lionArmyTimer.Tick += AddLion;
        _lionArmyTimer.Start();

        // inisialisasi SoundPlayer utk sound efek weapon, menjaga stream tetap hidup
        _collisionSound = new SoundPlayer(new MemoryStream(Properties.Resources.hit_sounds));
    }

    public void HandleLionMovements(ref int tigerPlayerHealth, ref int tigerScore)
    {
        foreach (Control x in _gameForm.Controls)
        {
            if (x is PictureBox lion && (string)lion.Tag == "lions")
            {
                var tigerPlayer = _gameForm.Controls["tigerPlayer"];

                // cek jika lion bertabrakan dgn tiger
                if (tigerPlayer != null && tigerPlayer.Bounds.IntersectsWith(lion.Bounds))
                {
                    tigerPlayerHealth -= 1;
                }

                var tigerBounds = tigerPlayer?.Bounds ?? Rectangle.Empty;

                // gerakan lion menuju tiger
                if (lion.Left > tigerBounds.Left)
                {
                    lion.Left -= 3;
                    lion.Image = Properties.Resources.lion_attack_left;
                }
                if (lion.Left < tigerBounds.Left)
                {
                    lion.Left += 3;
                    lion.Image = Properties.Resources.lion_attack_right;
                }
                if (lion.Top > tigerBounds.Top)
                {
                    lion.Top -= 3;
                    lion.Image = Properties.Resources.lion_attack_up;
                }
                if (lion.Top < tigerBounds.Top)
                {
                    lion.Top += 3;
                    lion.Image = Properties.Resources.lion_attack_down;
                }

                // cek tabrakan dgn branch
                foreach (Control branch in _gameForm.Controls)
                {
                    if (branch is PictureBox && (string)branch.Tag == "branch" && lion.Bounds.IntersectsWith(branch.Bounds))
                    {
                        _collisionSound.Play();
                        tigerScore++;

                        _gameForm.Controls.Remove(branch);
                        branch.Dispose();

                        _gameForm.Controls.Remove(lion);
                        lion.Dispose();

                        _lionAttacks.Remove(lion);
                        MakeLion();
                        break;
                    }
                }
            }
        }
    }

    public void MakeLion()
    {
        PictureBox lion = new PictureBox
        {
            Tag = "lions",
            Image = Properties.Resources.lion_attack_down,
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
                    lion.Left = -lion.Width;
                    lion.Top = _randNum.Next(0, _gameForm.ClientSize.Height - lion.Height);
                    break;
                case 1: // kanan
                    lion.Left = _gameForm.ClientSize.Width;
                    lion.Top = _randNum.Next(0, _gameForm.ClientSize.Height - lion.Height);
                    break;
                case 2: // atas
                    lion.Top = -lion.Height;
                    lion.Left = _randNum.Next(0, _gameForm.ClientSize.Width - lion.Width);
                    break;
                case 3: // bawah
                    lion.Top = _gameForm.ClientSize.Height;
                    lion.Left = _randNum.Next(0, _gameForm.ClientSize.Width - lion.Width);
                    break;
            }

            // check overlap dgn tiger lain
            isOverlapping = _lionAttacks.Any(existingLion => lion.Bounds.IntersectsWith(existingLion.Bounds));
        } while (isOverlapping);

        _lionAttacks.Add(lion);
        _gameForm.Controls.Add(lion);
    }

    public void ResetLionsArmy()
    {
        foreach (PictureBox lion in _lionAttacks)
        {
            _gameForm.Controls.Remove(lion);
            lion.Dispose();
        }

        _lionAttacks.Clear();

        // nambahin 3 lion awal
        for (int i = 0; i < 3; i++)
        {
            MakeLion();
        }
    }

    private void AddLion(object sender, EventArgs e)
    {
        MakeLion();
    }
}