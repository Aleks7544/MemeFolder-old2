namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class MediaFile
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public MediaFileType MediaFileType { get; init; }

        [Required]
        public string FileExtension { get; init; }

        [Required]
        public string FilePath { get; init; }

        [Required]
        public string UploaderId { get; init; }

        [Required]
        public User Uploader { get; init; }

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();
    }
}
