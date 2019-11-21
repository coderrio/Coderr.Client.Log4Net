Coderr log4net package
======================

This library is the log4net client library of Coderr. You also need to use a Coderr server.

https://coderr.io/documentation/getting-started/


Configuration
=============

To start with, you need to configure the connection to the Coderr server, 
this code is typically added in your Program.cs. The configuration settings
can be found in your chosen Coderr server.

    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // Coderr configuration
            var uri = new Uri("https://report.coderr.io/");
            Err.Configuration.Credentials(uri,
                "yourAppKey",
                "yourSharedSecret");

            // injects Coderr into the log4net pipeline
            Err.Configuration.CatchLog4NetExceptions();

            // the usual stuff
            // [...]
        }
    }



Questions
https://discuss.coderr.io

Guides and support
https://coderr.io/guides-and-support/
