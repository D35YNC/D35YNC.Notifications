/************** 
 * File: D35YNC.Notifications/Window/INotifyWindow.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/


using System.Windows;

namespace D35YNC.Notifications
{
    public delegate void ShowedNotifyEventHandler(Window window);
    public delegate void HidedNotifyEventHandler(Window window);
    
    /// <summary> Интерфейс NotifyWindow. </summary>
    interface INotifyWindow
    {
        /// <summary> Происходит после окончания анимации появления окна. </summary>
        event ShowedNotifyEventHandler OnShowedNotify;

        /// <summary> Происходит после окончания анимации закрытия окна. </summary>
        event HidedNotifyEventHandler OnHidedNotify;

        /// <summary> Стиль этого уведомления. </summary>
        NotifyStyle Style { get; set; }        

        /// <summary> Таймаут этого уведомления. </summary>
        int Timeout { get; set; }

        /// <summary> Длительность анимации этого уведомления </summary>
        int AnimationTimeout { get; set; }

        /// <summary> Метод начала анимации закрытия окна </summary>
        void StartHideAnimation();
    }
}
