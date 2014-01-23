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
using BusinessObjects.QueryDSTableAdapters;

public partial class QueryForm_FormReimburseList : BasePage {

    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "方案报销单查询");
            this.Page.Title = "方案报销单查询";
            if (Request["Search"] == null) {
                int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
                this.odsReimburseList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsReimburseList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsReimburseList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormReimburse, SystemEnums.OperateEnum.Manage);
            int opScrapId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormApply, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            HasScrapRight = positionRightBLL.CheckPositionRight(position.PositionId, opScrapId);
            if (!HasManageRight) {
                this.btnExport.Visible = false;
            }
        }
    }

    protected bool HasScrapRight {
        get {
            return (bool)this.ViewState["HasScrapRight"];
        }
        set {
            this.ViewState["HasScrapRight"] = value;
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();

                if (this.Request["ApplyFormNo"] != null)
                    this.txtApplyFormNo.Text = this.Request["ApplyFormNo"].ToString();

                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();

                if (this.Request["UCOUID"] != null)
                    this.UCOU.OUId = int.Parse(this.Request["UCOUID"].ToString());

                if (this.Request["CustomerID"] != null)
                    this.UCCustomer.CustomerID = this.Request["CustomerID"].ToString();

                this.PaymentTypeDDL.DataBind();
                if (this.Request["PaymentTypeID"] != null)
                    this.PaymentTypeDDL.SelectedValue = this.Request["PaymentTypeID"].ToString();

                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();

                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();

                if (this.Request["IsDeliveryComplete"] != null) {
                    if (this.Request["IsDeliveryComplete"].ToString().Equals("3")) {
                        this.IsDeliveryCompleteDDL.SelectedValue = "3";
                    } else if (this.Request["IsDeliveryComplete"].ToString().Equals("1")) {
                        this.IsDeliveryCompleteDDL.SelectedValue = "1";
                    } else if (this.Request["IsDeliveryComplete"].ToString().Equals("0")) {
                        this.IsDeliveryCompleteDDL.SelectedValue = "0";
                    }
                }

                if (this.Request["chkAwaiting"] != null)
                    this.chkAwaiting.Checked = true;
                if (this.Request["chkApproveCompleted"] != null)
                    this.chkApproveCompleted.Checked = true;
                if (this.Request["chkRejected"] != null)
                    this.chkRejected.Checked = true;
                if (this.Request["chkScrap"] != null)
                    this.chkScrap.Checked = true;

                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvReimburseList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormReimburseViewRow row = (QueryDS.FormReimburseViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[0].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/QueryForm/FormReimburseList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvReimburseList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/QueryForm/FormReimburseList.aspx");
                }
                Label lblStatus = (Label)e.Row.Cells[1].FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);
                e.Row.Cells[9].Visible = HasScrapRight;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvReimburseList.PageIndex = int.Parse(start);
            }

            this.odsReimburseList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.odsReimburseList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsReimburseList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvReimburseList.DataBind();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string queryExpession = getSearchCondition();
            int stuffID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int positionID = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
            int limit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExportCountLimit"]);
            FormQueryBLL bll = new FormQueryBLL();
            int count = bll.QueryFormReimburseViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new FormReimburseViewForExportByRightTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormReimburse" + DateTime.Now.ToString("yyyyMMddHHmmss");
                ToExcel(this.ExportDataGrid, fileName);
                Session.Timeout = 3;
            } catch (Exception ex) {
                PageUtility.DealWithException(this, ex);
            }
        }
    }

    public void ToExcel(System.Web.UI.Control ctl, String fileName) {
        Response.Clear();
        Response.Buffer = false;
        Response.Charset = "GB2312";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-excel";
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);
        ctl.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = "Form.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //报销单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //申请单编号
        if (txtApplyFormNo.Text.Trim() != string.Empty) {
            filterStr += " And charindex('P" + txtApplyFormNo.Text + "P',FormApplyNos)>0";
            this.ViewState["SearchCondition"] += "&ApplyFormNo=" + this.txtApplyFormNo.Text.Trim();
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

        //客户
        if (this.UCCustomer.CustomerID != string.Empty) {
            filterStr += " AND FormReimburse.CustomerID = " + UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //支付方式
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormReimburse.PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PaymentTypeID=" + PaymentTypeDDL.SelectedValue;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //是否报销完成
        if (this.IsDeliveryCompleteDDL.SelectedValue != "3") {
            if (this.IsDeliveryCompleteDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsDeliveryComplete=1";
                filterStr = filterStr + " And IsDeliveryComplete = 1";
            } else if (this.IsDeliveryCompleteDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsDeliveryComplete=0";
                filterStr = filterStr + " And (IsDeliveryComplete=0 or IsDeliveryComplete is null)";
            }
        }

        //单据状态
        filterStr += getStatusID();

        return filterStr;
    }

    private string getStatusID() {
        string strStatusID = string.Empty;
        //单据状态
        if (chkAwaiting.Checked == true ||
            chkApproveCompleted.Checked == true ||
            chkRejected.Checked == true ||
            chkScrap.Checked == true) {
            if (chkAwaiting.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkAwaiting=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Awaiting).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Awaiting).ToString();
                }
            }

            if (chkApproveCompleted.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkApproveCompleted=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                }
            }

            if (chkRejected.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkRejected=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Rejected).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Rejected).ToString();
                }
            }

            if (chkScrap.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkScrap=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Scrap).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Scrap).ToString();
                }
            }

            strStatusID = " AND StatusId IN (" + strStatusID + ")";
        }

        return strStatusID;
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

    protected void gvReimburseList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            FormDS.FormRow row = new SalesApplyBLL().GetFormByID(FormID)[0];
            if (row.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "只能作废审批完成状态的单据！");
            } else {
                new APFlowBLL().ScrapForm(FormID);
                this.gvReimburseList.DataBind();
            }
        }
    }
}
