using System;
using Coderr.Client.Contracts;
using Coderr.Client.Log4Net.ContextProviders;
using Coderr.Client.Reporters;
using FluentAssertions;
using Xunit;

namespace Coderr.Client.Log4Net.Tests.ContextProviders
{
    public class LogsProviderTests
    {
        private LogsProvider _sut;

        public LogsProviderTests()
        {
            _sut = new LogsProvider();
        }

        [Fact]
        public void Should_add_existing_entry()
        {
            _sut.Add(new LogEntryDto(DateTime.UtcNow.AddMinutes(-3), 3, "Hello"));
            var context = new ErrorReporterContext(this, new Exception());

            _sut.Collect(context);

            context.LogEntries[0].Message.Should().Be("Hello");
        }

        [Fact]
        public void Should_remove_old_entries()
        {
            _sut.Add(new LogEntryDto(DateTime.UtcNow.AddMinutes(-5).AddSeconds(-1), 3, "Hello"));
            var context = new ErrorReporterContext(this, new Exception());

            _sut.Collect(context);

            context.LogEntries.Should().BeNull();
        }

        [Fact]
        public void Should_merge_with_existing_entries()
        {
            _sut.Add(new LogEntryDto(DateTime.UtcNow.AddMinutes(-3), 3, "Hello1"));
            _sut.Add(new LogEntryDto(DateTime.UtcNow.AddMinutes(-1), 3, "Hello3"));
            var context = new ErrorReporterContext(this, new Exception())
            {
                LogEntries = new[] {new LogEntryDto(DateTime.UtcNow.AddMinutes(-2), 3, "Hello2")}
            };

            _sut.Collect(context);

            context.LogEntries[0].Message.Should().Be("Hello1");
            context.LogEntries[1].Message.Should().Be("Hello2");
            context.LogEntries[2].Message.Should().Be("Hello3");
        }

    }
}
