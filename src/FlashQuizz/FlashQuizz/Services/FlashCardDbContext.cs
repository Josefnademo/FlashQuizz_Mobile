using FlashQuizz.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FlashQuizz.Services;

public class FlashCardDbContext : DbContext
{
    public DbSet<FlashCard> FlashCards { get; set; }

    private string DbPath { get; }

    public FlashCardDbContext()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Combine(folder, "flashcards.db");

        Database.EnsureCreated(); //creation of DB if not created
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Filename={DbPath}");
}