using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;

public partial class RepairForm1 : Form
{
    private int dockid;
    public RepairForm1(int dock)
    {
        dockid = dock;
        InitializeComponent();
    }

    public void setdock_id(int dock)
    {
        dockid = dock;

        GridColumn column1 = selshiplist.PrimaryGrid.Columns[4];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column3 = selshiplist.PrimaryGrid.Columns[7];
        column3.EditorType = typeof(RepairShipButton);
        column3.EditorParams = new object[] { dockid, this };

        updateShipList();
    }

    private void updateShipList()
    {
        GridPanel panel = selshiplist.PrimaryGrid;
        panel.Rows.Clear();

        int i = 0;
        foreach (UserShip us in GameData.instance.UserShips)
        {

            if (us.battleProps.hp < us.battlePropsMax.hp)
            {
                var gr = getonerow(us);
                panel.Rows.Add(gr);
                i++;
            }
        }


    }

    public static GridRow getonerow(UserShip us)
    {
        object[] vals = new object[19];

        vals[0] = us.fleetId > 0 ? (GameData.instance.UserFleets[us.fleetId - 1].title) : "";
        vals[1] = us.level;
        vals[2] = us.ship.title;
        vals[3] = tools.helper.getshiptype(us.ship.type);
        vals[4] = tools.helper.getShipSmallImage(us);
        vals[5] = 100 * us.battleProps.hp / us.battlePropsMax.hp;
        vals[6] = "" + us.battleProps.hp + "/" + us.battlePropsMax.hp;
        vals[7] = "修理";// + "/" + us.battlePropsMax.atk;
        vals[8] = us.id;

        GridRow gr = new GridRow(vals);
        gr.RowHeight = 30;
        return gr;
    }
    internal class RepairShipButton : GridButtonXEditControl
    {
        private int dockid;
        private RepairForm1 form;
        public RepairShipButton(int dock, RepairForm1 par)
        {
            dockid = dock;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[8].Value;

            UserShip us = GameData.instance.GetShipById(shipid);

            if (us == null)
            {
                MessageBox.Show("错误的船只信息");
                form.Close();
            }

            UserFleet uf = GameData.instance.GetFleetOfId(us.fleetId);
            if (us.IsInRepair || us.IsInExplore || z.instance.isInBattle())
            {
                MessageBox.Show("请求的少女正在洗澡..远征,或者有少女正在战斗中....无法修理");
                return;
            }
            ServerRequestManager.instance.StartRepair(shipid, dockid);
            ServerRequestManager.instance.refreashUIData();
            form.Close();
        }

        #endregion
    }

}