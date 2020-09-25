using System;
using System.IO;
using Coderr.Client;
using log4net;
using log4net.Config;

namespace Coderr.Client.Log4net.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            LogManager.GetLogger(typeof(Program)).Info("Started");

            var url = new Uri("http://localhost:60473/");
            Err.Configuration.Credentials(url,
                "1a68bc3e123c48a3887877561b0982e2",
                "bd73436e965c4f3bb0578f57c21fde69");

            // injects into the log4net pipeline
            Err.Configuration.CatchLog4NetExceptions();

            var log = LogManager.GetLogger(typeof(Program));
            log.Info("Hello word");

            var service = new SomeService();
            service.DoSomeStuff();

            Console.WriteLine("Exception have been logged.");
            Console.ReadLine();
        }
    }
}