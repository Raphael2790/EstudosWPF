using EvernoteClone.Models;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.UserControls
{
    /// <summary>
    /// Interação lógica para DisplayNote.xam
    /// </summary>
    public partial class DisplayNote : UserControl
    {
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(DisplayNote), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var displayNoteControl = d as DisplayNote;
            if(displayNoteControl is not null)
            {
                displayNoteControl.DataContext = displayNoteControl.Note;
            }
        }

        public DisplayNote()
        {
            InitializeComponent();
        }
    }
}
