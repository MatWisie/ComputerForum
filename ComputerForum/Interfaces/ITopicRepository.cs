using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> GetTopics(string categoryName);
        Topic? GetTopic(int id);
        void EditTopic(Topic topic);
        void DeleteTopic(Topic topic);
    }
}