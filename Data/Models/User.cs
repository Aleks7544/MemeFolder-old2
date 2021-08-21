namespace MemeFolder.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public bool IsAccountActive { get; set; } = true;

        public DateTime CreatedOn { get; init; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? Birthday { get; set; }

        public string DisplayName { get; set; }

        public string ProfilePicturePath { get; set; }

        public string BackgroundPicturePath { get; set; }

        public string Bio { get; set; }

        public ICollection<User> Following { get; init; } = new HashSet<User>();

        public ICollection<User> Followers { get; init; } = new HashSet<User>();

        public ICollection<User> BlockedUsers { get; init; } = new HashSet<User>();

        public ICollection<Relationship> Relationships { get; init; } = new HashSet<Relationship>();

        public ICollection<Collection> Collections { get; init; } = new HashSet<Collection>();
    }
}
