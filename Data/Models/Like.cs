namespace MemeFolder.Data.Models
{
    using System;
    using Enums;

    public abstract class Like
    {
        public string UserId { get; init; }

        public string User { get; init; }

        public Reaction Reaction { get; set; }

        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }
    }
}
