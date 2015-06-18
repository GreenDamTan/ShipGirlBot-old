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


public partial class DisEquipConf : Form
{

    List<ShipPropsType> type_order_list = new List<ShipPropsType> {
            ShipPropsType.Atk, 
            ShipPropsType.Torpedo, 
            ShipPropsType.AircraftAtk, 
            ShipPropsType.AirDef, 
            ShipPropsType.Antisub,
            ShipPropsType.Radar,
            ShipPropsType.Hit,
            ShipPropsType.Miss,
            ShipPropsType.Range,
            ShipPropsType.Luck,
            ShipPropsType.Def };

    public DisEquipConf()
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
        var dlist = tools.configmng.instance.getDisEquipList();

        foreach (var eckv in GameConfigs.instance.AllEquipmentConfig)
        {
            var ec = eckv.Value;
            if (ec != null && dlist.ContainsKey(ec.cid.ToString()) == false)
            {

                List<int> vallist = new List<int> {
                    ec.atk,
                    ec.torpedo,
                    ec.aircraftAtk,
                    ec.airDef,
                    ec.antisub,
                    ec.radar,
                    ec.hit,
                    ec.miss,
                    ec.range,
                    ec.luck,
                    ec.def
                };

                string[] props = new string[4] { "", "","","" };
                int count = 0;
                for (int j = 0; (j < vallist.Count) && (count < 4); j++)
                {
                    if (vallist[j] != 0)
                    {
                        props[count] = tools.helper.getShipProptypestring(type_order_list[j]) + ":" + tools.helper.getShipProptypeval(type_order_list[j], vallist[j]);
                        count++;
                    }
                }

                object[] vals = new object[6];


                vals[0] = "添加";
                vals[1] = tools.helper.getstartstring(ec.star);
                vals[2] = tools.helper.getEquipmentTypeString(ec.type);
                vals[3] = ec.title + "\r\n" + props[0] + "\r\n" + props[1] + "\r\n" + props[2] + "\r\n" + props[3];
                vals[4] = tools.helper.getEquipmentImage(ec);
                vals[5] = ec.cid;

                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 60;
            }
        }
    }

    private void updateDisList()
    {

        GridPanel panel = dislist.PrimaryGrid;
        panel.Rows.Clear();

        var dlist = tools.configmng.instance.getDisEquipList();

        foreach (var cid in dlist.Keys)
        {
            try
            {
                EquipmentConfig ec = GameConfigs.instance.GetEquipmentByCid(int.Parse(cid));


                List<int> vallist = new List<int> {
                    ec.atk,
                    ec.torpedo,
                    ec.aircraftAtk,
                    ec.airDef,
                    ec.antisub,
                    ec.radar,
                    ec.hit,
                    ec.miss,
                    ec.range,
                    ec.luck,
                    ec.def
                };

                string[] props = new string[4] { "", "", "","" };
                int count = 0;
                for (int j = 0; (j < vallist.Count) && (count < 4); j++)
                {
                    if (vallist[j] != 0)
                    {
                        props[count] = tools.helper.getShipProptypestring(type_order_list[j]) + ":" + tools.helper.getShipProptypeval(type_order_list[j], vallist[j]);
                        count++;
                    }
                }

                if (ec != null)
                {
                    object[] vals = new object[6];

                    vals[0] = tools.helper.getstartstring(ec.star);
                    vals[1] = tools.helper.getEquipmentTypeString(ec.type);
                    vals[2] = ec.title + "\r\n" + props[0] + "\r\n" + props[1] + "\r\n" + props[2] + "\r\n" + props[3];
                    vals[3] = tools.helper.getEquipmentImage(ec);
                    vals[4] = "移除";
                    vals[5] = ec.cid;

                    GridRow gr = new GridRow(vals);
                    panel.Rows.Add(gr);
                    gr.RowHeight = 90;
                }
            }catch(Exception e)
            {

            }

        }
    }
    internal void tryaddtodis(int rowindex, int equipcid)
    {
        GridPanel p = nondislist.PrimaryGrid;
        int cid = (int)p.GetCell(rowindex, 5).Value;

        EquipmentConfig ec = GameConfigs.instance.GetEquipmentByCid(cid);

        if (cid == equipcid)
        {
            object[] vals = new object[6];

            vals[0] = p.GetCell(rowindex, 1).Value;
            vals[1] = p.GetCell(rowindex, 2).Value;
            vals[2] = p.GetCell(rowindex, 3).Value;
            vals[3] = tools.helper.getEquipmentImage(ec);
            vals[4] = "移除";
            vals[5] = cid;

            GridRow gr = new GridRow(vals);
            dislist.PrimaryGrid.Rows.Add(gr);
            gr.RowHeight = 90;

            p.Rows.Remove(p.Rows[rowindex]);
        }
        //var sc = AllShipConfigs.instance.getShip(cid);
        tools.configmng.instance.adddisequip(cid, tools.helper.getstartstring(ec.star)+ " " + ec.title);
        //EditorCell.GridRow.Cells[1].CellStyles.Default.Background = new DevComponents.DotNetBar.SuperGrid.Style.Background(s == "吃掉" ? Color.White : Color.LightGreen);
    }

    internal void tryremovefromdis(int rowindex, int equipcid)
    {
        GridPanel p = dislist.PrimaryGrid;
        int cid = (int)p.GetCell(rowindex, 5).Value;

        if (cid == equipcid)
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
            gr.RowHeight = 60;

            p.Rows.Remove(p.Rows[rowindex]);
        }
        tools.configmng.instance.removedisequip(cid);
    }

    internal class AddtoDisBTN : GridButtonXEditControl
    {
        private DisEquipConf form;
        public AddtoDisBTN(DisEquipConf par)
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
        private DisEquipConf form;
        public RemoveFromDisBTN(DisEquipConf par)
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