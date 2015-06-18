using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
////using UnityEngine;

public class GameData : MonoBehaviour
{
    private List<UserQuest> _allQuests;
    private List<UserPVECampaignChapter> _campaignChapterTimesInfo;
    private Dictionary<int, int[]> _campaignFleets;
    private int[] _canExplorePVEChapters;
    private int _currentPVEChapterId;
    private CurrentPVEProgress _currentPVEProgress;
    private BuildLogVO[] _equipBuildLogs;
    private bool _haveGotPVEData;
    private bool _isCampaignDataInited;
    private DateTime _lastGetBuildLogTime;
    private DateTime _lastGetEquipBuildLogTime;
    private LeaderBoardListData _leaderBoardData;
    private LoginVO _loginInfo;
    private int _newMailNum;
    private List<int> _openedCampaignLevels;
    private List<UserPVELevel> _passedPVELevels;
    private Dictionary<int, UserPVELevel> _passedPVELevelsDic;
    private List<PVPOpponent> _pvpOpponents;
    private BuildLogVO[] _shipBuildLogs;
    private UserPVECampaignTotalChallengeInfo _totalCampainInfo;
    private List<int> _unlockedCards;
    private DockInfo[] _userDocks;
    private BuildEquipDockInfo[] _userEquipDocks;
    private Dictionary<int, UserEquipment> _userEquipmentDic;
    private UserFleet[] _userFleets;
    private UserInfo _userInfo;
    private Dictionary<int, UserItems> _userItemsDic;
    private List<MailVO> _userMails;
    private UserPVEExplore[] _userPVEExplores;
    private Dictionary<int, RepairDockInfo> _userRepairDic;
    private RepairDockInfo[] _userRepairDocks;
    private List<UserShip> _userShips;
    [CompilerGenerated]
    private static Func<UserEquipment, int> f_am_cache31;
    [CompilerGenerated]
    private static Func<UserEquipment, UserEquipment> f_am_cache32;
    [CompilerGenerated]
    private static Func<UserPVELevel, int> f_am_cache33;
    [CompilerGenerated]
    private static Func<UserPVELevel, UserPVELevel> f_am_cache34;
    [CompilerGenerated]
    private static Func<UserPVEEventLevel, int> f_am_cache35;
    [CompilerGenerated]
    private static Func<UserPVEEventLevel, UserPVEEventLevel> f_am_cache36;
    [CompilerGenerated]
    private static Func<ShopItemCanBuyStatus, int> f_am_cache37;
    [CompilerGenerated]
    private static Func<ShopItemCanBuyStatus, ShopItemCanBuyStatus> f_am_cache38;
    private string[] gameNotice;
    private bool haveGotPVEEventData;
    private DateTime lastUpdateCampaignTime;
    private DateTime lastUpdateLeaderBoardTime;
    private DateTime lastUpdatePVPTime;
    private DateTime lastUpdateQuestTime;
    private LoginResponse loginResponse;
    private string loginSelectedServer;
    private MarketingData marketingDatas;
    private static GameData mInstance;
    private bool needInviteToRegister;
    private Dictionary<int, bool> passedEventNodeIds;
    private Dictionary<int, bool> passedNodeIds;
    private List<UserPVEEventLevel> passedPVEEventLevels;
    private Dictionary<int, UserPVEEventLevel> passedPVEEventLevelsDic;
    private Dictionary<int, UserShip> shipsDic;
    private Dictionary<int, ShopItemCanBuyStatus> shopItemCanBuyStatus;

    public void AddPassedNodeId(int id)
    {
        if (this.passedNodeIds == null)
        {
            this.passedNodeIds = new Dictionary<int, bool>();
        }
        this.passedNodeIds[id] = true;
    }

    public void AddUserEquipmenet(UserEquipment equip)
    {
        if (equip != null)
        {
            if (this._userEquipmentDic == null)
            {
                this._userEquipmentDic = new Dictionary<int, UserEquipment>();
            }
            this._userEquipmentDic[equip.id] = equip;
        }
    }

