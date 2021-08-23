namespace MemeFolder.Services.Collections
{
    public interface ICollectionsService
    {
        bool RemovePostFromCollection(string postId);

        T GetById<T>(string id);
    }
}
