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
            
            // Load HomeView by default when application starts
            HomeButton_Click(null, null);
        }

        // Navigation Methods
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Update navigation button styles
            UpdateNavButtonStyles(HomeButton);
            
            // Create and display HomeView
            ContentArea.Content = new HomeView();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            // Update navigation button styles
            UpdateNavButtonStyles(EmployeeButton);

            // Create and display ReportView
            ContentArea.Content = new EmployeeView();
        }

        private void AttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            // Update navigation button styles
            UpdateNavButtonStyles(AttendanceButton);

            // Create and display ReportView
            ContentArea.Content = new AttendanceView();
        }

        private void PayrollButton_Click(object sender, RoutedEventArgs e)
        {
            // Update navigation button styles
            UpdateNavButtonStyles(PayrollButton);
            // Create and display ReportView
            ContentArea.Content = new PayrollView();
        }
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Update navigation button styles
            UpdateNavButtonStyles(ReportButton);
            
            // Create and display ReportView
            ContentArea.Content = new ReportView();
        }
        
        // Helper method to update navigation button styles
        private void UpdateNavButtonStyles(Button activeButton)
        {
            // Reset all buttons to default style
            foreach (var button in new[] { HomeButton, ReportButton, EmployeeButton, AttendanceButton, PayrollButton })
            {
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                
                // Find the StackPanel inside the button
                if (button.Content is StackPanel sp)
                {
                    // Reset text and icon color
                    foreach (var child in sp.Children)
                    {
                        if (child is TextBlock tb)
                            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
                    }
                }
            }
            
            // Highlight active button
            if (activeButton != null)
            {
                activeButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEAF8"));
                activeButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D2D9C"));
                activeButton.BorderThickness = new Thickness(0, 0, 0, 0);
                
                
                // Update text and icon color in the active button
                if (activeButton.Content is StackPanel sp)
                {
                    foreach (var child in sp.Children)
                    {
                        if (child is TextBlock tb)
                            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D2D9C"));
                    }
                }
            }
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

        private void Border_DragOver(object sender, DragEventArgs e)
        {

        }
    }
}
