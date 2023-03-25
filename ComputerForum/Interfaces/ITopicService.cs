using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface ITopicService
    {
        IList<Topic> GetTopics(string categoryName);
        TopicWithComments? GetTopic(int id);
    }
}