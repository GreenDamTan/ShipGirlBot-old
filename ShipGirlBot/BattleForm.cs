using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;



public partial class BattleForm : Form
{
    private int fleetid;

    public BattleForm()
    {
        InitializeComponent();
    }

    public GridPanel getLevelGrid()
    {
        return levellist.PrimaryGrid;
    }
    public GridPanel getShipGrid()
    {
        return backupshiplist.PrimaryGrid;
    }
    public void setFleetId(int id)
    {
        this.fleetid = id;

        GridColumn column1 = levellist.PrimaryGrid.Columns[4];
        if (column1.EditControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        if (column1.RenderControl.GetType().Equals(typeof(GridImageEditControl)))
        {
            (column1.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        }
        GridColumn column2 = levellist.PrimaryGrid.Columns[6];
        column2.EditorType = typeof(ChallengelevelButton);
        column2.EditorParams = new object[] { fleetid, this };

        GridColumn gc4 = backupshiplist.PrimaryGrid.Columns[4];

        (gc4.EditControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;
        (gc4.RenderControl as GridImageEditControl).ImageSizeMode = ImageSizeMode.Zoom;


        updateLevelInfo();
        updateBackupShipInfo();
    }

    private void updateLevelInfo()
    {
        GridPanel panel = levellist.PrimaryGrid;
        panel.Rows.Clear();

        int i = 0;

        

        foreach (PVELevel level in PVEConfigs.instance.Levels)
        {
            object[] vals = new object[9];
            PVEChapter pc = PVEConfigs.instance.GetChapter(level.pveId);

            vals[0] = level.id;
            vals[1] = level.title;
            vals[2] = pc.title;
            vals[3] = level.subTitle;
            vals[4] = "res/png/map_route/" + level.mapId + ".png";
            vals[5] = false;
            vals[6] = "出战";
            vals[7] = level.id;

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 150;

            panel.Rows.Add(gr);
            i++;
        }

        foreach (PVEEventLevel level in PVEConfigs.instance.EventLevels)
        {
            object[] vals = new object[9];


            vals[0] = level.id;
            vals[1] = level.title;
            vals[2] = tools.helper.getPVEEVENTAwardString(level);
            vals[3] = level.subTitle;
            vals[4] = "res/png/map_route/" + level.mapId + ".png";
            vals[5] = false;
            vals[6] = "出战";
            vals[7] = level.id;

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 150;

            panel.Rows.Add(gr);
            i++;
        }


    }

    private void updateBackupShipInfo()
    {
        GridPanel panel = backupshiplist.PrimaryGrid;
        panel.Rows.Clear();

        int i = 0;
        foreach (UserShip us in GameData.instance.UserShips.OrderByDescending(o => o.level).ToList())
        {
            if (us.IsInExplore || us.fleetId > 0)
            {
                continue;
            }
            object[] vals = new object[17];

            vals[0] = false;
            vals[1] = us.level;
            vals[2] = tools.helper.getshiptype(us.ship.type);
            vals[3] = us.ship.title;
            vals[4] = tools.helper.getShipSmallImage(us);//"res/png/head_n/" + us.ship.picId + ".png";

            vals[5] = us.battleProps.hp + "/" + us.battlePropsMax.hp;
            vals[6] = us.battleProps.def;
            vals[7] = us.battleProps.atk;
            vals[8] = us.battleProps.torpedo;
            vals[9] = us.battleProps.airDef;
            vals[10] = us.battleProps.antisub;
            vals[11] = us.battleProps.radar;
            vals[12] = us.battleProps.speed;
            vals[13] = us.battleProps.luck;
            vals[14] = +us.battleProps.miss;
            vals[15] = us.love;
            vals[16] = us.id;

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 30;

            panel.Rows.Add(gr);
            i++;
        }
    }

    internal class ChallengelevelButton : GridButtonXEditControl
    {
        private int fleetid;
        private BattleForm form;
        public ChallengelevelButton(int flid, BattleForm par)
        {
            fleetid = flid;
            form = par;

            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {
            int levelid = (int)EditorCell.GridRow.Cells[7].Value;

            bool autostart = (bool)EditorCell.GridRow.Cells[5].Value;
            List<int> bslist = new List<int>();
            for (int i = 0; i < form.getShipGrid().Rows.Count; i++)
            {
                bool c = (bool)form.getShipGrid().GetCell(i, 0).Value;
                if (c == true)
                {
                    bslist.Add((int)form.getShipGrid().GetCell(i, 16).Value);
                }
            }

            z.instance.tryStartNewBattle(fleetid, levelid, autostart, bslist);
            form.Close();
        }

        #endregion
    }

}