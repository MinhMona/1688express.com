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

namespace NHST.Admin
{
    public partial class HistorySendWalletDetail : System.Web.UI.Page
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
                    if (ac.RoleID == 0 || ac.RoleID == 2 || ac.RoleID == 7)
                    {
                        loaddata();
                    }
                    else
                        Response.Redirect("/trang-chu");
                }
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var h = AdminSendUserWalletController.GetByID(id);
                if (h != null)
                {
                    ViewState["ID"] = id;
                    ViewState["UID"] = h.UID;
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int role = ac.RoleID.ToString().ToInt();

                    //if (role == 0 || role == 2 || role == 7)
                    //    pbadmin.Visible = true;
                    //else
                    //    pbadmin.Visible = false;
                    if (role == 0 || role == 2)
                        pbadmin.Visible = true;
                    else
                        pbadmin.Visible = false;
                    if (h.Status == 1)
                        ddlStatus.Enabled = true;
                    else
                        ddlStatus.Enabled = false;

                    lblUsername.Text = h.Username;
                    pContent.Content = h.TradeContent;
                    pWallet.Value = h.Amount;
                    ddlStatus.SelectedValue = h.Status.ToString();
                }
                else
                    Response.Redirect("/Admin/HistorySendWallet.aspx");
            }
            else
                Response.Redirect("/Admin/HistorySendWallet.aspx");
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            int role = 0;
            var u_loginin = AccountController.GetByUsername(username_current);
            if (u_loginin != null)
            {
                role = u_loginin.RoleID.ToString().ToInt(0);
            }
            double money = Convert.ToDouble(pWallet.Value);
            int UID = ViewState["UID"].ToString().ToInt(0);

            var user_wallet = AccountController.GetByID(UID);
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);
            int id = ViewState["ID"].ToString().ToInt(0);
            DateTime currentdate = DateTime.Now;
            string content = pContent.Content;
            var h = AdminSendUserWalletController.GetByID(id);
            string BackLink = "/Admin/HistorySendWallet.aspx";
            if (h != null)
            {
                if (h.Status == 2 || h.Status == 3)
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
                }
                else
                {
                    if (money > 0)
                    {
                        if (user_wallet != null)
                        {
                            double wallet = Convert.ToDouble(user_wallet.Wallet);
                            wallet = wallet + money;
                            if (role == 0 || role == 2 || role == 7)
                            {
                                if (status == 2)
                                {
                                    AdminSendUserWalletController.UpdateStatus(id, status, content, currentdate, username_current);
                                    AccountController.updateWallet(user_wallet.ID, wallet, currentdate, username_current);
                                    if(string.IsNullOrEmpty(content))
                                        HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, money, user_wallet.Username + " đã được nạp tiền vào tài khoản.", wallet, 2, 4, currentdate, username_current);
                                    else
                                        HistoryPayWalletController.Insert(user_wallet.ID, user_wallet.Username, 0, money, content, wallet, 2, 4, currentdate, username_current);

                                    NotificationController.Inser(u_loginin.ID, username_current, user_wallet.ID, user_wallet.Username, 0,
                                                           "Bạn đã được nạp: " + money + " vào tài khoản.", 0,
                                                           2, DateTime.Now, u_loginin.Username);
                                    try
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "<span style=\"font-weight:bold; color:#0070c0\">1688Express</span> đã nạp thành công số tiền: <strong>" + string.Format("{0:N0}", money) + " VNĐ</strong> vào tài khoản: <strong>" + user_wallet.Username + "</strong><br/><br/>";
                                        message += "Qúy khách vui lòng kiểm tra số dư tài khoản ( xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/quan-ly-so-du-va-lich-su-giao-dich\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a> )<br/>";
                                        message += "Chúng tôi sẽ tiến hành đặt đơn hàng, cập nhật và thông báo tình trạng đơn hàng qua email. Quý khách vui lòng check email thường xuyên trong quá trình đặt hàng, để cập nhật đơn hàng sớm nhất.<br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", user_wallet.Email,
                                            "Nạp tiền thành công tại 1688 Express", message, "");
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    AdminSendUserWalletController.UpdateStatus(id, status, content, currentdate, username_current);
                                }
                            }
                            //else
                            //{
                            //    AdminSendUserWalletController.Insert(user_wallet.ID, user_wallet.Username, money, 1, currentdate, username_current);
                            //}
                            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
                            //Response.Redirect("/Admin/HistorySendWallet.aspx");
                        }
                    }
                    else
                    {
                        PJUtils.ShowMessageBoxSwAlert("Vui lòng nhập số tiền lớn hơn 0.", "e", true, Page);
                    }
                }
            }

        }
    }
}