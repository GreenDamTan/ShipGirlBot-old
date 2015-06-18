using System;
using System.Collections.Generic;

public class EquipmentConfig
{
    public int aircraftAtk;
    public int airDef;
    public int aluminiumUse;
    public int antisub;
    public int atk;
    public int cid;
    public string correction;
    public int def;
    public string desc;
    public Dictionary<string, int> dismantle;
    public int equipIndex;
    public int hit;
    public int luck;
    public int miss;
    public string picId;
    public int radar;
    public int range;
    public ShipType[] shipType;
    public string specialEffect;
    public int star;
    public string title;
    public int torpedo;
    public EquipmentType type;

    public bool CanUsedInShip(ShipType type)
    {
        if (this.shipType != null)
        {
            foreach (ShipType type2 in this.shipType)
            {
                if (type2 == type)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

