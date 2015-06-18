using System;

public class GetBattleResultResponse : BasicResponse
{
    public int bossHp;
    public int bossHpLeft;
    public GetCampaignDataResponse campaignVo;
    public UserDetailInfo detailInfo;
    public WarProgressRecord extraProgress;
    public bool isIncludeNightFight;
    public GetPVEDataResponse newPveData;
    public UserShip[] newShipVO;
    public UserItems[] packageVo;
    public UserShip[] shipVO;
    public UserResInfo userResVo;
    public WarResult warResult;
}

