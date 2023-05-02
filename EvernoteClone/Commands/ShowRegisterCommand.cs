using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;

        public event EventHandler? CanExecuteChanged;

        public ShowRegisterCommand(LoginViewModel loginViewModel) 
            => _loginViewModel = loginViewModel;

        public bool CanExecute(object? parameter) 
            => true;

        public void Execute(object? parameter) 
            => _loginViewModel.SwitchViews();
    }
}
