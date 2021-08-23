namespace MemeFolder.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class MemeFolderDbContext : IdentityDbContext<User>
    {
        public MemeFolderDbContext(DbContextOptions<MemeFolderDbContext> options)
            : base(options)
        {
        }
    }
}
