using System;

public class ShopItemConfig
{
    public string brief;
    public string desc;
    public ShopItemDiscountType effect;
    public int functionAmount;
    public ShopItemFunction functionId;
    public ShopItemContent[] goods;
    public string icon;
    public int id;
    public string iosIapId;
    //public int lable;
    public ShopBuyLimitType limit;
    public int price;
    public int priceSale;
    public ShopPriceType priceType;
    public string title;
    public int type;

    public bool HaveItemInside(int itemId)
    {
        if (this.goods != null)
        {
            foreach (ShopItemContent content in this.goods)
            {
                if (content.id == (itemId + string.Empty))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

