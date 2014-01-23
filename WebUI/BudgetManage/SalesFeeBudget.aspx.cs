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

public partial class SalesFeeBudget : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "销售费用预算");
            this.Page.Title = "销售费用预算";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.BudgetSalesFee, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.BudgetSalesFee, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            this.GVBudget.Columns[8].Visible = (bool)this.ViewState["HasManageRight"];
        }
        DropDownList newExpenseItemDDL = (DropDownList)this.BudgetAddFormView.FindControl("newExpenseItemDDL");
        newExpenseItemDDL.Attributes.Add("onmouseover", "FixWidth(this)");
    }

    protected bool HasViewRight {
        get {
            return (bool)this.ViewState["HasViewRight"];
        }
        set {
            this.ViewState["HasViewRight"] = value;
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

    public string GetOUNameByOuID(object ouID) {
        int id = Convert.ToInt32(ouID);
        return new OUTreeBLL().GetOrganizationUnitById(id).OrganizationUnitName;
    }

    protected void odsBudget_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_CustomerControl ucNewCustomerSelect = (UserControls_CustomerControl)this.BudgetAddFormView.FindControl("ucNewCustomerSelect");
        if (ucNewCustomerSelect.CustomerID == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请选择预算客户!");
            e.Cancel = true;
            return;
        }

        UserControls_YearAndMonthUserControl ucNewPeriod = (UserControls_YearAndMonthUserControl)this.BudgetAddFormView.FindControl("ucNewPeriod");
        string period = ((TextBox)(ucNewPeriod.FindControl("txtDate"))).Text.Trim();
        if (period == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入费用期间!");
            e.Cancel = true;
            return;
        } else {
            DateTime yearAndMonth = DateTime.Parse(period.Substring(0, 4) + "-" + period.Substring(4, 2) + "-01");
            e.InputParameters["Period"] = yearAndMonth;
        }

        TextBox txtModifyReason = (TextBox)this.BudgetAddFormView.FindControl("txtNewModifyReason");
        if (txtModifyReason.Text == string.Empty) {
            e.InputParameters["ModifyReason"] = string.Empty;
        }

        TextBox txtNewAdjustBudget = (TextBox)this.BudgetAddFormView.FindControl("txtNewAdjustBudget");
        if (txtNewAdjustBudget.Text == string.Empty) {
            e.InputParameters["AdjustBudget"] = decimal.Zero;
        }
        TextBox txtNewTransferBudget = (TextBox)this.BudgetAddFormView.FindControl("txtNewTransferBudget");
        if (txtNewTransferBudget.Text == string.Empty) {
            e.InputParameters["TransferBudget"] = decimal.Zero;
        }
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;

    }

    protected void odsBudget_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsBudget_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
    }

    protected void odsBudget_Updating(object sender, ObjectDataSourceStatusEventArgs e) {
        this.GVHistory.DataBind();
        this.UPHistory.Update();
    }

    protected void odsBudget_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsBudget_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string filterStr = "1=1";

            if (ucSearchCustomer.CustomerID != "") {
                filterStr += " AND CustomerID = " + this.ucSearchCustomer.CustomerID.ToString();
            }
            if (SearchExpenseItemDDL.SelectedValue != "") {
                filterStr += " AND ExpenseItemID = " + this.SearchExpenseItemDDL.SelectedValue;
            }
            if (dplCustomerTypeBySearch.SelectedValue != "") {
                filterStr += " AND CustomerID in (select CustomerID from Customer where CustomerTypeID=" + this.dplCustomerTypeBySearch.SelectedValue + ")";
            }
            if (dplChannelTypeBySearch.SelectedValue != "") {
                filterStr += " AND CustomerID in (select CustomerID from Customer where ChannelTypeID=" + this.dplChannelTypeBySearch.SelectedValue + ")";
            }
            if (dplCityBySearch.SelectedValue != "") {
                filterStr += " AND CustomerID in (select CustomerID from Customer where CityID=" + this.dplCityBySearch.SelectedValue + ")";
            }
            //费用期间
            string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
            if (startPeriod != null && startPeriod != string.Empty) {
                string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
                filterStr += " AND Period >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                    " AND Period<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            }
            this.odsBudget.SelectParameters["queryExpression"].DefaultValue = filterStr;
            this.GVBudget.DataBind();
            this.UPBudget.Update();
        }
    }

    protected bool checkSearchConditionValid() {
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

    public string GetExpenseItemeNameByID(object ExpenseItemeID) {
        int id = Convert.ToInt32(ExpenseItemeID);
        return new MasterDataBLL().GetExpenseManageTypeByID(id).ExpenseManageTypeName;
    }

    public string GetUserNameByID(object UserID) {
        int id = Convert.ToInt32(UserID);
        return new StuffUserBLL().GetStuffUserById(id)[0].StuffName;
    }

    public string GetPositionNameByID(object PositionID) {
        int id = Convert.ToInt32(PositionID);
        return new OUTreeBLL().GetPositionById(id).PositionName;
    }

    public string GetCustomerNameByID(object CustomerId) {
        return new MasterDataBLL().GetCustomerById((int)CustomerId).CustomerName;
    }

    public string GetExpenseItemNameByID(object Id) {
        //return new MasterDataBLL().GetExpenseItemByID((int)Id).ExpenseItemName;
        ERS.ExpenseItemRow row = new MasterDataBLL().GetExpenseItemByID((int)Id);
        return row.AccountingCode + "--" + row.ExpenseItemName;
    }

    protected void GVBudget_SelectedIndexChanged(object sender, EventArgs e) {
        this.odsHistory.SelectParameters["BudgetSalesFeeID"].DefaultValue = this.GVBudget.SelectedValue.ToString();
    }
}