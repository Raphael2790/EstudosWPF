﻿using SQLite;

namespace ContactsApp.Models;

[Table("Contacts")]
public class Contact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
}
