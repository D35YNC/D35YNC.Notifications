/************** 
 * File: D35YNC.Notifications/NotificationsController.cs
 * Description: Pop-up notification library
 * D35YNC 2019-2020
 **************/


using D35YNC.Notifications.Enums;
using D35YNC.Notifications.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace D35YNC.Notifications
{
    public class NotificationsController
    {
        // ОСТАВИТЬ ТУТ ИЛИ УБРАТЬ
        public Markup Markup;

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


        private readonly List<Notification> _Notifications;

        
        /// <summary>
        /// Инициализирует контроллер со стандартными параметрами
        /// </summary>
        public NotificationsController()
        {
            Markup = new Markup();
            _Notifications = new List<Notification>();
        }


        /// <summary>
        /// Инициализирует контроллер с кастомными параметрами
        /// </summary>
        /// <param name="customMarkup"></param>
        public NotificationsController(Markup customMarkup)
        {
            Markup = customMarkup ?? throw new Exception("The value cannot be equal to null");
            _Notifications = new List<Notification>();
        }


        /// <summary>
        /// Показывает наследника от <see cref="D35YNC.Notifications.Notification"/>
        /// </summary>
        public void ShowNotification(Notification notification)
        {
            notification.OnHided += UnregisterNotify;
            RegisterNotify(notification);
        }


        /// <summary>
        /// Показывает <see cref="D35YNC.Notifications.SimpleNotification"/> с заданными параметрами
        /// </summary>
        /// <param name="header">Заголовок уведомления</param>
        /// <param name="text">Текст уведомления</param>
        /// <param name="timeout">Таймаут уведомления (ms)</param>
        /// <param name="animDuration">Длительность анимации (ms)</param>
        public void ShowNotification(string header, string text, int timeout = -1, int animDuration = -1)
        {
            ShowNotification(new SimpleNotification(header, text, Markup, timeout, animDuration, ForegroundColor, BackgroundColor));
        }


        /// <summary>
        /// Устанавливает новое значение таймаута для <see cref="D35YNC.Notifications.Notification"/>
        /// </summary>
        /// <param name="newValue">ms</param>
        public void SetDefaultTimeout(int newValue)
        {
            if (newValue > 0)
            {
                Notification.DefaultTimeout = newValue;
            }
            else
            {
                throw new Exception("Value less than zero");
            }
        }

        /// <summary>
        /// Устанавливает новое значение длительности анимации для <see cref="D35YNC.Notifications.Notification"/>
        /// </summary>
        /// <param name="newValue">ms</param>
        public void SetDefaultAnimDuration(int newValue)
        {
            if (newValue > 0)
            {
                Notification.DefaultAnimDuration = newValue;
            }
            else
            {
                throw new Exception("Value less than zero");
            }
        }


        /// <summary>
        /// Закрывает все активные уведомления
        /// </summary>
        public void CloseAll()
        {
            foreach (Window notification in _Notifications)
            {
                notification.Close();
            }
            _Notifications.Clear();
        }
        
        
        private void RegisterNotify(Notification window)
        {
            if (_Notifications.Count >= MaxNotifyCount)
            {
                UnregisterNotify(_Notifications[0]);
            }

            if (!_Notifications.Contains(window))
            {
                if (ReserveList)
                {
                    _Notifications.Insert(0, window);
                }
                else
                {
                    _Notifications.Add(window);
                }

                window.Show();
                UpdateWindowsPos();
            }
        }
        
        
        private void UnregisterNotify(Notification window)
        {
            if (_Notifications.Contains(window))
            {
                _Notifications.Remove(window);
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
                        double top = WindowsPadding;

                        foreach (Window window in _Notifications)
                        {
                            window.Top = top;
                            top += (window.Height + WindowsPadding);
                        }
                        break;
                    }
                case Position.TopRight:
                    {
                        double top = WindowsPadding;

                        foreach (Window window in _Notifications)
                        {
                            window.Top = top;
                            top += (window.Height + WindowsPadding);
                        }
                        break;
                    }
                case Position.BottomLeft:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _Notifications)
                        {
                            top -= (window.Height + WindowsPadding);
                            window.Top = top;
                        }
                        break;
                    }
                case Position.BottomRight:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in _Notifications)
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
