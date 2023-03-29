namespace ComputerForum.ViewModels
{
    public class CommentVM
    {
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        public int TopicId { get; set; }
        public int CreatorId { get; set; }
    }
}
