using ContactsApp.Models;
using ContactsApp.Views;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contact> _contacts;
        public MainWindow()
        {
            InitializeComponent();
            _contacts = new List<Contact>();
            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var contactForm = new NewContactWindow();
            contactForm.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using var connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Contact>();
            _contacts = connection.Table<Contact>().OrderBy(x => x.Name).ToList();

            contactsListView.ItemsSource = _contacts;
        }

        private void OnFilterName_Change(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;

            if(textBox is not null)
            {
                var filteredlist = _contacts.Where(c => c.Name.ToLower().ToLower().Contains(textBox.Text.ToLower())).ToList();
                contactsListView.ItemsSource = filteredlist;
            }
        }

        private void OnSelectItem_Event(object sender, SelectionChangedEventArgs e)
        {
            Contact? selectedContact = (sender as ListView)?.SelectedItem as Contact;

            if(selectedContact is not null)
            {
                var contactDetailsDialog = new ContactDetailsWindow(selectedContact);
                contactDetailsDialog.ShowDialog();

                ReadDatabase();
            }
        }
    }
}
