namespace ArkanoidWF.Constants;

internal static class PlayerPlatformParameters
{
    /// <summary>
    /// Картинка платформы
    /// </summary>
    public static readonly Image Image = Properties.Resources.katana;

    /// <summary>
    /// Длина платформы
    /// </summary>
    public const int Width = MonitorParameters.MaximizeWidth/4;

    /// <summary>
    /// Ширина платформы
    /// </summary>
    public const int Height = (int)(Width/12.5);

    /// <summary>
    /// Скорость платформы
    /// </summary>
    public const float Speed = Width/10;
}
