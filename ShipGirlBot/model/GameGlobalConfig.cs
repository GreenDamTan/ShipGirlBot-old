using System;

public class GameGlobalConfig
{
    public int canSkipWar;
    public int factoryFiveStarLevel;
    public string forumUrl;
    public string marketingUrl;
    public int[] nightWarCost;
    public int[] resourceRecoveryNum;
    public int[] resourceRecoveryTime;
    public string wikiUrl;
    public int[] skipWarFailCost;
    public int[] skipWarSuccessCost;
    public int[] startWarCost;

    public GameGlobalConfig()
    {
        int[] numArray1 = new int[4];
        numArray1[0] = -1;
        this.skipWarSuccessCost = numArray1;
        int[] numArray2 = new int[4];
        numArray2[0] = -1;
        this.skipWarFailCost = numArray2;
        int[] numArray3 = new int[4];
        numArray3[0] = -2;
        numArray3[1] = -2;
        this.startWarCost = numArray3;
        int[] numArray4 = new int[4];
        numArray4[1] = -1;
        this.nightWarCost = numArray4;
    }
}

