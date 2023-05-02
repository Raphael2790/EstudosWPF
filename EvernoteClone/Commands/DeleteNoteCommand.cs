using EvernoteClone.Models;
using EvernoteClone.ViewModels;
using System;
using System.Windows.Input;

namespace EvernoteClone.Commands;

public class DeleteNoteCommand : ICommand
{
    private readonly NotesViewModel _notesViewModel;

    public event EventHandler? CanExecuteChanged;

    public DeleteNoteCommand(NotesViewModel notesViewModel) 
        => _notesViewModel = notesViewModel;

    public bool CanExecute(object? parameter)
        => true;

    public void Execute(object? parameter)
    {
        Note note = parameter as Note;

        if (note is not null)
        {
            _notesViewModel.DeleteNote(note);
        }
    }
}