namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Collection;

    public class Collection : IVisible
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public string CreatorId { get; init; }

        [Required]
        public User Creator { get; init; }

        [Required] 
        public bool VisibleToThePublic { get; set; } = false;

        [Required]
        public bool VisibleToFollowers { get; set; } = false;

        [Required]
        public bool VisibleToFriends { get; set; } = false;

        [Required]
        public bool VisibleToBestFriends { get; set; } = false;

        public ICollection<MediaFile> MediaFiles { get; init; } = new HashSet<MediaFile>();

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();

        public ICollection<CollectionLike> CollectionLikes { get; init; } = new HashSet<CollectionLike>();
    }
}
