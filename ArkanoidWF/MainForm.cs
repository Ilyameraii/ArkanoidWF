using ArkanoidWF.Constants;

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

            gameCore = new GameCore(Width, Height);

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
            if (!gameCore.isGameOver)
            {
                gameCore.Tick();
                Invalidate();
            }
            else
            {
                if (gameTimer != null)
                {
                    gameTimer.Stop();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            PlayerPlatformPaint(e);

            ballPaint(e);

            brickPaint(e);
        }
        private void ballPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(BallParameters.Image, gameCore.BallX, gameCore.BallY, BallParameters.Size, BallParameters.Size);
        }
        private void brickPaint(PaintEventArgs e)
        {
            foreach (var brick in gameCore.Bricks)
            {
                e.Graphics.FillRectangle(new SolidBrush(brick.Color), brick.Bounds);
                e.Graphics.DrawRectangle(new Pen(Color.Black, BrickParameters.Bold), brick.Bounds);
            }
        }
        private void PlayerPlatformPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(PlayerPlatformParameters.Image, gameCore.PlatformX, gameCore.PlatformY, PlayerPlatformParameters.Width, PlayerPlatformParameters.Height);

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    gameCore.SetMoveLeft(true);
                    break;
                case Keys.D:
                case Keys.Right:
                    gameCore.SetMoveRight(true);
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    gameCore.SetMoveLeft(false);
                    break;
                case Keys.D:
                case Keys.Right:
                    gameCore.SetMoveRight(false);
                    break;
            }
        }
    }
}
