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

namespace D35YNC.Notifications
{
    /// <summary>
    /// Контроллер уведомлений. Создает, показывает и закрывает уведомления.
    /// </summary>
    public class NotificationsController
    {
        /// <summary>
        /// Конфигурация для <see cref="D35YNC.Notifications.SimpleNotification"/>
        /// </summary>
        public WindowConfig Config 
        {
            get
            {
                if (_CanUseConfig)
                {
                    return _WindowConfig;
                }
                else
                {
                    throw new Exception("Can't use config");
                }                
            }
            set
            {
                if (_CanUseConfig)
                {
                    if (value != null)
                    {
                        _WindowConfig = value;
                    }
                    else
                    {
                        throw new Exception("Value is null");
                    }
                }
                else
                {
                    throw new Exception("Can't use config");
                }
            }
        }
        

        /// <summary>
        /// Инвертирует порядок уведомлений на экране
        /// </summary>
        public bool ReserveList = false;
        
        /// <summary>Вертикальное расстояние между окнами</summary>
        public int WindowsPadding = 5;
        
        /// <summary>Максимальное количество уведомлений, которые могут одновременно находиться на экране</summary>
        public int MaxNotifyCount = 5;

        private WindowConfig _WindowConfig;
        private readonly List<Notification> _Notifications;
        private readonly bool _CanUseConfig;

        /// <summary>
        /// Инициализирует контроллер
        /// </summary>
        /// <param name="useSimpleNoti">true, для использования и настройки <see cref="D35YNC.Notifications.SimpleNotification"/></param>
        /// <param name="customConfig">Кастомные настройки</param>
        public NotificationsController(bool useSimpleNoti, WindowConfig customConfig = null)
        {
            _Notifications = new List<Notification>();
            if (useSimpleNoti)
            {
                Config = customConfig ?? new WindowConfig();
                _CanUseConfig = true;
            }
            else
            {
                _CanUseConfig = false;
            }
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
            if (_CanUseConfig)
            {
                ShowNotification(new SimpleNotification(header, text, Config, timeout, animDuration));
            }
            else
            {
                throw new Exception("Сan't initialize the window without configuration");
            }
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
