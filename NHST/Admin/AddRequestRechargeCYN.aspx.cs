using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;

namespace NHSG.admin
{
    public partial class AddRequestRechargeCYN : System.Web.UI.Page
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
                    if (ac != null)
                    {
                        if (ac.RoleID == 1 || ac.RoleID == 3)
                            Response.Redirect("/trang-chu");
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            string uReceive = txtUsername.Text.Trim().ToLower();
            var admin = AccountController.GetByUsername(username);
            var u = AccountController.GetByUsername(uReceive);
            string content = txtNote.Text;
            double money = Convert.ToDouble(pAmount.Value);
            DateTime currentdate = DateTime.Now;
            if (u != null)
            {
                int UID = u.ID;
                if (money > 0)
                {
                    int status = ddlStatus.SelectedValue.ToInt(0);
                    string kq = WithdrawController.InsertRechargeCYN(UID, u.Username,Convert.ToDouble(pAmount.Value),
                        txtNote.Text, status, DateTime.Now, username);
                    if (kq.ToInt(0) > 0)
                    {
                        if (status == 2)
                        {
                            double walletCYN = Convert.ToDouble(u.WalletCYN);
                            walletCYN = walletCYN + money;
                            AccountController.updateWalletCYN(u.ID, walletCYN);
                            if (string.IsNullOrEmpty(content))
                                HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, walletCYN, 2, 3, u.Username + " đã được nạp tiền tệ vào tài khoản.",
                                    currentdate, username);
                            else
                                HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, walletCYN, 2, 3, content, currentdate, username);

                            //NotificationController.Inser(admin.ID, admin.Username, UID,
                            //u.Username, 0, "", "Bạn đã được admin nạp " + money + " tệ vào trong tài khoản", 0,
                            //currentdate, username);                            

                        }
                        PJUtils.ShowMessageBoxSwAlert("Tạo lệnh nạp tiền thành công", "s", true, Page);
                    }
                }

            }
            else
            {
                lbl_check.Text = "Tên đăng nhập không tồn tại.";
                lbl_check.Visible = true;
            }
        }
    }
}