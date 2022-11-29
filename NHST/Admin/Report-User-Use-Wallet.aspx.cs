using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.Admin
{
    public partial class Report_User_Use_Wallet : System.Web.UI.Page
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
            rdatefrom.SelectedDate = DateTime.Now;
            rdateto.SelectedDate = DateTime.Now.AddDays(30);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //int UID = Request.QueryString["i"].ToInt();
            var listhist = HistoryPayWalletController.GetFromDateTodate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));

            gr.DataSource = listhist;
            gr.DataBind();
        }
        public void LoadGrid()
        {
            //int UID = Request.QueryString["i"].ToInt();
            var listhist = HistoryPayWalletController.GetFromDateTodate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));

            gr.DataSource = listhist;
            //gr.DataBind();
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
    }
}