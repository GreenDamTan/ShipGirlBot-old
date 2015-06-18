using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class ReqCoupon : BaseWWWRequest
{
    private GetCouponResponse finishResponse;

    public event EventHandler<EventArgs> GetGiftFail;

    public event EventHandler<EventArgs> GetGiftSuccess;

    protected virtual void OnGetGiftFail(EventArgs e)
    {
        if (this.GetGiftFail != null)
        {
            if (this.finishResponse != null)
            {
                this.GetGiftFail(this.finishResponse.eid, e);
            }
            else
            {
                this.GetGiftFail(0, e);
            }
        }
    }

    protected virtual void OnGetGiftSuccess(EventArgs e)
    {
        if (this.GetGiftSuccess != null)
        {
            this.GetGiftSuccess(this.finishResponse, e);
        }
    }

    private void onReqGetFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnGetGiftFail(EventArgs.Empty);
    }

    private void onReqGetSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.finishResponse = new JsonReader().Read<GetCouponResponse>(base.UTF8String);
            base.responseData = this.finishResponse;
            if (this.finishResponse.eid != 0)
            {
                this.onReqGetFail(obj);
            }
            else
            {
                if (this.finishResponse.userResVo != null)
                {
                    GameData.instance.UserInfo.UpdateResource(this.finishResponse.userResVo);
                }
                if (this.finishResponse.shipVO != null)
                {
                    foreach (UserShip ship in this.finishResponse.shipVO)
                    {
                        GameData.instance.AddUserShip(ship);
                    }
                }
                if (this.finishResponse.equipmentVo != null)
                {
                    foreach (UserEquipment equipment in this.finishResponse.equipmentVo)
                    {
                        GameData.instance.AddUserEquipmenet(equipment);
                    }
                }
                if (this.finishResponse.packageVo != null)
                {
                    GameData.instance.UpdateUserItems(this.finishResponse.packageVo);
                }
                this.OnGetGiftSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetFail(obj);
        }
    }

    private void Start()
    {
    }

    public void UseCoupon(string couponid)
    {
        base.path = "api/giftCode/" + couponid;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetSuccess), new BaseWWWRequest.OnFail(this.onReqGetFail), true, ServerType.ChoosedServer, false);
    }
}

