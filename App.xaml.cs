using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HRM_System.View;
using HRM_System.Views;
using HRM_System.ViewModels;

namespace HRM_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Prevent auto shutdown when login dialog closes.
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Create and show the login view as a dialog.
            var loginView = new LoginView();
            bool? result = loginView.ShowDialog();

            // Check if login was successful.
            if (result == true)
            {
                // Get the admin status from the view model
                bool isAdmin = ((LoginViewModel)loginView.DataContext).IsAdmin;

                // Create the appropriate window based on the user's role
                if (isAdmin)
                {
                    // Create the main window for administrators
                    var mainWindow = new MainView();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                }
                else
                {
                    // Create the user window for regular users
                    var userWindow = new UserView();
                    this.MainWindow = userWindow;
                    userWindow.Show();
                }

                // Now that we have a main window, switch back to shutdown on main window close.
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            else
            {
                // If login failed or was cancelled, shut down the application.
                this.Shutdown();
            }
        }
    }
}
