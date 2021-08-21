namespace MemeFolder.Data.Models
{
    public class CommentLike : Like
    {
        public string CommentId { get; init; }

        public Comment Comment { get; init; }
    }
}
