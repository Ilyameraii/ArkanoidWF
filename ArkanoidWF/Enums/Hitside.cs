namespace ArkanoidWF.Enums;

/// <summary>
/// для определения стороны прямоугольника при столкновении
/// </summary>
internal enum HitSide
{
    /// <summary>
    /// Сторона не определена
    /// </summary>
    None,
    /// <summary>
    /// Верхняя сторона прямоугольника
    /// </summary>
    Top,
    /// <summary>
    /// Нижняя сторона прямоугольника
    /// </summary>
    Bottom,
    /// <summary>
    /// Левая сторона прямоугольника
    /// </summary>
    Left,
    /// <summary>
    /// Правая сторона прямоугольника 
    /// </summary>
    Right
}
