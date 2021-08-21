namespace MemeFolder.Data.Models
{
    using System;

    public class PostLike : Like
    {
        public string PostId { get; init; }

        public Post Post { get; init; }
    }
}
