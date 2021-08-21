namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class MediaFile
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public MediaFileType MediaFileType { get; init; }

        public string FileName { get; init; }

        public string FileExtension { get; init; }

        public string FilePath { get; init; }

        public string UploaderId { get; init; }

        public User Uploader { get; init; }

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();
    }
}
