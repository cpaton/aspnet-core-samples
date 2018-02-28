using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApp
{
    public class ServiceHost : ServiceBase
    {
        private readonly IWebHost _webHost;
        private bool _stopRequestedByWindows;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        protected override void OnStart(string[] args)
        {
            OnStarting(args);
            _webHost.Services.GetRequiredService<IApplicationLifetime>()
                    .ApplicationStopped.Register(() =>
                    {
                        if (_stopRequestedByWindows)
                        {
                            return;
                        }
                        Stop();
                    });
            Log.Message("Starting WebHost");
            _webHost.Start();
            Log.Message("Started");
            OnStarted();
        }

        protected override void OnStop()
        {
            _stopRequestedByWindows = true;
            OnStopping();
            Log.Message("Stopping WebHost");
            _webHost?.Dispose();
            OnStopped();
        }

        protected virtual void OnStarted()
        {
        }

        protected virtual void OnStarting(string[] args)
        {
        }

        protected virtual void OnStopping()
        {
        }

        protected virtual void OnStopped()
        {
        }
    }
}