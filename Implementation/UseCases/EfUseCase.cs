using DataAccess.FlowDesk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases
{
    public class EfUseCase
    {
        protected readonly FlowDbContext _context;

        protected EfUseCase(FlowDbContext context)
        {
            _context = context;
        }
    }
}
