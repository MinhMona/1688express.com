using MB.Extensions;
using NHST.Bussiness;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Controllers;
using NHST.Controllers;

namespace NHSG.admin
{
    public partial class AddPriceChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            string id = PriceChangeController.Insert(Convert.ToDouble(pPriceFromCYN.Value), Convert.ToDouble(pPriceToCYN.Value),
                Convert.ToDouble(pVip0.Value), Convert.ToDouble(pVip1.Value), Convert.ToDouble(pVip2.Value),
                Convert.ToDouble(pVip3.Value), Convert.ToDouble(pVip4.Value), Convert.ToDouble(pVip5.Value), Convert.ToDouble(pVip6.Value),
                Convert.ToDouble(pVip7.Value), Convert.ToDouble(pVip8.Value),
                DateTime.Now, Username);
            int UID = Convert.ToInt32(id);
            if (UID > 0)
            {
                PJUtils.ShowMsg("Tạo giá tiền tệ thành công.", true, Page);
            }
        }
    }
}