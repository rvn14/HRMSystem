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

namespace HRM_System.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard.xaml
    /// </summary>
    public partial class EmployeeDashboard : Window
    {
        public EmployeeDashboard()
        {
            InitializeComponent();
        }
        private void AccountView_Click(object sender, RoutedEventArgs e)
        {
            var accountWindow = new AccountDetailsView();
            accountWindow.Show();
            this.Close(); 
        }

        private void AttendanceSheet_Click(object sender, RoutedEventArgs e)
        {
            var attendanceWindow = new EmployeeAttendanceView();
            attendanceWindow.Show();
            this.Close(); 
        }

       
    }
}
