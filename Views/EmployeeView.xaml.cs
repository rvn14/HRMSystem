using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;
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
using HRM_System.Models;

namespace HRM_System.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        private ObservableCollection<Employee> _employees;
        private string _connectionString = "Server=localhost;Database=voltexdb;Uid=root;";
        
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
                    string query = "SELECT * FROM employee";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _employees.Add(new Employee
                            {
                                EmployeeID = Convert.ToString(reader["EmployeeID"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["StartDate"]),
                                Position = reader["Position"].ToString(),
                                DepartmentName = reader["DepartmentName"].ToString(),
                                BankAccNo = reader["BankAccNo"].ToString(),
                                BasicSalary = Convert.ToSingle(reader["BasicSalary"]),
                                NIC = reader["NIC"].ToString(),
                                Username = reader["Username"].ToString(),
                                RemainingLeaves = Convert.ToInt32(reader["RemainingLeaves"])
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
                Employee selectedEmployee = (Employee)button.DataContext;
                
                if (selectedEmployee != null)
                {
                    // Navigate to edit page with selected employee
                    var parentWindow = Window.GetWindow(this);
                    if (parentWindow is MainView main)
                    {
                        // Create a new instance of AddEmployeeView with the employee ID
                        var addEmployeeView = new AddEmployeeView(selectedEmployee.EmployeeID);
                        
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
            Employee selectedEmployee = (Employee)button.DataContext;
            
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee.Name}?", 
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
                        command.Parameters.Add("@EmployeeId", MySqlDbType.Int32).Value = selectedEmployee.EmployeeID;
                        
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
            
            IEnumerable<Employee> filteredList;
            
            
            ComboBoxItem selectedItem = SearchFieldComboBox.SelectedItem as ComboBoxItem;
            string searchField = selectedItem?.Content.ToString() ?? "All Fields";
            
            switch (searchField)
            {
                case "ID":
                    filteredList = _employees.Where(emp => emp.EmployeeID.ToString().Contains(searchText));
                    break;
                    
                case "First Name":
                    filteredList = _employees.Where(emp => emp.Name.ToLower().Contains(searchText));
                    break;
                    
                case "Email":
                    filteredList = _employees.Where(emp => emp.Email.ToLower().Contains(searchText));
                    break;
                    
                case "Job Role":
                    filteredList = _employees.Where(emp => emp.Position.ToLower().Contains(searchText));
                    break;
                    
                default: // "All Fields"
                    filteredList = _employees.Where(emp => 
                        emp.Name.ToLower().Contains(searchText) || 
                        emp.Email.ToLower().Contains(searchText) ||
                        emp.Phone.ToLower().Contains(searchText) ||
                        emp.Position.ToLower().Contains(searchText) ||
                        emp.EmployeeID.ToString().Contains(searchText));
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
}


    public class Employee
    {
        public string EmployeeID { get; set; }
        public string Username { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position {  get; set; }
        public string BankAccNo { get; set; }
        public int RemainingLeaves {  get; set; }
        public DateTime StartDate { get; set; }
        public float BasicSalary { get; set; }
    }

