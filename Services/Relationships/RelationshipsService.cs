namespace MemeFolder.Services.Relationships
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Infrastructure.Extensions;

    public class RelationshipsService : IRelationshipsService
    {
        private readonly MemeFolderDbContext db;

        public RelationshipsService(MemeFolderDbContext db)
        {
            this.db = db;
        }

        public T GetRelationshipByUserIds<T>(string firstUserId, string secondUserId)
            => this.db.Relationships
                .Where(r => r.FirstUserId == firstUserId && r.SecondUserId == secondUserId 
                            || r.FirstUserId == secondUserId && r.SecondUserId == firstUserId)
                .To<T>()
                .FirstOrDefault();

        public bool IsFollowing(string firstUserId, string secondUserId)
        {
            Relationship relationship = this.GetRelationshipByUserIds<Relationship>(firstUserId, secondUserId);

            if (relationship == null)
            {
                return false;
            }

            if (firstUserId == relationship.FirstUserId)
            {
                return relationship.FirstUserFollowsSecondUser;
            }

            return relationship.SecondUserFollowsFirstUser;
        }

        public bool IsFriend(string firstUserId, string secondUserId)
        {
            Relationship relationship = this.GetRelationshipByUserIds<Relationship>(firstUserId, secondUserId);

            if (relationship == null)
            {
                return false;
            }

            return relationship.Friends == RelationshipStatus.ConfirmedRelationship;
        }

        public bool IsBestFriend(string firstUserId, string secondUserId)
        {
            Relationship relationship = this.GetRelationshipByUserIds<Relationship>(firstUserId, secondUserId);

            if (relationship == null)
            {
                return false;
            }

            return relationship.BestFriends == RelationshipStatus.ConfirmedRelationship;
        }
    }
}
