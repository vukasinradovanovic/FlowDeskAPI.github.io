using System;

namespace FlowDesk.API.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiKeyAuthorizationAttribute : Attribute
    {
    }
}
