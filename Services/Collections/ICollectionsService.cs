namespace MemeFolder.Services.Collections
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    using Models;
    using Shared;

    public interface ICollectionsService
    {
        string CreateCollection(CollectionCreateModel model, string userId);

        bool EditCollection(string collectionId, CollectionEditModel model);

        bool EditCollectionVisibility(string collectionId, VisibilityFormModel model);

        bool DeleteCollection(string collectionId);

        bool DeleteAllCollectionsFromUser(string userId);

        CollectionViewModel ViewCollection(string collectionId);

        IEnumerable<CollectionViewModel> ViewLikedCollections(string userId);

        bool LikeCollection(string collectionId, string userId);

        bool IsLiked(string collectionId, string userId);

        T GetCollectionById<T>(string collectionId);

        IEnumerable<T> GetAllCollectionsFromUser<T>(string userId);

        bool AddMediaFilesToCollection(string collectionId, IEnumerable<IFormFile> files, string userId);

        bool RemoveMediaFileFromCollection(string collectionId, string mediaFileId);

        bool AddPostsToCollection(string collectionId, IEnumerable<string> postsIds);

        bool RemovePostFromCollection(string collectionId, string postId);

        bool AddTagsToCollection(string collectionId, IEnumerable<string> tagsIds);

        bool RemoveTagFromCollection(string collectionId, string tagId);

        bool DownloadCollectionContent(string collectionId);
    }
}
