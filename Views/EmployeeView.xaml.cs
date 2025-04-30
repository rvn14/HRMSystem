using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient; // Changed from SqlClient
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
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        private ObservableCollection<EmployeeObject> _employees;
        private string _connectionString = "Server=localhost;Database=hrmsystem;Uid=root;Pwd=DJdas12345;";
        
        public EmployeeView()
        {
            InitializeComponent();
            LoadEmployees();
        }
        
        private void LoadEmployees()
        {
            try
            {
                _employees = new ObservableCollection<EmployeeObject>();
                
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeId, FirstName, LastName, Email, DateOfBirth, ContactNumber, RecruitementDate, JobRole, Department FROM employees";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _employees.Add(new EmployeeObject
                            {
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"]),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                RecruitementDate = reader.IsDBNull(reader.GetOrdinal("RecruitementDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["RecruitementDate"]),
                                JobRole = reader["JobRole"].ToString(),
                                Department = reader["Department"].ToString()
                            });
                        }
                    }
                }
                
                EmployeeDataGrid.ItemsSource = _employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                EmployeeObject selectedEmployee = (EmployeeObject)button.DataContext;
                
                if (selectedEmployee != null)
                {
                    // Navigate to edit page with selected employee
                    var parentWindow = Window.GetWindow(this);
                    if (parentWindow is MainView main)
                    {
                        // Create a new instance of AddEmployeeView with the employee ID
                        var addEmployeeView = new AddEmployeeView(selectedEmployee.EmployeeId);
                        
                        // Set the view in the content area
                        main.ContentArea.Content = addEmployeeView;
                    }
                    else
                    {
                        MessageBox.Show("Cannot navigate to edit employee view.", "Navigation Error", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an employee to edit.", "Selection Required", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when trying to edit employee: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            EmployeeObject selectedEmployee = (EmployeeObject)button.DataContext;
            
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee.FirstName} {selectedEmployee.LastName}?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM employees WHERE EmployeeId = @EmployeeId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.Add("@EmployeeId", MySqlDbType.Int32).Value = selectedEmployee.EmployeeId;
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            _employees.Remove(selectedEmployee);
                            MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterEmployees();
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            SearchFieldComboBox.SelectedIndex = 0;
            EmployeeDataGrid.ItemsSource = _employees;
        }

        private void FilterEmployees()
        {
            string searchText = SearchTextBox.Text.ToLower().Trim();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                EmployeeDataGrid.ItemsSource = _employees;
                return;
            }
            
            IEnumerable<EmployeeObject> filteredList;
            
            // Get the currently selected search field
            ComboBoxItem selectedItem = SearchFieldComboBox.SelectedItem as ComboBoxItem;
            string searchField = selectedItem?.Content.ToString() ?? "All Fields";
            
            switch (searchField)
            {
                case "ID":
                    filteredList = _employees.Where(emp => emp.EmployeeId.ToString().Contains(searchText));
                    break;
                    
                case "First Name":
                    filteredList = _employees.Where(emp => emp.FirstName.ToLower().Contains(searchText));
                    break;
                    
                case "Last Name":
                    filteredList = _employees.Where(emp => emp.LastName.ToLower().Contains(searchText));
                    break;
                    
                case "Email":
                    filteredList = _employees.Where(emp => emp.Email.ToLower().Contains(searchText));
                    break;
                    
                case "Job Role":
                    filteredList = _employees.Where(emp => emp.JobRole.ToLower().Contains(searchText));
                    break;
                    
                default: // "All Fields"
                    filteredList = _employees.Where(emp => 
                        emp.FirstName.ToLower().Contains(searchText) || 
                        emp.LastName.ToLower().Contains(searchText) || 
                        emp.Email.ToLower().Contains(searchText) ||
                        emp.ContactNumber.ToLower().Contains(searchText) ||
                        emp.JobRole.ToLower().Contains(searchText) ||
                        emp.EmployeeId.ToString().Contains(searchText));
                    break;
            }
            
            EmployeeDataGrid.ItemsSource = filteredList.ToList();
        }

        private void Click_Add_Employee(object sender, RoutedEventArgs e)
        {
            // Assume this is loaded inside a content control or frame
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is MainView main)
            {
                main.ContentArea.Content = new AddEmployeeView();
            }
        }
        
        private void Click_Back_Button(object sender, RoutedEventArgs e)
        {
            // Assume this is loaded inside a content control or frame
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is MainView main)
            {
                main.ContentArea.Content = new PayrollView();
            }
        }
    }

    public class EmployeeObject
    {
        public int EmployeeId { get; set; } // Changed from int to Guid
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RecruitementDate { get; set; }
        public string JobRole { get; set; }
        public string Email { get; set; }
        public string Department { get; set; } 
    }
}
