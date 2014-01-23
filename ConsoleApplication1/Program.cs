using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessObjects.FormDSTableAdapters;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            //FormReimburseInvoiceTableAdapter taInvoice = new FormReimburseInvoiceTableAdapter();
            //FormTableAdapter taForm = new FormTableAdapter();
            //FormDS.FormReimburseInvoiceDataTable tbInvoice = taInvoice.GetData();
            //foreach (FormDS.FormReimburseInvoiceRow item in tbInvoice) {
            //    if (!item.IsSystemInfoNull()) {
            //        string SystemInfo = item.SystemInfo;
            //        string[] FormNos = SystemInfo.Split(',');
            //        string RepeatFormStr = "";
            //        if (FormNos.Length > 0) {
            //            foreach (string FormNo in FormNos) {
            //                if (!string.IsNullOrEmpty(FormNo)) {
            //                    FormDS.FormDataTable tbForm = taForm.GetDataByFormNo(FormNo);
            //                    if (tbForm != null && tbForm.Count > 0) {
            //                        RepeatFormStr += "P" + tbForm[0].FormID + ":" + tbForm[0].FormNo + "P";
            //                    }
            //                }
            //            }
            //        }
            //        item.SystemInfo = RepeatFormStr;
            //        taInvoice.Update(item);
            //    }
            //}

            //FormReimburseInvoiceTableAdapter taInvoice = new FormReimburseInvoiceTableAdapter();
            //FormTableAdapter taForm = new FormTableAdapter();
            //FormDS.FormReimburseInvoiceDataTable tbInvoice = taInvoice.GetData();
            //foreach (FormDS.FormReimburseInvoiceRow item in tbInvoice) {
            //    if (!item.IsSystemInfoNull()) {
            //        string SystemInfo = item.SystemInfo;
            //        SystemInfo = SystemInfo.Replace("PP", "P");
            //        string[] FormNos = SystemInfo.Split('P');
            //        string RepeatFormStr = "";
            //        if (FormNos.Length > 0) {
            //            foreach (string FormNo in FormNos) {
            //                if (!string.IsNullOrEmpty(FormNo)) {
            //                    FormDS.FormDataTable tbForm = taForm.GetDataByID(int.Parse(FormNo.Split(':')[0]));
            //                    if (tbForm != null && tbForm.Count > 0) {
            //                        RepeatFormStr += "P" + tbForm[0].FormID + ":" + tbForm[0].FormNo + "P";
            //                    }
            //                }
            //            }
            //        }
            //        item.SystemInfo = RepeatFormStr;
            //        taInvoice.Update(item);
            //    }
            //}

            FormReimburseInvoiceTableAdapter taInvoice = new FormReimburseInvoiceTableAdapter();
            FormTableAdapter taForm = new FormTableAdapter();
            FormDS.FormReimburseInvoiceDataTable tbInvoice = taInvoice.GetData();
            foreach (FormDS.FormReimburseInvoiceRow item in tbInvoice) {
                if (!item.IsSystemInfoNull()) {
                    string SystemInfo = item.SystemInfo;
                    SystemInfo = SystemInfo.Replace("PP", "P");
                    string[] FormNos = SystemInfo.Split('P');
                    string RepeatFormStr = "";
                    if (FormNos.Length > 0) {
                        foreach (string FormNo in FormNos) {
                            if (!string.IsNullOrEmpty(FormNo)) {
                                FormDS.FormDataTable tbForm = taForm.GetDataByID(int.Parse(FormNo.Split(':')[0]));
                                if (tbForm != null && tbForm.Count > 0) {
                                    RepeatFormStr += "P" + tbForm[0].FormID + ":" + tbForm[0].FormNo + ":" + tbForm[0].PageType + "P";
                                }
                            }
                        }
                    }
                    item.SystemInfo = RepeatFormStr;
                    taInvoice.Update(item);
                }
            }
        }
    }
}
