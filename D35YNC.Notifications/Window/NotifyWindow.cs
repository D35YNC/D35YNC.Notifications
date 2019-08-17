/************** 
 * File: D35YNC.Notifications/Window/NotifyWindow.xaml.cs
 * Description: Pop-up notification library
 * D35YNC; 2019
 **************/


using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace D35YNC.Notifications
{
    /// <summary>
    /// Окно уведомления.
    /// При инициализации без конфига (NotifyWindow(NotifyStyle style)) необходимо инициализировать дополнительные свойства, такие как: Header, Text, Interval, AnimationTimeout.
    /// </summary>
    public partial class NotifyWindow : Window, INotifyWindow
    {
        /// <summary> Происходит после окончания анимации появления окна. </summary>
        public event ShowedNotifyEventHandler OnShowedNotify;

        /// <summary> Происходит после окончания анимации закрытия окна. </summary>
        public event HidedNotifyEventHandler OnHidedNotify;
        
        /// <summary> Стиль этого уведомления. </summary>
        public new NotifyStyle Style { get; set; }

        public NotifyBehavior Behavior { get; set; }
        /// <summary> Заголовок этого уведомления. </summary>
        public string Header { get; set; }

        /// <summary> Текст этого уведомления. </summary>
        public string Text { get; set; }

        /// <summary> Таймаут этого уведомления. </summary>
        public int Timeout { get; set; }

        /// <summary> Длительность анимации этого уведомления </summary>
        public int AnimationTimeout { get; set; }

        private delegate void HideAnim();
        private delegate void ShowAnim();
        private ShowAnim ShowAnimation;
        private HideAnim HideAnimation;

        private TextBlock _HeaderBlock  { get; set; }
        private TextBlock _TextBlock    { get; set; }
        private Border _Border          { get; set; }
        private StackPanel _StackPanel  { get; set; }
        private StackPanel _SPLine      { get; set; }

        /// <summary> Инициализирует окно без xaml - разметки </summary>
        private void InitComponent()
        {
            #region Window initialization
            this.WindowStyle = WindowStyle.None;
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.AllowsTransparency = true;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.Focusable = false;
            this.Loaded += Window_Loaded;

            this.Width = Style.Width;
            this.Height = Style.Height;
            if (Style.AutoHeight)
            {
                this.SizeToContent = SizeToContent.Height;
            }
            #endregion

            #region Controlls initialization
            _Border = new Border
            {
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(Style.CornerRadius),
                BorderBrush = Style.Foreground,
                Background = Style.Background
            };

            _HeaderBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = Style.Foreground,
                FontSize = 25
            };

            _SPLine = new StackPanel
            {
                Background = Style.Foreground,
                Height = 1
            };

            _TextBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = Style.Foreground,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap
            };

            _StackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            IAddChild container = _StackPanel;
            container.AddChild(_HeaderBlock);
            container.AddChild(_SPLine);
            container.AddChild(_TextBlock);

            container = _Border;
            container.AddChild(_StackPanel);

            container = this;
            container.AddChild(_Border);
            #endregion            
        }

        /// <summary>
        /// Конструктор для второго типа уведомлений.
        /// Необходимо инициализировать дополнительные свойства, такие как: Header, Text, Interval, AnimationTimeout.
        /// </summary>
        /// <param name="style">Стиль этого уведомления.</param>
        public NotifyWindow(NotifyStyle style, NotifyBehavior behavior)
        {
            Style = style;
            Behavior = behavior;
            InitComponent();
            InitAnimations();
        }

        /// <summary> Конструктор для первого типа уведомлений.</summary>
        /// <param name="config">Конфигурация этого уведомления.</param>
        /// <param name="type">Тип этого уведомления. Влияет на Header и Timeout.</param>
        /// <param name="text">Текст этого уведомления.</param>
        public NotifyWindow(NotifyConfig config, int type, string text)
        {
            Style = config.Style;
            Behavior = config.Behavior;
            Text = text;
            Header = config.NotifyTypesConfigs[type].Item1;
            Timeout = config.NotifyTypesConfigs[type].Item2;
            AnimationTimeout = config.AnimationTimeout;
            InitComponent();
            InitAnimations();
        }

        private void InitAnimations()
        {
            switch (Behavior.ShowAnimation)
            {
                case NotifyAnimation.Slide:
                    {
                        if (Behavior.Position == NotifyPosition.TopRight || Behavior.Position == NotifyPosition.BottomRight)
                        {
                            ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation((int)SystemParameters.WorkArea.Width, (int)SystemParameters.WorkArea.Width - this.Width,
                                    TimeSpan.FromMilliseconds(AnimationTimeout));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            ShowAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(-this.Width, 0, TimeSpan.FromMilliseconds(AnimationTimeout));
                                animation.Completed += ShowAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }                        
                        break;
                    }
                case NotifyAnimation.Transparent:
                    {
                        ShowAnimation = delegate ()
                        {
                            DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(AnimationTimeout));
                            animation.Completed += ShowAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };
                        break;
                    }
            }

            switch (Behavior.HideAnimation)
            {
                case NotifyAnimation.Slide:
                    {
                        if (Behavior.Position == NotifyPosition.TopRight || Behavior.Position == NotifyPosition.BottomRight)
                        {
                            HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(this.Left, (int)SystemParameters.WorkArea.Width,
                                    TimeSpan.FromMilliseconds(AnimationTimeout));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }
                        else
                        {
                            HideAnimation = delegate ()
                            {
                                DoubleAnimation animation = new DoubleAnimation(0, - this.Width, TimeSpan.FromMilliseconds(AnimationTimeout));
                                animation.Completed += HideAnimation_Completed;
                                ApplyAnimationClock(Window.LeftProperty, animation.CreateClock());
                            };
                        }                        
                        break;
                    }
                case NotifyAnimation.Transparent:
                    {
                        HideAnimation = delegate () 
                        {
                            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(AnimationTimeout));
                            animation.Completed += HideAnimation_Completed;
                            ApplyAnimationClock(Window.OpacityProperty, animation.CreateClock());
                        };                        
                        break;
                    }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _HeaderBlock.Text = Header;
            _TextBlock.Text = Text;
            ShowAnimation();            
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            OnShowedNotify(this);
        }
        private void HideAnimation_Completed(object sender, EventArgs e)
        {
            OnHidedNotify(this);
        }

        /// <summary> Воспроизводит анимацию закрытия окна через его Dispatcher </summary>
        public void StartHideAnimation()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () { HideAnimation(); });
        }        
    }
}