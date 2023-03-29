using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class ReputationRepository : IReputationRepository
    {
        private readonly ForumDbContext _context;
        public ReputationRepository(ForumDbContext context)
        {
            _context = context;
        }

        public ClickedReputation? GetClickedReputation(int userId, int topicId)
        {
            return _context.ClickedReputations.FirstOrDefault(e => e.UserId == userId && e.TopicId == topicId);
        }

        public void AddToClickedReputations(ClickedReputation clickedReputation)
        {
            _context.ClickedReputations.Add(clickedReputation);
            _context.SaveChanges();
        }

        public void RemoveFromClickedReputations(ClickedReputation clickedReputation)
        {
            _context.ClickedReputations.Remove(clickedReputation);
            _context.SaveChanges();
        }
        public void EditClickedReputations(ClickedReputation clickedReputation)
        {
            _context.ClickedReputations.Update(clickedReputation);
            _context.SaveChanges();
        }
    }
}
