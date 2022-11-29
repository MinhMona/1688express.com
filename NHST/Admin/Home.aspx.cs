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

namespace NHST.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "QingJing";
            //PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter","phuong.nguyenthanh90@gmail.com",
            //                        "Thông báo tại Vận chuyển đa quốc gia", "Test email", "");
        }

        [WebMethod]
        public static string checkallisread()
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var notiadmin = NotificationController.GetByReceivedID(obj_user.ID);
                if (notiadmin.Count > 0)
                {
                    foreach (var item in notiadmin)
                    {
                        NotificationController.UpdateStatus(item.ID, 1, DateTime.Now, username_current);
                    }
                    return "ok";
                }
            }
            return "none";
        }

        [WebMethod]
        public static string checkisread(int ID)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                string kq = NotificationController.UpdateStatus(ID, 1, DateTime.Now, username_current);
                return kq;
            }
            else return "none";
        }
        [WebMethod]
        public static string gettotal()
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                var notiadmin = NotificationController.GetByReceivedID(obj_user.ID);
                if (notiadmin.Count > 0)
                {
                    ResponseNoti r = new ResponseNoti();
                    r.count = notiadmin.Count;

                    StringBuilder html = new StringBuilder();
                    foreach (var item in notiadmin)
                    {
                        html.Append("<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + item.ID + "','" + item.OrderID + "','" + item.NotifType + "')\">" + item.Message + "</a></li>");
                    }
                    r.listNotification = html.ToString();

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(r);
                }
                else return "0";

            }
            else return "0";
        }
        public class ResponseNoti
        {
            public int count { get; set; }
            public string listNotification { get; set; }
        }
    }
}