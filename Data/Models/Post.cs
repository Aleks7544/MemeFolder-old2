namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Post
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string PosterId { get; init; }

        public User Poster { get; init; }

        public DateTime PostedOn { get; init; }

        public DateTime? EditedOn { get; set; }

        public Visibility Visibility { get; set; }

        public string Text { get; set; }

        public string RepostedPostId { get; set; }

        public Post RepostedPost { get; set; }

        public ICollection<MediaFile> MediaFiles { get; init; } = new List<MediaFile>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();

        public ICollection<PostLike> PostLikes { get; init; } = new HashSet<PostLike>();

        public ICollection<Comment> Comments { get; init; } = new HashSet<Comment>();

        public ICollection<Tag> Tags { get; init; } = new HashSet<Tag>();
    }
}
