using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace ArkanoidWF
{
    internal class Ball
    {
        /// <summary>
        /// Ресурс картинки для шара
        /// </summary>
        public static readonly Image BallImage = Properties.Resources.aegis;

        /// <summary>
        /// Переменная картинки шара
        /// </summary>
        public Image Image { get; set; } = BallImage;

        /// <summary>
        /// Координата X шара
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Коородината Y шара
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Размер шара
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Скорость шара
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Угол движения шара
        /// </summary>
        public float Angle { get; set; }

        private FloatPoint center;

        public Ball(float x, float y, int size, float speed,float angle)
        {
            X = x;
            Y = y;
            Size = size;
            Speed = speed;
            Angle = angle;

            // вычисляем центр шара
            center = new FloatPoint(x+(float)(size/2), y + (float)(size / 2));
        }

        /// <summary>
        /// Пересекает ли кирпич
        /// </summary>
        /// <param name="brick">Кирпич</param>
        /// <returns>Статус пересечения</returns>
        private bool collidesWith(Rectangle brick)
        {
            float r = Size / 2.0f;

            // Ближайшая точка на прямоугольнике к центру окружности
            float closestX = Clamp(center.X, brick.X, brick.X + brick.Width);
            float closestY = Clamp(center.Y, brick.Y, brick.Y + brick.Height);

            // Квадрат расстояния от центра до ближайшей точки
            float dx = center.X - closestX;
            float dy = center.Y - closestY;
            float distanceSquared = dx * dx + dy * dy;

            return distanceSquared <= r * r;
        }
        public void Move()
        {
            X += Speed * (float)Math.Cos(Angle);
            Y += Speed * (float)Math.Sin(Angle);
        }

        public void BounceOffWalls(float maxWidth, float maxHeight)
        {
            if (Y <= 0 || Y + Size >= maxHeight)
                Angle = -Angle;
            if (X <= 0 || X + Size >= maxWidth)
                Angle = (float)(Math.PI - Angle);
        }
        private static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
