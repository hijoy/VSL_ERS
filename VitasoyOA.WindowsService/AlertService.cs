using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VitasoyOA.WindowsService
{
    partial class AlertService : ServiceBase
    {
        //private string _domain;
        //private string _rootPath;
        //private string _siteUrl;
        //private string _groupName;
        //private string _userListName;
        //private string _userinfoListName;

        public AlertService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //_domain = ConfigurationManager.AppSettings["AD.Domain"];
            //_rootPath = ConfigurationManager.AppSettings["AD.RootPath"];
            //_siteUrl = ConfigurationManager.AppSettings["WSS.SiteURL"];
            //_groupName = ConfigurationManager.AppSettings["WSS.GroupName"];
            //_userListName = ConfigurationManager.AppSettings["WSS.UserListName"];
            //_userinfoListName = ConfigurationManager.AppSettings["WSS.UserInfoListName"];

            Timer();
        }

        private void Timer()
        {
            int interval = Convert.ToInt32(ConfigurationManager.AppSettings["EmailAlert.TimeInterval"]);
            System.Timers.Timer timer1 = new System.Timers.Timer(1000 * interval);//每隔 n 秒做一次check
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(AlertEvent);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void AlertEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            int runat = Int32.Parse(ConfigurationManager.AppSettings["EmailAlert.RunAt"]);
            if (Int32.Parse(DateTime.Now.ToString("HH")) == runat)
            {
                if (!EmailAlert.IsRunning)
                {
                    try
                    {
                        Utility.WriteLog("***Start alert service ***");

                        EmailAlert.IsRunning = true;
                        EmailAlert alert = new EmailAlert();

                        //run alert main function here
                        alert.SendAlertToApprovers();

                        Utility.WriteLog("***End alert service***");
                    }
                    catch (Exception ex)
                    {
                        Utility.WriteLog(ex.ToString());
                    }
                    finally
                    {
                        EmailAlert.IsRunning = false;
                    }
                }
            }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
