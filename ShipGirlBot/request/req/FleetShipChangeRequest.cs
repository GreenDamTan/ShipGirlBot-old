using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class FleetShipChangeRequest : BaseWWWRequest
{
    private int requestingFleetId;
    private UserShip requestingShip;
    private UpdateFleetReturnData updateData;

    public event EventHandler<EventArgs> ToggleLockFail;

    public event EventHandler<EventArgs> ToggleLockSuccess;

    public event EventHandler<EventArgs> UpdateFleetFail;

    public event EventHandler<EventArgs> UpdateFleetSuccess;

    public event EventHandler<EventArgs> WeddingFail;

    public event EventHandler<EventArgs> WeddingSuccess;

    public void ChangeFleetName(int fleetId, string name)
    {
        string str = WWW.EscapeURL(name);
        object[] objArray1 = new object[] { "boat/changeFleetName/", fleetId, "/", str, "/" };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.ChoosedServer, false);
    }

    public void ChangeShip(int fleetId, int shipId, int index)
    {
        this.requestingFleetId = fleetId;
        if (shipId == 0)
        {
            object[] objArray1 = new object[] { "boat/removeBoat/", fleetId, "/", index };
            base.path = string.Concat(objArray1);
        }
        else
        {
            object[] objArray2 = new object[] { "boat/changeBoat/", fleetId, "/", shipId, "/", index };
            base.path = string.Concat(objArray2);
        }
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onSuccess), new BaseWWWRequest.OnFail(this.onFail), true, ServerType.ChoosedServer, false);
    }

    public void GetWedding(UserShip userShip)
    {
        this.requestingShip = userShip;
        base.path = "boat/marry/" + userShip.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqWeddingSuccess), new BaseWWWRequest.OnFail(this.onReqWeddingFail), true, ServerType.ChoosedServer, false);
    }

    private void onFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnUpdateFleetFail(EventArgs.Empty);
        ServerRequestManager.instance.refreashUIData();
    }

    private void onReqToggleLockFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnToggleLockFail(EventArgs.Empty);
        ServerRequestManager.instance.refreashUIData();
    }

    private void onReqToggleLockSuccess(BaseWWWRequest obj)
    {
        try
        {
            ToggleUserShipLockResponse response = new JsonFx.Json.JsonReader().Read<ToggleUserShipLockResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.onReqToggleLockFail(obj);
            }
            else
            {
                this.requestingShip.isLocked = response.isLocked;
                this.OnToggleLockSuccess(EventArgs.Empty);
            }
            ServerRequestManager.instance.refreashUIData();
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqToggleLockFail(obj);
        }
    }

    private void onReqWeddingFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnWeddingFail(EventArgs.Empty);
        ServerRequestManager.instance.refreashUIData();
    }

    private void onReqWeddingSuccess(BaseWWWRequest obj)
    {
        try
        {
            WeddingResponse response = new JsonFx.Json.JsonReader().Read<WeddingResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.onReqWeddingFail(obj);
            }
            else
            {
                if (response.shipVO != null)
                {
                    foreach (UserShip ship in response.shipVO)
                    {
                        GameData.instance.UpdateUserShip(ship);
                    }
                }
                if (response.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(response.packageVo);
                }
                this.OnWeddingSuccess(EventArgs.Empty);
                ServerRequestManager.instance.refreashUIData();
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqWeddingFail(obj);
        }
    }

    private void onSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.updateData = new JsonFx.Json.JsonReader().Read<UpdateFleetReturnData>(this.UTF8String);
            base.responseData = this.updateData;
            if (this.updateData.eid != 0)
            {
                this.onFail(obj);
            }
            else
            {
                GameData instance = GameData.instance;
                if (this.requestingFleetId != 0)
                {
                    UserFleet fleetOfId = instance.GetFleetOfId(this.requestingFleetId);
                    if ((fleetOfId != null) && (fleetOfId.ships != null))
                    {
                        foreach (int num in fleetOfId.ships)
                        {
                            instance.GetShipById(num).fleetId = 0;
                        }
                    }
                }
                if (this.updateData.fleetVo != null)
                {
                    foreach (UserFleet fleet2 in this.updateData.fleetVo)
                    {
                        instance.UpdateFleet(fleet2);
                        if (fleet2.ships != null)
                        {
                            foreach (int num4 in fleet2.ships)
                            {
                                instance.GetShipById(num4).fleetId = fleet2.id;
                            }
                        }
                    }
                }
                this.OnUpdateFleetSuccess(EventArgs.Empty);
                ServerRequestManager.instance.refreashUIData();
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onFail(obj);
        }
    }

    protected virtual void OnToggleLockFail(EventArgs e)
    {
        if (this.ToggleLockFail != null)
        {
            this.ToggleLockFail(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnToggleLockSuccess(EventArgs e)
    {
        if (this.ToggleLockSuccess != null)
        {
            this.ToggleLockSuccess(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnUpdateFleetFail(EventArgs e)
    {
        if (this.UpdateFleetFail != null)
        {
            if ((this.updateData != null) && (this.updateData.eid != 0))
            {
                this.UpdateFleetFail(this.updateData.eid, e);
            }
            else
            {
                this.UpdateFleetFail(0, e);
            }
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnUpdateFleetSuccess(EventArgs e)
    {
        if (this.UpdateFleetSuccess != null)
        {
            this.UpdateFleetSuccess(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnWeddingFail(EventArgs e)
    {
        if (this.WeddingFail != null)
        {
            this.WeddingFail(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    protected virtual void OnWeddingSuccess(EventArgs e)
    {
        if (this.WeddingSuccess != null)
        {
            this.WeddingSuccess(this, e);
        }
        ServerRequestManager.instance.refreashUIData();
    }

    private void Start()
    {
    }

    public void ToggleLockStatus(UserShip userShip)
    {
        this.requestingShip = userShip;
        base.path = "boat/lock/" + userShip.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqToggleLockSuccess), new BaseWWWRequest.OnFail(this.onReqToggleLockFail), true, ServerType.ChoosedServer, false);
    }
}

