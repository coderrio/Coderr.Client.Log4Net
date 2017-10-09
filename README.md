log4net integration library
===========================

This library will report all exceptions that you log using log4net.

For instance:

```csharp
try
{
    SomeBusinessCode();
}
catch (Exception ex)
{
    _logger.Debug("Failed to do business", ex);
}
```

Will result in an exception in codeRR with the following (extra) context information:

![](docs/contextinfo.png)


# Installation

1. Download and install the [codeRR Community Server](https://github.com/coderrapp/coderr.server) or create an account at [coderrapp.com](https://coderrapp.com)
2. Install this client library (using nuget `coderr.client.log4net`)
3. Configure the credentials from your codeRR account in your `Program.cs`.
4. Add the following line to activate this library: `Err.Configuration.CatchLog4NetExceptions();`

Full example:

```csharp
namespace codeRR.Client.Log4net.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

			//when using our live service.
            var url = new Uri("https://report.coderrapp.com/");
            Err.Configuration.Credentials(url,
                "yourAppKey",
                "yourSharedSecret");

            // injects into the log4net pipeline
            Err.Configuration.CatchLog4NetExceptions();

			
			//try the config
            var log = LogManager.GetLogger(typeof(Program));
            log.Info("Hello word");

            var service = new SomeService();
            service.DoSomeStuff();

            Console.WriteLine("Exception have been logged.");
            Console.ReadLine();
        }
    }
}
```csharp

# Questions?

* [Documentation](https://coderrapp.com/documentation/client/libraries/log4net/) 
* [Forum](http://discuss.coderrapp.com)
