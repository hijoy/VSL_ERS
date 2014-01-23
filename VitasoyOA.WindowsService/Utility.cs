using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.ComponentModel;


namespace VitasoyOA.WindowsService {
    public class Utility {
        private const string LOGPATH = @"{0}Logs\{1}.log";
        //private const string LastTimeFilePath = @"{0}LastRunTime.config"; //纪录服务上一次运行时间

        public static void WriteLog(string message) {
            FileStream fs = null;
            try {
                string filename = String.Format(LOGPATH, System.AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd"));
                fs = File.Open(filename, FileMode.Append | FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.WriteLine(string.Format("{0}   {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
                }
                fs.Close();
            } catch {
                //wouldnt cause break just because cant write log
            } finally {
                if (fs != null)
                    fs.Close();
            }
        }

        public static void SendMail(string sendto, string cc, string subject, string body, string emailFrom, string emailUser, string password, string emailServer) {
            try {
                if (!string.IsNullOrEmpty(sendto) || !string.IsNullOrEmpty(cc)) {
                    MailMessage mail = new MailMessage();
                    string[] strs = sendto.Split(";；,，".ToCharArray());
                    for (int i = 0; i < strs.Length; i++) {
                        if (strs[i] != null && !"".Equals(strs[i].Trim()) && strs[i].IndexOf("@") > 0) {
                            mail.To.Add(strs[i]);
                        }
                    }
                    if (!string.IsNullOrEmpty(cc)) {
                        strs = cc.Split(";；,，".ToCharArray());
                        for (int i = 0; i < strs.Length; i++) {
                            if (strs[i] != null && !"".Equals(strs[i].Trim()) && strs[i].IndexOf("@") > 0) {
                                mail.CC.Add(strs[i]);
                            }
                        }
                    }

                    mail.From = new MailAddress(emailFrom);

                    mail.Subject = subject;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = body;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(emailUser, password);
                    smtpClient.Host = emailServer;

                    smtpClient.Send(mail);
                }
            } catch (Exception e) {
                Utility.WriteLog(e.ToString());
            }
        }
    }
}
