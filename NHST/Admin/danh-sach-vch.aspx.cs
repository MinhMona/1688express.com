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
using MB.Extensions;
using System.Text;

namespace NHST.Admin
{
    public partial class danh_sach_vch : System.Web.UI.Page
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
                    if (ac != null)
                        if (ac.RoleID == 1)
                            Response.Redirect("/trang-chu");

                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                int status = ddlStatus.SelectedValue.ToInt(-1);
                List<tbl_TransportationOrderNew> oAll = new List<tbl_TransportationOrderNew>();
                var os = TransportationOrderNewController.GetAllWithFilter_SqlHelper(status, fromdate, todate);
                if (os.Count > 0)
                {
                    var os0 = os.Where(o => o.Status == 0).ToList();
                    var os1 = os.Where(o => o.Status == 1).ToList();
                    var os2 = os.Where(o => o.Status == 2).ToList();
                    var os3 = os.Where(o => o.Status == 3).ToList();
                    var os4 = os.Where(o => o.Status == 4).ToList();
                    var os5 = os.Where(o => o.Status == 5).ToList();
                    var os6 = os.Where(o => o.Status == 6).ToList();
                    if (os3.Count > 0)
                    {
                        foreach (var o in os3)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os1.Count > 0)
                    {
                        foreach (var o in os1)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os2.Count > 0)
                    {
                        foreach (var o in os2)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os4.Count > 0)
                    {
                        foreach (var o in os4)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os5.Count > 0)
                    {
                        foreach (var o in os5)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os6.Count > 0)
                    {
                        foreach (var o in os6)
                        {
                            oAll.Add(o);
                        }
                    }
                    if (os0.Count > 0)
                    {
                        foreach (var o in os0)
                        {
                            oAll.Add(o);
                        }
                    }
                    gr.DataSource = oAll;
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
        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public int STT { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
            public string dathang { get; set; }
            public string kinhdoanh { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
        }
    }
}