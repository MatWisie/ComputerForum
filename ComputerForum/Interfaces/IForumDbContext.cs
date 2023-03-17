using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerForum.Interfaces
{
    public interface IForumDbContext 
    {
        DbSet<Category> Categories { get; set; }
        DbSet<ClickedReputation> ClickedReputations { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<User> Users { get; set; }
    }
}