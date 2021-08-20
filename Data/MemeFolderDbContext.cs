namespace MemeFolder.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MemeFolderDbContext : IdentityDbContext
    {
        public MemeFolderDbContext(DbContextOptions<MemeFolderDbContext> options)
            : base(options)
        {
        }
    }
}
