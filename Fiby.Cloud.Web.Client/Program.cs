using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((host, config) =>
                    {
                        var settings = config.Build();
                        //config.AddAzureAppConfiguration (options =>
                        //{
                        //    options
                        //    .ConfigureRefresh(refresh =>
                        //    {
                        //        refresh.Register("Version", true)
                        //           .SetCacheExpiration(TimeSpan.FromSeconds(5));
                        //    });
                        //});
                    }).UseKestrel(options =>
                    {
                        options.Limits.MaxRequestHeadersTotalSize = 1048576;
                    }).UseStartup<Startup>();
                });
    }
}
