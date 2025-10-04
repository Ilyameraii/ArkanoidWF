namespace ArkanoidWF.Rendering
{
    partial class ResultUC
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>


        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            restartButton = new Button();
            tableLayoutPanel = new TableLayoutPanel();
            pictureBoxResult = new PictureBox();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).BeginInit();
            SuspendLayout();
            // 
            // restartButton
            // 
            restartButton.Anchor = AnchorStyles.None;
            restartButton.BackgroundImage = Properties.Resources.restartButton;
            restartButton.BackgroundImageLayout = ImageLayout.Stretch;
            restartButton.Cursor = Cursors.Hand;
            restartButton.FlatStyle = FlatStyle.Popup;
            restartButton.Location = new Point(107, 121);
            restartButton.Name = "restartButton";
            restartButton.Size = new Size(327, 50);
            restartButton.TabIndex = 0;
            restartButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(pictureBoxResult, 0, 0);
            tableLayoutPanel.Controls.Add(restartButton, 0, 1);
            tableLayoutPanel.Location = new Point(90, 145);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Size = new Size(541, 195);
            tableLayoutPanel.TabIndex = 1;
            // 
            // pictureBoxResult
            // 
            pictureBoxResult.Dock = DockStyle.Fill;
            pictureBoxResult.Image = Properties.Resources.lose;
            pictureBoxResult.Location = new Point(3, 3);
            pictureBoxResult.Name = "pictureBoxResult";
            pictureBoxResult.Size = new Size(535, 91);
            pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxResult.TabIndex = 1;
            pictureBoxResult.TabStop = false;
            pictureBoxResult.Resize += ResultUC_Resize;
            // 
            // ResultUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanel);
            DoubleBuffered = true;
            Name = "ResultUC";
            Size = new Size(708, 441);
            Resize += ResultUC_Resize;
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button restartButton;
        private TableLayoutPanel tableLayoutPanel;
        private PictureBox pictureBoxResult;
    }
}
