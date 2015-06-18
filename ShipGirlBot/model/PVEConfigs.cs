using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
////using UnityEngine;

public class PVEConfigs : MonoBehaviour
{
    public List<PVECampaignChapter> _campaignChapters;
    public Dictionary<int, PVECampaignChapter> _campaignChaptersDic;
    public List<PVECampaignLevel> _campaignLevels;
    public Dictionary<int, PVECampaignLevel> _campaignLevelsDic;
    public List<PVEChapter> _chapters;
    public Dictionary<int, PVEChapter> _chaptersDic;
    public List<PVEEventLevel> _eventLevels;
    public Dictionary<int, PVEEventLevel> _eventLevelsDic;
    public List<PVELevel> _levels;
    public Dictionary<int, PVELevel> _levelsDic;
    public List<PVENode> _nodes;
    public Dictionary<int, PVENode> _nodesDic;
    [CompilerGenerated]
    private static Func<PVELevel, PVELevel> f_am_cache10;
    [CompilerGenerated]
    private static Func<PVEEventLevel, int> f_am_cache11;
    [CompilerGenerated]
    private static Func<PVEEventLevel, PVEEventLevel> f_am_cache12;
    [CompilerGenerated]
    private static Func<PVECampaignChapter, int> f_am_cache13;
    [CompilerGenerated]
    private static Func<PVECampaignChapter, PVECampaignChapter> f_am_cache14;
    [CompilerGenerated]
    private static Func<PVECampaignLevel, int> f_am_cache15;
    [CompilerGenerated]
    private static Func<PVECampaignLevel, PVECampaignLevel> f_am_cache16;
    [CompilerGenerated]
    private static Func<PVEChapter, int> f_am_cacheD;
    [CompilerGenerated]
    private static Func<PVEChapter, PVEChapter> f_am_cacheE;
    [CompilerGenerated]
    private static Func<PVELevel, int> f_am_cacheF;
    private static PVEConfigs mInstance;

    public PVECampaignChapter GetCampaignChapter(int id)
    {
        if ((this._campaignChaptersDic != null) && this._campaignChaptersDic.ContainsKey(id))
        {
            return this._campaignChaptersDic[id];
        }
        return null;
    }

    public PVECampaignLevel GetCampaignLevel(int id)
    {
        if ((this._campaignLevelsDic != null) && this._campaignLevelsDic.ContainsKey(id))
        {
            return this._campaignLevelsDic[id];
        }
        return null;
    }

    public List<PVECampaignLevel> GetCampaignLevelOfChapter(int chapterId)
    {
        GetCampaignLevelOfChapter_c__AnonStorey79 storey = new GetCampaignLevelOfChapter_c__AnonStorey79
        {
            chapterId = chapterId
        };
        return this._campaignLevels.FindAll(new Predicate<PVECampaignLevel>(storey.m_5B));
    }

    public PVEChapter GetChapter(int id)
    {
        if ((this._chaptersDic != null) && this._chaptersDic.ContainsKey(id))
        {
            return this._chaptersDic[id];
        }
        return null;
    }

    public PVEEventLevel GetEventLevel(int id)
    {
        if ((this._eventLevelsDic != null) && this._eventLevelsDic.ContainsKey(id))
        {
            return this._eventLevelsDic[id];
        }
        return null;
    }

    public PVECampaignLevel GetFirstLevelOfChapter(int chapterId)
    {
        List<PVECampaignLevel> campaignLevelOfChapter = this.GetCampaignLevelOfChapter(chapterId);
        if ((campaignLevelOfChapter != null) && (campaignLevelOfChapter.Count > 0))
        {
            return campaignLevelOfChapter[0];
        }
        return null;
    }

    public PVELevel GetLevel(int id)
    {
        if (id >= 0x2328)
        {
            return this.GetEventLevel(id);
        }
        if ((this._levelsDic != null) && this._levelsDic.ContainsKey(id))
        {
            return this._levelsDic[id];
        }
        return null;
    }

    public List<PVELevel> GetLevelsOfChapter(int chapterId)
    {
        List<PVELevel> list = new List<PVELevel>();
        foreach (PVELevel level in this._levels)
        {
            if (level.pveId == chapterId)
            {
                list.Add(level);
            }
        }
        return list;
    }

    public PVENode GetNode(int id)
    {
        if ((this._nodesDic != null) && this._nodesDic.ContainsKey(id))
        {
            return this._nodesDic[id];
        }
        return null;
    }

