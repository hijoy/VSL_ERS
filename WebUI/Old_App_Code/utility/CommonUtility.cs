using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using System.Text;
using lib.wf;

/// <summary>
/// Summary description for CommonUtility
/// </summary>
public class CommonUtility {

    public static string GetPathName() {
        return System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    }

    public static string GetStatusName(int statusID) {
        string returnStr = string.Empty;
        switch (statusID) {

            case (int)SystemEnums.FormStatus.Awaiting:
                returnStr = "待审批";
                break;
            case (int)SystemEnums.FormStatus.ApproveCompleted:
                returnStr = "审批完成";
                break;
            case (int)SystemEnums.FormStatus.Rejected:
                returnStr = "退回待修改";
                break;
            case (int)SystemEnums.FormStatus.Scrap:
                returnStr = "作废";
                break;
        }
        return returnStr;
    }

    public static string GetFormPostBackUrl(int FormID, int PageType, int StatusID, string SourceUrl) {
        string returnStr = string.Empty;
        if (StatusID == (int)SystemEnums.FormStatus.Draft) {
            switch (PageType) {
                case (int)SystemEnums.PageType.PromotionApply:
                    returnStr = "~/SalesForm/SalesPromotionApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.GeneralApply:
                    returnStr = "~/SalesForm/SalesGeneralApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RebateApply:
                    returnStr = "~/SalesForm/SalesRebateApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.MaterialApply:
                    returnStr = "~/OtherForm/MaterialApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ContractApply:
                    returnStr = "~/OtherForm/ContractApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PersonalReimburseApply:
                    returnStr = "~/OtherForm/PersonalReimburseApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ReimburseMoneyApply:
                    returnStr = "~/SalesForm/ReimburseMoneyApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ReimburseGoodsApply:
                    returnStr = "~/SalesForm/ReimburseGoodsApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.BudgetAllocationApply:
                    returnStr = "~/OtherForm/BudgetAllocationApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.TravelApply:
                    returnStr = "~/OtherForm/TravelApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.TravelReimburse:
                    returnStr = "~/OtherForm/TravelReimburseApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
            }
        } else {
            switch (PageType) {
                case (int)SystemEnums.PageType.PromotionApply:
                    returnStr = "~/SalesForm/SalesPromotionApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.GeneralApply:
                    returnStr = "~/SalesForm/SalesGeneralApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RebateApply:
                    returnStr = "~/SalesForm/SalesRebateApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.MaterialApply:
                    returnStr = "~/OtherForm/MaterialApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ContractApply:
                    returnStr = "~/OtherForm/ContractApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PersonalReimburseApply:
                    returnStr = "~/OtherForm/PersonalReimburseApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ReimburseMoneyApply:
                    returnStr = "~/SalesForm/ReimburseMoneyApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ReimburseGoodsApply:
                    returnStr = "~/SalesForm/ReimburseGoodsApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.BudgetAllocationApply:
                    returnStr = "~/OtherForm/BudgetAllocationApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.GeneralApplyExecute:
                    returnStr = "~/SalesForm/SalesGeneralExecution.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PromotionApplyExecute:
                    returnStr = "~/SalesForm/SalesPromotionExecution.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.TravelApply:
                    returnStr = "~/OtherForm/TravelApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.TravelReimburse:
                    returnStr = "~/OtherForm/TravelReimburseApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
            }
        }
        return returnStr;
    }

    public static string ByteSubString(string strInput, int startIndex, int length) {
        strInput = strInput.Trim();
        int byteLen = Encoding.Default.GetByteCount(strInput);
        if (byteLen > length) {
            string resultStr = String.Empty;
            for (int i = startIndex / 2; i < strInput.Length; i++) {
                if (Encoding.Default.GetByteCount(resultStr) < length) {
                    resultStr += strInput.Substring(i, 1);
                } else {
                    break;
                }
            }
            return resultStr;
        } else {
            return strInput;
        }
    }

    public static APHelper GetAPHelper(System.Web.SessionState.HttpSessionState session) {
        if (session["APHelper"] != null) {
            return (APHelper)session["APHelper"];
        } else {
            APHelper ap = new APHelper();
            session["APHelper"] = ap;
            return ap;
        }
    }

