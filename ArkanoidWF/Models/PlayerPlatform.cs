using ArkanoidWF.Constants;
using ArkanoidWF.Interfaces;

namespace ArkanoidWF.Сlasses;

internal class PlayerPlatform : IRectangle
{
    /// <summary>
    /// Координата X платформы
    /// </summary>
    public float X { get; private set; }

    /// <summary>
    /// Коородината Y платформы
    /// </summary>
    public float Y { get; } // координата y не должна изменяться изменяться 

    /// <summary>
    /// Длина платформы
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Ширина платформы
    /// </summary>
    public int Height { get; }

    // неизменяемая скорость платформы
    private readonly float speed;

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="x">Начальная позиция платформы по X</param>
    /// <param name="y">Позиция платформы по Y</param>
    public PlayerPlatform(float x,float y)
    {
        X = x;
        Y = y;
        Width = PlayerPlatformParameters.Width;
        Height = PlayerPlatformParameters.Height;
        speed = PlayerPlatformParameters.Speed;
    }
    
    /// <summary>
    /// Движение платформы влево
    /// </summary>
    public void MoveLeft()
    {
        X -= speed;
    }

    /// <summary>
    /// Движение платформы вправо
    /// </summary>
    public void MoveRight()
    {
        X += speed;
    }
}
