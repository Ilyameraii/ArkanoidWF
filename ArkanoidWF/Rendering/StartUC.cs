namespace ArkanoidWF.Rendering
{
    public partial class StartUC : UserControl
    {

        public event EventHandler? ButtonClicked;
        public StartUC()
        {
            InitializeComponent();
            CenteringButton();
            playButton.Click += OnButtonClicked;
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CenteringButton()
        {
            playButton.Location = new Point(
                (ClientSize.Width - playButton.Width) / 2,
                (ClientSize.Height - playButton.Height) / 2
            );
        }
        private void StartUC_Resize(object sender, EventArgs e)
        {
            CenteringButton();
        }
    }
}
