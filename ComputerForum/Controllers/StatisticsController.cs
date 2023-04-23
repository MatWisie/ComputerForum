using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult BlogStatistics()
        {
            BlogStatisticsVM blogStatistics = _statisticsService.GetBlogStatistics();
            return PartialView(blogStatistics);
        }
    }
}
