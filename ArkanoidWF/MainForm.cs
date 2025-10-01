namespace ArkanoidWF
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// герцовка вашего монитора
        /// </summary>
        private static float Herz = 120.0f;

        private System.Windows.Forms.Timer? gameTimer;

        private GameCore gameCore;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();

            gameCore = new GameCore(Width,Height);

            if (gameTimer != null)
            {
                gameTimer.Start();
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer
            {
                Enabled = false,
                /// частота тика равна герцовке
                Interval = (int)(1000 / Herz),
            };
            gameTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            gameCore.Tick();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {       
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawImage(gameCore.BallImage, gameCore.BallX, gameCore.BallY, gameCore.BallSize, gameCore.BallSize);
            foreach(var brick in gameCore.Bricks)
            {
                g.FillRectangle(new SolidBrush(brick.Color), brick.Bounds);
            }
        }

    }
}
