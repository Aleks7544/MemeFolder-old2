using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MemeFolder.Areas.Identity.IdentityHostingStartup))]
namespace MemeFolder.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}