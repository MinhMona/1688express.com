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


namespace NHST.Admin
{
    public partial class Warehouse_Management : System.Web.UI.Page
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

                    //if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8)
                    //    Response.Redirect("/trang-chu");
                    //else
                    //{
                    //    if (ac.RoleID == 0 || ac.RoleID == 4)
                    //        ltrrole.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/Add-Package.aspx\">Thêm bao hàng</a>";
                    //}
                    if (ac.RoleID == 0 || ac.RoleID == 4 || ac.RoleID == 5 || ac.RoleID == 2 || ac.RoleID == 3)
                        ltrrole.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/Add-Package.aspx\">Thêm bao hàng</a>";
                    else
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = BigPackageController.GetAll(tSearchName.Text.Trim());
            List<Warehouse> whs = new List<Warehouse>();
            if (la.Count > 0)
            {
                foreach (var item in la)
                {
                    Warehouse w = new Warehouse();
                    w.ID = item.ID;
                    w.PackageCode = item.PackageCode;
                    w.Weight = Math.Round(Convert.ToDouble(item.Weight),1).ToString();
                    w.Volume = Math.Round(Convert.ToDouble(item.Volume),1).ToString();
                    w.Status = PJUtils.IntToStringStatusPackage(Convert.ToInt32(item.Status));
                    string smallstring = "";
                    var smalls = SmallPackageController.GetBuyBigPackageID(item.ID, "");
                    if (smalls.Count > 0)
                    {
                        smallstring += "<table class=\"table table-bordered table-hover\">";
                        foreach (var s in smalls)
                        {
                            smallstring += "<tr><td>" + s.OrderTransactionCode + "</td></tr>";
                        }
                        smallstring += "</table>";
                    }
                    w.OrderCodeList = smallstring;
                    w.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);
                    whs.Add(w);
                }
            }
            gr.DataSource = whs;
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
        public class Warehouse
        {
            public int ID { get; set; }
            public string PackageCode { get; set; }
            public string Weight { get; set; }
            public string Volume { get; set; }
            public string Status { get; set; }
            public string OrderCodeList { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}