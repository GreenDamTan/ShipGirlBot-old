using System;

public class InitDataVO : BasicResponse
{
    public DockInfo[] dockVo;
    public BuildEquipDockInfo[] equipmentDockVo;
    public UserEquipment[] equipmentVo;
    public UserFleet[] fleetVo;
    public MarketingData marketingData;
    public int newMailNum;
    public UserItems[] packageVo;
    public PveExploreVo pveExploreVo;
    public RepairDockInfo[] repairDockVo;
    public long systime;
    public UserQuest[] taskVo;
    public int[] unlockShip;
    public UserShip[] userShipVO;
    public UserInfo userVo;
}

