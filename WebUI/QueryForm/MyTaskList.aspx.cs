using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessObjects;
using System.Web.UI.HtmlControls;

public partial class QueryForm_MyTaskList : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender,e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "批量审批");
            this.Page.Title = "批量审批";
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = "charindex('P" + stuffuserID.ToString() + "P',InTurnUserIds)>0 and Form.StatusId='1'";
        }
    }

    protected void gvMyAwaiting_RowDataBound(object sender, GridViewRowEventArgs e) {
        //对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormViewRow row = (QueryDS.FormViewRow)drvDetail.Row;

                LinkButton lbFormNo = (LinkButton)e.Row.Cells[2].FindControl("lblFormNo");
                lbFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/Home.aspx");
            }
        }

        List<int> checkedFormIds = (List<int>)this.ViewState["CheckedFormIds"];
        if (checkedFormIds == null) {
            checkedFormIds = new List<int>();
        }
        foreach (GridViewRow gridViewRow in this.gvMyAwaiting.Rows) {
            if (gridViewRow.RowType == DataControlRowType.DataRow) {
                int FormId = (int)this.gvMyAwaiting.DataKeys[gridViewRow.RowIndex].Value;
                CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("CheckCtl");
                if (checkedFormIds.Contains(FormId)) {
                    checkCtl.Checked = true;
                } else {
                    checkCtl.Checked = false;
                }
            }
        }
    }

    protected void gvMyAwaiting_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        this.ViewState["CheckedFormIds"] = this.GetCheckedFormIds();
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvMyAwaiting.PageIndex = int.Parse(start);
            }

            this.odsMyAwaiting.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();

            this.gvMyAwaiting.DataBind();
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = "Form.StatusId =" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //申请单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }
        //单据类型
        if (ckbType.SelectedIndex >= 0) {
            filterStr += " AND Form.FormTypeID in ( " + this.GetFormType() + ")";
            //this.ViewState["SearchCondition"] += "&FormTypeID=" + this.ddlFomrType.SelectedValue;
        }
        //申请人姓名
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //申请人组织机构
        if (this.UCOU.OUId != null) {
            filterStr += " AND charindex('P" + this.UCOU.OUId + "P',Position.OrganizationTreePath) > 0";
            this.ViewState["SearchCondition"] += "&UCOUID=" + this.UCOU.OUId;
        }



        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //属于我的待审批的单据
        filterStr += "and charindex('P" + stuffuserID.ToString() + "P',InTurnUserIds)>0";

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交起始日期!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交截止日期!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return false;
                }
            }
        }
        return true;
    }

    //得到选中的单据
    private List<int> GetCheckedFormIds() {
        List<int> checkedFormIds = (List<int>)this.ViewState["CheckedFormIds"];
        if (checkedFormIds == null) {
            checkedFormIds = new List<int>();
        }
        foreach (GridViewRow gridViewRow in this.gvMyAwaiting.Rows) {
            if (gridViewRow.RowType == DataControlRowType.DataRow) {
                int FormId = (int)this.gvMyAwaiting.DataKeys[gridViewRow.RowIndex].Value;
                CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("CheckCtl");
                if (checkCtl.Checked) {
                    if (!checkedFormIds.Contains(FormId)) {
                        checkedFormIds.Add(FormId);
                    }
                } else {
                    if (checkedFormIds.Contains(FormId)) {
                        checkedFormIds.Remove(FormId);
                    }
                }
            }
        }

        return checkedFormIds;
    }
    //得到单据类型
    private string GetFormType() {
        string strType = string.Empty;
        foreach (ListItem it in ckbType.Items) {
            if (it.Selected == true) {
                strType += it.Value + ",";
            }
        }
        return strType.TrimEnd(',');
    }


    protected void SubmitBtn_Click(object sender, EventArgs e) {
        try {
            AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            APFlowBLL AB = new APFlowBLL();
            List<int> formIDs = this.GetCheckedFormIds();
            if (formIDs.Count > 0) {
                string test = string.Empty;
                for (int i = 0; i < formIDs.Count; i++) {
                    AB.ApproveForm(CommonUtility.GetAPHelper(Session), formIDs[i], currentStuff.StuffUserId, currentStuff.StuffName, true, "", "", 0);
                }

                if (this.Request["Source"] != null) {
                    this.Response.Redirect(this.Request["Source"].ToString());
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            } else {
                PageUtility.ShowModelDlg(this, "请选择单据！");
                return;
            }

        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }

    }

}