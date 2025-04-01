using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using HRM_System.Models;
using HRM_System.Repositories;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Windows;

namespace HRM_System.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Properties
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }

        // Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            // Make sure username and password are not null or too short
            return !(string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3);
        }


        private void ExecuteLoginCommand(object obj)
        {
            string connectionString = "Server=localhost;Database=voltexdb;Uid=root;Pwd=DJdas12345;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    // Convert SecureString to plain text (for demo purposes)
                    string plainPassword = ConvertToUnsecureString(Password);

                    // Use the correct table name; note backticks for reserved words in MySQL
                    string query = "SELECT * FROM `users` WHERE Username = @Username AND Password = @Password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", plainPassword);

                        bool validUser = command.ExecuteScalar() != null;
                        if (validUser)
                        {
                            // Set the current principal
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);

                            // Set the DialogResult to true to indicate successful login
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                var loginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this);
                                if (loginWindow != null)
                                {
                                    loginWindow.DialogResult = true;
                                }
                            });
                        }
                        else
                        {
                            ErrorMessage = "Invalid username or password";
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Failed to connect to database";
                }
            }
        }

        // Helper method to convert SecureString to plain string
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
