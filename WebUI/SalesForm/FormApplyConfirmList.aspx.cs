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

public partial class QueryForm_FormApplyConfirmList : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "�ȴ���ȷ��ִ�е����뵥");
            this.Page.Title = "�ȴ���ȷ��ִ�е����뵥";
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.odsCustomer.SelectParameters["PositionID"].DefaultValue = position.PositionId.ToString();
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = "(UserID=" + stuffuserID + " or exists (select * from ProxyReimburse where ProxyReimburse.UserID = FormApplyView.UserID and ProxyReimburse.ProxyUserID =" + stuffuserID + " and ProxyReimburse.EndDate>FormApplyView.SubmitDate) )"
                + " And IsClose = 0 and StatusID = 2 And (IsComplete is not null and IsComplete=0) ";
            if (Request["Search"] == null) {
                //this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "UserID=" + stuffuserID + " And IsClose = 0 and StatusID = 2";
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = this.ViewState["DefaultFilter"].ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                this.CustomerDDL.DataBind();
                if (this.Request["CustomerID"] != null)
                    this.CustomerDDL.SelectedValue = this.Request["CustomerID"].ToString();
                this.SubCategoryDDL.DataBind();
                if (this.Request["ExpenseSubCategoryID"] != null)
                    this.SubCategoryDDL.SelectedValue = this.Request["ExpenseSubCategoryID"].ToString();
                this.PaymentTypeDDL.DataBind();
                if (this.Request["PaymentTypeID"] != null)
                    this.PaymentTypeDDL.SelectedValue = this.Request["PaymentTypeID"].ToString();
                if (this.Request["PeriodStart"] != null)
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                if (this.Request["PeriodEnd"] != null)
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // �������н��и�ֵ
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormApplyViewRow row = (QueryDS.FormApplyViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[2].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplyConfirmList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/SalesForm/FormApplyConfirmList.aspx");
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvApplyList.PageIndex = int.Parse(start);
            }
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvApplyList.DataBind();
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = this.ViewState["DefaultFilter"].ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //���뵥���
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //����������
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //�ͻ�
        if (this.CustomerDDL.SelectedValue != "0") {
            filterStr += " AND CustomerID = " + this.CustomerDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.CustomerDDL.SelectedValue;
        }

        //����С��
        if (!this.SubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND ExpenseSubCategoryID = " + SubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + SubCategoryDDL.SelectedValue;
        }

        //֧����ʽ
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PaymentTypeID=" + PaymentTypeDDL.SelectedValue;
        }

        //�����ڼ�
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND Period >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND Period<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01"
               + "&PeriodEnd=" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
        }

        //��������
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

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

        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ����ʼ�����ڼ�!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ���ֹ�����ڼ�!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
                DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "��ʼ�����ڼ���ڽ�ֹ�����ڼ䣡");
                    return false;
                }
            }
        }

        return true;
    }

    public string GetShopNameByID(object shopID) {
        if (shopID.ToString() != string.Empty) {
            int id = Convert.ToInt32(shopID);
            return new MasterDataBLL().GetShopByID(id).ShopName;
        } else {
            return null;
        }
    }


}
