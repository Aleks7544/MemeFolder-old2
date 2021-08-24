namespace MemeFolder.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Services.Comments;
    using Services.Comments.Models;

    using static Data.DataConstants.CommentsController;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [Authorize]
        public IActionResult CreateComment(CreateCommentModel model, string postId)
        {
            bool success = this.commentsService.CreateComment(model, postId, this.User.Id());

            return success ? View($"/Post/{postId}") : BadRequest();
        }

        public IActionResult ViewPostComments(string postId, int page)
        {
            IEnumerable<CommentViewModel> commentViews = this.commentsService
                .ConstructCommentSection(page, PageSize, postId, this.User.Id());

            return this.View(commentViews);
        }

        [Authorize]
        public IActionResult DeleteComment(string id)
        {
            string postId = this.commentsService.DeleteComment(id);

            return postId != null ? this.View($"/Post/{postId}") : BadRequest();
        }

        [Authorize]
        public IActionResult LikeComment(string id)
        {
            string postId = this.commentsService.LikeComment(id, this.User.Id());

            return postId != null ? this.View($"/Post/{postId}") : BadRequest();
        }

        [Authorize]
        public IActionResult EditComment(string id, string text)
        {
            string postId = this.commentsService.EditComment(id, text);

            return postId != null ? this.View($"/Post/{postId}") : BadRequest();
        }
    }
}
