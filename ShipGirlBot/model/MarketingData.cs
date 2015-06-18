using System;

public class MarketingData
{
    public ContinueLoginAwardData continueLoginAward;
    public FirstPayAwardData firstPayAward;
    public int hideBrokenImageInAndroid;
    public int isHasBuildEvent;
    public int isHasPVEEXPEvent;
    public int isHasPVEExploreEvent;
    public int pveEventOpen;
    public int[] reachLevelAward;
    public int showBrokenImageInIos;
    public int showCouponSetting;
    public int showForumLink;
    public int showRecentMarketing;
    public int showWikiLink;

    public bool NeedShowCouponLink
    {
        get
        {
            return (this.showCouponSetting == 1);
        }
    }

    public bool NeedShowForumLink
    {
        get
        {
            return (this.showForumLink == 1);
        }
    }

    public bool NeedShowMarketingLink
    {
        get
        {
            return (this.showRecentMarketing == 1);
        }
    }

    public bool NeedShowWikiLink
    {
        get
        {
            return (this.showWikiLink == 1);
        }
    }
}

