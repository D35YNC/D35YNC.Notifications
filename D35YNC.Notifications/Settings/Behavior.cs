/************** 
 * File: D35YNC.Notifications/Settings/Behavior.cs
 * Description: Pop-up notification library
 * D35YNC 2019 - 2020
 **************/

using D35YNC.Notifications.Enums;

namespace D35YNC.Notifications.Settings
{
    public static class Behavior
    {
        /// <summary>
        /// Расположение уведомлений на экране
        /// </summary>
        public static Position Position = Position.BottomRight;


        /// <summary>
        /// Тип анимации открытия уведомления
        /// </summary>
        public static AnimationType ShowAnimationType = AnimationType.Transparent;


        /// <summary>
        /// Тип анимации закрытия уведомления
        /// </summary>
        public static AnimationType HideAnimationType = AnimationType.Transparent;
    }
}
