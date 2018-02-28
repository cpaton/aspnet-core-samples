using System.IO;
using System.Reflection;
using System.ServiceProcess;
using CommandLine;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;

namespace SampleApp
{
    [Verb("web", HelpText = "Run the web server")]
    public class WebCommandLine : CommandLine
    {
        [Option("service", HelpText = "Whether the website is being hosted within a windows service")]
        public bool Service { get; set; }

        public override int Execute()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();

            if (Service)
            {
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyFolder = Path.GetDirectoryName(assemblyLocation);
                webHostBuilder.UseContentRoot(assemblyFolder);
            }

            webHostBuilder
                .UseStartup<Startup>()
                .UseHttpSys(options =>
                {
                    options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                    options.Authentication.AllowAnonymous = false;
                })
                .UseUrls("http://*:8000");
            var webHost = webHostBuilder.Build();

            if (Service)
            {
                var serviceHost = new ServiceHost(webHost);
                Log.Message("About to start service host");
                ServiceBase.Run(serviceHost);
            }
            else
            {
                webHost.Run();
            }

            return 0;
        }
    }
}