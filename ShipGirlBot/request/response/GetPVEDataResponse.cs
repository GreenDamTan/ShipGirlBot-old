using System;

public class GetPVEDataResponse : BasicResponse
{
    public CurrentPVEProgress currentVo;
    public int[] passedNodes;
    public UserPVEEventLevel[] pveEventLevel;
    public UserPVELevel[] pveLevel;
}

