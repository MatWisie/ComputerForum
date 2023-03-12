using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerForum.Data
{
    public class ForumDbContext : DbContext
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
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ClickedReputation> ClickedReputations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
