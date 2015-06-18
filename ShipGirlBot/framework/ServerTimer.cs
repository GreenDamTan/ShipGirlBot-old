using System;

public class ServerTimer
{
    public static long localInitTime;
    public static long systemInitTime;

    public static long GetNowServerTime()
    {
        long totalSeconds = (int) TimeSpan.FromTicks(DateTime.Now.Ticks - localInitTime).TotalSeconds;
        return (systemInitTime + totalSeconds);
    }

    public static long GetTimeLeftTo(long endTime)
    {
        long nowServerTime = GetNowServerTime();
        long num2 = endTime - nowServerTime;
        return Math.Max(num2, 0L);
    }

    public static long GetTimeLeftTo(long startTime, long endTime)
    {
        long num2 = GetNowServerTime() - startTime;
        long num3 = endTime - startTime;
        return Math.Max((long) (num3 - num2), (long) 0L);
    }

    public static void SetSystemInitTime(long value)
    {
        systemInitTime = value;
        localInitTime = DateTime.Now.Ticks;
    }
}

