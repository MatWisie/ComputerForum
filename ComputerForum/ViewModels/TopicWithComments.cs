using ComputerForum.Models;

namespace ComputerForum.ViewModels
{
    public class TopicWithComments
    {
        public Topic topic { get; set; }
        public List<Comment> comments { get; set; }
    }
}
