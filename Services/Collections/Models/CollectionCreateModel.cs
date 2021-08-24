namespace MemeFolder.Services.Collections.Models
{
    using System.ComponentModel.DataAnnotations;
    using Shared;
    using static Data.DataConstants.Collection;

    public class CollectionCreateModel : VisibilityFormModel
    {
        [Required]
        [StringLength(MaxNameLength, ErrorMessage = "The {0} of the collection must be between {2} and {1} characters.", MinimumLength = MinNameLength)]
        public string Name { get; init; }

        [StringLength(MaxDescriptionLength, ErrorMessage = "The {0} of the collection must be between {2} and {1} characters.", MinimumLength = MinDescriptionLength)]
        public string Description { get; init; }
    }
}
