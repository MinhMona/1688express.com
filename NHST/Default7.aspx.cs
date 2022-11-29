using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                Response.Write(Request.QueryString["listService"]);
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
                string timework = confi.TimeWork;
                string currency = string.Format("{0:N0}", Convert.ToDouble(confi.Currency));

                ltrCurrency.Text = currency;
                ltrEmail.Text = email;
                ltrTimeWork.Text = timework;
                ltrHotline.Text = hotline;

                ltrEmail1.Text = email;
                ltrHotline1.Text = hotline;
                ltrTimework1.Text = timework;
            }
            #region Dịch vụ
            var service = PageTypeController.GetByID(6);
            if (service != null)
            {
                ltrService.Text = service.PageTypeDescription;
                var pageService = PageController.GetByPagetTypeID(6).Take(6).ToList();
                if (pageService.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    foreach (var item in pageService)
                    {
                        html.Append("<article class=\"bus__child item\">");
                        html.Append("<div class=\"img\"><a href=\"" + item.NodeAliasPath + "\"><img src=\"" + item.IMG + "\" alt=\"business img\"></a></div>");
                        html.Append("<div class=\"content\">");
                        html.Append("<h1 class=\"fz-18\"><a href=\"" + item.NodeAliasPath + "\">" + item.Title + "</a></h1>");
                        html.Append("<p class=\"fz-14\">" + PJUtils.SubString(item.Summary, 100) + "</p>");
                        html.Append("</div>");
                        html.Append("</article>");
                    }
                    ltrServiceList.Text = html.ToString();
                }
            }
            #endregion
            #region Quyền lợi khách hàng
            var qlkh = PageTypeController.GetByID(5);
            if (qlkh != null)
            {
                var pageCus = PageController.GetByPagetTypeID(5);
                if (pageCus.Count > 0)
                {
                    pageCus = pageCus.Take(3).ToList();
                    StringBuilder html1 = new StringBuilder();
                    foreach (var item in pageCus)
                    {
                        html1.Append("<article class=\"col__child\">");
                        html1.Append("<div class=\"img\"><a href=\"" + item.NodeAliasPath + "\"><img src=\"" + item.IMG + "\" alt=\"" + item.Title + "\"></a></div>");
                        html1.Append("<section class=\"content\">");
                        html1.Append("<h4 class=\"fz-18\"><a href=\"" + item.NodeAliasPath + "\">" + item.Title + "</a></h4>");
                        html1.Append("<p class=\"fz-14\">" + item.Summary + "</p>");
                        html1.Append("<a href=\"" + item.NodeAliasPath + "\" class=\"link\">Xem thêm <i class=\"fas fa-arrow-alt-circle-right\"></i></a>");
                        html1.Append("</section>");
                        html1.Append("</article>");
                    }
                    ltrQLKH.Text = html1.ToString();
                }
            }
            #endregion
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] != null)
            {
                string search = txtSearch.Text;
                if (!string.IsNullOrEmpty(search))
                {
                    Session["linksearch"] = search;
                    Response.Redirect("/gio-hang");
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập link sản phẩm", "e", true, Page);
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Vui lòng đăng nhập", "e", true, Page);
            }
        }
        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopup;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n); 
                }
                else
                    return "null";
            }
            else
                return null;

        }
        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }
    }
}