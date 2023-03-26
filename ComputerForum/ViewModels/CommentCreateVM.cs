namespace ComputerForum.ViewModels
{
    public class CommentCreateVM
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public string? QuotedStatement { get; set; }
        public int TopicId { get; set; }
    }
}
