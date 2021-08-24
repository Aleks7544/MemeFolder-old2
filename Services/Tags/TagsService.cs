namespace MemeFolder.Services.Tags
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.Models;

    public class TagsService : ITagsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IConfigurationProvider mapper;

        public TagsService(MemeFolderDbContext db, IConfigurationProvider mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public Tag CreateTag(string name)
        {
            Tag tag = GetTagByName<Tag>(name);

            if (tag != null)
            {
                return tag;
            }

            tag = new Tag
            {
                Name = name
            };

            return tag;
        }

        public T GetTagByName<T>(string name)
            => this.db.Tags
                .Where(t => t.Name == name)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();

        public T GetTagById<T>(string id)
            => this.db.Tags
                .Where(t => t.Id == id)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();
    }
}
