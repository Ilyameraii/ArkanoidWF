namespace ArkanoidWF.Constants
{
    internal static class PlayerPlatformParameters
    {
        /// <summary>
        /// Картинка платформы
        /// </summary>
        public static readonly Image Image = Properties.Resources.katana;

        /// <summary>
        /// Длина платформы
        /// </summary>
        public static readonly int Width = MonitorParameters.MaximizeWidth/4;

        /// <summary>
        /// Ширина платформы
        /// </summary>
        public static readonly int Height = (int)(Width/12.5);

        /// <summary>
        /// Скорость платформы
        /// </summary>
        public static readonly float Speed = Width/10;
    }
}
