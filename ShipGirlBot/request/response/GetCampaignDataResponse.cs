using System;

public class GetCampaignDataResponse : BasicResponse
{
    public UserPVECampaignChapter[] campaignChallenge;
    public int[] canCampaignChallengeLevel;
    public UserPVECampaignTotalChallengeInfo passInfo;
}

