namespace MemeFolder.Services.Posts.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Attributes;
    using Microsoft.AspNetCore.Http;

    using static Data.DataConstants.Post;

    public class CreatePostModel
    {
        [Required]
        public bool VisibleToThePublic { get; init; }

        [Required]
        public bool VisibleToFollowers { get; init; }

        [Required]
        public bool VisibleToFriends { get; init; }

        [Required]
        public bool VisibleToBestFriends { get; init; }

        [MaxLength(MaxTextLength)]
        [TextOrMediaFileRequired]
        public string Text { get; init; }

        public ICollection<IFormFile> MediaFiles { get; init; }

        public string Tags { get; init; }
    }
}
