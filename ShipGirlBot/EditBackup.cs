using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
public partial class EditBackup : Form
{
    public EditBackup()
    {
        InitializeComponent();
        GameInfo.instance.UA = z.instance.getphone_choose();

        var rs = ServerRequestManager.instance.CheckIfHaveNewVerison();
        rs.Start();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (int scid in AllShipConfigs.instance.ShipCids)
        {
            DataServer.instance.cookie = "";
            DataServer.instance.ChoosedServerAddress = "http://s8.zj.p7game.com/";
            if (i >= integerInput1.Value && i < integerInput1.Value + 10)
            {

                ShipConfig ssc = AllShipConfigs.instance.getShip(scid);
                System.Threading.Thread.Sleep(3000);
                var reg = ServerRequestManager.instance.DoRegister("ohtomatok48" + i, "test123", "11133322344566", "", "p777");
                //var reg = ServerRequestManager.instance.QuickRegister(tools.configmng.deviceUniqueIdentifier + "zd" + i, "");
                //System.Threading.Thread.Sleep(3000);
                //if (reg != null && reg.loginResponse.eid != 0)
                //{
                //    reg = ServerRequestManager.instance.DoRegister("ohtomatok38" + i, "test123", "13800138000", "", "p777");
                //}

                if (reg != null && reg.loginResponse != null && reg.loginResponse.eid == 0)
                {
                    string uid = reg.loginResponse.userId;
                    System.Threading.Thread.Sleep(3000);
                    ServerRequestManager.instance.ChooseInitShipAndName("heiheitoma48" + i, ssc.cid.ToString());
                    System.Threading.Thread.Sleep(3000);
                    ServerRequestManager.instance.Login(uid);
                    System.Threading.Thread.Sleep(3000);
                    ServerRequestManager.instance.GetInitData();
                    UserShip us = GameData.instance.UserShips.First();

                    z.log("[reg] " + ssc.cid + " " + ssc.title + " => " + us.ship.cid + " " + us.ship.title);
                    addresult(us, ssc);


                }


                System.Threading.Thread.Sleep(3000);
            }

            i++;
        }
        integerInput1.Value = integerInput1.Value + 10;
    }


    private void addresult(UserShip us, ShipConfig sc)
    {
        var panel = dislist.PrimaryGrid;
        if (sc != null)
        {
            object[] vals = new object[6];

            vals[0] = "" + sc.cid + "-" + sc.title;
            vals[1] = tools.helper.getstartstring(sc.star);
            vals[2] = tools.helper.getshiptype(sc.type);
            vals[3] = sc.title;
            vals[4] = tools.helper.getShipSmallImage(us);
            vals[5] = sc.cid;

            GridRow gr = new GridRow(vals);
            panel.Rows.Add(gr);
            gr.RowHeight = 30;
        }
    }
}