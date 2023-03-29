using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReputationRepository
    {
        void AddToClickedReputations(ClickedReputation clickedReputation);
        ClickedReputation? GetClickedReputation(int userId, int topicId);
        void RemoveFromClickedReputations(ClickedReputation clickedReputation);
        void EditClickedReputations(ClickedReputation clickedReputation);
    }
}