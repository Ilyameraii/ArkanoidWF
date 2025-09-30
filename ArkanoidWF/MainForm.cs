using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ArkanoidWF
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// герцовка вашего монитора
        /// </summary>
        private static float Herz = 120.0f;
        private System.Windows.Forms.Timer gameTimer; 
        private Ball ball;
        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            ball.Move();
            ball.BounceOffWalls(ClientSize.Width, ClientSize.Height);
            Invalidate();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            ball = new Ball(x:300,y:300, size:50, speed:ClientSize.Width/100, angle: 1.5708f);
            gameTimer.Start();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawImage(ball.Image, ball.X, ball.Y, ball.Size,ball.Size);

        }
    }
}
