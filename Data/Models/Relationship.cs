namespace MemeFolder.Data.Models
{
    using System;
    using Enums;

    public class Relationship
    {
        public RelationshipStatus Friends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? FriendsSince { get; set; }

        public RelationshipStatus BestFriends { get; set; } = RelationshipStatus.NoRelationship;

        public DateTime? BestFriendsSince { get; set; }
    }
}
