using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] == null)
            {
                Response.Redirect("/Admin/Login.aspx");
            }
            else
            {
                string username_current = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username_current);
                if (obj_user != null)
                {
                    if (obj_user.RoleID != 1)
                    {
                        lUName.Text = obj_user.Username;

                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                LoadNotification();

            }
        }
        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                //if (obj_user.RoleID == 0)
                //{

                //}
                int UID = obj_user.ID;
                var notiadmin = NotificationController.GetByReceivedID(UID);
                ltrAmountNoti.Text = notiadmin.Count.ToString();
                if (notiadmin.Count > 0)
                {
                    StringBuilder html = new StringBuilder();
                    html.Append("<li><a href=\"javascript:;\" onclick=\"checkalldaxem()\" style=\"border-bottom: solid 1px #ccc;width:100%;text-align:center\">Đã đọc tất cả</a></li>");
                    foreach (var item in notiadmin)
                    {
                        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','" + item.NotifType + "')\">" + item.Message + "</a></li>");
                    }
                    ltrNoti.Text = html.ToString();
                }
                else
                {
                    var notiadmin1 = NotificationController.GetByReceivedIDAndTop20(UID);
                    if (notiadmin1.Count > 0)
                    {
                        StringBuilder html = new StringBuilder();
                        html.Append("<li><a href=\"javascript:;\" onclick=\"checkalldaxem()\" style=\"border-bottom: solid 1px #ccc;width:100%;text-align:center\">Đã đọc tất cả</a></li>");
                        foreach (var item in notiadmin1)
                        {
                            html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','" + item.NotifType + "')\">" + item.Message + "</a></li>");
                        }
                        ltrNoti.Text = html.ToString();
                    }
                    else
                        ltrNoti.Text = "<li role=\"presentation\" id=\"no-notis\"><a href=\"/thong-bao-cua-ban\">Xem thông báo cũ</a></li>";
                }
            }

        }
        protected void timer1_tick(object sender, EventArgs e)
        {

        }
    }
}