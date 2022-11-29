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
using System.Web.Services;

namespace NHST.Admin
{
    public partial class Report_VCH : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 2)
                    {
                        //loaddata();
                    }
                    else
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                List<ReportTrans> rs = new List<ReportTrans>();
                var r = ExportRequestTurnController.GetByFilter_SqlHelper(rFD.SelectedDate.ToString(), rTD.SelectedDate.ToString());
                if (r.Count > 0)
                {
                    double totalWeightAll = 0;
                    double totalPriceCYNAll = 0;
                    double totalPriceVNDAll = 0;
                    foreach (var item in r)
                    {
                        ReportTrans t = new ReportTrans();
                        t.ID = item.ID;
                        t.CreatedBy = item.CreatedBy;
                        t.DateRequest = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
                        string dateOutWH = "";
                        int TotalPackages = 0;
                        var re = RequestOutStockController.GetByExportRequestTurnID(item.ID);
                        if (re.Count > 0)
                        {
                            dateOutWH += "<table class=\"table table-bordered table-hover\">";
                            dateOutWH += "<tr>";
                            dateOutWH += "  <th>Mã vận đơn</th>";
                            dateOutWH += "  <th>Ngày XK</th>";
                            dateOutWH += "</tr>";
                            TotalPackages = re.Count;
                            foreach (var ro in re)
                            {
                                dateOutWH += "<tr>";
                                var smallpack = SmallPackageController.GetByID(Convert.ToInt32(ro.SmallPackageID));
                                if (smallpack != null)
                                {
                                    dateOutWH += "<td>" + smallpack.OrderTransactionCode + "</td>";
                                    if (smallpack.DateOutWH != null)
                                    {
                                        dateOutWH += "<td>" + string.Format("{0:dd/MM/yyyy}", smallpack.DateOutWH) + "</td>";
                                    }
                                    else
                                    {
                                        dateOutWH += "<td><span class=\"bg-red\">Chưa xuất kho</span></td>";
                                    }
                                }
                                dateOutWH += "<tr>";
                            }
                            dateOutWH += "</table>";
                        }

                        t.DateOutWH = dateOutWH;
                        t.TotalPackages = TotalPackages.ToString();
                        totalWeightAll += Convert.ToDouble(item.TotalWeight);
                        totalPriceCYNAll += Convert.ToDouble(item.TotalPriceCYN);
                        totalPriceVNDAll += Convert.ToDouble(item.TotalPriceVND);
                        t.TotalWeight = Math.Round(Convert.ToDouble(item.TotalWeight), 1).ToString();
                        t.TotalPrice = string.Format("{0:N0}", item.TotalPriceVND) + " VNĐ - Quy đổi: " + Math.Round(Convert.ToDouble(item.TotalPriceCYN), 2) + " tệ";
                        t.StaffNote = item.StaffNote;
                        t.ShippingTypeInVNID = Convert.ToInt32(item.ShippingTypeInVNID);
                        rs.Add(t);
                    }
                    lblWeightAll.Text = totalWeightAll.ToString();
                    lblPriceAllVND.Text = string.Format("{0:N0}", totalPriceVNDAll);
                    lblPriceAllCYN.Text = Math.Round(Convert.ToDouble(totalPriceCYNAll), 2).ToString();
                }
                else
                {
                    lblWeightAll.Text = "0";
                    lblPriceAllVND.Text = "0";
                    lblPriceAllCYN.Text = "0";
                }

                gr.DataSource = rs;
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
        public static string UpdateStaffNote(int ID, string staffNote)
        {
            var ex = ExportRequestTurnController.GetByID(ID);
            if (ex != null)
            {
                ExportRequestTurnController.UpdateStaffNote(ID, staffNote);
                return "ok";
            }
            return "none";
        }
        #endregion
        public class ReportTrans
        {
            public int ID { get; set; }
            public string CreatedBy { get; set; }
            public string DateRequest { get; set; }
            public string DateOutWH { get; set; }
            public string TotalPackages { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPrice { get; set; }
            public string StaffNote { get; set; }
            public int ShippingTypeInVNID { get; set; }
        }
    }
}