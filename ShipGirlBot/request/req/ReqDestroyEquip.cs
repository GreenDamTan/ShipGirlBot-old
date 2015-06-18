using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEngine;

public class ReqDestroyEquip : BaseWWWRequest
{
    private List<UserEquipment> toDestroyEqups;

    public event EventHandler<EventArgs> DestroyEquipFail;

    public event EventHandler<EventArgs> DestroyEquipSuccess;

    public void DestroyeEquip(List<UserEquipment> equips)
    {
        this.toDestroyEqups = equips;
        string str = "[";
        for (int i = 0; i < equips.Count; i++)
        {
            str = str + equips[i].id;
            if (i != (equips.Count - 1))
            {
                str = str + ",";
            }
        }
        str = str + "]";
        base.path = "dock/dismantleEquipment/" + str;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqChangeSuccess), new BaseWWWRequest.OnFail(this.onReqChangeFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnDestroyEquipFail(EventArgs e)
    {
        if (this.DestroyEquipFail != null)
        {
            this.DestroyEquipFail(this, e);
        }
    }

    protected virtual void OnDestroyEquipSuccess(EventArgs e)
    {
        if (this.DestroyEquipSuccess != null)
        {
            this.DestroyEquipSuccess(this, e);
        }
    }

    private void onReqChangeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnDestroyEquipFail(EventArgs.Empty);
    }

    private void onReqChangeSuccess(BaseWWWRequest obj)
    {
        try
        {
            DestroyEquipData data = new JsonFx.Json.JsonReader().Read<DestroyEquipData>(this.UTF8String);
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
                if (data.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(data.detailInfo);
                }
                this.UpdateEquip();
                if (data.equipmentVo != null)
                {
                    GameData.instance.SetUserEquipments(data.equipmentVo);
                }
                this.OnDestroyEquipSuccess(EventArgs.Empty);
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

    private void UpdateEquip()
    {
        if (this.toDestroyEqups != null)
        {
            GameData.instance.DeleteUserEquipments(this.toDestroyEqups);
        }
    }
}

