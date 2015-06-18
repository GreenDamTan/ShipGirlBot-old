using System;
using System.Collections.Generic;

public class GetDealNodeResponse : BasicResponse
{
    public UserEquipment[] equipmentVo = null;
    public Dictionary<string, int> lostItem = null;
    public GetPVEDataResponse newPveData = null;
    public UserShip[] newShipVO = null;
    public UserItems[] packageVo = null;
    public int pveLevelEnd = 0;
    public UserInfo userResVO = null;
    public WarProgressRecord warReport = null;
}

