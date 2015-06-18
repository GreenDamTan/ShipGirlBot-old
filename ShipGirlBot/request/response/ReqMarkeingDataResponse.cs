using System.Collections.Generic;

public class ReqMarkeingDataResponse : BasicResponse
{
    public UserEquipment[] equipmentVo;
    public MarketingData marketingData;
    public UserItems[] packageVo;
    public Dictionary<string, int> randAward;
    public UserShip[] shipVO;
    public UserResInfo userResVo;
}

