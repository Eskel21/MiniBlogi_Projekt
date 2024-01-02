using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniBlogiv2.Data.Models;
using System.Reflection.Emit;

namespace MiniBlogiv2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Tag> Tag { get; set; }

        

        public DbSet<TagNote> TagNote { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TagNote>()
            .HasKey(pg => new { pg.TagId, pg.NoteId });

            builder.Entity<TagNote>()
            .HasOne<Tag>(pg => pg.Tag)
            .WithMany(p => p.TagNotes)
            .HasForeignKey(p => p.TagId);

            builder.Entity<TagNote>()
            .HasOne<Note>(pg => pg.Note)
            .WithMany(g => g.TagNotes)
            .HasForeignKey(g => g.NoteId);



            builder.Entity<ImageNote>()
           .HasKey(pg => new { pg.ImageId, pg.NoteId });

            builder.Entity<ImageNote>()
            .HasOne<Image>(pg => pg.Image)
            .WithMany(p => p.ImageNotes)
            .HasForeignKey(p => p.ImageId);

            builder.Entity<ImageNote>()
            .HasOne<Note>(pg => pg.Note)
            .WithMany(g => g.ImageNotes)
            .HasForeignKey(g => g.NoteId);

            builder.Entity<Note>()
                .Property(e => e.UsersLiked)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        }

    }
}