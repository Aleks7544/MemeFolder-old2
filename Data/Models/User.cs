namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        public bool IsAccountActive { get; set; } = true;

        [Required]
        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        [MaxLength(MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string ProfilePicturePath { get; set; }

        public string BackgroundPicturePath { get; set; }

        [MaxLength(MaxBioLength)]
        public string Bio { get; set; }

        public ICollection<Relation> Relations { get; init; } = new HashSet<Relation>();

        public ICollection<Relationship> Relationships { get; init; } = new HashSet<Relationship>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();
    }
}
