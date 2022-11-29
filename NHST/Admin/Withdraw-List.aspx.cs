using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace NHST.Admin
{
    public partial class Withdraw_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 7 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                    if (ac.RoleID == 0)
                        ltr.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/add-withdraw.aspx\">Thêm lệnh rút</a>";
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = WithdrawController.GetAll(tSearchName.Text.Trim());
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion
        #region Webservice
        [WebMethod]
        public static string GetData(int ID)
        {
            var nap = WithdrawController.GetByID(ID);
            if (nap != null)
            {
                NaptienInfo n = new NaptienInfo();
                int UID = Convert.ToInt32(nap.UID);
                double Amount = Convert.ToDouble(nap.Amount);
                var ai = AccountInfoController.GetByUserID(UID);
                if (ai != null)
                {
                    n.FullName = ai.FirstName + " " + ai.LastName;
                    n.Address = ai.Address;
                }
                n.Money = string.Format("{0:N0}", Amount);
                if (!string.IsNullOrEmpty(nap.Note))
                    n.Note = nap.Note;
                DateTime currentDate = DateTime.Now;
                string CreateDate = "Ngày " + currentDate.Day + " tháng " + currentDate.Month + " năm " + currentDate.Year;
                n.CreateDate = CreateDate;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(n);
            }
            return "null";
        }
        public class NaptienInfo
        {
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Money { get; set; }
            public string Note { get; set; }
            public string CreateDate { get; set; }
        }
        #endregion
    }
}