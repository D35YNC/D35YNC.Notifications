/************** 
 * File: D35YNC.Notifications/Config/NotifyBehavior.cs
 * Description: Pop-up notification library
 * D35YNC; 2019 - 2020
 **************/

using D35YNC.Notifications.Enums;

namespace D35YNC.Notifications.Settings
{
    public static class NotificationBehavior
    {
        /// <summary>Расположение уведомлений на экране</summary>
        public static NotifyPosition Position = NotifyPosition.BottomRight;

        /// <summary>Тип анимации открытия уведомления</summary>
        public static NotifyAnimationType ShowAnimation = NotifyAnimationType.Transparent;

        /// <summary>Тип анимации закрытия уведомления</summary>
        public static NotifyAnimationType HideAnimation = NotifyAnimationType.Transparent;
    }
}
