/************** 
 * File: D35YNC.Notifications/Notification.cs
 * Description: Pop-up notification library
 * D35YNC 2019 - 2020
 **************/


using D35YNC.Notifications.Enums;
using D35YNC.Notifications.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace D35YNC.Notifications
{
    public abstract partial class Notification : Window
    {
        public delegate void ShowedNotifyEventHandler(Notification window);
        public delegate void HidedNotifyEventHandler(Notification window);


        /// <summary>
        /// Происходит после окончания анимации появления окна
        /// </summary>
        public event ShowedNotifyEventHandler OnShowed;


        /// <summary>
        /// Происходит после окончания анимации закрытия окна
        /// </summary>
        public event HidedNotifyEventHandler OnHided;


        /// <summary>
        /// Стандартный таймаут для <see cref="D35YNC.Notifications.Notification"/>
        /// </summary>
        public static int DefaultTimeout
        {
            get
            {
                return _DefaultTimeout;
            }
            set
            {
                if (value > 0)
                {
                    _DefaultTimeout = value;
                }
                else
                {
                    throw new Exception("Value less than zero");
                }
            }
        }


        /// <summary>
        /// Стандартная длительность анимации для <see cref="D35YNC.Notifications.Notification"/>
        /// </summary>
        public static int DefaultAnimDuration
        {
            get
            {
                return _DefaultAnimDuration;
            }
            set
            {
                if (value > 0)
                {
                    _DefaultAnimDuration = value;
                }
                else
                {
                    throw new Exception("Value less than zero");
                }
            }
        }


        private int _Timeout = 3000;

        private static int _DefaultTimeout = 3000;
        private static int _DefaultAnimDuration = 300;

        private delegate void ShowAnimationDelegate();
        private delegate void HideAnimationDelegate();

        private ShowAnimationDelegate _ShowAnimation;
        private HideAnimationDelegate _HideAnimation;


        /// <summary>
        /// Начинает инициализировать <see cref="System.Windows.Window"/> и анимации
        /// </summary>
        /// <param name="timeout">ms</param>
        /// <param name="animDuration">ms</param>
        public Notification(int timeout, int animDuration)
        {
            this.Loaded += NotifyWindow_Loaded;
            this.OnShowed += Notification_OnShowed;

            if (timeout > 0)
            {
                _Timeout = timeout;
            }
            else
            {
                _Timeout = DefaultTimeout;
            }

            if (animDuration <= 0)
            {
                animDuration = DefaultAnimDuration;
            }

            InitAnimations(animDuration);
        }


        private void InitAnimations(int animationDuration)
        {
            switch (Behavior.ShowAnimationType)
            {
                case AnimationType.Slide:
                    {
                        if (Behavior.Position == Position.TopRight || Behavior.Position == Position.BottomRight)
                        {
                            _ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation((int)SystemParameters.WorkArea.Width, (int)SystemParameters.WorkArea.Width - this.Width,
                                    TimeSpan.FromMilliseconds(animationDuration));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            _ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(-this.Width, 0, TimeSpan.FromMilliseconds(animationDuration));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        break;
                    }
                case AnimationType.Transparent:
                    {
                        _ShowAnimation = delegate ()
                        {
                            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(animationDuration));
                            animation.Completed += ShowAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };
                        break;
                    }
            }

            switch (Behavior.HideAnimationType)
            {
                case AnimationType.Slide:
                    {
                        if (Behavior.Position == Position.TopRight || Behavior.Position == Position.BottomRight)
                        {
                            _HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(this.Left, (int)SystemParameters.WorkArea.Width,
                                    TimeSpan.FromMilliseconds(animationDuration));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            _HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(0, -this.Width, TimeSpan.FromMilliseconds(animationDuration));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        break;
                    }
                case AnimationType.Transparent:
                    {
                        _HideAnimation = delegate ()
                        {
                            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(animationDuration));
                            animation.Completed += HideAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };
                        break;
                    }
            }
        }


        private void Notification_OnShowed(Notification notification)
        {
            new Task(new Action(
                delegate ()
                {
                    if (this._Timeout > 0)
                    {
                        Thread.Sleep(this._Timeout);
                    }
                    else
                    {
                        Thread.Sleep(Notification.DefaultTimeout);
                    }
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { _HideAnimation(); });
                })).Start();
        }


        private void NotifyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _ShowAnimation();
        }


        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            OnShowed(this);
        }


        private void HideAnimation_Completed(object sender, EventArgs e)
        {
            OnHided(this);
        }
    }
}
