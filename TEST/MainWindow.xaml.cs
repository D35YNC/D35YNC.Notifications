using System.Windows;
using System.Windows.Media;
using D35YNC.Notifications;

namespace TEST
{
    public enum NotifyType
    {
        // Your types here..

        Default,
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

            NotifyController.Config.Behavior.ShowAnimation = NotifyAnimation.Slide;

            NotifyController.AddNotifyType((int)NotifyType.Default, "First called notify", 2500);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotifyController.ShowNotify((int)NotifyType.Default, "Has Timeout = 2500 and default AnimationTimeout");

            NotifyController.ShowNotify(new NotifyWindow(NotifyController.Config.Style, NotifyController.Config.Behavior)
            {
                Header = "Second called notify",
                Text = "Has Timeout = 5000 and AnimationTimeout = 400",
                Timeout = 5000,
                AnimationTimeout = 400
            });           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NotifyController.CloseAll();
        }
    }
}