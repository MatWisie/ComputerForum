using ComputerForum.Interfaces;
using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;
using ComputerForum.ViewModels;

namespace ComputerForum.Data
{
    public class ForumDbContext : DbContext, IForumDbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(e => e.Topics)
               .WithOne(e => e.User)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasMany(e => e.ClickedReputations)
               .WithOne(e => e.User)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasMany(e => e.Comments)
               .WithOne(e => e.User)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasMany(e => e.ReportCreators)
               .WithOne(e => e.ReportCreator)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasMany(e => e.ReportedUsers)
               .WithOne(e => e.ReportedUser)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
               .HasIndex(e => e.Name)
               .IsUnique();
            modelBuilder.Entity<User>()
               .HasIndex(e => e.Email)
               .IsUnique();
            modelBuilder.Entity<Category>()
               .HasIndex(e => e.Name)
               .IsUnique();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ClickedReputation> ClickedReputations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ComputerForum.ViewModels.TopicVM>? TopicVM { get; set; }
    }
}
