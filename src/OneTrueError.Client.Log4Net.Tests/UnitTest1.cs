using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using log4net;
using log4net.Core;
using OneTrueError.Client.log4net;
using Xunit;

namespace OneTrueError.Client.Log4Net.Tests
{
    public class OneTrueAppenderTests
    {
        public OneTrueAppenderTests()
        {
            if (LogManager.GetAllRepositories().Length == 0)
                LogManager.CreateRepository("Myname");
        }
        [Fact]
        public void should_include_loglevel_in_the_error_report()
        {
            var logMessage = Guid.NewGuid().ToString();
            var ex = new Exception("msg");
            var evt = new LoggingEvent(GetType(), LogManager.GetAllRepositories().First(), "OurLogger", Level.Critical, logMessage, ex);
            var uploader = TestUploader.Instance;
            OneTrue.Configuration.Uploaders.Register(uploader);

            var sut = new OneTrueAppender();
            sut.DoAppend(evt);

            var entry = TestUploader.Instance.GetReport(logMessage);
            entry.GetCollectionProperty("LogEntryDetails", "LogLevel").Should().Be("CRITICAL");
        }

        [Fact]
        public void should_include_message()
        {
            var logMessage = Guid.NewGuid().ToString();
            var ex = new Exception("msg");
            var evt = new LoggingEvent(GetType(), LogManager.GetAllRepositories().First(), "OurLogger", Level.Critical, logMessage, ex);
            var uploader = TestUploader.Instance;
            OneTrue.Configuration.Uploaders.Register(uploader);

            var sut = new OneTrueAppender();
            sut.DoAppend(evt);

            var entry = TestUploader.Instance.GetReport(logMessage);
            entry.GetCollectionProperty("LogEntryDetails", "Message").Should().Be(logMessage);
        }

        [Fact]
        public void should_include_thread_name()
        {
            Thread.CurrentThread.Name = "Hello";
            var logMessage = Guid.NewGuid().ToString();
            var ex = new Exception("msg");
            var evt = new LoggingEvent(GetType(), LogManager.GetAllRepositories().First(), "OurLogger", Level.Critical, logMessage, ex);
            var uploader = TestUploader.Instance;
            OneTrue.Configuration.Uploaders.Register(uploader);

            var sut = new OneTrueAppender();
            sut.DoAppend(evt);

            var entry = TestUploader.Instance.GetReport(logMessage);
            entry.GetCollectionProperty("LogEntryDetails", "ThreadName").Should().Be("Hello");
        }

        [Fact]
        public void should_include_timestamp()
        {
            Thread.CurrentThread.Name = "Hello";
            var logMessage = Guid.NewGuid().ToString();
            var ex = new Exception("msg");
            var evt = new LoggingEvent(GetType(), LogManager.GetAllRepositories().First(), "OurLogger", Level.Critical, logMessage, ex);
            var uploader = TestUploader.Instance;
            OneTrue.Configuration.Uploaders.Register(uploader);

            var sut = new OneTrueAppender();
            sut.DoAppend(evt);

            var entry = TestUploader.Instance.GetReport(logMessage);
            entry.GetCollectionProperty("LogEntryDetails", "Timestamp").Should().NotBeNull();
        }

        [Fact]
        public void should_ignore_log_entries_that_do_not_contain_an_exception()
        {
            var logMessage = Guid.NewGuid().ToString();
            var evt = new LoggingEvent(GetType(), LogManager.GetAllRepositories().First(), "OurLogger", Level.Critical, logMessage, null);
            var uploader = TestUploader.Instance;
            OneTrue.Configuration.Uploaders.Register(uploader);

            var sut = new OneTrueAppender();
            sut.DoAppend(evt);

            TestUploader.Instance.FindReport(logMessage).Should().BeNull();
        }
    }
}
