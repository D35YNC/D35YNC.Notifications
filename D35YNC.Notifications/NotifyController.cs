/************** 
 * File: D35YNC.Notifications/NotifyController.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/
 
 
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace D35YNC.Notifications
{
    /// <summary> Контроллер уведомлений. </summary>
    public class NotifyController
    {
        /// <summary> Конфигурация уведомлений. </summary>
        public NotifyConfig Config { get; set; }

        /// <summary> Список уведомлений. </summary>
        private List<Window> NotifyList { get; }

        /// <summary> Стандартный конструктор. </summary>
        public NotifyController()
        {
            Config = new NotifyConfig();
            NotifyList = new List<Window>();
        }
        
        /// <summary> Показывает "персонализированное" уведомление window. </summary>
        /// <param name="window">Окно, инициализированное с дополнительными свойствами.</param>
        public void ShowNotify(Window window)
        {
            bool CheckData(INotifyWindow notifyWindow) =>
                notifyWindow.Style != null && notifyWindow.Timeout > 0 && notifyWindow.AnimationTimeout > 0;

            if (window is INotifyWindow)
            {
                (window as INotifyWindow).OnShowedNotify += NotifyTimeout;
                (window as INotifyWindow).OnHidedNotify += UnregisterNotify;
                if (CheckData((window as INotifyWindow)))
                {
                    RegisterNotify(window);
                }
                else
                {
                    throw new Exception("Notify window has bad data");
                }
            }
            else
            {
                throw new Exception("Window is not are INotifyWindow.");
            }
        }

        /// <summary> Показывает "стандартное" уведомление с типом type и текстом text. </summary>
        /// <param name="type">Тип уведомления</param>
        /// <param name="text">Текс уведомления</param>
        public void ShowNotify(int type, string text)
        {
            if (!Config.NotifyTypesConfigs.ContainsKey(type))
            {
                throw new Exception("Dictionary with types not contain this type");
            }
            NotifyWindow window = new NotifyWindow(Config, type, text);
            ShowNotify(window);
        }

        /// <summary> Метод ждет таймаут из window и проигрывает его анимацию закрытия. </summary>
        /// <param name="window">Вызывающее окно</param>
        private void NotifyTimeout(Window window) => 
            new Thread(new ThreadStart(delegate () { Thread.Sleep((window as INotifyWindow).Timeout); (window as INotifyWindow).StartHideAnimation(); }))
                .Start();

        /// <summary> Очищает все используемые ресурсы </summary>
        public void CloseAll()
        {
            while (NotifyList.Count > 0)
            {
                NotifyList[0].Close();
                NotifyList.RemoveAt(0);
            }                
        }

        /// <summary> Добавляет окно window в список NotifyList и показывает его. </summary>
        /// <param name="window">Целевое окно</param>
        private void RegisterNotify(Window window)
        {
            if (NotifyList.Count >= Config.MaxNotifyCount)
            {
                UnregisterNotify(NotifyList[0]);
            }

            if (!NotifyList.Contains(window))
            {
                if (Config.ReserveList)
                {
                    NotifyList.Insert(0, window);
                }
                else
                {
                    NotifyList.Add(window);
                }
                window.Show();
                UpdateWindowsPos();
            }
        }

        /// <summary> Удаляет окно window из списка NotifyList и закрывает его. </summary>
        /// <param name="window">Целевое окно</param>
        private void UnregisterNotify(Window window)
        {
            if (NotifyList.Contains(window))
            {
                NotifyList.Remove(window);
                window.Close();
                UpdateWindowsPos();
            }
        }

        /// <summary> Обновляет местоположение окон из списка NotifyList так, чтобы окна не перекрывали друг друга. </summary>
        private void UpdateWindowsPos()
        {
            switch (Config.Behavior.Position)
            {
                case NotifyPosition.TopLeft:
                    {
                        double top = Config.Padding;
                        
                        foreach (Window window in NotifyList)
                        {                            
                            window.Top = top;
                            top += (window.Height + Config.Padding);
                            //window.Left = 0;
                        }
                        break;
                    }
                case NotifyPosition.TopRight:
                    {
                        double top = Config.Padding;
                        //double left = SystemParameters.WorkArea.Width;

                        foreach (Window window in NotifyList)
                        {
                            window.Top = top;
                            top += (window.Height + Config.Padding);
                            //window.Left = left - window.Width;
                        }
                        break;
                    }
                case NotifyPosition.BottomLeft:
                    {
                        double top = SystemParameters.WorkArea.Height;

                        foreach (Window window in NotifyList)
                        {
                            top -= (window.Height + Config.Padding);
                            window.Top = top;
                            //window.Left = 0;
                        }
                        break;
                    }
                case NotifyPosition.BottomRight:
                    {
                        double top = SystemParameters.WorkArea.Height;
                        //double left = SystemParameters.WorkArea.Width;

                        foreach (Window window in NotifyList)
                        {
                            top -= (window.Height + Config.Padding);
                            window.Top = top;
                            //window.Left = left - window.Width;
                        }
                        break;
                    }
            }
        }

        public void AddNotifyType(int type, string header, int timeout)
        {
            if (Config.NotifyTypesConfigs.ContainsKey(type))
            {
                throw new Exception("Dictionary already contains this type");
            }
            Config.NotifyTypesConfigs.Add(type, new Tuple<string, int>(header, timeout));
        }
    }
}
