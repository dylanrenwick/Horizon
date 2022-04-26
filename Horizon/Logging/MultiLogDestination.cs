namespace Horizon.Logging;
public class MultiLogDestination : ILogDestination
{
    private readonly List<ILogDestination> destinations = new();

    public MultiLogDestination(params ILogDestination[] destinations)
    {
        this.destinations.AddRange(destinations);
    }

    public void Log(LogMessage message)
    {
        foreach (var destination in destinations)
        {
            destination.Log(message);
        }
    }
}
