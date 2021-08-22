namespace MemeFolder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CollectionLike : Like
    {
        [Required]
        public string CollectionId { get; init; }

        [Required]
        public Collection Collection { get; init; }
    }
}
