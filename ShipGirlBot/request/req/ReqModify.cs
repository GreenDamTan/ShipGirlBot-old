using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;
//using UnityEngine;

public class ReqModify : BaseWWWRequest
{
    private ModifyShipData modifyResponse;
    private UserShip toDestroyShip;

    public event EventHandler<EventArgs> ModifyFail;

    public event EventHandler<EventArgs> ModifySuccess;

    public event EventHandler<EventArgs> SellItemFail;

    public event EventHandler<EventArgs> SellItemSuccess;

    public void ModifyShip(UserShip ship)
    {
        this.toDestroyShip = ship;
        base.path = "dock/evolution/" + ship.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqModifySuccess), new BaseWWWRequest.OnFail(this.onReqModifyFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnModifyFail(EventArgs e)
    {
        if (this.ModifyFail != null)
        {
            this.ModifyFail(this, e);
        }
    }

    protected virtual void OnModifySuccess(EventArgs e)
    {
        if (this.ModifySuccess != null)
        {
            this.ModifySuccess(this.modifyResponse.shipVO, e);
        }
    }

    private void onReqModifyFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnModifyFail(EventArgs.Empty);
    }

    private void onReqModifySuccess(BaseWWWRequest obj)
    {
        try
        {
            this.modifyResponse = new JsonFx.Json.JsonReader().Read<ModifyShipData>(this.UTF8String);
            base.responseData = this.modifyResponse;
            if (this.modifyResponse.eid != 0)
            {
                this.onReqModifyFail(obj);
            }
            else
            {
                this.UpdateShips();
                if (this.modifyResponse.userResVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(this.modifyResponse.userResVo);
                }
                if (this.modifyResponse.shipVO != null)
                {
                    foreach (UserShip ship in this.modifyResponse.shipVO)
                    {
                        GameData.instance.AddUserShip(ship);
                    }
                }
                if (this.modifyResponse.equipmentVo != null)
                {
                    foreach (UserEquipment equipment in this.modifyResponse.equipmentVo)
                    {
                        GameData.instance.AddUserEquipmenet(equipment);
                    }
                }
                if (this.modifyResponse.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(this.modifyResponse.packageVo);
                }
                this.OnModifySuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqModifyFail(obj);
        }
    }

    private void onReqSellFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSellItemFail(EventArgs.Empty);
    }

    private void onReqSellSuccess(BaseWWWRequest obj)
    {
        try
        {
            SellModifyItemData data = new JsonFx.Json.JsonReader().Read<SellModifyItemData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqSellFail(obj);
            }
            else
            {
                if (data.userResVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userResVo);
                }
                if (data.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(data.packageVo);
                }
                this.OnSellItemSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqModifyFail(obj);
        }
    }

    protected virtual void OnSellItemFail(EventArgs e)
    {
        if (this.SellItemFail != null)
        {
            this.SellItemFail(this, e);
        }
    }

    protected virtual void OnSellItemSuccess(EventArgs e)
    {
        if (this.SellItemSuccess != null)
        {
            this.SellItemSuccess(this, e);
        }
    }

    public void SellItem(int itemId, int amount)
    {
        object[] objArray1 = new object[] { "dock/dismantleItem/", itemId, "/", amount };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSellSuccess), new BaseWWWRequest.OnFail(this.onReqSellFail), true, ServerType.ChoosedServer, false);
    }

    private void Start()
    {
    }

    private void UpdateShips()
    {
        if (this.toDestroyShip != null)
        {
            GameData.instance.DeleteUserShip(this.toDestroyShip.id);
        }
    }
}

