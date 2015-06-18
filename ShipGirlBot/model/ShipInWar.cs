using System;

public class ShipInWar
{
    private ShipConfig _config;
    private bool _isFlagShip = false;
    private ShipBrokenType _lastBorkenType;
    public int airDef;
    public int antisub;
    public int atk;
    public int capacity;
    public int[] capacitySlot;
    public int[] capacitySlotMax;
    public int def;
    public EquipInShip[] equipment;
    public int hp;
    public int hpMax;
    public int id;
    public int indexInFleet;
    public int level;
    public int married;
    public int miss;
    public int radar;
    public int range;
    public int shipCid;
    public float speed;
    public int star;
    public string title;
    public int torpedo;
    public int type;

    public void InitBrokenType()
    {
        this._lastBorkenType = UserShip.GetBrokenTypeOf(this.hp, this.hpMax);
    }

    public int OpenAirFightDropedPlaneNumber(int max)
    {
        Random r = new Random();
        return r.Next(0, max);
    }

    public void UpdateHP(int amount)
    {
        this.hp += amount;
        if (this.hp < 0)
        {
            this.hp = 0;
        }
    }

    public ShipBrokenType BrokenType
    {
        get
        {
            return UserShip.GetBrokenTypeOf(this.hp, this.hpMax);
        }
    }

    public bool CanDoAirAttack
    {
        get
        {
            if ((this.equipment != null) && (this.equipment.Length != 0))
            {
                foreach (EquipInShip ship in this.equipment)
                {
                    if (ship.CanDoAirAttack)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public bool CheckIfNeedShowAsFirstBroken
    {
        get
        {
            if ((this._lastBorkenType == ShipBrokenType.middleBroken) || (this._lastBorkenType == ShipBrokenType.bigBorken))
            {
                return false;
            }
            this._lastBorkenType = UserShip.GetBrokenTypeOf(this.hp, this.hpMax);
            return ((this._lastBorkenType == ShipBrokenType.middleBroken) || (this._lastBorkenType == ShipBrokenType.bigBorken));
        }
    }

    public bool IsFlagShip
    {
        get
        {
            return (this.indexInFleet == 0);
        }
    }

    public bool IsOverMiddleBroken
    {
        get
        {
            ShipBrokenType brokenTypeOf = UserShip.GetBrokenTypeOf(this.hp, this.hpMax);
            return ((brokenTypeOf == ShipBrokenType.middleBroken) || (brokenTypeOf == ShipBrokenType.bigBorken));
        }
    }

    public ShipConfig ship
    {
        get
        {
            if (this._config == null)
            {
                this._config = AllShipConfigs.instance.getShip(this.shipCid);
            }
            return this._config;
        }
    }
}

