namespace Horizon.Logging;
public interface ILogDestination
{
    public void Log(LogMessage message);
}
