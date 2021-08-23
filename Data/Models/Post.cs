namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Attributes;

    using static DataConstants.Post;

    public class Post : IVisible
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string PosterId { get; init; }

        [Required]
        public User Poster { get; init; }

        [Required]
        public DateTime PostedOn { get; init; }

        public DateTime? EditedOn { get; set; }

        [Required] 
        public bool VisibleToThePublic { get; set; } = true;

        [Required]
        public bool VisibleToFollowers { get; set; } = true;

        [Required]
        public bool VisibleToFriends { get; set; } = true;

        [Required]
        public bool VisibleToBestFriends { get; set; } = true;

        [MaxLength(MaxTextLength)]
        [TextOrMediaFileRequired]
        public string Text { get; set; }

        [MaxLength(MaxMediaFiles)]
        public ICollection<MediaFile> MediaFiles { get; init; } = new List<MediaFile>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<PostLike> PostLikes { get; init; } = new HashSet<PostLike>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();
    }
}
