using System;

public class BuildLogVO
{
    public int cid;
    public long createTime;
    public int fav;
    public int id;
    public BuildResource res;
    public BuildLogType type;
    public int uid;
    public string username;

    public void ToggleFav()
    {
        if (this.fav == 0)
        {
            this.fav = 1;
        }
        else
        {
            this.fav = 0;
        }
    }
}

