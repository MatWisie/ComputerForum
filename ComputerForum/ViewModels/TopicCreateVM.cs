namespace ComputerForum.ViewModels
{
    public class TopicCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
    }
}
