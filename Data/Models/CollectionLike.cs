namespace MemeFolder.Data.Models
{
    public class CollectionLike : Like
    {
        public string CollectionId { get; init; }

        public Collection Collection { get; init; }
    }
}
