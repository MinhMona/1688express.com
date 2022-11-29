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
using MB.Extensions;
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST.Admin
{
    public partial class addOrderWechat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "xuemei912";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                }
                LoadDDL();
                LoadData();
            }
        }
        public void LoadDDL()
        {
            ddlSaler.Items.Clear();
            ddlSaler.Items.Insert(0, "Chọn Saler");

            ddlDatHang.Items.Clear();
            ddlDatHang.Items.Insert(0, "Chọn nhân viên đặt hàng");

            ddlKhoTQ.Items.Clear();
            ddlKhoTQ.Items.Insert(0, "Chọn nhân viên kho TQ");

            ddlKhoVN.Items.Clear();
            ddlKhoVN.Items.Insert(0, "Chọn nhân viên kho đích");

            var salers = AccountController.GetAllByRoleID(6);
            if (salers.Count > 0)
            {
                ddlSaler.DataSource = salers;
                ddlSaler.DataBind();
            }

            var dathangs = AccountController.GetAllByRoleID(3);
            if (dathangs.Count > 0)
            {
                ddlDatHang.DataSource = dathangs;
                ddlDatHang.DataBind();
            }

            var khotqs = AccountController.GetAllByRoleID(4);
            if (khotqs.Count > 0)
            {
                ddlKhoTQ.DataSource = khotqs;
                ddlKhoTQ.DataBind();
            }

            var khovns = AccountController.GetAllByRoleID(5);
            if (khovns.Count > 0)
            {
                ddlKhoVN.DataSource = khovns;
                ddlKhoVN.DataBind();
            }
        }
        public void LoadData()
        {
            if (Request.QueryString["u"] != null)
            {
                #region Lấy thông tin người đặt
                string uOrder = Request.QueryString["u"];
                var accOrder = AccountController.GetByUsername(uOrder);
                if (accOrder != null)
                {
                    lblUsername.Text = accOrder.Username;
                    var aiOrder = AccountInfoController.GetByUserID(accOrder.ID);
                    if (aiOrder != null)
                    {
                        txtFullname.Text = aiOrder.FirstName + " " + aiOrder.LastName;
                        txtAddress.Text = aiOrder.Address;
                        txtEmail.Text = accOrder.Email;
                        txtPhone.Text = aiOrder.Phone;
                    }
                }
                #endregion
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    hdfCurrency.Value = config.Currency;
                }
            }
            else
            {
                Response.Redirect("/admin/userlist.aspx");
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            double currency = 0;
            double wechatFeeConfig = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
                wechatFeeConfig = Convert.ToDouble(config.WeChatFee);
            }
            DateTime currentDate = DateTime.Now;

            string product = hdfProductList.Value;
            int UIDCreate = 0;
            string userCreate = Session["userLoginSystem"].ToString();
            var obj_userCreate = AccountController.GetByUsername(userCreate);
            if (obj_userCreate != null)
            {
                UIDCreate = obj_userCreate.ID;
            }
            string userBuy = lblUsername.Text;
            var obj_user = AccountController.GetByUsername(userBuy);
            if (obj_user != null)
            {
                int salerID = ddlSaler.SelectedValue.ToInt(0);
                int dathangID = ddlDatHang.SelectedValue.ToInt(0);
                int UID = obj_user.ID;
                double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);

                double priceCYN = 0;
                double priceVND = 0;
                string[] products = product.Split('|');
                if (products.Length - 1 > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        #region Cách cũ
                        //string[] item = products[i].Split(']');
                        //string productlink = item[0];
                        //string productimage = item[1];
                        //string productname = item[2];
                        //string productvariable = item[3];
                        //double productquantity = Convert.ToDouble(item[4]);
                        //double productprice = Convert.ToDouble(item[5]);
                        //double productpromotionprice = Convert.ToDouble(item[6]);
                        //var productnote = item[7];
                        //double pricetoPay = 0;

                        //if (productpromotionprice <= productprice)
                        //{
                        //    pricetoPay = productpromotionprice;
                        //}
                        //else
                        //{
                        //    pricetoPay = productprice;
                        //}
                        //priceCYN += (pricetoPay * productquantity);
                        #endregion
                        #region Cách mới
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];

                        double price = 0;
                        if (item[3].ToFloat(0) > 0)
                            price = Convert.ToDouble(item[3]);

                        double productquantity = 0;
                        if (item[4].ToFloat(0) > 0)
                            productquantity = Convert.ToDouble(item[4]);

                        var productnote = item[4];
                        string productimage = item[5];
                        priceCYN += (price * productquantity);
                        #endregion
                    }
                }
                priceVND = priceCYN * currency;

                double serviceFeeMoney = 0;
                var adminfeebuypro = FeeBuyProController.GetAll();
                if (adminfeebuypro.Count > 0)
                {
                    foreach (var item in adminfeebuypro)
                    {
                        if (priceCYN >= item.AmountFrom && priceCYN < item.AmountTo)
                        {
                            serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                            break;
                        }
                    }
                }
                double feeWechatCYN = 0;
                double feeWechatVND = 0;
                if (wechatFeeConfig > 0)
                {
                    feeWechatCYN = (priceCYN * wechatFeeConfig) / 100;
                }
                feeWechatVND = feeWechatCYN * currency;

                double feebpnotdc = serviceFeeMoney * currency;
                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                double feebp = feebpnotdc - subfeebp;
                double TotalPriceVND = priceVND + feebp + feeWechatVND;
                //string AmountDeposit = (TotalPriceVND * LessDeposit / 100).ToString();
                string AmountDeposit = TotalPriceVND.ToString();

                string Deposit = "0";

                string kq = MainOrderController.InsertWeChat(UID, "1688express-orderwechat", "1688express-orderwechat", "", false, "0", false, "0", false, "0",
                            false, "0", false, "0", priceVND.ToString(), priceCYN.ToString(), "0",
                            feebp.ToString(), "0", "", txtFullname.Text, txtAddress.Text, txtEmail.Text,
                            txtPhone.Text, 0, Deposit, currency.ToString(), TotalPriceVND.ToString(), salerID,
                            dathangID, currentDate, UIDCreate, AmountDeposit, 2, feeWechatCYN.ToString(),
                            feeWechatVND.ToString());
                int idkq = Convert.ToInt32(kq);
                if (idkq > 0)
                {
                    for (int i = 0; i < products.Length - 1; i++)
                    {
                        string[] item = products[i].Split(']');
                        string productlink = item[0];
                        string productname = item[1];
                        string productvariable = item[2];

                        double price = 0;
                        if (item[3].ToFloat(0) > 0)
                            price = Convert.ToDouble(item[3]);

                        double productquantity = 0;
                        if (item[4].ToFloat(0) > 0)
                            productquantity = Convert.ToDouble(item[4]);

                        var productnote = item[5];
                        string productimage = item[6];
                        priceCYN += (price * productquantity);

                        int quantity = Convert.ToInt32(item[4]);
                        double u_pricecbuy = 0;
                        double u_pricevn = 0;
                        double e_pricebuy = 0;
                        double e_pricevn = 0;
                        u_pricecbuy = price;
                        u_pricevn = price * currency;

                        e_pricebuy = u_pricecbuy * quantity;
                        e_pricevn = u_pricevn * quantity;

                        HttpPostedFile postedFile = Request.Files["" + productimage + ""];
                        string imageinput = "";
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            var o = "/Uploads/Images/" + Guid.NewGuid() + System.IO.Path.GetExtension(postedFile.FileName);
                            postedFile.SaveAs(Server.MapPath(o));
                            imageinput = o;
                        }
                        string imagein = "";
                        if (!string.IsNullOrEmpty(imageinput))
                        {
                            imagein = imageinput;
                        }

                        string ret = OrderController.Insert(UID, productname, productname, price.ToString(), price.ToString(),
                            productvariable,
                        productvariable, productvariable, imagein, imagein, "1688express-orderwechat", "1688express-orderwechat", "1688express-orderwechat", "1688express-orderwechat", quantity.ToString(),
                        "", "", "", "", "", productlink, "", "", "", "", "", productnote,
                        "", "0", "Web", "", false, false, "0",
                        false, "0", false, "0", false, "0", false,
                        "0", e_pricevn.ToString(), e_pricebuy.ToString(), productnote, txtFullname.Text.Trim(), txtAddress.Text.Trim(), txtEmail.Text.Trim(),
                        txtPhone.Text.Trim(), 0, "0", currency.ToString(), TotalPriceVND.ToString(), idkq, DateTime.Now, UIDCreate);
                        OrderController.UpdatePricePriceReal(ret.ToInt(0), price.ToString(), price.ToString());
                    }
                    MainOrderController.UpdateReceivePlace(idkq, UID, "4", 1);
                    NotificationController.Inser(UIDCreate, userCreate, obj_user.ID,
                                                               obj_user.Username, idkq,
                                                               "Có đơn hàng wechat mới ID là: " + idkq, 0,
                                                               1, currentDate, userCreate);
                    var admins = AccountController.GetAllByRoleID(0);
                    if (admins.Count > 0)
                    {
                        foreach (var admin in admins)
                        {
                            NotificationController.Inser(UIDCreate, userCreate, admin.ID,
                                                               admin.Username, idkq,
                                                               "Có đơn hàng wechat mới ID là: " + idkq, 0,
                                                               1, currentDate, userCreate);
                        }
                    }

                    var managers = AccountController.GetAllByRoleID(2);
                    if (managers.Count > 0)
                    {
                        foreach (var manager in managers)
                        {
                            NotificationController.Inser(UIDCreate, userCreate, manager.ID,
                                                               manager.Username, 0,
                                                               "Có đơn hàng wechat mới ID là: " + idkq, 0,
                                                               1, currentDate, userCreate);
                        }
                    }
                }
                double salepercent = 0;
                double salepercentaf3m = 0;
                double dathangpercent = 0;
                if (config != null)
                {
                    salepercent = Convert.ToDouble(config.SalePercent);
                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                }
                string salerName = "";
                string dathangName = "";

                if (salerID > 0)
                {
                    var sale = AccountController.GetByID(salerID);
                    if (sale != null)
                    {
                        salerName = sale.Username;
                        NotificationController.Inser(UIDCreate, userCreate, sale.ID,
                                                               sale.Username, idkq,
                                                               "Có đơn hàng wechat mới ID là: " + idkq, 0,
                                                               1, currentDate, userCreate);
                        var createdDate = Convert.ToDateTime(sale.CreatedDate);
                        int d = currentDate.Subtract(createdDate).Days;
                        if (d > 90)
                        {
                            double per = feebp * salepercentaf3m / 100;
                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                            currentDate, currentDate, userCreate);
                        }
                        else
                        {
                            double per = feebp * salepercent / 100;
                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                            currentDate, currentDate, userCreate);
                        }
                    }
                }
                if (dathangID > 0)
                {
                    var dathang = AccountController.GetByID(dathangID);
                    if (dathang != null)
                    {
                        dathangName = dathang.Username;
                        NotificationController.Inser(UID, userCreate, dathang.ID,
                                                               dathang.Username, idkq,
                                                               "Có đơn hàng wechat mới ID là: " + idkq, 0,
                                                               1, currentDate, userCreate);
                        StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                            currentDate, currentDate, userCreate);
                    }
                }
                PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng wechat thành công", "s", true, Page);
            }
        }
    }
}