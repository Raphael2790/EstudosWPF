using SQLite;

namespace EvernoteClone.Models;

public class Notebook : IHasId
{
    [PrimaryKey, AutoIncrement]
    public string Id { get; set; }
    [Indexed]
    public string UserId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
}
