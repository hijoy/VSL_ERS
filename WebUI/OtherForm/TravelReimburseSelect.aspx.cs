using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using System.Collections.Generic;

public partial class OhterForm_TravelReimburseSelect : BasePage {

    private PersonalReimburseBLL _PersonalReimburseBLL;
    protected PersonalReimburseBLL PersonalReimburseBLL {
        get {
            if (this._PersonalReimburseBLL == null) {
                this._PersonalReimburseBLL = new PersonalReimburseBLL();
            }
            return this._PersonalReimburseBLL;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "���������");
            this.Page.Title = "���������";
            //��ѯ������״̬Ϊ2���Ѿ������� �Ҹ����뵥û�б�������������    
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = @"statusid=2 and UserID=" + stuffUser.StuffUserId +
            " and FormID not in  (select FormTravelApplyID from FormPersonalReimburse   inner join Form on Form.FormID=FormPersonalReimburse.FormPersonalReimburseID where StatusID in (0,1,2,3) and FormTravelApplyID is not null)";
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null) {
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                }
                if (this.Request["SubmitDateStart"] != null) {
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                }
                if (this.Request["SubmitDateEnd"] != null) {
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                }
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // �������н��и�ֵ
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                LinkButton lbtnFormNo = (LinkButton)e.Row.FindControl("lbtnFormNo");
                LinkButton lbtnReimburse = (LinkButton)e.Row.FindControl("lbtnReimburse");
                Label lblHiddenUserID = (Label)e.Row.FindControl("lblHiddenUserID");
                Label lblFormApplyID = (Label)e.Row.FindControl("lblFormApplyID");
                if (this.ViewState["SearchCondition"] != null) {
                    //��������·��
                    lbtnFormNo.PostBackUrl = "~/OtherForm/TravelApproval.aspx?ObjectID=" + lblFormApplyID.Text + "&Source="
                        + HttpUtility.UrlEncode("~/OtherForm/TravelReimburseSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex);
                    lbtnReimburse.PostBackUrl = "~/OtherForm/TravelReimburseApply.aspx?FormTravelApplyID=" + lblFormApplyID.Text + "&Source="
                        + HttpUtility.UrlEncode("~/OtherForm/TravelReimburseSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex);
                } else {
                    lbtnFormNo.PostBackUrl = "~/OtherForm/TravelApproval.aspx?ObjectID=" + lblFormApplyID.Text +
                        "&Source=~/OtherForm/TravelReimburseSelect.aspx";
                    //��������
                    lbtnReimburse.PostBackUrl = "~/OtherForm/TravelReimburseApply.aspx?FormTravelApplyID=" + lblFormApplyID.Text +
                        "&Source=~/OtherForm/TravelReimburseSelect.aspx";
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (checkSearchConditionValid()) {
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvApplyList.DataBind();
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;//�û�ID
        this.ViewState["SearchCondition"] = "Search=true";
        //�����ַ���
        string filterStr = @"statusid=2 and UserID=" + stuffuserID +
                            " and FormID not in  (select FormTravelApplyID from FormPersonalReimburse   inner join Form on Form.FormID=FormPersonalReimburse.FormPersonalReimburseID where StatusID in (0,1,2,3) and FormTravelApplyID is not null)";

        //���뵥���
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //��������
        string start = UCDateInputBeginDate.SelectedDate;
        if (start != null && start != string.Empty) {
            string end = UCDateInputEndDate.SelectedDate;
            filterStr += " and SubmitDate >='" + start + "' and dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = UCDateInputBeginDate.SelectedDate;
        string end = UCDateInputEndDate.SelectedDate;

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ�������ύ��ʼ����!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ�������ύ��ֹ����!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "��ʼ���ڴ��ڽ�ֹ���ڣ�");
                    return false;
                }
            }
        }
        return true;
    }

    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }
}
