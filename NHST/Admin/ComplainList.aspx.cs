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
using System.Data;
using System.Text;
using MB.Extensions;

namespace NHST.Admin
{
    public partial class ComplainList : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 3)
                        Response.Redirect("/trang-chu");
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac.RoleID == 3)
            {
                var la = ComplainController.GetAllWithStatus(tSearchName.Text.Trim().ToLower(), Convert.ToInt32(ddlStatus.SelectedValue));
                if (la != null)
                {
                    if (la.Count > 0)
                    {
                        //List<tbl_Complain> cs = new List<tbl_Complain>();
                        //foreach (var item in la)
                        //{
                        //    int mainorderID = Convert.ToInt32(item.OrderID);
                        //    var mainorder = MainOrderController.GetAllByID(mainorderID);
                        //    if (mainorder != null)
                        //    {
                        //        if (mainorder.DathangID == ac.ID)
                        //        {
                        //            cs.Add(item);
                        //        }
                        //    }
                        //}

                        //gr.DataSource = cs;
                        var listComs = new List<complainlist>();
                        foreach (var item in la)
                        {
                            int mainorderID = Convert.ToInt32(item.OrderID);
                            var mainorder = MainOrderController.GetAllByID(mainorderID);
                            if (mainorder != null)
                            {
                                if (mainorder.DathangID == ac.ID)
                                {
                                    complainlist c = new complainlist();
                                    c.ID = item.ID;
                                    c.MainOrderID = item.OrderID.ToString();
                                    c.Username = item.CreatedBy;
                                    double money = 0;
                                    if (item.Amount.ToFloat(0) > 0)
                                        money = Convert.ToDouble(item.Amount);

                                    c.Money = string.Format("{0:N0}", money) + " vnđ";
                                    c.Content = item.ComplainText;
                                    c.StatusStr = PJUtils.ReturnStatusComplainRequest(Convert.ToInt32(item.Status));
                                    c.Status = Convert.ToInt32(item.Status);
                                    c.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);
                                    listComs.Add(c);
                                }
                            }
                        }
                        gr.DataSource = listComs;
                    }
                }
            }
            else
            {
                var la = ComplainController.GetAllWithStatus(tSearchName.Text.Trim().ToLower(), Convert.ToInt32(ddlStatus.SelectedValue));
                if (la != null)
                {
                    if (la.Count > 0)
                    {
                        var listComs = new List<complainlist>();
                        foreach (var item in la)
                        {
                            complainlist c = new complainlist();
                            c.ID = item.ID;
                            c.MainOrderID = item.OrderID.ToString();
                            c.Username = item.CreatedBy;
                            double money = 0;
                            if (item.Amount.ToFloat(0) > 0)
                                money = Convert.ToDouble(item.Amount);

                            c.Money = string.Format("{0:N0}", money) + " vnđ";
                            c.Content = item.ComplainText;
                            c.StatusStr = PJUtils.ReturnStatusComplainRequest(Convert.ToInt32(item.Status));
                            c.Status = Convert.ToInt32(item.Status);
                            c.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);
                            listComs.Add(c);
                        }
                        gr.DataSource = listComs;
                    }
                }
            }
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion
        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion

        public class complainlist
        {
            public int ID { get; set; }
            public string MainOrderID { get; set; }
            public string Username { get; set; }
            public string Money { get; set; }
            public string Content { get; set; }
            public string StatusStr { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
        }
    }
}