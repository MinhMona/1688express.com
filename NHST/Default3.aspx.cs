using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "dinhvan";
                //Session["userLoginSystem"] = "DatHangTest";
                CheckUser();
                LoadService();
                LoadData();
                LoadSEO();
                
            }
        }
        public void LoadData()
        {
            var Partners = PartnersController.GetAll("");
            if (Partners.Count > 0)
            {
                foreach (var p in Partners)
                {
                    ltrPartner.Text += "<div class=\"item\"><img src=\"" + p.PartnerIMG + "\" alt=\"\"></div>";
                }
            }
            var c = ConfigurationController.GetByTop1();
            if (c != null)
            {
                ltrAbout.Text = c.AboutText;
                ltrExtension.Text += "<li style=\"width: 50%\">";
                ltrExtension.Text += "<a href=\"" + c.ChromeExtensionLink + "\" target=\"_blank\">";
                ltrExtension.Text += "<img src=\"/App_Themes/pdv/assets/images/setting1.png\" alt=\"#\"></a>";
                ltrExtension.Text += "<a href=\"" + c.ChromeExtensionLink + "\" target=\"_blank\" style=\"float: left; clear: both; width: 100%; text-align: center\">Chrome</a>";
                ltrExtension.Text += "</li>";
                ltrExtension.Text += "<li style=\"width: 50%\">";
                ltrExtension.Text += "<a href=\"" + c.CocCocExtensionLink + "\" target=\"_blank\">";
                ltrExtension.Text += "<img src=\"/App_Themes/pdv/assets/images/setting2.png\" alt=\"#\"></a>";
                ltrExtension.Text += "<a href=\"" + c.CocCocExtensionLink + "\" target=\"_blank\" style=\"float: left; clear: both; width: 100%; text-align: center\">cococ</a>";
                ltrExtension.Text += "</li>";

                ltrBanner.Text = "<img src=\"" + c.BannerIMG + "\" alt=\"\" />";
                ltrAddon.Text += "<div class=\"banner-add-on\">";
                ltrAddon.Text += "<label>ADD ON</label>";
                ltrAddon.Text += "<a href=\"" + c.ChromeExtensionLink + "\" class=\"addon-bg-chrome\" target=\"_blank\"></a>";
                ltrAddon.Text += "<a href=\"" + c.CocCocExtensionLink + "\" class=\"addon-bg-cococ\" target=\"_blank\"></a>";
                ltrAddon.Text += "</div>";
            }
            var webchina = WebChinaController.GetAllTop(3);
            if (webchina.Count > 0)
            {
                foreach (var w in webchina)
                {
                    ltrWebChina.Text += "<li>";
                    ltrWebChina.Text += "<a href=\"" + w.WebLink + "\" target=\"_blank\"><img src=\"" + w.Logo + "\" alt=\"\"></a>";
                    ltrWebChina.Text += "<h3><a href=\"" + w.WebLink + "\" target=\"_blank\">" + w.WebName + "</a></h3>";
                    ltrWebChina.Text += "<p>" + w.WebDescript + "</p>";
                    ltrWebChina.Text += "</li>";
                }
            }
            var quytrinh = StepController.GetAllUser();
            if (quytrinh.Count > 0)
            {
                int i = 1;
                foreach (var q in quytrinh)
                {

                    ltrQuytrinh.Text += "<li>";
                    ltrQuytrinh.Text += "<a href=\"" + q.StepLink + "\" target=\"_blank\"><img src=\"" + q.StepIMG + "\" alt=\"#\"></a>";
                    ltrQuytrinh.Text += "<p><a href=\"" + q.StepLink + "\" target=\"_blank\"><span class=\"step-number\">" + i + "</span> " + q.StepName + "</a></p>";
                    ltrQuytrinh.Text += "</li>";
                    i++;
                }
            }

            var camket = CommitmentController.GetAllUser();
            if (camket.Count > 0)
            {
                foreach (var ca in camket)
                {
                    ltrCamket.Text += "<div class=\"commit-list\">";
                    ltrCamket.Text += "<h3><a href=\"javascript:;\">" + ca.CommitmentName + "</a></h3>";
                    ltrCamket.Text += "<p>" + ca.CommitmentDescription + "</p>";
                    ltrCamket.Text += "</div>";
                }
            }
            var benefit = BenefitsController.GetAllUser();
            if (benefit.Count > 0)
            {
                var leftbenefit = benefit.Where(b => b.BenefitSide == 1).OrderBy(b => b.BenefitIndex).ToList();
                var rightbenefit = benefit.Where(b => b.BenefitSide == 2).OrderBy(b => b.BenefitIndex).ToList();
                foreach (var b in leftbenefit)
                {
                    ltrLeftBenefit.Text += "<div class=\"benefit-list\">";
                    ltrLeftBenefit.Text += "<div class=\"benefit-auto\">";
                    ltrLeftBenefit.Text += "<div class=\"benefit-img\"><img src=\"/App_Themes/pdv/assets/images/check.png\" alt=\"#\"></div>";
                    ltrLeftBenefit.Text += "<div class=\"benefit-info\">";
                    ltrLeftBenefit.Text += "<h3>" + b.BenefitName + "</h3>";
                    ltrLeftBenefit.Text += "<p>" + b.BenefitDescription + "</p>";
                    ltrLeftBenefit.Text += "</div>";
                    ltrLeftBenefit.Text += "</div>";
                    ltrLeftBenefit.Text += "</div>";
                }
                foreach (var b in rightbenefit)
                {
                    ltrRightBenefit.Text += "<div class=\"benefit-list\">";
                    ltrRightBenefit.Text += "<div class=\"benefit-auto\">";
                    ltrRightBenefit.Text += "<div class=\"benefit-img\"><img src=\"/App_Themes/pdv/assets/images/check.png\" alt=\"#\"></div>";
                    ltrRightBenefit.Text += "<div class=\"benefit-info\">";
                    ltrRightBenefit.Text += "<h3>" + b.BenefitName + "</h3>";
                    ltrRightBenefit.Text += "<p>" + b.BenefitDescription + "</p>";
                    ltrRightBenefit.Text += "</div>";
                    ltrRightBenefit.Text += "</div>";
                    ltrRightBenefit.Text += "</div>";
                }
            }
        }
        public void LoadSEO()
        {
            var home = PageSEOController.GetByID(1);
            string weblink = "https://vanchuyendaquocgia.com";
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (home != null)
            {
                HtmlHead objHeader = (HtmlHead)Page.Header;

                //we add meta description
                HtmlMeta objMetaFacebook = new HtmlMeta();

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "fb:app_id");
                objMetaFacebook.Content = "676758839172144";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:url");
                objMetaFacebook.Content = url;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:type");
                objMetaFacebook.Content = "website";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:title");
                objMetaFacebook.Content = home.ogtitle;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:description");
                objMetaFacebook.Content = home.ogdescription;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image");
                if (string.IsNullOrEmpty(home.ogimage))
                    objMetaFacebook.Content = weblink + "/App_Themes/vcdqg/images/main-logo.png";
                else
                    objMetaFacebook.Content = weblink + home.ogimage;
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:width");
                objMetaFacebook.Content = "200";
                objHeader.Controls.Add(objMetaFacebook);

                objMetaFacebook = new HtmlMeta();
                objMetaFacebook.Attributes.Add("property", "og:image:height");
                objMetaFacebook.Content = "500";
                objHeader.Controls.Add(objMetaFacebook);

                this.Title = home.metatitle;
                HtmlMeta meta = new HtmlMeta();
                meta = new HtmlMeta();
                meta.Attributes.Add("name", "description");
                meta.Content = home.metadescription;
                objHeader.Controls.Add(meta);

                meta = new HtmlMeta();
                meta.Attributes.Add("name", "keyword");
                meta.Content = home.metakeyword;
                objHeader.Controls.Add(meta);

            }
        }
        public void CheckUser()
        {
            if (Session["userLoginSystem"] != null)
            {
                hdfheha.Value = "1";
            }
        }
        public void LoadService()
        {
            var pages = PageController.GetByPagetTypeID(6).Take(9).ToList();
            if (pages.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                int PageTypeID = 6;
                string PageTypeName = "";
                var pagetype = PageTypeController.GetByID(6);
                if (pagetype != null)
                {
                    PageTypeName = pagetype.PageTypeName;
                }
                foreach (var p in pages)
                {

                    html.Append("<div class=\"sv-list-detail\">");
                    html.Append("<div class=\"sv-img\">");
                    html.Append("<a href=\"" + p.NodeAliasPath + "\">");
                    html.Append("<img src=\"" + PJUtils.GetIcon(p.IMG) + "\" alt=\"#\"></a>");
                    html.Append("<a class=\"more-link\" href=\"" + p.NodeAliasPath + "\">Xem tiếp <i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i></a>");
                    html.Append("</div>");
                    html.Append("<div class=\"sv-text\" style=\"padding:10px 15px;\">");
                    html.Append("<img src=\"/App_Themes/pdv/assets/images/span-yellow.png\" alt=\"#\">");
                    html.Append("<h3><a href=\"" + p.NodeAliasPath + "\">" + p.Title + "</a></h3>");
                    html.Append("<p>" + PJUtils.SubString(p.Summary, 200) + "</p>");
                    html.Append("</div>");
                    html.Append("</div>");
                }
                ltrService.Text = html.ToString();
            }
        }
        protected void btnsearchpro_Click(object sender, EventArgs e)
        {
            string text = txtTextSearch.Text.Trim();
            string a = PJUtils.TranslateText(text, "vi|zh");
            string page = hdfType.Value;
            SearchPage(page, PJUtils.RemoveHTMLTags(a));
        }
        #region Translate And Run
        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
                request.Method = "GET";
                var content = String.Empty;
                HttpStatusCode statusCode;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var contentType = response.ContentType;
                    Encoding encoding = null;
                    if (contentType != null)
                    {
                        var match = Regex.Match(contentType, @"(?<=charset\=).*");
                        if (match.Success)
                            encoding = Encoding.GetEncoding(match.ToString());
                    }

                    encoding = encoding ?? Encoding.UTF8;

                    statusCode = ((HttpWebResponse)response).StatusCode;
                    using (var reader = new StreamReader(stream, encoding))
                        content = reader.ReadToEnd();
                }
                var doc = Dcsoup.Parse(content);
                var scoreDiv = doc.Select("html").Select("span[id=result_box]").Html;
                return scoreDiv;
            }
            catch
            {
                return "";
            }
            
        }

        public void SearchPage(string page, string text)
        {
            string linkgo = "";
            if (page == "tmall")
            {
                string a = text;
                string textsearch_tmall = GetHashString(a);
                //string fullLinkSearch_tmall = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
                linkgo = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
            }
            else if (page == "taobao")
            {
                string a = text;
                string textsearch_taobao = GetHashString(a);
                //string fullLinkSearch_taobao = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                linkgo = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                //https://world.taobao.com/search/search.htm?q=%B9%AB%BC%A6&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1
            }
            else if (page == "1688")
            {
                string a = text;
                string textsearch_1688 = GetHashString(a);
                //string fullLinkSearch_1688 = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
                linkgo = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
            }
            Response.Redirect(linkgo);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "redirect('" + linkgo + "')", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "redirect('" + linkgo + "');", true);
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(inputString);
            return bytes;
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append("%" + b.ToString("X2"));

            return sb.ToString();
        }
        #endregion
        protected void btnCart_Click(object sender, EventArgs e)
        {
            string link = txtLink.Text.Trim();
            Session["linksearch"] = link;
            Response.Redirect("/gio-hang");
        }
    }
}