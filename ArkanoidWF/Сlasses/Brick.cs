using ArkanoidWF.Constants;
using ArkanoidWF.Interfaces;
namespace ArkanoidWF.Сlasses
{
    internal class Brick: IRectangle
    {
        /// <summary>
        /// Координата X кирпича
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Координата Y кирпича
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Длина кирпича
        /// </summary>
        public  int Width { get; } = BrickParameters.Width;

        /// <summary>
        /// Ширина кирпича
        /// </summary>
        public  int Height { get; } = BrickParameters.Height;

        /// <summary>
        /// ХП кирпича (может быть от 0 до 3 включительно)
        /// </summary>
        public int HP
        {
            get => hp;
            private set
            {
                if (value < 0 || value > 3)
                    throw new ArgumentOutOfRangeException(nameof(value), "HP должно быть от 0 до 3.");
                else hp = value;
            }
        }
        private int hp;

        /// <summary>
        /// Границы кирпичау
        /// </summary>
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, Width, Height);

        /// <summary>
        /// Цвет кирпича
        /// </summary>
        public Color Color
        {
            get
            {
                // устанавливаем цвет
                return setColor();
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="x">Позиция кирпича по X</param>
        /// <param name="y">Позиция кирпича по Y</param>
        /// <param name="hp">ХП кирпича</param>
        public Brick(float x, float y, int hp)
        {
            X = x;
            Y = y;
            HP = hp;
        }

        // Установка цвета в зависимости от ХП
        private Color setColor() {
            return HP switch
            {
                1 => BrickColors.ColorFor1HP,
                2 => BrickColors.ColorFor2HP,
                3 => BrickColors.ColorFor3HP,
                _ => Color.Black
            };
        }

        /// <summary>
        ///  Нанесение урона кирпичку
        /// </summary>
        /// <param name="damage">кол-во урона</param>
        public void TakeDamage(int damage = 1)
        {
            if (HP > 0)
            {
                HP -= damage;
            }
        }
    }
}
