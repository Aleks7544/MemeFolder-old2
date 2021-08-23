namespace MemeFolder.Infrastructure.Mapping
{
    using AutoMapper;

    using Data.Models;
    using Services.Posts.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Post, PostViewModel>();
        }
    }
}
