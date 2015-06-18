using System;
//////using UnityEngine;

public class UserShip
{
    private ShipConfig _ship;
    public ShipBattleProps battleProps;
    public ShipBattleProps battlePropsMax;
    public int[] capacitySlot;
    public int[] capacitySlotExist;
    public int[] capacitySlotMax;
    public int energy;
    public EquipInShip[] equipmentArr;
    public int exp;
    public int fleetId;
    public int id;
    public int isLocked;
    public int level;
    public int love;
    public int loveMax;
    public int married;
    public const float MIDDLE_DAMAGE = 0.25f;
    public int nextExp;
    public int nextSkillId;
    public const float NO_DAMAGE = 0.5f;
    public int shipCid;
    public int skillId;
    public int skillLevel;
    public int skillType;
    public StrengthenAttribute strengthenAttribute;
    public int uid;

    public static ShipBrokenType GetBrokenTypeOf(int hp, int hpMax)
    {
        if (hpMax == 0)
        {
            return ShipBrokenType.noBroken;
        }
        float num = (hp * 1f) / ((float) hpMax);
        if (num >= 0.5f)
        {
            return ShipBrokenType.noBroken;
        }
        if (num >= 0.25f)
        {
            return ShipBrokenType.middleBroken;
        }
        return ShipBrokenType.bigBorken;
    }

    public ShipBrokenType BrokenType
    {
        get
        {
            return GetBrokenTypeOf(this.battleProps.hp, this.battlePropsMax.hp);
        }
    }

    public int damageLevel
    {
        get
        {
            if (this.battlePropsMax.hp == 0)
            {
                this.battlePropsMax.hp = 1;
            }
            float num = (this.battleProps.hp * 1f) / ((float) this.battlePropsMax.hp);
            if (num >= 0.5f)
            {
                return 0;
            }
            if (num >= 0.25f)
            {
                return 1;
            }
            return 2;
        }
    }

    public bool HasAirPlane
    {
        get
        {
            return (this.MaxAluminium > 0);
        }
    }

    public bool IsBigBroken
    {
        get
        {
            return (GetBrokenTypeOf(this.battleProps.hp, this.battlePropsMax.hp) == ShipBrokenType.bigBorken);
        }
    }

    public bool IsFlagShip
    {
        get
        {
            if (this.fleetId <= 0)
            {
                return false;
            }
            UserFleet fleetOfId = GameData.instance.GetFleetOfId(this.fleetId);
            if (fleetOfId == null)
            {
                return false;
            }
            return (((fleetOfId.ships != null) && (fleetOfId.ships.Length != 0)) && (fleetOfId.ships[0] == this.id));
        }
    }

    public bool IsInExplore
    {
        get
        {
            if (this.fleetId <= 0)
            {
                return false;
            }
            return GameData.instance.IsFleetInExplore(this.fleetId);
        }
    }

    public bool IsInRepair
    {
        get
        {
            return GameData.instance.IsShipInReapir(this.id);
        }
    }

    public bool IsLocked
    {
        get
        {
            return (this.isLocked == 1);
        }
    }

    public bool IsOverMiddleBroken
    {
        get
        {
            ShipBrokenType brokenTypeOf = GetBrokenTypeOf(this.battleProps.hp, this.battlePropsMax.hp);
            return ((brokenTypeOf == ShipBrokenType.middleBroken) || (brokenTypeOf == ShipBrokenType.bigBorken));
        }
    }

    public bool IsShipNeedRepair
    {
        get
        {
            return (GetBrokenTypeOf(this.battleProps.hp, this.battlePropsMax.hp) != ShipBrokenType.noBroken);
        }
    }

    public bool IsShipNeedSupply
    {
        get
        {
            return ((((this.maxProps.oil - this.props.oil) + (this.maxProps.ammo - this.props.ammo)) + (this.MaxAluminium - this.OwnAluminium)) > 0);
        }
    }

    public int MaxAirPlans
    {
        get
        {
            if ((this.capacitySlotMax == null) || (this.capacitySlotMax.Length == 0))
            {
                return 0;
            }
            int num = 0;
            int length = this.capacitySlotMax.Length;
            int num3 = this.capacitySlotExist.Length;
            for (int i = 0; i < length; i++)
            {
                if ((num3 > i) && (this.capacitySlotExist[i] == 1))
                {
                    num += this.capacitySlotMax[i];
                }
            }
            return num;
        }
    }

    public int MaxAluminium
    {
        get
        {
            return this.maxProps.aluminium;
        }
    }

    public ShipBattleProps maxProps
    {
        get
        {
            return this.battlePropsMax;
        }
    }

    public int NeedSupplyAmount
    {
        get
        {
            return (((this.maxProps.oil - this.props.oil) + (this.maxProps.ammo - this.props.ammo)) + (this.MaxAluminium - this.OwnAluminium));
        }
    }

    public int OwnAirplans
    {
        get
        {
            if ((this.capacitySlot == null) || (this.capacitySlot.Length == 0))
            {
                return 0;
            }
            int num = 0;
            int length = this.capacitySlot.Length;
            int num3 = this.capacitySlotExist.Length;
            for (int i = 0; i < length; i++)
            {
                if ((num3 > i) && (this.capacitySlotExist[i] == 1))
                {
                    num += this.capacitySlot[i];
                }
            }
            return num;
        }
    }

    public int OwnAluminium
    {
        get
        {
            return this.props.aluminium;
        }
    }

    public ShipBattleProps props
    {
        get
        {
            return this.battleProps;
        }
    }

    public int RepairOilNeeds
    {
        get
        {
            int num = Mathf.CeilToInt(this.ship.repairOilModulus * (this.battlePropsMax.hp - this.battleProps.hp));
            if (this.props.hp == 0)
            {
                num *= 2;
            }
            return num;
        }
    }

    public int RepairSteelNeeds
    {
        get
        {
            int num = Mathf.CeilToInt(this.ship.repairSteelModulus * (this.battlePropsMax.hp - this.battleProps.hp));
            if (this.props.hp == 0)
            {
                num *= 2;
            }
            return num;
        }
    }

    public int RepairTimeSecondsNeeds
    {
        get
        {
            float num = 0f;
            if (this.level >= 11)
            {
                num = (Mathf.Sqrt((float) (this.level - 11)) * 10f) + 50f;
            }
            float f = ((((this.level * 5) + num) * this.ship.repairTime) * (this.battlePropsMax.hp - this.battleProps.hp)) + 30f;
            if (this.married == 1)
            {
                f *= 0.7f;
            }
            return Mathf.CeilToInt(f);
        }
    }

    public ShipConfig ship
    {
        get
        {
            if (this._ship == null)
            {
                this._ship = AllShipConfigs.instance.getShip(this.shipCid);
            }
            return this._ship;
        }
    }

    public int expNeedAtk
    {
        get
        {
            return this.ship.strengthenTop.atk - this.strengthenAttribute.atk;
        }
    }

    public int expNeedTorpedo
    {
        get
        {
            return this.ship.strengthenTop.torpedo - this.strengthenAttribute.torpedo;
        }
    }

    public int expNeedDef
    {
        get
        {
            return this.ship.strengthenTop.def - this.strengthenAttribute.def;
        }
    }

    public int expNeedAirdef
    {
        get
        {
            return this.ship.strengthenTop.air_def - this.strengthenAttribute.air_def;
        }
    }
}

