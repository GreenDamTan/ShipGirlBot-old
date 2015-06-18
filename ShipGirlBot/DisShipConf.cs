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


public partial class DisShipConf : Form
{
    public DisShipConf()
    {
        InitializeComponent();

        GridColumn column1 = dislist.PrimaryGrid.Columns[3];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column3 = dislist.PrimaryGrid.Columns[4];
        column3.EditorType = typeof(RemoveFromDisBTN);
        column3.EditorParams = new object[] { this };


        GridColumn column11 = nondislist.PrimaryGrid.Columns[4];
        if (column11.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column11.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column11.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column11.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }

        GridColumn column33 = nondislist.PrimaryGrid.Columns[0];
        column33.EditorType = typeof(AddtoDisBTN);
        column33.EditorParams = new object[] { this };

        updateDisList();
        updateNonDisList();
    }


    private void updateNonDisList()
    {

        GridPanel panel = nondislist.PrimaryGrid;
        panel.Rows.Clear();

        foreach (var cid in AllShipConfigs.instance.ShipCids)
        {
            ShipConfig sc = AllShipConfigs.instance.getShip(cid);
            var dlist = tools.configmng.instance.getDisShipList();
            if (sc != null && dlist.ContainsKey(sc.cid.ToString()) == false && sc.cid < 20000000)
            {
                object[] vals = new object[6];

                UserShip us = new UserShip();
                us.shipCid = sc.cid;
                us.level = 1;
                us.battleProps = new ShipBattleProps();
                us.battlePropsMax = new ShipBattleProps();
                us.battleProps.hp = 1;
                us.battlePropsMax.hp = 1;

                vals[0] = "添加";
                vals[1] = tools.helper.getstartstring(sc.star);
                vals[2] = tools.helper.getshiptype(sc.type);
                vals[3] = sc.title;
                vals[4] = null;//tools.helper.getShipSmallImage(us);
                vals[5] = sc.cid;

                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 30;
            }
        }
    }

    private void updateDisList()
    {

        GridPanel panel = dislist.PrimaryGrid;
        panel.Rows.Clear();

        var dlist = tools.configmng.instance.getDisShipList();

        foreach (var cid in dlist.Keys)
        {
            try
            {
                ShipConfig sc = AllShipConfigs.instance.getShip(int.Parse(cid));

                if (sc != null)
                {
                    object[] vals = new object[6];

                    UserShip us = new UserShip();
                    us.shipCid = sc.cid;
                    us.level = 1;
                    us.battleProps = new ShipBattleProps();
                    us.battlePropsMax = new ShipBattleProps();
                    us.battleProps.hp = sc.hp;
                    us.battlePropsMax.hp = sc.hpMax;


                    vals[0] = tools.helper.getstartstring(sc.star);
                    vals[1] = tools.helper.getshiptype(sc.type);
                    vals[2] = sc.title;
                    vals[3] = tools.helper.getShipSmallImage(us);
                    vals[4] = "移除";
                    vals[5] = sc.cid;

                    GridRow gr = new GridRow(vals);
                    panel.Rows.Add(gr);
                    gr.RowHeight = 30;
                }
            }catch(Exception e)
            {

            }

        }
    }
    internal void tryaddtodis(int rowindex, int shipcid)
    {
        GridPanel p = nondislist.PrimaryGrid;
        int cid = (int)p.GetCell(rowindex, 5).Value;

        ShipConfig sc = AllShipConfigs.instance.getShip(cid);

        if(cid == shipcid)
        {
            object[] vals = new object[6];
            UserShip us = new UserShip();
            us.shipCid = sc.cid;
            us.level = 1;
            us.battleProps = new ShipBattleProps();
            us.battlePropsMax = new ShipBattleProps();
            us.battleProps.hp = sc.hp;
            us.battlePropsMax.hp = sc.hpMax;

            vals[0] = p.GetCell(rowindex, 1).Value;
            vals[1] = p.GetCell(rowindex, 2).Value;
            vals[2] = p.GetCell(rowindex, 3).Value;
            vals[3] = tools.helper.getShipSmallImage(us);
            vals[4] = "移除";
            vals[5] = cid;

            GridRow gr = new GridRow(vals);
            dislist.PrimaryGrid.Rows.Add(gr);
            gr.RowHeight = 30;

            p.Rows.Remove(p.Rows[rowindex]);
        }
        //var sc = AllShipConfigs.instance.getShip(cid);
        tools.configmng.instance.adddisship(cid, tools.helper.getstartstring(sc.star)+ " " + sc.title);
        //EditorCell.GridRow.Cells[1].CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(s == "吃掉" ? Color.White : Color.LightGreen);
    }

    internal void tryremovefromdis(int rowindex, int shipcid)
    {
        GridPanel p = dislist.PrimaryGrid;
        int cid = (int)p.GetCell(rowindex, 5).Value;

        if (cid == shipcid)
        {
            object[] vals = new object[6];

            vals[0] = "添加";
            vals[1] = p.GetCell(rowindex, 0).Value;
            vals[2] = p.GetCell(rowindex, 1).Value;
            vals[3] = p.GetCell(rowindex, 2).Value;
            vals[4] = p.GetCell(rowindex, 3).Value;
            
            vals[5] = cid;

            GridRow gr = new GridRow(vals);
            nondislist.PrimaryGrid.Rows.Add(gr);
            gr.RowHeight = 30;

            p.Rows.Remove(p.Rows[rowindex]);
        }
        tools.configmng.instance.removedisship(cid);
    }

    internal class AddtoDisBTN : GridButtonXEditControl
    {
        private DisShipConf form;
        public AddtoDisBTN(DisShipConf par)
        {
            form = par;

            this.Click += CheckStateChangedCallback;
        }

        #region CheckStateChangedCallback

        void CheckStateChangedCallback(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[5].Value;
            form.tryaddtodis(EditorCell.RowIndex,shipid);


           

            //EditorCell.GridRow.Cells[1].CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(s == "吃掉" ? Color.White : Color.LightGreen);
        }

        #endregion
    }


    internal class RemoveFromDisBTN : GridButtonXEditControl
    {
        private DisShipConf form;
        public RemoveFromDisBTN(DisShipConf par)
        {
            form = par;

            this.Click += CheckStateChangedCallback;
        }

        #region CheckStateChangedCallback

        void CheckStateChangedCallback(object sender, EventArgs e)
        {
            int shipid = (int)EditorCell.GridRow.Cells[5].Value;
            form.tryremovefromdis(EditorCell.RowIndex, shipid);


            
        }

        #endregion
    }
}