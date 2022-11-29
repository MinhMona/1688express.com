using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.Admin
{
    public partial class EditShippingTypeVN : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
                LoadNews();

            }
        }
        public void LoadNews()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ShippingTypeVNController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtTitle.Text = news.ShippingTypeVNName;
                    isHidden.Checked = Convert.ToBoolean(news.IsHidden);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int NewsID = ViewState["NID"].ToString().ToInt(0);
            string BackLink = "/Admin/PageList.aspx";
            var news = ShippingTypeVNController.GetByID(NewsID);
            if (news != null)
            {
                string NewsTitle = txtTitle.Text;
                
                string kq = ShippingTypeVNController.Update(NewsID, NewsTitle, "", isHidden.Checked, currentDate, Email);
                PJUtils.ShowMessageBoxSwAlert("Cập nhật phương thức thành công.", "s", true, Page);
            }

        }
    }
}