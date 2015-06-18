using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;
public class RepairRequest : BaseWWWRequest
{
    private int getingShipDockId;

    public event EventHandler<EventArgs> GetShipFail;

    public event EventHandler<EventArgs> GetShipSuccess;

    public event EventHandler<EventArgs> InstanceRepairSuccess;

    public event EventHandler<EventArgs> InstantRepairFail;

    public event EventHandler<EventArgs> StartRepairShipFail;

    public event EventHandler<EventArgs> StartRepairShipSuccess;

   
    public void GetRepairedShip(int shipId, int dockId)
    {
        this.getingShipDockId = dockId;
        object[] objArray1 = new object[] { "boat/repairComplete/", dockId, "/", shipId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.reqGetShipSuccess), new BaseWWWRequest.OnFail(this.reqGetShipFail), true, ServerType.ChoosedServer, false);
    }

    private void instanceReqSuccess(BaseWWWRequest obj)
    {
        try
        {
            InstantRepairData data = new JsonFx.Json.JsonReader().Read<InstantRepairData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.instantReqFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(data.shipVO);
                }
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                if (data.repairDockVo != null)
                {
                    GameData.instance.SetRepairDocks(data.repairDockVo);
                }
                if (data.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(data.packageVo);
                }
                this.OnInstanceRepairSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.instantReqFail(obj);
        }
    }

    public void InstantRepairShip(int shipId, int useGold)
    {
        object[] objArray1 = new object[] { "boat/instantRepair/", shipId, "/", useGold };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.instanceReqSuccess), new BaseWWWRequest.OnFail(this.instantReqFail), true, ServerType.ChoosedServer, false);
    }

    private void instantReqFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnInstantRepairFail(EventArgs.Empty);
    }

    protected virtual void OnGetShipFail(EventArgs e)
    {
        if (this.GetShipFail != null)
        {
            this.GetShipFail(this.getingShipDockId, e);
        }
    }

    protected virtual void OnGetShipSuccess(EventArgs e)
    {
        if (this.GetShipSuccess != null)
        {
            this.GetShipSuccess(this.getingShipDockId, e);
        }
    }

    protected virtual void OnInstanceRepairSuccess(EventArgs e)
    {
        if (this.InstanceRepairSuccess != null)
        {
            this.InstanceRepairSuccess(this, e);
        }
    }

    protected virtual void OnInstantRepairFail(EventArgs e)
    {
        if (this.InstantRepairFail != null)
        {
            this.InstantRepairFail(this, e);
        }
    }

    protected virtual void OnStartRepairShipFail(EventArgs e)
    {
        if (this.StartRepairShipFail != null)
        {
            this.StartRepairShipFail(this, e);
        }
    }

    protected virtual void OnStartRepairShipSuccess(EventArgs e)
    {
        if (this.StartRepairShipSuccess != null)
        {
            this.StartRepairShipSuccess(this, e);
        }
    }

    private void reqGetShipFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetShipFail(EventArgs.Empty);
        z.log("[捞取治(he)疗(xie)中少女失败] ... " + obj.UTF8String);
    }

    private void reqGetShipSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetRepairData data = new JsonFx.Json.JsonReader().Read<GetRepairData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.reqGetShipFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(data.shipVO);
                }
                if (data.repairDockVo != null)
                {
                    GameData.instance.SetRepairDocks(data.repairDockVo);
                }
                z.log("[治(he)疗(xie)少女完毕] ... " + data.shipVO.ship.title + " Lv." + data.shipVO.level);
                this.OnGetShipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.reqGetShipFail(obj);
        }
    }

    private void startRepairFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnStartRepairShipFail(EventArgs.Empty);
    }

    public void StartRepairShip(int shipId, int dockId)
    {
        object[] objArray1 = new object[] { "boat/repair/", shipId, "/", dockId };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.startRepairSuccess), new BaseWWWRequest.OnFail(this.startRepairFail), true, ServerType.ChoosedServer, false);
    }

    private void startRepairSuccess(BaseWWWRequest obj)
    {
        try
        {
            StartRepairData data = new JsonFx.Json.JsonReader().Read<StartRepairData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.startRepairFail(obj);
                z.log("[少女开始泡澡失败] 请使用正确脱衣姿势 "); 
            }
            else
            {
                UserInfo ui = GameData.instance.UserInfo;
                int oilcost = ui.oil - data.userVo.oil;
                int ammocost = ui.ammo - data.userVo.ammo;
                int steelcost = ui.steel - data.userVo.steel;
                int aluminiumcost = ui.aluminium - data.userVo.aluminium;
                z.log("[少女成功开始泡澡] 实际资源消耗: " 
                    + (oilcost >0 ?("油 - " + oilcost):"")
                    + (ammocost > 0 ? ("弹 - " + ammocost) : "")
                    + (steelcost > 0 ? ("钢 - " + steelcost) : "")
                    + (aluminiumcost > 0 ? ("铝 - " + aluminiumcost) : "")
                    );
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                if (data.repairDockVo != null)
                {
                    GameData.instance.SetRepairDocks(data.repairDockVo);
                }
                this.OnStartRepairShipSuccess(EventArgs.Empty);
                
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.startRepairFail(obj);
        }
    }
}

