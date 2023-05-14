using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ComputerForum.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<StatisticsController> _logger;
        public StatisticsController(IStatisticsService statisticsService, IMemoryCache cache, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult BlogStatistics()
        {
            
            if (_cache.TryGetValue("BlogStatisticsCacheKey", out BlogStatisticsVM blogStatistics))
            {
                var cachedStats = _cache.Get("BlogStatisticsCacheKey");
                _logger.LogInformation("Statistics returned from cache");
                return PartialView(blogStatistics);
            }
            else
            {
                BlogStatisticsVM newBlogStatistics = _statisticsService.GetBlogStatistics();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(900))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);

                _cache.Set("BlogStatisticsCacheKey", newBlogStatistics, cacheEntryOptions);
                _logger.LogInformation("Statistics created in cache");

                return PartialView(newBlogStatistics);
            }
        }
    }
}
