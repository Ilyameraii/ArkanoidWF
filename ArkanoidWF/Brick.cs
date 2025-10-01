
using ArkanoidWF.Constants;
namespace ArkanoidWF
{
    internal class Brick
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; } = BrickParameters.Width;
        public int Height { get; } = BrickParameters.Height;
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
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
        public Color Color
        {
            get
            {
                return HP switch
                {
                    1 => BrickColors.ColorFor1HP,
                    2 => BrickColors.ColorFor2HP,
                    3 => BrickColors.ColorFor3HP,
                    _ => Color.Black 
                };
            }
        }
        public Brick(int x, int y, int hp)
        {
            X = x;
            Y = y;
            HP = hp;
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
