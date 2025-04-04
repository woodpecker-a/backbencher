namespace STSC.Infrastructure.Services.Utilities;

public class TimeService : ITimeService
{
    public DateTime Now
    {
        get => DateTime.UtcNow;
    }
}
