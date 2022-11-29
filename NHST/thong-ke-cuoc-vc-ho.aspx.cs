using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class thong_ke_cuoc_vc_ho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }

        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                List<ReportTrans> rs = new List<ReportTrans>();
                var r = ExportRequestTurnController.GetByCreatedByAndVCH(username);
                if (r.Count > 0)
                {
                    foreach (var item in r)
                    {
                        ReportTrans t = new ReportTrans();
                        t.ID = item.ID;
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
                        t.TotalWeight = Math.Round(Convert.ToDouble(item.TotalWeight), 1).ToString();
                        t.TotalPrice = string.Format("{0:N0}", item.TotalPriceVND) + " VNĐ - Quy đổi: " + Math.Round(Convert.ToDouble(item.TotalPriceCYN), 2) + " tệ.";
                        string htvc = "";
                        var h = ShippingTypeVNController.GetByID(Convert.ToInt32(item.ShippingTypeInVNID));
                        if (h != null)
                        {
                            htvc = h.ShippingTypeVNName;
                        }
                        t.HTVC = htvc;
                        t.StaffNote = item.StaffNote;
                        rs.Add(t);
                    }
                }
                pagingall(rs);
            }
        }
        #region Paging
        public void pagingall(List<ReportTrans> acs)
        {
            int PageSize = 20;
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
                    ltr.Text += "<tr>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.ID + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.DateRequest + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.DateOutWH + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalPackages + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalWeight + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.TotalPrice + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.HTVC + "</td>";
                    ltr.Text += "<td style=\"text-align: center\">" + item.StaffNote + "</td>";
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
        public class ReportTrans
        {
            public int ID { get; set; }
            public string DateRequest { get; set; }
            public string DateOutWH { get; set; }
            public string TotalPackages { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPrice { get; set; }
            public string HTVC { get; set; }
            public string StaffNote { get; set; }
        }
    }
}