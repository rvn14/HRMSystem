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
    public partial class EmployeeDetailView : Window
    {
        public EmployeeDetailView()
        {
            InitializeComponent();
        }

        //back button navigation to the admin panel
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainView home = new MainView();
            home.Show();
            this.Close();
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add button clicked");
        }

        private void RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Remove button clicked");
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit button clicked");
        }
    }
}
