using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Zero.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //使用SeriLog

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Warning()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//Microsoft开头的文件重置为Information等级
              .Enrich.FromLogContext()
              .WriteTo.Console()//WriteTo--log的类型
              .WriteTo.File(Path.Combine("logs", "log.txt"), rollingInterval: RollingInterval.Day)
              .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseSerilog();//使用serilog
    }
}
