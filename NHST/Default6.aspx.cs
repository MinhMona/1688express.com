using NHST.Bussiness;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
                string hotlineSupport = confi.HotlineSupport;
                string hotlineFeedback = confi.HotlineFeedback;
                lblHotline.Text = hotline;
                lblHotlineFeedback.Text = hotlineFeedback;
                lblHotlineSupport.Text = hotlineSupport;
            }

            #region lấy sản phẩm
            StringBuilder html = new StringBuilder();
            var chinaweb = ChinaSiteController.GetAllWithIsHidden("", false).OrderBy(p=>p.ID).ToList();
            if (chinaweb.Count > 0)
            {
                foreach (var c in chinaweb)
                {
                   
                    var productcate = ProductCategoryController.GetAllWithIsHiddenAndChinaWebID(false, c.ID);
                    if (productcate.Count > 0)
                    {
                        html.Append("<section class=\"" + c.Sitename + " order\">");
                        html.Append("   <div class=\"all\">");
                        html.Append("       <section class=\"order-hd\">");
                        html.Append("           <div class=\"hd__title\"");
                        html.Append("               <h2 class=\"fz-30\">Đặt hàng từ <a href=\"javascript:;\">");
                        html.Append("                       <img src=\"" + c.SiteLogo + "\" alt=\"taobao\">");
                        html.Append("                   </a>");
                        html.Append("               </h2>");
                        html.Append("           </div>");
                        html.Append("       </section>");
                        html.Append("       <div class=\"order-col-4\">");
                        foreach (var pc in productcate)
                        {
                            
                            html.Append("           <article class=\"col__child\">");
                            html.Append("               <div class=\"title\">");
                            html.Append("                   <div class=\"img\"><a href=\"#\"><img src=\"" + pc.CategoryIMG + "\" alt=\"category img\"></a></div>");
                            html.Append("                   <h4 class=\"fz-18\">" + pc.CategoryName + "</h4>");
                            html.Append("               </div>");
                            var productlinks = ProductLinkController.GetAllWithIsHiddenWithCateID(false, pc.ID);
                            if (productlinks.Count > 0)
                            {
                                html.Append("               <ul class=\"list\">");
                                foreach (var p in productlinks)
                                {
                                    html.Append("                   <li class=\"item\"><a href=\"" + p.ProductLink + "\" target=\"_blank\">" + p.ProductName + "</a></li>");
                                }
                                html.Append("               </ul>");
                            }
                            html.Append("           </article>");
                            
                        }
                        html.Append("       </div>");
                        html.Append("   </div>");
                        html.Append("</section>");
                    }

                   
                }
                ltrOrderProduct.Text = html.ToString();
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
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Vui lòng đăng nhập", "e", true, Page);
            }
        }
    }
}