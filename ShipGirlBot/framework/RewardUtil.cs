using System;

public class RewardUtil
{
    public static bool IsEquip(int id)
    {
        if (id < 100)
        {
            return false;
        }
        string str = id.ToString();
        return (int.Parse(str.Substring(str.Length - 2)) == 0x15);
    }

    public static bool IsItem(int id)
    {
        if (id < 100)
        {
            return false;
        }
        string str = id.ToString();
        return (int.Parse(str.Substring(str.Length - 2)) == 0x29);
    }

    public static bool IsShip(int id)
    {
        if (id < 100)
        {
            return false;
        }
        string str = id.ToString();
        int num = int.Parse(str.Substring(str.Length - 2));
        return ((((num == 11) || (num == 12)) || (num == 13)) || (num == 14));
    }
}

