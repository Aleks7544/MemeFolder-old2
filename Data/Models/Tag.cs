namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Tag;

    public class Tag
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; init; }

        public ICollection<MediaFile> MediaFiles { get; init; } = new HashSet<MediaFile>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();
    }
}
