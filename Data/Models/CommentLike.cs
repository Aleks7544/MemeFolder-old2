namespace MemeFolder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CommentLike : Like
    {
        [Required]
        public string CommentId { get; init; }

        [Required]
        public Comment Comment { get; init; }
    }
}
