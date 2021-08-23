namespace MemeFolder.Services.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Infrastructure.Extensions;

    public class CollectionsService : ICollectionsService
    {
        private readonly MemeFolderDbContext db;

        public CollectionsService(MemeFolderDbContext db)
        {
            this.db = db;
        }

        public bool RemovePostFromCollection(string postId)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(string id)
            => this.db.Collections
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
    }
}
