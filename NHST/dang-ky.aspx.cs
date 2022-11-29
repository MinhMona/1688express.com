using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;


namespace NHST
{
    public partial class dang_ky1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPrefix();
                LoadSEO();
            }
        }
        public void LoadSEO()
        {
            var home = PageSEOController.GetByID(5);
            string weblink = "https://1688express.com";
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
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}

            //ddlPrefix.DataBind();
            //ddlPrefix.SelectedValue = "+84";
        }
        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text.Trim().ToLower();
            string Email = txtEmail.Text.Trim();
            var checkuser = AccountController.GetByUsername(Username);
            var checkemail = AccountController.GetByEmail(Email);
            string Phone = txtPhone.Text.Trim().Replace(" ", "");
            var getaccountinfor = AccountInfoController.GetByPhone(Phone);
            bool checkusernamebool = false;
            bool checkemailbool = false;
            bool checkphonebool = false;
            string error = "";
            bool check = PJUtils.CheckUnicode(Username);
            DateTime currentDate = DateTime.Now;
            DateTime bir = DateTime.Now;
            if (!string.IsNullOrEmpty(rBirthday.SelectedDate.ToString()))
            {
                bir = Convert.ToDateTime(rBirthday.SelectedDate);
            }
            if (Username.Contains(" "))
            {
                lblcheckemail.Visible = true;
                lblcheckemail.Text = "Tên đăng nhập không được có dấu cách.";
            }
            else if (check == true)
            {
                lblcheckemail.Visible = true;
                lblcheckemail.Text = "Tên đăng nhập không được có dấu tiếng Việt.";
            }
            else
            {
                if (checkuser != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Tên đăng nhập / Nickname đã được sử dụng vui lòng chọn Tên đăng nhập / Nickname khác.<br/>";
                    checkusernamebool = true;
                }
                if (checkemail != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Email đã được sử dụng vui lòng chọn Email khác.<br/>";
                    checkemailbool = true;
                }
                if (getaccountinfor != null)
                {
                    //lblcheckemail.Visible = true;
                    error += "Số điện thoại đã được sử dụng vui lòng chọn Số điện thoại khác.<br/>";
                    checkphonebool = true;
                }
                if (checkusernamebool == false && checkemailbool == false && checkphonebool == false)
                {
                    string id = AccountController.Insert(Username, Email, txtpass.Text.Trim(), 1, 1, 0, 2, 0, 0, DateTime.Now, "", DateTime.Now, "");
                    if (Convert.ToInt32(id) > 0)
                    {
                        int UID = Convert.ToInt32(id);
                        string idai = AccountInfoController.Insert(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), "",
                            Phone, Email, txtPhone.Text.Trim(), txtAddress.Text, "", "", bir, ddlGender.SelectedValue.ToString().ToInt(1),
                            DateTime.Now, "", DateTime.Now, "");
                        if (idai == "1")
                        {
                            tbl_Account ac = AccountController.Login(Username, txtpass.Text.Trim());
                            if (ac != null)
                            {
                                var ai = AccountInfoController.GetByUserID(ac.ID);
                                if (ac.Status == 1)
                                {
                                    //Chưa kích hoạt
                                    //Session["userNotActive"] = ac.Username;
                                    //if (ai != null)
                                    //{
                                    //    string prefix = ai.MobilePhonePrefix;
                                    //    string phone = ai.MobilePhone;
                                    //    string otpreturn = OTPUtils.ResetAndCreateOTP(ac.ID, prefix, phone, 1);
                                    //    if (otpreturn != null)
                                    //    {
                                    //        string message = MessageController.GetByType(1).Message + " " + otpreturn;
                                    //        string kq = ESMS.Send(prefix + phone, message);
                                    //        Response.Redirect("/OTP");
                                    //    }
                                    //}
                                }
                                else if (ac.Status == 2)
                                {
                                    //Đã kích hoạt
                                    Session["userLoginSystem"] = ac.Username;

                                    #region Tặng tiền
                                    double moneytang = 0;
                                    var conf = ConfigurationController.GetByTop1();
                                    if (conf != null)
                                    {
                                        moneytang = Convert.ToDouble(conf.RegisterMoney);
                                    }
                                    if (moneytang > 0)
                                    {
                                        AdminSendUserWalletController.Insert(ac.ID, ac.Username, moneytang,
                                        2, "Tiền khuyến mãi khi đăng ký tài khoản", currentDate, "admin");
                                        AccountController.updateWallet(ac.ID, moneytang, currentDate, "admin");
                                        HistoryPayWalletController.Insert(ac.ID, ac.Username, 0, moneytang, ac.Username + " đã được tặng tiền vào tài khoản khi đăng ký.", moneytang, 2, 4, currentDate, "admin");
                                        NotificationController.Inser(1, "admin", ac.ID, ac.Username, 0,
                                                                       "Bạn đã được nạp: " + string.Format("{0:N0}", moneytang) + " VNĐ vào tài khoản khi đăng ký tài khoản.", 0,
                                                                       2, DateTime.Now, "admin");
                                    }

                                    #endregion

                                    var admins = AccountController.GetAllByRoleID(0);
                                    if (admins.Count > 0)
                                    {
                                        foreach (var admin in admins)
                                        {
                                            NotificationController.Inser(ac.ID, ac.Username, admin.ID,
                                                                               admin.Username, 0,
                                                                               "Khách hàng mới có username là: " + ac.Username, 0,
                                                                               6, currentDate, ac.Username);
                                        }
                                    }

                                    var managers = AccountController.GetAllByRoleID(2);
                                    if (managers.Count > 0)
                                    {
                                        foreach (var manager in managers)
                                        {
                                            NotificationController.Inser(ac.ID, ac.Username, manager.ID,
                                                                               manager.Username, 0,
                                                                               "Khách hàng mới có username là: " + ac.Username, 0,
                                                                               6, currentDate, ac.Username);
                                        }
                                    }
                                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                    hubContext.Clients.All.addNewMessageToPage("", "");

                                    try
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "Qúy khách đã khởi tạo thành công tài khoản trên hệ thống với username: <strong>" + ac.Username + "</strong><br/><br/>";
                                        message += "Để tiếp tục mua hàng, quý khách vui lòng tham khảo thêm các hướng dẫn:<br/>";
                                        message += "    1. <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-cai-dat-addon-mua-hang-1688express\" style=\"text-decoration:underline:\" target=\"_blank\">Cài ADDON</a><br/>";
                                        message += "    2. <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" style=\"text-decoration:underline:\" target=\"_blank\">Nạp tiền vào tài khoản </a><br/>";
                                        message += "    3. <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-tao-don-va-mua-hang\" style=\"text-decoration:underline:\" target=\"_blank\">Tạo đơn hàng và mua hàng </a><br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", Email, "Khởi tạo tài khoản tại 1688 Express", message, "");

                                    }
                                    catch
                                    {

                                    }

                                    Response.Redirect("/trang-chu");
                                }
                                else if (ac.Status == 3)
                                {
                                    //Đã Block
                                }
                            }
                            else
                            {
                                //lblError.Text = "Đăng nhập không thành công, vui lòng kiểm tra lại.";
                                //lblError.Visible = true;
                            }
                            //Response.Redirect("/Login.aspx");
                        }
                    }
                }
                else
                {
                    lblError.Text = error;
                    lblError.Visible = true;
                }
            }

        }
    }
}