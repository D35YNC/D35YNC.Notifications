/************** 
 * File: D35YNC.Notifications/SimpleNotification.cs
 * Description: Pop-up notification library
 * D35YNC; 2019 - 2020
 **************/



using D35YNC.Notifications.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace D35YNC.Notifications
{
    /// <summary>Обычное окно. Заголовок и текст</summary>
    internal class SimpleNotification : Notification
    {
        /// <summary> Заголовок этого уведомления. </summary>
        public string Header;


        /// <summary>Текст этого уведомления</summary>
        public string Text;


        #region Components
        private Border _Border;
        private TextBlock _HeaderBlock;
        private TextBlock _TextBlock;
        private StackPanel _StackPanel;
        private StackPanel _DividingLine;
        #endregion
        
        //НАГРОМОЖДЕНИЕ КАЛА
        public SimpleNotification(string header, string text, NotificationMarkup marking, int timeout,
            int animDuration, SolidColorBrush foreground, SolidColorBrush background) : base(timeout, animDuration)
        {
            Header = header;
            Text = text;

            InitComponents(marking, foreground, background);
        }


        private void InitComponents(NotificationMarkup marking, SolidColorBrush foreground, SolidColorBrush background)
        {
            #region Window initialization
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.AllowsTransparency = true;
            this.ShowInTaskbar = false;
            this.Topmost = true;
            this.Focusable = false;
            this.Loaded += Window_Loaded;
            this.MaxWidth = 300;
            this.Width = marking.Width;
            this.Height = marking.Height;
            if (marking.AutoHeight)
            {
                this.SizeToContent = SizeToContent.Height;
            }
            #endregion

            #region Controlls initialization
            _Border = new Border
            {
                BorderThickness = new Thickness(1),
                CornerRadius = marking.CornerRadius,
                BorderBrush = foreground,
                Background = background,
            };

            _HeaderBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = foreground,
                FontSize = 25
            };

            _DividingLine = new StackPanel
            {
                Background = foreground,
                Height = 1,
            };

            _TextBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = foreground,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap
            };

            _StackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            IAddChild container = _StackPanel;
            container.AddChild(_HeaderBlock);
            container.AddChild(_DividingLine);
            container.AddChild(_TextBlock);

            container = _Border;
            container.AddChild(_StackPanel);

            container = this;
            container.AddChild(_Border);
            #endregion            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _HeaderBlock.Text = Header;
            _TextBlock.Text = Text;
        }
    }
}