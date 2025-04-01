using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRM_System.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        //Minimize Animation


        //Maximize Animation

        public async Task AnimateWindowOpacity(double from, double to, int durationMs)
        {
            var tcs = new TaskCompletionSource<bool>();
            var animation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(durationMs)));
            animation.Completed += (s, e) => tcs.SetResult(true);
            this.BeginAnimation(Window.OpacityProperty, animation);
            await tcs.Task;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();

        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private async void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            await AnimateWindowOpacity(1, 0, 20);

            // Toggle between Maximized and Normal states
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }

            await AnimateWindowOpacity(0, 1, 20);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
