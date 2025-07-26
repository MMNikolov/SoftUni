namespace Calisthenix.Server.Models.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int ThumbsUpCount { get; set; } = 0;
        public bool LikedByCurrentUser { get; set; }
    }
}
