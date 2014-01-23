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

public partial class ManageFeeBudget : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "管理费用预算");
            this.Page.Title = "管理费用预算";
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.BudgetManageFee, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.BudgetManageFee, SystemEnums.OperateEnum.Manage);
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
        UserControls_OUSelect ucNewOuSelect = (UserControls_OUSelect)this.BudgetAddFormView.FindControl("ucNewOuSelect");
        if (ucNewOuSelect.OUId == null) {
            PageUtility.ShowModelDlg(this.Page, "请选择预算部门!");
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
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;

    }

    protected void odsBudget_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["UserID"] = ((AuthorizationDS.StuffUserRow)this.Session["StuffUser"]).StuffUserId;
        e.InputParameters["PositionID"] = ((AuthorizationDS.PositionRow)this.Session["Position"]).PositionId;
    }

    protected void SearchBtn_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string filterStr = "1=1";

            if (ucSearchOU.OUId != null) {
                filterStr += " AND OrganizationUnitID = " + this.ucSearchOU.OUId.ToString();
            }
            if (SearchExpenseTypeDDL.SelectedValue != "0") {
                filterStr += " AND ExpenseManageTypeID = " + this.SearchExpenseTypeDDL.SelectedValue;
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
            this.odsHistory.SelectParameters["BudgetManageFeeID"].DefaultValue = "";
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

    public string GetExpenseTypeNameByID(object ExpenseTypeID) {
        int id = Convert.ToInt32(ExpenseTypeID);
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

    protected void GVBudget_SelectedIndexChanged(object sender, EventArgs e) {
        // 将选中的“编码”传给“子类别”
        this.odsHistory.SelectParameters["BudgetManageFeeID"].DefaultValue = this.GVBudget.SelectedValue.ToString();
    }

    protected void odsBudget_Updating(object sender, ObjectDataSourceStatusEventArgs e) {
        this.GVHistory.DataBind();
        this.UPHistory.Update();
    }

}
