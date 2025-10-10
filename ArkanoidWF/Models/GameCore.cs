using ArkanoidWF.Constants;
namespace ArkanoidWF.Сlasses;

internal class GameCore
{
    // мяч
    private readonly Ball ball;

    // платформа игрока
    private readonly PlayerPlatform playerPlatform;

    // лист кирпичей
    private readonly List<Brick> bricks = new ();

    // длина формы
    private readonly float maxWidth;

    // ширина формы
    private readonly float maxHeight;

    /// <summary>
    /// Совершает ли игрок движение влево
    /// </summary>
    public bool MoveLeft = false;

    /// <summary>
    /// Совершает ли игрок движение вправо
    /// </summary>
    public bool MoveRight = false;

    /// <summary>
    /// Внешний код может только читать, но не менять список
    /// </summary>
    public IReadOnlyList<Brick> Bricks => bricks.AsReadOnly();

    /// <summary>
    /// Завершена ли игра
    /// </summary>
    public bool IsGameOver { get; private set; } = false;

    /// <summary>
    /// Победил ли игрок
    /// </summary>
    public bool isVictory { get; private set; } = false;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="maxWidth">длина формы</param>
    /// <param name="maxHeight">ширина формы</param>
    public GameCore(float maxWidth, float maxHeight)
    {
        this.maxWidth = maxWidth;
        this.maxHeight = maxHeight;

        playerPlatform = new PlayerPlatform((maxWidth-PlayerPlatformParameters.Width) / 2, maxHeight - 50);

        ball = new Ball(x: maxWidth/2, y: 300, speed: 10);

        FillBricks();
    }

    /// <summary>
    /// Фрейм игрового процесса
    /// </summary>
    public void Tick()
    {
        PlayerAction();
        if (!IsGameOver)
        {
            ball.Move();
            ball.BounceOffWalls(maxWidth);
            ball.BounceOffPlatform(playerPlatform);
            var toRemove = new List<Brick>();
            foreach (var brick in bricks)
            {
                ball.BounceOffBrick(brick);
                if (brick.HP <= 0)
                {
                    toRemove.Add(brick);
                }
            }
            foreach (var brick in toRemove)
            {
                bricks.Remove(brick);
            }
            CheckIsGameOver();
        }
    }

    // проверка действий игрока
    private void PlayerAction()
    {
        if (MoveLeft && playerPlatform.X > 0)
        { 
            playerPlatform.MoveLeft();
        }
        if (MoveRight && playerPlatform.X + playerPlatform.Width < maxWidth)
        { 
            playerPlatform.MoveRight();
        }

    }

    // заполнение кирпичами
    private void FillBricks()
    {
        var lastBrickRightX = 0f; // координата X правой стороны последнего кирпича в ряду
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

    // проверка на завершение игры
    private void CheckIsGameOver()
    {
        if (bricks.Count == 0)
        {
            isVictory = true;
            IsGameOver = true;
        }
        if (ball.Y > maxHeight)
        {
            IsGameOver = true;
        }
    }
    // Передача данных для отрисовки - без доступа к самому Ball
    public float BallX => ball.X;
    public float BallY => ball.Y;

    // Передача данных для отрисовки - без доступа к самому PlayerPlatform
    public float PlatformX => playerPlatform.X;
    public float PlatformY => playerPlatform.Y;
}
