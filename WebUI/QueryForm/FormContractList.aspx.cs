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

public partial class QueryForm_FormContractList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "合同查询");
            this.Page.Title = "合同查询";
            if (Request["Search"] == null) {
                int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
                this.odsContractList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsContractList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsContractList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormContract, SystemEnums.OperateEnum.Manage);
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

                if (this.Request["ContractTypeID"] != null)
                    this.dplContractType.SelectedValue = this.Request["ContractTypeID"].ToString();

                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();

                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                /*
                //是否盖章
                if (this.Request["IsStamped"] != null) {
                    if (this.Request["IsStamped"].ToString().Equals("3")) {
                        this.IsStampedDDL.SelectedValue = "3";
                    } else if (this.Request["IsStamped"].ToString().Equals("1")) {
                        this.IsStampedDDL.SelectedValue = "1";
                    } else if (this.Request["IsStamped"].ToString().Equals("0")) {
                        this.IsStampedDDL.SelectedValue = "0";
                    }
                }
                //是否正本已回收
                if (this.Request["IsRecovery"] != null) {
                    if (this.Request["IsRecovery"].ToString().Equals("3")) {
                        this.IsRecoveryDDL.SelectedValue = "3";
                    } else if (this.Request["IsRecovery"].ToString().Equals("1")) {
                        this.IsRecoveryDDL.SelectedValue = "1";
                    } else if (this.Request["IsRecovery"].ToString().Equals("0")) {
                        this.IsRecoveryDDL.SelectedValue = "0";
                    }
                }
                */
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

    protected void gvContractList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormContractViewRow row = (QueryDS.FormContractViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.Cells[0].FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/QueryForm/FormContractList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvContractList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/QueryForm/FormContractList.aspx");
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
                this.gvContractList.PageIndex = int.Parse(start);
            }

            this.odsContractList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.odsContractList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsContractList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvContractList.DataBind();
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
            int count = bll.QueryFormContractViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new FormContractViewForExportByRightTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormContract" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
        //合同编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND ContractNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //签约对方单位
        if (txtCompanyName.Text.Trim() != string.Empty) {
            filterStr += " AND (FirstCompany like '%" + this.txtCompanyName.Text.Trim() + "%'";
            filterStr += " OR SecondCompany like '%" + this.txtCompanyName.Text.Trim() + "%'";
            filterStr += " OR ThirdCompany like '%" + this.txtCompanyName.Text.Trim() + "%')";
            this.ViewState["SearchCondition"] += "&CompanyName=" + this.txtCompanyName.Text.Trim();
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

        //合同类型
        if (this.dplContractType.SelectedValue != string.Empty) {
            filterStr += " AND FormContract.ContractTypeID = " + dplContractType.SelectedValue;
            this.ViewState["SearchCondition"] += "&ContractTypeID=" + this.dplContractType.SelectedValue;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //是否盖章
        if (this.IsStampedDDL.SelectedValue != "3") {
            if (this.IsStampedDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&isStamped=1";
                filterStr = filterStr + " And isStamped = 1";
            } else if (this.IsStampedDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&isStamped=0";
                filterStr = filterStr + " And isStamped=0";
            }
        }

        //是否正本已回收
        if (this.IsRecoveryDDL.SelectedValue != "3") {
            if (this.IsRecoveryDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&isRecovery=1";
                filterStr = filterStr + " And isRecovery = 1";
            } else if (this.IsRecoveryDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&isRecovery=0";
                filterStr = filterStr + " And isRecovery=0";
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
    public string GetContractETD(string beginDate, string endDate) {
        return beginDate + "-" + endDate;
    }
}
