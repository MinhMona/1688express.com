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
using static NHST.Controllers.MainOrderController;

namespace NHST.manager
{
    public partial class OrderList : System.Web.UI.Page
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
                    if (ac.RoleID == 0)
                        btnExcel.Visible = true;
                }
            }
        }
        #region grid event
        //protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    string username_current = Session["userLoginSystem"].ToString();
        //    tbl_Account ac = AccountController.GetByUsername(username_current);
        //    if (ac != null)
        //    {
        //        int RoleID = Convert.ToInt32(ac.RoleID);
        //        int StaffID = ac.ID;
        //        var lo = MainOrderController.GetByUserInViewFilterWithStatusString(ac.RoleID.ToString().ToInt(), ac.ID, tSearchName.Text.Trim(), ddlType.SelectedValue.ToString().ToInt(1));

        //        gr.DataSource = lo;
        //        #region Code Cũ
        //        //var la = MainOrderController.GetByUserInViewFilter(ac.RoleID.ToString().ToInt(), ac.ID, tSearchName.Text.Trim(), ddlType.SelectedValue.ToString().ToInt(1));

        //        //if (la != null)
        //        //{
        //        //    List<Danhsachorder> ds = new List<Danhsachorder>();
        //        //    if (la.Count > 0)
        //        //    {
        //        //        int i = 1;
        //        //        foreach (var item in la)
        //        //        {
        //        //            Danhsachorder d = new Danhsachorder();
        //        //            d.ID = item.ID;
        //        //            d.STT = i;
        //        //            d.ProductImage = item.anhsanpham;
        //        //            d.TotalPriceVND = item.TotalPriceVND;
        //        //            d.Deposit = item.Deposit;
        //        //            d.CreatedDate = item.CreatedDate.ToString();
        //        //            d.username = item.Uname;
        //        //            d.kinhdoanh = item.saler;
        //        //            d.dathang = item.dathang;
        //        //            d.statusstring = PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status));
        //        //            ds.Add(d);
        //        //            i++;
        //        //        }
        //        //        gr.DataSource = ds;
        //        //    }
        //        //}

        //        #endregion
        //        #region Code mới
        //        //var la = MainOrderController.GetByUserInSQLHelper_nottextnottype(ac.RoleID.ToString().ToInt(), ac.ID);
        //        //if (la.Count > 0)
        //        //{
        //        //    la = la.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
        //        //    if (Request.QueryString["type"] == null && Request.QueryString["s"] == null)
        //        //    {
        //        //        gr.DataSource = la;
        //        //    }
        //        //    else
        //        //    {
        //        //        int type = Request.QueryString["type"].ToInt(1);
        //        //        string search = Request.QueryString["s"];
        //        //        tSearchName.Text = search;
        //        //        ddlType.SelectedValue = type.ToString();
        //        //        var list = new List<OrderGetSQL>();
        //        //        if (type == 1)
        //        //        {
        //        //            foreach (var item in la)
        //        //            {
        //        //                var pros = OrderController.GetByMainOrderID(item.ID);
        //        //                if (pros.Count > 0)
        //        //                {
        //        //                    foreach (var p in pros)
        //        //                    {
        //        //                        if (p.title_origin.Contains(search))
        //        //                        {
        //        //                            list.Add(item);
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            foreach (var item in la)
        //        //            {
        //        //                if (!string.IsNullOrEmpty(item.OrderTransactionCode))
        //        //                {
        //        //                    if (item.OrderTransactionCode.Contains(search))
        //        //                    {
        //        //                        list.Add(item);
        //        //                    }
        //        //                }
        //        //                else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
        //        //                {
        //        //                    if (item.OrderTransactionCode2.Contains(search))
        //        //                    {
        //        //                        list.Add(item);
        //        //                    }
        //        //                }
        //        //                else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
        //        //                {
        //        //                    if (item.OrderTransactionCode3.Contains(search))
        //        //                    {
        //        //                        list.Add(item);
        //        //                    }
        //        //                }
        //        //                else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
        //        //                {
        //        //                    if (item.OrderTransactionCode4.Contains(search))
        //        //                    {
        //        //                        list.Add(item);
        //        //                    }
        //        //                }
        //        //                else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
        //        //                {
        //        //                    if (item.OrderTransactionCode5.Contains(search))
        //        //                    {
        //        //                        list.Add(item);
        //        //                    }
        //        //                }
        //        //            }

        //        //        }

        //        //        gr.DataSource = list;
        //        //    }
        //        //}
        //        #endregion
        //    }
        //}

        public bool ShouldApplySortFilterOrGroup()
        {
            return gr.MasterTableView.FilterExpression != "" ||
                (gr.MasterTableView.GroupByExpressions.Count > 0 || isGrouping) ||
                gr.MasterTableView.SortExpressions.Count > 0;
        }
        bool isGrouping = false;
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                string s = tSearchName.Text.Trim();
                int type = ddlType.SelectedValue.ToString().ToInt(1);
                double priceFrom = Convert.ToDouble(rPriceFrom.Value);
                double priceTo = Convert.ToDouble(rPriceTo.Value);
                string fromdate = rFD.SelectedDate.ToString();
                string todate = rTD.SelectedDate.ToString();
                string status1 = hdfStatus.Value;
                int status = ddlStatus.SelectedValue.ToInt(-1);
                int orderType = 1;
                if (Request.QueryString["ot"] != null)
                {
                    orderType = Request.QueryString["ot"].ToInt(1);
                }
                if (string.IsNullOrEmpty(s) && priceFrom == 0 && priceTo == 0 && string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate) &&
                    status1 == "-1")
                {
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    int StaffID = ac.ID;

                    int totalRow = MainOrderController.getOrderByRoleIDStaffID_SQL(ac.RoleID.ToString().ToInt(), ac.ID, orderType);
                    int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
                    gr.VirtualItemCount = totalRow;
                    int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
                    var lo = MainOrderController.GetByUserInSQLHelper_nottextnottypeWithstatus(ac.RoleID.ToString().ToInt(), orderType, ac.ID, Page, maximumRows);
                    gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
                    gr.DataSource = lo;
                }
                else
                {
                    #region Cách cũ
                    //var la = MainOrderController.GetByUserInViewFilterWithStatusString(ac.RoleID.ToString().ToInt(), ac.ID, tSearchName.Text.Trim(),
                    //    ddlType.SelectedValue.ToString().ToInt(1), 1);
                    //if (priceTo > 0)
                    //{
                    //    if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                    //    {
                    //        DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo &&
                    //            o.CreatedDate >= fd).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo
                    //                            && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && double.Parse(o.TotalPriceVND) <= priceTo).ToList();
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(rFD.SelectedDate.ToString()))
                    //    {
                    //        DateTime fd = Convert.ToDateTime(rFD.SelectedDate);
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.CreatedDate >= fd && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom &&
                    //            o.CreatedDate >= fd).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(rTD.SelectedDate.ToString()))
                    //        {
                    //            DateTime td = Convert.ToDateTime(rTD.SelectedDate);
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom && o.CreatedDate <= td).ToList();
                    //        }
                    //        else
                    //        {
                    //            la = la.Where(o => double.Parse(o.TotalPriceVND) >= priceFrom).ToList();
                    //        }
                    //    }
                    //}

                    //if (status1 != "-1")
                    //{
                    //    var la1 = new List<View_OrderListFilterWithStatusString>();
                    //    string[] sts = status1.Split(',');
                    //    for (int i = 0; i < sts.Length; i++)
                    //    {
                    //        int stat = sts[i].ToInt();
                    //        if (stat > -1)
                    //        {
                    //            var la2 = new List<View_OrderListFilterWithStatusString>();
                    //            la2 = la.Where(o => o.Status == stat).ToList();
                    //            if (la2.Count > 0)
                    //            {
                    //                foreach (var item in la2)
                    //                {
                    //                    la1.Add(item);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    la1 = la1.OrderByDescending(o => o.ID).ToList();
                    //    gr.VirtualItemCount = la1.Count;
                    //    gr.DataSource = la1;
                    //}
                    //else
                    //{
                    //    gr.VirtualItemCount = la.Count;
                    //    gr.DataSource = la;
                    //}
                    #endregion
                    #region Cách mới
                    string fd = rFD.SelectedDate.ToString();
                    string td = rTD.SelectedDate.ToString();
                    var la = MainOrderController.GetByUserInSQLHelperWithFilter(ac.RoleID.ToString().ToInt(),
                        orderType, ac.ID, tSearchName.Text.Trim(),
                       ddlType.SelectedValue.ToString().ToInt(1), fd, td, priceFrom, priceTo, false);
                    if (la.Count > 0)
                    {
                        if (status1 != "-1")
                        {
                            var la1 = new List<OrderGetSQL>();
                            string[] sts = status1.Split(',');
                            for (int i = 0; i < sts.Length; i++)
                            {
                                int stat = sts[i].ToInt();
                                if (stat > -1)
                                {
                                    var la2 = new List<OrderGetSQL>();
                                    la2 = la.Where(o => o.Status == stat).ToList();
                                    if (la2.Count > 0)
                                    {
                                        foreach (var item in la2)
                                        {
                                            la1.Add(item);
                                        }
                                    }
                                }
                            }
                            gr.VirtualItemCount = la1.Count;
                            gr.DataSource = la1;
                        }
                        else
                        {
                            gr.VirtualItemCount = la.Count;
                            gr.DataSource = la;

                        }

                    }
                    else
                    {
                        gr.VirtualItemCount = la.Count;
                        gr.DataSource = la;
                    }
                    #endregion
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
            //string textsearch = tSearchName.Text.Trim();
            //if (!string.IsNullOrEmpty(textsearch))
            //{
            //    Response.Redirect("/admin/orderlist.aspx?type=" + ddlType.SelectedValue + "&s=" + textsearch + "");
            //}
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            var la = MainOrderController.GetAll();
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th><strong>OrderID</strong></th>");
            StrExport.Append("      <th><strong>Người đặt</strong></th>");
            StrExport.Append("      <th><strong>Sản phẩm</strong></th>");
            StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
            StrExport.Append("      <th><strong>Trạng thái</strong></th>");
            StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in la)
            {
                string htmlproduct = "";
                string username = "";
                var ui = AccountController.GetByID(item.UID.ToString().ToInt(1));
                if (ui != null)
                {
                    username = ui.Username;
                }
                var products = OrderController.GetByMainOrderID(item.ID);
                foreach (var p in products)
                {
                    string image_src = p.image_origin;
                    if (!image_src.Contains("http:") && !image_src.Contains("https:"))
                        htmlproduct += "https:" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                    else
                        htmlproduct += "" + p.image_origin + " <br/> " + p.title_origin + "<br/><br/>";
                }
                StrExport.Append("  <tr>");
                StrExport.Append("      <td>" + item.ID + "</td>");
                StrExport.Append("      <td>" + username + "</td>");
                StrExport.Append("      <td>" + htmlproduct + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Math.Floor(item.TotalPriceVND.ToFloat())) + "</td>");
                StrExport.Append("      <td>" + PJUtils.IntToRequestAdmin(Convert.ToInt32(item.Status)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</td>");
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "ExcelReportOrderList.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();
        }
    }
}