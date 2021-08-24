namespace MemeFolder.Services.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using MediaFiles;
    using Models;
    using Posts;
    using Shared;
    using Tags;

    public class CollectionsService : ICollectionsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IMediaFilesService mediaFilesService;
        private readonly IPostsService postsService;
        private readonly ITagsService tagsService;
        private readonly IConfigurationProvider mapper;

        public CollectionsService(MemeFolderDbContext db, IConfigurationProvider mapper, IMediaFilesService mediaFilesService, IPostsService postsService, ITagsService tagsService)
        {
            this.db = db;
            this.mapper = mapper;
            this.mediaFilesService = mediaFilesService;
            this.postsService = postsService;
            this.tagsService = tagsService;
        }

        public string CreateCollection(CollectionCreateModel model, string userId)
        {
            Collection collection = new Collection
            {
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                Description = model.Description,
                Name = model.Name,
                VisibleToBestFriends = model.VisibleToBestFriends,
                VisibleToFriends = model.VisibleToFriends,
                VisibleToFollowers = model.VisibleToFollowers,
                VisibleToThePublic = model.VisibleToThePublic
            };

            this.db.Collections.Add(collection);
            this.db.SaveChangesAsync();

            return collection.Id;
        }

        public bool EditCollection(string collectionId, CollectionEditModel model)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            collection.Name = model.Name;
            collection.Description = model.Description;
            collection.ModifiedOn = DateTime.UtcNow;

            this.db.SaveChangesAsync();

            return true;
        }

        public bool EditCollectionVisibility(string collectionId, VisibilityFormModel model)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            collection.VisibleToBestFriends = model.VisibleToBestFriends;
            collection.VisibleToFriends = model.VisibleToFriends;
            collection.VisibleToFollowers = model.VisibleToFollowers;
            collection.VisibleToThePublic = model.VisibleToThePublic;

            this.db.SaveChangesAsync();

            return true;
        }

        public bool DeleteCollection(string collectionId)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            this.db.Collections.Remove(collection);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool DeleteAllCollectionsFromUser(string userId)
        {
            IEnumerable<Collection> collections = this.GetAllCollectionsFromUser<Collection>(userId);

            if (!collections.Any())
            {
                return false;
            }

            this.db.Collections.RemoveRange(collections);
            this.db.SaveChangesAsync();

            return true;
        }

        public CollectionViewModel ViewCollection(string collectionId)
            => this.GetCollectionById<CollectionViewModel>(collectionId);

        public IEnumerable<CollectionViewModel> ViewLikedCollections(string userId)
            => this.db.Collections
                .Where(c => c.CollectionLikes
                    .Any(cl => cl.UserId == userId))
                .ProjectTo<CollectionViewModel>(this.mapper);

        public bool LikeCollection(string collectionId, string userId)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            CollectionLike collectionLike =
                collection.CollectionLikes
                    .FirstOrDefault(cl => cl.CollectionId == collectionId && cl.UserId == userId);

            if (collectionLike != null)
            {
                collection.CollectionLikes.Remove(collectionLike);

                this.db.CollectionLikes.Remove(collectionLike);
                this.db.SaveChangesAsync();

                return true;
            }

            collectionLike = new CollectionLike
            {
                CollectionId = collectionId,
                CreatedOn = DateTime.UtcNow,
                Reaction = Reaction.Like,
                UserId = userId
            };

            collection.CollectionLikes.Add(collectionLike);

            this.db.CollectionLikes.Add(collectionLike);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool IsLiked(string collectionId, string userId)
            => this.db.CollectionLikes
                .Any(cl => cl.CollectionId == collectionId
                           && cl.UserId == userId);

        public T GetCollectionById<T>(string collectionId)
            => this.db.Collections
                .Where(c => c.Id == collectionId)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<T> GetAllCollectionsFromUser<T>(string userId)
            => this.db.Collections
                .Where(c => c.CreatorId == userId)
                .ProjectTo<T>(this.mapper);

        public bool AddMediaFilesToCollection(string collectionId, IEnumerable<IFormFile> files, string userId)
        {
            IEnumerable<MediaFile> mediaFiles = this.mediaFilesService.ConvertToMediaFiles(files, userId);

            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null && !mediaFiles.Any())
            {
                return false;
            }

            foreach (var mediaFile in mediaFiles)
            {
                foreach (var collectionTag in collection.Tags)
                {
                    mediaFile.Tags.Add(collectionTag);
                }

                mediaFile.Collections.Add(collection);
                collection.MediaFiles.Add(mediaFile);
            }

            this.db.MediaFiles.AddRange(mediaFiles);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool RemoveMediaFileFromCollection(string collectionId, string mediaFileId)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            MediaFile mediaFile = this.mediaFilesService.GetMediaFileById<MediaFile>(mediaFileId);

            if (mediaFile == null)
            {
                return false;
            }

            collection.MediaFiles.Remove(mediaFile);
            mediaFile.Collections.Remove(collection);

            this.db.SaveChangesAsync();

            return true;
        }

        public bool AddPostsToCollection(string collectionId, IEnumerable<string> postsIds)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            List<Post> posts = new List<Post>();
            Post post = null;

            foreach (var postId in postsIds)
            {
                post = this.postsService.GetPostById<Post>(postId);

                if (post != null)
                {
                    posts.Add(post);
                }
            }

            foreach (var verifiedPost in posts)
            {
                foreach (var collectionTag in collection.Tags)
                {
                    verifiedPost.Tags.Add(collectionTag);
                }

                verifiedPost.Collections.Add(collection);
                collection.Posts.Add(verifiedPost);
            }

            this.db.SaveChangesAsync();

            return true;
        }

        public bool RemovePostFromCollection(string collectionId, string postId)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            Post post = this.postsService.GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            collection.Posts.Remove(post);
            post.Collections.Remove(collection);

            this.db.SaveChangesAsync();

            return true;
        }

        public bool AddTagsToCollection(string collectionId, IEnumerable<string> tagsIds)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            List<Tag> tags = new List<Tag>();

            Tag tag = null;

            foreach (var tagId in tagsIds)
            {
                tag = this.tagsService.GetTagById<Tag>(tagId);

                if (tag != null)
                {
                    tags.Add(tag);
                }
            }

            foreach (var verifiedTag in tags)
            {
                foreach (var mediaFile in collection.MediaFiles)
                {
                    verifiedTag.MediaFiles.Add(mediaFile);
                }

                verifiedTag.Collections.Add(collection);
                collection.Tags.Add(tag);
            }

            this.db.SaveChangesAsync();

            return true;
        }

        public bool RemoveTagFromCollection(string collectionId, string tagId)
        {
            Collection collection = this.GetCollectionById<Collection>(collectionId);

            if (collection == null)
            {
                return false;
            }

            Tag tag = this.tagsService.GetTagById<Tag>(tagId);

            if (tag == null)
            {
                return false;
            }

            collection.Tags.Remove(tag);
            tag.Collections.Remove(collection);

            this.db.SaveChangesAsync();

            return true;
        }

        public bool DownloadCollectionContent(string collectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
