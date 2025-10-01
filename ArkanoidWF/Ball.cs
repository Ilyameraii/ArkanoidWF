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
            Angle = -Angle;
        }
        public void Move()
        {
            X += Speed * (float)Math.Cos(Angle);
            Y += Speed * (float)Math.Sin(Angle);
        }

    }
}