    public void AddUserQuests(UserQuest[] quests)
    {
        if (this._allQuests == null)
        {
            this._allQuests = new List<UserQuest>();
        }
        if ((quests != null) && (quests.Length > 0))
        {
            this._allQuests.AddRange(quests.ToList<UserQuest>());
        }
        if (quests != null)
        {
            foreach (UserQuest quest in quests)
            {
                if (quest.IsFinished)
                {
                   // TutorialManager.instance.CheckTutorialTaskFinishWithQuestFinish(quest.taskCid);
                }
            }
        }
    }

    public void AddUserShip(UserShip us)
    {
        if (us != null)
        {
            this._userShips.Add(us);
            this.shipsDic[us.id] = us;
        }
    }

    public void CompleteResetGameData()
    {
        this._loginInfo = null;
        this._userInfo = null;
        this._userFleets = null;
        this._userShips = null;
        this._userItemsDic = null;
        this._userEquipmentDic = null;
        this._userDocks = null;
        this._userEquipDocks = null;
        this._userRepairDocks = null;
        this._userRepairDic = null;
        this.shipsDic = null;
        this._shipBuildLogs = null;
        this._equipBuildLogs = null;
        this._unlockedCards = null;
        this.lastUpdatePVPTime = new DateTime(0x7c6, 1, 1);
        this._pvpOpponents = null;
        this._allQuests = null;
        this.lastUpdateQuestTime = new DateTime(0x7c6, 1, 1);
        this.lastUpdateLeaderBoardTime = new DateTime(0x7c6, 1, 1);
        this._lastGetBuildLogTime = new DateTime(0x7c6, 1, 1);
        this._lastGetEquipBuildLogTime = new DateTime(0x7c6, 1, 1);
        this._leaderBoardData = null;
        this.IsCampaignDataInited = false;
        this._openedCampaignLevels = null;
        this._campaignChapterTimesInfo = null;
        this._campaignFleets = null;
        this._totalCampainInfo = null;
        this.shopItemCanBuyStatus = null;
        this.passedEventNodeIds = null;
        this.haveGotPVEEventData = false;
        this.passedPVEEventLevels = null;
        this.passedPVEEventLevelsDic = null;
        this.ResetPVEData();
    }

    public void DeleteMail(MailVO mail)
    {
        if (this._userMails != null)
        {
            this._userMails.Remove(mail);
        }
    }

    public void DeleteUserEquipments(List<UserEquipment> equips)
    {
        foreach (UserEquipment equipment in equips)
        {
            if (this._userEquipmentDic.ContainsKey(equipment.id))
            {
                this._userEquipmentDic.Remove(equipment.id);
            }
        }
    }

    public void DeleteUserShip(int shipId)
    {
        if (this.shipsDic.ContainsKey(shipId))
        {
            UserShip item = this.shipsDic[shipId];
            this.shipsDic.Remove(shipId);
            this._userShips.Remove(item);
        }
    }

    public void DeleteUserShips(List<UserShip> ships)
    {
        foreach (UserShip ship in ships)
        {
            if (this.shipsDic.ContainsKey(ship.id))
            {
                this.shipsDic.Remove(ship.id);
                this._userShips.Remove(ship);
            }
        }
    }

    public int[] GetCampaignFleetInfo(int levelId)
    {
        if ((this._campaignFleets != null) && this._campaignFleets.ContainsKey(levelId))
        {
            return this._campaignFleets[levelId];
        }
        return null;
    }

    public UserPVECampaignChapter GetCampaignInfo(int id)
    {
        if (this._campaignChapterTimesInfo != null)
        {
            foreach (UserPVECampaignChapter chapter in this._campaignChapterTimesInfo)
            {
                if (chapter.campaignId == id)
                {
                    return chapter;
                }
            }
        }
        return null;
    }

    public int[] GetCanExplorePVEChapters()
    {
        return this._canExplorePVEChapters;
    }

    public List<BuildLogVO> GetEquipBuildLogs()
    {
        this.SetLastGetEquipBuildLogTime();
        if ((this._equipBuildLogs != null) && (this._equipBuildLogs.Length >= 1))
        {
            return this._equipBuildLogs.ToList<BuildLogVO>();
        }
        return new List<BuildLogVO>();
    }

