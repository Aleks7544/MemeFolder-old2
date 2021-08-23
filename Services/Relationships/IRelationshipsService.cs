namespace MemeFolder.Services.Relationships
{
    public interface IRelationshipsService
    {
        T GetRelationshipByUserIds<T>(string firstUserId, string secondUserId);

        bool IsFollowing(string firstUserId, string secondUserId);

        bool IsFriend(string firstUserId, string secondUserId);

        bool IsBestFriend(string firstUserId, string secondUserId);
    }
}
