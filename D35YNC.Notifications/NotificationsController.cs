/************** 
 * File: D35YNC.Notifications/NotificationsController.cs
 * Description: Pop-up notification library
 * D35YNC; 2019-2020
 **************/


using D35YNC.Notifications.Enums;
using D35YNC.Notifications.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace D35YNC.Notifications
{
    public class NotificationsController
    {
        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public NotificationMarkup Marking;

        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public SolidColorBrush ForegroundColor;

        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public SolidColorBrush BackgroundColor;

        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public bool ReserveList = true;

        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        /// <summary>Вертикальное расстояние между окнами</summary>
        public int WindowsPadding = 5;

        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        /// <summary>Максимальное количество уведомлений, которые могут одновременно находиться на экране</summary>
        public int MaxNotifyCount = 5;
        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ// ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public int NotificationTimeout = 3000;
        public int AnimationDuration = 300;


        private readonly List<Notification> _NotificationsList;


        public NotificationsController()
        {
            Marking = new NotificationMarkup();
            _NotificationsList = new List<Notification>();
        }


        public NotificationsController(NotificationMarkup customMarking)
        {
            Marking = customMarking ?? throw new Exception("The value cannot be equal to null");
            _NotificationsList = new List<Notification>();
        }


        /// <summary>Показывает наследника от <see cref="D35YNC.Notifications.Notification"/></summary>
        public void ShowNotification(Notification window)
        {
            if (window is Notification)
            {
                (window as Notification).OnHided += UnregisterNotify;
                RegisterNotify(window);
            }
            else
            {
                throw new Exception("Window is not are D35YNC.Notifications.Notification.");
            }
        }


        /// <summary>Показывает <see cref="D35YNC.Notifications.SimpleNotification"/> с заданными параметрами</summary>
        /// <param name="header">Заголовок уведомления</param>
        /// <param name="text">Текст уведомления</param>
        /// <param name="timeout">Таймаут уведомления</param>
        /// <param name="animDuration">Длительность анимации</param>
        public void ShowNotification(string header, string text, int timeout = -1, int animDuration = -1)
        {
            if (timeout <= 0)
            {
                timeout = NotificationTimeout;
            }
            if (animDuration <= 0)
            {
                animDuration = AnimationDuration;
            }
            ShowNotification(new SimpleNotification(header, text, Marking, timeout, animDuration, ForegroundColor, BackgroundColor));
        }


        /// <summary>Закрывает все активные уведомления</summary>
        public void CloseAll()
        {
            foreach (Window notification in _NotificationsList)
            {
                notification.Close();
            }
            _NotificationsList.Clear();
        }
        
        
        private void RegisterNotify(Notification window)
        {
            if (_NotificationsList.Count >= MaxNotifyCount)
            {
                UnregisterNotify(_NotificationsList[0]);
            }

            if (!_NotificationsList.Contains(window))
            {
                if (ReserveList)
                {
                    _NotificationsList.Insert(0, window);
                }
                else
                {
                    _NotificationsList.Add(window);
                }

                window.Show();
                UpdateWindowsPos();
            }
        }
        
        
        private void UnregisterNotify(Notification window)
        {
            if (_NotificationsList.Contains(window))
            {
                _NotificationsList.Remove(window);
                window.Close();
                UpdateWindowsPos();
            }
        }

        
        private void UpdateWindowsPos()
        {
            switch (NotificationBehavior.Position)
            {
                case NotifyPosition.TopLeft:
                    {
                        double top = WindowsPadding;

                        foreach (Window window in _NotificationsList)
                        {
                            window.Top = top;
                            top += (window.Height + WindowsPadding);
                        }
                        break;
                    }
                case NotifyPosition.TopRight:
                    {
                        double top = WindowsPadding;

                        foreach (Window window in _NotificationsList)
                        {
                            window.Top = top;
                            top += (window.Height + WindowsPadding);
                        }
                        break;
                    }
                case NotifyPosition.BottomLeft:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _NotificationsList)
                        {
                            top -= (window.Height + WindowsPadding);
                            window.Top = top;
                        }
                        break;
                    }
                case NotifyPosition.BottomRight:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _NotificationsList)
                        {
                            top -= (window.Height + WindowsPadding);
                            window.Top = top;
                        }
                        break;
                    }
            }
        }
    }
}
