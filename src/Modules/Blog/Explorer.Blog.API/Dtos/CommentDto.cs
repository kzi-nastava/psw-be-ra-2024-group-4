namespace Explorer.Blog.API.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Username { get; set; }
    }
}
