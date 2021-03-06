namespace Horizon.Logging;
public class Logger
{
    private readonly string label;

    protected ILogDestination Destination { get; set; }

    public Logger(ILogDestination dest, string label = "")
    {
        Destination = dest;
        this.label = label;
    }

    public Logger Label(string label)
    {
        return new Logger(new LoggerLogDestination(this), label);
    }

    public void Log(string message, LogLevel level)
    {
        LogMessage(new LogMessage
        {
            Level = level,
            Message = message,
            Label = label
        });
    }
    public void LogMessage(LogMessage message)
    {
        Destination?.Log(message);
    }

    public void Exception(Exception ex)
    {
        string message = $"Caught unhandled exception of type {ex.GetType()}, terminating.";
        Log(message, LogLevel.Crit);
        throw new CriticalException(message, ex);
    }
    public void Crit(string message)
    {
        Log(message, LogLevel.Crit);
        throw new CriticalException(message);
    }
    public void Error(string message) => Log(message, LogLevel.Error);
    public void Warn(string message) => Log(message, LogLevel.Warn);
    public void Alert(string message) => Log(message, LogLevel.Alert);
    public void Info(string message) => Log(message, LogLevel.Info);
    public void Debug(string message) => Log(message, LogLevel.Debug);
}

public class CriticalException : Exception
{
    public CriticalException(): base() { }
    public CriticalException(string message) : base(message) { }
    public CriticalException(string message, Exception inner) : base(message, inner) { }
}

public enum LogLevel
{
    Crit,
    Error,
    Warn,
    Alert,
    Info,
    Debug
}
