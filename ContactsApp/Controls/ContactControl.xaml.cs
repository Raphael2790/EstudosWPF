using ContactsApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ContactsApp
{
    /// <summary>
    /// Interação lógica para ContactControl.xam
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact { Email = "email@email.com", Name = "Name LastName", Phone = "119999999"}, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ContactControl;
            var contact = e.NewValue as Contact;

            if(contact is not null && control is not null)
            {
                control.nameTextBlock.Text = contact.Name;
                control.phoneTextBlock.Text = contact.Phone;
                control.emailTextBlock.Text = contact.Email;
                control.contactPhoto.Source = new BitmapImage(new Uri(contact.Image));
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
