namespace ArkanoidWF.Constants
{
    internal static class BrickParameters
    {
        /// <summary>
        /// Длина кирпича
        /// </summary>
        public static readonly int Width = MonitorParameters.MaximizeWidth/16*4;

        /// <summary>
        /// Ширина кирпича
        /// </summary>
        public static readonly int Height = (int)(Width/2.5);

        /// <summary>
        /// Толщина контура кирпича
        /// </summary>
        public static readonly float Bold = MonitorParameters.MaximizeWidth/400f;
    }
}
