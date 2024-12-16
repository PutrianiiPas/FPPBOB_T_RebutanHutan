using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using RebutanHutanFP.Properties;

namespace RebutanHutanFP;

// class player 'Tiger'
public partial class TigerForm : Form
{
    private bool goLeft, goRight, goUp, goDown, gameOver;
    private string _facing = "up";
    private int _tigerPlayerHealth = 100;
    private int _tigerSpeed = 13;
    private int _tigerScore;
    private Label _titleTigerBattle;

    private SoundPlayer _collisionSound;
    private SoundPlayer _gameOverSound;

    private LionsArmy lionsArmy;

    // daftar pertanyaan quiz
    private List<(string Soal, string[] Pilihan, string CorrectAnswer)> forestQuiz;

    public TigerForm()
    {
        InitializeComponent();
        InitializeGameTiger();
        this.DoubleBuffered = true;

        lionsArmy = new LionsArmy(this);
        lionsArmy.ResetLionsArmy(); // reset dan tampilkan 3 lion awal

        InitializeForestQuiz();
        RestartGame();

        _collisionSound = new SoundPlayer(new MemoryStream(Properties.Resources.hit_sounds));
        _gameOverSound = new SoundPlayer(new MemoryStream(Properties.Resources.game_over));
    }

    private void InitializeGameTiger()
    {
        // membuat label judul
        _titleTigerBattle = new Label
        {
            Text = "Tiger VS Lions",
            Location = new Point(320, 35),
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            Size = new Size(280, 50),
            ForeColor = Color.SaddleBrown,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
        };
        this.Controls.Add(_titleTigerBattle);

        // set properti utama form
        this.Text = "Player Tiger vs Lions";
        this.Size = new Size(1200, 900);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeForestQuiz()
    {
        forestQuiz = new List<(string, string[], string)>
        {
            // list pertanyaan dan jawabannya
            ("Ternyata lama hidup harimau hanya 10-15 tahun di alam bebas.\nTrue or False?", new[] {"True", "False"}, "True"),
            ("Apa hayo makanan utama si harimau?", new[] {"Rumput", "Susu", "Daging", "Buah"}, "Daging"),
            ("Katanya sih harimau bisa tidur dalam durasi 16-20 jam loh.\nTrue or False?", new[] {"True", "False"}, "True"),
            ("Kamu tau ga berapa jumlah kaki si harimau?", new[] {"4", "2", "8", "1000 sih"}, "4"),
            ("Siapa yang suka berenang?\nKalo iya artinya kamu punya hobi yang sama dengan harimau.", new[] {"Oh ya?", "Masa sih", "aku ga suka berenang", "yay sama rawr"}, "yay sama rawr"),
            ("Katanya pola belang-belang tiap harimau itu sama.\nTrue or False?", new[] {"True", "False"}, "False"),
            ("Harimau itu 'morning-animal' atau 'night-animal'?", new[] {"Morning-Animal kyk aku", "Night-Animal sih", "24/7 produktif"}, "Night-Animal sih"),
            ("Kenapa harimau disebut sebagai spesies kunci?", new[] {"salah satu spesies yang ditakuti rawr", "berdampak besar pada kestabilan ekosistem, \nberada di puncak rantai makanan", "harimau sebagai predator", "karena harimau punya kunci!"}, "berdampak besar pada kestabilan ekosistem, \nberada di puncak rantai makanan"),
            ("Harimau punya perut yang kecil dan ringan, apa sih manfaatnya?", new[] {"makannya dikit", "ga cepat lapar", "gampang berlari mengejar mangsa", "ga dikatain buncit"}, "gampang berlari mengejar mangsa"),
            ("Saat ini ada 5 subspesies harimau yang masih ada \ndan 3 subspesies lainnya telah punah.\nTrue or False?", new[] {"True", "False"}, "True")
        };
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {
        int topBoundary = 100;
        int bottomBoundary = this.ClientSize.Height;
        int leftBoundary = 20;
        int rightBoundary = this.ClientSize.Width;

        if (_tigerPlayerHealth > 1)
        {
            tigerHealthBar.Value = _tigerPlayerHealth;
        }
        else if (!gameOver)
        {
            gameOver = true;
            tigerGameTimer.Stop();
            ShowQuiz();
        }

        tigerScore.Text = "Score: " + _tigerScore;

        // kontrol gerakan player tiger
        if (goLeft && tigerPlayer.Left > leftBoundary)
            tigerPlayer.Left -= _tigerSpeed;

        if (goRight && tigerPlayer.Right < rightBoundary)
            tigerPlayer.Left += _tigerSpeed;

        if (goUp && tigerPlayer.Top > topBoundary)
            tigerPlayer.Top -= _tigerSpeed;

        if (goDown && tigerPlayer.Bottom < bottomBoundary)
            tigerPlayer.Top += _tigerSpeed;

        // gerak lion
        lionsArmy.HandleLionMovements(ref _tigerPlayerHealth, ref _tigerScore);
    }

    private void ShowQuiz()
    {
        var random = new Random();
        var daftarSoal = forestQuiz[random.Next(forestQuiz.Count)];

        // tampilkan dialog kuis
        string jawabanPlayer = ShowQuizDialog(daftarSoal.Soal, daftarSoal.Pilihan);

        if (jawabanPlayer == daftarSoal.CorrectAnswer)
        {
            // jawaban benar
            _tigerPlayerHealth = Math.Min(_tigerPlayerHealth + 70, 100);
            gameOver = false;
            tigerGameTimer.Start();
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
            Text = "GAME OVER",
            Font = new Font("Segoe UI", 40, FontStyle.Bold),
            ForeColor = Color.Black,
            BackColor = Color.DarkRed,
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point((this.ClientSize.Width / 2) - 150, (this.ClientSize.Height / 2) - 50)
        };

        this.Controls.Add(gameOverLabel);
        gameOverLabel.BringToFront();

        // suara game over
        _gameOverSound.Play();

        // Berikan delay untuk suara sebelum keluar dari form
        Task.Delay(3000).ContinueWith(_ =>
        {
            this.Invoke((Action)(this.Close));
        });
    }

    private string ShowQuizDialog(string soal, string[] pilihan)
    {
        using (Form quizForm = new Form())
        {
            quizForm.Text = "Forest Quiz Seputar Harimau";
            quizForm.Size = new Size(600, 400);
            quizForm.BackColor = Color.Chocolate;
            quizForm.StartPosition = FormStartPosition.CenterScreen;

            Label boxSoalQuiz = new Label
            {
                Text = soal,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 80,
                Padding = new Padding(7)
            };
            quizForm.Controls.Add(boxSoalQuiz);

            FlowLayoutPanel boxJawaban = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(120, 77, 90, 7)
            };

            string playerAnswer = null;

            foreach (var option in pilihan)
            {
                Button buttonPilihan = new Button
                {
                    Text = option,
                    Width = 325,
                    Height = 50,
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
            tigerPlayer.Image = Properties.Resources.tiger_left;
        }
        if (e.KeyCode == Keys.Right)
        {
            goRight = true;
            _facing = "right";
            tigerPlayer.Image = Properties.Resources.tiger_right;
        }
        if (e.KeyCode == Keys.Up)
        {
            goUp = true;
            _facing = "up";
            tigerPlayer.Image = Properties.Resources.tiger_up;
        }
        if (e.KeyCode == Keys.Down)
        {
            goDown = true;
            _facing = "down";
            tigerPlayer.Image = Properties.Resources.tiger_down;
        }

    }

    private void KeyIsUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) goLeft = false;

        if (e.KeyCode == Keys.Right) goRight = false;

        if (e.KeyCode == Keys.Up) goUp = false;

        if (e.KeyCode == Keys.Down) goDown = false;

        if (e.KeyCode == Keys.Enter && gameOver == true) RestartGame();

        if (e.KeyCode == Keys.Space && gameOver == false) ShootBranch(_facing);
    }

    private void ShootBranch(string direction)
    {
        WeaponBranch shootBranch = new WeaponBranch();
        shootBranch.direction = direction;
        shootBranch.branchLeft = tigerPlayer.Left + (tigerPlayer.Width / 2) - 50;
        shootBranch.branchTop = tigerPlayer.Top + (tigerPlayer.Height / 2) - 50;
        shootBranch.MakeWeaponBranch(this);
    }

    private void RestartGame()
    {
        tigerPlayer.Image = Properties.Resources.tiger_up;

        // Atur ukuran tiger menjadi lebih kecil
        tigerPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
        tigerPlayer.Size = new Size(120, 120);

        // Reset semua variabel kontrol gerakan dan status permainan
        goUp = false;
        goDown = false;
        goLeft = false;
        goRight = false;
        gameOver = false;

        // Reset kesehatan tiger ke 100 (nilai awal)
        _tigerPlayerHealth = 100;

        // Reset skor tiger ke 0
        _tigerScore = 0;

        // Perbarui UI untuk health bar dan skor
        tigerHealthBar.Value = _tigerPlayerHealth;
        tigerScore.Text = "Score: " + _tigerScore;

        // Restart tigers army
        lionsArmy.ResetLionsArmy();

        // Mulai ulang timer game
        tigerGameTimer.Start();
    }
}