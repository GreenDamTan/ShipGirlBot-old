using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ReqSupply : BaseWWWRequest
{
    public event EventHandler<EventArgs> SupplyAllFail;

    public event EventHandler<EventArgs> SupplyAllSuccess;

    public event EventHandler<EventArgs> SupplyMultiFail;

    public event EventHandler<EventArgs> SupplyMultiSuccess;

    public event EventHandler<EventArgs> SupplyOneFleetFail;

    public event EventHandler<EventArgs> SupplyOneFleetSuccess;

    public event EventHandler<EventArgs> SupplyOneShipFail;

    public event EventHandler<EventArgs> SupplyOneShipSuccess;

    private void onFleetFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSupplyOneFleetFail(EventArgs.Empty);
    }

    private void onFleetSuccess(BaseWWWRequest obj)
    {
        try
        {
            SupplyOneFleetData data = new JsonFx.Json.JsonReader().Read<SupplyOneFleetData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onFleetFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData instance = GameData.instance;
                    foreach (UserShip ship in data.shipVO)
                    {
                        instance.UpdateUserShip(ship);
                    }
                }
                if (data.userVo != null)
                {
                    var ui = GameData.instance.UserInfo;
                    z.log("[补给舰队成功] 少女吃喝完毕... 消耗 "
                        + " 油 - " + (ui.oil - data.userVo.oil)
                        + " 弹 - " + (ui.ammo - data.userVo.ammo)
                        + " 钢 - " + (ui.steel - data.userVo.steel)
                        + " 铝 - " + (ui.aluminium - data.userVo.aluminium));
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnSupplyOneFleetSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onFleetFail(obj);
        }
    }

    private void onOneShipFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSupplyOneShipFail(EventArgs.Empty);
    }

    private void onOneShipSuccess(BaseWWWRequest obj)
    {
        try
        {
            SupplyOneShipData data = new JsonFx.Json.JsonReader().Read<SupplyOneShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onOneShipFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(data.shipVO);
                }
                if (data.userVo != null)
                {
                    var ui = GameData.instance.UserInfo;
                    z.log("[补给船只成功] 少女吃喝完毕... 消耗 "
                            + " 油 - " + (ui.oil - data.userVo.oil)
                            + " 弹 - " + (ui.ammo - data.userVo.ammo)
                            + " 钢 - " + (ui.steel - data.userVo.steel)
                            + " 铝 - " + (ui.aluminium - data.userVo.aluminium)
                            );
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnSupplyOneShipSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onOneShipFail(obj);
        }
    }

    private void onSAllFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSupplyAllFail(EventArgs.Empty);
    }

    private void onSAllSuccess(BaseWWWRequest obj)
    {
        try
        {
            SupplyAllShipData data = new JsonFx.Json.JsonReader().Read<SupplyAllShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onSAllFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData instance = GameData.instance;
                    foreach (UserShip ship in data.shipVO)
                    {
                        instance.UpdateUserShip(ship);
                    }
                }
                if (data.userVo != null)
                {
                    var ui = GameData.instance.UserInfo;
                    z.log("[补给全部舰艇成功] 少女吃喝完毕... 消耗 "
                            + " 油 - " + (ui.oil - data.userVo.oil)
                            + " 弹 - " + (ui.ammo - data.userVo.ammo)
                            + " 钢 - " + (ui.steel - data.userVo.steel)
                            + " 铝 - " + (ui.aluminium - data.userVo.aluminium)
                            );
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnSupplyAllSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onSAllFail(obj);
        }
    }

    private void onSMultiFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSupplyMultiFail(EventArgs.Empty);
    }

    private void onSMultiSuccess(BaseWWWRequest obj)
    {
        try
        {

            SupplyAllShipData data = new JsonFx.Json.JsonReader().Read<SupplyAllShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onSMultiFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData instance = GameData.instance;
                    foreach (UserShip ship in data.shipVO)
                    {
                        instance.UpdateUserShip(ship);
                    }
                }
                if (data.userVo != null)
                {
                    var ui = GameData.instance.UserInfo;
                    z.log("[补给舰队成功] 少女吃喝完毕... 消耗 "
                            + " 油 - " + (ui.oil - data.userVo.oil)
                            + " 弹 - " + (ui.ammo - data.userVo.ammo)
                            + " 钢 - " + (ui.steel - data.userVo.steel)
                            + " 铝 - " + (ui.aluminium - data.userVo.aluminium)
                                );
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                this.OnSupplyMultiSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onSMultiFail(obj);
        }
    }

    protected virtual void OnSupplyAllFail(EventArgs e)
    {
        if (this.SupplyAllFail != null)
        {
            this.SupplyAllFail(this, e);
        }
    }

    protected virtual void OnSupplyAllSuccess(EventArgs e)
    {
        if (this.SupplyAllSuccess != null)
        {
            this.SupplyAllSuccess(this, e);
        }
    }

    protected virtual void OnSupplyMultiFail(EventArgs e)
    {
        if (this.SupplyMultiFail != null)
        {
            this.SupplyMultiFail(this, e);
        }
    }

    protected virtual void OnSupplyMultiSuccess(EventArgs e)
    {
        if (this.SupplyMultiSuccess != null)
        {
            this.SupplyMultiSuccess(this, e);
        }
    }

    protected virtual void OnSupplyOneFleetFail(EventArgs e)
    {
        if (this.SupplyOneFleetFail != null)
        {
            this.SupplyOneFleetFail(this, e);
        }
    }

    protected virtual void OnSupplyOneFleetSuccess(EventArgs e)
    {
        if (this.SupplyOneFleetSuccess != null)
        {
            this.SupplyOneFleetSuccess(this, e);
        }
    }

    protected virtual void OnSupplyOneShipFail(EventArgs e)
    {
        if (this.SupplyOneShipFail != null)
        {
            this.SupplyOneShipFail(this, e);
        }
    }

    protected virtual void OnSupplyOneShipSuccess(EventArgs e)
    {
        if (this.SupplyOneShipSuccess != null)
        {
            this.SupplyOneShipSuccess(this, e);
        }
    }

    private void Start()
    {
    }

    public void SupplyAll(int useGold)
    {
        base.path = "boat/supplyAll/" + useGold;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSAllSuccess), new BaseWWWRequest.OnFail(this.onSAllFail), true, ServerType.ChoosedServer, false);
    }

    public void SupplyFleet(int fleetId, int useGold)
    {
        object[] objArray1 = new object[] { "boat/supplyFleet/", fleetId, "/", useGold };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onFleetSuccess), new BaseWWWRequest.OnFail(this.onFleetFail), true, ServerType.ChoosedServer, false);
    }

    public void SupplyMulti(List<UserShip> ships)
    {
        string str = "[";
        for (int i = 0; i < ships.Count; i++)
        {
            UserShip ship = ships[i];
            if (ship != null)
            {
                str = str + ship.id + string.Empty;
            }
            else
            {
                str = str + "0";
            }
            if (i != (ships.Count - 1))
            {
                str = str + ",";
            }
        }
        str = str + "]";
        base.path = "boat/supplyBoats/" + str;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSMultiSuccess), new BaseWWWRequest.OnFail(this.onSMultiFail), true, ServerType.ChoosedServer, false);
    }

    public void SupplyShip(int shipId, int useGold)
    {
        object[] objArray1 = new object[] { "boat/supply/", shipId, "/", useGold };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onOneShipSuccess), new BaseWWWRequest.OnFail(this.onOneShipFail), true, ServerType.ChoosedServer, false);
    }
}