    public List<PVENode> GetNodesOfLevel(int levelId)
    {
        List<PVENode> list = new List<PVENode>();
        foreach (PVENode node in this._nodes)
        {
            if (node.pveLevelId == levelId)
            {
                list.Add(node);
            }
        }
        return list;
    }

    public void SetCampaignChapters(PVECampaignChapter[] chapters)
    {
        this._campaignChapters = chapters.ToList<PVECampaignChapter>();
        if (f_am_cache13 == null)
        {
            f_am_cache13 = n => n.id;
        }
        if (f_am_cache14 == null)
        {
            f_am_cache14 = n => n;
        }
        this._campaignChaptersDic = this._campaignChapters.ToDictionary<PVECampaignChapter, int, PVECampaignChapter>(f_am_cache13, f_am_cache14);
    }

    public void SetCampaignLevels(PVECampaignLevel[] levels)
    {
        this._campaignLevels = levels.ToList<PVECampaignLevel>();
        if (f_am_cache15 == null)
        {
            f_am_cache15 = n => n.id;
        }
        if (f_am_cache16 == null)
        {
            f_am_cache16 = n => n;
        }
        this._campaignLevelsDic = this._campaignLevels.ToDictionary<PVECampaignLevel, int, PVECampaignLevel>(f_am_cache15, f_am_cache16);
    }

    public void SetChapters(PVEChapter[] chapters)
    {
        this._chapters = chapters.ToList<PVEChapter>();
        if (f_am_cacheD == null)
        {
            f_am_cacheD = n => n.id;
        }
        if (f_am_cacheE == null)
        {
            f_am_cacheE = n => n;
        }
        this._chaptersDic = this._chapters.ToDictionary<PVEChapter, int, PVEChapter>(f_am_cacheD, f_am_cacheE);
    }

    public void SetEventLevels(PVEEventLevel[] levels)
    {
        this._eventLevels = levels.ToList<PVEEventLevel>();
        if (f_am_cache11 == null)
        {
            f_am_cache11 = n => n.id;
        }
        if (f_am_cache12 == null)
        {
            f_am_cache12 = n => n;
        }
        this._eventLevelsDic = this._eventLevels.ToDictionary<PVEEventLevel, int, PVEEventLevel>(f_am_cache11, f_am_cache12);
    }

    public void SetLevels(PVELevel[] levels)
    {
        this._levels = levels.ToList<PVELevel>();
        if (f_am_cacheF == null)
        {
            f_am_cacheF = n => n.id;
        }
        if (f_am_cache10 == null)
        {
            f_am_cache10 = n => n;
        }
        this._levelsDic = this._levels.ToDictionary<PVELevel, int, PVELevel>(f_am_cacheF, f_am_cache10);
    }

    public void SetNodes(PVENode[] nodes)
    {
        if (this._nodes == null)
        {
            this._nodes = new List<PVENode>();
        }
        if (this._nodesDic == null)
        {
            this._nodesDic = new Dictionary<int, PVENode>();
        }
        if ((nodes != null) && (nodes.Length != 0))
        {
            this._nodes.AddRange(nodes.ToList<PVENode>());
            foreach (PVENode node in nodes)
            {
                this._nodesDic[node.id] = node;
            }
        }
    }


    public List<PVECampaignChapter> CampaignChapters
    {
        get
        {
            return this._campaignChapters;
        }
    }

    public List<PVECampaignLevel> CampaignLevels
    {
        get
        {
            return this._campaignLevels;
        }
    }

    public List<PVEChapter> Chapters
    {
        get
        {
            return this._chapters;
        }
    }

    public List<PVEEventLevel> EventLevels
    {
        get
        {
            return this._eventLevels;
        }
    }

    public static PVEConfigs instance
    {
        get
        {
            //mInstance = UnityEngine.Object.FindObjectOfType(typeof(PVEConfigs)) as PVEConfigs;
            if (mInstance == null)
            {
            //    mInstance = new GameObject { name = typeof(PVEConfigs).ToString() }.AddComponent<PVEConfigs>();
                mInstance = new PVEConfigs();
            }
            return mInstance;
        }
    }

    public List<PVELevel> Levels
    {
        get
        {
            return this._levels;
        }
    }

    public List<PVENode> Nodes
    {
        get
        {
            return this._nodes;
        }
    }

    [CompilerGenerated]
    private sealed class GetCampaignLevelOfChapter_c__AnonStorey79
    {
        internal int chapterId;

        internal bool m_5B(PVECampaignLevel n)
        {
            return (n.campaignId == this.chapterId);
        }
    }
}

