using EvernoteClone.ViewModels;
using System;
using System.Windows;

namespace EvernoteClone.Views
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel? _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            _viewModel = Resources["vm"] as LoginViewModel;
            _viewModel!.Authenticated += ViewModel_Authenticated;
        }

        //A view se inscreve no evento Authenticated do ViewModel
        private void ViewModel_Authenticated(object? sender, EventArgs e) 
            => Close();
    }
}