    public UserEquipment GetEquipmentById(int id)
    {
        if (this._userEquipmentDic.ContainsKey(id))
        {
            return this._userEquipmentDic[id];
        }
        return null;
    }

    public UserPVEExplore GetExploreAtLevel(int exploreId)
    {
        foreach (UserPVEExplore explore in this._userPVEExplores)
        {
            if (explore.exploreId == exploreId)
            {
                return explore;
            }
        }
        return null;
    }

    public UserFleet GetFleetOfId(int id)
    {
        foreach (UserFleet fleet in this._userFleets)
        {
            if (fleet.id == id)
            {
                return fleet;
            }
        }
        return null;
    }

    public UserItems GetItem(int cid)
    {
        if (this._userItemsDic.ContainsKey(cid))
        {
            return this._userItemsDic[cid];
        }
        return null;
    }

    public int GetItemAmount(ResourceTypes cid)
    {
        return this.GetItemAmount((int) cid);
    }

    public int GetItemAmount(int cid)
    {
        UserItems item = this.GetItem(cid);
        if (item != null)
        {
            return item.num;
        }
        return 0;
    }

    public List<BuildLogVO> GetShipBuildLogs()
    {
        this.SetLastGetBuildLogTime();
        if ((this._shipBuildLogs != null) && (this._shipBuildLogs.Length >= 1))
        {
            return this._shipBuildLogs.ToList<BuildLogVO>();
        }
        return new List<BuildLogVO>();
    }

    public UserShip GetShipById(int id)
    {
        if (this.shipsDic.ContainsKey(id))
        {
            return this.shipsDic[id];
        }
        return null;
    }

    public ShopItemCanBuyStatus GetShopItemCanBuyStatus(int id)
    {
        if ((this.shopItemCanBuyStatus != null) && this.shopItemCanBuyStatus.ContainsKey(id))
        {
            return this.shopItemCanBuyStatus[id];
        }
        return null;
    }

    public UserPVEEventLevel GetUserPVEEventLevel(int levelId)
    {
        if ((this.passedPVEEventLevelsDic != null) && this.passedPVEEventLevelsDic.ContainsKey(levelId))
        {
            return this.passedPVEEventLevelsDic[levelId];
        }
        return null;
    }

    public UserPVEExplore[] GetUserPVEExplores()
    {
        return this._userPVEExplores;
    }

    public UserPVELevel GetUserPVELevel(int levelId)
    {
        if (levelId >= 0x2328)
        {
            return this.GetUserPVEEventLevel(levelId);
        }
        if ((this._passedPVELevelsDic != null) && this._passedPVELevelsDic.ContainsKey(levelId))
        {
            return this._passedPVELevelsDic[levelId];
        }
        return null;
    }

    public bool HasPassedNode(int id)
    {
        return ((this.passedNodeIds != null) && this.passedNodeIds.ContainsKey(id));
    }

    public bool HaveGetShopItemCanByStatus()
    {
        return ((this.shopItemCanBuyStatus != null) && (this.shopItemCanBuyStatus.Count > 0));
    }

