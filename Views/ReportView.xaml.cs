using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRM_System.Views
{
    public partial class ReportView : Window
    {
        public ReportView()
        {
            InitializeComponent();

            EmailBox.LostFocus += EmailBox_LostFocus;
            AgeBox.PreviewTextInput += NumericTextBox_PreviewTextInput;
            ContactBox.PreviewTextInput += NumericTextBox_PreviewTextInput;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailView mainView = new EmployeeDetailView();
            mainView.Show();
            this.Close();
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text;
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Block non-digit characters
            e.Handled = !Regex.IsMatch(e.Text, @"^\d$");
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
            string recruitmentDate = RecruitmentDatePicker.SelectedDate?.ToShortDateString() ?? "";

            // Basic validation
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contact))
            {
                MessageBox.Show("Please fill out all required fields.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show(
                $"Employee Added:\n\nID: {id}\nName: {name}\nAge: {age}\nEmail: {email}\nContact: {contact}\nDepartment: {dept}\nPosition: {position}\nRecruited: {recruitmentDate}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            // TODO: Save to DB
        }
    }
}
