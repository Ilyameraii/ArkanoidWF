using ArkanoidWF.Constants;
using ArkanoidWF.Enums;
using ArkanoidWF.Interfaces;

namespace ArkanoidWF.Сlasses;

internal class Ball
{
    /// <summary>
    /// Координата X шара
    /// </summary>
    public float X { get; private set; }

    /// <summary>
    /// Координата Y шара
    /// </summary>
    public float Y { get; private set; }


    private float angle;

    /// <summary>
    /// Угол движения шара
    /// </summary>
    public float Angle
    {
        get => angle;
        private set
        {
            angle = value;
            // Опционально: нормализация
            angle = (float)(angle % (2 * Math.PI));
            if (angle < 0) angle += (float)(2 * Math.PI);
        }
    }

    /// <summary>
    /// Скорость шара
    /// </summary>
    public float Speed { get; }

    /// <summary>
    /// Размер шара
    /// </summary>
    public int Size { get; } = BallParameters.Size;

    /// <summary>
    /// Радиус
    /// </summary>
    private readonly float radius;

    // Координаты центра шара
    private FloatPoint Center => new (X + radius, Y + radius);

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="x">Начальная позиция шара по X</param>
    /// <param name="y">Начальная позиция шара по Y</param>
    /// <param name="speed">скорость шара</param>
    public Ball(float x, float y, float speed)
    {
        X = x;
        Y = y;
        Speed = speed;
        radius = Size / 2f; // находим сразу радиус для упрощения и сокращения написания методов
        Angle = (float)Math.PI / 2f; // сначала шар будет двигаться строго горизонтально вниз
    }

    // логика отскока от вертикальной поверхности
    private void CollideVertical()
    {
        Angle = (float)Math.PI - Angle;
    }

    // логика отскока от горизонтальной поверхности
    private void CollideHorizontal()
    {
        Angle = -Angle;
    }

    /// <summary>
    /// Движение шара
    /// </summary>
    public void Move()
    {
        X += Speed * (float)Math.Cos(Angle);
        Y += Speed * (float)Math.Sin(Angle);
    }

    /// <summary>
    /// Проверка на столкновение и логика столкновения шара со стенами
    /// </summary>
    /// <param name="maxWidth">длина формы</param>
    public void BounceOffWalls(float maxWidth)
    {
        // Проверка столкновения с верхней/нижней стенкой
        if (Y <= 0)
        {
            Y = 0; // выталкиваем вниз до края
            CollideHorizontal();
        }

        // Проверка столкновения с левой/правой стенкой
        if (X <= 0)
        {
            X = 0; // выталкиваем вправо до края
            CollideVertical();
        }
        else if (X + Size >= maxWidth)
        {
            X = maxWidth - Size; // выталкиваем влево до края
            CollideVertical();
        }
    }

    // проверка на столкновение и логика столкновения с прямоугольными объектами
    private void BounceOffRectangle(IRectangle rect, Action<HitSide> onHorizontalBounce)
    {
        if (!IsBallCollidesWith(rect))
            return;

        var side = GetHitSide(rect);

        // Коррекция позиции (выталкивание)
        switch (side)
        {
            case HitSide.Top:
                Y = rect.Y - Size;
                break;
            case HitSide.Bottom:
                Y = rect.Y + rect.Height;
                break;
            case HitSide.Left:
                X = rect.X - Size;
                break;
            case HitSide.Right:
                X = rect.X + rect.Width;
                break;
        }

        // Обработка отскока
        switch (side)
        {
            case HitSide.Top:
            case HitSide.Bottom:
                onHorizontalBounce?.Invoke(side); // разделяем логику горизонтального столкновения у прямоугольных объектов
                break;
            case HitSide.Left:
            case HitSide.Right:
                CollideVertical();
                break;
        }
    }

    /// <summary>
    /// логика столкновения с кирпичом
    /// </summary>
    /// <param name="brick"></param>
    public void BounceOffBrick(Brick brick)
    {
        if (!IsBallCollidesWith(brick))
            return;
        BounceOffRectangle(brick, side =>
        {
            CollideHorizontal(); // простой отскок
        });
        brick.TakeDamage(1);

       
    }

    /// <summary>
    /// логика столкновения с платформой
    /// </summary>
    /// <param name="platform"></param>
    public void BounceOffPlatform(PlayerPlatform platform)
    {
        BounceOffRectangle(platform, side =>
        {
            // Игнорируем удар снизу (шар не должен туда попадать)
            if (side == HitSide.Bottom)
                return;
            // Угол зависит от позиции удара
            var maxBounceAngle = MathF.PI / 3f;
            var relativeX = (Center.X - platform.X) / platform.Width;
            var normalized = Math.Clamp(2 * relativeX - 1, -1f, 1f);
            Angle = -MathF.PI / 2 + normalized * maxBounceAngle;
        });
    }

    // проверка на столкновение
    private bool IsBallCollidesWith(IRectangle rectangle)
    {

        // Ближайшая точка на прямоугольнике к центру окружности
        var closestX = Math.Clamp(Center.X, rectangle.X, rectangle.X + rectangle.Width);
        var closestY = Math.Clamp(Center.Y, rectangle.Y, rectangle.Y + rectangle.Height);

        // Квадрат расстояния от центра до ближайшей точки
        var dx = Center.X - closestX;
        var dy = Center.Y - closestY;
        var distanceSquared = dx * dx + dy * dy;
        return distanceSquared <= radius * radius;
    }

    // вычисление стороны прямоугольника, с которой сталкиваемся
    private HitSide GetHitSide(IRectangle rectangle)
    {
        // Границы кирпича
        var left = rectangle.X;
        var right = rectangle.X + rectangle.Width;
        var top = rectangle.Y;
        var bottom = rectangle.Y + rectangle.Height;

        // Расстояния от центра шара до граней
        var overlapLeft = Center.X - left;
        var overlapRight = right - Center.X;
        var overlapTop = Center.Y - top;
        var overlapBottom = bottom - Center.Y;

        // Находим минимальное перекрытие (глубину проникновения)
        var minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight),
                                    Math.Min(overlapTop, overlapBottom));

        if (minOverlap == overlapLeft) return HitSide.Left;
        if (minOverlap == overlapRight) return HitSide.Right;
        if (minOverlap == overlapTop) return HitSide.Top;
        return HitSide.Bottom;
    }
}
