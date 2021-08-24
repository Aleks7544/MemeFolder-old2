namespace MemeFolder.Services.Comments.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    using Data.Attributes;

    using static Data.DataConstants.Comment;

    public class CreateCommentModel
    {
        [MaxLength(MaxTextLength)]
        [TextOrMediaFileRequired]
        public string Text { get; set; }

        [MaxLength(MaxMediaFiles)]
        public ICollection<IFormFile> MediaFiles { get; init; }
    }
}
