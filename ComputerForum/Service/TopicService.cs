using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Service
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ICommentRepository _commentsRepository;
        public TopicService(ITopicRepository topicRepository, ICommentRepository commentsRepository)
        {
            _topicRepository = topicRepository;
            _commentsRepository = commentsRepository;
        }
        public IList<Topic> GetTopics(string categoryName)
        {
            return _topicRepository.GetTopics(categoryName).ToList();
        }
        public TopicWithComments? GetTopic(int id)
        {
            var topic = _topicRepository.GetTopic(id);
            var comments = _commentsRepository.GetTopicComments(id).ToList();
            if(topic != null)
            {
                TopicWithComments tmp = new TopicWithComments()
                {
                    topic = topic,
                    comments = comments

                };
                return tmp;
            }
            return null;
        }
    }
}
