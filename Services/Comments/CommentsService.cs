namespace MemeFolder.Services.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using MediaFiles;
    using Models;

    public class CommentsService : ICommentsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IMediaFilesService mediaFilesService;
        private readonly IConfigurationProvider mapper;

        public CommentsService(MemeFolderDbContext db, IMediaFilesService mediaFilesService, IConfigurationProvider mapper)
        {
            this.db = db;
            this.mediaFilesService = mediaFilesService;
            this.mapper = mapper;
        }

        public bool CreateComment(CreateCommentModel model, string postId, string userId)
        {
            Comment comment = new Comment
            {
                CommentatorId = userId,
                PostId = postId,
                Text = model.Text
            };

            if (model.MediaFiles.Any())
            {
                ICollection<MediaFile> mediaFiles = this.mediaFilesService.ConvertToMediaFiles(model.MediaFiles, userId);

                foreach (var mediaFile in mediaFiles)
                {
                    comment.MediaFiles.Add(mediaFile);
                }

                this.db.MediaFiles.AddRange(mediaFiles);
            }

            this.db.Comments.Add(comment);
            this.db.SaveChangesAsync();

            return true;
        }

        public string EditComment(string commentId, string text)
        {
            Comment comment = this.GetCommentById<Comment>(commentId);

            if (comment == null)
            {
                return null;
            }

            comment.Text = text;

            this.db.SaveChangesAsync();
            return comment.PostId;
        }

        public string DeleteComment(string commentId)
        {
            Comment comment = this.GetCommentById<Comment>(commentId);

            if (comment == null)
            {
                return null;
            }

            this.db.Comments.Remove(comment);
            this.db.SaveChangesAsync();

            return comment.PostId;
        }

        public bool DeleteAllCommentsFromUser(string userId)
        {
            IEnumerable<Comment> comments = this.GetAllCommentsFromUser<Comment>(userId);

            if (!comments.Any())
            {
                return false;
            }

            this.db.Comments.RemoveRange(comments);
            this.db.SaveChangesAsync();

            return true;
        }

        public string LikeComment(string commentId, string userId)
        {
            Comment comment = this.GetCommentById<Comment>(commentId);

            if (comment == null)
            {
                return null;
            }

            CommentLike commentLike = 
                comment.CommentLikes.FirstOrDefault(cl => cl.CommentId == commentId && cl.UserId == userId);

            if (commentLike != null)
            {
                comment.CommentLikes.Remove(commentLike);

                this.db.CommentLikes.Remove(commentLike);
                this.db.SaveChangesAsync();

                return comment.PostId;
            }

            commentLike = new CommentLike
            {
                CommentId = commentId,
                CreatedOn = DateTime.UtcNow,
                Reaction = Reaction.Like,
                UserId = userId
            };

            comment.CommentLikes.Add(commentLike);

            this.db.CommentLikes.Add(commentLike);
            this.db.SaveChangesAsync();

            return comment.PostId;
        }

        public bool IsLiked(string commentId, string userId)
            => this.db.CommentLikes
                .Any(cl => cl.CommentId == commentId
                           && cl.UserId == userId);

        public T GetCommentById<T>(string commentId)
            => this.db.Comments
                .Where(c => c.Id == commentId)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<T> GetAllCommentsFromUser<T>(string userId)
            => this.db.Comments
                .Where(c => c.CommentatorId == userId)
                .ProjectTo<T>(this.mapper);

        public IEnumerable<CommentViewModel> ConstructCommentSection(int page, int pageSize, string postId,
            string userId)
            => this.db.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CommentatorId == userId)
                .ThenByDescending(c => c.CommentLikes.Count)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CommentViewModel>(this.mapper);
    }
}
