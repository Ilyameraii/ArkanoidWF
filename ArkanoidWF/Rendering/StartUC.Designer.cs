namespace ArkanoidWF.Rendering
{
    partial class StartUC
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            PlayButton = new Button();
            SuspendLayout();
            // 
            // PlayButton
            // 
            PlayButton.BackColor = Color.Transparent;
            PlayButton.BackgroundImage = Properties.Resources.playbutton;
            PlayButton.BackgroundImageLayout = ImageLayout.Stretch;
            PlayButton.FlatStyle = FlatStyle.Popup;
            PlayButton.Location = new Point(204, 216);
            PlayButton.Margin = new Padding(0);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(327, 50);
            PlayButton.TabIndex = 0;
            PlayButton.UseVisualStyleBackColor = false;
            // 
            // StartUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(PlayButton);
            Name = "StartUC";
            Size = new Size(710, 503);
            Resize += StartUC_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Button PlayButton;
    }
}
