using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;

namespace NHST.Admin
{
    public partial class ComplainDetail : System.Web.UI.Page
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
                    if (ac != null)
                    {
                        if (ac.RoleID != 0 && ac.RoleID != 2 && ac.RoleID != 3)
                            Response.Redirect("/trang-chu");
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Request.QueryString["ID"].ToInt(0);
                if (ID > 0)
                {
                    var com = ComplainController.GetByID(ID);
                    if (com != null)
                    {
                        int com_Status = Convert.ToInt32(com.Status);
                        string username_current = Session["userLoginSystem"].ToString();
                        tbl_Account ac = AccountController.GetByUsername(username_current);
                        if (ac != null)
                        {
                            int role = Convert.ToInt32(ac.RoleID);
                            if (role == 0 || role == 2 || role == 3)
                            {
                                if (role == 0)
                                {
                                    ddlStatus.Items.Add(new ListItem("Đã hủy", "0"));
                                    ddlStatus.Items.Add(new ListItem("Chưa duyệt", "1"));
                                    ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                    ddlStatus.Items.Add(new ListItem("Đã xử lý", "3"));

                                    if (com_Status == 3)
                                    {
                                        ddlStatus.Enabled = false;
                                    }
                                }
                                else
                                {
                                    if (com_Status < 3)
                                    {
                                        ddlStatus.Items.Add(new ListItem("Đã hủy", "0"));
                                        ddlStatus.Items.Add(new ListItem("Chưa duyệt", "1"));
                                        ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                    }
                                    else
                                    {
                                        ddlStatus.Items.Add(new ListItem("Đã hủy", "0"));
                                        ddlStatus.Items.Add(new ListItem("Chưa duyệt", "1"));
                                        ddlStatus.Items.Add(new ListItem("Đang xử lý", "2"));
                                        ddlStatus.Items.Add(new ListItem("Đã xử lý", "3"));
                                        ddlStatus.Enabled = false;
                                    }
                                }
                            }
                        }



                        var ordershop = MainOrderController.GetAllByID(Convert.ToInt32(com.OrderID));
                        if (ordershop != null)
                        {
                            hdfCurrency.Value = ordershop.CurrentCNYVN;
                            lblCurrence.Text = string.Format("{0:N0}", Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                            lblAmountCYN.Text = string.Format("{0:N0}", Convert.ToDouble(com.Amount) / Convert.ToDouble(ordershop.CurrentCNYVN)).Replace(",", ".");
                            ViewState["ID"] = ID;
                            txtUsername.Text = com.CreatedBy;
                            txtMainOrderID.Text = com.OrderID.ToString();
                            txtOrderShopCode.Text = com.OrderShopCode.ToString();
                            txtOrderCode.Text = com.OrderCode;
                            txtStaffComment.Text = com.StaffComment;
                            pBuyNDT.Value = Convert.ToDouble(com.Amount);
                            txtComplainText.Text = com.ComplainText;
                            ddlStatus.SelectedValue = com.Status.ToString();
                            int type = 0;
                            if(com.Type!=null)
                            {
                                type = Convert.ToInt32(com.Type);
                            }
                            ddlType.SelectedValue = type.ToString();
                            if (!string.IsNullOrEmpty(com.IMG))
                            {
                                string[] imgs = com.IMG.Split('|');
                                if(imgs.Length-1>0)
                                {
                                    for (int i = 0; i < imgs.Length-1; i++)
                                    {
                                        var img = imgs[i];
                                        ltrImage.Text += "<a href=\""+img+"\" target=\"_blank\"><img src=\""+img+"\" width=\"120px\" height=\"120px\" style=\"float:left;margin-left:10px;\"/></a>";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string username_current = Session["userLoginSystem"].ToString();
            var ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            string BackLink = "/Admin/ComplainList.aspx";
            if (ID > 0)
            {
                var com = ComplainController.GetByID(ID);
                if (com != null)
                {
                    int status = ddlStatus.SelectedValue.ToInt();

                    ComplainController.Update(com.ID, pBuyNDT.Value.ToString(), txtComplainText.Text, status,
                       ddlType.SelectedValue.ToInt(1), txtStaffComment.Text, DateTime.Now, username_current);
                    //if (status == 3)
                    //{
                    //    string uReceive = txtUsername.Text.Trim().ToLower();
                    //    var u = AccountController.GetByUsername(uReceive);
                    //    if (u != null)
                    //    {
                    //        int UID = u.ID;
                    //        double wallet = Convert.ToDouble(u.Wallet);
                    //        wallet = wallet + Convert.ToDouble(pBuyNDT.Value);

                    //        AccountController.updateWallet(u.ID, wallet, currentDate, username_current);
                    //        HistoryPayWalletController.Insert(u.ID, u.Username, Convert.ToInt32(com.OrderID), Convert.ToDouble(pBuyNDT.Value),
                    //            u.Username + " đã được hoàn tiền khiếu nại của đơn hàng: " + com.OrderID + " vào tài khoản.",
                    //            wallet, 2, 7, currentDate, username_current);

                    //        //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                    //        //    AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                    //        //    "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã duyệt khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                    //        //    5, currentDate, ac.Username);
                    //    }
                    //}
                    //else if (status == 4)
                    //{
                    //    string uReceive = txtUsername.Text.Trim().ToLower();
                    //    var u = AccountController.GetByUsername(uReceive);
                    //    if (u != null)
                    //    {
                    //        int UID = u.ID;
                    //        //NotificationController.Inser(ac.ID, ac.Username, Convert.ToInt32(u.ID),
                    //        //   AccountController.GetByID(Convert.ToInt32(u.ID)).Username, Convert.ToInt32(com.OrderID),
                    //        //   "<a href=\"/khieu-nai?ordershopcode=" + com.OrderID + "\" target=\"_blank\">Admin đã hủy khiếu nại đơn hàng: " + com.OrderID + "  của bạn.</a>", 0,
                    //        //   5, currentDate, ac.Username);
                    //    }
                    //}
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công", "s", true, BackLink, Page);
                }
            }
        }
    }
}