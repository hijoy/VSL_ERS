using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using System.Data.SqlClient;
using System.Web;
using lib.wf;

namespace BusinessObjects {
    public class APFlowBLL {
        string mailTitle = "由{0}提交的编号为{1}的单据等待您的审批";
        string mailBody = "<br><b>单据编号：</b>{0}<br><b>系统入口：</b><a href='" + System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ERSEntry") + "'>维他奶协同办公系统</a>";

        public void ScrapForm(int FormID) {
            FormDS.FormRow formRow = new FormTableAdapter().GetDataByID(FormID)[0];
            formRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
            new FormTableAdapter().Update(formRow);
        }

        public void GenerateRebateReimburse(int FormApplyID) {
            FormDS.FormRow form = new FormTableAdapter().GetDataByID(FormApplyID)[0];
            UtilityBLL utility = new UtilityBLL();
            string formTypeString = utility.GetFormTypeString((int)SystemEnums.FormType.ReimburseApply);
            new FormApplyTableAdapter().GenerateRebateReimburse(FormApplyID, form.FormNo, utility.GetFormNo(formTypeString));
        }

        public void GenerateApplyForm(int FormApplyID) {
            new FormApplyTableAdapter().GenerateApplyForm(FormApplyID);
        }

        //审批方法
        public void ApproveForm(APHelper AP, int formID, int stuffUserId, string stuffName, bool pass, string comment, string ProxyStuffName, int OrganizationUnitID) {
            string email = string.Empty;
            SqlTransaction transaction = null;
            try {
                FormTableAdapter TAMainForm = new FormTableAdapter();

                transaction = TableAdapterHelper.BeginTransaction(TAMainForm);

                FormDS.FormRow formRow = TAMainForm.GetDataByID(formID)[0];

                string returnValue = AP.approve(pass, comment, stuffUserId.ToString(), stuffName, formRow.ProcID, ProxyStuffName, formRow.OrganizationUnitID, ref email);
                string[] approveinfo = AP.GetProcessApproveUser(formRow.ProcID);
                if (AP.GetProcessIsEnd(formRow.ProcID)) {
                    formRow.LastApprover = stuffUserId.ToString();
                    formRow.Comment = comment;
                    formRow.ApprovedDate = Convert.ToDateTime(approveinfo[0]);
                }
                formRow.ApproverIds = approveinfo[3];
                if (returnValue == null) {
                    formRow.InTurnUserIds = "P";
                    formRow.InTurnPositionIds = "P";
                    //formRow.SubmitDate = DateTime.Now;

                    if (pass) {
                        //如果审批通过且返回值为空则该流程结束

                        formRow.StatusID = (int)SystemEnums.FormStatus.ApproveCompleted;
                        //mailTitle = string.Format("您申请的编号为{0}的单据已经通过审批", formRow.FormNo);
                        //mailBody = string.Format(mailBody, formRow.FormNo);
                        //email = AP.getUserEmailByID(formRow.UserID);

                        //AP.sendMail(email, "", mailTitle, mailBody);

                    } else {
                        //如果不通过则为驳回
                        formRow.StatusID = (int)SystemEnums.FormStatus.Rejected;

                        //mailTitle = string.Format("您有单据申请被{0}退回", stuffName);
                        //mailBody = string.Format(mailBody, formRow.FormNo);
                        //email = AP.getUserEmailByID(formRow.UserID);

                        //AP.sendMail(email, "", mailTitle, mailBody);

                    }
                } else {
                    string[] InTurn = returnValue.Split('&');//不同流程角色下的人员和职位
                    string ids = "";
                    string pids = "";
                    for (int a = 0; a < InTurn.Length; a++) {
                        ids += InTurn[a].Split('$')[0].ToString();///人员
                        pids += InTurn[a].Split('$')[1].ToString();//职位
                    }
                    formRow.InTurnUserIds = ids;//下一步的人员
                    formRow.InTurnPositionIds = pids;//下一步的人员职位

                    //mailTitle = string.Format(mailTitle, stuffName, formRow.FormNo);
                    //mailBody = string.Format(mailBody, formRow.FormNo);
                    //AP.sendMail(email, "", mailTitle,mailBody);

                }

                TAMainForm.Update(formRow);

                transaction.Commit();
            } catch (Exception ex) {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            } finally {
                if (transaction != null)
                    transaction.Dispose();
            }
            //System.Threading.Thread.Sleep(3000);
        }

        //启动流程
        public void ApplyForm(APHelper AP, FormTableAdapter FormTA, int? RejectedFormID, FormDS.FormRow formRow, int OrganizationUnitID, string ProcessTemplateName, SystemEnums.FormStatus StatusID, Dictionary<string, object> map) {
            string email = string.Empty;
            if (StatusID == SystemEnums.FormStatus.Awaiting) {

                string ProcID = AP.createProcess(formRow.FormNo + ":" + DateTime.Now.ToString(), ProcessTemplateName, map);
                formRow.ProcID = ProcID;

                String inTurnStr = AP.startProcess(ProcID, OrganizationUnitID, ref email, formRow.UserID.ToString(), new AuthorizationBLL().GetStuffUserById(formRow.UserID).StuffName);

                string[] InTurn = inTurnStr.Split('&');//不同流程角色下的人员和职位
                string ids = "";
                string pids = "";
                for (int a = 0; a < InTurn.Length; a++) {
                    ids += InTurn[a].Split('$')[0].ToString();///人员
                    pids += InTurn[a].Split('$')[1].ToString();//职位
                }
                formRow.InTurnUserIds = ids;//下一步的人员
                formRow.InTurnPositionIds = pids;//下一步的人员职位
                formRow.StatusID = (int)SystemEnums.FormStatus.Awaiting;
                FormTA.Update(formRow);
            }

            //作废之前的单据
            if (RejectedFormID != null) {
                FormDS.FormRow oldRow = FormTA.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                    ScrapForm(oldRow.FormID);
                }
            }
        }
    }
}
