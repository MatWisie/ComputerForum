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
        public StatisticsController(IStatisticsService statisticsService, IMemoryCache cache)
        {
            _statisticsService = statisticsService;
            _cache = cache;
        }

        [HttpGet]
        public IActionResult BlogStatistics()
        {
            
            if (_cache.TryGetValue("BlogStatisticsCacheKey", out BlogStatisticsVM blogStatistics))
            {
                var cachedStats = _cache.Get("BlogStatisticsCacheKey");
                return PartialView(blogStatistics);
            }
            else
            {
                BlogStatisticsVM newBlogStatistics = _statisticsService.GetBlogStatistics();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);

                _cache.Set("BlogStatisticsCacheKey", newBlogStatistics, cacheEntryOptions);


                return PartialView(newBlogStatistics);
            }
        }
    }
}
