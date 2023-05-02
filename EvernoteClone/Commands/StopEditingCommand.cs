using EvernoteClone.Models;
using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class StopEditingCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public event EventHandler? CanExecuteChanged;
    public StopEditingCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public bool CanExecute(object? parameter) 
        => true;

    public void Execute(object? parameter)
    {
        var notebook = parameter as Notebook;
        if(notebook is not null)
            _notesViewModel.StopEditing(notebook);
    }
}