codeRR log4net package
======================

Welcome to codeRR! 

We try to answer questions as fast as we can at our forum: http://discuss.coderapp.com. 
If you have any trouble at all, don't hesitate to post a message there.

This library is the log4net client library of codeRR. It sends all logged exceptions to 
the codeRR server for analysis.

Do note that this library do not process the information but require a codeRR server for that.
You can either install the open source server from https://github.com/coderrapp/coderr.server, or
use our hosted service at https://coderrapp.com/live.


Configuration
=============

To start with, you need to configure the connection to the codeRR server, 
this code is typically added in your Program.cs. This information is found either
in our hosted service or in your installed codeRR server.

    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // codeRR configuration
            var uri = new Uri("https://report.coderrapp.com/");
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

All unhandled exceptions are reported directly by this library. However, sometimes you'll want to use try/catch
for some custom handling (or being able to display pretty error messages).

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

More examples: https://coderrapp.com/documentation/client/
Questions? http://discuss.coderrapp.com
