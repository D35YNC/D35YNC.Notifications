using D35YNC.Notifications;
using D35YNC.Notifications.Enums;
using D35YNC.Notifications.Settings;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Markup;


namespace Demo
{
    public partial class MainWindow : Window
    {
        NotificationsController NotifyController;

        public MainWindow()
        {
            InitializeComponent();
            
            NotifyController = new NotificationsController(true);
            
            NotifyController.Config.ForegroundColor = new SolidColorBrush(Colors.Lime);
            NotifyController.Config.BackgroundColor = new SolidColorBrush(Colors.Black);
            
            Behavior.ShowAnimationType = AnimationType.Slide;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotifyController.ShowNotification("First called notification", "Has default Timeout (3000ms) and AnimationDuration (300ms)");

            NotifyController.ShowNotification(new ExampleNotification(5000, 400)
            {
                Header = "Second called notification",
                Text = "Has Timeout = 5000 and AnimationTimeout = 400"
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NotifyController.CloseAll();
        }
    }

    class ExampleNotification : Notification
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
        
        
        public ExampleNotification(int timeout, int animDuration) : base(timeout, animDuration)
        {
            InitComponents();
        }
        
        
        private void InitComponents()
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
            this.Width = 300;
            this.Height = 150;
            /*
            if (marking.AutoHeight)
            {
                this.SizeToContent = SizeToContent.Height;
            }
             */
            #endregion

            #region Controlls initialization
            _Border = new Border
            {
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                BorderBrush = new SolidColorBrush(Colors.Lime),
                Background = new SolidColorBrush(Color.FromArgb(255, 15, 15, 15)),
            };

            _HeaderBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = new SolidColorBrush(Colors.Lime),
                FontSize = 25
            };

            _DividingLine = new StackPanel
            {
                Background = new SolidColorBrush(Colors.Lime),
                Height = 1,
            };

            _TextBlock = new TextBlock
            {
                Margin = new Thickness(10),
                Foreground = new SolidColorBrush(Colors.Lime),
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