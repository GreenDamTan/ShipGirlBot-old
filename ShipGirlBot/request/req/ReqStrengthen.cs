using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ReqStrengthen : BaseWWWRequest
{
    private List<int> materials;

    public event EventHandler<EventArgs> StrengthenFail;

    public event EventHandler<EventArgs> StrengthenSuccess;

    private void DeleteMaterials()
    {
        GameData instance = GameData.instance;
        foreach (int num in this.materials)
        {
            instance.DeleteUserShip(num);
        }
    }

    private void onReqStrenFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnStrengthenFail(EventArgs.Empty);
    }

    private void onReqStrenSuccess(BaseWWWRequest obj)
    {
        try
        {
            StrengthenData data = new JsonReader().Read<StrengthenData>(base.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqStrenFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(data.shipVO);
                }
                this.DeleteMaterials();
                if (data.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(data.detailInfo);
                }
                if (data.equipmentVo != null)
                {
                    GameData.instance.SetUserEquipments(data.equipmentVo);
                }
                this.OnStrengthenSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqStrenFail(obj);
        }
    }

    protected virtual void OnStrengthenFail(EventArgs e)
    {
        if (this.StrengthenFail != null)
        {
            this.StrengthenFail(this, e);
        }
    }

    protected virtual void OnStrengthenSuccess(EventArgs e)
    {
        if (this.StrengthenSuccess != null)
        {
            this.StrengthenSuccess(this, e);
        }
    }

    private void Start()
    {
    }

    public void Strengthen(int targetId, List<int> materialId)
    {
        this.materials = materialId;
        string str = "[";
        for (int i = 0; i < this.materials.Count; i++)
        {
            str = str + this.materials[i];
            if (i != (this.materials.Count - 1))
            {
                str = str + ",";
            }
        }
        str = str + "]";
        object[] objArray1 = new object[] { "boat/strengthen/", targetId, "/", str };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqStrenSuccess), new BaseWWWRequest.OnFail(this.onReqStrenFail), true, ServerType.ChoosedServer, false);
    }
}

