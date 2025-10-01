

namespace ArkanoidWF
{
    internal class GameCore
    {
        private readonly Ball ball;

        private readonly float maxWidth;

        private readonly float maxHeight;

        private readonly List<Brick> bricks = new List<Brick>();

        // Внешний код может только читать, но не менять список
        public IReadOnlyList<Brick> Bricks => bricks.AsReadOnly();

        public GameCore(float maxWidth, float maxHeight)
        {

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            ball = new Ball(x: 300, y: 300, size: 50, speed: maxWidth / 100, angle: 1f);

            var brick = new Brick(x: 100, y: 100, hp: 3);
            bricks.Add(brick);
        }

        public void Tick()
        {
            ball.Move();
            ball.BounceOffWalls(maxWidth, maxHeight);
            foreach (var brick in bricks)
            {
                ball.BounceOffBrick(brick.Bounds);
            }
        }

        // Только данные для отрисовки - без доступа к самому Ball
        public float BallX => ball.X;
        public float BallY => ball.Y;
        public int BallSize => ball.Size;
        public Image BallImage => ball.Image;
    }
}