    public bool IsFleetInExplore(int fleetId)
    {
        if ((this._userPVEExplores != null) && (this._userPVEExplores.Length >= 1))
        {
            foreach (UserPVEExplore explore in this._userPVEExplores)
            {
                if (explore.fleetId == fleetId)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsFleetInRepair(int fleetId)
    {
        foreach (int shipid in this.GetFleetOfId(fleetId).ships)
        {
            var us = GameData.instance.GetShipById(shipid);
            if (us != null && us.IsInRepair)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPVECampaignChapterOpened(int chapterId)
    {
        return this.IsPVECampaignLevelOpened(PVEConfigs.instance.GetFirstLevelOfChapter(chapterId).id);
    }

    public bool IsPVECampaignLevelOpened(int id)
    {
        return this._openedCampaignLevels.Contains(id);
    }

    public bool IsPVEEventLevelPassed(int levelid)
    {
        UserPVEEventLevel userPVEEventLevel = this.GetUserPVEEventLevel(levelid);
        if (userPVEEventLevel == null)
        {
            return false;
        }
        return (userPVEEventLevel.status == UsePVELevelStatus.passed);
    }

    public bool IsPVELevelPassed(int levelid)
    {
        UserPVELevel userPVELevel = this.GetUserPVELevel(levelid);
        if (userPVELevel == null)
        {
            return false;
        }
        return (userPVELevel.status == UsePVELevelStatus.passed);
    }

    public bool IsShipInReapir(int shipId)
    {
        if (this._userRepairDic == null)
        {
            return false;
        }
        if (!this._userRepairDic.ContainsKey(shipId))
        {
            return false;
        }
        return true;
    }

    public bool IsShipNewUnlocked(int cardId)
    {
        if ((this._unlockedCards != null) && this._unlockedCards.Contains(cardId))
        {
            return false;
        }
        return true;
    }

    public void RemoveAllQuests()
    {
        this._allQuests = new List<UserQuest>();
    }

    public void RemoveQuest(UserQuest quest)
    {
        if (this._allQuests != null)
        {
            this._allQuests.Remove(quest);
        }
    }

    private void ResetPVEData()
    {
        this.passedNodeIds = null;
        this._currentPVEChapterId = 0;
        this._passedPVELevels = null;
        this._currentPVEProgress = null;
        this._passedPVELevelsDic = null;
        this._userPVEExplores = null;
        this._canExplorePVEChapters = null;
        this._haveGotPVEData = false;
    }

    public void SetCampaignChapterTimesInfo(UserPVECampaignChapter[] cs)
    {
        if (cs == null)
        {
            this._campaignChapterTimesInfo = new List<UserPVECampaignChapter>();
        }
        else
        {
            this._campaignChapterTimesInfo = cs.ToList<UserPVECampaignChapter>();
        }
    }

    public void SetCanExplorePVEChapters(int[] ids)
    {
        this._canExplorePVEChapters = ids;
    }

    private void SetCommonNodeIds(int[] ids)
    {
        if (this.passedNodeIds == null)
        {
            this.passedNodeIds = new Dictionary<int, bool>();
        }
        if ((ids != null) && (ids.Length >= 1))
        {
            foreach (int num in ids)
            {
                this.passedNodeIds[num] = true;
            }
        }
    }

    public void SetCurrentNodeStatus(CurrentPVEProgress obj)
    {
        this._currentPVEProgress = obj;
    }

    private void SetLastGetBuildLogTime()
    {
        this._lastGetBuildLogTime = DateTime.Now;
    }

    private void SetLastGetEquipBuildLogTime()
    {
        this._lastGetEquipBuildLogTime = DateTime.Now;
    }

    public void SetLastGetLeaderBoardDataTime()
    {
        this.lastUpdateLeaderBoardTime = DateTime.Now;
    }

    public void SetLastUpdatePVPTime()
    {
        this.lastUpdatePVPTime = DateTime.Now;
    }

    public void SetLastUpdateQuestTime()
    {
        this.lastUpdateQuestTime = DateTime.Now;
    }

    public void SetLeaderBoardData(LeaderBoardListData listData)
    {
        this._leaderBoardData = listData;
        this.SetLastGetLeaderBoardDataTime();
    }

    public void SetOpenedPVECampaignLevels(int[] ids)
    {
        this.IsCampaignDataInited = true;
        if (ids == null)
        {
            this._openedCampaignLevels = new List<int>();
        }
        else
        {
            this._openedCampaignLevels = ids.ToList<int>();
        }
    }

    public void SetPassedEventNodeIds(int[] ids)
    {
        this.haveGotPVEEventData = true;
        this.SetCommonNodeIds(ids);
    }

    public void SetPassedNodeIds(int[] ids)
    {
        this._haveGotPVEData = true;
        this.SetCommonNodeIds(ids);
    }

    public void SetPassedPVEEventLevels(UserPVEEventLevel[] levels)
    {
        if ((levels == null) || (levels.Length == 0))
        {
            this.passedPVEEventLevels = new List<UserPVEEventLevel>();
        }
        else
        {
            this.passedPVEEventLevels = levels.ToList<UserPVEEventLevel>();
        }
        if (f_am_cache35 == null)
        {
            f_am_cache35 = n => n.id;
        }
        if (f_am_cache36 == null)
        {
            f_am_cache36 = n => n;
        }
        this.passedPVEEventLevelsDic = this.passedPVEEventLevels.ToDictionary<UserPVEEventLevel, int, UserPVEEventLevel>(f_am_cache35, f_am_cache36);
    }

    public void SetPassedPVELevels(UserPVELevel[] levels)
    {
        if ((levels == null) || (levels.Length == 0))
        {
            this._passedPVELevels = new List<UserPVELevel>();
        }
        else
        {
            this._passedPVELevels = levels.ToList<UserPVELevel>();
        }
        if (f_am_cache33 == null)
        {
            f_am_cache33 = n => n.pveLevelId;
        }
        if (f_am_cache34 == null)
        {
            f_am_cache34 = n => n;
        }
        this._passedPVELevelsDic = this._passedPVELevels.ToDictionary<UserPVELevel, int, UserPVELevel>(f_am_cache33, f_am_cache34);
    }

    public void SetPVEExploreLevels(UserPVEExplore[] explores)
    {
        this._userPVEExplores = explores;
    }

    public void SetPVPOpponents(PVPOpponent[] opps)
    {
        this.SetLastUpdatePVPTime();
        if ((opps != null) && (opps.Length > 0))
        {
            this._pvpOpponents = opps.ToList<PVPOpponent>();
        }
        else
        {
            this._pvpOpponents = new List<PVPOpponent>();
        }
    }

    public void SetRepairDocks(RepairDockInfo[] docks)
    {
        this._userRepairDocks = docks;
        this._userRepairDic = new Dictionary<int, RepairDockInfo>();
        foreach (RepairDockInfo info in docks)
        {
            if (info.shipId > 0)
            {
                this._userRepairDic[info.shipId] = info;
            }
        }
    }

public void SetShipHasUnlocked(int cardId)
    {
        if (this._unlockedCards == null)
        {
            this._unlockedCards = new List<int>();
        }
        this._unlockedCards.Add(cardId);
    }

    public void SetShopItemCanBuyStatus(ShopItemCanBuyStatus[] list)
    {
        if (list == null)
        {
            this.shopItemCanBuyStatus = new Dictionary<int, ShopItemCanBuyStatus>();
        }
        else
        {
            if (f_am_cache37 == null)
            {
                f_am_cache37 = n => n.id;
            }
            if (f_am_cache38 == null)
            {
                f_am_cache38 = n => n;
            }
            this.shopItemCanBuyStatus = list.ToDictionary<ShopItemCanBuyStatus, int, ShopItemCanBuyStatus>(f_am_cache37, f_am_cache38);
        }
    }

    public void SetUnlockedCards(int[] cards)
    {
        if ((cards != null) && (cards.Length > 0))
        {
            this._unlockedCards = cards.ToList<int>();
        }
        else
        {
            this._unlockedCards = new List<int>();
        }
    }

    public void SetUserEquipments(UserEquipment[] equips)
    {
        if (equips != null)
        {
            if (f_am_cache31 == null)
            {
                f_am_cache31 = n => n.id;
            }
            if (f_am_cache32 == null)
            {
                f_am_cache32 = n => n;
            }
            this._userEquipmentDic = equips.ToDictionary<UserEquipment, int, UserEquipment>(f_am_cache31, f_am_cache32);
        }
        else
        {
            this._userEquipmentDic = new Dictionary<int, UserEquipment>();
        }
    }

    //public void SetUserMailInfo(GetMailListResponse mailList)
    //{
    //    if (mailList != null)
    //    {
    //        this._userMails = mailList.mailList.ToList<MailVO>();
    //        this.NewMailNum = mailList.newMailNum;
    //    }
    //}

    public void SetUserShips(UserShip[] ships)
    {
        this._userShips = new List<UserShip>();
        this.shipsDic = new Dictionary<int, UserShip>();
        foreach (UserShip ship in ships)
        {
            this._userShips.Add(ship);
            this.shipsDic[ship.id] = ship;
        }
    }

    public void UpdateCampaignFleetInfo(int levelId, int[] shipIds)
    {
        if (this._campaignFleets == null)
        {
            this._campaignFleets = new Dictionary<int, int[]>();
        }
        this._campaignFleets[levelId] = shipIds;
    }

    public void UpdateFleet(UserFleet newFleet)
    {
        for (int i = 0; i < this._userFleets.Length; i++)
        {
            UserFleet fleet = this._userFleets[i];
            if (fleet.id == newFleet.id)
            {
                this._userFleets[i] = newFleet;
                return;
            }
        }
    }

    public void UpdateMailList(MailVO[] mails)
    {
        if (mails != null)
        {
            this._userMails = mails.ToList<MailVO>();
        }
        else
        {
            this._userMails = new List<MailVO>();
        }
    }

    public void UpdatePVEExplore(PveExploreVo vo)
    {
        this.SetPVEExploreLevels(vo.levels);
        this.SetCanExplorePVEChapters(vo.chapters);
    }

    public void UpdateQuest(UserQuestUpdater[] quests)
    {
        UpdateQuest_c__AnonStorey78 storey = new UpdateQuest_c__AnonStorey78();
        UserQuestUpdater[] updaterArray = quests;
        for (int i = 0; i < updaterArray.Length; i++)
        {
            storey.newCondition = updaterArray[i];
            UserQuest quest = this._allQuests.Find(new Predicate<UserQuest>(storey.m_4E));
            if (quest != null)
            {
                quest.UpdateCondition(storey.newCondition.condition);
                if (quest.IsFinished)
                {
                    //TutorialManager.instance.CheckTutorialTaskFinishWithQuestFinish(quest.taskCid);
                }
            }
        }
    }

    public void UpdateUserItems(UserItems[] items)
    {
        if (items != null)
        {
            if (this._userItemsDic == null)
            {
                this._userItemsDic = new Dictionary<int, UserItems>();
            }
            foreach (UserItems items2 in items)
            {
                if (this._userItemsDic.ContainsKey(items2.itemCid))
                {
                    this._userItemsDic[items2.itemCid] = items2;
                }
                else
                {
                    this._userItemsDic.Add(items2.itemCid, items2);
                }
            }
        }
    }

    public void UpdateUserShip(UserShip newShip)
    {
        if (newShip != null)
        {
            if (this.shipsDic.ContainsKey(newShip.id))
            {
                UserShip item = this.shipsDic[newShip.id];
                this.shipsDic.Remove(newShip.id);
                this.shipsDic[newShip.id] = newShip;
                this._userShips.Remove(item);
                this._userShips.Add(newShip);
            }
            else
            {
                this.AddUserShip(newShip);
            }
        }
    }

    public List<UserQuest> AllQuests
    {
        get
        {
            return this._allQuests;
        }
    }

    public CurrentPVEProgress CurrentPVE
    {
        get
        {
            return this._currentPVEProgress;
        }
    }

    public int CurrentPVEChapterId
    {
        get
        {
            return this._currentPVEChapterId;
        }
        set
        {
            this._currentPVEChapterId = value;
        }
    }

    public BuildLogVO[] EquipBuildLogs
    {
        set
        {
            this._equipBuildLogs = value;
        }
    }

    public string[] GameNotice
    {
        get
        {
            return this.gameNotice;
        }
        set
        {
            this.gameNotice = value;
        }
    }

    public bool HaveGotPVEData
    {
        get
        {
            return this._haveGotPVEData;
        }
    }

    public bool HaveGotPVEEventData
    {
        get
        {
            return this.haveGotPVEEventData;
        }
    }

    public static GameData instance
    {
        get
        {
            //mInstance = UnityEngine.Object.FindObjectOfType(typeof(GameData)) as GameData;
            if (mInstance == null)
            {
                //mInstance = new GameObject { name = typeof(GameData).ToString() }.AddComponent<GameData>();
                mInstance = new GameData();
            }
            return mInstance;
        }
    }

    public bool IsCampaignDataInited
    {
        get
        {
            if (!this._isCampaignDataInited)
            {
                return false;
            }
            DateTime now = DateTime.Now;
            if (now.DayOfYear != this.lastUpdateCampaignTime.DayOfYear)
            {
                return false;
            }
            if ((now.Hour >= 3) && (this.lastUpdateCampaignTime.Hour < 3))
            {
                return false;
            }
            TimeSpan span = TimeSpan.FromHours(5.0);
            return (TimeSpan.FromTicks(now.Ticks - this.lastUpdateCampaignTime.Ticks) < span);
        }
        set
        {
            this._isCampaignDataInited = value;
            this.lastUpdateCampaignTime = DateTime.Now;
        }
    }

    public bool IsNeedGetBuildLog
    {
        get
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.FromMinutes(5.0);
            return (TimeSpan.FromTicks(now.Ticks - this._lastGetBuildLogTime.Ticks) > span);
        }
    }

    public bool IsNeedGetEquipBuildLog
    {
        get
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.FromMinutes(5.0);
            return (TimeSpan.FromTicks(now.Ticks - this._lastGetEquipBuildLogTime.Ticks) > span);
        }
    }

    public bool IsNeedRefreshPVP
    {
        get
        {
            DateTime now = DateTime.Now;
            TimeSpan span = TimeSpan.FromMinutes(5.0);
            return (TimeSpan.FromTicks(now.Ticks - this.lastUpdatePVPTime.Ticks) > span);
        }
    }

    public bool IsNeedRefreshQuest
    {
        get
        {
            DateTime now = DateTime.Now;
            if (now.DayOfYear != this.lastUpdateQuestTime.DayOfYear)
            {
                return true;
            }
            TimeSpan span = TimeSpan.FromTicks(now.Ticks - this.lastUpdateQuestTime.Ticks);
            TimeSpan span2 = TimeSpan.FromHours(3.0);
            return (span >= span2);
        }
    }

    public bool IsNeedUpdateLeaderBoard
    {
        get
        {
            DateTime now = DateTime.Now;
            if (now.DayOfYear != this.lastUpdateLeaderBoardTime.DayOfYear)
            {
                return true;
            }
            if ((now.Hour >= 12) && (this.lastUpdateLeaderBoardTime.Hour < 12))
            {
                return true;
            }
            TimeSpan span = TimeSpan.FromHours(5.0);
            return (TimeSpan.FromTicks(now.Ticks - this.lastUpdateLeaderBoardTime.Ticks) > span);
        }
    }

    public bool IsUserEquipmentFull
    {
        get
        {
            return (this.UserEquipmentNum >= this._userInfo.equipmentNumTop);
        }
    }

    public bool IsUserShipsFull
    {
        get
        {
            if (this._userShips == null)
            {
                return false;
            }
            return (this._userShips.Count >= this._userInfo.shipNumTop);
        }
    }

    public LeaderBoardListData LeaderBoardData
    {
        get
        {
            return this._leaderBoardData;
        }
    }

    public LoginVO LoginInfo
    {
        get
        {
            return this._loginInfo;
        }
        set
        {
            this._loginInfo = value;
        }
    }

    public LoginResponse LoginResponse
    {
        get
        {
            return this.loginResponse;
        }
        set
        {
            this.loginResponse = value;
        }
    }

    public string LoginSelectedServer
    {
        get
        {
            if (string.IsNullOrEmpty(this.loginSelectedServer))
            {
                //this.loginSelectedServer = this.loginResponse.defaultServer;
            }
            return this.loginSelectedServer;
        }
        set
        {
            this.loginSelectedServer = value;
        }
    }

    public MarketingData MarketingDatas
    {
        get
        {
            return this.marketingDatas;
        }
        set
        {
            this.marketingDatas = value;
            if ((this.marketingDatas != null) && (this.marketingDatas.showBrokenImageInIos == 1))
            {
                GameInfo.instance.disableBrokenImage = false;
            }
            if ((this.marketingDatas != null) && (this.marketingDatas.hideBrokenImageInAndroid == 1))
            {
                GameInfo.instance.disableBrokenImage = true;
            }
        }
    }

    public bool NeedInviteToRegister
    {
        get
        {
            return this.needInviteToRegister;
        }
        set
        {
            this.needInviteToRegister = value;
        }
    }

    public int NewMailNum
    {
        get
        {
            return this._newMailNum;
        }
        set
        {
            this._newMailNum = value;
        }
    }

    public List<UserPVEEventLevel> PassedPVEEventLevels
    {
        get
        {
            return this.passedPVEEventLevels;
        }
    }

    public List<UserPVELevel> PassedPVELevels
    {
        get
        {
            return this._passedPVELevels;
        }
    }

    public List<PVPOpponent> pvpOpponents
    {
        get
        {
            if (this._pvpOpponents == null)
            {
                this._pvpOpponents = new List<PVPOpponent>();
            }
            return this._pvpOpponents;
        }
    }

    public PVPOpponent getPVPOpponentbyuid(int uid)
    {
        if (this._pvpOpponents == null)
        {
            return null;
        }
        foreach(var p in _pvpOpponents)
        {
            if(p.uid == uid)
            {
                return p;
            }
        }
        return null;
    }

    public BuildLogVO[] ShipBuildLogs
    {
        get
        {
            return this._shipBuildLogs;
        }
        set
        {
            this._shipBuildLogs = value;
        }
    }

    public UserPVECampaignTotalChallengeInfo TotalCampainInfo
    {
        get
        {
            if (this._totalCampainInfo == null)
            {
                this._totalCampainInfo = new UserPVECampaignTotalChallengeInfo();
            }
            return this._totalCampainInfo;
        }
        set
        {
            this._totalCampainInfo = value;
        }
    }

    public List<int> UnlockedCards
    {
        get
        {
            if (this._unlockedCards == null)
            {
                this._unlockedCards = new List<int>();
            }
            return this._unlockedCards;
        }
    }

    public List<UserPVECampaignChapter> UserCampaigns
    {
        get
        {
            return this._campaignChapterTimesInfo;
        }
    }

    public DockInfo[] UserDocks
    {
        get
        {
            return this._userDocks;
        }
        set
        {
            this._userDocks = value;
        }
    }

    public BuildEquipDockInfo[] UserEquipDocks
    {
        get
        {
            return this._userEquipDocks;
        }
        set
        {
            this._userEquipDocks = value;
        }
    }

    public int UserEquipmentNum
    {
        get
        {
            if (this._userEquipmentDic == null)
            {
                return 0;
            }
            int num = 0;
            foreach (KeyValuePair<int, UserEquipment> pair in this._userEquipmentDic)
            {
                if (pair.Value.status == 0)
                {
                    num++;
                }
            }
            return num;
        }
    }

    public List<UserEquipment> UserEquipments
    {
        get
        {
            if (this._userEquipmentDic == null)
            {
                return new List<UserEquipment>();
            }
            return this._userEquipmentDic.Values.ToList<UserEquipment>();
        }
    }

    public UserFleet[] UserFleets
    {
        get
        {
            return this._userFleets;
        }
        set
        {
            this._userFleets = value;
        }
    }

    public UserInfo UserInfo
    {
        get
        {
            return this._userInfo;
        }
        set
        {
            this._userInfo = value;
        }
    }

    public Dictionary<int, UserItems> UserItems
    {
        get
        {
            return this._userItemsDic;
        }
    }

    public List<MailVO> UserMails
    {
        get
        {
            if (this._userMails == null)
            {
                this._userMails = new List<MailVO>();
            }
            return this._userMails;
        }
    }

    public RepairDockInfo[] UserRepairDock
    {
        get
        {
            return this._userRepairDocks;
        }
    }

    public List<UserShip> UserShips
    {
        get
        {
            if (this._userShips == null)
            {
                this._userShips = new List<UserShip>();
            }
            return this._userShips;
        }
    }

    [CompilerGenerated]
    private sealed class UpdateQuest_c__AnonStorey78
    {
        internal UserQuestUpdater newCondition;

        internal bool m_4E(UserQuest n)
        {
            return (n.taskCid == this.newCondition.taskCid);
        }
    }
}

