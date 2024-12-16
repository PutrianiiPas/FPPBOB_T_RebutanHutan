using RebutanHutanFP.Properties;
using RebutanHutanFP;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace RebutanHutanFP;

public class StartPage : Form
{
    private Button _lionButton;
    private Button _tigerButton;
    private Button _spBackButton;
    private SoundPlayer _clickSound;

    public StartPage()
    {
        InitializeForm();
        InitializeControls();

        _clickSound = new SoundPlayer(new MemoryStream(Properties.Resources.click_sounds));
    }

    private void InitializeForm()
    {
        SuspendLayout();
        // 
        // StartPage
        // 
        BackgroundImage = Resources.bg_start;
        BackgroundImageLayout = ImageLayout.Stretch;
        //ClientSize = new Size(284, 261);
        DoubleBuffered = true;
        ResumeLayout(false);

        this.Text = "Choose Character";
        this.ForeColor = Color.Black;
        this.Size = new Size(700, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeControls()
    {
        // utk tombol back
        _spBackButton = new Button
        {
            Text = "Back",
            Location = new Point(300, 450),
            Size = new Size(80, 35),
            ForeColor = Color.Black,
            BackColor = Color.ForestGreen
        };
        _spBackButton.Click += SPBackButton_Click;
        this.Controls.Add(_spBackButton);

        // utk tombol pilih chara lion
        _lionButton = new Button
        {
            Text = "Lion",
            Location = new Point(150, 375),
            Size = new Size(120, 35),
            ForeColor = Color.Black,
            BackColor = Color.DarkOrange
        };
        _lionButton.Click += LionButton_Click;
        this.Controls.Add(_lionButton);

        // utk tombol pilih chara tiger
        _tigerButton = new Button
        {
            Text = "Tiger",
            Location = new Point(420, 375),
            Size = new Size(120, 35),
            ForeColor = Color.White,
            BackColor = Color.SaddleBrown
        };
        _tigerButton.Click += TigerButton_Click;
        this.Controls.Add(_tigerButton);
    }

    private void SPBackButton_Click(object sender, EventArgs e)
    {
        // utk menutup StartPage & kembali ke HomePage
        _clickSound.Play();
        this.Close();
    }

    private void LionButton_Click(object sender, EventArgs e)
    {
        _clickSound.Play();
        LionForm gform = new LionForm();
        gform.FormClosed += (s, args) => this.Show();
        gform.Show();
        this.Hide();
    }

    private void TigerButton_Click(object sender, EventArgs e)
    {
        _clickSound.Play();
        TigerForm gform = new TigerForm();
        gform.FormClosed += (s, args) => this.Show();
        gform.Show();
        this.Hide();
    }
}