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
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        public AddEmployeeView()
        {
            InitializeComponent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = "";
            EmployeeIdBox.Text = "";
            EmailBox.Text = "";
            ContactBox.Text = "";
            DobPicker.SelectedDate = null;
            AddressBox.Text = "";
            PositionBox.Text = "";
            DepartmentBox.Text = "";
            RecruitmentDatePicker.SelectedDate = null;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Assume this is loaded inside a content control or frame
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is MainView main)
            {
                main.ContentArea.Content = new EmployeeView();
            }
        }
    }
}
