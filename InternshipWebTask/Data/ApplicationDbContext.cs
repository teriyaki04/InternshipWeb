using InternshipWebTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipWebTask.Data;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Author>(entity =>
            {
                entity
                    .ToTable("Authors");

                entity
                    .HasKey(x => x.Id);

                entity
                    .Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(20)
                    .IsRequired();

                entity
                    .Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(50)
                    .IsRequired();

                entity
                    .Property(x => x.Patronymic)
                    .HasColumnName("Patronymic")
                    .HasMaxLength(50);

                entity
                    .Property(x => x.DateOfBirth)
                    .HasColumnName("DateOfBirth")
                    .IsRequired();
                
                entity
                    .HasMany(x => x.Books)
                    .WithOne(x => x.Author)
                    .HasForeignKey(x => x.AuthorId)
                    .HasPrincipalKey(x => x.Id);
            });

        modelBuilder.Entity<Book>(entity =>
        {
            entity
                .ToTable("Books");

            entity
                .HasKey(x => x.Id);
            
            entity
                .Property(x => x.BookName)
                .HasColumnName("BookName")
                .IsRequired();
            
            entity
                .Property(x => x.PageQty)
                .HasColumnName("PageQty")
                .IsRequired();

            entity
                .Property(x => x.Genre)
                .HasColumnName("Genre")
                .IsRequired();

            entity
                .HasCheckConstraint(
                    "CK_CONSTRAINT_BOOKGENRE",
                    "Genre in ('Хоррор','Триллер','Фентези')"
                );
        });
    }
}