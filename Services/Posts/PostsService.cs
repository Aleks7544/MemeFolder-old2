namespace MemeFolder.Services.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Models;
    using Relationships;
    using Shared;

    public class PostsService : IPostsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IRelationshipsService relationshipsService;

        public PostsService(MemeFolderDbContext db, IRelationshipsService relationshipsService)
        {
            this.db = db;
            this.relationshipsService = relationshipsService;
        }

        public Post CreatePost(string text, string userId)
            => new Post
            {
                PostedOn = DateTime.UtcNow,
                PosterId = userId,
                Text = text
            };

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

        public T GetPostById<T>(string postId)
            => this.db.Posts
                .Where(p => p.Id == postId)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<T> GetAllPostFromUser<T>(string userId)
            => this.db.Posts
                .Where(p => p.PosterId == userId)
                .To<T>();

        public IEnumerable<PostViewModel> ConstructPostsFeed(int page, int pageSize, string userId, int days, string section)
        {
            Expression<Func<Post, bool>> bestFriendCondition = p
                => p.VisibleToBestFriends
                   && this.relationshipsService
                       .IsBestFriend(userId, p.PosterId);

            Expression<Func<Post, bool>> friendCondition = p
                => p.VisibleToFriends
                   && this.relationshipsService
                       .IsFriend(userId, p.PosterId);

            Expression<Func<Post, bool>> followingCondition = p
                => p.VisibleToFollowers
                   && this.relationshipsService
                       .IsFollowing(userId, p.PosterId);

            Expression<Func<Post, bool>> publicCondition = p => p.VisibleToThePublic;

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
            Expression<Func<Post, bool>> condition)
            => this.db.Posts
                .Where(condition)
                .Where(p => (DateTime.UtcNow - p.PostedOn).TotalDays <= 3)
                .OrderByDescending(p => p.PostLikes.Count)
                .ThenByDescending(p => p.Comments.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4)
                .To<T>();

        public IEnumerable<T> GetNewestPosts<T>(int page, int pageSize, string userId,
            Expression<Func<Post, bool>> condition)
            => this.db.Posts
                .Where(condition)
                .OrderBy(p => p.PostedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4)
                .To<T>();

        public IEnumerable<T> GetTopPosts<T>(int page, int pageSize, string userId, int days,
            Expression<Func<Post, bool>> condition)
            => this.db.Posts
                .Where(condition)
                .Where(p => (DateTime.UtcNow - p.PostedOn).TotalDays <= days)
                .OrderByDescending(p => p.PostLikes.Count)
                .ThenByDescending(p => p.Comments.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize / 4)
                .To<T>();

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
