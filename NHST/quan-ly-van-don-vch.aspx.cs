using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Services;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class quan_ly_van_don_vch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                LoadData();
                LoadShippingTypeVN();
            }
        }
        public void LoadShippingTypeVN()
        {
            string html = "";
            var s = ShippingTypeVNController.GetAllWithIsHidden("", false);
            if (s.Count > 0)
            {
                foreach (var item in s)
                {
                    html += item.ID + ":" + item.ShippingTypeVNName + "|";
                }
            }
            hdfListShippingVN.Value = html;


        }
        public void LoadData()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int UID = obj_user.ID;

                string code = Request.QueryString["code"];
                int type = Request.QueryString["type"].ToInt(3);
                int status = Request.QueryString["stt"].ToInt(-1);
                string fd = Request.QueryString["fd"];
                string td = Request.QueryString["td"];

                if (Request.QueryString["stt"] != null)
                    ddlStatus.SelectedValue = status.ToString();
                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    rFD.SelectedDate = Convert.ToDateTime(fd);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    rTD.SelectedDate = Convert.ToDateTime(td);

                if (!string.IsNullOrEmpty(code))
                    txtOrderCode.Text = code;
                ddlType.SelectedValue = type.ToString();

                //var os = MainOrderController.GetAllByUIDNotHidden_SqlHelper(UID, status, fd, td);

                if (!string.IsNullOrEmpty(Request.QueryString["fd"]))
                {
                    rFD.SelectedDate = Convert.ToDateTime(fd);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["td"]))
                    rTD.SelectedDate = Convert.ToDateTime(td);
                var os = TransportationOrderNewController.GetAllByUIDWithFilter_SqlHelper(UID, code, type, status, fd, td);
                List<tbl_TransportationOrderNew> oAll = new List<tbl_TransportationOrderNew>();
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
                }
                if (oAll.Count > 0)
                {
                    foreach (var item in oAll)
                    {
                        ltr.Text += "<tr data-id=\"" + item.ID + "\">";
                        ltr.Text += "<td style=\"text-align: center\">";
                        if (item.Status == 4)
                        {
                            ltr.Text += "    <input type=\"checkbox\" onchange=\"selectdeposit()\" class=\"form-control chk-deposit\" data-id=\"" + item.ID + "\">";
                        }
                        ltr.Text += "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + item.ID + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + item.BarCode + "</td>";
                        double weight = 0;
                        if (item.SmallPackageID != null)
                        {
                            if (item.SmallPackageID > 0)
                            {
                                var pack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
                                if (pack != null)
                                {
                                    if (pack.Weight.ToString().ToFloat(0) > 0)
                                        weight = Convert.ToDouble(pack.Weight);
                                }
                            }
                        }
                        ltr.Text += "<td style=\"text-align: center\">" + weight + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + item.Note + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.AdditionFeeVND)) + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.SensorFeeeVND)) + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>";
                        string ngayycxk = "";
                        string ngayxk = "";
                        if (item.DateExportRequest != null)
                        {
                            ngayycxk = string.Format("{0:dd/MM/yyyy}", item.DateExportRequest);
                        }
                        if (item.DateExport != null)
                        {
                            ngayxk = string.Format("{0:dd/MM/yyyy}", item.DateExport);
                        }
                        ltr.Text += "<td style=\"text-align: center\">" + ngayycxk + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + ngayxk + "</td>";
                        string shippintType = "";
                        if (item.ShippingTypeVN != null)
                        {
                            var sht = ShippingTypeVNController.GetByID(item.ShippingTypeVN.ToString().ToInt(0));
                            if (sht != null)
                            {
                                shippintType = sht.ShippingTypeVNName;
                            }
                        }
                        ltr.Text += "<td style=\"text-align: center\">" + shippintType + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + item.ExportRequestNote + "</td>";
                        ltr.Text += "<td style=\"text-align: center\">" + PJUtils.GeneralTransportationOrderNewStatus(Convert.ToInt32(item.Status)) + "</td>";


                        ltr.Text += "<td style=\"text-align: center\">";
                        if (item.Status == 1)
                        {
                            ltr.Text += "     <a href=\"javascript:;\" onclick=\"rejectOrder($(this))\" class=\"bg-black\" style=\"float:left;width:100%;margin-bottom:5px;\">Hủy đơn</a>";
                        }
                        //ltr.Text += "     <a href=\"javascript:;\"  class=\"btn btn-success btn-block pill-btn primary-btn main-btn hover\" onclick=\"viewdetail($(this))\">Chi tiết</a>";
                        ltr.Text += "  </td>";
                        ltr.Text += "</tr>";
                    }
                }
            }
        }
        #region Paging
        public void pagingall(List<MainOrderController.mainorder> acs)
        {
            int PageSize = 40;
            if (acs.Count > 0)
            {
                int TotalItems = acs.Count;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    int status = Convert.ToInt32(item.Status);
                    ltr.Text += "<tr>";
                    ltr.Text += "<td style=\"text-align: center\">";
                    if (item.OrderType == 3)
                    {
                        if (item.IsCheckNotiPrice == true)
                        {

                        }
                        else
                        {
                            if (item.Status == 0)
                            {
                                ltr.Text += "    <input type=\"checkbox\" onchange=\"selectdeposit()\" class=\"form-control chk-deposit\" data-id=\"" + item.ID + "\">";
                                //ltr.Text += "    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-orange\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>";
                            }
                        }
                    }
                    else
                    {
                        if (item.Status == 0)
                        {
                            ltr.Text += "    <input type=\"checkbox\" onchange=\"selectdeposit()\" class=\"form-control chk-deposit\" data-id=\"" + item.ID + "\">";
                        }
                    }
                    ltr.Text += "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.ID + "</td>";
                    //ltr.Text += "<td style=\"text-align: center\">" + item.STT + "</td>";
                    ltr.Text += "<td style=\"text-align: center\"><img src=\"" + item.anhsanpham + "\" alt=\"\" /></td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.ShopName + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.Site + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.AmountDeposit)) + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:N0}", Convert.ToDouble(item.Deposit)) + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</td>";
                    string ngaydatcoc = "";
                    if (item.DepositDate != null)
                    {
                        ngaydatcoc = string.Format("{0:dd/MM/yyyy}", item.DepositDate);
                    }
                    ltr.Text += "<td style=\"text-align: center\">" + ngaydatcoc + "</td>";

                    if (item.OrderType == 3)
                    {
                        if (item.IsCheckNotiPrice == true)
                        {
                            ltr.Text += "<td style=\"text-align: center;white-space: nowrap;\"><span class=\"bg-yellow-gold\">Chờ báo giá</span></td>";
                        }
                        else
                        {
                            ltr.Text += "<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>";
                        }
                    }
                    else
                    {
                        ltr.Text += "<td style=\"text-align: center;white-space: nowrap;\">" + PJUtils.IntToRequestAdmin(status) + "</td>";
                    }

                    //if (status < 9)
                    //    ltr.Text += "  <td style=\"text-align: center\"><input type=\"checkbox\" class=\"ycgh-chk\" data-id=\"" + item.ID + "\" data-status=\"" + status + "\"  disabled=\"disabled\"/></td>";
                    //else if (status == 9)
                    //{
                    //    if (item.IsGiaohang == true)
                    //    {
                    //        ltr.Text += "  <td style=\"text-align: center\"><input type=\"checkbox\" class=\"ycgh-chk\" data-id=\"" + item.ID + "\" checked disabled=\"disabled\" data-sta=\"1\" data-status=\"" + status + "\" /></td>";
                    //    }
                    //    else
                    //    {
                    //        ltr.Text += "  <td style=\"text-align: center\"><input type=\"checkbox\" class=\"ycgh-chk\" data-id=\"" + item.ID + "\" data-status=\"" + status + "\"  data-sta=\"0\" /></td>";
                    //    }
                    //}
                    //else if (status == 10)
                    //{
                    //    if (item.IsGiaohang == true)
                    //    {
                    //        ltr.Text += "  <td style=\"text-align: center\"><input type=\"checkbox\" class=\"ycgh-chk\" data-id=\"" + item.ID + "\" checked disabled=\"disabled\" data-status=\"" + status + "\" /></td>";
                    //    }
                    //    else
                    //    {
                    //        ltr.Text += "  <td style=\"text-align: center\"><input type=\"checkbox\" class=\"ycgh-chk\" data-id=\"" + item.ID + "\" disabled=\"disabled\" data-status=\"" + status + "\" /></td>";
                    //    }
                    //}

                    ltr.Text += "<td style=\"text-align: center\">";
                    if (item.OrderType == 3)
                    {
                        if (item.IsCheckNotiPrice == true)
                        {

                        }
                        else
                        {
                            if (item.Status == 0)
                            {
                                ltr.Text += "    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-orange\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>";
                            }
                        }
                    }
                    else
                    {
                        if (item.Status == 0)
                        {
                            ltr.Text += "    <a href=\"javascript:;\" onclick=\"depositOrder('" + item.ID + "')\" class=\"bg-orange\" style=\"float:left;width:100%;margin-bottom:5px;\">Đặt cọc</a><br/>";
                        }
                    }

                    ltr.Text += "     <a href=\"/chi-tiet-don-hang/" + item.ID + "\" class=\"viewmore-orderlist\">Chi tiết</a>";
                    //ltr.Text += "     <a href=\"/them-khieu-nai/" + item.ID + "\" class=\"viewmore-orderlist\">Khiếu nại</a>";
                    ltr.Text += "  </td>";
                    ltr.Text += "</tr>";
                }
            }
        }
        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));

        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<li class=\"pagerange\" ><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion
        public class Danhsachorder
        {
            //public tbl_MainOder morder { get; set; }
            public int ID { get; set; }
            public string ProductImage { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string TotalPriceVND { get; set; }
            public string AmountDeposit { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public string CreatedDate { get; set; }
            public string statusstring { get; set; }
            public string username { get; set; }
        }
        protected void btnSear_Click(object sender, EventArgs e)
        {
            string ordercode = txtOrderCode.Text;
            string type = ddlType.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string fd = "";
            string td = "";

            if (rFD.SelectedDate != null)
            {
                fd = rFD.SelectedDate.ToString();
            }
            if (rTD.SelectedDate != null)
            {
                td = rTD.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(fd) && !string.IsNullOrEmpty(td))
            {
                if (Convert.ToDateTime(fd) > Convert.ToDateTime(td))
                {
                    PJUtils.ShowMessageBoxSwAlert("Từ ngày phải trước đến ngày", "e", false, Page);
                }
                else
                {
                    Response.Redirect("/quan-ly-van-don-vch?stt=" + status + "&fd=" + fd + "&td=" + td + "&code=" + ordercode + "&type=" + type + "");
                }
            }
            else
            {
                Response.Redirect("/quan-ly-van-don-vch?stt=" + status + "&fd=" + fd + "&td=" + td + "&code=" + ordercode + "&type=" + type + "");
            }
            //LoadData();
        }
        #region webservice
        [WebMethod]
        public static string rejectOrder(int id, string cancelnote)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    int UID = acc.ID;
                    var t = TransportationOrderNewController.GetByIDAndUID(id, UID);
                    if (t != null)
                    {
                        TransportationOrderNewController.UpdateCancelOrder(id, 0, cancelnote, DateTime.Now, username);
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        [WebMethod]
        public static string exportAll()
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    bool isLocal = false;
                    if (acc.IsLocal != null)
                    {
                        isLocal = Convert.ToBoolean(acc.IsLocal);
                    }
                    int UID = acc.ID;
                    double wallet = Convert.ToDouble(acc.Wallet);

                    var ts = TransportationOrderNewController.GetByUIDAndStatus(UID, 4);
                    if (ts.Count > 0)
                    {
                        double feeOutStockCYN = 0;
                        double feeOutStockVND = 0;
                        double feeWeightOutStock = 0;

                        double totalWeight = 0;
                        double currency = 0;

                        double TotalAdditionFeeCYN = 0;
                        double TotalAdditionFeeVND = 0;

                        double TotalSensoredFeeCYN = 0;
                        double TotalSensoredFeeVND = 0;

                        double totalWeightPriceVND = 0;
                        double totalWeightPriceCYN = 0;

                        double totalPriceVND = 0;
                        double totalPriceCYN = 0;
                        double totalCount = ts.Count;

                        string listID = "";

                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                        {
                            currency = Convert.ToDouble(config.AgentCurrency);
                            feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                            feeOutStockVND = feeOutStockCYN * currency;
                            if (isLocal == true)
                                feeWeightOutStock = Convert.ToDouble(config.AccountLocalWeightPrice);
                            else
                                feeWeightOutStock = Convert.ToDouble(config.PriceCheckOutWarePerWeight);
                        }

                        foreach (var t in ts)
                        {
                            listID += t.ID + "|";
                            double weight = 0;

                            if (t.SmallPackageID != null)
                            {
                                if (t.SmallPackageID > 0)
                                {
                                    var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                    if (package != null)
                                    {
                                        if (package.Weight != null)
                                        {
                                            if (package.Weight > 0)
                                                weight = Convert.ToDouble(package.Weight);
                                        }
                                    }
                                }
                            }

                            totalWeight += weight;
                            double addfeeVND = 0;
                            double addfeeCYN = 0;
                            double sensorVND = 0;
                            double sensorCYN = 0;

                            if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                if (t.AdditionFeeVND.ToFloat(0) > 0)
                                    addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                            if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                    addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                            if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                if (t.SensorFeeeVND.ToFloat(0) > 0)
                                    sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                            if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                if (t.SensorFeeCYN.ToFloat(0) > 0)
                                    sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                            TotalAdditionFeeCYN += addfeeCYN;
                            TotalAdditionFeeVND += addfeeVND;

                            TotalSensoredFeeVND += sensorVND;
                            TotalSensoredFeeCYN += sensorCYN;
                        }
                        totalWeightPriceCYN = totalWeight * feeWeightOutStock;
                        totalWeightPriceVND = totalWeightPriceCYN * currency;

                        totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                        totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                        string ret = "";
                        if (wallet >= totalPriceVND)
                        {
                            //ret = 1 + ":" + string.Format("{0:N0}", wallet) + ":" + totalCount + ":"
                            //    + string.Format("{0:N0}", totalWeight) + ":"
                            //    + string.Format("{0:N0}", totalWeightPriceCYN) + ":"
                            //    + string.Format("{0:N0}", totalWeightPriceVND) + ":"
                            //    + feeOutStockCYN + ":" + feeOutStockVND + ":"
                            //    + string.Format("{0:N0}", totalPriceCYN)
                            //    + string.Format("{0:N0}", totalPriceVND)
                            //    + listID;
                            ret = 1 + ":" + wallet + ":" + totalCount + ":"
                                + totalWeight + ":"
                                + totalWeightPriceCYN + ":"
                                + totalWeightPriceVND + ":"
                                + feeOutStockCYN + ":" + feeOutStockVND + ":"
                                + totalPriceCYN + ":"
                                + totalPriceVND + ":"
                                + listID + ":"
                                + "0" + ":"
                                + TotalAdditionFeeCYN + ":"
                                + TotalAdditionFeeVND + ":";

                        }
                        else
                        {
                            double leftmoney = totalPriceVND - wallet;
                            if (leftmoney > 0)
                            {
                                //ret = 0 + ":" + string.Format("{0:N0}", wallet) + ":" + totalCount + ":"
                                //+ string.Format("{0:N0}", totalWeight) + ":"
                                //+ string.Format("{0:N0}", totalWeightPriceCYN) + ":"
                                //+ string.Format("{0:N0}", totalWeightPriceVND) + ":"
                                //+ feeOutStockCYN + ":" + feeOutStockVND + ":"
                                //+ string.Format("{0:N0}", totalPriceCYN)
                                //+ string.Format("{0:N0}", totalPriceVND)
                                //+ listID
                                //+ string.Format("{0:N0}", leftmoney);
                                ret = 0 + ":" + wallet + ":" + totalCount + ":"
                                + totalWeight + ":"
                                + totalWeightPriceCYN + ":"
                                + totalWeightPriceVND + ":"
                                + feeOutStockCYN + ":" + feeOutStockVND + ":"
                                + totalPriceCYN + ":"
                                + totalPriceVND + ":"
                                + listID + ":"
                                + leftmoney + ":"
                                + TotalAdditionFeeCYN + ":"
                                + TotalAdditionFeeVND + ":"; ;
                            }
                        }

                        return ret;
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        [WebMethod]
        public static string exportSelectedAll(string listID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    bool isLocal = false;
                    if (acc.IsLocal != null)
                    {
                        isLocal = Convert.ToBoolean(acc.IsLocal);
                    }
                    int UID = acc.ID;
                    double wallet = Convert.ToDouble(acc.Wallet);
                    double feeOutStockCYN = 0;
                    double feeOutStockVND = 0;
                    double feeWeightOutStock = 0;

                    double totalWeight = 0;
                    double currency = 0;

                    double TotalAdditionFeeCYN = 0;
                    double TotalAdditionFeeVND = 0;

                    double TotalSensoredFeeCYN = 0;
                    double TotalSensoredFeeVND = 0;

                    double totalWeightPriceVND = 0;
                    double totalWeightPriceCYN = 0;

                    double totalPriceVND = 0;
                    double totalPriceCYN = 0;
                    double totalCount = 0;


                    var config = ConfigurationController.GetByTop1();
                    if (config != null)
                    {
                        currency = Convert.ToDouble(config.AgentCurrency);
                        feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                        feeOutStockVND = feeOutStockCYN * currency;
                        if (isLocal == true)
                            feeWeightOutStock = Convert.ToDouble(config.AccountLocalWeightPrice);
                        else
                            feeWeightOutStock = Convert.ToDouble(config.PriceCheckOutWarePerWeight);
                    }
                    string[] IDs = listID.Split('|');
                    if (IDs.Length - 1 > 0)
                    {
                        for (int i = 0; i < IDs.Length - 1; i++)
                        {
                            int ID = IDs[i].ToInt(0);
                            var t = TransportationOrderNewController.GetByID(ID);
                            if (t != null)
                            {
                                totalCount++;
                                double weight = 0;
                                if (t.SmallPackageID != null)
                                {
                                    if (t.SmallPackageID > 0)
                                    {
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                        if (package != null)
                                        {
                                            if (package.Weight != null)
                                            {
                                                if (package.Weight > 0)
                                                    weight = Convert.ToDouble(package.Weight);
                                            }
                                        }
                                    }
                                }

                                totalWeight += weight;
                                double addfeeVND = 0;
                                double addfeeCYN = 0;
                                double sensorVND = 0;
                                double sensorCYN = 0;

                                if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                    if (t.AdditionFeeVND.ToFloat(0) > 0)
                                        addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                                if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                    if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                        addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                                if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                    if (t.SensorFeeeVND.ToFloat(0) > 0)
                                        sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                                if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                    if (t.SensorFeeCYN.ToFloat(0) > 0)
                                        sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                                TotalAdditionFeeCYN += addfeeCYN;
                                TotalAdditionFeeVND += addfeeVND;

                                TotalSensoredFeeVND += sensorVND;
                                TotalSensoredFeeCYN += sensorCYN;
                            }
                        }
                    }

                    totalWeightPriceCYN = totalWeight * feeWeightOutStock;
                    totalWeightPriceVND = totalWeightPriceCYN * currency;

                    totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                    totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalSensoredFeeCYN;

                    string ret = "";
                    if (wallet >= totalPriceVND)
                    {
                        //ret = 1 + ":" + string.Format("{0:N0}", wallet) + ":" + totalCount + ":"
                        //    + string.Format("{0:N0}", totalWeight) + ":"
                        //    + string.Format("{0:N0}", totalWeightPriceCYN) + ":"
                        //    + string.Format("{0:N0}", totalWeightPriceVND) + ":"
                        //    + feeOutStockCYN + ":" + feeOutStockVND + ":"
                        //    + string.Format("{0:N0}", totalPriceCYN)
                        //    + string.Format("{0:N0}", totalPriceVND)
                        //    + listID;
                        ret = 1 + ":" + wallet + ":" + totalCount + ":"
                            + totalWeight + ":"
                            + totalWeightPriceCYN + ":"
                            + totalWeightPriceVND + ":"
                            + feeOutStockCYN + ":" + feeOutStockVND + ":"
                            + totalPriceCYN + ":"
                            + totalPriceVND + ":"
                            + listID;
                    }
                    else
                    {
                        double leftmoney = totalPriceVND - wallet;
                        if (leftmoney > 0)
                        {
                            //ret = 0 + ":" + string.Format("{0:N0}", wallet) + ":" + totalCount + ":"
                            //+ string.Format("{0:N0}", totalWeight) + ":"
                            //+ string.Format("{0:N0}", totalWeightPriceCYN) + ":"
                            //+ string.Format("{0:N0}", totalWeightPriceVND) + ":"
                            //+ feeOutStockCYN + ":" + feeOutStockVND + ":"
                            //+ string.Format("{0:N0}", totalPriceCYN)
                            //+ string.Format("{0:N0}", totalPriceVND)
                            //+ listID
                            //+ string.Format("{0:N0}", leftmoney);
                            ret = 0 + ":" + wallet + ":" + totalCount + ":"
                            + totalWeight + ":"
                            + totalWeightPriceCYN + ":"
                            + totalWeightPriceVND + ":"
                            + feeOutStockCYN + ":" + feeOutStockVND + ":"
                            + totalPriceCYN + ":"
                            + totalPriceVND + ":"
                            + listID + ":"
                            + leftmoney;
                        }
                    }

                    return ret;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        #endregion
        protected void btnPayExport_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                bool isLocal = false;
                if (acc.IsLocal != null)
                {
                    isLocal = Convert.ToBoolean(acc.IsLocal);
                }
                int UID = acc.ID;
                DateTime currentDate = DateTime.Now;
                double wallet = Convert.ToDouble(acc.Wallet);
                string strListID = hdfListID.Value;
                if (!string.IsNullOrEmpty(strListID))
                {
                    string[] listID = strListID.Split('|');
                    if (listID.Length - 1 > 0)
                    {
                        double feeOutStockCYN = 0;
                        double feeOutStockVND = 0;
                        double feeWeightOutStock = 0;

                        double totalWeight = 0;
                        double currency = 0;

                        double TotalAdditionFeeCYN = 0;
                        double TotalAdditionFeeVND = 0;

                        double TotalSensoredFeeCYN = 0;
                        double TotalSensoredFeeVND = 0;

                        double totalWeightPriceVND = 0;
                        double totalWeightPriceCYN = 0;

                        double totalPriceVND = 0;
                        double totalPriceCYN = 0;

                        var config = ConfigurationController.GetByTop1();
                        if (config != null)
                        {
                            currency = Convert.ToDouble(config.AgentCurrency);
                            feeOutStockCYN = Convert.ToDouble(config.PriceCheckOutWareDefault);
                            feeOutStockVND = feeOutStockCYN * currency;
                            if (isLocal == true)
                                feeWeightOutStock = Convert.ToDouble(config.AccountLocalWeightPrice);
                            else
                                feeWeightOutStock = Convert.ToDouble(config.PriceCheckOutWarePerWeight);
                        }

                        for (int i = 0; i < listID.Length - 1; i++)
                        {
                            int ID = listID[i].ToInt(0);
                            var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                            if (t != null)
                            {
                                double weight = 0;
                                if (t.SmallPackageID != null)
                                {
                                    if (t.SmallPackageID > 0)
                                    {
                                        var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                        if (package != null)
                                        {
                                            if (package.Weight != null)
                                            {
                                                if (package.Weight > 0)
                                                    weight = Convert.ToDouble(package.Weight);
                                            }
                                        }
                                    }
                                }
                                totalWeight += weight;
                                double addfeeVND = 0;
                                double addfeeCYN = 0;
                                double sensorVND = 0;
                                double sensorCYN = 0;

                                if (!string.IsNullOrEmpty(t.AdditionFeeVND))
                                    if (t.AdditionFeeVND.ToFloat(0) > 0)
                                        addfeeVND = Convert.ToDouble(t.AdditionFeeVND);

                                if (!string.IsNullOrEmpty(t.AdditionFeeCYN))
                                    if (t.AdditionFeeCYN.ToFloat(0) > 0)
                                        addfeeCYN = Convert.ToDouble(t.AdditionFeeCYN);

                                if (!string.IsNullOrEmpty(t.SensorFeeeVND))
                                    if (t.SensorFeeeVND.ToFloat(0) > 0)
                                        sensorVND = Convert.ToDouble(t.SensorFeeeVND);

                                if (!string.IsNullOrEmpty(t.SensorFeeCYN))
                                    if (t.SensorFeeCYN.ToFloat(0) > 0)
                                        sensorCYN = Convert.ToDouble(t.SensorFeeCYN);

                                TotalAdditionFeeCYN += addfeeCYN;
                                TotalAdditionFeeVND += addfeeVND;

                                TotalSensoredFeeVND += sensorVND;
                                TotalSensoredFeeCYN += sensorCYN;
                            }
                        }
                        totalWeightPriceCYN = totalWeight * feeWeightOutStock;
                        totalWeightPriceVND = totalWeightPriceCYN * currency;

                        totalPriceVND = totalWeightPriceVND + feeOutStockVND + TotalSensoredFeeVND + TotalAdditionFeeVND;
                        totalPriceCYN = totalWeightPriceCYN + feeOutStockCYN + TotalSensoredFeeCYN + TotalAdditionFeeCYN;

                        if (wallet >= totalPriceVND)
                        {
                            //Lưu xuống 1 lượt yêu cầu xuất kho
                            #region Tạo lượt xuất kho
                            string note = hdfNote.Value;
                            int shippingtype = hdfShippingType.Value.ToInt(0);

                            string kq = ExportRequestTurnController.InsertWithUID(UID, username, 0, currentDate, totalPriceVND,
                                totalPriceCYN, totalWeight, note, shippingtype, currentDate, username);

                            int eID = kq.ToInt(0);
                            for (int i = 0; i < listID.Length - 1; i++)
                            {
                                int ID = listID[i].ToInt(0);
                                var t = TransportationOrderNewController.GetByIDAndUID(ID, acc.ID);
                                if (t != null)
                                {
                                    double weight = 0;
                                    if (t.SmallPackageID != null)
                                    {
                                        if (t.SmallPackageID > 0)
                                        {
                                            var package = SmallPackageController.GetByID(Convert.ToInt32(t.SmallPackageID));
                                            if (package != null)
                                            {
                                                if (package.Status == 3)
                                                {
                                                    var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                                                    if (check == null)
                                                    {
                                                        RequestOutStockController.InsertT(package.ID,
                                                            package.OrderTransactionCode,
                                                            t.ID,
                                                            Convert.ToInt32(package.OrderShopCodeID), 1,
                                                            DateTime.Now, username, eID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Update lại trạng thái từng đơn và ngày ship
                                    TransportationOrderNewController.UpdateRequestOutStock(t.ID, 5, note, currentDate, shippingtype);
                                }
                            }
                            #endregion
                            //Trừ tiền 
                            #region Trừ tiền xuất kho
                            double walletLeft = wallet - totalPriceVND;
                            AccountController.updateWallet(UID, walletLeft, currentDate, username);
                            HistoryPayWalletController.Insert(UID, username, 0, totalPriceVND,
                                username + " đã thanh toán đơn hàng vận chuyển hộ.", walletLeft, 1, 8, currentDate, username);
                            #endregion
                            #region Gửi thông báo
                            var admins = AccountController.GetAllByRoleID(0);
                            if (admins.Count > 0)
                            {
                                foreach (var admin in admins)
                                {
                                    NotificationController.Inser(UID, username, admin.ID,
                                                                       admin.Username, 0,
                                                                       "Có yêu cầu xuất kho của user: " + username, 0,
                                                                       10, currentDate, username);
                                }
                            }

                            var managers = AccountController.GetAllByRoleID(2);
                            if (managers.Count > 0)
                            {
                                foreach (var manager in managers)
                                {
                                    NotificationController.Inser(UID, username, manager.ID,
                                                                       manager.Username, 0,
                                                                       "Có yêu cầu xuất kho của user: " + username, 0,
                                                                       10, currentDate, username);
                                }
                            }
                            var khoVNs = AccountController.GetAllByRoleID(5);
                            if (khoVNs.Count > 0)
                            {
                                foreach (var khoVN in khoVNs)
                                {
                                    NotificationController.Inser(UID, username, khoVN.ID,
                                                                       khoVN.Username, 0,
                                                                       "Có yêu cầu xuất kho của user: " + username, 0,
                                                                       10, currentDate, username);
                                }
                            }
                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hubContext.Clients.All.addNewMessageToPage("", "");
                            #endregion
                            PJUtils.ShowMessageBoxSwAlert("Bạn đã gửi yêu cầu xuất kho thành công. Xin chân thành cảm ơn", "s", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Số tiền của bạn không đủ để thanh toán phiên xuất kho, vui lòng thử lại sau.", "e", true, Page);
                        }
                    }
                }
            }
        }
    }
}