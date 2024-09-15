using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.DataAccess;

public class NoteAppDB : DbContext
{
    public NoteAppDB(DbContextOptions<NoteAppDB> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
}