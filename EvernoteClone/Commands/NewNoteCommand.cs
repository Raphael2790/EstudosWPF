using EvernoteClone.Models;
using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class NewNoteCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public NewNoteCommand(NotesViewModel notesViewModel) 
        => _notesViewModel = notesViewModel;

    public bool CanExecute(object? parameter)
    {
        Notebook? notebook = parameter as Notebook;
        return notebook is not null;
    }

    public void Execute(object? parameter)
    {
        Notebook? notebook = parameter as Notebook;

        if (notebook is not null)
            _notesViewModel.CreateNote(notebook.Id);
    }
}
