using System;

public class EquipInShip
{
    public int equipmentCid;
    public int id;
    public int level;

    public bool CanDoAirAttack
    {
        get
        {
            if (this.equipmentCid <= 0)
            {
                return false;
            }
            EquipmentConfig config = this.config;
            if (config == null)
            {
                return false;
            }
            EquipmentType type = config.type;
            return (((type == EquipmentType.AttackPlane) || (type == EquipmentType.XAttackPlane)) || (type == EquipmentType.FightPlane));
        }
    }

    public EquipmentConfig config
    {
        get
        {
            if (this.equipmentCid <= 0)
            {
                return null;
            }
            return GameConfigs.instance.GetEquipmentByCid(this.equipmentCid);
        }
    }
}

