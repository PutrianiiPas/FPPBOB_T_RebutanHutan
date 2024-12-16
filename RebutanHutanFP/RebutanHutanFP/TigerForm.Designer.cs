namespace RebutanHutanFP
{
    partial class TigerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tigerScore = new Label();
            tigerHealthLabel = new Label();
            tigerHealthBar = new ProgressBar();
            tigerPlayer = new PictureBox();
            tigerGameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)tigerPlayer).BeginInit();
            SuspendLayout();
            // 
            // tigerScore
            // 
            tigerScore.AutoSize = true;
            tigerScore.BackColor = Color.Transparent;
            tigerScore.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tigerScore.ForeColor = Color.Gold;
            tigerScore.Location = new Point(12, 12);
            tigerScore.Margin = new Padding(4, 0, 4, 0);
            tigerScore.Name = "tigerScore";
            tigerScore.Size = new Size(128, 33);
            tigerScore.TabIndex = 0;
            tigerScore.Text = "Score: 0";
            // 
            // tigerHealthLabel
            // 
            tigerHealthLabel.AutoSize = true;
            tigerHealthLabel.BackColor = Color.Transparent;
            tigerHealthLabel.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tigerHealthLabel.ForeColor = Color.Crimson;
            tigerHealthLabel.Location = new Point(787, 12);
            tigerHealthLabel.Margin = new Padding(4, 0, 4, 0);
            tigerHealthLabel.Name = "tigerHealthLabel";
            tigerHealthLabel.Size = new Size(111, 33);
            tigerHealthLabel.TabIndex = 1;
            tigerHealthLabel.Text = "Health:";
            // 
            // tigerHealthBar
            // 
            tigerHealthBar.BackColor = Color.Crimson;
            tigerHealthBar.ForeColor = Color.Crimson;
            tigerHealthBar.Location = new Point(897, 12);
            tigerHealthBar.Margin = new Padding(4, 5, 4, 5);
            tigerHealthBar.Name = "tigerHealthBar";
            tigerHealthBar.Size = new Size(275, 33);
            tigerHealthBar.TabIndex = 2;
            tigerHealthBar.Value = 100;
            // 
            // tigerPlayer
            // 
            tigerPlayer.BackColor = Color.Transparent;
            tigerPlayer.Image = Properties.Resources.tiger_up;
            tigerPlayer.Location = new Point(526, 664);
            tigerPlayer.Margin = new Padding(4, 5, 4, 5);
            tigerPlayer.Name = "tigerPlayer";
            tigerPlayer.Size = new Size(175, 175);
            tigerPlayer.TabIndex = 3;
            tigerPlayer.TabStop = false;
            // 
            // tigerGameTimer
            // 
            tigerGameTimer.Enabled = true;
            tigerGameTimer.Interval = 20;
            tigerGameTimer.Tick += MainTimerEvent;
            // 
            // TigerForm
            // 
            AutoScaleDimensions = new SizeF(12F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            BackgroundImage = Properties.Resources.bg_lionform;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 853);
            Controls.Add(tigerPlayer);
            Controls.Add(tigerHealthBar);
            Controls.Add(tigerHealthLabel);
            Controls.Add(tigerScore);
            Font = new Font("Arial Black", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ActiveCaptionText;
            Margin = new Padding(4, 5, 4, 5);
            Name = "TigerForm";
            Text = "Lion vs Tigers";
            UseWaitCursor = true;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)tigerPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tigerScore;
        private Label tigerHealthLabel;
        private ProgressBar tigerHealthBar;
        private PictureBox tigerPlayer;
        private System.Windows.Forms.Timer tigerGameTimer;
    }
}
