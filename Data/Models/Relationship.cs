namespace MemeFolder.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Relationship
    {
        [Required]
        public string FirstUserId { get; init; }

        [Required]
        public User FirstUser { get; init; }

        [Required]
        public string SecondUserId { get; init; }

        [Required]
        public User SecondUser { get; init; }

        [Required]
        public bool FirstUserFollowsSecondUser { get; set; } = false;

        public DateTime? FirstUserFollowsSecondUserSince { get; set; }

        [Required]
        public bool SecondUserFollowsFirstUser { get; set; } = false;

        public DateTime? SecondUserFollowsFirstUserSince { get; set; }

        [Required]
        public bool FirstUserBlockedSecondUser { get; set; } = false;

        public DateTime? FirstUserBlockedSecondUserSince { get; set; }

        [Required]
        public bool SecondUserBlockedFirstUser { get; set; } = false;

        public DateTime? SecondUserBlockedFirstUserSince { get; set; }

        [Required]
        public RelationshipStatus Friends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? FriendsSince { get; set; }

        [Required]
        public RelationshipStatus BestFriends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? BestFriendsSince { get; set; }
    }
}
