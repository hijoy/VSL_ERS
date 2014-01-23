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

public partial class QueryForm_FormApplyBatchPrint : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            this.ViewState["FormIDs"] = "";
            PageUtility.SetContentTitle(this, "方案书批量打印");
            this.Page.Title = "方案书批量打印";

            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();

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

                if (this.Request["IsComplete"] != null) {
                    if (this.Request["IsComplete"].ToString().Equals("3")) {
                        this.IsCompleteDDL.SelectedValue = "3";
                    } else if (this.Request["IsComplete"].ToString().Equals("1")) {
                        this.IsCompleteDDL.SelectedValue = "1";
                    } else if (this.Request["IsComplete"].ToString().Equals("0")) {
                        this.IsCompleteDDL.SelectedValue = "0";
                    }
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
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/QueryForm/FormApplyBatchPrint.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/QueryForm/FormApplyBatchPrint.aspx");
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
                this.gvApplyList.PageIndex = int.Parse(start);
            }
            Session["QueryExpression"] = getSearchCondition();
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvApplyList.DataBind();
        }
    }
    
    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        //string filterStr = "FormApplyView.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
        //取审批完成的数据
        string filterStr = "FormApplyView.StatusId =" + ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
        //取当前操作人数据
        filterStr += " AND UserID='" + stuffuserID + "'";
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


        //客户
        if (this.UCCustomer.CustomerID != string.Empty) {
            filterStr += " AND CustomerID = " + UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //费用小类
        if (!this.SubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND ExpenseSubCategoryID = " + SubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + SubCategoryDDL.SelectedValue;
        }

        //支付方式
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
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


        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
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

    public string FormApplyNameFormat(object obj) {
        string name = obj == null ? "" : obj.ToString();
        if (name == null || name.Length <= 6) {
            return name;
        } else {
            return name.Substring(0, 6) + "..";
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e) {
        string FormIDS = this.ViewState["FormIDs"].ToString() ;
        CheckBox chk = (CheckBox)sender;
        FormIDS += chk.ToolTip + ",";
        this.ViewState["FormIDs"] = FormIDS;
        
        //this.hlPrint.NavigateUrl = "javascript:window.open('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/ReportManage/SalesApplyListBatch.aspx?ShowDialog=1&FormIDS=" + FormIDS + "','', 'dialogWidth:800px;dialogHeight:750px;resizable:yes;');";
    }
}
