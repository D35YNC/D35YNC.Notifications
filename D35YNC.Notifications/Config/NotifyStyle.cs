/************** 
 * File: D35YNC.Notifications/Config/NotifyStyle.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/

using System;
using System.Windows.Media;

namespace D35YNC.Notifications
{
    public class NotifyStyle
    {        
        private int _Height                 = 130;
        private int _Width                  = 300;
        private int _CornerRadius           = 10;
        private SolidColorBrush _Foreground = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _Background = new SolidColorBrush(Colors.White);

        /// <summary> Высота окна. </summary>
        public int Height
        {
            get => _Height;
            set
            {
                if (value > 0)
                {
                    _Height = value;
                }
                else
                {
                    throw new Exception("The value cannot be less than zero");
                }
            }
        }

        /// <summary> Ширина окна. </summary>
        public int Width
        {
            get => _Width;
            set
            {
                if (value > 0)
                {
                    _Width = value;
                }
                else
                {
                    throw new Exception("The value cannot be less than zero");
                }
            }
        }

        /// <summary> Закруглять углы? </summary>
        public bool RoundСorners { get; set; } = true;
        /// <summary> Радиус закругления </summary>
        public int CornerRadius
        {
            get => _CornerRadius;
            set
            {
                if (value > 0 && RoundСorners)
                {
                    _CornerRadius = value;
                }
                else
                {
                    _CornerRadius = 0;
                }
            }
        }

        /// <summary> Цвет текста. </summary>
        public SolidColorBrush Foreground
        {
            get => _Foreground;
            set
            {
                if (value != null)
                {
                    _Foreground = value;
                }
                else
                {
                    throw new Exception("The value cannot be equal to NULL");
                }
            }
        }

        /// <summary> Цвет фона. </summary>
        public SolidColorBrush Background
        {
            get => _Background;
            set
            {
                if (value != null)
                {
                    _Background = value;
                }
                else
                {
                    throw new Exception("The value cannot be equal to NULL");
                }
            }
        }

        /// <summary> Автоматическая установка высоты, с учетом содержимого </summary>
        public bool AutoHeight { get; set; } = false;
    }
}
