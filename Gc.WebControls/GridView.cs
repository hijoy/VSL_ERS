
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gc.WebControls {
    public class GridView : System.Web.UI.WebControls.GridView {

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            //��ʼ��
            this.Initial();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Initial() {
            //����Grid����ɫ
            this.AlternatingRowStyle.CssClass = "gridAlternatingRowStyle";
            this.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
            this.PagerStyle.CssClass = "PagerStyle";

            foreach (DataControlField item in this.Columns) {
                if (item.ItemStyle.HorizontalAlign == HorizontalAlign.NotSet) {
                    item.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
            }
            //
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e) {
            base.OnRowDataBound(e);

            //������������  �н��㱳��ɫ��ʾ
            if (e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Attributes.Add("oldClassName", "");
                e.Row.Attributes.Add("onmouseover", "javascript:this.oldClassName = this.className;this.className='gridMouseOver';");
                e.Row.Attributes.Add("onmouseout", "javascript:this.className=this.oldClassName;");
            }
        }
    }
}
