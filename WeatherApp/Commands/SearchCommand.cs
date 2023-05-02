using System;
using System.Windows.Input;
using WeatherApp.ViewModel;

namespace WeatherApp.Commands;

public class SearchCommand : ICommand
{
    private readonly WeatherViewModel _weatherViewModel;

    public SearchCommand(WeatherViewModel weatherViewModel)
    {
        _weatherViewModel = weatherViewModel;
    }

    public event EventHandler? CanExecuteChanged 
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        var query = parameter as string;

        if(string.IsNullOrWhiteSpace(query))
            return false;

        return true;
    }

    public void Execute(object? parameter)
    {
        _weatherViewModel.MakeQuery();
    }
}
