namespace MemeFolder.Services.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Data.Models;
    using Models;
    using Shared;

    public interface IPostsService
    {
        Post CreatePost(string text, string userId);

        bool EditPost(string postId, string text);

        bool EditPostVisibility(string postId, VisibilityFormModel model);

        bool DeletePost(string postId);

        bool DeleteAllPostsFromUser(string userId);

        bool LikePost(string postId, string userId);

        bool IsLiked(string postId, string userId);

        T GetPostById<T>(string postId);

        IEnumerable<T> GetAllPostFromUser<T>(string userId);

        IEnumerable<PostViewModel> ConstructPostsFeed(int page, int pageSize, string userId, int days, string section);

        IEnumerable<T> GetHottestPosts<T>(int page, int pageSize, string userId, Func<Post, bool> condition);

        IEnumerable<T> GetNewestPosts<T>(int page, int pageSize, string userId, Func<Post, bool> condition);

        IEnumerable<T> GetTopPosts<T>(int page, int pageSize, string userId, int days, Func<Post, bool> condition);

        IEnumerable<T> Shuffle<T>(IEnumerable<T> list, int size);
    }
}
