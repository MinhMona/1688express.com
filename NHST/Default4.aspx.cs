using MB.Extensions;
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
    public partial class Default3 : System.Web.UI.Page
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
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Điện thoại:</span><span class=\"txt\"><a href=\"tel:" + config.Hotline + "\">" + config.Hotline + "</a></span>";
                ltrContact.Text += "</li>";
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Email:</span><span class=\"txt\"><a href=\"mailto:" + config.EmailSupport + "\">" + config.EmailSupport + "</a></span>";
                ltrContact.Text += "</li>";
                ltrContact.Text += "<li>";
                ltrContact.Text += "<span class=\"lbl\">Địa chỉ:</span><span class=\"txt\"><a href=\"tel:" + config.Address + "\">" + config.Address + "</a></span>";
                ltrContact.Text += "</li>";
            }
            var ps = ProductController.GetIsHot(true, false);
            if (ps.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                foreach (var p in ps)
                {
                    html.Append("<li class=\"it col\">");
                    html.Append("   <div class=\"inner\">");
                    html.Append("       <aside class=\"tmb\"><a href=\"" + p.WebLink + "\" target=\"_blank\"><img src=\"" + p.ProductIMG + "\" alt=\"\" /></a></aside>");
                    html.Append("       <aside class=\"cont\">");
                    html.Append("           <h4 class=\"web\">" + p.WebName + "</h4>");
                    html.Append("           <h5 class=\"tit\"><a href=\"" + p.WebLink + "\" target=\"_blank\">" + p.Productname + "</a></h5>");
                    html.Append("       </aside>");
                    html.Append("   </div>");
                    html.Append("</li>");
                }
                ltrProductHot.Text = html.ToString();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kq = ContactController.Insert(txtFullname.Text, txtEmail.Text, txtPhone.Text, txtContent.Text, false, DateTime.Now, txtFullname.Text);
            if (kq.ToInt(0) > 0)
                PJUtils.ShowMessageBoxSwAlert("Gửi liên hệ thành công", "s", true, Page);
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
        [WebMethod]
        public static string UpdateNotification(int ID, string UserName)
        {
            string ret = NotificationController.UpdateStatus(ID, 1, DateTime.Now, UserName);
            return ret;
        }
        //protected void btnCart_Click(object sender, EventArgs e)
        //{
        //    string link = txtLink.Text.Trim();
        //    Session["linksearch"] = link;
        //    Response.Redirect("/gio-hang");
        //}
    }
}