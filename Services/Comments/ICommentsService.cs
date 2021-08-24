namespace MemeFolder.Services.Comments
{
    using System.Collections.Generic;
    using Models;

    public interface ICommentsService
    {
        bool CreateComment(CreateCommentModel model, string postId, string userId);

        string EditComment(string commentId, string text);

        string DeleteComment(string commentId);

        bool DeleteAllCommentsFromUser(string userId);

        string LikeComment(string commentId, string userId);

        bool IsLiked(string commentId, string userId);

        T GetCommentById<T>(string commentId);

        IEnumerable<T> GetAllCommentsFromUser<T>(string userId);

        IEnumerable<CommentViewModel> ConstructCommentSection(int page, int pageSize, string postId, string userId);
    }
}
