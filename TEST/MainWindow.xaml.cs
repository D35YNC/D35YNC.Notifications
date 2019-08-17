using System.Windows;
using System.Windows.Media;
using D35YNC.Notifications;

namespace TEST
{
    public enum NotifyType
    {
        // Your types here..

        Default,
        Error
    }
    public partial class MainWindow : Window
    {
        NotifyController NotifyController;

        public MainWindow()
        {
            InitializeComponent();

            NotifyController = new NotifyController();

            NotifyController.Config.Style.Foreground = new SolidColorBrush(Colors.Lime);
            NotifyController.Config.Style.Background = new SolidColorBrush(Colors.Black);
            NotifyController.Config.Style.AutoHeight = true;
            NotifyController.Config.Behavior.HideAnimation = NotifyAnimation.Transparent;
            NotifyController.Config.Behavior.Position = NotifyPosition.BottomLeft;
            NotifyController.AddNotifyType((int)NotifyType.Default, "Обычный заголовок", 2500);
            NotifyController.AddNotifyType((int)NotifyType.Error, "Заголовок ошибки", 4000);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotifyController.ShowNotify(new NotifyWindow(NotifyController.Config.Style, NotifyController.Config.Behavior)
            {
                Header = "Hi!",
                Text = "I can create a notification like this",
                Timeout = 5000,
                AnimationTimeout = 400
            });

            NotifyController.ShowNotify((int)NotifyType.Default, "And like this");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NotifyController.CloseAll();
        }
    }
}