using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Media;

namespace RebutanHutanFP;

// class player 'Lion'
public partial class LionForm : Form
{
    private bool goLeft, goRight, goUp, goDown, gameOver;
    private string _facing = "up";
    private int _lionPlayerHealth = 100;
    private int _lionSpeed = 13;
    private int _lionScore;
    private Label _titleLionBattle;

    // objek sound
    private SoundPlayer _collisionSound;
    private SoundPlayer _gameOverSound;

    private TigersArmy _tigersArmy;

    // daftar pertanyaan quiz
    List<(string Soal, string[] Pilihan, string CorrectAnswer)> _forestQuiz;

    public LionForm()
    {
        InitializeComponent();
        InitializeGameLion();
        this.DoubleBuffered = true; // mengaktifkan double buffering utk performa better
       
        _tigersArmy = new TigersArmy(this);
        _tigersArmy.ResetTigersArmy(); // reset dan tampilkan 3 tiger awal

        InitializeForestQuiz();
        RestartGame();

        // inisialisasi sound
        _collisionSound = new SoundPlayer(new MemoryStream(Properties.Resources.hit_sounds));
        _gameOverSound = new SoundPlayer(new MemoryStream(Properties.Resources.game_over));
    }

    private void InitializeGameLion()
    {
        // membuat label judul
        _titleLionBattle = new Label
        {
            Text = "Lion VS Tigers",
            Location = new Point(320, 35),
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            Size = new Size(280, 50),
            ForeColor = Color.DarkOrange,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };
        this.Controls.Add(_titleLionBattle);

        // set properti utama form
        this.Text = "Player Lion VS Tigers";
        this.Size = new Size(1200, 900);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeForestQuiz()
    {
        _forestQuiz = new List<(string, string[], string)>
        {
            // list pertanyaan dan jawabannya
            ("Singa takut air\nTrue or False?", new[] {"True", "False"}, "True"),
            ("Nama ilmiah singa adalah Panthera pardus.", new[] {"Benar, itu singa", "Salah, itu harimau", "Salah, itu macan tutul"}, "Salah, itu macan tutul"),
            ("Singa punya lebih dari 35 gigi\nTrue or False?", new[] {"True", "False"}, "False"),
            ("Sebagian besar populasi singa berasal dari benua apa sih?", new[] {"Indonesia", "Afrika", "Brazil", "Jepang"}, "Afrika"),
            ("Ternyata singa betina yang bertugas sebagai pemburu loh!\nKarena si betina lebih gesit dibandingkan si jantan.", new[] {"Kyknya bukan", "Bisa jadi", "Masa sih?", "Wah keren"}, "Wah keren"),
            ("Katanya singa itu hewan yang hidup individual dan introvert abiez.", new[] {"Wah kyk aku", "Gapapa aku temenin","Salah, singa berjiwa sosial tinggi"}, "Salah, singa berjiwa sosial tinggi"),
            ("Singa adalah spesies kucing terbesar pertama.\nTrue or False?", new[] {"True", "False"}, "False"),
            ("Surai singa berfungsi melindungi daerah leher dari serangan singa lain.\nTrue or False?", new[] {"True", "False"}, "True"),
            ("Lama hidup rata-rata seekor singa adalah 40 tahun loh.\nTrue or False?", new[] {"True", "False"}, "False"),
            ("Siapa sangka, Singa bisa tidur hingga 20 jam sehari untuk menghemat energi berburu.\nTrue or False?", new[] {"True", "False"}, "True")
        };
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {
        // batas area game
        int topBoundary = 100;
        int bottomBoundary = this.ClientSize.Height;
        int leftBoundary = 20;
        int rightBoundary = this.ClientSize.Width;

        // perbarui health bar or tampilkan quiz jika game over
        if (_lionPlayerHealth > 1)
        {
            lionHealthBar.Value = _lionPlayerHealth;
        }
        else if (!gameOver)
        {
            gameOver = true;
            lionGameTimer.Stop();
            ShowQuiz(); // tampilan quiz setelah gameover
        }

        // perbarui skor
        lionScore.Text = "Score: " + _lionScore;

        // kontrol gerakan lion as player
        if (goLeft && lionPlayer.Left > leftBoundary)
            lionPlayer.Left -= _lionSpeed;

        if (goRight && lionPlayer.Right < rightBoundary)
            lionPlayer.Left += _lionSpeed;

        if (goUp && lionPlayer.Top > topBoundary)
            lionPlayer.Top -= _lionSpeed;

        if (goDown && lionPlayer.Bottom < bottomBoundary)
            lionPlayer.Top += _lionSpeed;

        // pergerakan tiger
        _tigersArmy.HandleTigerMovements(ref _lionPlayerHealth, ref _lionScore);
    }

    private void ShowQuiz()
    {
        // pilih soal acak
        var random = new Random();
        var soalTerpilih = _forestQuiz[random.Next(_forestQuiz.Count)];

        // tampilan dialog quiz
        string jawabanPlayer = ShowQuizDialog(soalTerpilih.Soal, soalTerpilih.Pilihan);

        if (jawabanPlayer == soalTerpilih.CorrectAnswer)
        {
            // jawaban benar
            _lionPlayerHealth = Math.Min(_lionPlayerHealth + (70), 100);
            gameOver = false;
            lionGameTimer.Start();
        }
        else
        {
            // jawaban salah
            TampilkanGameOver();
        }
    }

    private void TampilkanGameOver()
    {
        gameOver = true;

        // label "Game Over"
        Label gameOverLabel = new Label
        {
            Text = "~ GAME OVER ~",
            Font = new Font("Segoe UI", 36, FontStyle.Bold),
            ForeColor = Color.Black,
            BackColor = Color.DarkRed,
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point((this.ClientSize.Width / 2) - 150, (this.ClientSize.Height / 2) - 50)
        };

        this.Controls.Add(gameOverLabel);
        gameOverLabel.BringToFront();

        // mainkan suara game over
        _gameOverSound.Play();

        // delay sebelum keluar dari form game
        Task.Delay(3000).ContinueWith(_ =>
        {
            this.Invoke((Action)(this.Close));
        });
    }

    private string ShowQuizDialog(string soal, string[] pilihan)
    {
        using (Form quizForm = new Form())
        {
            quizForm.Text = "Forest Quiz Seputar Singa";
            quizForm.Size = new Size(600, 400);
            quizForm.BackColor = Color.DarkOrange;
            quizForm.StartPosition = FormStartPosition.CenterScreen;

            Label boxSoalQuiz = new Label
            {
                Text = soal,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 80,
                Padding = new Padding(10)
            };
            quizForm.Controls.Add(boxSoalQuiz);

            FlowLayoutPanel boxJawaban = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(130, 90, 130, 10)
            };

            string playerAnswer = null;

            foreach (var option in pilihan)
            {
                Button buttonPilihan = new Button
                {
                    Text = option,
                    Width = 300,
                    Height = 40,
                    Margin = new Padding(5)
                };
                buttonPilihan.Click += (s, e) => { playerAnswer = option; quizForm.Close(); };
                boxJawaban.Controls.Add(buttonPilihan);
            }

            quizForm.Controls.Add(boxJawaban);
            quizForm.ShowDialog();

            return playerAnswer;
        }
    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (gameOver == true) return;

        if (e.KeyCode == Keys.Left)
        {
            goLeft = true;
            _facing = "left";
            lionPlayer.Image = Properties.Resources.lion_left;
        }
        if (e.KeyCode == Keys.Right)
        {
            goRight = true;
            _facing = "right";
            lionPlayer.Image = Properties.Resources.lion_right;
        }
        if (e.KeyCode == Keys.Up)
        {
            goUp = true;
            _facing = "up";
            lionPlayer.Image = Properties.Resources.lion_up;
        }
        if (e.KeyCode == Keys.Down)
        {
            goDown = true;
            _facing = "down";
            lionPlayer.Image = Properties.Resources.lion_down;
        }

    }

    private void KeyIsUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) goLeft = false;

