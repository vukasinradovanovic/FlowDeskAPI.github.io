using Sentry;
using System;

namespace FlowDesk.API.ExceptionLogging
{
    public class SentryExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            var id = SentrySdk.CaptureException(ex);
            return guid;
        }
    }
}
