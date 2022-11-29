using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;

namespace NHST.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string a = PJUtils.Encrypt("userpass", "dar123");
                //Response.Write(a);
                if (Session["userLoginSystem"] == null)
                {
                   
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username_current);
                    if (obj_user != null)
                    {
                        
                        if (obj_user.RoleID == 1)
                        {
                            Response.Redirect("/trang-chu");
                        }
                        else
                        {
                            Response.Redirect("/admin/home.aspx");
                        }
                    }

                }               
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = rUser.Text;
            string password = rPass.Text;
            var ac = AccountController.Login(username.Trim().ToLower(), password.Trim());
            if (ac != null)
            {
                if (ac.RoleID != 1)
                {
                    Session["userLoginSystem"] = username;
                    Response.Redirect("/Admin/Home.aspx");
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
            else
            {
                lblError.Text = "Sai Username hoặc Password, vui lòng kiểm tra lại.";
                lblError.Visible = true;
            }
        }
    }
}