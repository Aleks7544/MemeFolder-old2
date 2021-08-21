namespace MemeFolder.Data.Models
{
    using System;

    public class Relation
    {
        public string FirstUserId { get; init; }

        public User FirstUser { get; init; }

        public string SecondUserId { get; init; }

        public User SecondUser { get; init; }

        public bool Follows { get; set; } = false;

        public DateTime? FollowsSince { get; set; }

        public bool Blocked { get; set; }

        public DateTime? BlockedSince { get; set; }
    }
}
