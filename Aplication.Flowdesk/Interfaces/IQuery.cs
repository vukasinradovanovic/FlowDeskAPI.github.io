using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Flowdesk.Interfaces
{
    public interface IQuery<TParam, TResponse> : IPermission
        where TResponse : class
    {
        TResponse Execute(TParam request);
    }
}
