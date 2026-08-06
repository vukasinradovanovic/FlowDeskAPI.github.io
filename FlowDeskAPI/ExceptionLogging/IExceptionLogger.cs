using System;

namespace FlowDesk.API.ExceptionLogging
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex);
    }
}
