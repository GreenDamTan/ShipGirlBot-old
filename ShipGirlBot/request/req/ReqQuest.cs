using JsonFx.Json;
using System;
using System.Runtime.CompilerServices;

public class ReqQuest : BaseWWWRequest
{
    private UserQuest finishingQuest;
    private FinishQuestResponse finishResponse;

    public event EventHandler<EventArgs> FinishQuestFail;

    public event EventHandler<EventArgs> FinishQuestSuccess;

    public event EventHandler<EventArgs> GetQuestFail;

    public event EventHandler<EventArgs> GetQuestSuccess;

    public void FinishQuest(UserQuest quest)
    {
        this.finishingQuest = quest;
        base.path = "task/getAward/" + quest.taskCid;
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqChangeSuccess), new BaseWWWRequest.OnFail(this.onReqChangeFail), true, ServerType.ChoosedServer, false);
    }

    public void GetQuest()
    {
        base.path = "task/getUserData";
        base.SetupParams(null, new BaseWWWRequest.OnSuccess(this.onReqGetSuccess), new BaseWWWRequest.OnFail(this.onReqGetFail), true, ServerType.ChoosedServer, false);
    }

    protected virtual void OnFinishQuestFail(EventArgs e)
    {
        if (this.FinishQuestFail != null)
        {
            this.FinishQuestFail(this, e);
        }
    }

    protected virtual void OnFinishQuestSuccess(EventArgs e)
    {
        if (this.FinishQuestSuccess != null)
        {
            this.FinishQuestSuccess(this.finishResponse, e);
        }
    }

    protected virtual void OnGetQuestFail(EventArgs e)
    {
        if (this.GetQuestFail != null)
        {
            this.GetQuestFail(this, e);
        }
    }

    protected virtual void OnGetQuestSuccess(EventArgs e)
    {
        if (this.GetQuestSuccess != null)
        {
            this.GetQuestSuccess(this, e);
        }
    }

    private void onReqChangeFail(BaseWWWRequest obj)
    {
        base.ShowServerError();
        this.OnFinishQuestFail(EventArgs.Empty);
    }

    private void onReqChangeSuccess(BaseWWWRequest obj)
    {
        try
        {
            this.finishResponse = new JsonFx.Json.JsonReader().Read<FinishQuestResponse>(this.UTF8String);
            base.responseData = this.finishResponse;
            if (this.finishResponse.eid != 0)
            {
                this.onReqChangeFail(obj);
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
                if (this.finishResponse.detailInfo != null)
                {
                    GameData.instance.UserInfo.UpdateDetailInfo(this.finishResponse.detailInfo);
                }
                if (this.finishResponse.fleetVo != null)
                {
                    GameData instance = GameData.instance;
                    foreach (UserFleet fleet in this.finishResponse.fleetVo)
                    {
                        instance.UpdateFleet(fleet);
                        if (fleet.ships != null)
                        {
                            foreach (int num4 in fleet.ships)
                            {
                                instance.GetShipById(num4).fleetId = fleet.id;
                            }
                        }
                    }
                }
                //TutorialManager.instance.CheckTutorialTaskFinishWithGetQuestReward(this.finishingQuest.taskCid);
                GameData.instance.AddUserQuests(this.finishResponse.taskVo);
                GameData.instance.RemoveQuest(this.finishingQuest);
                this.OnFinishQuestSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqChangeFail(obj);
        }
    }

    private void onReqGetFail(BaseWWWRequest obj)
    {
        this.OnGetQuestFail(EventArgs.Empty);
    }

    private void onReqGetSuccess(BaseWWWRequest obj)
    {
        try
        {
            GetQuestResponse response = new JsonFx.Json.JsonReader().Read<GetQuestResponse>(this.UTF8String);
            base.responseData = response;
            if (response.eid != 0)
            {
                this.onReqGetFail(obj);
            }
            else
            {
                GameData.instance.RemoveAllQuests();
                GameData.instance.AddUserQuests(response.taskVo);
                GameData.instance.SetLastUpdateQuestTime();
                this.OnGetQuestSuccess(EventArgs.Empty);
            }
        }
        catch (Exception exception)
        {
            z.log(exception.Message);
            this.onReqGetFail(obj);
        }
    }


}

