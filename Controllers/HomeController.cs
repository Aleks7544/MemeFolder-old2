namespace MemeFolder.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;


    using Models;
    using Services.Posts;
    using Services.Posts.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostsService postsService;

        public HomeController(ILogger<HomeController> logger, IPostsService postsService)
        {
            _logger = logger;
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            IEnumerable<PostViewModel> posts =
                this.postsService.ConstructPostsFeed(1, 20, this.User.Id(), 5, "hot");

            return View(posts);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
