using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidWF
{
    internal class Ball
    {
        private static readonly Image BallImage = Properties.Resources.aegis;

        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public Point[] BorderPoints { get; set; }
        Ball(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
            BorderPoints = CalculateBorderPoints();
        }
        private Point[] CalculateBorderPoints()
        {
            // находим границы шара в приближении

            int N = 8; // количество сторон вписанной фигуры (чьи вершины мы должны записать)
            int R = Size / 2; // радиус описанной окружности
            var center = new Point(X + R, Y + R); // центер окружности

            Point[] borderPoints = new Point[N];
            for (int i = 0; i < N; i++)
            {
                double angle = 2 * Math.PI * i / N;
                int x = center.X + (int)(R * Math.Cos(angle));
                int y = center.Y + (int)(R * Math.Sin(angle));
                borderPoints[i] = new Point(x, y);
            }
            return borderPoints;
        }
    }
}
