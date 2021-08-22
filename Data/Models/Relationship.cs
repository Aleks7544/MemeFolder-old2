namespace MemeFolder.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Relationship
    {
        [Required]
        public RelationshipStatus Friends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? FriendsSince { get; set; }

        [Required]
        public RelationshipStatus BestFriends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? BestFriendsSince { get; set; }
    }
}
