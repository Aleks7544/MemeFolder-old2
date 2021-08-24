namespace MemeFolder.Controllers
{
    using Data.Models;
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
        public IActionResult EditPost(string id)
        {

        }

        public IActionResult DeletePost(string id)
        {

        }

        public IActionResult 
    }
}
