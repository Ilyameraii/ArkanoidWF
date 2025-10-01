using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidWF
{
    internal class Ball
    {
        private static readonly Image BallImage = Properties.Resources.aegis;

        /// <summary>
        /// Переменная картинки шара
        /// </summary>
        public Image Image { get; } = BallImage;

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
        public readonly int Size;

        // Координаты центра шара
        private FloatPoint Center => new FloatPoint(X + Size / 2f, Y + Size / 2f);

        public Ball(float x, float y, int size, float speed, float angle)
        {
            X = x;
            Y = y;
            Size = size;
            Speed = speed;
            Angle = angle;
        }
        private void CollideVertical()
        {
            Angle = (float)Math.PI - Angle;
        }
        private void CollideHorizontal()
        {
            Angle =  - Angle;
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

        public void BounceOffBrick(Rectangle brick)
        {
            if (!collidesWith(brick)) return;

            HitSide side = GetHitSide(brick);

            // Выталкиваем шар из кирпича
            ResolvePenetration(brick, side);

            switch (side)
            {
                case HitSide.Top:
                case HitSide.Bottom:
                    CollideHorizontal(); // отскок по Y
                    break;
                case HitSide.Left:
                case HitSide.Right:
                    CollideVertical();   // отскок по X
                    break;
            }
        }
        private void ResolvePenetration(Rectangle brick, HitSide side)
        {
            float r = Size / 2.0f;

            switch (side)
            {
                case HitSide.Top:
                    // Шар должен быть НАД кирпичом
                    Y = brick.Y - Size;
                    break;
                case HitSide.Bottom:
                    // Шар должен быть ПОД кирпичом
                    Y = brick.Y + brick.Height;
                    break;
                case HitSide.Left:
                    // Шар должен быть СЛЕВА от кирпича
                    X = brick.X - Size;
                    break;
                case HitSide.Right:
                    // Шар должен быть СПРАВА от кирпича
                    X = brick.X + brick.Width;
                    break;
            }

            // Обновляем центр (если используете поле, а не вычисляемое свойство)
            // В вашем случае Center — свойство, так что не нужно
        }
        private bool collidesWith(Rectangle brick)
        {
            float r = Size / 2.0f;

            // Ближайшая точка на прямоугольнике к центру окружности
            float closestX = Math.Clamp(Center.X, brick.X, brick.X + brick.Width);
            float closestY = Math.Clamp(Center.Y, brick.Y, brick.Y + brick.Height);

            // Квадрат расстояния от центра до ближайшей точки
            float dx = Center.X - closestX;
            float dy = Center.Y - closestY;
            float distanceSquared = dx * dx + dy * dy;

            return distanceSquared <= r * r;
        }
        private enum HitSide
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }

        private HitSide GetHitSide(Rectangle brick)
        {
            float r = Size / 2.0f;
            FloatPoint brickCenter = new FloatPoint(
                brick.X + brick.Width / 2f,
                brick.Y + brick.Height / 2f
            );

            float dx = Center.X - brickCenter.X;
            float dy = Center.Y - brickCenter.Y;

            // Сравниваем абсолютные смещения по осям
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // Удар по вертикальной грани
                return dx > 0 ? HitSide.Right : HitSide.Left;
            }
            else
            {
                // Удар по горизонтальной грани
                return dy > 0 ? HitSide.Bottom : HitSide.Top;
            }
        }
    }
}
