using JsonFx.Json;
using System;

public class LoadConfigs : BaseWWWRequest
{
    public bool needZip = true;
    public string shipConfigs;
    public bool useLocal = false;
    public string shipUpdateconfigs;
    public bool testShipUpdate;

    private void LoadShipConfigs()
    {
        if (this.shipConfigs != null)
        {
            AllConfigs configs = new JsonFx.Json.JsonReader().Read<AllConfigs>(this.shipConfigs);
            AllShipConfigs.instance.setShips(configs.shipCard);
            ServerRequestManager.instance.OnLoadConfigsComplete();
        }
    }

    private void onFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        ServerRequestManager.instance.OnLoadConfigsError(obj.UTF8String);
    }

    private void onSuccess(BaseWWWRequest obj)
    {
        try
        {
            AllConfigs configs;
            if (this.needZip)
            {
                configs = new JsonFx.Json.JsonReader().Read<AllConfigs>(base.UTF8String);
            }
            else
            {
                configs = new JsonFx.Json.JsonReader().Read<AllConfigs>(this.UTF8String);
            }
            if (configs.eid != 0)
            {
                this.onFail(obj);
            }
            else
            {
                z.log("setShips");
                AllShipConfigs.instance.setShips(configs.shipCard);
                z.log("shipCard");
                GameConfigs.instance.PrepareEquipmentConfigs(configs.shipEquipment);
                z.log("PrepareEquipmentConfigs");
                GameConfigs.instance.PreparePVEExploreLevels(configs.pveExplore);
                z.log("PreparePVEExploreLevels");
                GameConfigs.instance.SetShopItems(configs.shipShop);
                GameConfigs.instance.SetShipEvoItems(configs.shipItem);
                GameConfigs.instance.SetSkillConfigs(configs.shipSkill);
                z.log("SetSkillConfigs");
                GameConfigs.instance.SetBuffConfigs(configs.shipSkillBuff);
                GameConfigs.instance.GlobalConfig = configs.globalConfig;
                GameConfigs.instance.marketingConfigs = configs.marketingConfigs;
                z.log("marketingConfigs");
                PVEConfigs.instance.SetCampaignChapters(configs.shipCampaign);
                PVEConfigs.instance.SetCampaignLevels(configs.shipCampaignLevel);
                GameConfigs.instance.errCode = configs.errorCode;

                //if (this.testShipUpdate && (this.shipUpdateconfigs != null))
                //{
                //    UpdateConfigs configs2 = JsonReader.Deserialize<UpdateConfigs>(this.shipUpdateconfigs.text);
                //    UpdateManager.Instance.SetShips(configs2.ships);
                //}

                ServerRequestManager.instance.OnLoadConfigsComplete();
            }
        }
        catch (Exception e)
        {
            z.log(e.Message+ "  " + e.StackTrace);
            this.onFail(obj);
        }
    }

    public override void Start()
    {
        if (this.useLocal)
        {
            this.LoadShipConfigs();
        }
        else
        {
            if (string.IsNullOrEmpty(base.path))
            {
                base.path = "index/getInitConfigs/";
            }
            base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.LoginServer, this.needZip);
        }
    }
}

