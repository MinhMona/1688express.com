using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;
using static NHST.Controllers.SmallPackageController;

namespace NHST.Admin
{
    public partial class report_warehouse_staff : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
            }
        }
        public void LoadData()
        {

            var listStaff = AccountController.GetAllByRoleIDAndRoleIDNotOr(4, 5);
            ddlUsername.Items.Clear();
            ddlUsername.Items.Insert(0, "Chọn nhân viên kho");
            if (listStaff.Count > 0)
            {
                ddlUsername.DataSource = listStaff;
                ddlUsername.DataBind();
            }
            int UID = Request.QueryString["uid"].ToInt(0);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            ddlUsername.SelectedValue = UID.ToString();
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.SelectedDate = Convert.ToDateTime(fd);
            if (!string.IsNullOrEmpty(td))
                rdateto.SelectedDate = Convert.ToDateTime(td);

            double totalPackage = 0;
            double totalPackageWeight = 0;
            var acc = AccountController.GetByID(UID);
            if (acc != null)
            {
                int role = Convert.ToInt32(acc.RoleID);
                List<SmallPackageGet> smallpackages = new List<SmallPackageGet>() ;
                if (role == 4)
                {
                    smallpackages = SmallPackageController.GetbyKhoTQIDOrKho(UID, 0, fd, td);
                }
                else
                {
                    smallpackages = SmallPackageController.GetbyKhoTQIDOrKho(0, UID, fd, td);
                }

                if (smallpackages.Count > 0)
                {
                    totalPackage = smallpackages.Count;
                    foreach (var p in smallpackages)
                    {
                        double weight = 0;
                        if(p.Weight.ToString().ToFloat(0)>0)                        
                            weight = Convert.ToDouble(p.Weight);
                        totalPackageWeight += weight;
                    }
                }
            }
            lblPackageCount.Text = string.Format("{0:N0}", totalPackage);
            lblPackageWeight.Text = string.Format("{0:N0}", totalPackageWeight);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string uid = ddlUsername.SelectedValue;
            string fd = rdatefrom.SelectedDate.ToString();
            string td = rdateto.SelectedDate.ToString();
            Response.Redirect("/admin/report-warehouse-staff.aspx?uid=" + uid + "&fd=" + fd + "&td=" + td + "");
        }
    }
}