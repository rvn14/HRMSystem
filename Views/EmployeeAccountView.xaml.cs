using System.Windows;

namespace HRM_System.Views
{
    public partial class AccountDetailsView : Window
    {
        private string originalName;
        private string originalContact;

        public AccountDetailsView()
        {
            InitializeComponent();

            // Store original values at load
            originalName = NameBox.Text;
            originalContact = ContactBox.Text;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string updatedName = NameBox.Text;
            string updatedContact = ContactBox.Text;

            MessageBox.Show($"Changes saved!\n\nName: {updatedName}\nContact: {updatedContact}", "Success");

            // Update stored values after saving
            originalName = updatedName;
            originalContact = updatedContact;
        }

        private void ResetChanges_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = originalName;
            ContactBox.Text = originalContact;

            MessageBox.Show("Inputs have been reset.", "Reset");
        }
    }
}
