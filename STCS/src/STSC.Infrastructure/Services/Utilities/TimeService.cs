namespace STCS.Infrastructure.Services.Utilities;

public class TimeService : ITimeService
{
    public DateTime Now
    {
        get => DateTime.UtcNow;
    }
}
