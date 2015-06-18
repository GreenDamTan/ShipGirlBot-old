using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
////using UnityEngine;

public class GameConfigs : MonoBehaviour
{
    [CompilerGenerated]
    private static Func<BuffConfig, int> f_am_cache10;
    [CompilerGenerated]
    private static Func<BuffConfig, BuffConfig> f_am_cache11;
    [CompilerGenerated]
    private static Func<KeyValuePair<int, PVEExploreLevelConfig>, PVEExploreLevelConfig> f_am_cacheB;
    [CompilerGenerated]
    private static Func<ShopItemConfig, int> f_am_cacheC;
    [CompilerGenerated]
    private static Func<ShopItemConfig, ShopItemConfig> f_am_cacheD;
    [CompilerGenerated]
    private static Func<SkillConfig, int> f_am_cacheE;
    [CompilerGenerated]
    private static Func<SkillConfig, SkillConfig> f_am_cacheF;
    private Dictionary<int, EquipmentConfig> allEquipConfigs;
    private Dictionary<int, PVEExploreLevelConfig> allExploreLevels;
    private List<PVEExploreLevelConfig> allExploreLevelsList;
    private Dictionary<int, BuffConfig> buffConfigsDic;
    private GameGlobalConfig globalConfig;
    public MarketingConfig marketingConfigs;
    private static GameConfigs mInstance;
    private List<ShipEvoItemConfig> shipEvoItemsList;
    private Dictionary<int, ShopItemConfig> shopItems;
    private List<ShopItemConfig> shopItemsList;
    private Dictionary<int, SkillConfig> skillConfigsDic;

    public Dictionary<string, string> errCode;


    public string GetErrorString(int eid)
    {
        if ((this.errCode != null) && this.errCode.ContainsKey(eid.ToString()))
        {
            return this.errCode[eid.ToString()];
        }
        return "未知错误"+ eid.ToString();
    }
    public BuffConfig GetBuffConfig(int cid)
    {
        if ((this.buffConfigsDic != null) && this.buffConfigsDic.ContainsKey(cid))
        {
            return this.buffConfigsDic[cid];
        }
        return null;
    }

    public EquipmentConfig GetEquipmentByCid(int cid)
    {
        if (this.allEquipConfigs.ContainsKey(cid))
        {
            return this.allEquipConfigs[cid];
        }
        return null;
    }

    public PVEExploreLevelConfig GetPVEExploreLevel(int id)
    {
        if (this.allExploreLevels.ContainsKey(id))
        {
            return this.allExploreLevels[id];
        }
        return null;
    }

    public Dictionary<int, PVEExploreLevelConfig> getallExploreLevels()
    {
        return allExploreLevels;
    }

    public List<PVEExploreLevelConfig> GetPVEExploreLevelsOfChapter(int pveId)
    {
        GetPVEExploreLevelsOfChapter_c__AnonStorey75 storey = new GetPVEExploreLevelsOfChapter_c__AnonStorey75 {
            pveId = pveId
        };
        if (f_am_cacheB == null)
        {
            f_am_cacheB = n => n.Value;
        }
        return this.allExploreLevels.Select<KeyValuePair<int, PVEExploreLevelConfig>, PVEExploreLevelConfig>(f_am_cacheB).Where<PVEExploreLevelConfig>(new Func<PVEExploreLevelConfig, bool>(storey.m_3F)).ToList<PVEExploreLevelConfig>();
    }

    public ShipEvoItemConfig GetShipEvoItemConfig(int cid)
    {
        GetShipEvoItemConfig_c__AnonStorey77 storey = new GetShipEvoItemConfig_c__AnonStorey77 {
            cid = cid
        };
        return this.shipEvoItemsList.Find(new Predicate<ShipEvoItemConfig>(storey.m_43));
    }

    public ShopItemConfig GetShopItem(int id)
    {
        if (this.shopItems.ContainsKey(id))
        {
            return this.shopItems[id];
        }
        return null;
    }

    public List<ShopItemConfig> GetShopItemsOfType(int type)
    {
        GetShopItemsOfType_c__AnonStorey76 storey = new GetShopItemsOfType_c__AnonStorey76
        {
            type = type
        };
        return this.shopItemsList.FindAll(new Predicate<ShopItemConfig>(storey.m_42));
    }

    public SkillConfig GetSkillConfig(int id)
    {
        if ((this.skillConfigsDic != null) && this.skillConfigsDic.ContainsKey(id))
        {
            return this.skillConfigsDic[id];
        }
        return null;
    }

    public void PrepareEquipmentConfigs(EquipmentConfig[] configs)
    {
        this.allEquipConfigs = new Dictionary<int, EquipmentConfig>();
        foreach (EquipmentConfig config in configs)
        {
            this.allEquipConfigs[config.cid] = config;
        }
    }

