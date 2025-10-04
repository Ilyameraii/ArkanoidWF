namespace ArkanoidWF
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ExitPB = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ExitPB).BeginInit();
            SuspendLayout();
            // 
            // ExitPB
            // 
            ExitPB.BackColor = Color.Transparent;
            ExitPB.BackgroundImage = Properties.Resources.closebutton;
            ExitPB.Cursor = Cursors.Hand;
            ExitPB.Location = new Point(719, 0);
            ExitPB.Name = "ExitPB";
            ExitPB.Size = new Size(80, 61);
            ExitPB.TabIndex = 0;
            ExitPB.TabStop = false;
            ExitPB.Click += pictureBox1_Click;
            ExitPB.MouseDown += pictureBox1_MouseDown;
            ExitPB.MouseEnter += pictureBox1_MouseEnter;
            ExitPB.MouseLeave += ExitPB_MouseLeave;
            ExitPB.MouseUp += pictureBox1_MouseUp;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.juggernaut_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(ExitPB);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Arkanoid";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            Resize += MainForm_Resize;
            ((System.ComponentModel.ISupportInitialize)ExitPB).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ExitPB;
    }
}
