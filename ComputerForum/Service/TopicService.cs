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
        public IList<Topic> GetTopics(int categoryId)
        {
            return _topicRepository.GetTopics(categoryId).ToList();
        }
        public Topic? GetTopicWithComments(int id)
        {
            var topic = _topicRepository.GetTopicIncludeComments(id);
            if(topic != null)
            {

                return topic;
            }
            return null;
        }
        public Topic? GetTopic(int id)
        {
            var topic = _topicRepository.GetTopic(id);
            if (topic != null)
            {
                return topic;
            }
            return null;
        }
        public void EditTopic(Topic topic)
        {
            _topicRepository.EditTopic(topic);
        }
        public void DeleteTopic(Topic topic)
        {
            _topicRepository.DeleteTopic(topic);
        }

        public void AddTopic(TopicVM topic)
        {
            Topic tmp = new Topic
            {
                Title = topic.Title,
                Description = topic.Description,
                CreatorId = topic.CreatorId,
                CategoryId = topic.CategoryId
            };
            _topicRepository.AddTopic(tmp);
        }
    }
}
