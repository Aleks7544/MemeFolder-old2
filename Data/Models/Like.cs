namespace MemeFolder.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public abstract class Like
    {
        [Required]
        public string UserId { get; init; }

        [Required]
        public string User { get; init; }

        [Required]
        public Reaction Reaction { get; set; }

        [Required]
        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }
    }
}
