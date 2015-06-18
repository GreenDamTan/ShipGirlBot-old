using System;
using System.Collections.Generic;
////using UnityEngine;

public class AllShipConfigs
{
    private List<int> _shipCids;
    private static AllShipConfigs mInstance;
    private Dictionary<int, ShipConfig> shipCardIndexConfig;
    private Dictionary<int, ShipConfig> ships;

    public ShipConfig getShip(int cid)
    {
        if (this.ships.ContainsKey(cid))
        {
            return this.ships[cid];
        }
        return null;
    }

    public ShipConfig getShipAtIndex(int index)
    {
        if (this.shipCardIndexConfig.ContainsKey(index))
        {
            return this.shipCardIndexConfig[index];
        }
        return null;
    }

    public bool IsCardReleased(int index)
    {
        ShipConfig config = this.getShipAtIndex(index);
        return ((config != null) && (config.release == 1));
    }

    public void setShips(ShipConfig[] shipList)
    {
        this.ships = new Dictionary<int, ShipConfig>();
        this.shipCardIndexConfig = new Dictionary<int, ShipConfig>();
        this._shipCids = new List<int>();
        foreach (ShipConfig config in shipList)
        {
            this._shipCids.Add(config.cid);
            this.ships[config.cid] = config;
            this.shipCardIndexConfig[config.shipIndex] = config;
        }
    }

    public static AllShipConfigs instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new AllShipConfigs();
            }
            return mInstance;
        }
    }

    public ShipConfig RandomShip
    {
        get
        {
            Random r = new Random();
            return this.ships[this._shipCids[r.Next(0, this._shipCids.Count)]];
        }
    }

    public List<int> ShipCids
    {
        get
        {
            return this._shipCids;
        }
    }

    internal ShipConfig getShipByName(string p)
    {
        foreach(var s in this.ships)
        {
            if(s.Value.title == p)
            {
                return s.Value;
            }
        }
        return null;
    }
}

