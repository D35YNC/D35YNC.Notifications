/************** 
 * File: D35YNC.Notifications/Notification.cs
 * Description: Pop-up notification library
 * D35YNC; 2019 - 2020
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


        /// <summary>Происходит после окончания анимации появления окна</summary>
        public event ShowedNotifyEventHandler OnShowed;


        /// <summary>Происходит после окончания анимации закрытия окна</summary>
        public event HidedNotifyEventHandler OnHided;


        private int _Timeout = 3000;
        private int _AnimationDuration = 300;
        private Task _TimeoutTask;
        private delegate void ShowAnimationDelegate();
        private delegate void HideAnimationDelegate();
        private ShowAnimationDelegate _ShowAnimation;
        private HideAnimationDelegate _HideAnimation;


        public Notification(int timeout, int animDuration)
        {
            this.Loaded += NotifyWindow_Loaded;
            this.OnShowed += Notification_OnShowed;

            _Timeout = timeout;
            _AnimationDuration = animDuration;

            _TimeoutTask = new Task(new Action(
                delegate
                {
                    Thread.Sleep(this._Timeout);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { _HideAnimation(); });
                }));

            InitAnimations();
        }


        private void InitAnimations()
        {
            switch (NotificationBehavior.ShowAnimation)
            {
                case NotifyAnimationType.Slide:
                    {
                        if (NotificationBehavior.Position == NotifyPosition.TopRight || NotificationBehavior.Position == NotifyPosition.BottomRight)
                        {
                            _ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation((int)SystemParameters.WorkArea.Width, (int)SystemParameters.WorkArea.Width - this.Width,
                                    TimeSpan.FromMilliseconds(_AnimationDuration));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            _ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(-this.Width, 0, TimeSpan.FromMilliseconds(_AnimationDuration));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        break;
                    }
                case NotifyAnimationType.Transparent:
                    {
                        _ShowAnimation = delegate ()
                        {
                            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(_AnimationDuration));
                            animation.Completed += ShowAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };
                        break;
                    }
            }

            switch (NotificationBehavior.HideAnimation)
            {
                case NotifyAnimationType.Slide:
                    {
                        if (NotificationBehavior.Position == NotifyPosition.TopRight || NotificationBehavior.Position == NotifyPosition.BottomRight)
                        {
                            _HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(this.Left, (int)SystemParameters.WorkArea.Width,
                                    TimeSpan.FromMilliseconds(_AnimationDuration));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            _HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(0, -this.Width, TimeSpan.FromMilliseconds(_AnimationDuration));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        break;
                    }
                case NotifyAnimationType.Transparent:
                    {
                        _HideAnimation = delegate ()
                        {
                            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(_AnimationDuration));
                            animation.Completed += HideAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };
                        break;
                    }
            }
        }


        private void Notification_OnShowed(Window window)
        {
            _TimeoutTask.Start();
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
