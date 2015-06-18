using System;
using System.Collections.Generic;
using System.Linq;

public class ShipConfig
{
    public int airDef;
    public int airDefMax;
    public int antisub;
    public int antisubMax;
    public int atk;
    public int atkMax;
    public string author;
    public int borderId;
    public int canEvo;
    public int capacity;
    public int[] capacitySlot;
    public int cid;
    public string classNo;
    public int country;
    public string critRepair;
    public string customField;
    public int def;
    public int defMax;
    public string desc;
    public Dictionary<string, int> dismantle;
    public int[] equipmentType;
    public int evoCid;
    public int evoClass;
    public int evoLevel;
    public int evoNeedItemCid;
    public Dictionary<string, int> evoNeedResource;
    public int evoToCid;
    public string getDialogue;
    public string hitRepair;
    public int hp;
    public int hpMax;
    public int luck;
    public int luckMax;
    public int maxAmmo;
    public int maxLevel;
    public int maxOil;
    public int miss;
    public int missMax;
    public string missRepair;
    public string picId;
    public int radar;
    public int radarMax;
    public int range;
    public int release;
    public float repairOilModulus;
    public float repairSteelModulus;
    public float repairTime;
    public int shipIndex;
    public int[] showAttribute;
    public float speed;
    public int star;
    public int strengthenLevelUpExp;
    public StrengthenAttribute strengthenSupplyExp;
    public StrengthenAttribute strengthenTop;
    public string title;
    public string titleClass;
    public int torpedo;
    public int torpedoMax;
    public ShipType type;
    public string vow;

    public bool CanUseEquip(int equipType)
    {
        return ((this.equipmentType != null) && this.equipmentType.Contains<int>(equipType));
    }
}

