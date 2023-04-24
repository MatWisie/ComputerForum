using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> GetTopics(int categoryId);
        Topic? GetTopic(int id);
        Topic? GetTopicIncludeComments(int id);
        void EditTopic(Topic topic);
        void DeleteTopic(Topic topic);
        void AddTopic(Topic topic);
        int CountTopics();
    }
}