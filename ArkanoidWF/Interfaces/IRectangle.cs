namespace ArkanoidWF.Interfaces;

/// <summary>
/// Определяет прямоугольные границы объекта для использования в расчётах коллизий и отрисовки.
/// </summary>
internal interface IRectangle
{
    float X { get; }
    float Y { get; }
    int Width { get; }
    int Height { get; }
}
