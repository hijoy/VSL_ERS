using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;
using BusinessObjects.LogDSTableAdapters;
using System.IO;

namespace BusinessObjects {
    public class SysLog {
        public enum LogType {
            SystemError = 0,
            LogIn = 1,
            AuthorizationConfigure = 2,
            CommonDateEdit = 4
        }

        private static TraceSource m_SysTrace;

        public static TraceSource SysTrace {
            get {
                if (m_SysTrace == null) {
                    m_SysTrace = new TraceSource("SysTrace");                    
                    m_SysTrace.Listeners.Add(new SysTraceListener());
                }
                return m_SysTrace;
            }
        }

        public static void LogSystemError(Exception ex) {
            SysTrace.TraceData(TraceEventType.Error, (int)LogType.SystemError, ex);
        }

        public static void LogLogInAction(LogInAction action) {
            SysTrace.TraceData(TraceEventType.Information, (int)LogType.LogIn, action);
        }

        public static void LogAuthorizationConfigure(AuthorizationConfigure configure) {
            SysTrace.TraceData(TraceEventType.Information, (int)LogType.AuthorizationConfigure, configure);
        }

        public static void LogCommonDataEditAction(CommonDataEditAction action) {
            SysTrace.TraceData(TraceEventType.Information, (int)LogType.CommonDateEdit, action);
        }

        //public static void DisplaySqlErrors(SqlException exception) {
        //    for (int i = 0; i < exception.Errors.Count; i++) {
        //        SysTrace.TraceInformation("Index #" + i + "\n" +
        //            "Source: " + exception.Errors[i].Source + "\n" +
        //            "Number: " + exception.Errors[i].Number.ToString() + "\n" +
        //            "State: " + exception.Errors[i].State.ToString() + "\n" +
        //            "Class: " + exception.Errors[i].Class.ToString() + "\n" +
        //            "Server: " + exception.Errors[i].Server + "\n" +
        //            "Message: " + exception.Errors[i].Message + "\n" +
        //            "Procedure: " + exception.Errors[i].Procedure + "\n" +
        //            "LineNumber: " + exception.Errors[i].LineNumber.ToString());
        //    }
        //    SysTrace.TraceData(TraceEventType.Error, 1, exception);            
        //}
    }

    public class SysTraceListener : TraceListener {
        private LogInActionLogTableAdapter m_LogInActionLogTA;
        public LogInActionLogTableAdapter LogInActionLogTA {
            get {
                if (m_LogInActionLogTA == null) {
                    m_LogInActionLogTA = new LogInActionLogTableAdapter();
                }
                return m_LogInActionLogTA;
            }
        }

        private CommonDataEditActionLogTableAdapter m_CommonDataEditActionLogTA;
        public CommonDataEditActionLogTableAdapter CommonDataEditActionLogTA {
            get {
                if (m_CommonDataEditActionLogTA == null) {
                    m_CommonDataEditActionLogTA = new CommonDataEditActionLogTableAdapter();
                }
                return this.m_CommonDataEditActionLogTA;
            }
        }

