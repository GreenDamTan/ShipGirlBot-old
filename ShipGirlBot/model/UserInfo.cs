using System;

public class UserInfo
{
    public int aluminium;
    public int ammo;
    public int buyCardSlotNum;
    public int buyEquipmentSlotNum;
    public UserDetailInfo detailInfo;
    public int equipmentNumTop;
    public int exp;
    public int exploreNum;
    public int exploreWinNum;
    public int fleetMaxNum;
    public int gold;
    public int level;
    public int loseNum;
    public int material;
    public int nextExp;
    public int oil;
    public int packageSlot;
    public int pveLevelId;
    public int pvpLoseNum;
    public int pvpWinNum;
    public long[] resourceRecoveryTimes;
    public int[] resourcesTops;
    public int shipNumTop;
    public string sign;
    public int steel;
    public int[] systemOpen;
    public int uid;
    public string username;
    public int winNum;

    public void AddAutoProducedResources(int[] nums)
    {
        if (this.oil < this.resourcesTops[0])
        {
            this.oil = Math.Min(this.oil + nums[0], this.resourcesTops[0]);
        }
        if (this.steel < this.resourcesTops[1])
        {
            this.steel = Math.Min(this.steel + nums[1], this.resourcesTops[1]);
        }
        if (this.ammo < this.resourcesTops[2])
        {
            this.ammo = Math.Min(this.ammo + nums[2], this.resourcesTops[2]);
        }
        if (this.aluminium < this.resourcesTops[3])
        {
            this.aluminium = Math.Min(this.aluminium + nums[3], this.resourcesTops[3]);
        }
    }

    public void AddGold(int amount)
    {
        this.gold += amount;
    }

    public void ApplyNewExpInfo(UserExpAndLevelVO expLevelVo)
    {
        //if ((expLevelVo.level > this.level) && AndroidPlatformManager.instance.IsLenjingPlatform)
        //{
        //    LJSDK.Instance.SetLevelUpData();
        //}
        this.level = expLevelVo.level;
        this.exp += expLevelVo.expAdd;
        this.nextExp = expLevelVo.nextLevelExpNeed;
    }

    public UserResInfo GetResourceChange(UserResInfo newInfo)
    {
        return new UserResInfo { oil = newInfo.oil - this.oil, ammo = newInfo.ammo - this.ammo, steel = newInfo.steel - this.steel, aluminium = newInfo.aluminium - this.aluminium, gold = newInfo.gold - this.gold };
    }

    public void UpdateDetailInfo(UserDetailInfo info)
    {
        if (info != null)
        {
            this.detailInfo = info;
        }
    }

    public void UpdateResource(UserInfo newInfo)
    {
        this.oil = newInfo.oil;
        this.steel = newInfo.steel;
        this.ammo = newInfo.ammo;
        this.aluminium = newInfo.aluminium;
        this.gold = newInfo.gold;
    }

    public void UpdateResource(UserResInfo newInfo)
    {
        this.oil = newInfo.oil;
        this.steel = newInfo.steel;
        this.ammo = newInfo.ammo;
        this.aluminium = newInfo.aluminium;
        this.gold = newInfo.gold;
    }
}

