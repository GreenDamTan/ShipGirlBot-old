using System;

public class ShipInWarInitStatus
{
    public int hp;
    public int hpMax;

    public void UpdateHP(int amount)
    {
        this.hp += amount;
        if (this.hp < 0)
        {
            this.hp = 0;
        }
    }
}

