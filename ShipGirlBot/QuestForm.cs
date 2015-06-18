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

public partial class QuestForm : Form
{
    private Dictionary<int, UserQuest> quest_dic = new Dictionary<int, UserQuest>();

    public QuestForm()
    {
        InitializeComponent();

        GridColumn c11 = questlist.PrimaryGrid.Columns[6];
        if (c11 != null)
        {
            c11.EditorType = typeof(QuestCommandButton);
            c11.EditorParams = new object[] { this };
        }

        GridColumn c2 = questlist.PrimaryGrid.Columns[3];
        if (c2 != null)
        {
            c2.EditorType = typeof(GridTextBoxXEditControl);
            (c2.EditControl as GridTextBoxXEditControl).Multiline = true;
            (c2.EditControl as GridTextBoxXEditControl).WordWrap = true;
            (c2.RenderControl as GridTextBoxXEditControl).Multiline = true;
            (c2.RenderControl as GridTextBoxXEditControl).WordWrap = true;
        }



        refreshquestUi();

    }

    internal void tryFinishQuest(int questid)
    {
        if (quest_dic.ContainsKey(questid))
        {
            UserQuest uq = this.quest_dic[questid];
            if (uq == null || uq.IsFinished == false)
            {
                return;
            }

            var ret = ServerRequestManager.instance.FinishQuest(uq);
            if (ret != null && ret.responseData != null && ret.responseData.eid == 0)
            {
                z.log("[完成任务成功] " + uq.title);
                refreshquestUi();
            }


        }
    }

    private void refreshquestUi()
    {
        ServerRequestManager.instance.GetQuests();
        quest_dic.Clear();

        GridPanel panel = questlist.PrimaryGrid;
        panel.Rows.Clear();
        int i = 0;
        foreach (var q in GameData.instance.AllQuests)
        {
            if (quest_dic.ContainsKey(q.taskCid))
            {
                MessageBox.Show("任务Cid冲突...." + q.title);
            }
            quest_dic[q.taskCid] = q;

            object[] vals = new object[7];

            Regex reg = new Regex(@"\^\w{9}");
            Console.WriteLine();

            vals[0] = q.taskCid;
            vals[1] = q.title;
            vals[2] = tools.helper.getQuestTypeStr(q.type);
            vals[3] = reg.Replace(q.desc, "");
            vals[4] = tools.helper.getQuestConStr(q);
            vals[5] = tools.helper.getQuestRewardStr(q);
            vals[6] = q.IsFinished ? "完成" : "";

            GridRow gr = new GridRow(vals);
            gr.RowHeight = 50;

            panel.Rows.Add(gr);
            i++;
        }
    }

    

    internal class QuestCommandButton : GridButtonXEditControl
    {
        private int fleetid;
        private QuestForm expform;
        public QuestCommandButton(QuestForm f)
        {
            expform = f;
            Click += new EventHandler(CommandButtonClick);
        }

        #region CommandButtonClick

        void CommandButtonClick(object sender, EventArgs e)
        {

            int questid = (int)EditorCell.GridRow[0].Value;

            expform.tryFinishQuest(questid);
        }

        #endregion
    }

}
