using ComputerForum.Interfaces;
using ComputerForum.ViewModels;

namespace ComputerForum.Service
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topicRepository;

        public StatisticsService(IUserRepository userRepository, ICategoryRepository categoryRepository, ITopicRepository topicRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _topicRepository = topicRepository;
        }

        public BlogStatisticsVM GetBlogStatistics()
        {
            int userCount = _userRepository.CountUsers();
            int categoryCount = _categoryRepository.CountCategories();
            int topicCount = _topicRepository.CountTopics();
            BlogStatisticsVM tmp = new BlogStatisticsVM()
            {
                UserCount = userCount,
                CategoryCount = categoryCount,
                TopicCount = topicCount
            };
            return tmp;
        }

    }
}
