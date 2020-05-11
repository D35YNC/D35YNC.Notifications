﻿/************** 
 * File: D35YNC.Notifications/Settings/NotificationMarking.cs
 * Description: Pop-up notification library
 * D35YNC; 2019 - 2020
 **************/

using System;
using System.Windows;

namespace D35YNC.Notifications.Settings
{
    public class NotificationMarkup
    {
        /// <summary>Автоматическая установка высоты, с учетом содержимого</summary>
        public bool AutoHeight = false;


        /// <summary>Закруглять углы?</summary>
        public bool RoundСorners = true;


        /// <summary>Радиус закругления</summary>
        public CornerRadius CornerRadius => _CornerRadius;

        public void SetCornerRadius(double uniformRadius)
        {
            if (uniformRadius >= 0)
            {
                _CornerRadius = new CornerRadius(uniformRadius);
            }
            else
            {
                throw new Exception("Value less than zero");
            }
        }
        public void SetCornerRadius(double topLeft, double topRight, double bottomRight, double bottomLeft)
        {
            if (topLeft >= 0 && topRight >= 0 && bottomRight >= 0 && bottomLeft >= 0)
            {
                _CornerRadius = new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
            }
            else
            {
                throw new Exception("Some value less than zero");
            }
        }

        /// <summary>Высота окна</summary>
        public int Height 
        {
            get 
            {
                return _Height;
            }
            set 
            {
                if (value > 0)
                {
                    _Height = value;
                }
                else
                {
                    throw new Exception("Value less than zero");
                }
            }
        }


        /// <summary>Ширина окна</summary>
        public int Width 
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value > 0)
                {
                    _Height = value;
                }
                else
                {
                    throw new Exception("Value less than zero");
                }
            }
        }

        private CornerRadius _CornerRadius = new CornerRadius(10);
        private int _Height = 130;
        private int _Width = 300;
    }
}
