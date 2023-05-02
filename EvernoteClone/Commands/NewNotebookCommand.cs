using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class NewNotebookCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public event EventHandler? CanExecuteChanged;

    public NewNotebookCommand(NotesViewModel notesViewModel) 
        => _notesViewModel = notesViewModel;

    public bool CanExecute(object? parameter) 
        => true;

    public void Execute(object? parameter) 
        => _notesViewModel.CreateNotebook();
}
