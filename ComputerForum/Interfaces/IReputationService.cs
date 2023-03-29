using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReputationService
    {
        string AddToClickedReputations(int userId, int topicId, bool isPositive);
        void RemoveFromClickedReputations(int userId, int topicId);
        ClickedReputation? GetClickedReputation(int userId, int topicId);
    }
}