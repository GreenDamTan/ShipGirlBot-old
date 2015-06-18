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
public partial class DropCount : Form
{
    System.Net.Http.HttpClient p = null;
    public DropCount()
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

    private void updateNodeList(bool viewbywintype)
    {
        string url = tools.helper.count_server_addr + "/analyze/" + (viewbywintype ? "weapon/weaponlist" : "ship/shiplist") + ".json";
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
        updateDrop(nodename,checkBox1.Checked);
    }

    private void updateDrop(string nodename, bool viewbywintype)
    {
        try
        {

            var response = p.GetAsync(tools.helper.count_server_addr + "/analyze/" + (viewbywintype ? "weapon/weapon_" : "ship/ship_") + nodename + ".json").Result;
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


            foreach (var cl in list.nodeval)
            {
                object[] vals = new object[6];

                vals[0] = cl.name;
                vals[1] = cl.count;
                float c = (float)cl.count;
                vals[2] = "" + (c * 100.0f / t) + "%";

                GridRow gr = new GridRow(vals);
                panel.Rows.Add(gr);
                gr.RowHeight = 20;

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
