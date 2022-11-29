﻿using System;
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
    public partial class AddProduct : System.Web.UI.Page
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
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/ProductIMG/";
            string BackLink = "/Admin/ProductList.aspx";
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
            string kq = ProductController.Insert(txtWebName.Text, IMG, txtProductname.Text, txtWebLink.Text, chkIshot.Checked, chkIsHidden.Checked
                , DateTime.Now, Username);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Tạo thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Tạo mới. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}