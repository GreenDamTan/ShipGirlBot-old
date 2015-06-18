using System;

public class UserPVEExplore
{
    public long endTime;
    public int exploreId;
    public int fleetId;
    public long startTime;

    public bool IsFinished
    {
        get
        {
            return (ServerTimer.GetTimeLeftTo(this.startTime, this.endTime) <= 0L);
        }
    }

    public string TimeLeft
    {
        get
        {
            return TimeFormatter.Get6DFormat((int) ServerTimer.GetTimeLeftTo(this.startTime, this.endTime));
        }
    }
}

