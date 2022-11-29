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
    public partial class EditStep : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadData();

            }
        }

        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = StepController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtStepName.Text = news.StepName;
                    txtStepLink.Text = news.StepLink;
                    pPartnerIndex.Value = news.StepIndex;
                    if (news.StepIMG != null)
                        imgDaiDien.ImageUrl = news.StepIMG;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            int ID = ViewState["NID"].ToString().ToInt(0);
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            string BackLink = "/Admin/StepList.aspx";
            if (pIcon.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in pIcon.UploadedFiles)
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
            else
                IMG = imgDaiDien.ImageUrl;
            string kq = StepController.Update(ID, txtStepName.Text, IMG, Convert.ToInt32(pPartnerIndex.Value), txtStepLink.Text, DateTime.Now, Username);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}