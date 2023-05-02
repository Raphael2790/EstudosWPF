using EvernoteClone.Models;
using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class DeleteNotebookCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public DeleteNotebookCommand(NotesViewModel notesViewModel) 
        => _notesViewModel = notesViewModel;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) 
        => true;

    public void Execute(object? parameter)
    {
        Notebook notebook = parameter as Notebook;

        if (notebook is not null)
        {
            _notesViewModel.DeleteNotebook(notebook);
        }
    }
}
