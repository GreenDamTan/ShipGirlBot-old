using System;
using System.Collections.Generic;

public class PVECampaignLevel
{
    public int campaignId;
    public string desc;
    public int difficulty;
    public int[] fleetRule;
    public int fleetType;
    public int id;
    public string itemDropDesc;
    public int mapId;
    public int needCampaignLevelId;
    public int needPveLevelId;
    public int npcFromationId;
    public Dictionary<string, int> productAward;
    public int productItem;
    public string title;

    public int GetRuleAt(int index)
    {
        int num = 0;
        if ((this.fleetRule != null) && (this.fleetRule.Length > index))
        {
            num = this.fleetRule[index];
        }
        return num;
    }

    public int[] fleetRuleRe
    {
        get
        {
            if ((this.fleetRule != null) && (this.fleetRule.Length >= 1))
            {
                return this.fleetRule;
            }
            return new int[6];
        }
    }
}

