using System;

public class QuestCondition
{
    public int actionType;
    public int finishedAmount;
    public int targetCid;
    public int totalAmount;

    public bool IsFinished
    {
        get
        {
            return (this.finishedAmount >= this.totalAmount);
        }
    }
}

