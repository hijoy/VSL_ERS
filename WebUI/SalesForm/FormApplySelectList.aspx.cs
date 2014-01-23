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

public partial class QueryForm_FormApplySelectList : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "方案申请单查询");
            this.Page.Title = "方案申请单查询";
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.odsCustomer.SelectParameters["PositionID"].DefaultValue = position.PositionId.ToString();
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = "(UserID=" + stuffuserID + " or exists (select * from ProxyReimburse where ProxyReimburse.UserID = FormApplyView.UserID and ProxyReimburse.ProxyUserID =" + stuffuserID + " and ProxyReimburse.EndDate>FormApplyView.SubmitDate) )"
                + " And IsClose = 0 and StatusID = 2 And (IsComplete is not null and IsComplete=1) ";
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
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormApplyViewRow row = (QueryDS.FormApplyViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[2].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/SalesForm/FormApplySelectList.aspx");
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
        //申请单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //申请人姓名
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //客户
        if (this.CustomerDDL.SelectedValue != "0") {
            filterStr += " AND CustomerID = " + this.CustomerDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.CustomerDDL.SelectedValue;
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

    //protected void gvApplyList_PageIndexChanging(object sender, GridViewPageEventArgs e) {
    //    this.ViewState["CheckedFormIds"] = this.GetCheckedFormIds();
    //}

    //protected void gvApplyList_DataBound(object sender, EventArgs e) {
    //    List<int> checkedFormIds = (List<int>)this.ViewState["CheckedFormIds"];
    //    if (checkedFormIds == null) {
    //        checkedFormIds = new List<int>();
    //    }
    //    foreach (GridViewRow gridViewRow in this.gvApplyList.Rows) {
    //        if (gridViewRow.RowType == DataControlRowType.DataRow) {
    //            int FormId = (int)this.gvApplyList.DataKeys[gridViewRow.RowIndex].Value;
    //            CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("CheckCtl");
    //            if (checkedFormIds.Contains(FormId)) {
    //                checkCtl.Checked = true;
    //            } else {
    //                checkCtl.Checked = false;
    //            }
    //        }
    //    }
    //}

    //private List<int> GetCheckedFormIds() {
    //    List<int> checkedFormIds = (List<int>)this.ViewState["CheckedFormIds"];
    //    if (checkedFormIds == null) {
    //        checkedFormIds = new List<int>();
    //    }
    //    foreach (GridViewRow gridViewRow in this.gvApplyList.Rows) {
    //        if (gridViewRow.RowType == DataControlRowType.DataRow) {
    //            int FormId = (int)this.gvApplyList.DataKeys[gridViewRow.RowIndex].Value;
    //            CheckBox checkCtl = (CheckBox)gridViewRow.FindControl("CheckCtl");
    //            if (checkCtl.Checked) {
    //                if (!checkedFormIds.Contains(FormId)) {
    //                    checkedFormIds.Add(FormId);
    //                }
    //            } else {
    //                if (checkedFormIds.Contains(FormId)) {
    //                    checkedFormIds.Remove(FormId);
    //                }
    //            }
    //        }
    //    }

    //    return checkedFormIds;
    //}

    protected void CreateBtn_Click(object sender, EventArgs e) {
        string strFormID = string.Empty;
        string strFormNo = string.Empty;
        bool isSameCustID = true;
        bool isSamePaymentType = true;
        bool isSameYear = true;
        bool isSameCategory = true;
        int paymentTypeID = 0;
        int custID = 0;
        int year = 0;
        int categoryID = 0;
        MasterDataBLL masterBLL = new MasterDataBLL();
        string intFormID = string.Empty;
        foreach (GridViewRow row in this.gvApplyList.Rows) {
            CheckBox CheckCtl = (CheckBox)row.FindControl("CheckCtl");
            if (CheckCtl.Checked) {
                FormDS.FormApplyRow formApply = new SalesApplyBLL().GetFormApplyByID((int)this.gvApplyList.DataKeys[row.RowIndex].Value)[0];
                FormDS.FormRow form = new SalesApplyBLL().GetFormByID((int)this.gvApplyList.DataKeys[row.RowIndex].Value)[0];
                intFormID = formApply.FormApplyID.ToString();
                if (custID == 0) {
                    custID = formApply.CustomerID;
                } else {
                    if (custID != formApply.CustomerID) {
                        isSameCustID = false;
                        break;
                    }
                }
                if (paymentTypeID == 0) {
                    paymentTypeID = formApply.PaymentTypeID;
                } else {
                    if (paymentTypeID != formApply.PaymentTypeID) {
                        isSamePaymentType = false;
                        break;
                    }
                }
                if (year == 0) {
                    year = formApply.Period.AddMonths(-3).Year;
                } else {
                    if (year != formApply.Period.AddMonths(-3).Year) {
                        isSameYear = false;
                        break;
                    }
                }
                if (categoryID == 0) {
                    categoryID = masterBLL.GetExpenseSubCategoryById(formApply.ExpenseSubCategoryID).ExpenseCategoryID;
                } else {
                    if (categoryID != masterBLL.GetExpenseSubCategoryById(formApply.ExpenseSubCategoryID).ExpenseCategoryID) {
                        isSameCategory = false;
                        break;
                    }
                }
                if (strFormID == string.Empty) {
                    strFormID = this.gvApplyList.DataKeys[row.RowIndex].Value.ToString();
                } else {
                    strFormID = strFormID + "," + this.gvApplyList.DataKeys[row.RowIndex].Value.ToString();
                }
                if (strFormNo == string.Empty) {
                    strFormNo = "P" + form.FormNo + "P";
                } else {
                    strFormNo = strFormNo + "P" + form.FormNo + "P"; ;
                }
            }
        }

        if (strFormID == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择申请单!");
            return;
        }

        if (!isSameCustID) {
            PageUtility.ShowModelDlg(this, "请选择相同客户的申请单!");
            return;
        }
        if (!isSamePaymentType) {
            PageUtility.ShowModelDlg(this, "请选择相同支付方式的申请单!");
            return;
        }
        if (!isSameYear) {
            PageUtility.ShowModelDlg(this, "请选择同一年的申请单!");
            return;
        }
        if (!isSameCategory) {
            PageUtility.ShowModelDlg(this, "请选择同一费用大类的申请单!");
            return;
        }
        

        string url = string.Empty;
        if (paymentTypeID == (int)SystemEnums.PaymentType.ShiWu) {
            //如果支付方式是实物，那么只能对应一张申请单
            if (strFormID.Contains(",")) {
                PageUtility.ShowModelDlg(this, "实物报销只能对应一张申请单!");
                return;
            }
            //如果有多个费用项，不能报销
            if (new SalesReimburseBLL().QueryExpenseItemCountByReimburseID(int.Parse(strFormID))>1) {
                PageUtility.ShowModelDlg(this, "实物报销对应申请单只能有一个费用项!");
                return;
            }
            if (this.ViewState["SearchCondition"] != null) {
                url = "~/SalesForm/ReimburseGoodsApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&PaymentTypeID=" + paymentTypeID.ToString() + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
            } else {
                url = "~/SalesForm/ReimburseGoodsApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&PaymentTypeID=" + paymentTypeID.ToString() + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplySelectList.aspx");
            }
        } else {
            if (this.ViewState["SearchCondition"] != null) {
                url = "~/SalesForm/ReimburseMoneyApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&PaymentTypeID=" + paymentTypeID.ToString() + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
            } else {
                url = "~/SalesForm/ReimburseMoneyApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&PaymentTypeID=" + paymentTypeID.ToString() + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/SalesForm/FormApplySelectList.aspx");
            }
        }
        Response.Redirect(url);
    }
}
