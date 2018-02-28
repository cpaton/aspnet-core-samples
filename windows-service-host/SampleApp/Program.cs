using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Message("Starting");

                var webHostBuilder = WebHost.CreateDefaultBuilder();

                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyFolder = Path.GetDirectoryName(assemblyLocation);
                webHostBuilder.UseContentRoot(assemblyFolder);
                Environment.CurrentDirectory = assemblyFolder;

                webHostBuilder
                    .UseStartup<Startup>()
                    .UseHttpSys(options =>
                    {
                        options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                        options.Authentication.AllowAnonymous = false;
                    })
                    .UseUrls("http://*:8000");
                var webHost = webHostBuilder.Build();

                var serviceHost = new ServiceHost(webHost);

                Log.Message("About to start service host");
                ServiceBase.Run(serviceHost);

                //webHost.Run();
            }
            catch (Exception e)
            {
                Log.Message($"Exception - {e}");
            }

        }
    }
}
