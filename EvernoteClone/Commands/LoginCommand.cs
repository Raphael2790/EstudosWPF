using EvernoteClone.Models;
using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class LoginCommand : ICommand
{
    private readonly LoginViewModel _loginViewModel;

    public event EventHandler? CanExecuteChanged 
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public LoginCommand(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        User user = parameter as User;

        if (user is null)
            return false;

        if (string.IsNullOrEmpty(user.Username) 
            || string.IsNullOrEmpty(user.Password))
            return false;

        return true;
    }

    public void Execute(object? parameter) 
        => _loginViewModel.Login();
}
