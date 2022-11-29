using MB.Extensions;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default4 : System.Web.UI.Page
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
                ltrContactFooter.Text += "<p class=\"item__title\">" + confi.Websitename + "</p>";
                ltrContactFooter.Text += "<div class=\"item\">";
                ltrContactFooter.Text += "<p class=\"contact-method__sub\">Email contact</p>";
                ltrContactFooter.Text += "<a class=\"contact-method__main\" href=\"mailto:" + email + "\">" + email + "</a>";
                ltrContactFooter.Text += "</div>";
                ltrContactFooter.Text += "<div class=\"item\">";
                ltrContactFooter.Text += "<p class=\"contact-method__sub\">Giờ hoạt động</p>";
                ltrContactFooter.Text += "<a class=\"contact-method__main\" href=\"\">" + confi.TimeWork + "</a>";
                ltrContactFooter.Text += "</div>";
                ltrContactFooter.Text += "<div class=\"item\">";
                ltrContactFooter.Text += "<p class=\"contact-method__sub\">Hotline</p>";
                ltrContactFooter.Text += "<a class=\"contact-method__main\" href=\"tel:" + hotline + "\">" + hotline + "</a>";
                ltrContactFooter.Text += "</div>";
            }


        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string content = txtContent.Text.Trim();
            if(string.IsNullOrEmpty(fullname))
            {
                lblError.Text = "Vui lòng nhập họ tên khách hàng.";
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(phone))
            {
                lblError.Text = "Vui lòng nhập số điện thoại.";
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(content))
            {
                lblError.Text = "Vui lòng nhập lời nhắn.";
                lblError.Visible = true;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string kq = ContactController.Insert(fullname, "", phone, content, false, DateTime.Now, "");
                if(kq.ToInt(0)>0)
                {
                    txtFullname.Text = "";
                    txtPhone.Text = "";
                    txtContent.Text = "";
                    lblError.Text = "Gửi liên hệ thành công, chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất. Xin cám ơn.";
                    lblError.ForeColor = System.Drawing.Color.Blue;
                    lblError.Visible = true;
                }
            }
        }
    }
}