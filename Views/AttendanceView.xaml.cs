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
    public partial class EmployeeAttendanceView : Window
    {
        public EmployeeAttendanceView()
        {
            InitializeComponent();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            GeneratedPdfLink.Text = "Generated Report: dummy.pdf";
        }

        private void MonthDropdown_DropDownOpened(object sender, System.EventArgs e)
        {
            if (MonthDropdown.Items.Count == 0)
            {
                string[] months = {
                    "January", "February", "March", "April", "May", "June",
                    "July", "August", "September", "October", "November", "December"
                };

                foreach (var month in months)
                {
                    MonthDropdown.Items.Add(month);
                }
            }
        }
    }
}
