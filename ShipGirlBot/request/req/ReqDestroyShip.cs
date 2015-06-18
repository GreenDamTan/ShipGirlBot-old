using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEngine;

public class ReqDestroyShip : BaseWWWRequest
{
    private List<UserShip> toDestroyShips;

    public event EventHandler<EventArgs> DestroyFail;

    public event EventHandler<EventArgs> DestroySuccess;

    public void DestroyeShip(List<UserShip> ships, int saveEquip)
    {
        this.toDestroyShips = ships;
        string str = "[";
        for (int i = 0; i < ships.Count; i++)
        {
            str = str + ships[i].id;
            if (i != (ships.Count - 1))
            {
                str = str + ",";
            }
        }
        str = str + "]";
        object[] objArray1 = new object[] { "dock/dismantleBoat/", str, "/", saveEquip };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqChangeSuccess), new BaseWWWRequest.OnFail(this.onReqChangeFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnDestroyFail(EventArgs e)
    {
        if (this.DestroyFail != null)
        {
            this.DestroyFail(this, e);
        }
    }

    protected virtual void OnDestroySuccess(EventArgs e)
    {
        if (this.DestroySuccess != null)
        {
            this.DestroySuccess(this, e);
        }
    }

    private void onReqChangeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnDestroyFail(EventArgs.Empty);
    }

    private void onReqChangeSuccess(BaseWWWRequest obj)
    {
        try
        {
            DestroyShipData data = new JsonFx.Json.JsonReader().Read<DestroyShipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqChangeFail(obj);
            }
            else
            {
                if (data.userVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(data.userVo);
                }
                if (data.equipmentVo != null)
                {
                    GameData.instance.SetUserEquipments(data.equipmentVo);
                }
                if (data.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(data.detailInfo);
                }
                this.UpdateShips();
                this.OnDestroySuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqChangeFail(obj);
        }
    }

    private void Start()
    {
    }

    private void UpdateShips()
    {
        if (this.toDestroyShips != null)
        {
            GameData.instance.DeleteUserShips(this.toDestroyShips);
        }
    }
}

