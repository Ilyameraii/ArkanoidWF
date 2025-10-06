using ArkanoidWF.Constants;
namespace ArkanoidWF.Rendering
{
    public partial class ResultUC : UserControl
    {
        //// <summary>
        /// Возникает, когда пользователь нажимает кнопку перезапуска игры.
        /// </summary>
        public event EventHandler? ButtonClicked;
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="isVictory">параметр, отвечающий за результат игры</param>
        public ResultUC(bool isVictory)
        {
            InitializeComponent();
            CenteringTable();
            restartButton.Click += OnButtonClicked;

            if (isVictory)
            {
                pictureBoxResult.Image = FormImages.Victory;
            }
            else
            {
                pictureBoxResult.Image = FormImages.Lose;
            }
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        //центрирование картинки с кнопкой
        private void CenteringTable()
        {
            tableLayoutPanel.Location = new Point(
                (ClientSize.Width - tableLayoutPanel.Width) / 2,
                (ClientSize.Height - tableLayoutPanel.Height) / 2
            );
        }
        private void ResultUC_Resize(object sender, EventArgs e)
        {
            CenteringTable();
        }
    }
}
