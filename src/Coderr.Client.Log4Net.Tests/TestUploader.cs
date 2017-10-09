using System;
using System.Collections.Generic;
using System.Linq;
using codeRR.Client.Contracts;
using codeRR.Client.Uploaders;

namespace codeRR.Client.Log4Net.Tests
{
    public class TestUploader : IReportUploader
    {
        public static TestUploader Instance = new TestUploader();
        public List<FeedbackDTO> Feedbacks = new List<FeedbackDTO>();
        public List<ErrorReportDTO> Reports = new List<ErrorReportDTO>();

        public void UploadFeedback(FeedbackDTO feedback)
        {
            Feedbacks.Add(feedback);
        }

        public void UploadReport(ErrorReportDTO report)
        {
            Reports.Add(report);
        }

        public event EventHandler<UploadReportFailedEventArgs> UploadFailed;

        public ErrorReportDTO GetReport(string message)
        {
            foreach (var report in Reports)
            {
                var collection = report.ContextCollections.First(x => x.Name == "LogEntryDetails");
                if (collection.Properties["Message"] == message)
                    return report;
            }
            throw new InvalidOperationException("No matching message");
        }

        public ErrorReportDTO FindReport(string message)
        {
            foreach (var report in Reports)
            {
                var collection = report.ContextCollections.First(x => x.Name == "LogEntryDetails");
                if (collection.Properties["Message"] == message)
                    return report;
            }
            return null;
        }
    }
}