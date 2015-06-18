using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;


public class ReqEquipment : BaseWWWRequest
{
    private int newEquipId;
    private int oldEquipId;

    public event EventHandler<EventArgs> ChangeEquipFail;

    public event EventHandler<EventArgs> ChangeEquipSuccess;

    public void ChangeEquip(UserShip ship, int position, int equpId)
    {
        if ((ship.equipmentArr.Length > position) && (ship.equipmentArr[position] != null))
        {
            this.oldEquipId = ship.equipmentArr[position].id;
        }
        else
        {
            this.oldEquipId = -1;
        }
        this.newEquipId = equpId;
        object[] objArray1 = new object[] { "boat/changeEquipment/", ship.id, "/", equpId, "/", position };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqChangeSuccess), new BaseWWWRequest.OnFail(this.onReqChangeFail), true, ServerType.ChoosedServer, false);
    }

    public void DeleteEquip(UserShip ship, int position)
    {
        this.oldEquipId = ship.equipmentArr[position].id;
        this.newEquipId = -1;
        object[] objArray1 = new object[] { "boat/removeEquipment/", ship.id, "/", position };
        base.path = string.Concat(objArray1);
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqChangeSuccess), new BaseWWWRequest.OnFail(this.onReqChangeFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnChangeEquipFail(EventArgs e)
    {
        if (this.ChangeEquipFail != null)
        {
            this.ChangeEquipFail(this, e);
        }
    }

    protected virtual void OnChangeEquipSuccess(EventArgs e)
    {
        if (this.ChangeEquipSuccess != null)
        {
            this.ChangeEquipSuccess(this, e);
        }
    }

    private void onReqChangeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnChangeEquipFail(EventArgs.Empty);
    }

    private void onReqChangeSuccess(BaseWWWRequest obj)
    {
        try
        {
            ChangeEquipData data = new JsonFx.Json.JsonReader().Read<ChangeEquipData>(this.UTF8String);
            base.responseData = data;
            if (data.eid != 0)
            {
                this.onReqChangeFail(obj);
            }
            else
            {
                if (data.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(data.shipVO);
                }
                if (data.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(data.detailInfo);
                }
                this.UpdateEquip();
                this.OnChangeEquipSuccess(EventArgs.Empty);
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
        if (this.oldEquipId > 0)
        {
            UserEquipment equipmentById = GameData.instance.GetEquipmentById(this.oldEquipId);
            if (equipmentById != null)
            {
                equipmentById.status = 0;
            }
        }
        if (this.newEquipId > 0)
        {
            UserEquipment equipment2 = GameData.instance.GetEquipmentById(this.newEquipId);
            if (equipment2 != null)
            {
                equipment2.status = 1;
            }
        }
    }
}

