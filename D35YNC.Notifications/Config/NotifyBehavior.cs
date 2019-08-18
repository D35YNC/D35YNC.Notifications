/************** 
 * File: D35YNC.Notifications/Config/NotifyBehavior.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/


namespace D35YNC.Notifications
{
    public class NotifyBehavior
    {
        /// <summary>
        /// Расположение уведомлений на экране.
        /// </summary>
        public NotifyPosition Position { get; set; } = NotifyPosition.BottomRight;

        /// <summary>
        /// Тип анимации открытия уведомления
        /// </summary>
        public NotifyAnimation ShowAnimation { get; set; } = NotifyAnimation.Transparent;

        /// <summary>
        /// Тип анимации закрытия уведомления
        /// </summary>
        public NotifyAnimation HideAnimation { get; set; } = NotifyAnimation.Transparent;
    }
}
