namespace MemeFolder.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Models;
    using Services.Posts;
    using Services.Posts.Models;

    using static Data.DataConstants.HomeController;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostsService postsService;

        public HomeController(ILogger<HomeController> logger, IPostsService postsService)
        {
            _logger = logger;
            this.postsService = postsService;
        }

        public IActionResult Index(int page)
        {
            IEnumerable<PostViewModel> posts =
                this.postsService.ConstructPostsFeed(1, PageSize, this.User.Id(), MaxAgeForHotPostsInDays, PostsFeedSectionName);

            return View(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
