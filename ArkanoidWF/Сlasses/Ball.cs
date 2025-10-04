using ArkanoidWF.Constants;
using ArkanoidWF.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ArkanoidWF.Сlasses
{
    internal class Ball
    {

        /// <summary>
        /// Переменная картинки шара
        /// </summary>
        public Image Image { get; } = BallParameters.Image;

        /// <summary>
        /// Координата X шара
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Коородината Y шара
        /// </summary>
        public float Y { get; private set; }


        private float _angle;

        /// <summary>
        /// Угол движения шара
        /// </summary>
        public float Angle
        {
            get => _angle;
            private set
            {
                _angle = value;
                // Опционально: нормализация
                _angle = (float)(_angle % (2 * Math.PI));
                if (_angle < 0) _angle += (float)(2 * Math.PI);
            }
        }

        /// <summary>
        /// Скорость шара
        /// </summary>
        public readonly float Speed;

        /// <summary>
        /// Размер шара
        /// </summary>
        public int Size { get; } = BallParameters.Size;

        /// <summary>
        /// Радиус
        /// </summary>
        private readonly float radius;

        // Координаты центра шара
        private FloatPoint Center => new FloatPoint(X + radius, Y + radius);

        public Ball(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
            radius = Size / 2f;
            Angle = (float)Math.PI / 2f;
        }
        private void CollideVertical()
        {
            Angle = (float)Math.PI - Angle;
        }
        private void CollideHorizontal()
        {
            Angle = -Angle;
        }
        public void Move()
        {
            X += Speed * (float)Math.Cos(Angle);
            Y += Speed * (float)Math.Sin(Angle);
        }

        public void BounceOffWalls(float maxWidth, float maxHeight)
        {
            if (Y <= 0 || Y + Size >= maxHeight)
                CollideHorizontal();
            if (X <= 0 || X + Size >= maxWidth)
                CollideVertical();
        }
        private void BounceOffRectangle(IRectangle rect, Action<HitSide> onHorizontalBounce)
        {
            if (!IsBallCollidesWith(rect))
                return;

            HitSide side = GetHitSide(rect);

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
                    onHorizontalBounce?.Invoke(side);
                    break;
                case HitSide.Left:
                case HitSide.Right:
                    CollideVertical();
                    break;
            }
        }
        public void BounceOffBrick(Brick brick)
        {
            if (!IsBallCollidesWith(brick))
                return;

            brick.TakeDamage(1);

            BounceOffRectangle(brick, side =>
            {
                CollideHorizontal(); // простой отскок
            });
        }

        public void BounceOffPlatform(PlayerPlatform platform)
        {
            BounceOffRectangle(platform, side =>
            {
                // Угол зависит от позиции удара
                var maxBounceAngle = MathF.PI / 3f;
                var relativeX = (Center.X - platform.X) / platform.Width;
                var normalized = Math.Clamp(2 * relativeX - 1, -1f, 1f);
                Angle = -MathF.PI / 2 + normalized * maxBounceAngle;
            });
        }
        private bool IsBallCollidesWith(IRectangle rectangle)
        {

            // Ближайшая точка на прямоугольнике к центру окружности
            float closestX = Math.Clamp(Center.X, rectangle.X, rectangle.X + rectangle.Width);
            float closestY = Math.Clamp(Center.Y, rectangle.Y, rectangle.Y + rectangle.Height);

            // Квадрат расстояния от центра до ближайшей точки
            float dx = Center.X - closestX;
            float dy = Center.Y - closestY;
            float distanceSquared = dx * dx + dy * dy;
            return distanceSquared <= radius * radius;
        }
        private HitSide GetHitSide(IRectangle rectangle)
        {
            // Границы кирпича
            float left = rectangle.X;
            float right = rectangle.X + rectangle.Width;
            float top = rectangle.Y;
            float bottom = rectangle.Y + rectangle.Height;

            // Расстояния от центра шара до граней
            float overlapLeft = Center.X - left;
            float overlapRight = right - Center.X;
            float overlapTop = Center.Y - top;
            float overlapBottom = bottom - Center.Y;

            // Находим минимальное перекрытие (глубину проникновения)
            float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight),
                                        Math.Min(overlapTop, overlapBottom));

            if (minOverlap == overlapLeft) return HitSide.Left;
            if (minOverlap == overlapRight) return HitSide.Right;
            if (minOverlap == overlapTop) return HitSide.Top;
            return HitSide.Bottom;
        }
    }
}
