using ArkanoidWF.Constants;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
namespace ArkanoidWF
{
    internal class PlayerPlatform
    {
        public Image Image { get; }

        /// <summary>
        /// Координата X платформы
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Коородината Y платформы
        /// </summary>

        public float Y { get; } // координата y не должна изменяться изменяться 

        public int Width { get; }
        public int Height { get; }
        private readonly float speed;
        public PlayerPlatform(float x,float y)
        {
            X = x;
            Y = y;
            Image = PlayerPlatformParameters.Image;
            Width = PlayerPlatformParameters.Width;
            Height = PlayerPlatformParameters.Height;
            speed = PlayerPlatformParameters.Speed;
        }
        public void MoveLeft()
        {
            X -= speed;
        }
        public void MoveRight()
        {
            X += speed;
        }
    }

}
