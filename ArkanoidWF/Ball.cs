using ArkanoidWF.Constants;

namespace ArkanoidWF
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
        public void BounceOffBrick(Brick brick)
        {
            if (!IsBallCollidesWith(brick))
            {
                return;
            }

            brick.TakeDamage(1);
            HitSide side = GetHitSide(brick);

            switch (side)
            {
                case HitSide.Top:
                case HitSide.Bottom:
                    if (side == HitSide.Top)
                    {
                        Y = brick.Y - Size; // поднять шар над кирпичом
                    }
                    else if (side == HitSide.Bottom)
                    {
                        Y = brick.Y + brick.Height; // опустить под кирпич
                    }
                    CollideHorizontal(); // отскок по Y
                    break;
                case HitSide.Left:
                case HitSide.Right:
                    if (side == HitSide.Left)
                    {
                        X = brick.X - Size; // поднять шар над кирпичом
                    }
                    else if (side == HitSide.Right)
                    {
                        X = brick.X + brick.Width; // опустить под кирпич
                    }
                    CollideVertical();   // отскок по X
                    break;
            }

        }
        private bool IsBallCollidesWith(Brick brick)
        {

            // Ближайшая точка на прямоугольнике к центру окружности
            float closestX = Math.Clamp(Center.X, brick.X, brick.X + brick.Width);
            float closestY = Math.Clamp(Center.Y, brick.Y, brick.Y + brick.Height);

            // Квадрат расстояния от центра до ближайшей точки
            float dx = Center.X - closestX;
            float dy = Center.Y - closestY;
            float distanceSquared = dx * dx + dy * dy;
            return (distanceSquared) <= radius * radius;
        }
        private HitSide GetHitSide(Brick brick)
        {
            // Границы кирпича
            float left = brick.X;
            float right = brick.X + brick.Width;
            float top = brick.Y;
            float bottom = brick.Y + brick.Height;

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
