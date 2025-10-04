using ArkanoidWF.Constants;
namespace ArkanoidWF.Rendering
{
    public partial class ResultUC : UserControl
    {
        public event EventHandler? ButtonClicked;
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
