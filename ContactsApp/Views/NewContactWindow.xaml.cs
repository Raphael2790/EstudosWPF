using ContactsApp.Models;
using Microsoft.Win32;
using SQLite;
using System;
using System.Windows;

namespace ContactsApp.Views
{
    /// <summary>
    /// Lógica interna para NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        private string _contactPhotoPath = null!;

        public NewContactWindow()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact 
            {
                Email = emailTextBox.Text,
                Name = nameTextBox.Text,
                Phone = phoneTextBox.Text,
                Image = _contactPhotoPath
            };

            

            using var connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Contact>();
            connection.Insert(contact);

            Close();
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png; *.jpg) | *.png;*.jpeg;*.jpg | All files (*.*) | *.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if ((bool)openFileDialog.ShowDialog()!)
            {
                _contactPhotoPath = openFileDialog.FileName;
            }
        }
    }
}
