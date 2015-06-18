using System.Collections.Generic;
internal class AllConfigs : BasicResponse
{
    public GameGlobalConfig globalConfig = null;
    public MarketingConfig marketingConfigs = null;
    public PVEExploreLevelConfig[] pveExplore = null;
    public PVECampaignChapter[] shipCampaign = null;
    public PVECampaignLevel[] shipCampaignLevel = null;
    public ShipConfig[] shipCard = null;
    public EquipmentConfig[] shipEquipment = null;
    public ShipEvoItemConfig[] shipItem = null;
    public ShopItemConfig[] shipShop = null;
    public SkillConfig[] shipSkill = null;
    public BuffConfig[] shipSkillBuff = null;
    public Dictionary<string, string> errorCode = new Dictionary<string, string>();

}

