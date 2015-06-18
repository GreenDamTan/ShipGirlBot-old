using System;

public class UserEquipment
{
    private EquipmentConfig _config;
    public long createTime;
    public int equipmentCid;
    public int id;
    public int level;
    public int num;
    public int objId;
    public int status;
    public string uid;

    public EquipmentConfig config
    {
        get
        {
            if (this._config == null)
            {
                this._config = GameConfigs.instance.GetEquipmentByCid(this.equipmentCid);
            }
            return this._config;
        }
    }
}

