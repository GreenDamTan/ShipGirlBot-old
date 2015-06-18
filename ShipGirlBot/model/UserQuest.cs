using System;
using System.Collections.Generic;

public class UserQuest
{
    public Dictionary<string, int> award;
    public QuestCondition[] condition;
    public long createTime;
    public string desc;
    public int fleetId;
    public int iconType;
    public int[] link;
    public int status;
    public int[] systemOpen;
    public int taskCid;
    public string title;
    public QuestType type;
    public int userLevel;

    public int GetItemAmount(int id)
    {
        if ((this.award != null) && this.award.ContainsKey(string.Empty + id))
        {
            return this.award[string.Empty + id];
        }
        return 0;
    }

    public void UpdateCondition(QuestCondition[] condition)
    {
        this.condition = condition;
    }

    public int AwardEquipId
    {
        get
        {
            if (this.award != null)
            {
                foreach (KeyValuePair<string, int> pair in this.award)
                {
                    int id = int.Parse(pair.Key);
                    if (RewardUtil.IsEquip(id))
                    {
                        return id;
                    }
                }
            }
            return 0;
        }
    }

    public int AwardItemId
    {
        get
        {
            if (this.award != null)
            {
                foreach (KeyValuePair<string, int> pair in this.award)
                {
                    int id = int.Parse(pair.Key);
                    if (RewardUtil.IsItem(id))
                    {
                        return id;
                    }
                }
            }
            return 0;
        }
    }

    public int AwardShipId
    {
        get
        {
            if (this.award != null)
            {
                foreach (KeyValuePair<string, int> pair in this.award)
                {
                    int id = int.Parse(pair.Key);
                    if (RewardUtil.IsShip(id))
                    {
                        return id;
                    }
                }
            }
            return 0;
        }
    }

    public bool IsFinished
    {
        get
        {
            foreach (QuestCondition condition in this.condition)
            {
                if (!condition.IsFinished)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

