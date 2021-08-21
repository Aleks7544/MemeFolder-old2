namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Tag
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string Name { get; init; }

        public ICollection<MediaFile> MediaFiles { get; init; } = new HashSet<MediaFile>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();
    }
}
