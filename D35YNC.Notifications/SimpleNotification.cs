/************** 
 * File: D35YNC.Notifications/SimpleNotification.cs
 * Description: Pop-up notification library
 * D35YNC 2019 - 2020
 **************/



using D35YNC.Notifications.Settings;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace D35YNC.Notifications
{
    internal class SimpleNotification : Notification
    {
        public string Header;
        public string Text;

        #region Components
        private Border _Border;
        private TextBlock _HeaderBlock;
        private TextBlock _TextBlock;
        private StackPanel _StackPanel;
        private StackPanel _DividingLine;
        #endregion


        public SimpleNotification(string header, string text, WindowConfig marking, int timeout, int animDuration) : base(timeout, animDuration)
        {
            Header = header;
            Text = text;

            InitComponents(marking);
        }


        private void InitComponents(WindowConfig markup)
        {
            if (markup == null)
            {
                throw new Exception("Window cannot be initialized");
            }

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
            this.Width = markup.Width;
            this.Height = markup.Height;
            if (markup.AutoHeight)
            {
                this.SizeToContent = SizeToContent.Height;
            }
            #endregion

            #region Controlls initialization
            _Border = new Border
            {
                BorderThickness = new Thickness(1),
                CornerRadius = markup.CornerRadius,
                BorderBrush = markup.ForegroundColor,
                Background = markup.BackgroundColor,
            };

            _HeaderBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = markup.ForegroundColor,
                FontSize = 25
            };

            _DividingLine = new StackPanel
            {
                Background = markup.ForegroundColor,
                Height = 1,
            };

            _TextBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = markup.ForegroundColor,
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