namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Attributes;

    using static DataConstants.Comment;

    public class Comment
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string PostId { get; init; }

        [Required]
        public Post Post { get; init; }

        [Required]
        public string CommentatorId { get; init; }

        [Required]
        public User Commentator { get; init; }

        [MaxLength(MaxTextLength)]
        [TextOrMediaFileRequired]
        public string Text { get; set; }

        [MaxLength(MaxMediaFiles)]
        public ICollection<MediaFile> MediaFiles { get; init; } = new List<MediaFile>();

        public ICollection<CommentLike> CommentLikes { get; init; } = new HashSet<CommentLike>();
    }
}
