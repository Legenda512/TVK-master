using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TVK.Client.Daemon.Web.Models;

namespace TVK.Client.Daemon.Web
{
    public class Program
    {
        static string ip = RegisterIPMiddleware.GetIP();
       

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://" + ip + ":5000")
                .UseStartup<Startup>();
    }
}
