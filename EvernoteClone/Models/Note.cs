using SQLite;
using System;

namespace EvernoteClone.Models;

public class Note : IHasId
{
    [PrimaryKey, AutoIncrement]
    public string Id { get; set; }
    [Indexed]
    public string NotebookId { get; set; }
    [MaxLength(50)]
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string FileLocation { get; set; }
}
