namespace MemeFolder.Services.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Infrastructure.Extensions;
    using MediaFiles;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using Relationships;
    using Shared;
    using Tags;

    public class PostsService : IPostsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IRelationshipsService relationshipsService;
        private readonly IMediaFilesService mediaFilesService;
        private readonly ITagsService tagsService;
        private readonly IConfigurationProvider mapper;

        public PostsService(MemeFolderDbContext db, IRelationshipsService relationshipsService, IConfigurationProvider mapper, IMediaFilesService mediaFilesService, ITagsService tagsService)
        {
            this.db = db;
            this.relationshipsService = relationshipsService;
            this.mapper = mapper;
            this.mediaFilesService = mediaFilesService;
            this.tagsService = tagsService;
        }

        public string CreatePost(CreatePostModel model, string userId)
        {
            Post post = new Post
            {
                PostedOn = DateTime.UtcNow,
                PosterId = userId,
                Text = model.Text,
                VisibleToBestFriends = model.VisibleToBestFriends,
                VisibleToFriends = model.VisibleToFriends,
                VisibleToFollowers = model.VisibleToFollowers,
                VisibleToThePublic = model.VisibleToThePublic,
            };

            ICollection<MediaFile> mediaFiles = this.mediaFilesService.ConvertToMediaFiles(model.MediaFiles, userId);

            if (model.MediaFiles.Any())
            {
                foreach (var mediaFile in mediaFiles)
                {
                    post.MediaFiles.Add(mediaFile);
                }
            }

            ICollection<Tag> tags = new List<Tag>();

            if (!string.IsNullOrEmpty(model.Tags))
            {
                ICollection<string> tagStrings = model.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries);

                foreach (var tagString in tagStrings)
                {
                    Tag tag = this.tagsService.CreateTag(tagString);

                    post.Tags.Add(tag);
                    tags.Add(tag);
                }
            }

            foreach (var mediaFile in post.MediaFiles)  
            {
                foreach (var tag in tags)
                {
                    mediaFile.Tags.Add(tag);
                }
            }

            foreach (var tag in post.Tags) 
            {
                foreach (var mediaFile in mediaFiles)
                {
                    tag.MediaFiles.Add(mediaFile);
                }
            }

            this.db.Posts.Add(post);
            this.db.SaveChangesAsync();

            return post.Id;
        }

        public bool EditPost(string postId, string text)
        {
            Post post = GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            post.Text = text;
            post.EditedOn = DateTime.UtcNow;

            this.db.SaveChangesAsync();

            return true;
        }

        public bool EditPostVisibility(string postId, VisibilityFormModel model)
        {
            Post post = GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            post.VisibleToThePublic = model.VisibleToThePublic;
            post.VisibleToBestFriends = model.VisibleToBestFriends;
            post.VisibleToFollowers = model.VisibleToFollowers;
            post.VisibleToFriends = model.VisibleToFriends;

            this.db.SaveChangesAsync();

            return true;
        }

        public bool DeletePost(string postId)
        {
            Post post = this.GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            this.db.Posts.Remove(post);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool DeleteAllPostsFromUser(string userId)
        {
            List<Post> posts = this.GetAllPostFromUser<Post>(userId).ToList();

            if (!posts.Any())
            {
                return true;
            }

            this.db.Posts.RemoveRange(posts);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool LikePost(string postId, string userId)
        {
            Post post = GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            PostLike postLike = new PostLike
            {
                CreatedOn = DateTime.UtcNow,
                PostId = postId,
                Reaction = Reaction.Like,
                UserId = userId,
            };

            post.PostLikes.Add(postLike);

            this.db.PostLikes.Add(postLike);
            this.db.SaveChangesAsync();

            return true;
        }

        public bool IsLiked(string postId, string userId)
            => this.db.PostLikes
                .Any(pl => pl.PostId == postId
                           && pl.UserId == userId);

        public bool RemoveTagFromPost(string postId, string tagId)
        {
            Post post = GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            Tag tag = this.tagsService.GetTagById<Tag>(tagId);

            if (tag == null)
            {
                return false;
            }

            bool success = post.Tags.Remove(tag);
            this.db.SaveChangesAsync();

            return success;
        }

        public bool AddTagToPost(string postId, string tagName)
        {
            Post post = GetPostById<Post>(postId);

            if (post == null)
            {
                return false;
            }

            Tag tag = this.tagsService.CreateTag(tagName);

            post.Tags.Add(tag);
            this.db.SaveChangesAsync();

            return true;
        }

        public T GetPostById<T>(string postId)
            => this.db.Posts
                .Where(p => p.Id == postId)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<T> GetAllPostFromUser<T>(string userId)
            => this.db.Posts
                .Where(p => p.PosterId == userId)
                .ProjectTo<T>(this.mapper);

        public IEnumerable<PostViewModel> ConstructPostsFeed(int page, int pageSize, string userId, int days, string section)
        {
            Func<Post, bool> bestFriendCondition = p
                => p.VisibleToBestFriends
                   && this.relationshipsService
                       .IsBestFriend(userId, p.PosterId);

            Func<Post, bool> friendCondition = p
                => p.VisibleToFriends
                   && this.relationshipsService
                       .IsFriend(userId, p.PosterId);

            Func<Post, bool> followingCondition = p
                => p.VisibleToFollowers
                   && this.relationshipsService
                       .IsFollowing(userId, p.PosterId);

            Func<Post, bool> publicCondition = p => p.VisibleToThePublic;

            IEnumerable<PostViewModel> posts = null;

            if (section == "hot")
            {
                posts = this.GetHottestPosts<PostViewModel>(page, pageSize, userId, bestFriendCondition);
                posts.Concat(this.GetHottestPosts<PostViewModel>(page, pageSize, userId, friendCondition));
                posts.Concat(this.GetHottestPosts<PostViewModel>(page, pageSize, userId, followingCondition));
                posts.Concat(this.GetHottestPosts<PostViewModel>(page, pageSize, userId, publicCondition));
            }
            else if (section == "new")
            {
                posts = this.GetNewestPosts<PostViewModel>(page, pageSize, userId, bestFriendCondition);
                posts.Concat(this.GetNewestPosts<PostViewModel>(page, pageSize, userId, friendCondition));
                posts.Concat(this.GetNewestPosts<PostViewModel>(page, pageSize, userId, followingCondition));
                posts.Concat(this.GetNewestPosts<PostViewModel>(page, pageSize, userId, publicCondition));
            }
            else if (section == "top")
            {
                posts = this.GetTopPosts<PostViewModel>(page, pageSize, userId, days, bestFriendCondition);
                posts.Concat(this.GetTopPosts<PostViewModel>(page, pageSize, userId, days, friendCondition));
                posts.Concat(this.GetTopPosts<PostViewModel>(page, pageSize, userId, days, followingCondition));
                posts.Concat(this.GetTopPosts<PostViewModel>(page, pageSize, userId, days, publicCondition));
            }

            if (section == "hot")
            {
                return this.Shuffle(posts, posts.Count());
            }

            return posts;
        }

        public IEnumerable<T> GetHottestPosts<T>(int page, int pageSize, string userId,
            Func<Post, bool> condition)
        {
            DateTime oldestDate = DateTime.UtcNow.AddDays(-5);

            var posts = this.db.Posts
                .Where(p => p.PostedOn >= oldestDate)
                .OrderByDescending(p => p.PostLikes.Count)
                .ThenByDescending(p => p.Comments.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4);

            return posts
                .AsQueryable()
                .ProjectTo<T>(this.mapper);
        }

        public IEnumerable<T> GetNewestPosts<T>(int page, int pageSize, string userId,
            Func<Post, bool> condition)
        {
            var posts = this.db.Posts
                .AsEnumerable()
                .Where(condition)
                .OrderBy(p => p.PostedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4);

            return posts
                .AsQueryable()
                .ProjectTo<T>(this.mapper);
        }

        public IEnumerable<T> GetTopPosts<T>(int page, int pageSize, string userId, int days,
            Func<Post, bool> condition)
        {
            var posts = this.db.Posts
                .AsEnumerable()
                .Where(condition)
                .Where(p => (DateTime.UtcNow - p.PostedOn).TotalDays <= days)
                .OrderByDescending(p => p.PostLikes.Count)
                .ThenByDescending(p => p.Comments.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4);

            return posts
                .AsQueryable()
                .ProjectTo<T>(this.mapper);
        }

        public IEnumerable<T> Shuffle<T>(IEnumerable<T> list, int size)
        {
            var r = new Random();

            var shuffledList =
                list.
                    Select(x => new { Number = r.Next(), Item = x }).
                    OrderBy(x => x.Number).
                    Select(x => x.Item).
                    Take(size);

            return shuffledList;
        }
    }
}
