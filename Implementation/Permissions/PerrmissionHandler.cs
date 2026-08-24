using Application;
using Application.Exceptions;
using Application.Flowdesk.Interfaces;
using DataAccess.FlowDesk;
using Domain.Identity;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Implementation.Permissions

{
    public class PerrmissionHandler
    {
        private IApplicationUser _user;
        private FlowDbContext _context;

        public PerrmissionHandler(IApplicationUser user, FlowDbContext context)
        {
            _user = user;
            _context = context;
        }

        public void HandleAuthorization(IPermission permissions)
        {
            if (!_user.Permissions.Contains(permissions.Name))
            {
                throw new UnauthorizedPermissionException(_user.Id, _user.Username, _user.FirstName, _user.LastName, _user.Email, permissions.Name);
            }
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            UseCaseLog log = new UseCaseLog
            {
                Username = _user.Username,
                UseCaseName = command.Name,
                UseCaseData = JsonConvert.SerializeObject(request),

            };

            try
            {
                HandleAuthorization(command);

                stopwatch.Start();

                command.Execute(request);

                stopwatch.Stop();
                log.IsSuccessfull = true;
                _context.UseCaseLogs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                log.IsSuccessfull = false;
                _context.UseCaseLogs.Add(log);
                _context.SaveChanges();
                stopwatch.Stop();
                throw;
            }


            Console.WriteLine($"{_user.Username} has executed use case: {command.Name}" + stopwatch.ElapsedMilliseconds + " ms.");
        }

        public TResult ExecuteQuery<TParam, TResult>(IQuery<TParam, TResult> query, TParam request)
            where TResult : class
        {
            HandleAuthorization(query);
            Stopwatch stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            var result = query.Execute(request);

            stopwatch.Stop();

            Console.WriteLine($"{_user.Id} has executed use case: {query.Name}" + stopwatch.ElapsedMilliseconds + " ms.");

            return result;
        }
    }
}
