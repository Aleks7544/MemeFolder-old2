namespace MemeFolder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostLike : Like
    {
        [Required]
        public string PostId { get; init; }

        [Required]
        public Post Post { get; init; }
    }
}
