using System;
using System.Collections.Generic;

public class PVEExploreLevelConfig
{
    public Dictionary<string, int> award;
    public int bigSuccessRate;
    public string desc;
    public int id;
    public int needAmmo;
    public int needFlagShipLevel;
    public int needOil;
    public int needShipNum;
    public TypeAndNum[] needShipType;
    public int needTime;
    public int[] pruductGoods;
    public int pveId;
    public string title;
    public RandAward[] randAward;
    public string TimeNeed
    {
        get
        {
            return TimeFormatter.Get6DFormat(this.needTime);
        }
    }
}

