using System;

public class BasicResponse
{
    public int _eid;
    public int eid
    {
        get { return _eid; }
        set { _eid = value; }
    }
    public string eidstring
    {
        get { return GameConfigs.instance.GetErrorString(_eid); }
    }
    public UserMailUpdater newMailInfo;
    public UserQuestUpdater[] updateTaskVo;
}

