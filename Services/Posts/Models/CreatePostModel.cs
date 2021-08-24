namespace MemeFolder.Services.Posts.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Attributes;
    using Microsoft.AspNetCore.Http;
    using Shared;
    using static Data.DataConstants.Post;

    public class CreatePostModel : VisibilityFormModel
    {
        [MaxLength(MaxTextLength)]
        [TextOrMediaFileRequired]
        public string Text { get; init; }

        public ICollection<IFormFile> MediaFiles { get; init; }

        public string Tags { get; init; }
    }
}
