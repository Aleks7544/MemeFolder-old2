namespace MemeFolder.Infrastructure.Mapping
{
    using AutoMapper;

    using Data.Models;
    using Services.Collections.Models;
    using Services.Comments.Models;
    using Services.MediaFiles.Models;
    using Services.Posts.Models;
    using Services.Tags.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Post, PostViewModel>();
            this.CreateMap<Collection, CollectionViewModel>();
            this.CreateMap<Comment, CommentViewModel>();
            this.CreateMap<MediaFile, MediaFileViewModel>();
            this.CreateMap<PostLike, PostLikeViewModel>();
            this.CreateMap<Tag, TagViewModel>();
            this.CreateMap<Relationship, Relationship>();
            this.CreateMap<Post, Post>();
        }
    }
}
