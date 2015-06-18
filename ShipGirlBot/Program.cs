
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.ExceptionServices;
static class Program
{
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    [HandleProcessCorruptedStateExceptions]  
    static void Main()
    {


        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

        var zzz = new z();
        bool createdNew;
#if DEBUG || RELEASE_TEST

#else

        Mutex instance = new Mutex(true, zzz.GetType().ToString(), out createdNew);

        if (!createdNew)
        {
            MessageBox.Show("不能多开");
            return;
        }
#endif
        try
            {


                Application.Run(zzz);
                //Application.Run(new ShipForm());
            }
            catch (Exception e)
            {
                write_dump(e.ToString());
            }
        //var f = new CommandForm();
        //Application.Run(f);
    }

    static public void write_dump(string desc)
    {
        string fname = "MiniDmp_" + DateTime.Now.ToString("yyyy-MM-dd_hh_mm_ss") + ".dmp";

        MessageBox.Show("发生未知错误，崩溃了，请将此窗口截图发送；如有必要，也请发送" + fname + " 以便修错误\r\n "+ desc);

            var dic = new Dictionary<string,string>();
            dic["msg"]=desc;
            var c = new System.Net.Http.FormUrlEncodedContent(dic);
            
            try
            {
                            var p = new System.Net.Http.HttpClient();
                var r = p.PostAsync(tools.helper.count_server_addr + "/sssgbsssgb/logerror", c).Result;
            }
            catch(Exception)
            {

            }

        using (FileStream fs = new FileStream(fname, FileMode.Create, FileAccess.ReadWrite, FileShare.Write))
        {
            MiniDump.Write(fs.SafeFileHandle, MiniDump.Option.Normal);
        }
    }

    [HandleProcessCorruptedStateExceptions]  
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = e.ExceptionObject as Exception;
        write_dump(ex.ToString());
    }


    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        write_dump(e.Exception.ToString());
    }
}