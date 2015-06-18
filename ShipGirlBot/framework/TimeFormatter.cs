using System;

public class TimeFormatter
{
    public static string Get6DFormat(int timeInSeconds)
    {
        int num = timeInSeconds / 0xe10;
        int num2 = (timeInSeconds - (0xe10 * num)) / 60;
        int num3 = (timeInSeconds - (0xe10 * num)) - (60 * num2);
        return string.Format("{0:00}:{1:00}:{2:00}", num, num2, num3);
    }

    public static string Get8DTimeFormat(long timeStamp)
    {
        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return time.AddSeconds((double) timeStamp).ToString("MM-dd hh:mm");
    }

    public static string Get6DTimeFormat(long timeStamp)
    {
        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return time.AddSeconds((double)timeStamp).ToString("hh:mm:ss");
    }
    public static string GetDate(long timeStamp)
    {
        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return time.AddSeconds((double) timeStamp).ToString("yyyy-MM-dd");
    }

    public static string GetTimeFormatAgo(long timeStamp)
    {
        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        time = time.AddSeconds((double) timeStamp);
        TimeSpan span = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - time.Ticks);
        TimeSpan span2 = TimeSpan.FromHours(1.0);
        if (span < span2)
        {
            return string.Format("{1}分", span.Minutes);
        }
        TimeSpan span3 = TimeSpan.FromDays(1.0);
        if (span < span3)
        {
            return string.Format("{1}小时", span.Hours);
        }
        return time.ToLocalTime().ToString("MM-dd hh:mm");
    }

    public static string GetETAString(long delta)
    {
        int h = (int) delta / 3600;
        int m = (int) delta % 3600 / 60;
        int s = (int) delta % 60;
        if (h < 1)
        {
            if(m <1)
            {
                return string.Format("{0}秒", s);
            }else{
                return string.Format("{0}分{1}秒", m,s );
            }

        }
        else 
        {
            return string.Format("{0}小时{1}分{2}秒", h, m ,s);
        }
    }
}

