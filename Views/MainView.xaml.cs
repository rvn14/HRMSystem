using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HRM_System.Views
{
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
            UpdateNavButtonStyles(HomeButton);
            ContentArea.Content = new HomeView();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNavButtonStyles(EmployeeButton);
            ContentArea.Content = new EmployeeDetailView();
        }

        private void AttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNavButtonStyles(AttendanceButton);
            ContentArea.Content = new AttendanceView();
        }

        private void PayrollButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNavButtonStyles(PayrollButton);
            ContentArea.Content = new PayrollView();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNavButtonStyles(ReportButton);
            ContentArea.Content = new ReportView();
        }

        // Helper method to update navigation button styles
        private void UpdateNavButtonStyles(Button activeButton)
        {
            foreach (var button in new[] { HomeButton, ReportButton, EmployeeButton, AttendanceButton, PayrollButton })
            {
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;

                if (button.Content is StackPanel sp)
                {
                    foreach (var child in sp.Children)
                    {
                        if (child is TextBlock tb)
                            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
                    }
                }
            }

            if (activeButton != null)
            {
                activeButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEAF8"));
                activeButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4D2D9C"));
                activeButton.BorderThickness = new Thickness(0);

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

        // Minimize and Maximize Animation
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

            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

            await AnimateWindowOpacity(0, 1, 20);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            // Drag effect logic placeholder
        }

        // Injecting dummy email list view into ContentArea ######################################################
        public void LoadEmailMessages()
        {
            var stack = new StackPanel();
            string[,] dummyEmails =
            {
                { "noreply@gmail.com", "Login Alert", "We noticed a new login", "Today 9:30 AM" },
                { "newsletter@outlook.com", "Weekly Digest", "Here's what you missed...", "Yesterday" },
                { "github@code.com", "New Pull Request", "Check the latest updates", "2 days ago" }
            };

            for (int i = 0; i < dummyEmails.GetLength(0); i++)
            {
                var emailCard = new Border
                {
                    Margin = new Thickness(0, 0, 0, 10),
                    Padding = new Thickness(10),
                    Background = Brushes.White,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(6)
                };

                var cardContent = new StackPanel();
                cardContent.Children.Add(new TextBlock { Text = $"Subject: {dummyEmails[i, 1]}", FontWeight = FontWeights.Bold });
                cardContent.Children.Add(new TextBlock { Text = $"From: {dummyEmails[i, 0]}", Foreground = Brushes.Gray });
                cardContent.Children.Add(new TextBlock { Text = dummyEmails[i, 2] });
                cardContent.Children.Add(new TextBlock { Text = dummyEmails[i, 3], HorizontalAlignment = HorizontalAlignment.Right });

                emailCard.Child = cardContent;
                stack.Children.Add(emailCard);
            }

            ContentArea.Content = new ScrollViewer { Content = stack, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        }
    }
}
