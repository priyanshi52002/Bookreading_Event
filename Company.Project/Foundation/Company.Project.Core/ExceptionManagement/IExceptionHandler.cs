using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Project.Core.ExceptionManagement
{
    public interface IExceptionHandler
    {
        Exception Process(Exception exception);
    }
}
