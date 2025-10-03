
using ArkanoidWF.Constants;
using System.Runtime.InteropServices;

namespace ArkanoidWF
{
    internal class GameCore
    {
        private readonly Ball ball;

        private readonly PlayerPlatform playerPlatform;

        private readonly List<Brick> bricks = new List<Brick>();

        private readonly float maxWidth;

        private readonly float maxHeight;

        private bool moveLeft = false;
        private bool moveRight = false;
        public void SetMoveLeft(bool value) => moveLeft = value;
        public void SetMoveRight(bool value) => moveRight = value;
        // Внешний код может только читать, но не менять список
        public IReadOnlyList<Brick> Bricks => bricks.AsReadOnly();
        public bool isGameOver { get; private set; } = false;

        public GameCore(float maxWidth, float maxHeight)
        {
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;

            playerPlatform = new PlayerPlatform((maxWidth-PlayerPlatformParameters.Width) / 2, maxHeight - 50);

            ball = new Ball(x: maxWidth/2, y: 300, speed: 10);

            fillBricksLevelOne();
        }
        public void Tick()
        {
            playerAction();
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
        private void playerAction()
        {
            if (moveLeft && playerPlatform.X > 0)
                playerPlatform.MoveLeft();
            if (moveRight && playerPlatform.X + playerPlatform.Width < maxWidth)
                playerPlatform.MoveRight();

            // Здесь также обновляйте шар, кирпичи и т.д.
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

        public float PlatformX => playerPlatform.X;
        public float PlatformY => playerPlatform.Y;
    }
}
