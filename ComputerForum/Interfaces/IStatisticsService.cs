using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IStatisticsService
    {
        BlogStatisticsVM GetBlogStatistics();
    }
}