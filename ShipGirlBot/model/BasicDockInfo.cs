using System;

public class BasicDockInfo
{
    public long endTime;
    public int id;
    public int locked;
    public long startTime;

    public bool IsFinished
    {
        get
        {
            return (((this.locked <= 0) && (this.startTime != 0)) && (ServerTimer.GetTimeLeftTo(this.startTime, this.endTime) <= 0L));
        }
    }

    public int secondsLeft
    {
        get
        {
            return (int) ServerTimer.GetTimeLeftTo(this.startTime, this.endTime);
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

