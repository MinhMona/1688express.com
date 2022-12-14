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

namespace NHST.manager
{
    public partial class AddProductCate : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadDLL();
                }
            }
        }
        public void LoadDLL()
        {
            var pt = ChinaSiteController.GetAll("");
            if (pt != null)
            {
                if (pt.Count > 0)
                {
                    ddlPageType.DataSource = pt;
                    ddlPageType.DataBind();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Email = Session["userLoginSystem"].ToString();
            string KhieuNaiIMG = "/Uploads/";
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/PageList.aspx";
            string IMG = "";
            if (rSiteLogo.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in rSiteLogo.UploadedFiles)
                {
                    var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                    try
                    {
                        f.SaveAs(Server.MapPath(o));
                        IMG = o;
                    }
                    catch { }
                }
            }

            string kq = ProductCategoryController.Insert(ddlPageType.SelectedValue.ToInt(0), txtSitename.Text, IMG, chkIshidden.Checked, currentDate, Email);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công.", "s", true, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}