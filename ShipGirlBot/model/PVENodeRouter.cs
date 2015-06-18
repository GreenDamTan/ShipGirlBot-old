using System;

public class PVENodeRouter
{
    public PVENodeRouterCondition[] condition;
    public int nodeId;

    public bool HasNonZeroCondition
    {
        get
        {
            if ((this.condition != null) && (this.condition.Length != 0))
            {
                foreach (PVENodeRouterCondition condition in this.condition)
                {
                    if (condition.type != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public PVENodeRouterCondition NonZeroCondition
    {
        get
        {
            if ((this.condition != null) && (this.condition.Length != 0))
            {
                foreach (PVENodeRouterCondition condition in this.condition)
                {
                    if (condition.type != 0)
                    {
                        return condition;
                    }
                }
            }
            return null;
        }
    }

    public int Weight
    {
        get
        {
            if ((this.condition != null) && (this.condition.Length != 0))
            {
                foreach (PVENodeRouterCondition condition in this.condition)
                {
                    if (condition.type == 0)
                    {
                        return condition.weight;
                    }
                }
            }
            return 0;
        }
    }
}

