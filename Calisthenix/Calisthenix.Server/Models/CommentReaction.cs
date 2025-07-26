namespace Calisthenix.Server.Models
{
    public class CommentReaction
    {
        public int Id { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsThumbsUp { get; set; } 
    }
}
