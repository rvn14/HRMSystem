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
    public partial class AddEmployeeView : Window
    {
        public AddEmployeeView()
        {
            InitializeComponent();
        }

        //back button navigation to the admin panel
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailView home = new EmployeeDetailView();
            home.Show();
            this.Close();
        }

        private void SubmitEmployee_Click(object sender, RoutedEventArgs e)
        {
            string id = EmployeeIdBox.Text;
            string name = NameBox.Text;
            string age = AgeBox.Text;
            string email = EmailBox.Text;
            string contact = ContactBox.Text;
            string dept = DepartmentBox.Text;
            string position = PositionBox.Text;
            string recruitmentDate = RecruitmentDatePicker.SelectedDate?.ToShortDateString() ?? "Not selected";

            MessageBox.Show(
                $"Employee Added:\n\nID: {id}\nName: {name}\nAge: {age}\nEmail: {email}\nContact: {contact}\nDepartment: {dept}\nPosition: {position}\nRecruited: {recruitmentDate}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            
        }
    }
}
