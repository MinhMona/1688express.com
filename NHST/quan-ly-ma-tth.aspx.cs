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
using NHST.Controllers;
using NHST.Models;
using static NHST.Controllers.PayhelpController;

namespace NHST
{
    public partial class quan_ly_ma_tth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "nhutsg8844";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                string textsearch = "";
                int type = 1;
                int status = 0;
                string fd = "";
                string td = "";

                if (Request.QueryString["s"] != null)
                {
                    textsearch = Request.QueryString["s"];
                }
                if (Request.QueryString["t"] != null)
                {
                    type = Request.QueryString["t"].ToInt(1);
                }
                if (Request.QueryString["stt"] != null)
                {
                    status = Request.QueryString["stt"].ToInt(0);
                }
                if (Request.QueryString["fd"] != null)
                {
                    fd = Request.QueryString["fd"];
                }
                if (Request.QueryString["td"] != null)
                {
                    td = Request.QueryString["td"];
                }
                txtOrderCode.Text = textsearch;
                ddlStatus.SelectedValue = status.ToString();
                ddlType.SelectedValue = type.ToString();
                if (!string.IsNullOrEmpty(fd))
                {
                    try
                    {
                        rFD.SelectedDate = DateTime.ParseExact(fd, "dd/MM/yyyy", null);
                    }
                    catch
                    {

                    }
                }
                if (!string.IsNullOrEmpty(td))
                {
                    try
                    {
                        rTD.SelectedDate = DateTime.ParseExact(td, "dd/MM/yyyy", null);
                    }
                    catch
                    {

                    }
                }


                var listpa = PayhelpController.GetAllByUIDWithSearchTextStatusFromDateToDate_SqlHelper1(u.ID,
                    textsearch, type, status, fd, td);
                if (listpa.Count > 0)
                {
                    pagingall(listpa);
                }
            }
        }
        #region Paging
        public void pagingall(List<payhelpCustom> acs)
        {
            int PageSize = 15;
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

                int UID = Convert.ToInt32(ViewState["UID"]);
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];
                    string tdstatus = "Đã duyệt";
                    int status = Convert.ToInt32(item.Status);
                    if (status == 0)
                        tdstatus = "Chưa thanh toán";
                    else if (status == 1)
                        tdstatus = "Đã thanh toán";
                    else
                        tdstatus = "Đã hủy";

                    ltr.Text += "<tr>";
                    ltr.Text += "   <td>" + item.ID + "</td>";
                    ltr.Text += "   <td>" + string.Format("{0:dd/MM/yyyy}", item.DateSend) + "</td>";
                    ltr.Text += "   <td>" + item.TotalPriceCYN + "</td>";
                    if(item.Status == 4)
                        ltr.Text += "   <td>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)).Replace(",", ".") + "</td>";
                    else
                        ltr.Text += "   <td></td>";
                    ltr.Text += "   <td>" + item.Note + "</td>";
                    ltr.Text += "   <td>" + item.StaffNote + "</td>";
                    ltr.Text += "   <td>" + item.strStatus + "</td>";                    
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
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.Append("<a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a>");
                output.Append("<a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><</a>");
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Previous</a></li>");
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
                //output.Append("<li class=\"pagerange\"><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {

                    output.Append("<span class=\"current\">" + i.ToString() + "</span>");
                    //output.Append("<li class=\"current-page-item\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                    //output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a>");
                output.Append("<a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">></a>");
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a></li>");
                output.Append("<a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        [WebMethod]
        public static string getCurrency(string totalprice)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int level = Convert.ToInt32(u.LevelID);
                double pc_config = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    pc_config = Convert.ToDouble(config.PricePayHelpDefault);
                }

                double amount = Convert.ToDouble(totalprice);
                if (amount > 0)
                {
                    var pricechange = PriceChangeController.GetByPriceFT(amount);
                    double pc = 0;
                    if (pricechange != null)
                    {
                        if (level == 1)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip0);
                        }
                        else if (level == 2)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip1);
                        }
                        else if (level == 3)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip2);
                        }
                        else if (level == 4)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip3);
                        }
                        else if (level == 5)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip4);
                        }
                        else if (level == 6)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip5);
                        }
                        else if (level == 7)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip6);
                        }
                        else if (level == 8)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip7);
                        }
                        else if (level == 9)
                        {
                            pc = pc_config + Convert.ToDouble(pricechange.Vip8);
                        }
                        return pc.ToString();
                    }
                }
            }
            return "0";
        }

        protected void btnSear_Click(object sender, EventArgs e)
        {
            string textsearch = txtOrderCode.Text.Trim();
            int type = ddlType.SelectedValue.ToInt(1);
            int status = ddlStatus.SelectedValue.ToInt(0);
            string fd = rFD.SelectedDate.ToString();
            string td = rTD.SelectedDate.ToString();
            Response.Redirect("/quan-ly-ma-tth?s=" + textsearch + "&t=" + type + "&stt=" + status + "&fd=" + fd + "&td=" + td + "");
        }
    }
}