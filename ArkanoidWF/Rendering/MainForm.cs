using ArkanoidWF.Constants;
using ArkanoidWF.Rendering;
using ArkanoidWF.Сlasses;

namespace ArkanoidWF
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer? gameTimer;

        private GameCore? gameCore;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
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
        private void ClearControls()
        {
            foreach (Control control in Controls)
            {
                if (!(control is PictureBox))
                    control.Dispose();
            }
        }
        private void PrintStartUC()
        {
            ClearControls();
            var startUC = new StartUC();
            startUC.ButtonClicked += StartGame;
            Controls.Add(startUC);
            startUC.Dock = DockStyle.Fill;
        }
        // Обработчик события для ResultUC
        private void OnRestartButtonClicked(object? sender, EventArgs e)
        {
            PrintStartUC(); // вызываем обычный метод
        }

        private void PrintResultUC()
        {
            ClearControls();
            if (gameCore != null)
            {
                var resultUC = new ResultUC(gameCore.isVictory);
                resultUC.ButtonClicked += OnRestartButtonClicked;
                Controls.Add(resultUC);
                resultUC.Dock = DockStyle.Fill;
            }
        }
        private void StartGame(object? sender, EventArgs e)
        {
            // Отписываемся и удаляем текущий StartUC
            if (sender is StartUC startUC)
            {
                startUC.ButtonClicked -= StartGame;
                startUC.Dispose();
            }

            InitializeTimer();
            InitializeGameCore();
            gameTimer?.Start();
        }
        private void GameRestart()
        {
            PrintResultUC();
        }
        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer
            {
                Enabled = false,
                /// частота тика равна герцовке
                Interval = (int)(1000 / MonitorParameters.Herz),
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
                if (gameTimer.Enabled == true && gameCore != null)
                {

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.OKCancel);
            if (dialog == DialogResult.OK)
            {
                Close();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ExitPB.Image = FormImages.ClickImage;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ExitPB.Image = FormImages.DefaultImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ExitPB.Image = FormImages.EnterImage;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ExitPB.Location = new Point(ClientSize.Width - ExitPB.Width, 0);
        }

        private void ExitPB_MouseLeave(object sender, EventArgs e)
        {
            ExitPB.Image = FormImages.DefaultImage;
        }
    }
}
