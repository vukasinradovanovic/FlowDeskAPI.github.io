using DataAccess.FlowDesk;
using FluentValidation;

namespace Implementation.Permissions.Validators.Common
{
    public abstract class CommonValidator<T> : AbstractValidator<T>
    {
        protected readonly FlowDbContext _context;

        protected CommonValidator(FlowDbContext context)
        {
            _context = context;
        }
    }
}
