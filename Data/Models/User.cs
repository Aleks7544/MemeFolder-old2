namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        public bool IsAccountActive { get; set; } = true;

        [Required]
        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        [MaxLength(MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string ProfilePicturePath { get; set; }

        public string BackgroundPicturePath { get; set; }

        [MaxLength(MaxBioLength)]
        public string Bio { get; set; }

        public ICollection<Relationship> Relationships { get; init; } = new HashSet<Relationship>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();

        public ICollection<CollectionLike> CollectionLikes { get; init; } = new HashSet<CollectionLike>();

        public ICollection<CommentLike> CommentLikes { get; init; } = new HashSet<CommentLike>();

        public ICollection<PostLike> PostLikes { get; init; } = new HashSet<PostLike>();

        public ICollection<MediaFile> MediaFiles { get; init; } = new HashSet<MediaFile>();
    }
}
