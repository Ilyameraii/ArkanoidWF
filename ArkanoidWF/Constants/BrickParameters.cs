namespace ArkanoidWF.Constants;

internal static class BrickParameters
{
    /// <summary>
    /// Длина кирпича
    /// </summary>
    public const int Width = MonitorParameters.MaximizeWidth/16;

    /// <summary>
    /// Ширина кирпича
    /// </summary>
    public const int Height = (int)(Width/2.5);

    /// <summary>
    /// Толщина контура кирпича
    /// </summary>
    public const float Bold = MonitorParameters.MaximizeWidth/400f;
}
