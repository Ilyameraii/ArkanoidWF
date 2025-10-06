using ArkanoidWF.Constants;
using ArkanoidWF.Rendering;
using ArkanoidWF.Сlasses;

namespace ArkanoidWF
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer? gameTimer;

        private readonly ResultUC winUC = new ResultUC(isVictory: true);
        private readonly ResultUC loseUC = new ResultUC(isVictory: false);
        private readonly StartUC startUC = new StartUC();

        private GameCore? gameCore;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true; // перехвать нажатий клавиш
            InitializeTimer();
            InitializeUC();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            PrintStartUC();
        }

        private void InitializeUC()
        {
            startUC.ButtonClicked += StartGame;
            startUC.Dock = DockStyle.Fill;

            winUC.ButtonClicked += OnRestartButtonClicked;
            winUC.Dock = DockStyle.Fill;

            loseUC.ButtonClicked += OnRestartButtonClicked;
            loseUC.Dock = DockStyle.Fill;
        }

        // Обработчик события для ResultUC
        private void OnRestartButtonClicked(object? sender, EventArgs e)
        {
            ClearUC();
            PrintStartUC();
        }

        // Открытие экрана начала игры
        private void PrintStartUC() => Controls.Add(startUC);

        // Открытие экрана результата
        private void PrintResultUC()
        {
            if (gameCore?.isVictory == true)
                Controls.Add(winUC);
            else
                Controls.Add(loseUC);
        }

        // Закрытие экранов UserControl
        private void ClearUC()
        {
            var userControls = Controls.OfType<UserControl>().ToList();
            foreach (var uc in userControls)
            {
                Controls.Remove(uc);
            }
        }
        // начинает игру
        private void StartGame(object? sender, EventArgs e)
        {
            ClearUC();
            gameCore = new GameCore(ClientSize.Width, ClientSize.Height);
            gameTimer?.Start();
            Focus();// Возвращаем фокус форме!
            ActiveControl = null; // чтобы ни один дочерний контрол не имел фокуса
        }
        // создает таймер
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
        // каждый тик таймера вызываем тик игрового процесса, затем её перерисовку
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
                        PrintResultUC();
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
        // отрисовка  шара(мяча)(аегиса)
        private void ballPaint(PaintEventArgs e)
        {
            if (gameCore != null)
            {
                e.Graphics.DrawImage(BallParameters.Image, gameCore.BallX, gameCore.BallY, BallParameters.Size, BallParameters.Size);
            }
        }
        // отрисовка каждого из кирпичика
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
        // отрисовка игровой платформы
        private void PlayerPlatformPaint(PaintEventArgs e)
        {
            if (gameCore != null)
            {
                e.Graphics.DrawImage(PlayerPlatformParameters.Image, gameCore.PlatformX, gameCore.PlatformY, PlayerPlatformParameters.Width, PlayerPlatformParameters.Height);
            }
        }
        // управление игрока
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
        // проверка на миссклик и закрытие формы
        private void ExitPB_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.OKCancel);
            if (dialog == DialogResult.OK)
            {
                Close();
            }
        }
        // красивая анимка 'кнопки' выхода
        private void ExitPB_MouseDown(object sender, MouseEventArgs e)
        {
            ExitPB.Image = FormImages.ClickImage;
        }

        private void ExitPB_MouseUp(object sender, MouseEventArgs e)
        {
            ExitPB.Image = FormImages.DefaultImage;
        }

        private void ExitPB_MouseEnter(object sender, EventArgs e)
        {
            ExitPB.Image = FormImages.EnterImage;
        }
        private void ExitPB_MouseLeave(object sender, EventArgs e)
        {
            ExitPB.Image = FormImages.DefaultImage;
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ExitPB.Location = new Point(ClientSize.Width - ExitPB.Width, 0);
        }
    }
}
