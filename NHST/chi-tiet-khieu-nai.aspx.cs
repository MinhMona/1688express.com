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
using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class chi_tiet_khieu_nai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }

        }
        public void LoadData()
        {
            if (Request.QueryString["id"] != null)
            {
                string username = Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    int uID = acc.ID;
                    int cID = Request.QueryString["id"].ToString().ToInt(0);
                    var c = ComplainController.GetByID(cID);
                    if (c != null)
                    {
                        if (c.UID == uID)
                        {
                            txtOrderID.Text = c.OrderID.ToString();
                            txtOrderCode.Text = c.OrderCode;
                            ddlType.SelectedValue = c.Type.ToString();
                            string img = c.IMG;
                            StringBuilder html = new StringBuilder();
                            if (!string.IsNullOrEmpty(img))
                            {
                                if (img.Contains("|"))
                                {
                                    string[] imgs = img.Split('|');
                                    if (imgs.Length - 1 > 0)
                                    {
                                        for (int i = 0; i < imgs.Length - 1; i++)
                                        {
                                            string imgItem = imgs[i];
                                            html.Append("<a href=\""+ imgItem + "\" target=\"_blank\" style=\"float:left;margin-right:20px;\"><img src=\"" + imgItem + "\" width=\"150px\" height=\"150px\"/></a>");
                                        }
                                    }
                                }
                                else
                                {
                                    html.Append("<a href=\"" + img + "\" target=\"_blank\" style=\"float:left;margin-right:20px;\"><img src=\"" + img + "\" width=\"150px\" height=\"150px\"/></a>");
                                }
                            }
                            else
                            {
                                html.Append("Không có hình ảnh đính kèm.");
                            }
                            ltrIMG.Text = html.ToString();
                            txtNote.Text = c.ComplainText;
                            txtStaffComment.Text = c.StaffComment;
                            ddlStatus.SelectedValue = c.Status.ToString();
                        }
                    }
                }
            }
        }
    }
}