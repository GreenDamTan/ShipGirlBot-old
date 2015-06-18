using System;

public class PVELevel
{
    public int bossId;
    public string desc;
    public int difficulty;
    public int hp;
    public int id;
    public int initNodeId;
    public int isRedPath;
    public int mapId;
    public int needPveLevelId;
    public int needUserLevel;
    public int nodeNum;
    public int pveId;
    public string subTitle;
    public string tips;
    public string title;
    public PVELevelType type;

    public bool IsBossLevel
    {
        get
        {
            return (this.type == PVELevelType.boss);
        }
    }

    public bool IsInProgress
    {
        get
        {
            return (GameData.instance.CurrentPVE.pveLevelId == this.id);
        }
    }

    public bool IsLockedByPreLevel
    {
        get
        {
            if (this.needPveLevelId <= 0)
            {
                return false;
            }
            return (!GameData.instance.IsPVELevelPassed(this.needPveLevelId) && !this.IsInProgress);
        }
    }

    public bool IsLockedByUserlevel
    {
        get
        {
            return (GameData.instance.UserInfo.level < this.needUserLevel);
        }
    }

    public bool IsPassed
    {
        get
        {
            return GameData.instance.IsPVELevelPassed(this.id);
        }
    }

    public bool IsUnLocked
    {
        get
        {
            return (!this.IsLockedByUserlevel && !this.IsLockedByPreLevel);
        }
    }

    public string NeedPVELevelTitle
    {
        get
        {
            PVELevel level = PVEConfigs.instance.GetLevel(this.needPveLevelId);
            if (level != null)
            {
                return level.title;
            }
            return (this.needPveLevelId + string.Empty);
        }
    }

    public bool UseRedPath
    {
        get
        {
            return (this.isRedPath == 1);
        }
    }
}

