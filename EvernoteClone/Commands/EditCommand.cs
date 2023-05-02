using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class EditCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public event EventHandler? CanExecuteChanged;

    public EditCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public bool CanExecute(object? parameter) 
        => true;

    public void Execute(object? parameter) 
        => _notesViewModel.StartEditing();
}
