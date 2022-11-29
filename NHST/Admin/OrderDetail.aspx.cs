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
using System.Web.Script.Serialization;

namespace NHST.Admin
{
    public partial class OrderDetail : System.Web.UI.Page
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
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                    else
                    {
                        if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                        {
                            Response.Redirect("/admin/home.aspx");
                        }
                    }
                }
                checkOrderStaff();
                LoadDDL();
                loaddata();
            }
        }
        public void checkOrderStaff()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {
                int RoleID = obj_user.RoleID.ToString().ToInt();
                int UID = obj_user.ID;
                var id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        int status_order = Convert.ToInt32(o.Status);
                        if (RoleID == 0 || RoleID == 2)
                        {

                        }
                        else if (RoleID == 3)
                        {
                            if (status_order >= 2)
                            {
                                //Role đặt hàng
                                if (o.DathangID == UID)
                                {
                                }
                                else
                                {
                                    Response.Redirect("/Admin/OrderList.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }

                        }
                        else if (RoleID == 4)
                        {
                            if (status_order >= 5 && status_order < 7)
                            {
                                //Role kho TQ
                                if (o.KhoTQID == UID || o.KhoTQID == 0)
                                {

                                }
                                else
                                {
                                    Response.Redirect("/Admin/OrderList.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }

                        }
                        else if (RoleID == 5)
                        {
                            if (status_order >= 5 && status_order <= 7)
                            {
                                //Role Kho VN
                                if (o.KhoVNID == UID || o.KhoVNID == 0)
                                {

                                }
                                else
                                {
                                    Response.Redirect("/Admin/OrderList.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }

                        }
                        else if (RoleID == 6)
                        {
                            if (status_order != 1)
                            {

                                //Role sale
                                if (o.SalerID == UID)
                                {

                                }
                                else
                                {
                                    Response.Redirect("/Admin/OrderList.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }
                        }
                        else if (RoleID == 7)
                        {
                            if (status_order >= 2)
                            {

                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }
                        }
                        else if (RoleID == 8)
                        {
                            if (status_order >= 9 && status_order < 10)
                            {

                            }
                            else
                            {
                                Response.Redirect("/Admin/OrderList.aspx");
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Admin/OrderList.aspx");
                }
            }
        }
        public void LoadDDL()
        {
            LoadReceiPlace();
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
        public void LoadReceiPlace()
        {
            var dt = WarehouseController.GetAllWithIsHidden(false);
            ddlReceivePlace.Items.Clear();
            if (dt.Count > 0)
            {
                foreach (var item in dt)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlReceivePlace.Items.Add(listitem);
                }
            }
            ddlReceivePlace.DataBind();
        }
        public void loaddata()
        {

            var config = ConfigurationController.GetByTop1();
            double currency = 0;
            double currency1 = 0;
            if (config != null)
            {
                double currencyconfig = 0;
                if (!string.IsNullOrEmpty(config.Currency))
                    currencyconfig = Convert.ToDouble(config.Currency);

                hdfcurrent.Value = Math.Floor(currencyconfig).ToString();
                currency = Math.Floor(currencyconfig);
                currency1 = Math.Floor(currencyconfig);
            }

            var hnfee = FeeWeightTQVNController.GetByReceivePlace("Kho Hà Nội");
            if (hnfee.Count > 0)
            {
                string htmlhnfee = "";
                foreach (var item in hnfee)
                {
                    htmlhnfee += item.ReceivePlace + "," + item.WeightFrom + "," + item.WeightTo + "," + item.Amount + "|";
                }
                hdfFeeTQVNHN.Value = htmlhnfee;
            }
            var hcmfee = FeeWeightTQVNController.GetByReceivePlace("Kho Việt Trì");
            if (hcmfee.Count > 0)
            {
                string htmlhcmfee = "";
                foreach (var item in hcmfee)
                {
                    htmlhcmfee += item.ReceivePlace + "," + item.WeightFrom + "," + item.WeightTo + "," + item.Amount + "|";
                }
                hdfFeeTQVNHCM.Value = htmlhcmfee;
            }

            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);

            int uid = obj_user.ID;
            var id = Convert.ToInt32(Request.QueryString["id"]);
            ltrOrderID.Text = id.ToString();
            if (id > 0)
            {
                var oU = MainOrderController.GetAllByID(id);
                if (oU != null)
                {
                    #region Lấy Sản phẩm có group lại
                    double totalWeight = 0;
                    List<objProductOfShop> poss = new List<objProductOfShop>();
                    var listShopID = OrderController.GetByMainOrderIDGroupByShopID(oU.ID);
                    if (listShopID.Count > 0)
                    {
                        for (int i = 0; i < listShopID.Count; i++)
                        {
                            string shopID = listShopID[i];
                            var productlist = OrderController.GetByMainOrderIDAndShopID(oU.ID, shopID);
                            if (productlist.Count > 0)
                            {
                                objProductOfShop pos = new objProductOfShop();
                                pos.shopID = shopID;
                                List<ShopProduct> sps = new List<ShopProduct>();
                                pos.shopName = productlist[0].shop_name;
                                double FeeShipVN = 0;
                                if (productlist[0].FeeShipCN != null)
                                {
                                    if (!string.IsNullOrEmpty(productlist[0].FeeShipCN))
                                        FeeShipVN = Convert.ToDouble(productlist[0].FeeShipCN);
                                }

                                pos.FeeShipVN = FeeShipVN;
                                foreach (var item in productlist)
                                {
                                    double currentcyt = Convert.ToDouble(item.CurrentCNYVN);
                                    double price = 0;
                                    double pricepromotion = Convert.ToDouble(item.price_promotion);
                                    double priceorigin = Convert.ToDouble(item.price_origin);
                                    if (pricepromotion > 0)
                                    {
                                        if (priceorigin > pricepromotion)
                                        {
                                            price = pricepromotion;
                                        }
                                        else
                                        {
                                            price = priceorigin;
                                        }
                                    }
                                    else
                                    {
                                        price = priceorigin;
                                    }
                                    double vndprice = price * currentcyt;
                                    ShopProduct sp = new ShopProduct();
                                    sp.ID = item.ID;
                                    sp.stt = 0;
                                    sp.IMG = item.image_origin;
                                    sp.productName = item.title_origin;
                                    sp.productLink = item.link_origin;
                                    sp.variable = item.property;
                                    sp.quantity = item.quantity;
                                    //sp.priceVND = string.Format("{0:N0}", vndprice);
                                    //sp.priceCYN = "¥" + string.Format("{0:0.##}", price);
                                    sp.priceVND = vndprice.ToString();
                                    sp.priceCYN = price.ToString();
                                    sp.note = item.brand;
                                    bool isCensor = false;
                                    if (item.IsCensorProduct != null)
                                        isCensor = Convert.ToBoolean(item.IsCensorProduct);
                                    sp.isCensorProduct = isCensor;
                                    if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                    {
                                        sp.status = "<p class=\"\">Còn hàng</p></td>";
                                    }
                                    else
                                    {
                                        if (item.ProductStatus == 2)
                                            sp.status = "<p class=\"bg-red\">Hết hàng</p>";
                                        else
                                            sp.status = "<p class=\"\">Còn hàng</p>";
                                    }
                                    sps.Add(sp);
                                }
                                pos.sp = sps;
                                List<OrderShopCode> osds = new List<OrderShopCode>();
                                var ods = OrderShopCodeController.GetByShopIDAndMainOrderID(shopID, oU.ID);
                                if (ods.Count > 0)
                                {
                                    foreach (var item in ods)
                                    {
                                        OrderShopCode osd = new OrderShopCode();
                                        osd.ordershopCode = item;
                                        List<tbl_SmallPackage> spsnew = new List<tbl_SmallPackage>();
                                        spsnew = SmallPackageController.GetByOrderShopCodeID(item.ID);
                                        osd.smallPackage = spsnew;
                                        osds.Add(osd);
                                    }
                                }
                                pos.orderShopCode = osds;
                                poss.Add(pos);
                            }
                        }
                    }
                    if (poss.Count > 0)
                    {
                        double fscn = 0;
                        foreach (var item in poss)
                        {
                            fscn += Convert.ToDouble(item.FeeShipVN);
                        }

                        pCNShipFeeNDT.Value = fscn;
                        double feeshiptqvnd = fscn * currency1;
                        pCNShipFee.Value = feeshiptqvnd;
                        MainOrderController.UpdateFeeShipTQVN(oU.ID, feeshiptqvnd.ToString());
                    }
                    #endregion
                }

                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    hdfcurrent.Value = Math.Floor(Convert.ToDouble(o.CurrentCNYVN)).ToString();
                    currency = Math.Floor(Convert.ToDouble(o.CurrentCNYVN));
                    currency1 = Math.Floor(Convert.ToDouble(o.CurrentCNYVN));

                    #region Lấy ra lịch sử yêu cầu xuất kho
                    StringBuilder htmlExport = new StringBuilder();
                    var exp = ExportRequestTurnController.GetAllExportByMainOrderID(id);
                    if (exp.Count > 0)
                    {
                        foreach (var item in exp)
                        {
                            var list = ExportRequestTurnController.GetByMainOrderIDAndFTTD(id, item.DateExport, item.DateExport.AddDays(1));
                            if (list.Count > 0)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    var ex = list[i];
                                    var reoutsto = RequestOutStockController.GetByExportRequestTurnID(ex.ID);
                                    if (i == 0)
                                    {
                                        htmlExport.Append("<tr>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\" rowspan=\"" + list.Count + "\">" + string.Format("{0:dd/MM/yyyy}", item.DateExport) + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + Math.Round(Convert.ToDouble(ex.TotalWeight), 1) + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + reoutsto.Count + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + Math.Round(Convert.ToDouble(ex.TotalPriceCYN), 2) + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + string.Format("{0:N0}", ex.TotalPriceVND) + "</td>");
                                        var sh = ShippingTypeVNController.GetByID(Convert.ToInt32(ex.ShippingTypeInVNID));
                                        if (sh != null)
                                        {
                                            htmlExport.Append("<td class=\"vermid-tecenter\">" + sh.ShippingTypeVNName + "</td>");
                                        }
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + ex.Note + "</td>");
                                        htmlExport.Append("</tr>");

                                    }
                                    else
                                    {
                                        htmlExport.Append("<tr>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + Math.Round(Convert.ToDouble(ex.TotalWeight), 1) + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + reoutsto.Count + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + Math.Round(Convert.ToDouble(ex.TotalPriceCYN), 2) + "</td>");
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + string.Format("{0:N0}", ex.TotalPriceVND) + "</td>");
                                        var sh = ShippingTypeVNController.GetByID(Convert.ToInt32(ex.ShippingTypeInVNID));
                                        if (sh != null)
                                        {
                                            htmlExport.Append("<td class=\"vermid-tecenter\">" + sh.ShippingTypeVNName + "</td>");
                                        }
                                        htmlExport.Append("<td class=\"vermid-tecenter\">" + ex.Note + "</td>");
                                        htmlExport.Append("</tr>");
                                    }
                                }
                            }
                        }
                    }
                    ltrExportHistory.Text = htmlExport.ToString();
                    #endregion
                    hdfOrderID.Value = id.ToString();
                    ViewState["ID"] = id;
                    ltrPrint.Text += "<a class=\"btn pill-btn primary-btn admin-btn\" target=\"_blank\" href='/Admin/PrintStamp.aspx?id=" + id + "'>In Tem</a>";
                    double currentcyynn = 0;
                    if (!string.IsNullOrEmpty(o.CurrentCNYVN))
                        currentcyynn = Convert.ToDouble(o.CurrentCNYVN);
                    currency = currentcyynn;
                    ViewState["MOID"] = id;
                    #region Lịch sử thanh toán
                    var PayorderHistory = PayOrderHistoryController.GetAllByMainOrderID(o.ID);
                    if (PayorderHistory.Count > 0)
                    {
                        rptPayment.DataSource = PayorderHistory;
                        rptPayment.DataBind();
                    }
                    else
                    {
                        ltrpa.Text = "<tr>Chưa có lịch sử thanh toán nào.</tr>";
                    }
                    #endregion

                    #region CheckRole
                    if (obj_user != null)
                    {
                        int RoleID = Convert.ToInt32(obj_user.RoleID);
                        hdfUserRole.Value = RoleID.ToString();
                        if (RoleID == 0)
                            ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";
                        else if (RoleID == 1)
                            ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" />";
                        else
                            ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";

                        if (RoleID == 7)
                        {
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = false;
                            pCNShipFeeNDT.Visible = false;
                            //pBuyNDT.Visible = false;
                            pWeightNDT.Visible = false;
                            pCheckNDT.Visible = false;
                            pPackedNDT.Visible = false;
                            pDeposit.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pCheck.Enabled = false;
                            pWeight.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = true;
                            ltr_OrderFee_UserInfo.Visible = false;
                            ltr_AddressReceive.Visible = false;
                            btnUpdate.Visible = true;
                            btnThanhtoan.Visible = true;
                            //pShipHomeNDT.Visible = true;
                            //pnadminmanager.Visible = true;
                            ddlReceivePlace.Enabled = true;
                            ddlShippingType.Enabled = true;
                            pAmountDeposit.Enabled = false;
                        }
                        else if (RoleID == 3)
                        {
                            ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            pCNShipFeeNDT.Visible = true;
                            pCNShipFee.Enabled = true;
                            if (o.Status > 2)
                            {
                                ddlStatus.Enabled = false;
                                ddlStatus.Items.Add(new ListItem("Đang chuyển đến kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                pCNShipFeeNDT.Visible = false;
                                pCNShipFee.Enabled = false;
                            }
                            ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\">Thêm mã vận đơn</a></div>";
                            //pDepositNDT.Visible = false;

                            //pBuyNDT.Visible = false;
                            pWeightNDT.Visible = false;
                            pCheckNDT.Visible = false;
                            pPackedNDT.Visible = false;
                            pDeposit.Enabled = false;

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pCheck.Enabled = false;
                            pWeight.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;
                            txtOrdertransactionCode.Enabled = true;
                            txtOrdertransactionCode2.Enabled = true;
                            txtOrdertransactionCode3.Enabled = true;
                            txtOrdertransactionCode4.Enabled = true;
                            txtOrdertransactionCode5.Enabled = true;
                            txtOrdertransactionCodeWeight.Enabled = true;
                            txtOrdertransactionCodeWeight2.Enabled = true;
                            txtOrdertransactionCodeWeight3.Enabled = true;
                            txtOrdertransactionCodeWeight4.Enabled = true;
                            txtOrdertransactionCodeWeight5.Enabled = true;
                            ltr_OrderFee_UserInfo.Visible = true;
                            ltr_AddressReceive.Visible = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn pill-btn primary-btn admin-btn full-width\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 4)
                        {
                            //ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            //ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            //ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            if (o.Status < 5)
                            {
                                ddlStatus.Enabled = false;
                                ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                                ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                btnUpdate.Visible = false;
                                pPackedNDT.Enabled = false;
                                pPacked.Enabled = false;
                                pWeightNDT.Enabled = false;
                                pWeight.Enabled = false;
                            }
                            else if (o.Status >= 5 && o.Status < 6)
                            {
                                ddlStatus.Enabled = true;
                                pPackedNDT.Enabled = true;
                                pPacked.Enabled = true;

                                pWeightNDT.Enabled = true;
                                pWeight.Enabled = true;

                                txtOrdertransactionCode.Enabled = true;
                                txtOrdertransactionCode2.Enabled = true;
                                txtOrdertransactionCode3.Enabled = true;
                                txtOrdertransactionCode4.Enabled = true;
                                txtOrdertransactionCode5.Enabled = true;
                                txtOrdertransactionCodeWeight.Enabled = true;
                                txtOrdertransactionCodeWeight2.Enabled = true;
                                txtOrdertransactionCodeWeight3.Enabled = true;
                                txtOrdertransactionCodeWeight4.Enabled = true;
                                txtOrdertransactionCodeWeight5.Enabled = true;
                                ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\">Thêm mã vận đơn</a></div>";

                                ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            }
                            else if (o.Status >= 6)
                            {
                                ddlStatus.Enabled = false;
                                ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                                ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                btnUpdate.Visible = false;
                                pPackedNDT.Enabled = false;
                                pPacked.Enabled = false;
                                pWeightNDT.Enabled = false;
                                pWeight.Enabled = false;
                            }
                            //pDepositNDT.Visible = false;                            

                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;

                            pCheck.Enabled = false;
                            pCheckNDT.Enabled = false;



                            pDeposit.Enabled = false;
                            pBuyNDT.Enabled = false;

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;



                            pShipHome.Enabled = false;
                            ltr_OrderFee_UserInfo.Visible = false;
                            ltr_AddressReceive.Visible = false;

                            txtOrderWeight.Enabled = true;

                            //pShipHomeNDT.Visible = false;
                        }
                        else if (RoleID == 5)
                        {
                            //ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            //ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));

                            if (o.Status < 5)
                            {
                                ddlStatus.Enabled = false;
                                ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                                ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                btnUpdate.Visible = false;
                                pPackedNDT.Enabled = false;
                                pPacked.Enabled = false;
                                pWeightNDT.Enabled = false;
                                pWeight.Enabled = false;
                            }
                            else if (o.Status >= 5)
                            {
                                ddlStatus.Enabled = true;
                                ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                                ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                                ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                                //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                                //ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                                //ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                                pPackedNDT.Enabled = false;
                                pPacked.Enabled = false;

                                pWeightNDT.Enabled = false;
                                pWeight.Enabled = false;
                            }
                            //if (o.Status >= 7)
                            //{
                            //    ddlStatus.Enabled = false;
                            //    ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            //    ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            //    ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            //    ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //    ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            //    ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            //    btnUpdate.Visible = false;
                            //    pPackedNDT.Enabled = false;
                            //    pPacked.Enabled = false;
                            //    pWeightNDT.Enabled = false;
                            //    pWeight.Enabled = false;
                            //}

                            //pDepositNDT.Visible = false;

                            pCNShipFeeNDT.Enabled = false;



                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;

                            pDeposit.Enabled = false;

                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;

                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;

                            pShipHome.Enabled = false;

                            ltr_OrderFee_UserInfo.Visible = false;
                            ltr_AddressReceive.Visible = false;
                            txtOrderWeight.Enabled = true;
                            txtOrdertransactionCode.Enabled = true;
                            txtOrdertransactionCode2.Enabled = true;
                            txtOrdertransactionCode3.Enabled = true;
                            txtOrdertransactionCode4.Enabled = true;
                            txtOrdertransactionCode5.Enabled = true;
                            txtOrdertransactionCodeWeight.Enabled = true;
                            txtOrdertransactionCodeWeight2.Enabled = true;
                            txtOrdertransactionCodeWeight3.Enabled = true;
                            txtOrdertransactionCodeWeight4.Enabled = true;
                            txtOrdertransactionCodeWeight5.Enabled = true;
                            ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\" >Thêm mã vận đơn</a></div>";
                            //pShipHomeNDT.Visible = false;
                        }
                        else if (RoleID == 0)
                        {
                            pnadminmanager.Visible = true;
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            txtOrdertransactionCode.Enabled = true;
                            txtOrdertransactionCode2.Enabled = true;
                            txtOrdertransactionCode3.Enabled = true;
                            txtOrdertransactionCode4.Enabled = true;
                            txtOrdertransactionCode5.Enabled = true;
                            txtOrdertransactionCodeWeight.Enabled = true;
                            txtOrdertransactionCodeWeight2.Enabled = true;
                            txtOrdertransactionCodeWeight3.Enabled = true;
                            txtOrdertransactionCodeWeight4.Enabled = true;
                            txtOrdertransactionCodeWeight5.Enabled = true;
                            ltraddordercode.Text = "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addordercode()\" >Thêm mã vận đơn</a></div>";
                            txtOrderWeight.Enabled = true;
                            btnThanhtoan.Visible = true;
                            pAmountDeposit.Enabled = true;
                            pDeposit.Enabled = true;
                            chkCheck.Enabled = true;
                            chkPackage.Enabled = true;
                            chkShiphome.Enabled = true;
                            ddlReceivePlace.Enabled = true;
                            ddlShippingType.Enabled = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn pill-btn primary-btn admin-btn full-width\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 2)
                        {
                            pnadminmanager.Visible = true;
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = true;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = true;
                            pCNShipFee.Enabled = true;
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;
                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;
                            pPackedNDT.Enabled = true;
                            pPacked.Enabled = true;
                            pShipHome.Enabled = true;
                            btnUpdate.Visible = true;
                            txtComment.Visible = true;
                            ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            ltrBtnUpdate.Text = "<a href=\"javascript:;\" class=\"btn pill-btn primary-btn admin-btn full-width\" onclick=\"UpdateOrder()\">CẬP NHẬT</a>";
                        }
                        else if (RoleID == 6)
                        {
                            ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            //ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = false;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;
                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;
                            pPackedNDT.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;

                            txtComment.Visible = true;
                            ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        else if (RoleID == 8)
                        {
                            //ddlStatus.Items.Add(new ListItem("Chờ đặt cọc", "0"));
                            //ddlStatus.Items.Add(new ListItem("Hủy đơn hàng", "1"));
                            //ddlStatus.Items.Add(new ListItem("Đã đặt cọc", "2"));
                            //ddlStatus.Items.Add(new ListItem("Đã mua hàng", "5"));
                            //ddlStatus.Items.Add(new ListItem("Đang về kho đích", "6"));
                            //ddlStatus.Items.Add(new ListItem("Đã nhận hàng tại kho đích", "7"));
                            //ddlStatus.Items.Add(new ListItem("Chờ thanh toán", "8"));
                            ddlStatus.Items.Add(new ListItem("Khách đã thanh toán", "9"));
                            ddlStatus.Items.Add(new ListItem("Đã hoàn thành", "10"));
                            ddlStatus.Enabled = true;
                            pDeposit.Enabled = false;
                            pCNShipFeeNDT.Enabled = false;
                            pCNShipFee.Enabled = false;
                            pBuyNDT.Enabled = false;
                            pBuy.Enabled = false;
                            pWeightNDT.Enabled = false;
                            pWeight.Enabled = false;
                            pCheckNDT.Enabled = false;
                            pCheck.Enabled = false;
                            pPackedNDT.Enabled = false;
                            pPacked.Enabled = false;
                            pShipHome.Enabled = false;
                            btnUpdate.Visible = true;

                            txtComment.Visible = true;
                            ddlTypeComment.Visible = true;
                            btnSend.Visible = true;
                            txtOrderWeight.Enabled = false;
                        }
                        int countOc = 1;
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode2) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight2))
                        {
                            hdfoc2.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc2.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode3) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight3))
                        {
                            hdfoc3.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc3.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode4) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight4))
                        {
                            hdfoc4.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc4.Value = "0";
                        }
                        if (!string.IsNullOrEmpty(o.OrderTransactionCode5) || !string.IsNullOrEmpty(o.OrderTransactionCodeWeight5))
                        {
                            hdfoc5.Value = "1";
                            countOc++;
                        }
                        else
                        {
                            hdfoc5.Value = "0";
                        }
                        hdforderamount.Value = countOc.ToString();
                        #endregion
                        int orderType = Convert.ToInt32(o.OrderType);
                        if (orderType == 1)
                        {
                            ltrOrderType.Text = "<span class=\"bg-green\">Đơn hàng thường</span>";
                            pnordertype23.Visible = false;
                            pnOrderType1.Visible = true;
                            pnWechatFee.Visible = false;
                        }
                        else if (orderType == 2)
                        {
                            ltrOrderType.Text = "<span class=\"bg-blue\">Đơn hàng Wechat</span>";
                            //pnordertype23.Visible = true;
                            //pnOrderType1.Visible = false;
                            pnordertype23.Visible = false;
                            pnOrderType1.Visible = true;
                            pnWechatFee.Visible = true;
                        }
                        else if (orderType == 3)
                        {
                            ltrOrderType.Text = "<span class=\"bg-yellow\">Đơn hàng khác</span>";
                            pnordertype23.Visible = true;
                            pnOrderType1.Visible = false;
                            pnOrderType3.Visible = true;
                            pnWechatFee.Visible = false;
                        }
                        else
                        {
                            ltrOrderType.Text = "<span class=\"bg-green\">Đơn hàng thường</span>";
                        }
                        hdfOrderType.Value = orderType.ToString();
                        ltrOrderStatus.Text = PJUtils.IntToRequestAdmin(Convert.ToInt32(o.Status));

                        #region Lấy thông tin nhân viên
                        ddlSaler.SelectedValue = o.SalerID.ToString();
                        ddlDatHang.SelectedValue = o.DathangID.ToString();
                        ddlKhoTQ.SelectedValue = o.KhoTQID.ToString();
                        ddlKhoVN.SelectedValue = o.KhoVNID.ToString();
                        #endregion
                        #region Lấy thông tin người đặt
                        var usercreate = AccountController.GetByID(Convert.ToInt32(o.UID));
                        double ckFeeBuyPro = 0;
                        double ckFeeWeight = 0;
                        if (usercreate != null)
                        {
                            ckFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeBuyPro.ToString());
                            ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());

                            lblCKFeebuypro.Text = ckFeeBuyPro.ToString();
                            lblCKFeeWeight.Text = ckFeeWeight.ToString();

                            hdfFeeBuyProDiscount.Value = ckFeeBuyPro.ToString();
                            hdfFeeWeightDiscount.Value = ckFeeWeight.ToString();
                        }
                        else
                        {
                            lblCKFeebuypro.Text = "0";
                            lblCKFeeWeight.Text = "0";
                            hdfFeeBuyProDiscount.Value = "0";
                            hdfFeeWeightDiscount.Value = "0";
                        }

                        //ltr_OrderFee_UserInfo.Text += "<div class=\"order-panel\">";
                        //ltr_OrderFee_UserInfo.Text += " <div class=\"title\">Thông tin đơn hàng</div>";
                        //ltr_OrderFee_UserInfo.Text += " <div class=\"cont\">";
                        //ltr_OrderFee_UserInfo.Text += "     <dl>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequest(Convert.ToInt32(o.Status)) + "</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí ship Trung Quốc</dt><dd>0 vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí mua hàng</dt><dd>0 vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí cân nặng</dt><dd>0 vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí kiểm đếm</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsCheckProductPrice)) + " vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí đóng gỗ</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsPackedPrice)) + " vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí ship giao hàng tận nhà</dt><dd>0 vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Phí đơn hàng hỏa tốc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice)) + " vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "         <dt>Tổng tiền</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)) + " vnđ</dd>";
                        //ltr_OrderFee_UserInfo.Text += "     </dl>";
                        //ltr_OrderFee_UserInfo.Text += " </div>";
                        //ltr_OrderFee_UserInfo.Text += "</div>";
                        if (RoleID != 8)
                        {
                            ltr_OrderFee_UserInfo.Text += "<div class=\"order-panel print1\">";
                            ltr_OrderFee_UserInfo.Text += " <div class=\"title\">Thông tin người đặt hàng</div>";
                            ltr_OrderFee_UserInfo.Text += "     <div class=\"cont\">";
                            var ui = AccountInfoController.GetByUserID(Convert.ToInt32(o.UID));
                            if (ui != null)
                            {
                                string phone = ui.MobilePhonePrefix + ui.MobilePhone;
                                ltr_OrderFee_UserInfo.Text += "         <p>" + ui.FirstName + " " + ui.LastName + "</p>";
                                ltr_OrderFee_UserInfo.Text += "         <p>" + ui.Address + "</p>";
                                ltr_OrderFee_UserInfo.Text += "         <p>" + ui.Email + "</p>";
                                ltr_OrderFee_UserInfo.Text += "         <p class=\"\"><a href=\"tel:+" + phone + "\" class=\"hl-txt2\">" + phone + "</a></p>";
                            }

                            ltr_OrderFee_UserInfo.Text += "     </div>";
                            ltr_OrderFee_UserInfo.Text += "</div>";
                        }

                        ltr_OrderCode.Text += "<div class=\"order-panel\">";
                        ltr_OrderCode.Text += " <div class=\"title\">Mã đơn hàng</div>";
                        ltr_OrderCode.Text += "     <div class=\"cont\">";
                        ltr_OrderCode.Text += "         <p><strong>" + o.ID + "</strong></p>";
                        ltr_OrderCode.Text += "     </div>";
                        ltr_OrderCode.Text += "</div>";

                        ltr_OrderFee_UserInfo1.Text += "<div class=\"order-panel\">";
                        ltr_OrderFee_UserInfo1.Text += " <div class=\"title\">Khách hàng</div>";
                        ltr_OrderFee_UserInfo1.Text += "     <div class=\"cont\">";
                        var use = AccountController.GetByID(Convert.ToInt32(o.UID));
                        if (use != null)
                        {
                            lblOrderID.Text = o.ID.ToString();
                            lblUsername.Text = use.Username;
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>User Đặt hàng:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + use.Username + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>Ghi chú:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + o.Note + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                            //ltr_OrderFee_UserInfo1.Text += "         <p><strong>" + use.Username + "</strong></p>";
                        }

                        ltr_OrderFee_UserInfo1.Text += "     </div>";
                        ltr_OrderFee_UserInfo1.Text += "</div>";
                        ltr_OrderFee_UserInfo1.Text += "<div class=\"order-panel\">";
                        ltr_OrderFee_UserInfo1.Text += " <div class=\"title\">Nhân viên xử lý</div>";
                        ltr_OrderFee_UserInfo1.Text += "     <div class=\"cont\">";
                        var kd = AccountController.GetByID(Convert.ToInt32(o.SalerID));
                        var dathang = AccountController.GetByID(Convert.ToInt32(o.DathangID));
                        var khotq = AccountController.GetByID(Convert.ToInt32(o.KhoTQID));
                        var khovn = AccountController.GetByID(Convert.ToInt32(o.KhoVNID));
                        if (kd != null)
                        {
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>Nhân viên kinh doanh:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + kd.Username + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                        }
                        if (dathang != null)
                        {
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>Nhân viên đặt hàng:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + dathang.Username + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                        }
                        if (khotq != null)
                        {
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>Nhân viên kho TQ:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + khotq.Username + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                        }
                        if (khovn != null)
                        {
                            ltr_OrderFee_UserInfo1.Text += "<dl>";
                            ltr_OrderFee_UserInfo1.Text += "    <dt>Nhân viên kho đích:</dt>";
                            ltr_OrderFee_UserInfo1.Text += "    <dd><strong>" + khovn.Username + "</strong></dd>";
                            ltr_OrderFee_UserInfo1.Text += "</dl>";
                        }

                        ltr_OrderFee_UserInfo1.Text += "     </div>";
                        ltr_OrderFee_UserInfo1.Text += "</div>";


                        #endregion
                        #region Lấy thông tin đơn hàng
                        chkIsCheckNotiPrice.Checked = Convert.ToBoolean(o.IsCheckNotiPrice);
                        //txtMainOrderCode.Text = o.MainOrderCode;
                        int count = 0;
                        hdfListMainOrderCode.Value = o.MainOrderCode;
                        string listmainordercode = o.MainOrderCode;
                        if (!string.IsNullOrEmpty(listmainordercode))
                        {
                            string[] cc = listmainordercode.Split(';');
                            if (cc.Length - 1 > 0)
                            {
                                count = cc.Length - 1;

                                string html = "";
                                for (int i = 0; i < cc.Length - 1; i++)
                                {
                                    string code = cc[i];
                                    html += "<tr class=\"ordercodemain\" data-code=\"" + code + "\">";
                                    html += "<td>" + code + "</td>";
                                    html += "<td>";
                                    html += "<a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteordercode($(this))\">Xóa</a>";
                                    html += "</td>";
                                    html += "</tr>";
                                }
                                ltrListCodeadd.Text = html;
                            }
                        }
                        hdfcount.Value = count.ToString();
                        chkCheck.Checked = o.IsCheckProduct.ToString().ToBool();
                        chkPackage.Checked = o.IsPacked.ToString().ToBool();
                        chkShiphome.Checked = o.IsFastDelivery.ToString().ToBool();
                        //chkIsFast.Checked = o.IsFast.ToString().ToBool();
                        if (o.IsGiaohang != null)
                            chkIsGiaohang.Checked = o.IsGiaohang.ToString().ToBool();
                        if (!string.IsNullOrEmpty(o.AmountDeposit))
                        {
                            double amountdeposit = Math.Floor(Convert.ToDouble(o.AmountDeposit.ToString()));
                            pAmountDeposit.Value = amountdeposit;
                            //lblAmountDeposit.Text = string.Format("{0:N0}", amountdeposit) + " ";
                        }
                        else
                        {
                            pAmountDeposit.Value = 0;
                            //lblAmountDeposit.Text = "0 ";
                        }
                        if (!string.IsNullOrEmpty(o.TotalPriceReal))
                            rTotalPriceReal.Value = Convert.ToDouble(o.TotalPriceReal);
                        else
                            rTotalPriceReal.Value = 0;

                        if (!string.IsNullOrEmpty(o.TotalPriceRealCYN))
                            rTotalPriceRealCYN.Value = Convert.ToDouble(o.TotalPriceRealCYN);
                        else
                            rTotalPriceRealCYN.Value = 0;

                        ddlStatus.SelectedValue = o.Status.ToString();
                        if (!string.IsNullOrEmpty(o.Deposit))
                            pDeposit.Value = Math.Floor(Convert.ToDouble(o.Deposit));


                        double realprice = 0;
                        if (!string.IsNullOrEmpty(o.TotalPriceReal))
                            realprice = Convert.ToDouble(o.TotalPriceReal);

                        double remoneyCYN = 0;
                        double remoneyVND = 0;
                        var orders = OrderController.GetByMainOrderID(Convert.ToInt32(id));
                        if (orders.Count > 0)
                        {
                            foreach (var item in orders)
                            {
                                if (item.IsCensorProduct == true)
                                {
                                    remoneyCYN += Convert.ToDouble(item.PriceOfCensorCYN);
                                    remoneyVND += Convert.ToDouble(item.PriceOfCensorVND);
                                }
                            }
                        }

                        rAdditionFeeForSensorProductVND.Value = remoneyVND;
                        rAdditionFeeForSensorProductCYN.Value = remoneyCYN;



                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            pBuy.Value = Convert.ToDouble(o.FeeBuyPro);
                            lblFeeBuyProduct.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeBuyPro));
                        }
                        double feeweight = 0;
                        double feeweightNDT = 0;
                        if (!string.IsNullOrEmpty(o.FeeWeight))
                        {
                            feeweight = Convert.ToDouble(o.FeeWeight);

                        }
                        pWeight.Value = feeweight;
                        feeweightNDT = feeweight / currency;

                        //if (!string.IsNullOrEmpty(o.TQVNWeight))
                        //{
                        //    pWeightNDT.Value = Convert.ToDouble(o.TQVNWeight);
                        //}
                        //else
                        //{
                        //    pWeightNDT.Value = 0;
                        //}
                        pWeightNDT.Value = feeweightNDT;


                        double checkproductprice = Convert.ToDouble(o.IsCheckProductPrice);
                        pCheck.Value = checkproductprice;
                        pCheckNDT.Value = checkproductprice / currency;

                        //if (o.IsCheckProduct == true)
                        //{
                        //    pCheck.Visible = true;
                        //    lblCheck.Visible = false;
                        //    pCheck.Value = Convert.ToDouble(o.IsCheckProductPrice);
                        //    pCheckNDT.Value = Convert.ToDouble(o.IsCheckProductPrice) / currency;
                        //}
                        //else
                        //{
                        //    pCheck.Visible = false;
                        //    lblCheck.Visible = true;
                        //    pCheckNDT.Visible = false;
                        //}

                        double packagedprice = Convert.ToDouble(o.IsPackedPrice);
                        pPacked.Value = packagedprice;
                        pPackedNDT.Value = packagedprice / currency;

                        pShipHome.Value = Convert.ToDouble(o.IsFastDeliveryPrice);

                        //if (o.IsPacked == true)
                        //{
                        //    pPacked.Visible = true;
                        //    lblPacked.Visible = false;
                        //    pPacked.Value = Convert.ToDouble(o.IsPackedPrice);
                        //    pPackedNDT.Value = Convert.ToDouble(o.IsPackedPrice) / currency;
                        //}
                        //else
                        //{
                        //    pPacked.Visible = false;
                        //    lblPacked.Visible = true;
                        //    pPackedNDT.Visible = false;
                        //}

                        //if (o.IsFastDelivery == true)
                        //{
                        //    pShipHome.Visible = true;
                        //    lblShipHome.Visible = false;
                        //    pShipHome.Value = Convert.ToDouble(o.IsFastDeliveryPrice);
                        //}
                        //else
                        //{
                        //    pShipHome.Visible = false;
                        //    lblShipHome.Visible = true;
                        //    //pShipHomeNDT.Visible = false;
                        //}

                        //lblFastPrice.Text = string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice));
                        
                        lblMoneyNotFee.Text = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND));
                        lblTotalMoney.Text = string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + "đ (¥" + string.Format("{0:#.##}", Convert.ToDouble(o.PriceVND) / currency) + ")";
                        double totalFee = Convert.ToDouble(o.IsCheckProductPrice) + Convert.ToDouble(o.IsPackedPrice) +
                           Convert.ToDouble(o.IsFastDeliveryPrice) + Convert.ToDouble(o.IsFastPrice) + remoneyVND;
                        lblAllFee.Text = string.Format("{0:N0}", totalFee);
                        lblFeeTQVN.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeWeight));
                        txtOrdertransactionCode.Text = o.OrderTransactionCode;
                        txtOrdertransactionCode2.Text = o.OrderTransactionCode2;
                        txtOrdertransactionCode3.Text = o.OrderTransactionCode3;
                        txtOrdertransactionCode4.Text = o.OrderTransactionCode4;
                        txtOrdertransactionCode5.Text = o.OrderTransactionCode5;
                        txtOrdertransactionCodeWeight.Text = o.OrderTransactionCodeWeight;
                        txtOrdertransactionCodeWeight2.Text = o.OrderTransactionCodeWeight2;
                        txtOrdertransactionCodeWeight3.Text = o.OrderTransactionCodeWeight3;
                        txtOrdertransactionCodeWeight4.Text = o.OrderTransactionCodeWeight4;
                        txtOrdertransactionCodeWeight5.Text = o.OrderTransactionCodeWeight5;
                        double odweight = 0;
                        if (!string.IsNullOrEmpty(o.OrderWeight))
                            odweight = Convert.ToDouble(o.OrderWeight);
                        txtOrderWeight.Value = odweight;
                        string orderweightfeedc = o.FeeWeightCK;

                        if (!string.IsNullOrEmpty(o.ReceivePlace))
                        {
                            int receivePlace = o.ReceivePlace.ToInt(1);
                            ddlReceivePlace.SelectedValue = receivePlace.ToString();

                            if (receivePlace != 4)
                            {
                                //pnShippingType.Visible = true;
                                ddlShippingType.SelectedValue = o.ShippingType.ToString();
                            }
                        }
                        hdfReceivePlace.Value = o.ReceivePlace;
                        hdfShippingType.Value = o.ShippingType.ToString();




                        if (string.IsNullOrEmpty(orderweightfeedc))
                        {
                            lblCKFeeweightPrice.Text = "0";
                            hdfFeeweightPriceDiscount.Value = "0";
                        }
                        else
                        {
                            lblCKFeeweightPrice.Text = orderweightfeedc;
                            hdfFeeweightPriceDiscount.Value = orderweightfeedc;
                        }
                        //lblCKFeeweightPrice.Text = string.Format("{0:N0}", Convert.ToDouble(orderweightfeedc));
                        double FeewechatCYN = 0;
                        double FeewechatVND = 0;
                        if (o.WeChatFeeCYN.ToFloat(0) > 0)
                            FeewechatCYN = Convert.ToDouble(o.WeChatFeeCYN);
                        if (o.WeChatFeeVND.ToFloat(0) > 0)
                            FeewechatVND = Convert.ToDouble(o.WeChatFeeVND);

                        double alltotal = totalFee + Convert.ToDouble(o.PriceVND) + Convert.ToDouble(o.FeeShipCN) + Convert.ToDouble(o.FeeBuyPro) + Convert.ToDouble(o.FeeShipCNToVN)
                            + Convert.ToDouble(o.FeeWeight) + FeewechatVND;

                        lblAllTotal.Text = string.Format("{0:N0}", alltotal);
                        lblDeposit.Text = string.Format("{0:N0}", Convert.ToDouble(o.Deposit));
                        lblLeftPay.Text = string.Format("{0:N0}", alltotal - Convert.ToDouble(o.Deposit));
                        #endregion
                        #region Lấy thông tin nhận hàng
                        ltr_AddressReceive.Text += "<div class=\"order-panel print2\">";
                        ltr_AddressReceive.Text += "    <div class=\"title\">Thông tin người nhận hàng</div>";
                        ltr_AddressReceive.Text += "    <div class=\"cont\">";
                        ltr_AddressReceive.Text += "        <p>" + o.FullName + "</p>";
                        ltr_AddressReceive.Text += "        <p>" + o.Address + "</p>";
                        ltr_AddressReceive.Text += "        <p>" + o.Email + "</p>";
                        ltr_AddressReceive.Text += "        <p class=\"\"><a href=\"tel:+" + o.Phone + "\" class=\"hl-txt2\">" + o.Phone + "</a></p>";
                        ltr_AddressReceive.Text += "        <p class=\"\">Ghi chú: </p>";
                        ltr_AddressReceive.Text += "        <p class=\"\"><textarea readonly class=\"form-control\">" + o.Note + "</textarea></p>";
                        ltr_AddressReceive.Text += "    </div>";
                        ltr_AddressReceive.Text += "</div>";
                        #endregion
                        #region Lấy sản phẩm
                        List<tbl_Order> lo = new List<tbl_Order>();
                        lo = OrderController.GetByMainOrderID(o.ID);
                        if (lo.Count > 0)
                        {

                            //rpt.DataSource = lo;
                            //rpt.DataBind();
                            int stt = 1;
                            foreach (var item in lo)
                            {
                                double currentcyt = Convert.ToDouble(item.CurrentCNYVN);
                                double price = 0;
                                double pricepromotion = Convert.ToDouble(item.price_promotion);
                                double priceorigin = Convert.ToDouble(item.price_origin);
                                if (pricepromotion > 0)
                                {
                                    if (priceorigin > pricepromotion)
                                    {
                                        price = pricepromotion;
                                    }
                                    else
                                    {
                                        price = priceorigin;
                                    }
                                }
                                else
                                {
                                    price = priceorigin;
                                }
                                double vndprice = price * currentcyt;
                                ltrProducts.Text += "<tr>";
                                //ltrProducts.Text += "<td class=\"pro\">" + item.ID + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + item.shop_id + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + item.shop_name + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + stt + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">";
                                ltrProducts.Text += "   <div class=\"thumb-product\">";
                                ltrProducts.Text += "       <div class=\"pd-img\"><a href=\"" + item.link_origin + "\" target=\"_blank\"><img src=\"" + item.image_origin + "\" alt=\"\"></a></div>";
                                ltrProducts.Text += "       <div class=\"info\"><a href=\"" + item.link_origin + "\" target=\"_blank\">" + item.title_origin + "</a></div>";
                                ltrProducts.Text += "   </div>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + item.property + "</td>";
                                ltrProducts.Text += "<td class=\"qty\">" + item.quantity + "</td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", vndprice) + " vnđ</p></td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", price) + "</p></td>";
                                //int checkroke = PJUtils.CheckRoleShowRosePrice();
                                //if (checkroke == 0)
                                //{
                                //    ltrProducts.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", Convert.ToDouble(item.RealPrice)) + "</p></td>";
                                //    ltrProducts.Text += "<td class=\"price\">";
                                //    ltrProducts.Text += "<p class=\"\">¥" + string.Format("{0:0.##}", price - Convert.ToDouble(item.RealPrice)) + "</p>";
                                //    ltrProducts.Text += "</td>";
                                //}
                                //else
                                //{
                                //    ltrProducts.Text += "<td class=\"price\"><p class=\"\">xxx</p></td>";
                                //    ltrProducts.Text += "<td class=\"price\">";
                                //    ltrProducts.Text += "<p class=\"\">xxx</p>";
                                //    ltrProducts.Text += "</td>";
                                //}
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\" style=\"color:orange;font-weight:bold\">" + item.brand + "</p></td>";
                                if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                else
                                {
                                    if (item.ProductStatus == 2)
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"bg-red\">Hết hàng</p></td>";
                                    else
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                ltrProducts.Text += "<td class=\"price\"><a class=\"btn btn-info btn-sm\" href=\"/Admin/ProductEdit.aspx?id=" + item.ID + "\">Sửa</a></td>";

                                ltrProducts.Text += "</tr>";

                                //Print
                                ltrProductPrint.Text += "<tr>";
                                ltrProductPrint.Text += "<td class=\"pro\">" + item.ID + "</td>";
                                ltrProductPrint.Text += "<td class=\"pro\">";
                                ltrProductPrint.Text += "   <div class=\"thumb-product\">";
                                ltrProductPrint.Text += "       <div class=\"pd-img\"><img src=\"" + item.image_origin + "\" alt=\"\"></div>";
                                ltrProductPrint.Text += "       <div class=\"info\">" + item.title_origin + "</div>";
                                ltrProductPrint.Text += "   </div>";
                                ltrProductPrint.Text += "</td>";
                                ltrProductPrint.Text += "<td class=\"pro\">" + item.property + "</td>";
                                ltrProductPrint.Text += "<td class=\"qty\">" + item.quantity + "</td>";
                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", vndprice) + " vnđ</p></td>";
                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", price) + "</p></td>";
                                ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">" + item.brand + "</p></td>";
                                if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                else
                                {
                                    if (item.ProductStatus == 2)
                                        ltrProductPrint.Text += "<td class=\"price\"><p class=\"bg-red\">Hết hàng</p></td>";
                                    else
                                        ltrProductPrint.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                }
                                ltrProducts.Text += "</tr>";
                                stt++;
                            }
                        }
                        #endregion
                        #region Lấy bình luận
                        var cs = OrderCommentController.GetByOrderID(o.ID);
                        if (cs != null)
                        {
                            if (cs.Count > 0)
                            {
                                foreach (var item in cs)
                                {
                                    string fullname = "";
                                    int role = 0;
                                    var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                                    if (user != null)
                                    {
                                        role = Convert.ToInt32(user.RoleID);
                                        var userinfo = AccountInfoController.GetByUserID(user.ID);
                                        if (userinfo != null)
                                        {
                                            fullname = userinfo.FirstName + " " + userinfo.LastName;
                                        }
                                    }
                                    if (item.Type == 1)
                                        ltr_comment.Text += "<li class=\"item\">";
                                    else
                                        ltr_comment.Text += "<li class=\"item local\">";
                                    ltr_comment.Text += "   <div class=\"item-left\">";
                                    if (role == 1)
                                    {
                                        ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" /></span>";

                                    }
                                    else
                                    {
                                        ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" /></span>";
                                    }
                                    ltr_comment.Text += "   </div>";
                                    ltr_comment.Text += "   <div class=\"item-right\">";
                                    ltr_comment.Text += "       <strong class=\"item-username\">" + fullname + "</strong>";
                                    ltr_comment.Text += "       <span class=\"item-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</span>";
                                    ltr_comment.Text += "       <p class=\"item-comment\">";
                                    ltr_comment.Text += item.Comment;
                                    ltr_comment.Text += "       </p>";
                                    ltr_comment.Text += "   </div>";
                                    ltr_comment.Text += "</li>";
                                }
                            }
                            else
                            {
                                ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                            }
                        }
                        else
                        {
                            ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                        }
                        #endregion
                        #region Lấy danh sách bao nhỏ
                        string orderwebcode = o.MainOrderCode;
                        StringBuilder spsList = new StringBuilder();
                        var smallpackages = SmallPackageController.GetByMainOrderID(id);
                        if (smallpackages.Count > 0)
                        {
                            foreach (var s in smallpackages)
                            {
                                int status = Convert.ToInt32(s.Status);
                                spsList.Append("<div class=\"ordercode order-versionnew\" data-packageID=\"" + s.ID + "\">");
                                spsList.Append("    <div class=\"item-element\">");
                                spsList.Append("        <span>Mã Vận đơn:</span>");
                                spsList.Append("        <input class=\"transactionCode form-control\" value=\"" + s.OrderTransactionCode + "\" type=\"text\" placeholder=\"Mã vận đơn\" />");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\">");
                                spsList.Append("        <span>Cân nặng:</span>");
                                if (RoleID != 0 && RoleID != 2)
                                    spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + s.Weight + "\" type=\"text\" placeholder=\"Cân nặng\" disabled onkeyup=\"gettotalweight2()\" />");
                                else
                                    spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + s.Weight + "\" type=\"text\" placeholder=\"Cân nặng\" onkeyup=\"gettotalweight2()\" />");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\">");
                                spsList.Append("        <span>Trạng thái:</span>");
                                spsList.Append("        <select class=\"transactionCodeStatus form-control\">");
                                if (status == 1)
                                    spsList.Append("            <option value=\"1\" selected>Chưa về kho TQ</option>");
                                else
                                    spsList.Append("            <option value=\"1\">Chưa về kho TQ</option>");
                                if (status == 2)
                                    spsList.Append("            <option value=\"2\" selected>Đã về kho TQ</option>");
                                else
                                    spsList.Append("            <option value=\"2\">Đã về kho TQ</option>");
                                if (status == 3)
                                    spsList.Append("            <option value=\"3\" selected>Đã về kho đích</option>");
                                else
                                    spsList.Append("            <option value=\"3\">Đã về kho đích</option>");
                                if (status == 4)
                                    spsList.Append("            <option value=\"4\" selected>Đã giao khách hàng</option>");
                                else
                                    spsList.Append("            <option value=\"4\">Đã giao khách hàng</option>");
                                spsList.Append("        </select>");
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\">");
                                spsList.Append("        <span>Ghi chú:</span>");
                                spsList.Append("        <input class=\"smallstaffnote form-control\" value=\"" + s.StaffNote + "\" type=\"text\" placeholder=\"Mã vận đơn\" />");
                                spsList.Append("    </div>");
                                string orderwebcode_package = s.OrderWebCode;

                                string listorderwebcode = "<select class=\"transactionCodeOrderCodeString form-control\">";
                                listorderwebcode += "<option value=\"\">--Chọn mã đơn hàng--</option>";
                                if (!string.IsNullOrEmpty(orderwebcode))
                                {
                                    if (orderwebcode.Contains(";"))
                                    {
                                        string[] arr_Code = orderwebcode.Split(';');
                                        if (arr_Code.Length - 1 > 0)
                                        {
                                            for (int k = 0; k < arr_Code.Length - 1; k++)
                                            {
                                                string codeItem = arr_Code[k];
                                                if (codeItem == orderwebcode_package)
                                                {
                                                    listorderwebcode += "<option value=\"" + codeItem + "\" selected>" + codeItem + "</option>";
                                                }
                                                else
                                                {
                                                    listorderwebcode += "<option value=\"" + codeItem + "\">" + codeItem + "</option>";
                                                }
                                            }
                                        }
                                    }
                                }
                                listorderwebcode += "</select>";
                                spsList.Append("    <div class=\"item-element\">");
                                spsList.Append("        <span>Mã đơn hàng:</span>");
                                spsList.Append(listorderwebcode);
                                spsList.Append("    </div>");
                                spsList.Append("    <div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>");
                                spsList.Append("</div>");
                            }
                            ltrCodeList.Text = spsList.ToString();
                        }
                        #endregion
                        #region Lấy Sản phẩm có group lại
                        double totalWeight = 0;
                        List<objProductOfShop> poss = new List<objProductOfShop>();
                        var listShopID = OrderController.GetByMainOrderIDGroupByShopID(o.ID);
                        if (listShopID.Count > 0)
                        {
                            for (int i = 0; i < listShopID.Count; i++)
                            {
                                string shopID = listShopID[i];
                                var productlist = OrderController.GetByMainOrderIDAndShopID(o.ID, shopID);
                                if (productlist.Count > 0)
                                {
                                    objProductOfShop pos = new objProductOfShop();
                                    pos.shopID = shopID;
                                    List<ShopProduct> sps = new List<ShopProduct>();
                                    pos.shopName = productlist[0].shop_name;
                                    double FeeShipVN = 0;


                                    //if (productlist[0].TotalPriceShopCYN != null)
                                    //{
                                    //    if (productlist[0].TotalPriceShopCYN.ToString().ToFloat(0) > 0)
                                    //    {
                                    //        TotalPriceShopCYN = Convert.ToDouble(productlist[0].TotalPriceShopCYN);
                                    //    }
                                    //}
                                    //if (productlist[0].TotalPriceShopVND != null)
                                    //{
                                    //    if (productlist[0].TotalPriceShopVND.ToString().ToFloat(0) > 0)
                                    //    {
                                    //        TotalPriceShopVND = Convert.ToDouble(productlist[0].TotalPriceShopVND);
                                    //    }
                                    //}
                                    //if (productlist[0].TotalShopQuantity != null)
                                    //{
                                    //    if (productlist[0].TotalShopQuantity.ToString().ToFloat(0) > 0)
                                    //    {
                                    //        TotalShopQuantity = Convert.ToDouble(productlist[0].TotalShopQuantity);
                                    //    }
                                    //}

                                    if (productlist[0].FeeShipCN != null)
                                    {
                                        if (!string.IsNullOrEmpty(productlist[0].FeeShipCN))
                                            FeeShipVN = Convert.ToDouble(productlist[0].FeeShipCN);
                                    }

                                    pos.FeeShipVN = FeeShipVN;

                                    double TotalPriceShopCYN = 0;
                                    double TotalPriceShopVND = 0;
                                    double TotalShopQuantity = 0;
                                    foreach (var item in productlist)
                                    {
                                        double currentcyt = Convert.ToDouble(item.CurrentCNYVN);
                                        double price = 0;
                                        double pricepromotion = Convert.ToDouble(item.price_promotion);
                                        double priceorigin = Convert.ToDouble(item.price_origin);
                                        if (pricepromotion > 0)
                                        {
                                            if (priceorigin > pricepromotion)
                                            {
                                                price = pricepromotion;
                                            }
                                            else
                                            {
                                                price = priceorigin;
                                            }
                                        }
                                        else
                                        {
                                            price = priceorigin;
                                        }
                                        double vndprice = price * currentcyt;
                                        double quantity = 0;
                                        if (item.quantity.ToFloat(0) > 0)
                                            quantity = Convert.ToDouble(item.quantity);

                                        TotalPriceShopCYN += price * quantity;
                                        TotalPriceShopVND += vndprice * quantity;
                                        TotalShopQuantity += quantity;

                                        ShopProduct sp = new ShopProduct();
                                        sp.ID = item.ID;
                                        sp.stt = 0;
                                        sp.IMG = item.image_origin;
                                        sp.productName = item.title_origin;
                                        sp.productLink = item.link_origin;
                                        sp.variable = item.property;
                                        sp.quantity = item.quantity;
                                        //sp.priceVND = string.Format("{0:N0}", vndprice);
                                        //sp.priceCYN = "¥" + string.Format("{0:0.##}", price);
                                        sp.priceVND = vndprice.ToString();
                                        sp.priceCYN = price.ToString();
                                        sp.note = item.brand;
                                        bool isCensor = false;
                                        if (item.IsCensorProduct != null)
                                            isCensor = Convert.ToBoolean(item.IsCensorProduct);
                                        sp.isCensorProduct = isCensor;
                                        if (string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                        {
                                            sp.status = "<p class=\"\">Còn hàng</p></td>";
                                        }
                                        else
                                        {
                                            if (item.ProductStatus == 2)
                                                sp.status = "<p class=\"bg-red\">Hết hàng</p>";
                                            else
                                                sp.status = "<p class=\"\">Còn hàng</p>";
                                        }
                                        sps.Add(sp);
                                    }
                                    pos.TotalPriceShopCYN = TotalPriceShopCYN;
                                    pos.TotalPriceShopVND = TotalPriceShopVND;
                                    pos.TotalShopQuantity = TotalShopQuantity;
                                    pos.sp = sps;
                                    List<OrderShopCode> osds = new List<OrderShopCode>();
                                    var ods = OrderShopCodeController.GetByShopIDAndMainOrderID(shopID, o.ID);
                                    if (ods.Count > 0)
                                    {
                                        foreach (var item in ods)
                                        {
                                            OrderShopCode osd = new OrderShopCode();
                                            osd.ordershopCode = item;
                                            List<tbl_SmallPackage> spsnew = new List<tbl_SmallPackage>();
                                            spsnew = SmallPackageController.GetByOrderShopCodeID(item.ID);
                                            osd.smallPackage = spsnew;
                                            osds.Add(osd);
                                        }
                                    }
                                    pos.orderShopCode = osds;
                                    poss.Add(pos);
                                }
                            }
                        }
                        if (poss.Count > 0)
                        {
                            StringBuilder htmlOSD = new StringBuilder();
                            htmlOSD.Append("<table class=\"tb-product\">");
                            htmlOSD.Append("    <tr>");
                            htmlOSD.Append("        <th>Tên Shop</th>");
                            htmlOSD.Append("        <th>Shop ID</th>");
                            htmlOSD.Append("        <th>Sản phẩm</th>");
                            htmlOSD.Append("        <th>Mã đơn hàng</th>");
                            htmlOSD.Append("    </tr>");
                            double fscn = 0;
                            foreach (var item in poss)
                            {
                                htmlOSD.Append("    <tr id=\"" + item.shopID + "\" data-shopid=\"" + item.shopID + "\">");
                                htmlOSD.Append("        <td style=\"width: 5%;\" class=\"middle-center\">" + item.shopName + "</td>");
                                htmlOSD.Append("        <td style=\"width: 5%;\" class=\"middle-center\">" + item.shopID + "");
                                htmlOSD.Append("            <input value=\"" + item.FeeShipVN + "\" oninput=\"updateFeeShipCN()\" type=\"number\" placeholder=\"Phí nội địa\" data-shopid=\"" + item.shopID + "\" data-mainorderid=\"" + o.ID + "\" class=\"feeshipcnshopcode form-control\" min=\"0\">");
                                //htmlOSD.Append("            <br/><br/>Tổng tiền Tệ: <input value=\"" + item.TotalPriceShopCYN + "\" oninput=\"updateTotalprice($(this),'cyn')\" type=\"number\" placeholder=\"Tổng tiền CYN\" data-shopid=\"" + item.shopID + "\" data-mainorderid=\"" + o.ID + "\" class=\"totalpricecynshopcode form-control\" readonly min=\"0\">");
                                //htmlOSD.Append("            <br/><br/>Tổng tiền VNĐ: <input value=\"" + item.TotalPriceShopVND + "\" oninput=\"updateTotalprice($(this),'vnd')\" type=\"number\" placeholder=\"Tổng tiền VNĐ\" data-shopid=\"" + item.shopID + "\" data-mainorderid=\"" + o.ID + "\" class=\"totalpricevndshopcode form-control\" readonly min=\"0\">");
                                //htmlOSD.Append("            <br/><br/>Tổng số lượng: <input value=\"" + item.TotalShopQuantity + "\" type=\"number\" placeholder=\"Tổng tiền số lượng\" data-shopid=\"" + item.shopID + "\" data-mainorderid=\"" + o.ID + "\" class=\"totalquantityshopcode form-control\" readonly min=\"0\">");

                                htmlOSD.Append("            <br/><br/>Tổng tiền Tệ: <strong>¥" + item.TotalPriceShopCYN + "</strong>");
                                htmlOSD.Append("            <br/><br/>Tổng tiền VNĐ: <strong>" + string.Format("{0:N0}", item.TotalPriceShopVND) + " VNĐ</strong>");
                                htmlOSD.Append("            <br/><br/>Tổng số lượng: <strong>" + item.TotalShopQuantity + "</strong>");

                                //htmlOSD.Append("            <input value=\"" + fscn / currency1 + "\" placeholder=\"Tổng phí nội địa\" disabled class=\"totalfeeshipnoidia form-control\">");
                                fscn += Convert.ToDouble(item.FeeShipVN);
                                htmlOSD.Append("            <a href=\"javascript:;\" class=\"btn-not-radius\" style=\"margin: 0; margin-bottom: 20px; width: 100%; margin-top: 10px;\" onclick=\"callpopupaddordershopcode('" + item.shopID + "','" + PJUtils.removeSpecialCharacter(item.shopName) + "','" + o.ID + "')\">Thêm mã đơn</a>");
                                htmlOSD.Append("        </td>");
                                htmlOSD.Append("        <td style=\"width: 40%\">");
                                htmlOSD.Append("            <table class=\"tb-product\">");
                                htmlOSD.Append("                <tbody>");
                                htmlOSD.Append("                    <tr>");
                                htmlOSD.Append("                        <th class=\"pro\">ID</th>");
                                htmlOSD.Append("                        <th class=\"pro\">Sản phẩm</th>");
                                htmlOSD.Append("                        <th class=\"pro\">Thuộc tính</th>");
                                htmlOSD.Append("                        <th class=\"qty\">Số lượng</th>");
                                htmlOSD.Append("                        <th class=\"price\">Đơn giá</th>");
                                htmlOSD.Append("                        <th class=\"price\">Tổng giá VNĐ</th>");
                                htmlOSD.Append("                        <th class=\"price\">Giá sản phẩm CNY</th>");
                                htmlOSD.Append("                        <th class=\"price\">Tổng giá CNY</th>");
                                htmlOSD.Append("                        <th class=\"price\">Ghi chú riêng sản phẩm</th>");
                                htmlOSD.Append("                        <th class=\"price\">Trạng thái</th>");
                                htmlOSD.Append("                        <th class=\"price\">Hàng đặc biệt</th>");
                                htmlOSD.Append("                        <th class=\"tool\"></th>");
                                htmlOSD.Append("                    </tr>");
                                var products = item.sp;
                                if (products.Count > 0)
                                {
                                    foreach (var p in products)
                                    {
                                        htmlOSD.Append("                    <tr data-id=\"" + p.ID + "\">");
                                        htmlOSD.Append("                        <td class=\"pro\">" + p.ID + "</td>");
                                        htmlOSD.Append("                        <td class=\"pro\">");
                                        htmlOSD.Append("                            <div class=\"thumb-product\">");
                                        htmlOSD.Append("                                <div class=\"pd-img\">");
                                        htmlOSD.Append("                                    <a href=\"" + p.productLink + "\" target=\"_blank\">");
                                        htmlOSD.Append("                                        <img src=\"" + p.IMG + "\" alt=\"\"></a>");
                                        htmlOSD.Append("                                </div>");
                                        htmlOSD.Append("                                <div class=\"info\"><a href=\"" + p.productLink + "\" target=\"_blank\">" + p.productName + "</a></div>");
                                        htmlOSD.Append("                            </div>");
                                        htmlOSD.Append("                        </td>");
                                        htmlOSD.Append("                        <td class=\"pro\">" + p.variable + "</td>");
                                        htmlOSD.Append("                        <td class=\"qty\">" + p.quantity + "</td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", Convert.ToDouble(p.priceVND)) + " vnđ</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", Convert.ToDouble(p.priceVND) * Convert.ToDouble(p.quantity)) + " vnđ</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.priceCYN + "</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + Convert.ToDouble(p.priceCYN) * Convert.ToDouble(p.quantity) + "</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\" style=\"color: orange; font-weight: bold\">" + p.note + "</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.status + "</p></td>");
                                        if (p.isCensorProduct == true)
                                            htmlOSD.Append("                        <td class=\"price\"><input type=\"checkbox\" onchange=\"countFeeAdd($(this))\" checked></td>");
                                        else
                                            htmlOSD.Append("                        <td class=\"price\"><input type=\"checkbox\" onchange=\"countFeeAdd($(this))\"></td>");

                                        htmlOSD.Append("                        <td class=\"price\"><a class=\"btn btn-info btn-sm\" href=\"/Admin/ProductEdit.aspx?id=" + p.ID + "\">Sửa</a></td>");
                                        htmlOSD.Append("                    </tr>");
                                    }
                                }

                                htmlOSD.Append("                </tbody>");
                                htmlOSD.Append("            </table>");
                                htmlOSD.Append("        </td>");
                                htmlOSD.Append("        <td style=\"width: 20%\">");
                                htmlOSD.Append("            <table class=\"tb-product\" style=\"width: 100%\">");
                                htmlOSD.Append("                <thead>");
                                htmlOSD.Append("                    <tr>");
                                htmlOSD.Append("                        <th>Mã đơn hàng</th>");
                                htmlOSD.Append("                        <th>Mã vận đơn</th>");
                                htmlOSD.Append("                    </tr>");
                                htmlOSD.Append("                </thead>");
                                htmlOSD.Append("                <tbody class=\"shopOrderList\">");
                                var ordershopcode_group = item.orderShopCode;
                                if (ordershopcode_group.Count > 0)
                                {
                                    foreach (var osc in ordershopcode_group)
                                    {
                                        var ordershopcodes = osc.ordershopCode;
                                        htmlOSD.Append("                    <tr class=\"shoporder-item\" data-shopid=\"" + item.shopID + "\" data-shopname=\"" + item.shopName
                                                                                + "\" data-mainorderid=\"" + o.ID + "\" data-ordershopcode=\"" + ordershopcodes.OrderShopCode
                                                                                + "\"  data-ordershopcodeID=\"" + ordershopcodes.ID + "\">");
                                        htmlOSD.Append("                        <td>");
                                        htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link ordershopcode-atag\" onclick=\"showordershopcodedetail($(this))\">" + ordershopcodes.OrderShopCode + "</a>");
                                        htmlOSD.Append("                            <a href=\"javascript:;\" class=\"btn-not-radius\" style=\"clear: both; width: 100%; text-align: center\" onclick=\"callpopupaddsmallpackage($(this))\">Thêm mã vận đơn</a>");
                                        htmlOSD.Append("                        </td>");
                                        htmlOSD.Append("                        <td class=\"smallpackage-list-of-odsc\">");
                                        var smlpacks = osc.smallPackage;
                                        if (smlpacks.Count > 0)
                                        {
                                            foreach (var smlo in smlpacks)
                                            {
                                                if (smlo.Status == 1)
                                                    htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-red\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 2)
                                                    htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-orange\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 3)
                                                    htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-green\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 4)
                                                    htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-blue\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\">" + smlo.OrderTransactionCode + "</a>");

                                                double weight = 0;
                                                if (smlo.Weight != null)
                                                {
                                                    weight = Convert.ToDouble(smlo.Weight);
                                                }
                                                totalWeight += weight;
                                            }
                                        }
                                        htmlOSD.Append("                        </td>");
                                        htmlOSD.Append("                    </tr>");
                                    }
                                }
                                htmlOSD.Append("                </tbody>");
                                htmlOSD.Append("            </table>");
                                htmlOSD.Append("        </td>");
                                htmlOSD.Append("    </tr>");
                            }
                            htmlOSD.Append("</table>");

                            pCNShipFeeNDT.Value = fscn;
                            double feeshiptqvnd = fscn * currency1;
                            pCNShipFee.Value = feeshiptqvnd;
                            MainOrderController.UpdateFeeShipTQVN(o.ID, feeshiptqvnd.ToString());

                            lblShipTQ.Text = string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN));
                            double tot = Convert.ToDouble(o.PriceVND) + fscn - realprice;
                            double totCYN = tot / currency1;
                            pHHCYN.Value = totCYN;
                            pHHVND.Value = tot;
                            double wechatFeeCYN = 0;
                            double wechatFeeVND = 0;
                            if (o.WeChatFeeCYN.ToFloat(0) > 0)
                                wechatFeeCYN = Convert.ToDouble(o.WeChatFeeCYN);
                            if (o.WeChatFeeVND.ToFloat(0) > 0)
                                wechatFeeVND = Convert.ToDouble(o.WeChatFeeVND);

                            rFeeWeChatCYN.Value = wechatFeeCYN;
                            rFeeWeChatVND.Value = wechatFeeVND;
                            if (o.OrderType == 2)
                            {
                                ltrFeeWechat.Text += "<dt>Phí mua hàng wechat</dt>";
                                ltrFeeWechat.Text += "<dd>" + string.Format("{0:N0}", wechatFeeVND) + " vnđ</dd>";
                            }


                            StringBuilder htmlOSD1 = new StringBuilder();
                            htmlOSD1.Append("<table class=\"tb-product\">");
                            htmlOSD1.Append("    <tr>");
                            htmlOSD1.Append("        <th>Sản phẩm</th>");
                            htmlOSD1.Append("        <th>Mã đơn hàng</th>");
                            htmlOSD1.Append("    </tr>");
                            foreach (var item in poss)
                            {
                                htmlOSD1.Append("    <tr id=\"" + item.shopID + "\" data-shopid=\"" + item.shopID + "\">");
                                htmlOSD1.Append("        <td style=\"width: 60%\">");
                                htmlOSD1.Append("            <table class=\"tb-product\">");
                                htmlOSD1.Append("                <tbody>");
                                htmlOSD1.Append("                    <tr>");
                                htmlOSD1.Append("                        <th class=\"pro\">ID</th>");
                                htmlOSD1.Append("                        <th class=\"pro\" style=\"width:50%\">Sản phẩm</th>");
                                htmlOSD1.Append("                        <th class=\"pro\">Thuộc tính</th>");
                                htmlOSD1.Append("                        <th class=\"qty\">Số lượng</th>");
                                htmlOSD1.Append("                        <th class=\"price\">Đơn giá</th>");
                                htmlOSD1.Append("                        <th class=\"price\">Giá sản phẩm CNY</th>");
                                htmlOSD1.Append("                        <th class=\"price\">Ghi chú riêng sản phẩm</th>");
                                htmlOSD1.Append("                        <th class=\"price\">Trạng thái</th>");
                                htmlOSD1.Append("                        <th class=\"price\">Hàng đặc biệt</th>");
                                htmlOSD1.Append("                    </tr>");
                                var products = item.sp;
                                if (products.Count > 0)
                                {
                                    foreach (var p in products)
                                    {
                                        htmlOSD1.Append("                    <tr>");
                                        htmlOSD1.Append("                        <td class=\"pro\">" + p.ID + "</td>");
                                        htmlOSD1.Append("                        <td class=\"pro\">");
                                        htmlOSD1.Append("                            <div class=\"thumb-product\">");
                                        htmlOSD1.Append("                                <div class=\"pd-img\">");
                                        //htmlOSD1.Append("                                    <a href=\"" + p.productLink + "\" target=\"_blank\"></a>");
                                        htmlOSD1.Append("                                        <img src=\"" + p.IMG + "\" alt=\"\">");
                                        htmlOSD1.Append("                                </div>");
                                        //htmlOSD1.Append("                                <div class=\"info\"><a href=\"" + p.productLink + "\" target=\"_blank\">" + p.productName + "</a></div>");
                                        htmlOSD1.Append("                                <div class=\"info\">" + p.productName + "</div>");
                                        htmlOSD1.Append("                            </div>");
                                        htmlOSD1.Append("                        </td>");
                                        htmlOSD1.Append("                        <td class=\"pro\">" + p.variable + "</td>");
                                        htmlOSD1.Append("                        <td class=\"qty\">" + p.quantity + "</td>");
                                        htmlOSD1.Append("                        <td class=\"price\"><p class=\"\">" + p.priceVND + " vnđ</p></td>");
                                        htmlOSD1.Append("                        <td class=\"price\"><p class=\"\">" + p.priceCYN + "</p></td>");
                                        htmlOSD1.Append("                        <td class=\"price\"><p class=\"\" style=\"color: orange; font-weight: bold\">" + p.note + "</p></td>");
                                        htmlOSD1.Append("                        <td class=\"price\"><p class=\"\">" + p.status + "</p></td>");
                                        if (p.isCensorProduct == true)
                                            htmlOSD1.Append("                        <td class=\"price\"><input type=\"checkbox\" onchange=\"countFeeAdd($this)\" checked></td>");
                                        else
                                            htmlOSD1.Append("                        <td class=\"price\"><input type=\"checkbox\" onchange=\"countFeeAdd($this)\"></td>");
                                        htmlOSD1.Append("                    </tr>");
                                    }
                                }
                                htmlOSD1.Append("                </tbody>");
                                htmlOSD1.Append("            </table>");
                                htmlOSD1.Append("        </td>");
                                htmlOSD1.Append("        <td style=\"width: 20%\">");
                                htmlOSD1.Append("            <table class=\"tb-product\" style=\"width: 100%\">");
                                htmlOSD1.Append("                <thead>");
                                htmlOSD1.Append("                    <tr>");
                                htmlOSD1.Append("                        <th>Mã đơn hàng</th>");
                                htmlOSD1.Append("                        <th>Mã vận đơn</th>");
                                htmlOSD1.Append("                    </tr>");
                                htmlOSD1.Append("                </thead>");
                                htmlOSD1.Append("                <tbody class=\"shopOrderList\">");
                                var ordershopcode_group = item.orderShopCode;
                                if (ordershopcode_group.Count > 0)
                                {
                                    foreach (var osc in ordershopcode_group)
                                    {
                                        var ordershopcodes = osc.ordershopCode;
                                        htmlOSD1.Append("                    <tr class=\"shoporder-item\" data-shopid=\"" + item.shopID + "\" data-shopname=\"" + item.shopName
                                                                                + "\" data-mainorderid=\"" + o.ID + "\" data-ordershopcode=\"" + ordershopcodes.OrderShopCode
                                                                                + "\"  data-ordershopcodeID=\"" + ordershopcodes.ID + "\">");
                                        htmlOSD1.Append("                        <td>");
                                        htmlOSD1.Append("                            <a href=\"javascript:;\" class=\"custom-link ordershopcode-atag\" onclick=\"showordershopcodedetail($(this))\" style=\"color:#000\">" + ordershopcodes.OrderShopCode + "</a>");
                                        htmlOSD1.Append("                        </td>");
                                        htmlOSD1.Append("                        <td class=\"smallpackage-list-of-odsc\">");
                                        var smlpacks = osc.smallPackage;
                                        if (smlpacks.Count > 0)
                                        {
                                            foreach (var smlo in smlpacks)
                                            {
                                                if (smlo.Status == 1)
                                                    htmlOSD1.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-red\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\" style=\"color:#000\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 2)
                                                    htmlOSD1.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-orange\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\" style=\"color:#000\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 3)
                                                    htmlOSD1.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-green\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\" style=\"color:#000\">" + smlo.OrderTransactionCode + "</a>");
                                                else if (smlo.Status == 4)
                                                    htmlOSD1.Append("                            <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-blue\" data-smallpackageid=\"" + smlo.ID + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackageweight=\"" + Math.Round(Convert.ToDouble(smlo.Weight), 1) + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"editsmallpackage($(this))\" style=\"color:#000\">" + smlo.OrderTransactionCode + "</a>");

                                            }
                                        }
                                        htmlOSD1.Append("                        </td>");
                                        htmlOSD1.Append("                    </tr>");
                                    }
                                }
                                htmlOSD1.Append("                </tbody>");
                                htmlOSD1.Append("            </table>");
                                htmlOSD1.Append("        </td>");
                                htmlOSD1.Append("    </tr>");
                            }
                            htmlOSD1.Append("</table>");
                            ltrProductOrderShopCode.Text = htmlOSD.ToString();
                            ltrProductOrderShopCode1.Text = htmlOSD1.ToString();
                        }
                        ltrTotalWeight.Text = totalWeight.ToString();
                        #endregion
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            #region History 
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    var historyorder = HistoryOrderChangeController.GetByMainOrderID(o.ID);
                    if (historyorder.Count > 0)
                    {
                        List<historyCustom> hc = new List<historyCustom>();
                        foreach (var item in historyorder)
                        {
                            string username = item.Username;
                            string rolename = "admin";
                            var acc = AccountController.GetByUsername(username);
                            if (acc != null)
                            {
                                int role = Convert.ToInt32(acc.RoleID);

                                var r = RoleController.GetByID(role);
                                if (r != null)
                                {
                                    rolename = r.RoleDescription;
                                }
                            }
                            historyCustom h = new historyCustom();
                            h.ID = item.ID;
                            h.Username = username;
                            h.RoleName = rolename;
                            h.Date = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);
                            h.Content = item.HistoryContent;
                            hc.Add(h);
                        }
                        gr.DataSource = hc;
                    }
                }
            }

            #endregion
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        #region Button
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                var id = Convert.ToInt32(ViewState["ID"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        int type = ddlTypeComment.SelectedValue.ToString().ToInt();
                        if (type > 0)
                        {
                            string kq = OrderCommentController.Insert(id, txtComment.Text, true, type, DateTime.Now, uid);
                            if (type == 1)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                    1, currentDate, obj_user.Username);
                                try
                                {
                                    PJUtils.SendMailGmail("no-reply@mona-media.com", "fufgtsqnrjnxyuhf", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                        "Thông báo tại 1688 Express.", "Đã có đánh giá mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                }
                                catch { }
                            }
                            if (Convert.ToInt32(kq) > 0)
                            {
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");
                                PJUtils.ShowMsg("Gửi đánh giá thành công.", true, Page);
                            }
                        }
                        else
                        {
                            PJUtils.ShowMessageBoxSwAlert("Vui lòng chọn khu vực", "e", false, Page);
                        }
                    }
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                int RoleID = obj_user.RoleID.ToString().ToInt();
                //var id = Convert.ToInt32(Request.QueryString["id"]);
                var id = Convert.ToInt32(ViewState["ID"]);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByID(id);
                    if (o != null)
                    {
                        var config1 = ConfigurationController.GetByTop1();
                        double currency = 0;
                        if (config1 != null)
                        {
                            currency = Convert.ToDouble(config1.Currency);
                        }

                        string CurrentOrderTransactionCode = o.OrderTransactionCode;
                        string CurrentOrderTransactionCode2 = o.OrderTransactionCode2;
                        string CurrentOrderTransactionCode3 = o.OrderTransactionCode3;
                        string CurrentOrderTransactionCode4 = o.OrderTransactionCode4;
                        string CurrentOrderTransactionCode5 = o.OrderTransactionCode5;
                        string CurrentOrderTransactionCodeWeight = o.OrderTransactionCodeWeight;
                        string CurrentOrderTransactionCodeWeight2 = o.OrderTransactionCodeWeight2;
                        string CurrentOrderTransactionCodeWeight3 = o.OrderTransactionCodeWeight3;
                        string CurrentOrderTransactionCodeWeight4 = o.OrderTransactionCodeWeight4;
                        string CurrentOrderTransactionCodeWeight5 = o.OrderTransactionCodeWeight5;

                        string CurrentOrderWeight = o.OrderWeight;

                        #region cập nhật và tạo mới smallpackage
                        string tcl = hdfCodeTransactionList.Value;
                        if (!string.IsNullOrEmpty(tcl))
                        {
                            string[] list = tcl.Split('|');
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                string[] item = list[i].Split(',');
                                int ID = item[0].ToInt(0);
                                string code = item[1].Trim();
                                string weight = item[2];
                                int smallpackage_status = item[3].ToInt(1);
                                string webordercode = item[4];
                                if (ID > 0)
                                {
                                    var smp = SmallPackageController.GetByID(ID);
                                    if (smp != null)
                                    {
                                        int bigpackageID = Convert.ToInt32(smp.BigPackageID);
                                        SmallPackageController.UpdateOrderWebCode(ID, bigpackageID, code, smp.ProductType, Convert.ToDouble(smp.FeeShip),
                                            Convert.ToDouble(weight), Convert.ToDouble(smp.Volume), smallpackage_status, webordercode, currentDate, username);
                                        var bigpack = BigPackageController.GetByID(bigpackageID);
                                        if (bigpack != null)
                                        {
                                            int TotalPackageWaiting = SmallPackageController.GetCountByBigPackageIDStatus(bigpackageID, 1, 2);
                                            if (TotalPackageWaiting == 0)
                                            {
                                                BigPackageController.UpdateStatus(bigpackageID, 2, currentDate, username);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SmallPackageController.InsertWithOrderWebCode(id, 0, code, "", 0, Convert.ToDouble(weight), 0, smallpackage_status, webordercode, currentDate, username);
                                    }
                                }
                                else
                                {
                                    SmallPackageController.InsertWithOrderWebCode(id, 0, code, "", 0, Convert.ToDouble(weight), 0, smallpackage_status, webordercode, currentDate, username);
                                }
                            }
                        }
                        #endregion
                        #region cập nhật fee nội địa
                        double feeShipCN = 0;
                        string listSFee = hdfUpdateFeeShipVN.Value;
                        if (!string.IsNullOrEmpty(listSFee))
                        {
                            string[] SFees = listSFee.Split('|');
                            if (SFees.Length - 1 > 0)
                            {
                                for (int i = 0; i < SFees.Length - 1; i++)
                                {
                                    string[] item = SFees[i].Split(',');
                                    string price = item[0];
                                    double TotalPriceShopCYN = 0;
                                    double TotalPriceShopVND = 0;
                                    double TotalShopQuantity = 0;
                                    //if (item[3].ToFloat(0) > 0)
                                    //    TotalPriceShopCYN = Convert.ToDouble(item[3]);
                                    //if (item[4].ToFloat(0) > 0)
                                    //    TotalPriceShopVND = Convert.ToDouble(item[4]);
                                    //if (item[5].ToFloat(0) > 0)
                                    //    TotalShopQuantity = Convert.ToDouble(item[5]);

                                    int mID = item[1].ToInt(0);
                                    string shopID = item[2];
                                    feeShipCN += Convert.ToDouble(price);

                                    OrderController.UpdateFeeShipCNByShopIDAndMainOrderID(mID, shopID, price);
                                    OrderController.Update3TotalByShopIDAndMainOrderID(mID, shopID,
                                        TotalPriceShopCYN, TotalPriceShopVND, TotalShopQuantity);
                                }
                            }
                        }
                        #endregion
                        #region Lấy ra text của trạng thái đơn hàng
                        string orderstatus = "";
                        int currentOrderStatus = Convert.ToInt32(o.Status);
                        switch (currentOrderStatus)
                        {
                            case 0:
                                orderstatus = "Chờ đặt cọc";
                                break;
                            case 1:
                                orderstatus = "Hủy đơn hàng";
                                break;
                            case 2:
                                orderstatus = "Đã đặt cọc";
                                break;
                            case 5:
                                orderstatus = "Đã mua hàng";
                                break;
                            case 6:
                                orderstatus = "Đang về kho đích";
                                break;
                            case 7:
                                orderstatus = "Đã nhận hàng tại kho đích";
                                break;
                            case 8:
                                orderstatus = "Chờ thanh toán";
                                break;
                            case 9:
                                orderstatus = "Khách đã thanh toán";
                                break;
                            case 10:
                                orderstatus = "Đã hoàn thành";
                                break;
                            default:
                                break;
                        }
                        #endregion
                        #region Cập nhật nhân viên KhoTQ và nhân viên KhoVN
                        if (RoleID == 4)
                        {
                            if (o.KhoTQID == uid || o.KhoTQID == 0)
                            {
                                MainOrderController.UpdateStaff(o.ID, o.SalerID.ToString().ToInt(0), o.DathangID.ToString().ToInt(0), uid, o.KhoVNID.ToString().ToInt(0));
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
                            }
                        }
                        else if (RoleID == 5)
                        {
                            if (o.KhoVNID == uid || o.KhoTQID == 0)
                            {
                                MainOrderController.UpdateStaff(o.ID, o.SalerID.ToString().ToInt(0), o.DathangID.ToString().ToInt(0), o.KhoTQID.ToString().ToInt(0), uid);
                            }
                            else
                            {
                                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình xử lý", "e", true, Page);
                            }
                        }
                        #endregion
                        #region cập nhật thông tin của đơn hàng
                        int status = ddlStatus.SelectedValue.ToString().ToInt();
                        if (status == 1)
                        {
                            MainOrderController.UpdateStatusByID(o.ID, Convert.ToInt32(ddlStatus.SelectedValue));
                            double Deposit = 0;
                            if (!string.IsNullOrEmpty(o.Deposit))
                                Deposit = Convert.ToDouble(o.Deposit);
                            if (Deposit > 0)
                            {
                                var user_order = AccountController.GetByID(o.UID.ToString().ToInt());
                                if (user_order != null)
                                {
                                    double wallet = 0;
                                    if (user_order.Wallet != null)
                                        wallet = Convert.ToDouble(user_order.Wallet);
                                    wallet = wallet + Deposit;
                                    HistoryPayWalletController.Insert(user_order.ID, user_order.Username, o.ID, Deposit,
                                        "Đơn hàng: " + o.ID + " bị hủy và hoàn tiền cọc cho khách.", wallet, 2, 2, currentDate, obj_user.Username);
                                    //HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                    //    " đã đổi trạng thái của đơn hàng: " + o.ID + " từ " + orderstatus + " sang " + ddlStatus.SelectedItem + "", 0, currentDate);
                                    AccountController.updateWallet(user_order.ID, wallet, currentDate, obj_user.Username);
                                }
                            }

                        }
                        else
                        {
                            if (status == 5)
                            {
                                try
                                {
                                    var user_order = AccountController.GetByID(o.UID.ToString().ToInt());
                                    if (user_order != null)
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "Đơn hàng của Qúy khách đã được đặt hàng và thanh toán thành công. Qúy khách vui lòng kiểm tra lại tất cả thông tin trên đơn hàng. Nếu có bất cứ sai sót gì từ phía nhân viên đặt hàng, Qúy khách vui lòng gọi điện ngay đến hotline: <strong>024.6326.5589</strong> – <strong>091.458.1688</strong> ( mở máy nhận cuộc gọi từ 8h30-17h30 từ thứ 2- thứ 7).<br/><br/>";
                                        message += "Nếu <span style=\"font-weight:bold; color:#0070c0\">1688Express</span> không nhận được phản hồi gì, đơn đặt hàng sẽ được chốt và căn cứ để trả hàng cho Qúy khách. Sau khi đơn hàng được chốt, <span style=\"font-weight:bold; color:#0070c0\">1688Express</span> sẽ không tiếp nhận thêm bất cứ thông tin sửa chữa nào.<br/><br/>";
                                        message += "Để kiểm tra tình trạng đơn hàng, Qúy khách xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-kiem-tra-trang-thai-don-hang\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a>.<br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", user_order.Email,
                                            "Đơn hàng được mua thành công tại 1688 Express", message, "");
                                    }

                                }
                                catch
                                {

                                }
                            }
                            else if (status == 7)
                            {
                                try
                                {
                                    var user_order = AccountController.GetByID(o.UID.ToString().ToInt());
                                    if (user_order != null)
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "Đơn hàng của Qúy khách đã có kiện hàng vê đến kho Hà Nội.<br/><br/>";
                                        message += "Để nhận hàng, quý khách vui lòng thanh toán số tiền còn thiếu bằng cách nạp tiền vào tài khoản và gửi yêu cầu xuất kho ( xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" style=\"text-decoration:underline:\" target=\"_blank\">nạp tiền</a> và <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-gui-yeu-cau-xuat-kho\" style=\"text-decoration:underline:\" target=\"_blank\">gửi yêu cầu xuất kho</a> ).<br/><br/>";
                                        message += "Lưu ý: Sau 3 ngày kể từ khi đơn hàng về đủ, nếu khách hàng không nhận hàng, mọi yêu cầu xử lý khiếu nại được chấp nhận giải quyết là rất thấp do quá thời hạn xử lý của shop bán. Vì vậy 1688Express mong quý khách sắp xếp nhận hàng sớm, đặc biệt đối với khách hàng sử dụng dịch vụ chuyển phát chậm.<br/><br/>";
                                        message += "- <strong>Khách ở HN</strong> : đến trực tiếp Vp 1688Express để lấy hàng , hoặc 1688Express sẽ hỗ trợ tìm Shipper chuyển hàng đến tận nơi cho khách <br/><i style=\"font-style: italic;\">( khách hàng thanh toán phí vận chuyển với shipper khi nhận hàng)</i><br/><br/>";
                                        message += "<span style=\"width:100%;text-align:center;font-style:italic\"><span style=\"color:red\">THỜI GIAN NHẬN HÀNG TẠI VĂN PHÒNG HÀ NỘI:</span><br/>"
                                                + ">>> Sáng : Từ 9h-12h từ thứ 2 đến thứ 7<br/>"
                                                + ">>> Chiều : Từ 14h-17h  từ thứ 2 đến thứ 7<br/>"
                                                + "  </span><br/><br/>";
                                        message += "- <strong>Khách ở khu vực khác</strong> : 1688Express sẽ chuyển hàng đến tận nơi cho khách  thông qua Viettel post hoặc hình thức vận chuyển khách hàng đề xuất ( <span style=\"color:red\">XEM HƯỚNG DẪN CHI TIẾT BÊN DƯỚI</span>)<br/><br/>";
                                        message += "<span style=\"font-weight:bold; color:#0070c0\">>>> CHUYỂN HÀNG BẰNG VIETTEL POST :</span><br/>";
                                        message += "<span style=\"font-weight:bold; color:#0070c0\">Qúy khách vui lòng chọn hình thức <strong>Chuyển phát Nhanh hoặc Chậm</strong> của Viettel post để 1688Express chủ động gửi hàng cho bạn.</span><br/>";
                                        message += "Hàng sẽ được vận chuyển đến Địa Chỉ của bạn. Phí ship bạn sẽ thanh toán với nhân viên Viettel khi nhận hàng. Thông tin về từng hình thức:<br/><br/>";
                                        message += "   • Chuyển phát nhanh: 1-2 ngày nhận hàng, phí 30.000- 40.000 VND/kg.<br/>";
                                        message += "   • Chuyển phát chậm: 3-6 ngày nhận hàng, phí 15.000- 25.000 VND/kg.<br/><br/>";

                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", user_order.Email,
                                            "Đơn hàng có sản phẩm đã về kho đích tại 1688 Express", message, "");
                                    }
                                }
                                catch
                                {

                                }
                            }
                            double OCurrent_deposit = 0;
                            if (!string.IsNullOrEmpty(o.Deposit))
                                OCurrent_deposit = Convert.ToDouble(o.Deposit);

                            double OCurrent_FeeShipCN = 0;
                            if (!string.IsNullOrEmpty(o.FeeShipCN))
                                OCurrent_FeeShipCN = Convert.ToDouble(o.FeeShipCN);

                            double OCurrent_FeeBuyPro = 0;
                            if (!string.IsNullOrEmpty(o.FeeBuyPro))
                                OCurrent_FeeBuyPro = Convert.ToDouble(o.FeeBuyPro);

                            double OCurrent_FeeWeight = 0;
                            if (!string.IsNullOrEmpty(o.FeeWeight))
                                OCurrent_FeeWeight = Convert.ToDouble(o.FeeWeight);

                            double OCurrent_IsCheckProductPrice = 0;
                            if (!string.IsNullOrEmpty(o.IsCheckProductPrice))
                                OCurrent_IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);

                            double OCurrent_IsPackedPrice = 0;
                            if (!string.IsNullOrEmpty(o.IsPackedPrice))
                                OCurrent_IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);

                            double OCurrent_IsFastDeliveryPrice = 0;
                            if (!string.IsNullOrEmpty(o.IsFastDeliveryPrice))
                                OCurrent_IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);
                            double OCurrent_TotalPriceReal = 0;
                            if (!string.IsNullOrEmpty(o.TotalPriceReal))
                                OCurrent_TotalPriceReal = Convert.ToDouble(o.TotalPriceReal);

                            double OCurrent_TotalPriceRealCYN = 0;
                            if (!string.IsNullOrEmpty(o.TotalPriceRealCYN))
                                OCurrent_TotalPriceRealCYN = Convert.ToDouble(o.TotalPriceRealCYN);

                            //double OCurrent_FeeShipCNToVN = Convert.ToDouble(o.FeeShipCNToVN);

                            double Deposit = Convert.ToDouble(pDeposit.Value);
                            //double FeeShipCN = Math.Floor(Convert.ToDouble(pCNShipFee.Value));
                            double feeShipVN = feeShipCN * currency;
                            //double FeeShipCN = Convert.ToDouble(pCNShipFee.Value);
                            double FeeShipCN = feeShipVN;
                            double FeeBuyPro = Convert.ToDouble(pBuy.Value);
                            double FeeWeight = Convert.ToDouble(pWeight.Value);
                            double FeeWeChatCYN = Convert.ToDouble(rFeeWeChatCYN.Value);
                            double FeeWeChatVND = Convert.ToDouble(rFeeWeChatVND.Value);
                            //double FeeSenser = Convert.ToDouble(rAdditionFeeForSensorProductVND.Value);
                            double FeeSenser = 0;
                            double remoneyCYN = 0;
                            double remoneyVND = 0;
                            var orders = OrderController.GetByMainOrderID(Convert.ToInt32(id));
                            if (orders.Count > 0)
                            {
                                foreach (var item in orders)
                                {
                                    if (item.IsCensorProduct == true)
                                    {
                                        remoneyCYN += Convert.ToDouble(item.PriceOfCensorCYN);
                                        remoneyVND += Convert.ToDouble(item.PriceOfCensorVND);
                                    }
                                }
                            }
                            FeeSenser = remoneyVND;
                            double TotalPriceReal = Convert.ToDouble(rTotalPriceReal.Value);
                            double TotalPriceRealCYN = Convert.ToDouble(rTotalPriceRealCYN.Value);
                            //double FeeShipCNToVN = Convert.ToDouble(pWeight.Value);

                            double IsCheckProductPrice = 0;
                            if (!string.IsNullOrEmpty(pCheck.Value.ToString()))
                                IsCheckProductPrice = Convert.ToDouble(pCheck.Value.ToString());
                            //if (o.IsCheckProduct == true)
                            //    IsCheckProductPrice = Convert.ToDouble(pCheck.Value);
                            //else
                            //    IsCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);

                            double IsPackedPrice = 0;
                            if (!string.IsNullOrEmpty(pPacked.Value.ToString()))
                                IsPackedPrice = Convert.ToDouble(pPacked.Value.ToString());
                            //if (o.IsPacked == true)
                            //    IsPackedPrice = Convert.ToDouble(pPacked.Value);
                            //else
                            //    IsPackedPrice = Convert.ToDouble(o.IsPackedPrice);

                            double IsFastDeliveryPrice = 0;
                            if (!string.IsNullOrEmpty(pShipHome.Value.ToString()))
                                IsFastDeliveryPrice = Convert.ToDouble(pShipHome.Value.ToString());

                            //if (o.IsFastDelivery == true)
                            //    IsFastDeliveryPrice = Convert.ToDouble(pShipHome.Value);
                            //else
                            //    IsFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);


                            #region Ghi lịch sử chỉnh sửa các loại giá
                            if (OCurrent_deposit != Deposit)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền đặt cọc cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);

                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền đặt cọc cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch
                                //{

                                //}
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền đặt cọc của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_deposit) + ", sang: "
                                        + string.Format("{0:N0}", Deposit) + "", 1, currentDate);
                            }
                            if (OCurrent_FeeShipCN != FeeShipCN)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí ship Trung Quốc cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí ship Trung Quốc cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí ship Trung Quốc của đơn hàng ID là: " + o.ID + ", từ " + string.Format("{0:N0}", OCurrent_FeeShipCN) + " sang "
                                        + string.Format("{0:N0}", FeeShipCN) + "", 2, currentDate);
                            }
                            if (OCurrent_FeeBuyPro < FeeBuyPro || OCurrent_FeeBuyPro > FeeBuyPro)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí mua sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí mua sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí mua sản phẩm của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_FeeBuyPro) + ", sang: "
                                        + string.Format("{0:N0}", FeeBuyPro) + "", 3, currentDate);
                            }
                            if (OCurrent_TotalPriceReal < TotalPriceReal || OCurrent_TotalPriceReal > TotalPriceReal)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về Tổng tiền mua thật cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);
                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí mua sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí mua thật của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_TotalPriceReal) + ", sang: "
                                        + string.Format("{0:N0}", TotalPriceReal) + "", 3, currentDate);
                            }
                            if (OCurrent_FeeWeight != FeeWeight)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí TQ-VN cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);

                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí TQ-VN cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí TQ-VN của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_FeeWeight) + ", sang: "
                                        + string.Format("{0:N0}", FeeWeight) + "", 4, currentDate);
                            }
                            if (OCurrent_IsCheckProductPrice != IsCheckProductPrice)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí kiểm tra sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);

                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí kiểm tra sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí kiểm tra sản phẩm của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsCheckProductPrice) + ", sang: "
                                        + string.Format("{0:N0}", IsCheckProductPrice) + "", 5, currentDate);
                            }
                            if (OCurrent_IsPackedPrice != IsPackedPrice)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí đóng gỗ cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí đóng gỗ cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí đóng gỗ của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsPackedPrice) + ", sang: "
                                        + string.Format("{0:N0}", IsPackedPrice) + "", 6, currentDate);
                            }
                            if (OCurrent_IsFastDeliveryPrice != IsFastDeliveryPrice)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về tiền phí ship giao hàng tận nhà cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về tiền phí ship giao hàng tận nhà cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi tiền phí ship giao hàng tận nhà của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_IsFastDeliveryPrice) + ", sang: "
                                        + string.Format("{0:N0}", IsFastDeliveryPrice) + "", 7, currentDate);
                            }
                            //if (OCurrent_FeeShipCNToVN != FeeShipCNToVN)
                            //{
                            //    HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                            //            " đã đổi tiền phí vận chuyển TQ-VN của đơn hàng ID là: " + o.ID + ", từ: " + string.Format("{0:N0}", OCurrent_FeeShipCNToVN) + ", sang: "
                            //            + string.Format("{0:N0}", FeeShipCNToVN) + "", 7, currentDate);
                            //}
                            #endregion
                            double isfastprice = 0;
                            if (!string.IsNullOrEmpty(o.IsFastPrice))
                                isfastprice = Convert.ToDouble(o.IsFastPrice);

                            double pricenvd = 0;
                            if (!string.IsNullOrEmpty(o.PriceVND))
                                pricenvd = Convert.ToDouble(o.PriceVND);

                            double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                 + IsFastDeliveryPrice + isfastprice + pricenvd + FeeSenser + FeeWeChatVND;

                            //if (ddlStatus.SelectedValue != o.Status.ToString())
                            //{
                            //int stt = Convert.ToInt32(ddlStatus.SelectedValue);
                            //MainOrderController.UpdateFeeShipTQVN(o.ID, FeeShipCNToVN.ToString());
                            MainOrderController.UpdateFee(o.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(), IsCheckProductPrice.ToString(),
                                IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                            MainOrderController.UpdateFeeSensor(o.ID, FeeSenser.ToString());
                            MainOrderController.UpdateFeeWeChat(o.ID, FeeWeChatCYN.ToString(), FeeWeChatVND.ToString());

                            if (status == 2)
                            {
                                MainOrderController.UpdateDepositDate(o.ID, currentDate);
                            }
                            if (TotalPriceVND > Deposit)
                            {
                                var leftmustpaid = TotalPriceVND - Deposit;
                                if (leftmustpaid > 1000)
                                {
                                    if (status > 7)
                                    {
                                        MainOrderController.UpdateStatusByID(o.ID, 7);
                                    }
                                    else
                                    {
                                        MainOrderController.UpdateStatusByID(o.ID, Convert.ToInt32(ddlStatus.SelectedValue));
                                    }
                                }
                                else
                                {
                                    MainOrderController.UpdateStatusByID(o.ID, Convert.ToInt32(ddlStatus.SelectedValue));
                                }
                            }
                            else
                            {
                                MainOrderController.UpdateStatusByID(o.ID, Convert.ToInt32(ddlStatus.SelectedValue));
                                if (TotalPriceVND < Deposit)
                                {
                                    double refundmoney = Deposit - TotalPriceVND;
                                    var accrefund = AccountController.GetByID(Convert.ToInt32(o.UID));
                                    if (accrefund != null)
                                    {
                                        double wallet = Convert.ToDouble(accrefund.Wallet);
                                        double walletNew = wallet + refundmoney;
                                        AccountController.updateWallet(accrefund.ID, walletNew, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 12, refundmoney, 2, currentDate, obj_user.Username);
                                        HistoryPayWalletController.Insert(accrefund.ID, accrefund.Username, o.ID, refundmoney, "Hoàn tiền đơn hàng: " + o.ID + ".", walletNew, 2, 2, currentDate, obj_user.Username);
                                        MainOrderController.UpdateDeposit(o.ID, accrefund.ID, TotalPriceVND.ToString());
                                    }
                                }
                            }
                        }
                        int currentstt = Convert.ToInt32(o.Status);

                        if (currentstt < 3 || currentstt > 7)
                        {
                            if (status != currentstt)
                            {
                                //string retnote = NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);

                                OrderCommentController.Insert(id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn.", true, 1, DateTime.Now, uid);
                                try
                                {
                                    PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email, "Thông báo tại 1688Express.", "Đã có cập nhật trạng thái cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                }
                                catch { }
                            }
                        }
                        else if (currentstt > 2 && currentstt < 8)
                        {
                            if (status < 3 || status > 7)
                            {
                                //string retnote = NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                OrderCommentController.Insert(id, "Đã có cập nhật mới cho đơn hàng #" + id + " của bạn.", true, 1, DateTime.Now, uid);

                                try
                                {
                                    PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email, "Thông báo tại 1688Express.", "Đã có cập nhật trạng thái cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                }
                                catch { }
                            }
                        }
                        #region Ghi lịch sử update status của đơn hàng
                        if (status != currentstt)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: " + orderstatus + ", sang: " + ddlStatus.SelectedItem + "", 0, currentDate);
                        }
                        #endregion
                        //}
                        string OrderWeight = txtOrderWeight.Value.ToString();
                        if (string.IsNullOrEmpty(CurrentOrderWeight))
                        {
                            if (CurrentOrderWeight != OrderWeight)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi cân nặng của đơn hàng ID là: " + o.ID + ", là: " + OrderWeight + "", 8, currentDate);
                            }
                        }
                        else
                        {
                            if (CurrentOrderWeight != OrderWeight)
                            {
                                //NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(o.UID),
                                //    AccountController.GetByID(Convert.ToInt32(o.UID)).Username, id, "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                //    currentDate, obj_user.Username);


                                //try
                                //{
                                //    PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(o.UID)).Email,
                                //        "Thông báo tại 1688 Express.", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                //}
                                //catch { }
                                HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                           " đã đổi cân nặng của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderWeight + ", sang: " + OrderWeight + "",
                                           9, currentDate);
                            }
                        }

                        //string OrderTransactionCode = txtOrdertransactionCode.Text;
                        //if (string.IsNullOrEmpty(CurrentOrderTransactionCode))
                        //{
                        //    if (CurrentOrderTransactionCode != OrderTransactionCode)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn của đơn hàng ID là: " + o.ID + ", là: " + OrderTransactionCode + "", 8, currentDate);
                        //    }
                        //}
                        //else
                        //{
                        //    if (CurrentOrderTransactionCode != OrderTransactionCode)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode + ", sang: " + OrderTransactionCode + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //string OrderTransactionCode2 = txtOrdertransactionCode2.Text;
                        //if (string.IsNullOrEmpty(OrderTransactionCode2))
                        //{
                        //    if (CurrentOrderTransactionCode2 != OrderTransactionCode2)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 2 của đơn hàng ID là: " + o.ID + ", là: " + OrderTransactionCode2 + "", 8, currentDate);
                        //    }
                        //}
                        //else
                        //{
                        //    if (CurrentOrderTransactionCode2 != OrderTransactionCode2)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 2 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode2 + ", sang: " + OrderTransactionCode2 + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(OrderTransactionCode2))
                        //{
                        //    if (CurrentOrderTransactionCode2 != OrderTransactionCode2)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 2 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode2 + ", sang: " + OrderTransactionCode2 + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //string OrderTransactionCode3 = txtOrdertransactionCode3.Text;
                        //if (string.IsNullOrEmpty(OrderTransactionCode3))
                        //{
                        //    if (CurrentOrderTransactionCode3 != OrderTransactionCode3)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 3 của đơn hàng ID là: " + o.ID + ", là: " + OrderTransactionCode3 + "", 8, currentDate);
                        //    }
                        //}
                        //else
                        //{
                        //    if (CurrentOrderTransactionCode3 != OrderTransactionCode3)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 3 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode3 + ", sang: " + OrderTransactionCode3 + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(OrderTransactionCode3))
                        //{
                        //    if (CurrentOrderTransactionCode3 != OrderTransactionCode3)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 3 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode3 + ", sang: " + OrderTransactionCode3 + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //string OrderTransactionCode4 = txtOrdertransactionCode4.Text;
                        //if (string.IsNullOrEmpty(OrderTransactionCode4))
                        //{
                        //    if (CurrentOrderTransactionCode4 != OrderTransactionCode4)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 4 của đơn hàng ID là: " + o.ID + ", là: " + OrderTransactionCode4 + "", 8, currentDate);
                        //    }
                        //}
                        //else
                        //{
                        //    if (CurrentOrderTransactionCode4 != OrderTransactionCode4)
                        //    {                                
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 4 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode4 + ", sang: " + OrderTransactionCode4 + "",
                        //                   8, currentDate);                                
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(OrderTransactionCode4))
                        //{
                        //    if (CurrentOrderTransactionCode4 != OrderTransactionCode4)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 4 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode4 + ", sang: " + OrderTransactionCode4 + "",
                        //                   8, currentDate);
                        //    }
                        //}
                        //string OrderTransactionCode5 = txtOrdertransactionCode5.Text;
                        //if (!string.IsNullOrEmpty(OrderTransactionCode5))
                        //{
                        //    if (CurrentOrderTransactionCode5 != OrderTransactionCode5)
                        //    {
                        //        HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                        //                   " đã đổi mã vận đơn 5 của đơn hàng ID là: " + o.ID + ", từ: " + CurrentOrderTransactionCode5 + ", sang: " + OrderTransactionCode5 + "",
                        //                   8, currentDate);
                        //    }
                        //}

                        string CurrentReceivePlace = o.ReceivePlace;
                        string ReceivePlace = ddlReceivePlace.SelectedValue;

                        if (CurrentReceivePlace != ReceivePlace)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi nơi nhận hàng của đơn hàng ID là: " + o.ID + ", từ: " + CurrentReceivePlace + ", sang: " + ReceivePlace + "",
                                       8, currentDate);
                        }

                        string CurrentAmountDeposit = o.AmountDeposit.Trim();
                        string AmountDeposit = pAmountDeposit.Value.ToString().Trim();

                        if (CurrentAmountDeposit != AmountDeposit)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi số tiền phải cọc của đơn hàng ID là: " + o.ID + ", từ: " + CurrentAmountDeposit + ", sang: " + AmountDeposit + "",
                                       8, currentDate);
                        }

                        bool Currentcheckpro = o.IsCheckProduct.ToString().ToBool();
                        bool checkpro = chkCheck.Checked;
                        if (Currentcheckpro != checkpro)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi dịch vụ kiểm tra đơn hàng của đơn hàng ID là: " + o.ID + ", từ: " + Currentcheckpro + ", sang: " + checkpro + "",
                                       8, currentDate);
                        }
                        bool CurrentPackage = o.IsPacked.ToString().ToBool();
                        bool Package = chkPackage.Checked;
                        if (CurrentPackage != Package)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi dịch vụ đóng gỗ của đơn hàng ID là: " + o.ID + ", từ: " + CurrentPackage + ", sang: " + Package + "",
                                       8, currentDate);
                        }
                        bool CurrentIsFastDelivery = o.IsFastDelivery.ToString().ToBool();
                        bool MoveIsFastDelivery = chkShiphome.Checked;
                        string TotalPriceReal1 = rTotalPriceReal.Value.ToString();
                        string TotalPriceRealCYN1 = rTotalPriceRealCYN.Value.ToString();
                        if (CurrentIsFastDelivery != MoveIsFastDelivery)
                        {
                            HistoryOrderChangeController.Insert(o.ID, obj_user.ID, obj_user.Username, obj_user.Username +
                                       " đã đổi dịch vụ giao hàng tận nhà của đơn hàng ID là: " + o.ID + ", từ: " + CurrentIsFastDelivery + ", sang: " + MoveIsFastDelivery + "",
                                       8, currentDate);
                        }

                        MainOrderController.UpdateOrderWeight(o.ID, OrderWeight);
                        MainOrderController.UpdateCheckPro(o.ID, checkpro);
                        MainOrderController.UpdateIsPacked(o.ID, Package);
                        MainOrderController.UpdateIsFastDelivery(o.ID, MoveIsFastDelivery);
                        MainOrderController.UpdateAmountDeposit(o.ID, AmountDeposit);
                        MainOrderController.UpdateReceivePlace(o.ID, Convert.ToInt32(o.UID), ddlReceivePlace.SelectedValue.ToString(), ddlShippingType.SelectedValue.ToInt(1));
                        MainOrderController.UpdateFeeWeightDC(o.ID, hdfFeeweightPriceDiscount.Value);
                        MainOrderController.UpdateOrderWeightCK(o.ID, hdfFeeweightPriceDiscount.Value);
                        MainOrderController.UpdateTQVNWeight(o.ID, o.UID.ToString().ToInt(), pWeightNDT.Value.ToString());
                        MainOrderController.UpdateTotalPriceReal(o.ID, TotalPriceReal1.ToString(), TotalPriceRealCYN1.ToString());
                        MainOrderController.UpdateMainOrderCode(o.ID, o.UID.ToString().ToInt(), hdfListMainOrderCode.Value);
                        int orderType = Convert.ToInt32(o.OrderType);
                        if (orderType == 3)
                        {
                            MainOrderController.UpdateIsCheckNotiPrice(o.ID, Convert.ToInt32(o.UID), chkIsCheckNotiPrice.Checked);
                        }
                        #endregion
                        #region Tính theo công thức và hoàn tiền
                        int MainOrderID = id;
                        var listorder = OrderController.GetByMainOrderID(MainOrderID);
                        var mainorder = MainOrderController.GetAllByID(MainOrderID);
                        if (mainorder != null)
                        {
                            int UIDUser = Convert.ToInt32(mainorder.UID);
                            bool isLocal = false;
                            var accountKhach = AccountController.GetByID(UIDUser);
                            if (accountKhach != null)
                            {
                                if (accountKhach.IsLocal != null)
                                {
                                    isLocal = Convert.ToBoolean(accountKhach.IsLocal);
                                }
                            }
                            double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                            if (listorder != null)
                            {
                                if (listorder.Count > 0)
                                {
                                    double pricevnd = 0;
                                    double pricecyn = 0;
                                    foreach (var item in listorder)
                                    {
                                        double originprice = Convert.ToDouble(item.price_origin);
                                        double promotionprice = Convert.ToDouble(item.price_promotion);
                                        double oprice = 0;
                                        double opricecyn = 0;
                                        if (item.ProductStatus != 2)
                                        {
                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    //pricecyn += promotionprice;
                                                    opricecyn = promotionprice * Convert.ToDouble(item.quantity);
                                                    oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                                }
                                                else
                                                {
                                                    //pricecyn += originprice;
                                                    opricecyn = originprice * Convert.ToDouble(item.quantity);
                                                    oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                                }
                                            }
                                            else
                                            {
                                                //pricecyn += originprice;
                                                opricecyn = originprice * Convert.ToDouble(item.quantity);
                                                oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                            }
                                        }

                                        pricevnd += oprice;
                                        pricecyn += opricecyn;
                                    }
                                    MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                                    MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());

                                    double Deposit = Convert.ToDouble(mainorder.Deposit);
                                    double FeeShipCN = Convert.ToDouble(mainorder.FeeShipCN);
                                    double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                                    double FeeWeight = Convert.ToDouble(mainorder.FeeWeight);
                                    double FeeWeightCYN = 0;
                                    double FeeWeightCK = 0;

                                    double FeeWeChatCYN = 0;
                                    double FeeWeChatVND = 0;
                                    if (mainorder.WeChatFeeCYN.ToFloat(0) > 0)
                                        FeeWeChatCYN = Convert.ToDouble(mainorder.WeChatFeeCYN);
                                    if (mainorder.WeChatFeeVND.ToFloat(0) > 0)
                                        FeeWeChatVND = Convert.ToDouble(mainorder.WeChatFeeVND);

                                    double IsCheckProductPrice = 0;
                                    if (mainorder.IsCheckProduct == true)
                                    {
                                        double total = 0;
                                        double counpros = 0;
                                        if (listorder.Count > 0)
                                        {
                                            foreach (var item in listorder)
                                            {
                                                counpros += item.quantity.ToInt(1);
                                            }
                                        }

                                        if (counpros >= 1 && counpros <= 2)
                                        {
                                            total = total + (5000 * counpros);
                                        }
                                        else if (counpros > 2 && counpros <= 20)
                                        {
                                            total = total + (3500 * counpros);
                                        }
                                        else if (counpros > 20 && counpros <= 100)
                                        {
                                            total = total + (2000 * counpros);
                                        }
                                        else if (counpros > 100)
                                        {
                                            total = total + (1500 * counpros);
                                        }
                                        IsCheckProductPrice = total;
                                    }
                                    else
                                        IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);

                                    double IsPackedPrice = 0;
                                    IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);

                                    double IsFastDeliveryPrice = 0;
                                    IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);

                                    #region phần chỉnh sửa giá
                                    double totalo = 0;
                                    var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                                    double UL_CKFeeBuyPro = 0;
                                    double UL_CKFeeWeight = 0;
                                    double LessDeposito = 0;
                                    if (ui != null)
                                    {
                                        UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                                        UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                                        LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                                    }

                                    #region Phần tính lại fee vận chuyển
                                    //int shipping = Convert.ToInt32(mainorder.ShippingType);
                                    //int warehouse = Convert.ToInt32(mainorder.ReceivePlace);
                                    //var getAllPackage = SmallPackageController.GetByMainOrderID(MainOrderID);
                                    //if (getAllPackage.Count > 0)
                                    //{
                                    //    string weightlist = "";
                                    //    foreach (var s in getAllPackage)
                                    //    {
                                    //        weightlist += s.Weight + "|";
                                    //    }
                                    //    double returnprice = 0;
                                    //    if (!string.IsNullOrEmpty(weightlist))
                                    //    {
                                    //        double currentWeight = 0;
                                    //        bool checkoutweight = false;
                                    //        if (!string.IsNullOrEmpty(weightlist))
                                    //        {
                                    //            string[] items = weightlist.Split('|');
                                    //            for (int i = 0; i < items.Length - 1; i++)
                                    //            {
                                    //                double weight = Convert.ToDouble(items[i]);
                                    //                if (weight >= 20)
                                    //                    checkoutweight = true;

                                    //                currentWeight += weight;
                                    //            }
                                    //        }
                                    //        double totalWeight = 0;
                                    //        totalWeight += currentWeight;
                                    //        if (warehouse != 4)
                                    //        {
                                    //            var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                                    //            if (fee.Count > 0)
                                    //            {
                                    //                foreach (var f in fee)
                                    //                {
                                    //                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    //                    {
                                    //                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            if (checkoutweight == false)
                                    //            {
                                    //                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                                    //                foreach (var f in fee)
                                    //                {
                                    //                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    //                    {
                                    //                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    //                    }
                                    //                }
                                    //            }
                                    //            else
                                    //            {
                                    //                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                                    //                foreach (var f in fee)
                                    //                {
                                    //                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    //                    {
                                    //                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //    var totalfeeweight = returnprice;
                                    //    FeeWeight = totalfeeweight * current;
                                    //    FeeWeightCK = FeeWeight * UL_CKFeeWeight / 100;
                                    //    FeeWeight = FeeWeight - FeeWeightCK;
                                    //    FeeWeightCYN = FeeWeight / current;
                                    //}
                                    #endregion

                                    double fastprice = 0;
                                    double pricepro = pricevnd;
                                    double servicefee = 0;
                                    double servicefeeMoney = 0;
                                    double feebpnotdc = 0;
                                    double subfeebp = 0;
                                    int ordertype = Convert.ToInt32(mainorder.OrderType);                                   
                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (pricecyn >= item.AmountFrom && pricecyn < item.AmountTo)
                                            {
                                                servicefee = Convert.ToDouble(item.FeePercent.ToString()) / 100;
                                                servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                            }
                                        }
                                    }
                                    feebpnotdc = (pricecyn * servicefee + servicefeeMoney) * current;
                                    subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                    double feebp = feebpnotdc - subfeebp;

                                    if (isLocal == true)
                                    {
                                        feebp = 0;
                                        UL_CKFeeBuyPro = 0;
                                    }

                                    if (mainorder.IsFast == true)
                                    {
                                        fastprice = (pricepro * 5 / 100);
                                    }
                                    double FeeCNShip = FeeShipCN;
                                    double FeeBuyPros = feebp;
                                    double FeeCheck = IsCheckProductPrice;
                                    //double sesnprice = Convert.ToDouble(rAdditionFeeForSensorProductVND.Value);
                                    double sesnprice = 0;
                                    double remoneyCYN = 0;
                                    double remoneyVND = 0;
                                    var orders = OrderController.GetByMainOrderID(Convert.ToInt32(id));
                                    if (orders.Count > 0)
                                    {
                                        foreach (var item in orders)
                                        {
                                            if (item.IsCensorProduct == true)
                                            {
                                                remoneyCYN += Convert.ToDouble(item.PriceOfCensorCYN);
                                                remoneyVND += Convert.ToDouble(item.PriceOfCensorVND);
                                            }
                                        }
                                    }

                                    sesnprice = remoneyVND;

                                    totalo = fastprice + pricepro + FeeCNShip + FeeBuyPros + FeeWeight + FeeCheck + IsPackedPrice + IsFastDeliveryPrice + sesnprice + FeeWeChatVND;

                                    //double AmountDeposit1 = Math.Floor((totalo * LessDeposito) / 100);
                                    double AmountDeposit1 = 0;
                                    if (ordertype == 2)                                    
                                        AmountDeposit1 = Math.Round(totalo, 0);                                    
                                    else                                    
                                        AmountDeposit1 = Math.Floor((totalo * LessDeposito) / 100);
                                    
                                    double newdeposit = 0;

                                    //cập nhật lại giá phải deposit của đơn hàng
                                    MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit1.ToString());

                                    //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                                    newdeposit = AmountDeposit1;

                                    //nếu đã đặt cọc rồi thì trả phí lại cho người ta                                    
                                    #region tính cách mới
                                    if (Deposit > 0)
                                    {
                                        if (Deposit > totalo)
                                        {
                                            double drefund = Deposit - totalo;
                                            double userwallet = 0;
                                            if (ui.Wallet.ToString() != null)
                                                userwallet = Convert.ToDouble(ui.Wallet.ToString());
                                            newdeposit = totalo;
                                            double wallet = userwallet + drefund;
                                            AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                                            PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);

                                            if (status == 2)
                                                HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                                            else
                                                HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);

                                            NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID),
                                                AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, id, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                                1, currentDate, obj_user.Username);
                                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                            hubContext.Clients.All.addNewMessageToPage("", "");
                                            try
                                            {
                                                PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                                    "Thông báo tại 1688 Express", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        else
                                        {
                                            newdeposit = Deposit;
                                        }
                                    }
                                    else
                                    {
                                        newdeposit = 0;
                                    }
                                    #endregion

                                    #endregion
                                    MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                                        FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());
                                    MainOrderController.UpdateFeeweightCK(MainOrderID, FeeWeightCK.ToString());

                                }
                            }
                        }
                        #endregion
                        #region Update User Level
                        if (status >= 9)
                        {
                            int cusID = o.UID.ToString().ToInt(0);
                            var cust = AccountController.GetByID(cusID);
                            if (cust != null)
                            {
                                var cus_orders = MainOrderController.GetSuccessByCustomer(cust.ID);

                                double totalpay = 0;
                                if (cus_orders.Count > 0)
                                {
                                    foreach (var item in cus_orders)
                                    {
                                        double ttpricenvd = 0;
                                        if (!string.IsNullOrEmpty(item.TotalPriceVND))
                                            ttpricenvd = Convert.ToDouble(item.TotalPriceVND);
                                        totalpay += ttpricenvd;
                                    }

                                    if (totalpay >= 100000000 && totalpay < 500000000)
                                    {
                                        if (cust.LevelID == 1)
                                        {
                                            AccountController.updateLevelID(cusID, 2, currentDate, cust.Username);
                                        }
                                    }
                                    else if (totalpay >= 500000000 && totalpay < 1000000000)
                                    {
                                        if (cust.LevelID == 2)
                                        {
                                            AccountController.updateLevelID(cusID, 3, currentDate, cust.Username);
                                        }
                                    }
                                    else if (totalpay >= 1000000000 && totalpay < 5000000000)
                                    {
                                        if (cust.LevelID == 3)
                                        {
                                            AccountController.updateLevelID(cusID, 4, currentDate, cust.Username);
                                        }
                                    }
                                    else if (totalpay >= 5000000000 && totalpay < 999999999999999)
                                    {
                                        if (cust.LevelID == 4)
                                        {
                                            AccountController.updateLevelID(cusID, 5, currentDate, cust.Username);
                                        }
                                    }
                                    //else if (totalpay >= 2500000000 && totalpay < 5000000000)
                                    //{
                                    //    if (cust.LevelID == 5)
                                    //    {
                                    //        AccountController.updateLevelID(cusID, 6, currentDate, cust.Username);
                                    //    }
                                    //}
                                    //else if (totalpay >= 5000000000 && totalpay < 10000000000)
                                    //{
                                    //    if (cust.LevelID == 6)
                                    //    {
                                    //        AccountController.updateLevelID(cusID, 7, currentDate, cust.Username);
                                    //    }
                                    //}
                                    //else if (totalpay >= 10000000000 && totalpay < 20000000000)
                                    //{
                                    //    if (cust.LevelID == 7)
                                    //    {
                                    //        AccountController.updateLevelID(cusID, 8, currentDate, cust.Username);
                                    //    }
                                    //}
                                    //else if (totalpay >= 20000000000)
                                    //{
                                    //    if (cust.LevelID == 8)
                                    //    {
                                    //        AccountController.updateLevelID(cusID, 9, currentDate, cust.Username);
                                    //    }
                                    //}
                                }
                            }
                        }
                        #endregion
                        #region Cập nhật thông tin nhân viên sale và đặt hàng
                        int SalerID = ddlSaler.SelectedValue.ToString().ToInt(0);
                        int DathangID = ddlDatHang.SelectedValue.ToString().ToInt(0);
                        int KhoTQID = ddlKhoTQ.SelectedValue.ToString().ToInt(0);
                        int khoVNID = ddlKhoVN.SelectedValue.ToString().ToInt(0);
                        var mo = MainOrderController.GetAllByID(id);
                        if (mo != null)
                        {
                            double feebp = Convert.ToDouble(mo.FeeBuyPro);
                            DateTime CreatedDate = Convert.ToDateTime(mo.CreatedDate);
                            double salepercent = 0;
                            double salepercentaf3m = 0;
                            double dathangpercent = 0;
                            var config = ConfigurationController.GetByTop1();
                            if (config != null)
                            {
                                salepercent = Convert.ToDouble(config.SalePercent);
                                salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                                dathangpercent = Convert.ToDouble(config.DathangPercent);
                            }
                            string salerName = "";
                            string dathangName = "";

                            int salerID_old = Convert.ToInt32(mo.SalerID);
                            int dathangID_old = Convert.ToInt32(mo.DathangID);

                            #region Saler
                            if (SalerID > 0)
                            {
                                if (SalerID == salerID_old)
                                {
                                    var staff = StaffIncomeController.GetByMainOrderIDUID(id, salerID_old);
                                    if (staff != null)
                                    {
                                        int rStaffID = staff.ID;
                                        int staffstatus = Convert.ToInt32(staff.Status);
                                        if (staffstatus == 1)
                                        {
                                            var sale = AccountController.GetByID(salerID_old);
                                            if (sale != null)
                                            {
                                                salerName = sale.Username;
                                                var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                                int d = CreatedDate.Subtract(createdDate).Days;
                                                if (d > 90)
                                                {
                                                    salepercentaf3m = Convert.ToDouble(staff.PercentReceive);
                                                    double per = feebp * salepercentaf3m / 100;
                                                    StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercentaf3m.ToString(), 1,
                                                        per.ToString(), false, currentDate, username);
                                                }
                                                else
                                                {
                                                    salepercent = Convert.ToDouble(staff.PercentReceive);
                                                    double per = feebp * salepercent / 100;
                                                    StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercent.ToString(), 1,
                                                        per.ToString(), false, currentDate, username);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var staff = StaffIncomeController.GetByMainOrderIDUID(id, salerID_old);
                                    if (staff != null)
                                    {
                                        StaffIncomeController.Delete(staff.ID);
                                    }
                                    var sale = AccountController.GetByID(SalerID);
                                    if (sale != null)
                                    {
                                        salerName = sale.Username;
                                        var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                        int d = CreatedDate.Subtract(createdDate).Days;
                                        if (d > 90)
                                        {
                                            double per = feebp * salepercentaf3m / 100;
                                            StaffIncomeController.Insert(id, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                            CreatedDate, currentDate, username);
                                        }
                                        else
                                        {
                                            double per = feebp * salepercent / 100;
                                            StaffIncomeController.Insert(id, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                            CreatedDate, currentDate, username);
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Đặt hàng
                            if (DathangID > 0)
                            {
                                if (DathangID == dathangID_old)
                                {
                                    var staff = StaffIncomeController.GetByMainOrderIDUID(id, dathangID_old);
                                    if (staff != null)
                                    {
                                        if (staff.Status == 1)
                                        {
                                            //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                            double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                            double totalRealPrice = 0;
                                            if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                                totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                            if (totalRealPrice > 0)
                                            {
                                                double totalpriceloi = totalPrice - totalRealPrice;

                                                dathangpercent = Convert.ToDouble(staff.PercentReceive);
                                                double income = totalpriceloi * dathangpercent / 100;
                                                //double income = totalpriceloi;
                                                StaffIncomeController.Update(staff.ID, totalRealPrice.ToString(), dathangpercent.ToString(), 1,
                                                            income.ToString(), false, currentDate, username);
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    var staff = StaffIncomeController.GetByMainOrderIDUID(id, dathangID_old);
                                    if (staff != null)
                                    {
                                        StaffIncomeController.Delete(staff.ID);
                                    }
                                    var dathang = AccountController.GetByID(DathangID);
                                    if (dathang != null)
                                    {
                                        dathangName = dathang.Username;
                                        //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                        double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                        double totalRealPrice = 0;
                                        if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                            totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                        if (totalRealPrice > 0)
                                        {
                                            double totalpriceloi = totalPrice - totalRealPrice;
                                            double income = totalpriceloi * dathangpercent / 100;
                                            //double income = totalpriceloi;

                                            StaffIncomeController.Insert(id, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                                income.ToString(), false, CreatedDate, currentDate, username);
                                        }
                                        else
                                        {
                                            StaffIncomeController.Insert(id, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                            CreatedDate, currentDate, username);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        var dathang1 = AccountController.GetByID(DathangID);
                        if (dathang1 != null)
                        {
                            NotificationController.Inser(obj_user.ID, username, dathang1.ID,
                                                           dathang1.Username, id,
                                                           "Có đơn hàng mới ID là: " + id, 0,
                                                           1, DateTime.Now, username);
                        }
                        MainOrderController.UpdateStaff(id, SalerID, DathangID, KhoTQID, khoVNID);
                        #endregion
                        var ordergetlast = MainOrderController.GetAllByID(id);
                        if (ordergetlast != null)
                        {
                            var ordershopcode = OrderShopCodeController.GetByMainOrderID(id);
                            if (ordershopcode.Count > 0)
                            {
                                if (ordergetlast.Status < 5)
                                {
                                    MainOrderController.UpdateStatus(id, Convert.ToInt32(o.UID), 5);
                                }
                            }
                            //var smallpackages = SmallPackageController.GetByMainOrderID(id);
                            //if (smallpackages.Count > 0)
                            //{
                            //    if (ordergetlast.Status < 5)
                            //    {
                            //        MainOrderController.UpdateStatus(id, Convert.ToInt32(o.UID), 5);
                            //    }
                            //}
                        }

                        PJUtils.ShowMsg("Cập nhật thông tin thành công.", true, Page);
                    }
                }
            }
        }
        protected void btnStaffUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int SalerID = ddlSaler.SelectedValue.ToString().ToInt(0);
            int DathangID = ddlDatHang.SelectedValue.ToString().ToInt(0);
            int KhoTQID = ddlKhoTQ.SelectedValue.ToString().ToInt(0);
            int khoVNID = ddlKhoVN.SelectedValue.ToString().ToInt(0);
            int ID = ViewState["MOID"].ToString().ToInt();
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            var mo = MainOrderController.GetAllByID(ID);
            if (mo != null)
            {
                double feebp = Convert.ToDouble(mo.FeeBuyPro);
                DateTime CreatedDate = Convert.ToDateTime(mo.CreatedDate);
                double salepercent = 0;
                double salepercentaf3m = 0;
                double dathangpercent = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    salepercent = Convert.ToDouble(config.SalePercent);
                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                }
                string salerName = "";
                string dathangName = "";

                int salerID_old = Convert.ToInt32(mo.SalerID);
                int dathangID_old = Convert.ToInt32(mo.DathangID);

                #region Saler
                if (SalerID > 0)
                {
                    if (SalerID == salerID_old)
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, salerID_old);
                        if (staff != null)
                        {
                            int rStaffID = staff.ID;
                            int status = Convert.ToInt32(staff.Status);
                            if (status == 1)
                            {
                                var sale = AccountController.GetByID(salerID_old);
                                if (sale != null)
                                {
                                    salerName = sale.Username;
                                    var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                    int d = CreatedDate.Subtract(createdDate).Days;
                                    if (d > 90)
                                    {
                                        salepercentaf3m = Convert.ToDouble(staff.PercentReceive);
                                        double per = feebp * salepercentaf3m / 100;
                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercentaf3m.ToString(), 1,
                                            per.ToString(), false, currentDate, username);
                                    }
                                    else
                                    {
                                        salepercent = Convert.ToDouble(staff.PercentReceive);
                                        double per = feebp * salepercent / 100;
                                        StaffIncomeController.Update(rStaffID, mo.TotalPriceVND, salepercent.ToString(), 1,
                                            per.ToString(), false, currentDate, username);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var sale = AccountController.GetByID(SalerID);
                            if (sale != null)
                            {
                                salerName = sale.Username;
                                var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                int d = CreatedDate.Subtract(createdDate).Days;
                                if (d > 90)
                                {
                                    double per = feebp * salepercentaf3m / 100;
                                    StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, currentDate, username);
                                }
                                else
                                {
                                    double per = feebp * salepercent / 100;
                                    StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                    CreatedDate, currentDate, username);
                                }
                            }
                        }
                    }
                    else
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, salerID_old);
                        if (staff != null)
                        {
                            StaffIncomeController.Delete(staff.ID);
                        }
                        var sale = AccountController.GetByID(SalerID);
                        if (sale != null)
                        {
                            salerName = sale.Username;
                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                            int d = CreatedDate.Subtract(createdDate).Days;
                            if (d > 90)
                            {
                                double per = feebp * salepercentaf3m / 100;
                                StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, currentDate, username);
                            }
                            else
                            {
                                double per = feebp * salepercent / 100;
                                StaffIncomeController.Insert(ID, per.ToString(), salepercent.ToString(), SalerID, salerName, 6, 1, per.ToString(), false,
                                CreatedDate, currentDate, username);
                            }
                        }
                    }
                }
                #endregion
                #region Đặt hàng
                if (DathangID > 0)
                {
                    if (DathangID == dathangID_old)
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                        if (staff != null)
                        {
                            if (staff.Status == 1)
                            {
                                //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                double totalRealPrice = 0;
                                if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                    totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                if (totalRealPrice > 0)
                                {
                                    double totalpriceloi = totalPrice - totalRealPrice;

                                    dathangpercent = Convert.ToDouble(staff.PercentReceive);
                                    double income = totalpriceloi * dathangpercent / 100;
                                    //double income = totalpriceloi;
                                    StaffIncomeController.Update(staff.ID, totalRealPrice.ToString(), dathangpercent.ToString(), 1,
                                                income.ToString(), false, currentDate, username);
                                }

                            }
                        }
                        else
                        {
                            var dathang = AccountController.GetByID(DathangID);
                            if (dathang != null)
                            {
                                dathangName = dathang.Username;
                                //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                                double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                                double totalRealPrice = 0;
                                if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                    totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                                if (totalRealPrice > 0)
                                {
                                    double totalpriceloi = totalPrice - totalRealPrice;
                                    double income = totalpriceloi * dathangpercent / 100;
                                    //double income = totalpriceloi;
                                    StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                        income.ToString(), false, CreatedDate, currentDate, username);
                                }
                                else
                                {
                                    StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                    CreatedDate, currentDate, username);
                                }
                            }
                        }
                    }
                    else
                    {
                        var staff = StaffIncomeController.GetByMainOrderIDUID(ID, dathangID_old);
                        if (staff != null)
                        {
                            StaffIncomeController.Delete(staff.ID);
                        }
                        var dathang = AccountController.GetByID(DathangID);
                        if (dathang != null)
                        {
                            dathangName = dathang.Username;
                            //double totalPrice = Convert.ToDouble(mo.TotalPriceVND);
                            double totalPrice = Convert.ToDouble(mo.PriceVND) + Convert.ToDouble(mo.FeeShipCN);
                            double totalRealPrice = 0;
                            if (!string.IsNullOrEmpty(mo.TotalPriceReal))
                                totalRealPrice = Convert.ToDouble(mo.TotalPriceReal);
                            if (totalRealPrice > 0)
                            {
                                double totalpriceloi = totalPrice - totalRealPrice;
                                double income = totalpriceloi * dathangpercent / 100;
                                //double income = totalpriceloi;

                                StaffIncomeController.Insert(ID, totalpriceloi.ToString(), dathangpercent.ToString(), DathangID, dathangName, 3, 1,
                                    income.ToString(), false, CreatedDate, currentDate, username);
                            }
                            else
                            {
                                StaffIncomeController.Insert(ID, "0", dathangpercent.ToString(), DathangID, dathangName, 3, 1, "0", false,
                                CreatedDate, currentDate, username);
                            }
                        }
                    }
                }
                #endregion
            }
            var dathang1 = AccountController.GetByID(DathangID);
            if (dathang1 != null)
            {
                NotificationController.Inser(obj_user.ID, username, dathang1.ID,
                                               dathang1.Username, ID,
                                               "Có đơn hàng mới ID là: " + ID, 0,
                                               1, DateTime.Now, username);
            }
            MainOrderController.UpdateStaff(ID, SalerID, DathangID, KhoTQID, khoVNID);
            PJUtils.ShowMsg("Cập nhật nhân viên thành công.", true, Page);
        }
        protected void btnThanhtoan_Click(object sender, EventArgs e)
        {
            int id = ViewState["MOID"].ToString().ToInt(0);
            //var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                var o = MainOrderController.GetAllByID(id);
                if (o != null)
                {
                    Response.Redirect("/Admin/Pay-Order.aspx?id=" + id);
                }
            }
        }
        #endregion
        #region Ajax
        [WebMethod]
        public static string getCode()
        {
            DateTime currr = DateTime.Now;
            string code = "wc" + currr.Year + currr.Month + currr.Day + currr.Hour + currr.Minute + currr.Second + currr.Millisecond;
            return code;
        }
        [WebMethod]
        public static string DeleteSmallPackage(string IDPackage)
        {
            int ID = IDPackage.ToInt(0);
            var package = SmallPackageController.GetByID(ID);
            if (package != null)
            {
                string kq = SmallPackageController.Delete(ID);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    int orderID = mainorder.ID;
                    int warehouse = mainorder.ReceivePlace.ToInt(1);
                    int shipping = Convert.ToInt32(mainorder.ShippingType);

                    var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

                    double FeeWeight = 0;
                    double FeeWeightDiscount = 0;
                    double ckFeeWeight = 0;
                    ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                    double returnprice = 0;
                    var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                    double totalWeight = 0;
                    if (smallpackage.Count > 0)
                    {

                        bool checkoutweight = false;
                        foreach (var item in smallpackage)
                        {
                            double weight = Convert.ToDouble(item.Weight);
                            if (weight >= 20)
                            {
                                checkoutweight = true;
                            }
                            totalWeight += Convert.ToDouble(item.Weight);
                        }
                        if (warehouse != 4)
                        {
                            var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                            if (fee.Count > 0)
                            {
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (checkoutweight == true)
                            {
                                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                            else
                            {
                                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                                foreach (var f in fee)
                                {
                                    if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                    {
                                        returnprice = totalWeight * Convert.ToDouble(f.Price);
                                    }
                                }
                            }
                        }
                    }
                    double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    FeeWeight = returnprice * currency;
                    FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
                    FeeWeight = FeeWeight - FeeWeightDiscount;

                    double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
                    double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                    double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
                    double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
                    double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
                    double isfastprice = 0;
                    if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                        isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
                    double pricenvd = 0;
                    if (!string.IsNullOrEmpty(mainorder.PriceVND))
                        pricenvd = Convert.ToDouble(mainorder.PriceVND);
                    double Deposit = Convert.ToDouble(mainorder.Deposit);

                    double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                 + IsFastDeliveryPrice + isfastprice + pricenvd;

                    MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
                        IsCheckProductPrice.ToString(),
                        IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
                    MainOrderController.UpdateTotalWeight(mainorder.ID, totalWeight.ToString(), totalWeight.ToString());

                }

                return "ok";
            }
            else
            {
                return "null";
            }
        }
        [WebMethod]
        public static double getWeightPriceAll(int orderID, int warehouse, int shipping)
        {
            double returnprice = 0;
            var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
            if (smallpackage.Count > 0)
            {
                double totalWeight = 0;
                bool checkoutweight = false;
                foreach (var item in smallpackage)
                {
                    double weight = Convert.ToDouble(item.Weight);
                    if (weight >= 20)
                    {
                        checkoutweight = true;
                    }
                    totalWeight += Convert.ToDouble(item.Weight);
                }
                if (warehouse != 4)
                {
                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                    if (fee.Count > 0)
                    {
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                }
                else
                {
                    if (checkoutweight == true)
                    {
                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                    else
                    {
                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                }
            }
            return returnprice;
        }
        [WebMethod]
        public static double getWeightPrice(int orderID, int warehouse, int shipping, string weightlist)
        {
            double returnprice = 0;
            if (!string.IsNullOrEmpty(weightlist))
            {
                double currentWeight = 0;
                bool checkoutweight = false;
                if (!string.IsNullOrEmpty(weightlist))
                {
                    string[] items = weightlist.Split('|');
                    for (int i = 0; i < items.Length - 1; i++)
                    {
                        double weight = Convert.ToDouble(items[i]);
                        if (weight >= 20)
                            checkoutweight = true;

                        currentWeight += weight;
                    }
                }
                double totalWeight = 0;

                //var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
                //if (smallpackage.Count > 0)
                //{
                //    if (checkoutweight == true)
                //    {
                //        foreach (var item in smallpackage)
                //        {
                //            double weight = Convert.ToDouble(item.Weight);
                //            totalWeight += Convert.ToDouble(item.Weight);
                //        }
                //    }
                //    else
                //    {
                //        foreach (var item in smallpackage)
                //        {
                //            double weight = Convert.ToDouble(item.Weight);
                //            if (weight >= 20)
                //            {
                //                checkoutweight = true;
                //            }
                //            totalWeight += Convert.ToDouble(item.Weight);
                //        }
                //    }
                //}
                totalWeight += currentWeight;
                if (warehouse != 4)
                {
                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                    if (fee.Count > 0)
                    {
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                }
                else
                {
                    if (checkoutweight == false)
                    {
                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                    else
                    {
                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                        foreach (var f in fee)
                        {
                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                            {
                                returnprice = totalWeight * Convert.ToDouble(f.Price);
                            }
                        }
                    }
                }
            }
            return returnprice;
        }
        [WebMethod]
        public static string addOrderShopCode(string ordershopcode, string ShopID, string ShopName, int MainOrderID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {
                    }
                    else
                    {
                        var osd = OrderShopCodeController.GetByOrderShopCodeAndShopIDAndMainOrderID(ordershopcode,
                            ShopID, MainOrderID);
                        if (osd == null)
                        {
                            string kq = OrderShopCodeController.Insert(MainOrderID, ordershopcode, ShopID,
                                 ShopName, currentDate, username_current);
                            return kq;
                        }
                        else
                        {
                            return "exist";
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string editOrderShopCode(int id, string ordershopcode, string ShopID, string ShopName, int MainOrderID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {
                    }
                    else
                    {
                        var osd = OrderShopCodeController.GetByOrderShopCodeAndShopIDAndMainOrderID(ordershopcode,
                            ShopID, MainOrderID);
                        if (osd != null)
                        {
                            if (osd.ID == id)
                            {
                                return "ok";
                            }
                            else
                            {
                                return "exist";
                            }
                        }
                        else
                        {
                            OrderShopCodeController.UpdateOrderShopCode(id, ordershopcode, username_current, currentDate);
                            return "ok";
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string deleteOrderShopCode(int id)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {
                    }
                    else
                    {
                        var osd = OrderShopCodeController.GetByID(id);
                        if (osd != null)
                        {
                            OrderShopCodeController.Delete(id);
                            var smallpackage = SmallPackageController.GetByOrderShopCodeID(id);
                            if (smallpackage.Count > 0)
                            {
                                foreach (var s in smallpackage)
                                {
                                    SmallPackageController.Delete(s.ID);
                                }
                            }
                            return "ok";
                        }
                        else
                        {
                            return "notexist";
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string addSmallPackage(int orderShopCodeID, int MainOrderID, string smItems)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {
                    }
                    else
                    {
                        string[] items = smItems.Split('|');
                        if (items.Length - 1 > 0)
                        {
                            string ret = "";
                            for (int i = 0; i < items.Length - 1; i++)
                            {
                                string item = items[i];
                                string[] elements = item.Split(',');
                                string code = elements[0];
                                //float weight = elements[1].ToFloat(0);
                                double weight = 0;
                                if (elements[1].ToFloat(0) > 0)
                                    weight = Convert.ToDouble(elements[1]);
                                int status = elements[2].ToInt(1);
                                string staffnote = elements[3];
                                string kq = SmallPackageController.InsertWithMainOrderIDAndOrderShopCodeIDNew(MainOrderID, 0, code, "", 0,
                                    weight, 0, status, orderShopCodeID, staffnote, currentDate, username_current);
                                if (kq.ToInt(0) > 0)
                                {
                                    ret += kq.ToInt() + "," + code + "," + weight + "," + status + "," + staffnote + "|";
                                }
                            }
                            return ret;
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string deleteSmallPackage(int id)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {
                    }
                    else
                    {
                        var s = SmallPackageController.GetByID(id);
                        if (s != null)
                        {
                            SmallPackageController.Delete(id);
                            var check = RequestOutStockController.GetBySmallpackageID(id);
                            if (check != null)
                            {
                                RequestOutStockController.Delete(check.ID);
                            }
                            return "ok";
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string updateSmallPackage(int id, string code, double weight, int status, string staffnote)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int RoleID = Convert.ToInt32(ac.RoleID);
                if (ac.RoleID != 1)
                {
                    if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                    {

                    }
                    else
                    {
                        var s = SmallPackageController.GetByID(id);
                        if (s != null)
                        {
                            SmallPackageController.UpdateNew(id, 0, code, "", 0, weight, 0, status, staffnote, currentDate, username_current);
                            return "ok";
                        }
                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string setIsCensor(int id, bool check)
        {
            var o = OrderController.GetAllByID(id);
            if (o != null)
            {
                if (check == true)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }

                    double price = 0;
                    double pricepromotion = Convert.ToDouble(o.price_promotion);
                    double priceorigin = Convert.ToDouble(o.price_origin);
                    if (pricepromotion > 0)
                    {
                        if (priceorigin > pricepromotion)
                        {
                            price = pricepromotion;
                        }
                        else
                        {
                            price = priceorigin;
                        }
                    }
                    else
                    {
                        price = priceorigin;
                    }
                    int quantity = o.quantity.ToInt(0);
                    double moneyCYN = ((price * quantity) * 10) / 100;
                    double moneyVND = moneyCYN * currency;
                    OrderController.UpdateIsCencor(id, check, moneyCYN.ToString(), moneyVND.ToString());
                }
                else
                {
                    OrderController.UpdateIsCencor(id, check, "0", "0");
                }

                double remoneyCYN = 0;
                double remoneyVND = 0;
                var orders = OrderController.GetByMainOrderID(Convert.ToInt32(o.MainOrderID));
                if (orders.Count > 0)
                {
                    foreach (var item in orders)
                    {
                        if (item.IsCensorProduct == true)
                        {
                            remoneyCYN += Convert.ToDouble(item.PriceOfCensorCYN);
                            remoneyVND += Convert.ToDouble(item.PriceOfCensorVND);
                        }
                    }
                }

                feeCensore f = new feeCensore();
                f.moneyCYN = remoneyCYN.ToString();
                f.moneyVND = remoneyVND.ToString();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(f);
            }
            else
                return "none";
        }
        #endregion
        #region Class
        public class feeCensore
        {
            public string moneyCYN { get; set; }
            public string moneyVND { get; set; }
        }
        public class historyCustom
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string RoleName { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
        }
        public class objProductOfShop
        {
            public string shopName { get; set; }
            public string shopID { get; set; }
            public double FeeShipVN { get; set; }
            public double TotalPriceShopCYN { get; set; }
            public double TotalPriceShopVND { get; set; }
            public double TotalShopQuantity { get; set; }
            public List<ShopProduct> sp { get; set; }
            public List<OrderShopCode> orderShopCode { get; set; }
        }
        public class ShopProduct
        {
            public int ID { get; set; }
            public int stt { get; set; }
            public string IMG { get; set; }
            public string productName { get; set; }
            public string productLink { get; set; }
            public string variable { get; set; }
            public string quantity { get; set; }
            public string priceVND { get; set; }
            public string priceCYN { get; set; }
            public string note { get; set; }
            public string status { get; set; }
            public bool isCensorProduct { get; set; }
        }
        public class OrderShopCode
        {
            public tbl_OrderShopCode ordershopCode { get; set; }
            public List<tbl_SmallPackage> smallPackage { get; set; }
        }
        #endregion
    }
}
