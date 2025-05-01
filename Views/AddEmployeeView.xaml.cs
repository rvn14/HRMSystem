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
        private string _employeeId = null;  // Changed from Guid? to int?
        private bool _isEditMode = false;
        private string _connectionString = "Server=localhost;Database=voltexdb;Uid=root;";

        // Default constructor for adding a new employee
        public AddEmployeeView()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        // Constructor for editing existing employee
        public AddEmployeeView(string employeeId)  // Changed from Guid to int
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
                    string query = "SELECT * FROM employee WHERE EmployeeID = @EmployeeID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@EmployeeID", MySqlDbType.VarString).Value = _employeeId;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the form fields with employee data
                            EmployeeIdBox.Text = _employeeId.ToString();
                            Username.Text = reader["Username"].ToString();

                            // Add Department field population
                            DepartmentBox.Text = reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"].ToString() : string.Empty;

                            string name = reader["Name"].ToString();
                            NameBox.Text = name;
                            NICBox.Text = reader["NIC"].ToString();

                            // Handle possible null values for the remaining fields
                            ContactBox.Text = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : string.Empty;
                            
                            EmailBox.Text = reader["Email"].ToString();
                            // Job Role instead of JobPosition
                            PositionBox.Text = reader["Position"] != DBNull.Value ? reader["Position"].ToString() : string.Empty;

                            BankAcc.Text = reader["BankAccNo"].ToString();

                            RemainingLeaves.Text = reader["RemainingLeaves"].ToString();

                            
                            if (reader["StartDate"] != DBNull.Value && DateTime.TryParse(reader["StartDate"].ToString(), out DateTime recruitDate))
                                RecruitmentDatePicker.SelectedDate = recruitDate;

                            if (reader["BasicSalary"] != DBNull.Value && float.TryParse(reader["BasicSalary"].ToString(), out float basic))
                                BasicSalary.Text = basic.ToString();

                            Password.Text = "***********";
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
                EmployeeIdBox.Text = string.Empty;
                Username.Text = string.Empty;
                Password.Text = string.Empty;
                DepartmentBox.Text = string.Empty;
                NameBox.Text = string.Empty;
                NICBox.Text = string.Empty;
                ContactBox.Text = string.Empty;
                EmailBox.Text = string.Empty;
                PositionBox.Text = string.Empty;
                BankAcc.Text = string.Empty;
                RemainingLeaves.Text = string.Empty;
                RecruitmentDatePicker.SelectedDate = null;
                BasicSalary.Text = string.Empty;
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
                        query = @"UPDATE employee
                                SET EmployeeID = @EmployeeID, Username = @Username, DepartmentName = @DepartmentName, Name = @Name, 
                                NIC = @NIC, Phone = @Phone, Email = @Email, Position = @Position,
                                BankAccNo = @BankAccNo, RemainingLeaves = @RemainingLeaves, StartDate = @StartDate, BasicSalary = @BasicSalary
                                WHERE EmployeeID = @EmployeeID";
                        
                        command = new MySqlCommand(query, connection);
                        command.Parameters.Add("@EmployeeID", MySqlDbType.Int32).Value = _employeeId;
                    }
                    else
                    {
                        // Add new employee with auto-increment id
                        query = @"
                                INSERT INTO users (Username, PasswordHash, Role) VALUES (@Username, @Password, @Role);

                                INSERT INTO employee 
                                (EmployeeID, Username, DepartmentName, Name, NIC, Phone, Email, Position, BankAccNo, RemainingLeaves, StartDate, BasicSalary) 
                                VALUES 
                                (@EmployeeID, @Username, @DepartmentName, @Name, @NIC, @Phone, @Email, @Position, @BankAccNo, @RemainingLeaves, @StartDate, @BasicSalary)";

                        command = new MySqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Password", Password.Text.Trim());
                        command.Parameters.AddWithValue("@Role", "employee");
                    }

                    // Add parameters for both insert and update
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeIdBox.Text.Trim());
                    command.Parameters.AddWithValue("@Username", Username.Text.Trim());
                    command.Parameters.AddWithValue("@DepartmentName", string.IsNullOrWhiteSpace(DepartmentBox.Text) ? DBNull.Value : (object)DepartmentBox.Text.Trim());
                    command.Parameters.AddWithValue("@Name", $"{firstName} {lastName}");
                    command.Parameters.AddWithValue("@NIC", NICBox.Text.Trim());
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(ContactBox.Text) ? DBNull.Value : (object)ContactBox.Text.Trim());
                    command.Parameters.AddWithValue("@Email", EmailBox.Text.Trim());
                    command.Parameters.AddWithValue("@Position", string.IsNullOrWhiteSpace(PositionBox.Text) ? DBNull.Value : (object)PositionBox.Text.Trim());
                    command.Parameters.AddWithValue("@BankAccNo", BankAcc.Text.Trim());
                    command.Parameters.AddWithValue("@RemainingLeaves", RemainingLeaves.Text.Trim());
                    command.Parameters.AddWithValue("@StartDate", RecruitmentDatePicker.SelectedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BasicSalary", BasicSalary.Text.Trim());

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
