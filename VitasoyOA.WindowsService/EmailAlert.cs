using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace VitasoyOA.WindowsService {
    public class EmailAlert {
        EmailTableAdapters.FormTableAdapter TAform = new EmailTableAdapters.FormTableAdapter();
        EmailTableAdapters.FormViewTableAdapter TAview = new EmailTableAdapters.FormViewTableAdapter();
        EmailTableAdapters.FormApplyViewTableAdapter TAApplyView = new EmailTableAdapters.FormApplyViewTableAdapter();
        EmailTableAdapters.StuffUserTableAdapter TAstuff = new EmailTableAdapters.StuffUserTableAdapter();
        EmailTableAdapters.EmailHistoryTableAdapter TAHisotry = new EmailTableAdapters.EmailHistoryTableAdapter();
        string style_td = "style='border:1px solid gray;width:200px;height:60px;text-align:center;'";
        string style_table = "style='border:1px solid gray;border-collapse:collapse;'";

        public static bool IsRunning = false;

        //the main func to send email to approvers
        public void SendAlertToApprovers() {
            DataTable dt = TAform.GetData();
            string userids = string.Empty;
            if (dt.Rows.Count > 0) {
                //先拼接成字符串
                foreach (DataRow dr in dt.Rows) {
                    userids += dr[0];
                }
                userids = userids.Replace("PP", "P");
                List<string> list = new List<string>();
                //筛选出userid
                string[] strUser = userids.Split('P');
                for (int i = 0; i < strUser.Length; i++) {
                    if (!list.Contains(strUser[i])) {
                        list.Add(strUser[i]);
                    }
                }
                string strBody = string.Empty;
                string strTitle = ConfigurationManager.AppSettings["EmailAlert.Subject"];
                string strFrom = ConfigurationManager.AppSettings["EmailAlert.SendFrom"];
                string strEmailUser = ConfigurationManager.AppSettings["EmailAlert.EmailUser"];
                string pid = ConfigurationManager.AppSettings["EmailAlert.EmailPwd"];
                string strServer = ConfigurationManager.AppSettings["EmailAlert.EmailServer"];
                int EmptyCount = 0;

                //根据userid发送邮件
                Email.EmailHistoryDataTable tbHistory = new Email.EmailHistoryDataTable();
                Email.EmailHistoryRow row;
                for (int n = 0; n < list.Count; n++) {
                    if (string.IsNullOrEmpty(list[n])) {
                        EmptyCount++;
                        continue;
                    } else {
                        string strAddress = TAstuff.GetDataByID(int.Parse(list[n]))[0].EMail;

                        strBody = GetBody(list[n]);
                        row = tbHistory.NewEmailHistoryRow();
                        row.SentTo = strAddress;
                        row.EmailContent = strBody;
                        row.SendDate = DateTime.Now;
                        row.Result = "Success " + n + "of" + list.Count + ", Empty:EmptyCount";
                        row.ResultType = 1;
                        try {
                            Utility.SendMail(strAddress, "", strTitle, strBody, strFrom, strEmailUser, pid, strServer);
                        } catch (Exception e) {
                            row.Result = "Failed:" + e.ToString();
                            row.ResultType = 0;
                            throw e;
                        }
                        tbHistory.AddEmailHistoryRow(row);
                        this.TAHisotry.Update(row);
                    }
                }
                this.TAHisotry.Update(tbHistory);
            }
        }

        public string GetBody(string userid) {
            StringBuilder sb = new StringBuilder();
            string Title = "";
            DataTable dt = TAview.GetDataByUserID(userid);//待审批
            DataTable dtComplete = TAApplyView.GetDataByUserID(int.Parse(userid));//待确认
            DataTable dtCompleteClone = dtComplete.Clone();

            sb.Append("<div>系统入口:<a href='" + ConfigurationManager.AppSettings["ERSEntry"] + "'>维他奶协同办公系统</a><div>");
            if (dt.Rows.Count > 0) {
                Title = "等待您审批的单据";
                sb.Append("<div>");
                sb.Append("<div>" + Title + "<div>");
                sb.Append(GetTable(dt));
                sb.Append("</div>");
            }

            if (dtComplete.Rows.Count > 0) {
                Title = "等待您确认的单据";
                //是否大于三天
                DateTime d1 = DateTime.Now;
                DateTime d2 = new DateTime();
                TimeSpan s1;
                foreach (DataRow dr in dtCompleteClone.Rows) {
                    d2 = Convert.ToDateTime(dr["PromotionBeginDate"]);
                    s1 = d1 - d2;
                    //当月的最后一天
                    if (d1.AddDays(1).Month != d1.Month) {
                        Title = "等待您确认的单据（当月）";
                        //只取执行日期在当月的数据
                        if (!(d2.Month == d1.Month && d2.Year == d1.Year)) {
                            dtComplete.Rows.Remove(dr);
                        }
                    }
                        //执行日期三天之内以及未开始的全部去掉
                   else if (s1.Days < 3) {
                        dtComplete.Rows.Remove(dr);
                    }
                }
                sb.Append("<div>");
                sb.Append("<div>" + Title + "<div>");
                sb.Append(GetTable(dtComplete));
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        public string GetTable(DataTable dt) {
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0) {
                sb.Append("<table " + style_table + ">");
                sb.Append("<tr>");
                sb.Append("<td " + style_td + ">单据类型</td>");
                sb.Append("<td " + style_td + ">单据编号</td>");
                sb.Append("<td " + style_td + ">申请人</td>");
                sb.Append("<td " + style_td + ">申请时间</td>");
                sb.Append("</tr>");
                foreach (DataRow dr in dt.Rows) {
                    sb.Append("<tr>");
                    sb.Append("<td " + style_td + ">" + dr["FormTypeName"] + "</td>");
                    sb.Append("<td " + style_td + ">" + dr["FormNo"] + "</td>");
                    sb.Append("<td " + style_td + ">" + dr["StuffName"] + "</td>");
                    sb.Append("<td " + style_td + ">" + Convert.ToDateTime(dr["SubmitDate"]).ToString("yyyy-MM-dd") + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
    }
}