        private AuthorizationConfigureLogTableAdapter m_AuthorizationConfigureLogTA;
        public AuthorizationConfigureLogTableAdapter AuthorizationConfigureLogTA {
            get {
                if (this.m_AuthorizationConfigureLogTA == null) {
                    this.m_AuthorizationConfigureLogTA = new AuthorizationConfigureLogTableAdapter();
                }
                return this.m_AuthorizationConfigureLogTA;
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data) {
            switch (id) {
                case (int)SysLog.LogType.LogIn:
                    this.LogLogInAction((LogInAction)data);
                    break;
                case (int)SysLog.LogType.SystemError:
                    this.LogSystemError((Exception)data);
                    break;
                case (int)SysLog.LogType.AuthorizationConfigure:
                    this.LogAuthorizationConfigure((AuthorizationConfigure)data);
                    break;
                case (int)SysLog.LogType.CommonDateEdit:
                    this.LogCommonDataEditAction((CommonDataEditAction)data);
                    break;
                default:
                    base.TraceData(eventCache, source, eventType, id, data);
                    break;
            } 
        }

        public void LogAuthorizationConfigure(AuthorizationConfigure configure) {
            this.AuthorizationConfigureLogTA.Insert(configure.ConfigureTarget,
                configure.NewContent, configure.OldContent, configure.ConfigureType,
                configure.ConfigureTime, configure.StuffId, configure.StuffName);
        }

        public void LogSystemError(Exception ex) {
            Exception innerException = ex;
            while (innerException.InnerException != null) {
                innerException = innerException.InnerException;
            }
            string errorMsg = "异常发生时间；" + DateTime.Now +
                "\n" + "异常：" + innerException.GetType().Name +
                "\n" + "异常消息：" + innerException.Message + 
                "\n" + "异常堆栈：" +
                "\n" + ex.StackTrace +
                "\n" + " " + "\n";
            if (!Directory.Exists(@"c:\SysLog")) {
                Directory.CreateDirectory(@"c:\SysLog");
            }

            FileInfo fileInfo = new FileInfo(@"c:\SysLog\log1.txt");
            if (!fileInfo.Exists || fileInfo.Length < 1000000) {
                File.AppendAllText(@"c:\SysLog\log1.txt", errorMsg);
                return;
            } 

            fileInfo = new FileInfo(@"c:\SysLog\log2.txt");
            if (!fileInfo.Exists || fileInfo.Length < 1000000) {
                File.AppendAllText(@"c:\SysLog\log2.txt", errorMsg);
                return;
            } 

            fileInfo = new FileInfo(@"c:\SysLog\log3.txt");
            if (!fileInfo.Exists || fileInfo.Length < 1000000) {
                File.AppendAllText(@"c:\SysLog\log3.txt", errorMsg);
                return;
            } 

            File.Copy(@"c:\SysLog\log2.txt", @"c:\SysLog\log1.txt",true);
            File.Copy(@"c:\SysLog\log3.txt", @"c:\SysLog\log2.txt",true);
            File.Delete(@"c:\SysLog\log3.txt");
            File.AppendAllText(@"c:\SysLog\log3.txt", errorMsg);            
        }

        public void LogLogInAction(LogInAction action) {
            LogDS logDS = new LogDS();
            LogDS.LogInActionLogRow log = logDS.LogInActionLog.NewLogInActionLogRow();
            log.ClientIP = action.ClientIP;
            log.UserName = action.UserName;
            log.Success = action.Success;
            if (log.Success) {
                log.StuffId = action.StuffId;
                log.StuffName = action.StuffName;
            }
            log.LogInTime = action.LogInTime;
            logDS.LogInActionLog.AddLogInActionLogRow(log);
            this.LogInActionLogTA.Update(logDS.LogInActionLog);
        }

        public void LogCommonDataEditAction(CommonDataEditAction action) {
            LogDS logDS = new LogDS();
            LogDS.CommonDataEditActionLogRow log = logDS.CommonDataEditActionLog.NewCommonDataEditActionLogRow();
            log.ActionTime = action.ActionTime;
            log.ActionType = action.ActionType;
            log.DataTableName = action.DataTableName;
            if (action.NewValue != null) {
                log.NewValue = action.NewValue;
            }
            if (action.OldValue != null) {
                log.OldValue = action.OldValue;
            }
            if (action.StuffId != null) {
                log.StuffId = action.StuffId;
            }
            log.StuffName = action.StuffName;
            logDS.CommonDataEditActionLog.AddCommonDataEditActionLogRow(log);
            this.CommonDataEditActionLogTA.Update(logDS.CommonDataEditActionLog);
        }

        
        public override void Write(string message) {
            //throw new ApplicationException("The method or operation is not implemented.");
        }

        public override void WriteLine(string message) {
            //throw new ApplicationException("The method or operation is not implemented.");
        }
    }

    public class LogInAction {
        public string ClientIP;
        public string StuffId;
        public string StuffName;
        public string UserName;
        public bool Success;
        public DateTime LogInTime;
    }

    public class CommonDataEditAction {
        public string StuffId;
        public string StuffName;
        public string DataTableName;
        public DateTime ActionTime;
        public string ActionType;
        public string NewValue;
        public string OldValue;
    }

    public class AuthorizationConfigure {
        public string StuffId;
        public string StuffName;
        public DateTime ConfigureTime;
        public string ConfigureTarget;
        public string NewContent;
        public string OldContent;
        public string ConfigureType;
    }
}
