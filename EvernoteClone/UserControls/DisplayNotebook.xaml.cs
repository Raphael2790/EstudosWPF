﻿using EvernoteClone.Models;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.UserControls
{
    /// <summary>
    /// Interação lógica para DisplayNotebook.xam
    /// </summary>
    public partial class DisplayNotebook : UserControl
    {
        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(DisplayNotebook), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var displayNotebookControl = d as DisplayNotebook;

            if(displayNotebookControl is not null)
            {
                displayNotebookControl.DataContext = displayNotebookControl.Notebook;
            }
        }

        public DisplayNotebook()
        {
            InitializeComponent();
        }
    }
}
