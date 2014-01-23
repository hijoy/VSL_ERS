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

public partial class QueryForm_FormPersonalReimburseList : BasePage {

    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "个人费用报销查询");
            this.Page.Title = "个人费用报销查询";
            if (Request["Search"] == null) {
                int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
                this.odsPersonalReimburseList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsPersonalReimburseList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsPersonalReimburseList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormPersonalReimburse, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!HasManageRight) {
                this.btnExport.Visible = false;
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
                if (this.Request["UCOUID"] != null)
                    this.UCOU.OUId = int.Parse(this.Request["UCOUID"].ToString());
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                if (this.Request["PeriodStart"] != null)
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                if (this.Request["PeriodEnd"] != null)
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                if (this.Request["chkAwaiting"] != null)
                    this.chkAwaiting.Checked = true;
                if (this.Request["chkApproveCompleted"] != null)
                    this.chkApproveCompleted.Checked = true;
                if (this.Request["chkRejected"] != null)
                    this.chkRejected.Checked = true;
                if (this.Request["chkScrap"] != null)
                    this.chkScrap.Checked = true;
                if (this.Request["chkPersonalReimburse"] != null)
                    this.chkPersonalReimburse.Checked = true;
                if (this.Request["chkTravelReimburse"] != null)
                    this.chkTravelReimburse.Checked = true;
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvPersonalReimburseList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormPersonalReimburseViewRow row = (QueryDS.FormPersonalReimburseViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[0].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/QueryForm/PersonalReimburseList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvPersonalReimburseList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/QueryForm/PersonalReimburseList.aspx");
                }
                Label lblStatus = (Label)e.Row.Cells[1].FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvPersonalReimburseList.PageIndex = int.Parse(start);
            }
            this.odsPersonalReimburseList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.odsPersonalReimburseList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsPersonalReimburseList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvPersonalReimburseList.DataBind();
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
            int count = bll.QueryFormPersonalReimburseViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new FormPersonalReimburseViewForExportByRightTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormPersonalReimburse" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
        //单据编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
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

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND Period >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND Period<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01"
               + "&PeriodEnd=" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //单据状态
        filterStr += getStatusID();

        //单据类型
        filterStr += getFormType();

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

    private string getFormType() {
        string PageTypeStr = string.Empty;
        if (this.chkPersonalReimburse.Checked) {
            this.ViewState["SearchCondition"] += "&chkPersonalReimburse=true";
            PageTypeStr += "5,";
        }
        if (this.chkTravelReimburse.Checked) {
            this.ViewState["SearchCondition"] += "&chkTravelReimburse=true";
            PageTypeStr += "12,";
        }
        if (!string.IsNullOrEmpty(PageTypeStr)) {
            PageTypeStr = " and Form.PageType in (" + PageTypeStr.Substring(0, PageTypeStr.Length - 1) + ")";
        }
        return PageTypeStr;
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

        string periodStart = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string periodEnd = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
        if (periodStart == null || periodStart == string.Empty) {
            if (periodEnd != null && periodEnd != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择费用期间起始日期!");
                return false;
            }
        } else {
            if (periodEnd == null || periodEnd == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择费用期间截止日期!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(periodStart.Substring(0, 4) + "-" + periodStart.Substring(4, 2) + "-01");
                DateTime dtEnd = DateTime.Parse(periodEnd.Substring(0, 4) + "-" + periodEnd.Substring(4, 2) + "-01");
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return false;
                }
            }
        }
        return true;
    }

    public string GetPersonalReimburseETD(string beginDate, string endDate) {
        return beginDate + "-" + endDate;
    }
}
