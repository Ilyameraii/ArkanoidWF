
using ArkanoidWF.Constants;

namespace ArkanoidWF
{
    internal class GameCore
    {
        private readonly Ball ball;

        private readonly float maxWidth;

        private readonly float maxHeight;

        private readonly List<Brick> bricks = new List<Brick>();
        public bool isGameOver { get; private set; } = false;

        // Внешний код может только читать, но не менять список
        public IReadOnlyList<Brick> Bricks => bricks.AsReadOnly();

        public GameCore(float maxWidth, float maxHeight)
        {

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            ball = new Ball(x: maxWidth/2, y: 300, speed: 10);
            fillBricksLevelOne();
        }
        private void fillBricksLevelOne()
        {
            var lastBrickRightX = 0; // координата X правой стороны последнего кирпича в ряду
            var brickWidth = BrickParameters.Width;
            while (lastBrickRightX + brickWidth < maxWidth)
            {
                var brick = new Brick(x: bricks.Count * brickWidth, y: 150, hp: 3);
                lastBrickRightX += brickWidth;
                bricks.Add(brick);
            }
            // смещение кирпичей по центру
            var displacement = (maxWidth - bricks.Count * brickWidth) / 2;

            foreach (var brick in bricks)
            {
                brick.X += displacement;
            }
        }
        public void Tick()
        {
            if (!isGameOver)
            {
                ball.Move();
                ball.BounceOffWalls(maxWidth, maxHeight);
                foreach (var brick in bricks.ToList())
                {
                    ball.BounceOffBrick(brick);
                    if (brick.HP < 1)
                    {
                        bricks.Remove(brick);
                        checkIsGameOver();
                    }
                }
            }

        }
        private void checkIsGameOver()
        {
            if (bricks.Count == 0)
            {
                isGameOver = true;
            }
        }
        // Только данные для отрисовки - без доступа к самому Ball
        public float BallX => ball.X;
        public float BallY => ball.Y;
        public int BallSize => ball.Size;
        public Image BallImage => ball.Image;
    }
}
