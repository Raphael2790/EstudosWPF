using EvernoteClone.Commands;
using EvernoteClone.Helpers;
using EvernoteClone.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EvernoteClone.ViewModels;

public class NotesViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Notebook> Notebooks { get; set; }

	private Notebook _selectedNotebook;

    public Notebook SelectedNotebook
	{
		get { return _selectedNotebook; }
		set 
		{ 
			_selectedNotebook = value;
			OnPropertyChanged(nameof(SelectedNotebook));
			GetNotes();
		}
	}

	private Note _seletedNote;

	public Note SelectedNote
	{
		get { return _seletedNote; }
		set 
		{
			_seletedNote = value;
			OnPropertyChanged(nameof(SelectedNote));
			SelectedNoteChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public ObservableCollection<Note> Notes { get; set; }

	private Visibility _isVisible;

	public Visibility IsVisible
	{
		get { return _isVisible; }
		set 
		{ 
			_isVisible = value;
			OnPropertyChanged(nameof(IsVisible));
		}
	}

	public NewNoteCommand NewNoteCommand { get; set; }
	public NewNotebookCommand NewNotebookCommand { get; set; }
	public EditCommand EditCommand { get; set; }
	public DeleteNotebookCommand DeleteNotebookCommand { get; set; }
	public DeleteNoteCommand DeleteNoteCommand { get; set; }
	public StopEditingCommand StopEditingCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler? SelectedNoteChanged;

	public NotesViewModel()
	{
        NewNoteCommand = new(this);
        NewNotebookCommand = new(this);
		EditCommand = new(this);
		DeleteNotebookCommand = new(this);
		DeleteNoteCommand = new(this);
		StopEditingCommand = new(this);

		Notebooks = new();
		Notes = new();

		GetNotebooks();
		IsVisible = Visibility.Collapsed;
    }

	public void CreateNote(string notebookId)
	{
		Note note = new() 
		{
			NotebookId = notebookId,
            Title = $"Note for {DateTime.Now}",
            CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
        };

		DatabaseHelper.Insert(note);
		GetNotes();
	}

	public void CreateNotebook()
	{
        Notebook notebook = new()
		{
			Name = "New Notebook",
			UserId = App.UserId
        };

        DatabaseHelper.Insert(notebook);
		GetNotebooks();
    }

	public void GetNotebooks()
	{
		var notebooks = DatabaseHelper.Read<Notebook>().Where(n => n.UserId == App.UserId).ToList();
		Notebooks.Clear();
		notebooks.ForEach(notebook => Notebooks.Add(notebook));
	}

	public void GetNotes()
	{
		if(SelectedNotebook is null)
			return;

        var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
        Notes.Clear();
        notes.ForEach(note => Notes.Add(note));
    }

	public void StartEditing()
	{
		IsVisible = Visibility.Visible;
	}

	public void StopEditing(Notebook notebook)
	{
        IsVisible = Visibility.Collapsed;
		DatabaseHelper.Update(notebook);
		GetNotebooks();
    }

	private void OnPropertyChanged(string propertyName)
	{
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void DeleteNotebook(Notebook notebook)
    {
        DatabaseHelper.Delete(notebook);
		GetNotebooks();
    }

    public void DeleteNote(Note note)
    {
        DatabaseHelper.Delete(note);
		GetNotes();
    }
}
