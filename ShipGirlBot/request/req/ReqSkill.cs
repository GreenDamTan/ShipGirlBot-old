using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class ReqSkill : BaseWWWRequest
{
    private SkillShipData skillResponse;
    private UserShip toDestroyShip;

    public event EventHandler<EventArgs> SkillFail;

    public event EventHandler<EventArgs> SkillSuccess;

    private void onReqSkillFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnSkillFail(EventArgs.Empty);
    }

    private void onReqSkillSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.skillResponse = new JsonReader().Read<SkillShipData>(base.UTF8String);
            base.responseData = this.skillResponse;
            if (this.skillResponse.eid != 0)
            {
                this.onReqSkillFail(obj);
            }
            else
            {
                if (this.skillResponse.shipVO != null)
                {
                    GameData.instance.UpdateUserShip(this.skillResponse.shipVO);
                }
                this.OnSkillSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqSkillFail(obj);
        }
    }

    protected virtual void OnSkillFail(EventArgs e)
    {
        if (this.SkillFail != null)
        {
            this.SkillFail(this, e);
        }
    }

    protected virtual void OnSkillSuccess(EventArgs e)
    {
        if (this.SkillSuccess != null)
        {
            this.SkillSuccess(this.skillResponse.shipVO, e);
        }
    }

    public void SkillShip(UserShip ship)
    {
        this.toDestroyShip = ship;
        base.path = "boat/skillLevelUp/" + ship.id;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqSkillSuccess), new BaseWWWRequest.OnFail(this.onReqSkillFail), true, ServerType.ChoosedServer, false);
    }

    private void Start()
    {
    }
}

