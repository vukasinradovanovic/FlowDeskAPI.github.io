using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Flowdesk.Interfaces
{
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest data);
    }
}