        if (e.KeyCode == Keys.Right) goRight = false;

        if (e.KeyCode == Keys.Up) goUp = false;

        if (e.KeyCode == Keys.Down) goDown = false;

        if (e.KeyCode == Keys.Enter && gameOver == true) RestartGame();

        if (e.KeyCode == Keys.Space && gameOver == false) ShootBones(_facing);
    }

    private void ShootBones(string direction)
    {
        WeaponBones shootBones = new WeaponBones();
        shootBones.direction = direction;
        shootBones.bonesLeft = lionPlayer.Left + (lionPlayer.Width / 2) - 50;
        shootBones.bonesTop = lionPlayer.Top + (lionPlayer.Height / 2) - 50;
        shootBones.MakeWeaponBones(this);
    }

    private void RestartGame()
    {
        lionPlayer.Image = Properties.Resources.lion_up;

        // Atur ukuran lion menjadi lebih kecil
        lionPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
        lionPlayer.Size = new Size(120, 120);

        // Reset semua variabel kontrol gerakan dan status permainan
        goUp = false;
        goDown = false;
        goLeft = false;
        goRight = false;
        gameOver = false;

        // Reset kesehatan lion ke 100 (nilai awal)
        _lionPlayerHealth = 100;

        // Reset skor lion ke 0
        _lionScore = 0;

        // Perbarui UI untuk health bar dan skor
        lionHealthBar.Value = _lionPlayerHealth;
        lionScore.Text = "Score: " + _lionScore;

        // Restart tigers army
        _tigersArmy.ResetTigersArmy();

        // Mulai ulang timer game
        lionGameTimer.Start();
    }
}