    public void PreparePVEExploreLevels(PVEExploreLevelConfig[] levels)
    {
        if ((levels != null) && (levels.Length > 0))
        {
            this.allExploreLevelsList = levels.ToList<PVEExploreLevelConfig>();
            this.allExploreLevels = new Dictionary<int, PVEExploreLevelConfig>();
            foreach (PVEExploreLevelConfig config in levels)
            {
                this.allExploreLevels[config.id] = config;
            }
        }
    }

    public void SetBuffConfigs(BuffConfig[] configs)
    {
        if (configs == null)
        {
            this.buffConfigsDic = new Dictionary<int, BuffConfig>();
        }
        else
        {
            if (f_am_cache10 == null)
            {
                f_am_cache10 = n => n.cid;
            }
            if (f_am_cache11 == null)
            {
                f_am_cache11 = n => n;
            }
            this.buffConfigsDic = configs.ToDictionary<BuffConfig, int, BuffConfig>(f_am_cache10, f_am_cache11);
        }
    }

    public void SetShipEvoItems(ShipEvoItemConfig[] items)
    {
        if (items == null)
        {
            this.shipEvoItemsList = new List<ShipEvoItemConfig>();
        }
        else
        {
            this.shipEvoItemsList = items.ToList<ShipEvoItemConfig>();
        }
    }

    public void SetShopItems(ShopItemConfig[] items)
    {
        if (items == null)
        {
            this.shopItemsList = new List<ShopItemConfig>();
            this.shopItems = new Dictionary<int, ShopItemConfig>();
        }
        else
        {
            this.shopItemsList = items.ToList<ShopItemConfig>();
            if (f_am_cacheC == null)
            {
                f_am_cacheC = n => n.id;
            }
            if (f_am_cacheD == null)
            {
                f_am_cacheD = n => n;
            }
            this.shopItems = this.shopItemsList.ToDictionary<ShopItemConfig, int, ShopItemConfig>(f_am_cacheC, f_am_cacheD);
        }
    }

    public void SetSkillConfigs(SkillConfig[] configs)
    {
        if (configs == null)
        {
            this.skillConfigsDic = new Dictionary<int, SkillConfig>();
        }
        else
        {
            if (f_am_cacheE == null)
            {
                f_am_cacheE = n => n.skillId;
            }
            if (f_am_cacheF == null)
            {
                f_am_cacheF = n => n;
            }
            this.skillConfigsDic = configs.ToDictionary<SkillConfig, int, SkillConfig>(f_am_cacheE, f_am_cacheF);
        }
    }


    public PVEExploreLevelConfig FirstPVEExploreLevel
    {
        get
        {
            if ((this.allExploreLevelsList != null) && (this.allExploreLevelsList.Count > 0))
            {
                return this.allExploreLevelsList[0];
            }
            return null;
        }
    }

    public GameGlobalConfig GlobalConfig
    {
        get
        {
            return this.globalConfig;
        }
        set
        {
            this.globalConfig = value;
        }
    }

    public Dictionary<int, EquipmentConfig> AllEquipmentConfig
    {
        get
        {
            return this.allEquipConfigs;
        }

    }

    public static GameConfigs instance
    {
        get
        {
            //mInstance = UnityEngine.Object.FindObjectOfType(typeof(GameConfigs)) as GameConfigs;
            if (mInstance == null)
            {
                //mInstance = new GameObject { name = typeof(GameConfigs).ToString() }.AddComponent<GameConfigs>();
                mInstance = new GameConfigs();
            }
            return mInstance;
        }
    }

    public MarketingConfig MarketingConfigs
    {
        get
        {
            return this.marketingConfigs;
        }
        set
        {
            this.marketingConfigs = value;
        }
    }

    public List<ShipEvoItemConfig> ShipEvoItemsConfigList
    {
        get
        {
            return this.shipEvoItemsList;
        }
    }

    [CompilerGenerated]
    private sealed class GetPVEExploreLevelsOfChapter_c__AnonStorey75
    {
        internal int pveId;

        internal bool m_3F(PVEExploreLevelConfig n1)
        {
            return (n1.pveId == this.pveId);
        }
    }

    [CompilerGenerated]
    private sealed class GetShipEvoItemConfig_c__AnonStorey77
    {
        internal int cid;

        internal bool m_43(ShipEvoItemConfig n)
        {
            return (n.cid == this.cid);
        }
    }

    [CompilerGenerated]
    private sealed class GetShopItemsOfType_c__AnonStorey76
    {
        internal int type;

        internal bool m_42(ShopItemConfig n)
        {
            return (n.type == this.type);
        }
    }
}

