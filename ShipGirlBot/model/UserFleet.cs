using System;
using System.Collections.Generic;

public class UserFleet
{
    public FleetFormation formation;
    public int id;
    public int level;
    public int[] ships;
    public int status;
    public string title;
    public string uid;

    public List<UserShip> GetUserShips()
    {
        if ((this.ships == null) || (this.ships.Length == 0))
        {
            return new List<UserShip>();
        }
        List<UserShip> list = new List<UserShip>();
        GameData instance = GameData.instance;
        for (int i = 0; i < this.ships.Length; i++)
        {
            list.Add(instance.GetShipById(this.ships[i]));
        }
        return list;
    }
}

