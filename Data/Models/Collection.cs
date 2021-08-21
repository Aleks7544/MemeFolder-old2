namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Collection
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatorId { get; init; }

        public User Creator { get; init; }

        public Visibility Visibility { get; set; }

        public ICollection<MediaFile> MediaFiles { get; init; } = new HashSet<MediaFile>();

        public ICollection<Post> Posts { get; init; } = new HashSet<Post>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();

        public ICollection<CollectionLike> CollectionLikes { get; init; } = new HashSet<CollectionLike>();

        public ICollection<User> Followers { get; init; } = new HashSet<User>();
    }
}
