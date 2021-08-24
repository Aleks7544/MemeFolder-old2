namespace MemeFolder.Controllers
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Services.Collections;
    using Services.Collections.Models;
    using Services.Shared;

    public class CollectionsController : Controller
    {
        private readonly ICollectionsService collectionsService;

        public CollectionsController(ICollectionsService collectionsService)
        {
            this.collectionsService = collectionsService;
        }

        public IActionResult CreateCollection(CollectionCreateModel model)
        {
            string postId = this.collectionsService.CreateCollection(model, this.User.Id());

            return postId != null ? this.ViewCollection(postId) : BadRequest();
        }

        public IActionResult EditCollection(string id, CollectionEditModel model)
        {
            bool success = this.collectionsService.EditCollection(id, model);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult EditCollectionVisibility(string id, VisibilityFormModel model)
        {
            bool success = this.collectionsService.EditCollectionVisibility(id, model);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult DeleteCollection(string id)
        {
            bool success = this.collectionsService.DeleteCollection(id);

            return success ? View("/") : BadRequest();
        }

        public IActionResult ViewCollection(string id)
        {
            CollectionViewModel collectionView = this.collectionsService.ViewCollection(id);

            return View(collectionView);
        }

        public IActionResult ViewLikedCollections()
        {
            IEnumerable<CollectionViewModel> collectionView = this.collectionsService.ViewLikedCollections(this.User.Id());

            return View(collectionView);
        }

        public IActionResult LikeCollection(string id)
        {
            bool success = this.collectionsService.LikeCollection(id, this.User.Id());

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult AddMediaFilesToCollection(string id, IEnumerable<IFormFile> files)
        {
            bool success = this.collectionsService.AddMediaFilesToCollection(id, files, this.User.Id());

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult RemoveMediaFileFromCollection(string id, string mediaFileId)
        {
            bool success = this.collectionsService.RemoveMediaFileFromCollection(id, mediaFileId);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult AddPostsToCollection(string id, IEnumerable<string> postsIds)
        {
            bool success = this.collectionsService.AddPostsToCollection(id, postsIds);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult RemovePostFromCollection(string id, string postId)
        {
            bool success = this.collectionsService.RemovePostFromCollection(id, postId);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult AddTagsToCollection(string id, IEnumerable<string> tagsIds)
        {
            bool success = this.collectionsService.AddTagsToCollection(id, tagsIds);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult RemoveTagFromCollection(string id, string tagId)
        {
            bool success = this.collectionsService.RemoveTagFromCollection(id, tagId);

            return success ? ViewCollection(id) : BadRequest();
        }

        public IActionResult DownloadCollectionContent(string id)
        {
            throw new NotImplementedException();
        }
    }
}
