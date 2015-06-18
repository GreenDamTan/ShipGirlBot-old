using System;
using System.Collections.Generic;

internal class GetNextNodeResponse : BasicResponse
{
    public GetPVEDataResponse newPveData = null;
    public int node = 0;
    public Dictionary<string, int> nodeStatus = new Dictionary<string, int>();
}

