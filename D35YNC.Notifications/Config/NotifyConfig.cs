/************** 
 * File: D35YNC.Notifications/Config/NotifyConfig.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/


using System;
using System.Collections.Generic;

namespace D35YNC.Notifications
{
    /// <summary>
    /// Класс с конфигурацией уведомлений.
    /// </summary>
    public class NotifyConfig
    {
        /// <summary> Стандартный стиль уведомлений. </summary>
        public NotifyStyle Style { get; set; }

        /// <summary> Стандартное поведение уведомлений. </summary>
        public NotifyBehavior Behavior { get; set; }

        /// <summary> Заголовки и таймауты для пользовательских типов </summary>
        public Dictionary<int, Tuple<string, int>> NotifyTypesConfigs { get; set; }

        /// <summary> Длительность анимации. </summary>
        public int AnimationTimeout { get; set; } = 250;

        /// <summary> Вертикальное расстояние между окнами. </summary>
        public int Padding { get; set; } = 5;

        /// <summary> 
        /// Влияет на порядок уведомлений на экране.
        /// Например, при ReserveList = false и Behavior.Position = NotifyPosition.BottomRight, последнее добавленное в список NotifyList в NotifyController, будет находиться сверху.
        /// </summary>
        public bool ReserveList { get; set; } = true;

        /// <summary> Максимальное количество уведомлений, которые могут одновременно находиться на экране. </summary>
        public int MaxNotifyCount { get; set; } = 6;
        
        /// <summary> Стандартный конструктор. </summary>
        public NotifyConfig()
        {
            Style = new NotifyStyle();
            Behavior = new NotifyBehavior();
            NotifyTypesConfigs = new Dictionary<int, Tuple<string, int>>();
        }
    }
}
