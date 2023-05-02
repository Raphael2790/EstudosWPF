using ContactsApp.Models;
using SQLite;
using System.Windows;

namespace ContactsApp.Views
{
    /// <summary>
    /// Lógica interna para ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        private Contact _contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            _contact = contact;
            phoneTextBox.Text = _contact.Phone;
            nameTextBox.Text = _contact.Name;
            emailTextBox.Text = _contact.Email;
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _contact.Phone = phoneTextBox.Text;
            _contact.Name = nameTextBox.Text;   
            _contact.Email = emailTextBox.Text;

            using var connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Contact>();
            connection.Update(_contact);

            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using var connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Contact>();
            connection.Delete(_contact);

            Close();
        }
    }
}
