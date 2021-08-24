namespace MemeFolder.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Posts;
    using Services.Posts.Models;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }
        
        [Authorize]
        public IActionResult CreatePost(CreatePostModel model)
        {
            string postId = this.postsService.CreatePost(model, this.User.Id());

            return View($"/Post/{postId}");
        }

        [Authorize]
        public IActionResult LikePost(string id)
        {
            bool success = this.postsService.LikePost(id, this.User.Id());

            return success ? Redirect("/") : BadRequest();
        }

        [Authorize]
        public IActionResult EditPost(string id, string text)
        {
            bool success = this.postsService.EditPost(id, text);

            return success ? View($"/Post/{id}") : BadRequest();
        }

        [Authorize]
        public IActionResult DeletePost(string id)
        {
            this.postsService.DeletePost(id);

            return View("/");
        }

        public IActionResult RemoveTagFromPost(string id, string tagId)
        {
            bool success = this.postsService.RemoveTagFromPost(id, tagId);

            return success ? View($"/Post/{id}") : BadRequest();
        }

        public IActionResult AddTagToPost(string id, string name)
        {
            bool success = this.postsService.AddTagToPost(id, name);

            return success ? View($"/Post/{id}") : BadRequest();
        }
    }
}
