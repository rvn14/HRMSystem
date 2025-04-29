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
        private ObservableCollection<Employee> _employees;
        private string _connectionString = "Server=localhost;Database=voltexdb;Uid=root;Pwd=DJdas12345;";
        
        public EmployeeView()
        {
            InitializeComponent();
            LoadEmployees();
        }
        
        private void LoadEmployees()
        {
            try
            {
                _employees = new ObservableCollection<Employee>();
                
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeId, FirstName, LastName, Email, DateOfBirth, ContactNumber, RecruitementDate, JobRole FROM users";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _employees.Add(new Employee
                            {
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"]),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                RecruitementDate = reader.IsDBNull(reader.GetOrdinal("RecruitementDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["RecruitementDate"]),
                                JobRole = reader["JobRole"].ToString()
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
            Button button = (Button)sender;
            Employee selectedEmployee = (Employee)button.DataContext;
            
            // Navigate to edit page with selected employee
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is MainView main)
            {
                main.ContentArea.Content = new AddEmployeeView(selectedEmployee.EmployeeId);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Employee selectedEmployee = (Employee)button.DataContext;
            
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee.FirstName} {selectedEmployee.LastName}?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM users WHERE EmployeeId = @EmployeeId";
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
            string searchText = SearchTextBox.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                EmployeeDataGrid.ItemsSource = _employees;
                return;
            }
            
            var filteredList = _employees.Where(emp => 
                emp.FirstName.ToLower().Contains(searchText) || 
                emp.LastName.ToLower().Contains(searchText) || 
                emp.Email.ToLower().Contains(searchText) ||
                emp.EmployeeId.ToString().Contains(searchText)).ToList();
                
            EmployeeDataGrid.ItemsSource = filteredList;
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

    public class Employee
    {
        public int EmployeeId { get; set; } // Changed from int to Guid
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RecruitementDate { get; set; }
        public string JobRole { get; set; }
        public string Email { get; set; }
    }
}
