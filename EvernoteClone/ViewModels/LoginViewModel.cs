using EvernoteClone.Commands;
using EvernoteClone.Helpers;
using EvernoteClone.Models;
using System;
using System.ComponentModel;
using System.Windows;

namespace EvernoteClone.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
	private bool isRegistering = false;

	private User _user;
	public User User
	{
		get { return _user; }
		set 
		{ 
			_user = value;
			OnPropertyChanged(nameof(User));
		}
	}

	private string _username;
	public string Username
	{
        get { return _username; }
        set
		{
            _username = value;
			User = new User
			{
				Username = _username,
				Password = Password,
				ConfirmPassword = ConfirmPassword,
				Name = Name,
				LastName = LastName
			};
            OnPropertyChanged(nameof(Username));
        }
    }

	private string _password;
	public string Password
	{
        get { return _password; }
        set
		{
            _password = value;
			User = new User
			{
				Username = Username,
				Password = _password,
				ConfirmPassword = ConfirmPassword,
				Name = Name,
				LastName = LastName
			};
            OnPropertyChanged(nameof(Password));
        }
    }

	private string _confirmPassword;
	public string ConfirmPassword
	{
        get { return _confirmPassword; }
        set
		{
            _confirmPassword = value;
			User = new User
			{
				Username = Username,
				Password = Password,
				ConfirmPassword = _confirmPassword,
				Name = Name,
				LastName = LastName
			};
            OnPropertyChanged(nameof(ConfirmPassword));
        }
    }

	private string _name;
	public string Name
	{
        get { return _name; }
        set
		{
            _name = value;
			User = new User
			{
				Username = Username,
				Password = Password,
				ConfirmPassword = ConfirmPassword,
				Name = _name,
				LastName = LastName
			};
            OnPropertyChanged(nameof(Name));
        }
    }

	private string _lastName;
	public string LastName
	{
        get { return _lastName; }
        set
		{
            _lastName = value;
			User = new User
			{
				Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                Name = Name,
                LastName = _lastName
			};
            OnPropertyChanged(nameof(LastName));
        }
    }

	private Visibility _registerVis;
	public Visibility RegisterVis
	{
		get { return _registerVis; }
		set 
		{
			_registerVis = value;
			OnPropertyChanged(nameof(RegisterVis));
		}
	}

    private Visibility _loginVis;
    public Visibility LoginVis
    {
        get { return _loginVis; }
        set 
		{ 
			_loginVis = value;
			OnPropertyChanged(nameof(LoginVis));
		}
    }

    public RegisterCommand RegisterCommand { get; set; }
	public LoginCommand LoginCommand { get; set; }
	public ShowRegisterCommand ShowRegisterCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler? Authenticated;

    public LoginViewModel()
    {
		LoginVis = Visibility.Visible;
		RegisterVis = Visibility.Collapsed;

		RegisterCommand = new(this);
		LoginCommand = new(this);
		ShowRegisterCommand = new(this);

		User = new();
    }

	public void SwitchViews()
	{
		isRegistering = !isRegistering;

		if(isRegistering) 
		{
			LoginVis = Visibility.Collapsed;
            RegisterVis = Visibility.Visible;
		}
		else
		{
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;
		}
	}

	private void OnPropertyChanged(string propertyName)
	{
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

	public async void Register()
	{
		var result = await FirebaseAuthHelper.Register(User);

		if(result)
			Authenticated?.Invoke(this, new EventArgs());
	}

	public async void Login()
	{
		var result = await FirebaseAuthHelper.Login(User);

		if (result)
			Authenticated?.Invoke(this, new EventArgs());
    }
}
