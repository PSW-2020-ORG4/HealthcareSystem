using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IntegrationAdapters
{
    public class Program
    {
        public static void Main(string[] args)
        {
		//trigger build
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
