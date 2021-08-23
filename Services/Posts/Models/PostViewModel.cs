namespace MemeFolder.Services.Posts.Models
{
    using System.Collections.Generic;

    using Collections.Models;
    using Comments.Models;
    using MediaFiles.Models;
    using Tags.Models;

    public class PostViewModel
    {
        public string Id { get; init; }

        public string PosterId { get; init; }

        public string PostedOn { get; init; }

        public bool IsEdited { get; init; }

        public bool VisibleToThePublic { get; init; }

        public bool VisibleToFollowers { get; init; }

        public bool VisibleToFriends { get; init; }

        public bool VisibleToBestFriends { get; init; }

        public string Text { get; init; }

        public IEnumerable<MediaFileViewModel> MediaFiles { get; init; }

        public IEnumerable<TagViewModel> Tags { get; init; }

        public IEnumerable<CollectionViewModel> Collections { get; init; }

        public IEnumerable<PostLikeViewModel> PostLikes { get; init; }

        public IEnumerable<CommentViewModel> Comments { get; init; }
    }
}
