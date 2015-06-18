using System;

public class SkipWarResponse : BasicResponse
{
    public int bossHp;
    public int bossHpLeft;
    public GetCampaignDataResponse campaignVo;
    public UserDetailInfo detailInfo;
    public int isSuccess;
    public GetPVEDataResponse newPveData;
    public UserShip[] newShipVO;
    public UserItems[] packageVo;
    public int pveLevelEnd;
    public UserShip[] shipVO;
    public UserResInfo userResVo;
    public WarResult warResult;
}

