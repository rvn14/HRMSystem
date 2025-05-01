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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRM_System.Views
{
    /// <summary>
    /// Interaction logic for UserAttendanceView.xaml
    /// </summary>
    public partial class UserAttendanceView : UserControl
    {
        public UserAttendanceView()
        {
            InitializeComponent();
        }
        
        private void requestLeaveButton_Click(object sender, RoutedEventArgs e)
        {
            OpenLeaveRequestView();
        }
        
        private void OpenLeaveRequestView()
        {
            // Create an instance of LeaveRequestView
            var leaveRequestView = new LeaveRequestView();
            
            // Find the parent window to show the view
            var parentWindow = Window.GetWindow(this);
            
            if (parentWindow != null)
            {
                // Option 1: If LeaveRequestView is meant to be shown in a new window
                var leaveRequestWindow = new Window
                {
                    Title = "Leave Request",
                    Content = leaveRequestView,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = parentWindow
                };
                
                leaveRequestWindow.ShowDialog();
                
                // Option 2: If you have a content frame in the parent window
                // Uncomment and adjust this code if needed
                // var contentFrame = parentWindow.FindName("ContentFrame") as Frame;
                // if (contentFrame != null)
                // {
                //     contentFrame.Navigate(leaveRequestView);
                // }
            }
        }
    }
}
