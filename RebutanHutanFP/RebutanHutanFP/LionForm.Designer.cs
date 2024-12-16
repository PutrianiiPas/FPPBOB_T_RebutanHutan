namespace RebutanHutanFP
{
    partial class LionForm
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
            lionPlayer = new PictureBox();
            lionGameTimer = new System.Windows.Forms.Timer(components);
            lionScore = new Label();
            lionHealthLabel = new Label();
            lionHealthBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)lionPlayer).BeginInit();
            SuspendLayout();
            // 
            // lionPlayer
            // 
            lionPlayer.BackColor = Color.Transparent;
            lionPlayer.Image = Properties.Resources.lion_up;
            lionPlayer.Location = new Point(524, 665);
            lionPlayer.Margin = new Padding(3, 4, 3, 4);
            lionPlayer.Name = "lionPlayer";
            lionPlayer.Size = new Size(180, 175);
            lionPlayer.TabIndex = 3;
            lionPlayer.TabStop = false;
            // 
            // lionGameTimer
            // 
            lionGameTimer.Enabled = true;
            lionGameTimer.Interval = 20;
            lionGameTimer.Tick += MainTimerEvent;
            // 
            // lionScore
            // 
            lionScore.AutoSize = true;
            lionScore.BackColor = Color.Transparent;
            lionScore.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lionScore.ForeColor = Color.Gold;
            lionScore.Location = new Point(12, 12);
            lionScore.Margin = new Padding(4, 0, 4, 0);
            lionScore.Name = "lionScore";
            lionScore.Size = new Size(128, 33);
            lionScore.TabIndex = 4;
            lionScore.Text = "Score: 0";
            lionScore.UseWaitCursor = true;
            // 
            // lionHealthLabel
            // 
            lionHealthLabel.AutoSize = true;
            lionHealthLabel.BackColor = Color.Transparent;
            lionHealthLabel.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lionHealthLabel.ForeColor = Color.Crimson;
            lionHealthLabel.Location = new Point(787, 12);
            lionHealthLabel.Margin = new Padding(4, 0, 4, 0);
            lionHealthLabel.Name = "lionHealthLabel";
            lionHealthLabel.Size = new Size(111, 33);
            lionHealthLabel.TabIndex = 5;
            lionHealthLabel.Text = "Health:";
            lionHealthLabel.UseWaitCursor = true;
            // 
            // lionHealthBar
            // 
            lionHealthBar.BackColor = Color.Crimson;
            lionHealthBar.ForeColor = Color.Crimson;
            lionHealthBar.Location = new Point(897, 12);
            lionHealthBar.Margin = new Padding(4, 5, 4, 5);
            lionHealthBar.Name = "lionHealthBar";
            lionHealthBar.Size = new Size(275, 33);
            lionHealthBar.TabIndex = 6;
            lionHealthBar.UseWaitCursor = true;
            lionHealthBar.Value = 100;
            // 
            // LionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg_lionform;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 853);
            Controls.Add(lionHealthBar);
            Controls.Add(lionHealthLabel);
            Controls.Add(lionScore);
            Controls.Add(lionPlayer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LionForm";
            Text = "Lion vs Tigers";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)lionPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox lionPlayer;
        private System.Windows.Forms.Timer lionGameTimer;
        private Label lionScore;
        private Label lionHealthLabel;
        private ProgressBar lionHealthBar;
    }
}