    public static HyperLink GetPostbackURLForRepeatForm(String RepeatFormStr) {
        HyperLink hl = new HyperLink();
        string[] RepeatInfo = RepeatFormStr.Split(':');
        hl.Text = RepeatInfo[1];
        string url = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + @"/";
        switch (int.Parse(RepeatInfo[2])) {
            case (int)SystemEnums.PageType.GeneralApply:
                url += "SalesForm/SalesGeneralApproval.aspx?ObjectId=" + RepeatInfo[0];
                break;
            case (int)SystemEnums.PageType.GeneralApplyExecute:
                url += "SalesForm/SalesGeneralExecution.aspx?ObjectId=" + RepeatInfo[0];
                break;
            case (int)SystemEnums.PageType.PromotionApply:
                url += "SalesForm/SalesPromotionApproval.aspx?ObjectId=" + RepeatInfo[0];
                break;
            case (int)SystemEnums.PageType.PromotionApplyExecute:
                url += "SalesForm/SalesPromotionExecution.aspx?ObjectId=" + RepeatInfo[0];
                break;
            case (int)SystemEnums.PageType.ReimburseMoneyApply:
                url += "SalesForm/ReimburseMoneyApproval.aspx?ObjectId=" + RepeatInfo[0];
                break;
        }
        url += "&ShowDialog=1";
        hl.NavigateUrl = "javascript:window.showModalDialog('" + url + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
        return hl;
    }

    public static void GenerateRepeatControl(ControlCollection controls, string SystemInfo) {
        if (!string.IsNullOrEmpty(SystemInfo)) {
            SystemInfo = SystemInfo.Replace("PP", "P");
            string[] RepeatFormStr = SystemInfo.Split('P');
            if (RepeatFormStr.Length > 0) {
                controls.Clear();
                foreach (String formStr in RepeatFormStr) {
                    if (!string.IsNullOrEmpty(formStr)) {
                        if (controls.Count != 0) {
                            controls.Add(new LiteralControl(" , "));
                        }
                        controls.Add(CommonUtility.GetPostbackURLForRepeatForm(formStr));
                    }
                }
            }
        }
    }

    public static void InitSplitRate(FormDS.FormApplySplitRateDataTable tbSplitRate, DateTime BeginPeriod, DateTime EndPeriod) {
        int MonthCount = EndPeriod.Month - BeginPeriod.Month;
        if (MonthCount < 0 && MonthCount + 12 > 0 && MonthCount + 12 <= 12) {
            MonthCount += 12;
        }
        int UsedRate = 0;
        if (BeginPeriod != null && EndPeriod != null && MonthCount > 0) {
            for (int i = 0; i < MonthCount + 1; i++) {
                FormDS.FormApplySplitRateRow rowSplitRate = tbSplitRate.NewFormApplySplitRateRow();
                rowSplitRate.FormApplyID = 0;
                if (i == MonthCount) {
                    rowSplitRate.Rate = 100 - UsedRate;
                } else {
                    decimal a = 100;
                    rowSplitRate.Rate = decimal.ToInt32((decimal)100 / (decimal)(MonthCount + 1));
                    UsedRate += rowSplitRate.Rate;
                }
                rowSplitRate.Period = BeginPeriod.AddMonths(i);
                tbSplitRate.AddFormApplySplitRateRow(rowSplitRate);
            }
        }
        if (MonthCount > 0) {
            int tempRate = tbSplitRate[MonthCount].Rate;
            tbSplitRate[MonthCount].Rate = tbSplitRate[0].Rate;
            tbSplitRate[0].Rate = tempRate;
        }
    }

    public static string GetNameByFormID(int FormTypeID, int FormID) {
        string ApplyName = string.Empty;

        if (FormTypeID == (int)SystemEnums.FormType.SalesApply) {
            FormDS.FormApplyRow applyRow = new SalesApplyBLL().GetFormApplyByID(FormID)[0];
            if (!applyRow.IsFormApplyNameNull()) {
                ApplyName = applyRow.FormApplyName.Length > 10 ? applyRow.FormApplyName.Substring(0, 10) : applyRow.FormApplyName;
            }
        }

        if (FormTypeID == (int)SystemEnums.FormType.ContractApply) {
            FormDS.FormContractRow applyRow = new ContractApplyBLL().GetFormContractByID(FormID)[0];
            ApplyName = applyRow.ContractName.Length > 10 ? applyRow.ContractName.Substring(0, 10) : applyRow.ContractName;
        }

        return ApplyName;
    }
}
