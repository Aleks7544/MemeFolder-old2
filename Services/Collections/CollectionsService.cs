namespace MemeFolder.Services.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;

    public class CollectionsService : ICollectionsService
    {
        private readonly MemeFolderDbContext db;
        private readonly IConfigurationProvider mapper;

        public CollectionsService(MemeFolderDbContext db, IConfigurationProvider mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool RemovePostFromCollection(string postId)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(string id)
            => this.db.Collections
                .Where(c => c.Id == id)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();
    }
}
