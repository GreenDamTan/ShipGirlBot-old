using System;

public class ShopItemCanBuyStatus
{
    public int canBuyNum;
    public int hadBuyNum;
    public int id;

    public bool CanBuy
    {
        get
        {
            return (this.canBuyNum != 0);
        }
    }
}

