using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using Topshelf;

namespace TopshelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfig =>
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                hostConfig.UseLog4Net();

                hostConfig.EnableServiceRecovery(serviceRecovery =>
                {
                    // first failure, 5 minute delay
                    serviceRecovery.RestartService(5);

                    // second failure, 10 minute delay
                    serviceRecovery.RunProgram(10, @"C:\Windows\Notepad.exe");

                    // subsequent failures, 15 minute delay
                    serviceRecovery.RestartComputer(15, "Topshelf demo failure");
                });

                hostConfig.DependsOn("Spooler");
                hostConfig.DependsOnEventLog();

                hostConfig.UseAssemblyInfoForServiceInfo();

                hostConfig.StartManually();

                hostConfig.RunAsNetworkService();

                hostConfig.Service<MyWindowsService>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(() => new MyWindowsService());
                    serviceConfig.WhenStarted(s => s.Start());
                    serviceConfig.WhenStopped(s => s.Stop());

                    serviceConfig.WhenPaused(s => s.Pause());
                    serviceConfig.WhenContinued(s => s.Resume());
                    serviceConfig.WhenShutdown(s => s.Shutdown());
                });
            });
        }
    }
}
