namespace ArkanoidWF.Rendering
{
    partial class StartUC
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            playButton = new Button();
            SuspendLayout();
            // 
            // playButton
            // 
            playButton.BackColor = Color.Transparent;
            playButton.BackgroundImage = Properties.Resources.playbutton;
            playButton.BackgroundImageLayout = ImageLayout.Stretch;
            playButton.Cursor = Cursors.Hand;
            playButton.FlatStyle = FlatStyle.Popup;
            playButton.Location = new Point(204, 216);
            playButton.Margin = new Padding(0);
            playButton.Name = "playButton";
            playButton.Size = new Size(327, 50);
            playButton.TabIndex = 0;
            playButton.UseVisualStyleBackColor = false;
            // 
            // StartUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(playButton);
            DoubleBuffered = true;
            Name = "StartUC";
            Size = new Size(710, 503);
            Resize += StartUC_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Button playButton;
    }
}
