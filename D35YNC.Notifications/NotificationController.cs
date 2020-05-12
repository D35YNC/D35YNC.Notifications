/************** 
 * File: D35YNC.Notifications/NotifyController.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/


using D35YNC.Notifications.Enums;
using D35YNC.Notifications.Settings;
using System.Collections.Generic;
using System.Windows;

namespace D35YNC.Notifications
{
    public class NotificationController
    {
        public Markup NotificationMarkup;
        public int DefaultTimeout = 3000;
        public int DefaultAnimationDuration { get; set; } = 300;
        

        /// <summary> Вертикальное расстояние между окнами. </summary>
        public int Padding { get; set; } = 5;

        /// <summary> 
        /// Влияет на порядок уведомлений на экране.
        /// Например, при ReserveList = false и Behavior.Position = NotifyPosition.BottomRight, последнее добавленное в список NotifyList в NotifyController, будет находиться сверху.
        /// </summary>
        public bool ReserveList { get; set; } = true;

        /// <summary> Максимальное количество уведомлений, которые могут одновременно находиться на экране. </summary>
        public int MaxNotifyCount { get; set; } = 6;

        /// <summary> Список уведомлений. </summary>
        private List<Notification> _NotifyList { get; }

        /// <summary> Стандартный конструктор. </summary>
        public NotificationController()
        {
            _NotifyList = new List<Notification>();
            NotificationMarkup = new Markup();
        }


        public void ShowNotify(Notification notification)
        {
            notification.OnHidedNotify += UnregisterNotify;
            RegisterNotify(notification);
        }


        public void ShowNotify(string header, string text, int timeout = -1, int animDuration = -1)
        {
            ShowNotify(new SimpleNotification(NotificationMarkup, header, text, timeout, animDuration));
        }

        /// <summary>Закрывает все активные уведомления</summary>
        public void CloseAll()
        {
            foreach (Notification notification in _NotifyList)
            {
                notification.Close();
            }
            _NotifyList.Clear();
        }

        /// <summary> Добавляет окно window в список NotifyList и показывает его. </summary>
        /// <param name="window">Целевое окно</param>
        private void RegisterNotify(Notification window)
        {
            if (_NotifyList.Count >= MaxNotifyCount)
            {
                UnregisterNotify(_NotifyList[0]);
            }

            if (!_NotifyList.Contains(window))
            {
                if (ReserveList)
                {
                    _NotifyList.Insert(0, window);
                }
                else
                {
                    _NotifyList.Add(window);
                }
                window.Show();
                UpdateWindowsPos();
            }
        }


        private void UnregisterNotify(Notification window)
        {
            if (_NotifyList.Contains(window))
            {
                _NotifyList.Remove(window);
                window.Close();
                UpdateWindowsPos();
            }
        }


        private void UpdateWindowsPos()
        {
            switch (Behavior.Position)
            {
                case Position.TopLeft:
                    {
                        double top = Padding;

                        foreach (Window window in _NotifyList)
                        {
                            window.Top = top;
                            top += (window.Height + Padding);
                        }
                        break;
                    }
                case Position.TopRight:
                    {
                        double top = Padding;

                        foreach (Window window in _NotifyList)
                        {
                            window.Top = top;
                            top += (window.Height + Padding);
                        }
                        break;
                    }
                case Position.BottomLeft:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _NotifyList)
                        {
                            top -= (window.Height + Padding);
                            window.Top = top;
                        }
                        break;
                    }
                case Position.BottomRight:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _NotifyList)
                        {
                            top -= (window.Height + Padding);
                            window.Top = top;
                        }
                        break;
                    }
            }
        }
    }
}