using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Hymn.Services.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}

// TODO: Clean up logic as it may be messy in some parts
// TODO: Cut as much fat as possible
// TODO: Setup presentation layer
// TODO: Remove weird dependencies (NetDevPack)
// TODO: Fix history
// TODO: Implement paging
// TODO: Implement PATCH endpoints (low prio)