using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using MB.Extensions;

namespace NHST.Admin
{
    public partial class Report_user_wallet : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
                //LoadGrid1();
            }
        }
        public void LoadData()
        {
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            int filter = ddlFilter.SelectedValue.ToInt(0);
            double totalsodu = 0;
            if (filter == 0)
            {
                var la = AccountController.GetAll_View("");
                if (la.Count > 0)
                {
                    List<UserToExcel> us = new List<UserToExcel>();
                    foreach (var item in la)
                    {
                        string username = item.Username;
                        int UID = item.ID;
                        UserToExcel u = new UserToExcel();
                        u.ID = item.ID;
                        u.UserName = item.Username;
                        u.Ho = item.FirstName;
                        u.Ten = item.LastName;
                        u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
                        u.Status = PJUtils.StatusToRequest(item.Status);
                        u.Role = item.RoleName;
                        u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
                        u.RoleID = item.RoleID.ToString().ToInt(1);
                        u.wallet = string.Format("{0:N0}", item.Wallet);
                        u.Saler = item.saler;
                        u.dathang = item.dathang;
                        us.Add(u);
                        totalsodu += Convert.ToDouble(u.wallet);
                    }

                    gr.DataSource = us;
                    gr.DataBind();
                }
            }
            else
            {
                var la = AccountController.GetAllWithWallet_View("");
                if (la.Count > 0)
                {
                    List<UserToExcel> us = new List<UserToExcel>();
                    foreach (var item in la)
                    {
                        string username = item.Username;
                        int UID = item.ID;
                        UserToExcel u = new UserToExcel();
                        u.ID = item.ID;
                        u.UserName = item.Username;
                        u.Ho = item.FirstName;
                        u.Ten = item.LastName;
                        u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
                        u.Status = PJUtils.StatusToRequest(item.Status);
                        u.Role = item.RoleName;
                        u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
                        u.RoleID = item.RoleID.ToString().ToInt(1);
                        u.wallet = string.Format("{0:N0}", item.Wallet);
                        u.Saler = item.saler;
                        u.dathang = item.dathang;
                        us.Add(u);
                        totalsodu += Convert.ToDouble(u.wallet);
                    }

                    gr.DataSource = us;
                    gr.DataBind();
                }
            }
            ltrinf.Text = "";
            ltrinf.Text += "<div class=\"row\">";
            ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
            ltrinf.Text += "<span class=\"label-title\">Tổng số dư của toàn bộ khách hàng</span>";
            ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalsodu) + " VNĐ</span>";
            ltrinf.Text += "</div>";
            ltrinf.Text += "</div>";            
            #endregion

        }

        public void LoadGrid()
        {
            var acc = Session["userLoginSystem"].ToString();
            #region Thống kê thanh toán
            int filter = ddlFilter.SelectedValue.ToInt(0);
            double totalsodu = 0;
            if (filter == 0)
            {
                var la = AccountController.GetAll_View("");
                if (la.Count > 0)
                {
                    List<UserToExcel> us = new List<UserToExcel>();
                    foreach (var item in la)
                    {
                        string username = item.Username;
                        int UID = item.ID;
                        UserToExcel u = new UserToExcel();
                        u.ID = item.ID;
                        u.UserName = item.Username;
                        u.Ho = item.FirstName;
                        u.Ten = item.LastName;
                        u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
                        u.Status = PJUtils.StatusToRequest(item.Status);
                        u.Role = item.RoleName;
                        u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
                        u.RoleID = item.RoleID.ToString().ToInt(1);
                        u.wallet = string.Format("{0:N0}", item.Wallet);
                        u.Saler = item.saler;
                        u.dathang = item.dathang;
                        us.Add(u);
                        totalsodu += Convert.ToDouble(u.wallet);
                    }
                    gr.DataSource = us;
                    //gr.DataBind();
                }
            }
            else
            {
                var la = AccountController.GetAllWithWallet_View("");
                if (la.Count > 0)
                {
                    List<UserToExcel> us = new List<UserToExcel>();
                    foreach (var item in la)
                    {
                        string username = item.Username;
                        int UID = item.ID;
                        UserToExcel u = new UserToExcel();
                        u.ID = item.ID;
                        u.UserName = item.Username;
                        u.Ho = item.FirstName;
                        u.Ten = item.LastName;
                        u.Sodt = item.MobilePhonePrefix + item.MobilePhone;
                        u.Status = PJUtils.StatusToRequest(item.Status);
                        u.Role = item.RoleName;
                        u.CreatedDate = string.Format("{0:dd/MM/yyyy hh:mm}", item.CreatedDate);
                        u.RoleID = item.RoleID.ToString().ToInt(1);
                        u.wallet = string.Format("{0:N0}", item.Wallet);
                        u.Saler = item.saler;
                        u.dathang = item.dathang;
                        us.Add(u);
                        totalsodu += Convert.ToDouble(u.wallet);
                    }

                    gr.DataSource = us;
                    //gr.DataBind();
                }
            }
            ltrinf.Text = "";
            ltrinf.Text += "<div class=\"row\">";
            ltrinf.Text += "<div class=\"col-md-12\" style=\"margin-top: 20px;\">";
            ltrinf.Text += "<span class=\"label-title\">Tổng số dư của toàn bộ khách hàng</span>";
            ltrinf.Text += "<span class=\"label-infor\">" + string.Format("{0:N0}", totalsodu) + " VNĐ</span>";
            ltrinf.Text += "</div>";
            ltrinf.Text += "</div>";
            #endregion
        }

        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            //gr.Rebind();
        }
        #endregion
        public class UserToExcel
        {
            public int ID { get; set; }
            public string UserName { get; set; }
            public string Ho { get; set; }
            public string Ten { get; set; }
            public string Sodt { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public int RoleID { get; set; }
            public string Saler { get; set; }
            public string dathang { get; set; }
            public string wallet { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}