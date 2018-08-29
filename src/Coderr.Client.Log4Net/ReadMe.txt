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

            // injects codeRR into the log4net pipeline
            Err.Configuration.CatchLog4NetExceptions();

            // the usual stuff
            // [...]
        }
    }


Reporting exceptions
====================

All logged exceptions are reported directly by this library. 
To report exceptions manually, you can use the Err.Report() method.

Example:

    public void SomeBusiness(PostViewModel model)
    {
        try
        {
            _somService.Execute(model);
        }
        catch (Exception ex)
        {
            Err.Report(ex, model);

            //some custom handling
        }
    }


Questions
https://discuss.coderr.io

Guides and support
https://coderr.io/guides-and-support/
