using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using MB.Extensions;
using System.Text;

namespace NHST.admin
{
    public partial class Payforlist : System.Web.UI.Page
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
                string textsearch = "";
                int type = 1;
                int statuss = 0;
                string fd = "";
                string td = "";
                double fPrice = 0;
                double tPrice = 0;

                textsearch = tSearchName.Text.Trim();
                type = ddlType.SelectedValue.ToInt(1);
                statuss = ddlStatus.SelectedValue.ToInt(0);
                fPrice = Convert.ToDouble(rPriceFrom.Value);
                tPrice = Convert.ToDouble(rPriceTo.Value);
                fd = rFD.SelectedDate.ToString();
                td = rTD.SelectedDate.ToString();

                var orders = PayhelpController.GetAllWithSearchTextStatusFromPriceToPriceFromDateToDate_SqlHelper1(textsearch,
                    type, statuss, fPrice.ToString(), tPrice.ToString(), fd, td);
                List<Danhsachyeucau> ds = new List<Danhsachyeucau>();
                if (orders.Count > 0)
                {
                    foreach (var o in orders)
                    {
                        int status = Convert.ToInt32(o.Status);
                        Danhsachyeucau d = new Danhsachyeucau();
                        d.ID = o.ID;
                        d.Username = o.Username;
                        d.TotalPriceCYN = Convert.ToDouble(o.TotalPriceCYN);
                        d.TotalPriceCYN_String = string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceCYN)).Replace(",", ".");

                        if (status == 4)
                        {
                            d.TotalPriceVND = Convert.ToDouble(o.TotalPriceVND);
                            d.TotalPriceVND_String = string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)).Replace(",", ".");
                        }
                        else
                        {
                            d.TotalPriceVND = 0;
                            d.TotalPriceVND_String = "";
                        }

                        string stt = PJUtils.ReturnStatusPayHelp(Convert.ToInt32(o.Status));
                        d.statusstring = stt;
                        d.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", o.DateSend);
                        ds.Add(d);
                    }
                }
                gr.DataSource = ds;
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
        public class Danhsachyeucau
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public double TotalPriceCYN { get; set; }
            public string TotalPriceCYN_String { get; set; }
            public double TotalPriceVND { get; set; }
            public double Currency { get; set; }
            public object IsNotComplete { get; set; }
            public string TotalPriceVND_String { get; set; }
            public string statusstring { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}