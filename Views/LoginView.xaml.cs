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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using HRM_System.ViewModels;

namespace HRM_System.View
{
    /// <summary>
    /// Interaction logic for loginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private DispatcherTimer clockTimer;
        public LoginView()
        {
            InitializeComponent();
            StartClock();
            
        }

        

        private void StartClock()
        {
            // Create a timer that ticks every second.
            clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Update the clock TextBlock with current time.
            ClockTextBlock.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();

        }

        private void MainBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is Border border)
            {
                border.Clip = new RectangleGeometry()
                {
                    Rect = new Rect(0, 0, border.ActualWidth, border.ActualHeight),
                    RadiusX = border.CornerRadius.TopLeft,
                    RadiusY = border.CornerRadius.TopLeft
                };
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NoSpace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is the Space key
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
