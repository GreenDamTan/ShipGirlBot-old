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
using System.Text;
using System.Text.RegularExpressions;

public partial class Analyze : Form
{
    System.Net.Http.HttpClient p = null;
    public Analyze()
    {
        InitializeComponent();

        var handler = new System.Net.Http.HttpClientHandler
        {

        };
        p = new System.Net.Http.HttpClient(handler);

        updateNodeList(false);


    }
    internal class NodeVal
    {
        public string name;
        public int count;
    }


    internal class NodeList
    {
        public NodeVal[] nodelist;
    }


    internal class Drop
    {
        public string name;
        public int count;
    }


    internal class NodeDrop
    {
        public Drop[] nodeval;
    }

    private void updateNodeList( bool  viewbywintype)
    {
        string url = tools.helper.count_server_addr + "/analyze/nodelist" + (viewbywintype ? "wt" : "") + ".json";
        try
        {

            var response = p.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsByteArrayAsync().Result;
            string ss = System.Text.Encoding.UTF8.GetString(content);
            NodeList list = new JsonFx.Json.JsonReader().Read<NodeList>(ss);


            GridPanel p1 = this.droplist.PrimaryGrid;
            p1.Rows.Clear();

            GridPanel panel = nodelist.PrimaryGrid;
            panel.Rows.Clear();

            foreach (var cl in list.nodelist)
            {
                object[] vals = new object[6];

                vals[0] = cl.name;
                vals[1] = cl.count;

                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 20;
            }

        }
        catch (Exception)
        {

        }


    }

    private void nodelist_RowClick(object sender, GridRowClickEventArgs e)
    {
        string nodename = (string)e.GridPanel.GetCell(e.GridRow.RowIndex, 0).Value;
        updateDrop(nodename);
    }

    private void updateDrop(string nodename)
    {
        try
        {

            var response = p.GetAsync(tools.helper.count_server_addr + "/analyze/" + nodename + ".json").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsByteArrayAsync().Result;
            string ss = System.Text.Encoding.UTF8.GetString(content);
            NodeDrop list = new JsonFx.Json.JsonReader().Read<NodeDrop>(ss);


            GridPanel panel = droplist.PrimaryGrid;
            panel.Rows.Clear();
            int totalcount = 0;
            foreach (var cl in list.nodeval)
            {
                totalcount += cl.count;
            }
            float t = (float)totalcount;
            
            float allfire = 0.0f;
            float alltorpedo = 0.0f;
            float alldef = 0.0f;
            float allantiair = 0.0f;

            float dist = t;
            float alloil = 0.0f;
            float allammo = 0.0f;
            float allsteel = 0.0f;
            float allal = 0.0f;

            foreach (var cl in list.nodeval)
            {
                object[] vals = new object[6];

                vals[0] = cl.name;
                vals[1] = cl.count;
                float c = (float)cl.count;
                vals[2] = ""+ (c * 100.0f /t) + "%";

                ShipConfig sc = AllShipConfigs.instance.getShipByName(cl.name);
                if(sc != null)
                {
                    allfire +=(float) (sc.strengthenSupplyExp.atk * cl.count);
                    alltorpedo += (float)(sc.strengthenSupplyExp.torpedo * cl.count);
                    alldef += (float)(sc.strengthenSupplyExp.def * cl.count);
                    allantiair += (float)(sc.strengthenSupplyExp.air_def * cl.count);

                    if(sc.star<=3)
                    {
                        alloil += (float)(sc.dismantle["2"] * cl.count);
                        allammo += (float)(sc.dismantle["3"] * cl.count);
                        allsteel += (float)(sc.dismantle["4"] * cl.count);
                        allal += (float)(sc.dismantle["9"] * cl.count);
                    }
                    else
                    {
                        dist -= cl.count;
                    }
                }
                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 20;

                label2.Text = "火力: +" + (allfire/t);
                label3.Text = "雷装: +" + (alltorpedo/t);
                label4.Text = "装甲: +" + (alldef/t);
                label5.Text = "防空: +" + (allantiair/t);


                label9.Text = "油: +" + (alloil / dist);
                label8.Text = "弹: +" + (allammo / dist);
                label6.Text = "钢: +" + (allsteel / dist);
                label7.Text = "铝: +" + (allal / dist);

            }

        }
        catch (Exception)
        {

        }

    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        updateNodeList(checkBox1.Checked);
    }

}