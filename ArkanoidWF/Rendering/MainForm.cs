using ArkanoidWF.Constants;
using ArkanoidWF.Rendering;
using ArkanoidWF.Сlasses;

namespace ArkanoidWF
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// герцовка вашего монитора
        /// </summary>
        private static float Herz = 120.0f;

        private System.Windows.Forms.Timer? gameTimer;

        private GameCore? gameCore;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeGameCore();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            PrintStartUC();
        }
        private void InitializeGameCore()
        {
            gameCore = new GameCore(ClientSize.Width, ClientSize.Height);
        }
        private void PrintStartUC()
        {
            var startUC = new StartUC();
            startUC.ButtonClicked += StartGame;
            Controls.Add(startUC);
            startUC.Dock = DockStyle.Fill;
        }
        private void StartGame(object? sender, EventArgs e)
        {
            if (gameTimer != null)
            {
                gameTimer.Start();

            }
        }
        private void GameRestart()
        {
            InitializeGameCore();
            PrintStartUC();

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
            if (gameCore != null)
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
                        GameRestart();
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (gameTimer != null)
            {
                if (gameTimer.Enabled == true)
                {
                    base.OnPaint(e);

                    PlayerPlatformPaint(e);

                    ballPaint(e);

                    brickPaint(e);
                }
            }
        }
        private void ballPaint(PaintEventArgs e)
        {
            if (gameCore != null)
            {
                e.Graphics.DrawImage(BallParameters.Image, gameCore.BallX, gameCore.BallY, BallParameters.Size, BallParameters.Size);
            }
        }
        private void brickPaint(PaintEventArgs e)
        {
            if (gameCore != null)
            {
                foreach (var brick in gameCore.Bricks)
                {
                    using var brush = new SolidBrush(brick.Color);
                    using var pen = new Pen(Color.Black, BrickParameters.Bold);
                    e.Graphics.FillRectangle(brush, brick.Bounds);
                    e.Graphics.DrawRectangle(pen, brick.Bounds);
                }
            }
        }
        private void PlayerPlatformPaint(PaintEventArgs e)
        {
            if (gameCore != null)
            {
                e.Graphics.DrawImage(PlayerPlatformParameters.Image, gameCore.PlatformX, gameCore.PlatformY, PlayerPlatformParameters.Width, PlayerPlatformParameters.Height);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameCore != null)
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
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (gameCore != null)
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
}
