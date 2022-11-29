using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class adminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoginSystem"] == null)
            {
                Response.Redirect("/manager/Login.aspx");
            }
            else
            {
                string username_current = Session["userLoginSystem"].ToString();
                var obj_user = AccountController.GetByUsername(username_current);
                if (obj_user != null)
                {
                    if (obj_user.RoleID != 1)
                    {
                        //lUName.Text = obj_user.Username;

                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                LoadNotification();

            }
        }
        public void LoadMenu()
        {

        }
        public void LoadNotification()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    ltrinfo.Text += "<a href=\"/manager/home\" class=\"right-it rate\"><i class=\"fa fa-home\"></i></a>";
                    ltrinfo.Text += "<a href=\"/manager/configuration\" class=\"right-it rate\"> ¥1 = " + string.Format("{0:N0}", config.Currency) + "</a>";
                    ltrinfo.Text += "<a href=\"#\" class=\"right-it noti\"><i class=\"fa fa-bell-o\"></i><span class=\"badge\">10</span></a>";
                    ltrinfo.Text += "<span class=\"right-it username\">" + obj_user.Username + "</span>";
                    ltrinfo.Text += "<a href=\"/trang-chu\" class=\"right-it rate\"><span class=\"right-it username\">Trang ngoài</span></a>";
                    ltrinfo.Text += "<a href=\"/dang-xuat\" class=\"right-it logout\"><i class=\"fa fa-sign-out\"></i>Sign out</a>";
                }
                //if (obj_user.RoleID == 0)
                //{

                //}
                int UID = obj_user.ID;
                var notiadmin = NotificationController.GetByReceivedID(UID);
                //ltrAmountNoti.Text = notiadmin.Count.ToString();
                //if (notiadmin.Count > 0)
                //{
                //    StringBuilder html = new StringBuilder();
                //    foreach (var item in notiadmin)
                //    {
                //        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','2')\">" + item.Message + "</a></li>");
                //    }
                //    ltrNoti.Text = html.ToString();
                //}
                //else
                //{
                //    ltrNoti.Text = "<li role=\"presentation\"><a href=\"javascript:;\">Không có thông báo mới</a></li>";
                //}
            }
        }
    }
}