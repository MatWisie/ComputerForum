using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Service
{
    public class ReputationService : IReputationService
    {
        private readonly IReputationRepository _reputationRepository;
        private readonly IUserService _userService;
        public ReputationService(IReputationRepository reputationRepository, IUserService userService)
        {
            _reputationRepository = reputationRepository;
            _userService = userService;
        }

        public string AddToClickedReputations(int userId, int topicId, bool isPositive)
        {
            var clickedReputation = _reputationRepository.GetClickedReputation(userId, topicId);
            if (clickedReputation == null)
            {
                ClickedReputation tmp = new ClickedReputation
                {
                    UserId = userId,
                    TopicId = topicId,
                    IsGoodOpinion = isPositive
                };
                if (isPositive)
                {
                    _userService.AddReputation(userId, +1);
                }
                else
                {
                    _userService.AddReputation(userId, -1);
                }
                _reputationRepository.AddToClickedReputations(tmp);
                return "Added";
            }
            if (clickedReputation != null)
            {
                if(clickedReputation.IsGoodOpinion && isPositive)
                {
                    RemoveFromClickedReputations(userId, topicId);
                    _userService.AddReputation(userId, -1);
                    return "Deleted";
                }
                if (!clickedReputation.IsGoodOpinion && !isPositive)
                {
                    RemoveFromClickedReputations(userId, topicId);
                    _userService.AddReputation(userId, +1);
                    return "Deleted";
                }
                if (!clickedReputation.IsGoodOpinion && isPositive)
                {
                    clickedReputation.IsGoodOpinion = !clickedReputation.IsGoodOpinion;

                    _reputationRepository.EditClickedReputations(clickedReputation);
                    _userService.AddReputation(userId, 2);
                    return "Added";
                }
                if (clickedReputation.IsGoodOpinion && !isPositive)
                {
                    clickedReputation.IsGoodOpinion = !clickedReputation.IsGoodOpinion;

                    _reputationRepository.EditClickedReputations(clickedReputation);
                    _userService.AddReputation(userId, -2);
                    return "Added";
                }
            }
            return "Something went wrong";
        }

        public void RemoveFromClickedReputations(int userId, int topicId)
        {
            var clickedReputation = _reputationRepository.GetClickedReputation(userId, topicId);
            if (clickedReputation != null)
                _reputationRepository.RemoveFromClickedReputations(clickedReputation);
        }

        public ClickedReputation? GetClickedReputation(int userId, int topicId)
        {
            return _reputationRepository.GetClickedReputation(userId, topicId);
        }
    }
}
