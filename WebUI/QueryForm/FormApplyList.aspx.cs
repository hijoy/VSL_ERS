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

public partial class QueryForm_FormApplyList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "方案申请单查询");
            this.Page.Title = "方案申请单查询";

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormApply, SystemEnums.OperateEnum.Manage);
            int opScrapId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormApply, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            HasScrapRight = positionRightBLL.CheckPositionRight(position.PositionId, opScrapId);
            if (!HasManageRight) {
                this.hlExport_Good.Visible = false;
                this.hlExport_Total.Visible = false;
            }

            if (Request["Search"] == null) {
                int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsApplyList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsApplyList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }
            hlExport_Good.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/ReportManage/SalesApplyExportReport.aspx?ShowDialog=1&ExportType=Good";
            hlExport_Total.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/ReportManage/SalesApplyExportReport.aspx?ShowDialog=1&ExportType=Total";
        }
    }

    protected bool HasManageRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
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
            ddlPromotionScope.DataBind();
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();

                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();

                if (this.Request["UCOUID"] != null)
                    this.UCOU.OUId = int.Parse(this.Request["UCOUID"].ToString());

                if (this.Request["CustomerID"] != null)
                    this.UCCustomer.CustomerID = this.Request["CustomerID"].ToString();

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

                if (this.Request["IsClose"] != null) {
                    if (this.Request["IsClose"].ToString().Equals("3")) {
                        this.IsCloseDDL.SelectedValue = "3";
                    } else if (this.Request["IsClose"].ToString().Equals("1")) {
                        this.IsCloseDDL.SelectedValue = "1";
                    } else if (this.Request["IsClose"].ToString().Equals("0")) {
                        this.IsCloseDDL.SelectedValue = "0";
                    }
                }


                if (this.Request["AccruedPeriodStart"] != null)
                    this.UCAccruedPeriodBegin.SelectedDate = this.Request["AccruedPeriodStart"].ToString();

                if (this.Request["AccruedPeriodEnd"] != null)
                    this.UCAccruedPeriodEnd.SelectedDate = this.Request["AccruedPeriodEnd"].ToString();

                if (this.Request["ConfirmDateStart"] != null)
                    this.UCConfirmBeginDate.SelectedDate = this.Request["ConfirmDateStart"].ToString();

                if (this.Request["ConfirmDateEnd"] != null)
                    this.UCConfirmEndDate.SelectedDate = this.Request["ConfirmDateEnd"].ToString();

                if (this.Request["IsComplete"] != null) {
                    if (this.Request["IsComplete"].ToString().Equals("3")) {
                        this.IsCompleteDDL.SelectedValue = "3";
                    } else if (this.Request["IsComplete"].ToString().Equals("1")) {
                        this.IsCompleteDDL.SelectedValue = "1";
                    } else if (this.Request["IsComplete"].ToString().Equals("0")) {
                        this.IsCompleteDDL.SelectedValue = "0";
                    }
                }

                if (this.Request["DeliveryDate"] != null) {
                    this.ucDeliveryDate.SelectedDate = this.Request["DeliveryDate"].ToString();
                }

                if (this.Request["IsAutoSplit"] != null) {
                    if (this.Request["IsAutoSplit"].ToString().Equals("3")) {
                        this.IsAutoSplitDDL.SelectedValue = "3";
                    } else if (this.Request["IsAutoSplit"].ToString().Equals("1")) {
                        this.IsAutoSplitDDL.SelectedValue = "1";
                    } else if (this.Request["IsAutoSplit"].ToString().Equals("0")) {
                        this.IsAutoSplitDDL.SelectedValue = "0";
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

                if (this.Request["PromotionScopeID"] != null) {
                    this.ddlPromotionScope.SelectedValue = this.Request["PromotionScopeID"];
                } else {
                    this.ddlPromotionScope.SelectedValue = "0";
                }
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormApplyViewRow row = (QueryDS.FormApplyViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[0].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/QueryForm/FormApplyList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/QueryForm/FormApplyList.aspx");
                }
                Label lblStatus = (Label)e.Row.Cells[1].FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);
                e.Row.Cells[16].Visible = HasScrapRight;
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
            Session["QueryExpression"] = getSearchCondition();
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.odsApplyList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsApplyList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvApplyList.DataBind();
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
        //申请单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //方案名称

        if (txtFormApplyName.Text.Trim() != string.Empty) {
            filterStr += " AND FormApplyName like '%" + this.txtFormApplyName.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormApplyName=" + this.txtFormApplyName.Text.Trim();
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
            filterStr += " AND FormApply.CustomerID = " + UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //费用小类
        if (!this.SubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormApply.ExpenseSubCategoryID = " + SubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + SubCategoryDDL.SelectedValue;
        }

        //支付方式
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormApply.PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PaymentTypeID=" + PaymentTypeDDL.SelectedValue;
        }

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND Period >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND Period <='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01"
               + "&PeriodEnd=" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
        }

        //预提费用期间
        string startAccruedPeriod = ((TextBox)(this.UCAccruedPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startAccruedPeriod != null && startAccruedPeriod != string.Empty) {
            string endAccruedPeriod = ((TextBox)(this.UCAccruedPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND AccruedPeriod >='" + startAccruedPeriod.Substring(0, 4) + "-" + startAccruedPeriod.Substring(4, 2) + "-01'" +
                " AND AccruedPeriod <='" + endAccruedPeriod.Substring(0, 4) + "-" + endAccruedPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&AccruedPeriodStart=" + startAccruedPeriod.Substring(0, 4) + "-" + startAccruedPeriod.Substring(4, 2) + "-01"
               + "&AccruedPeriodEnd=" + endAccruedPeriod.Substring(0, 4) + "-" + endAccruedPeriod.Substring(4, 2) + "-01";
        }
        //供货日期
        string DeliveryDate = ((TextBox)(this.ucDeliveryDate.FindControl("txtDate"))).Text.Trim();
        if (DeliveryDate != null && DeliveryDate != string.Empty) {
            filterStr += " AND DeliveryEndDate >='" + DeliveryDate + "' AND DeliveryBeginDate<='" + DeliveryDate + "'";
            this.ViewState["SearchCondition"] += "&DeliveryDate=" + DeliveryDate;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //执行确认日期
        string startConfirmDate = ((TextBox)(this.UCConfirmBeginDate.FindControl("txtDate"))).Text.Trim();
        if (startConfirmDate != null && startConfirmDate != string.Empty) {
            string endConfirmDate = ((TextBox)(this.UCConfirmEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND ConfirmCompleteDate >='" + startConfirmDate + "' AND dateadd(day,-1,ConfirmCompleteDate)<='" + endConfirmDate + "'";
            this.ViewState["SearchCondition"] += "&ConfirmDateStart=" + startConfirmDate + "&ConfirmDateEnd=" + endConfirmDate;
        }

        //是否报销完成
        if (this.IsCloseDDL.SelectedValue != "3") {
            if (this.IsCloseDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsClose=1";
                filterStr = filterStr + " And IsClose = 1";
            } else if (this.IsCloseDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsClose=0";
                filterStr = filterStr + " And IsClose=0";
            }
        }

        //是否执行完成
        if (this.IsCompleteDDL.SelectedValue != "3") {
            if (this.IsCompleteDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsComplete=1";
                filterStr = filterStr + " And IsComplete = 1";
            } else if (this.IsCompleteDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsComplete=0";
                filterStr = filterStr + " And IsComplete=0";
            }
        }

        //是否拆分
        if (this.IsAutoSplitDDL.SelectedValue != "3") {
            if (this.IsAutoSplitDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsAutoSplit=1";
                filterStr = filterStr + " And IsAutoSplit = 1";
            } else if (this.IsAutoSplitDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsAutoSplit=0";
                filterStr = filterStr + " And IsAutoSplit=0";
            }
        }
        //促销类型
        if (!this.ddlPromotionScope.SelectedValue.Equals("0")) {
            filterStr += " AND FormApply.PromotionScopeID = " + ddlPromotionScope.SelectedValue;
            this.ViewState["SearchCondition"] += "&PromotionScopeID=" + ddlPromotionScope.SelectedValue;
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

        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择起始费用期间!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择截止费用期间!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
                DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "起始费用期间大于截止费用期间！");
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

    protected void gvApplyList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            FormDS.FormRow row = new SalesApplyBLL().GetFormByID(FormID)[0];
            int count = new SalesApplyBLL().GetEnabledFormReimburseByApplyID(FormID);
            if (count > 0) {
                PageUtility.ShowModelDlg(this, "不能作废单据，此单据有相关报销单！");
            } else {
                if (row.StatusID != 2) {
                    PageUtility.ShowModelDlg(this, "只能作废审批完成状态的单据！");
                } else {
                    new APFlowBLL().ScrapForm(FormID);
                    gvApplyList.DataBind();
                }
            }
        }
    }

    public string FormApplyNameFormat(object obj) {
        string name = obj == null ? "" : obj.ToString();
        if (name == null || name.Length <= 10) {
            return name;
        } else {
            return name.Substring(0, 10) + "..";
        }
    }
}
