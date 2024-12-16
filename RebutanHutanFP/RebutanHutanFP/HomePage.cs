using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace RebutanHutanFP;

// class sbg form utama
public class HomePage : Form
{
    private Button _startButton;
    private Button _exitButton;
    private Image _logoImage;
    public SoundPlayer _clickSound;

    public HomePage()
    {
        InitializeForm();
        InitializeControls();

        _clickSound = new SoundPlayer(new MemoryStream(Properties.Resources.click_sounds));

        // Mainkan backsound
        BackSound.Instance.Play();
    }

    private void InitializeForm()
    {
        SuspendLayout();
        // 
        // HomePage
        // 
        BackgroundImage = Properties.Resources.bg_home;
        BackgroundImageLayout = ImageLayout.Stretch;
        DoubleBuffered = true;
        ResumeLayout(false);

        this.Text = "Home";
        this.ForeColor = Color.Black;
        this.Size = new Size(700, 700);
        this.StartPosition = FormStartPosition.CenterScreen;

    }

    private void InitializeControls()
    {
        // utk tombol start game
        _startButton = new Button
        {
            Text = "Start Game",
            Location = new Point(280, 330),
            Size = new Size(120, 40),
            ForeColor = Color.Black,
            BackColor = Color.Chocolate
        };
        _startButton.Click += StartButton_Click;
        this.Controls.Add(_startButton);

        // utk tombol exit
        _exitButton = new Button
        {
            Text = "Exit",
            Location = new Point(280, 420),
            Size = new Size(120, 40),
            ForeColor = Color.Black,
            BackColor = Color.Chocolate
        };
        _exitButton.Click += ExitButton_Click;
        this.Controls.Add(_exitButton);
    }

    // utk membuka StartPage & menyembunyikan HomePage.
    // saat StartPage ditutup (misal dgn menekan tombol Back), HomePage akan ditampilkan kembali.
    private void StartButton_Click(object sender, EventArgs e)
    {
        _clickSound.Play();
        StartPage gform = new StartPage();
        gform.FormClosed += (s, args) => this.Show();
        gform.Show();
        this.Hide();
    }

    private void ExitButton_Click(Object sender, EventArgs e)
    {
        _clickSound.Play();
        Application.Exit();
    }
}