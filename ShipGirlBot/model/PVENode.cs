using System;
using System.Collections.Generic;
using System.Linq;

public class PVENode
{
    public string flag;
    public object formation;
    public Dictionary<string, int> gain;
    public int id;
    public Dictionary<string, int> loss;
    public int mapId;
    public int[] nextNode;
    public string[] nextNodePath;
    public PVENodeAttribute nodeAttribute;
    public PVENodeRouter[] nodeRounter;
    public PVENodeType nodeType;
    public PositionXY position;
    public int pveLevelId;
    public int roundabout;
    public int shipExp;
    public int userExp;

    public PVENodeRouter GetRouterOf(int nodeId)
    {
        if ((this.nodeRounter != null) && (this.nodeRounter.Length != 0))
        {
            foreach (PVENodeRouter router in this.nodeRounter)
            {
                if (router.nodeId == nodeId)
                {
                    return router;
                }
            }
        }
        return null;
    }

    public List<PVENodeRouter> GetRoutersWithCondition()
    {
        List<PVENodeRouter> list = new List<PVENodeRouter>();
        if ((this.nodeRounter != null) && (this.nodeRounter.Length != 0))
        {
            foreach (PVENodeRouter router in this.nodeRounter)
            {
                if (router.HasNonZeroCondition)
                {
                    list.Add(router);
                }
            }
        }
        return list;
    }

    public int GetRouterWeightOf(int id)
    {
        if ((this.nodeRounter != null) && (this.nodeRounter.Length != 0))
        {
            foreach (PVENodeRouter router in this.nodeRounter)
            {
                if (router.nodeId == id)
                {
                    return router.Weight;
                }
            }
        }
        return 0;
    }

    public bool CanSkip
    {
        get
        {
            return (this.roundabout == 1);
        }
    }
    public bool HaveMultiNextNodes
    {
        get
        {
            return ((this.nextNode != null) && (this.nextNode.Length > 1));
        }
    }

    public bool IsBattleNode
    {
        get
        {
            return ((this.nodeType == PVENodeType.normalBattle) || (this.nodeType == PVENodeType.bossBattle));
        }
    }

    public List<int> NextNodes
    {
        get
        {
            if ((this.nextNode != null) && (this.nextNode.Length != 0))
            {
                return this.nextNode.ToList<int>();
            }
            return new List<int>();
        }
    }
}

