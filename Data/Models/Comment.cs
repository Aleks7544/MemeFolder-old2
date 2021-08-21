namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Comment
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string PostId { get; init; }

        public Post Post { get; init; }

        public string CommentatorId { get; init; }

        public User Commentator { get; init; }

        public string Text { get; set; }

        public ICollection<MediaFile> MediaFiles { get; init; } = new List<MediaFile>();

        public ICollection<CommentLike> CommentLikes { get; init; } = new HashSet<CommentLike>();
    }
}
