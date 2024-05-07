using Serilog;

namespace AFCStudioEmployees.Application.Exceptions;

public class LoggedFatalException : Exception
{
    public LoggedFatalException(string message) : base(message)
    {
        Log.Fatal(message);
    }
}