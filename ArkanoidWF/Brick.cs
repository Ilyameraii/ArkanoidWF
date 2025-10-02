
using ArkanoidWF.Constants;
namespace ArkanoidWF
{
    internal class Brick
    {

        public float X { get; set; }
        public float Y { get; set; }
        public  int Width { get; } = BrickParameters.Width;
        public  int Height { get; } = BrickParameters.Height;
        private int _hp;
        public int HP
        {
            get => _hp;
            private set
            {
                if (value < 0 || value > 3)
                    throw new ArgumentOutOfRangeException(nameof(value), "HP должно быть от 0 до 3.");
                else _hp = value;
            }
        }
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, Width, Height);
        public Color Color
        {
            get
            {
                return setColor();
            }
        }
        public Brick(int x, int y, int hp)
        {
            X = x;
            Y = y;
            HP = hp;
        }
        private Color setColor() {
            return HP switch
            {
                1 => BrickColors.ColorFor1HP,
                2 => BrickColors.ColorFor2HP,
                3 => BrickColors.ColorFor3HP,
                _ => Color.Black
            };
        }
        public void TakeDamage(int damage = 1)
        {
            if (HP > 0)
            {
                HP -= damage;
            }
        }
    }
}
