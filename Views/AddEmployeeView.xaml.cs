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
using MySql.Data.MySqlClient;

namespace HRM_System.Views
{
    /// <summary>
    /// Interaction logic for AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        private int? _employeeId = null;  // Changed from Guid? to int?
        private bool _isEditMode = false;
        private string _connectionString = "Server=localhost;Database=hrmsystem;Uid=root;Pwd=DJdas12345;";

        // Default constructor for adding a new employee
        public AddEmployeeView()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        // Constructor for editing existing employee
        public AddEmployeeView(int employeeId)  // Changed from Guid to int
        {
            InitializeComponent();
            _employeeId = employeeId;
            _isEditMode = true;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT FirstName, LastName, Email, DateOfBirth, ContactNumber, RecruitementDate, JobRole FROM employees WHERE EmployeeId = @EmployeeId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@EmployeeId", MySqlDbType.Int32).Value = _employeeId;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the form fields with employee data
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            NameBox.Text = $"{firstName} {lastName}";
                            EmployeeIdBox.Text = _employeeId.ToString();
                            EmailBox.Text = reader["Email"].ToString();
                            
                            // Handle possible null values for the remaining fields
                            ContactBox.Text = reader["ContactNumber"] != DBNull.Value ? reader["ContactNumber"].ToString() : string.Empty;
                            
                            if (reader["DateOfBirth"] != DBNull.Value && DateTime.TryParse(reader["DateOfBirth"].ToString(), out DateTime dob))
                                DobPicker.SelectedDate = dob;
                            
                            // Job Role instead of JobPosition
                            PositionBox.Text = reader["JobRole"] != DBNull.Value ? reader["JobRole"].ToString() : string.Empty;
                            
                            // These fields might not exist in the table - handle appropriately
                            // AddressBox.Text = ""; // Omitted as it doesn't exist in the table
                            // DepartmentBox.Text = ""; // Omitted as it doesn't exist in the table
                            
                            if (reader["RecruitementDate"] != DBNull.Value && DateTime.TryParse(reader["RecruitementDate"].ToString(), out DateTime recruitDate))
                                RecruitmentDatePicker.SelectedDate = recruitDate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is MainView main)
            {
                main.ContentArea.Content = new EmployeeView();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isEditMode)
            {
                LoadEmployeeData(); // Reload original data
            }
            else
            {
                // Clear all fields
                NameBox.Text = string.Empty;
                EmployeeIdBox.Text = string.Empty;
                EmailBox.Text = string.Empty;
                ContactBox.Text = string.Empty;
                DobPicker.SelectedDate = null;
                AddressBox.Text = string.Empty;
                PositionBox.Text = string.Empty;
                DepartmentBox.Text = string.Empty;
                RecruitmentDatePicker.SelectedDate = null;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate form data
                if (string.IsNullOrWhiteSpace(NameBox.Text) || string.IsNullOrWhiteSpace(EmailBox.Text))
                {
                    MessageBox.Show("Name and Email are required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Split name into first and last name
                string[] nameParts = NameBox.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                string lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    string query;
                    MySqlCommand command;

                    if (_isEditMode)
                    {
                        // Update existing employee
                        query = @"UPDATE employees 
                                SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                    ContactNumber = @ContactNumber, DateOfBirth = @DateOfBirth, 
                                    JobRole = @JobRole, RecruitementDate = @RecruitementDate
                                WHERE EmployeeId = @EmployeeId";
                        
                        command = new MySqlCommand(query, connection);
                        command.Parameters.Add("@EmployeeId", MySqlDbType.Int32).Value = _employeeId;
                    }
                    else
                    {
                        // Add new employee with auto-increment id
                        query = @"INSERT INTO employees 
                                (FirstName, LastName, Email, ContactNumber, DateOfBirth, JobRole, RecruitementDate) 
                                VALUES 
                                (@FirstName, @LastName, @Email, @ContactNumber, @DateOfBirth, @JobRole, @RecruitementDate)";
                        
                        command = new MySqlCommand(query, connection);
                    }

                    // Add parameters for both insert and update
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", EmailBox.Text.Trim());
                    command.Parameters.AddWithValue("@ContactNumber", string.IsNullOrWhiteSpace(ContactBox.Text) ? DBNull.Value : (object)ContactBox.Text.Trim());
                    command.Parameters.AddWithValue("@DateOfBirth", DobPicker.SelectedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@JobRole", string.IsNullOrWhiteSpace(PositionBox.Text) ? DBNull.Value : (object)PositionBox.Text.Trim());
                    command.Parameters.AddWithValue("@RecruitementDate", RecruitmentDatePicker.SelectedDate ?? (object)DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show(_isEditMode ? "Employee updated successfully!" : "Employee added successfully!", 
                            "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        // Navigate back to employee list
                        var parentWindow = Window.GetWindow(this);
                        if (parentWindow is MainView main)
                        {
                            main.ContentArea.Content = new EmployeeView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
