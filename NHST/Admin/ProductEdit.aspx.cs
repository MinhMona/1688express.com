using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.Admin
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "phuongnguyen";
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0 && ac.RoleID != 3)
                    //    Response.Redirect("/trang-chu");
                    Loaddata();
                }
            }
        }
        public void Loaddata()
        {
            int MainOrderID = 0;
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        hdfcurrent.Value = mainorder.CurrentCNYVN.ToString();
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    int RoleID = Convert.ToInt32(ac.RoleID);
                    hdfUserRole.Value = RoleID.ToString();
                    if (ac.RoleID != 0 && ac.RoleID != 3 && ac.RoleID != 2)
                    {
                        Response.Redirect("/Admin/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }
                    else
                    {
                        //if (ac.RoleID == 3)
                        //{
                        //    if (mainorder.Status >= 5)
                        //        btncreateuser.Visible = false;
                        //    pProductPriceOriginal.Enabled = false;
                        //    pRealPrice.Enabled = true;
                        //}
                    }
                    lblBrandname.Text = o.title_origin;
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);
                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

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
                    ViewState["productprice"] = price;
                    pProductPriceOriginal.Value = price;
                    if (!string.IsNullOrEmpty(o.quantity))
                        pQuanity.Value = Convert.ToDouble(o.quantity);
                    else pQuanity.Value = 0;
                    pRealPrice.Value = Convert.ToDouble(o.RealPrice);
                    txtproducbrand.Text = o.brand;
                    ltrback.Text += "<a href=\"/Admin/OrderDetail.aspx?id=" + o.MainOrderID + "\" class=\"btn btn-success btn-block small-btn\">Trở về</a>";
                    string productstatus = "";
                    if (!string.IsNullOrEmpty(o.ProductStatus.ToString()))
                        ddlStatus.SelectedValue = o.ProductStatus.ToString();
                    else
                        ddlStatus.SelectedValue = "1";
                    txtProductLink.Text = o.link_origin;
                    txtProductImgLink.Text = o.image_origin;
                    //txtOrderShopCode.Text = o.OrderShopCode;
                    #region Lấy danh sách bao nhỏ
                    //StringBuilder spsList = new StringBuilder();
                    //var smallpackages = SmallPackageController.GetByOrderID(id);
                    //if (smallpackages.Count > 0)
                    //{
                    //    foreach (var s in smallpackages)
                    //    {
                    //        int status = Convert.ToInt32(s.Status);
                    //        spsList.Append("<div class=\"ordercode order-versionnew\" data-packageID=\"" + s.ID + "\">");
                    //        spsList.Append("    <div class=\"item-element\">");
                    //        spsList.Append("        <span>Mã Vận đơn:</span>");
                    //        spsList.Append("        <input class=\"transactionCode form-control\" value=\"" + s.OrderTransactionCode + "\" type=\"text\" placeholder=\"Mã vận đơn\" />");
                    //        spsList.Append("    </div>");
                    //        spsList.Append("    <div class=\"item-element\">");
                    //        spsList.Append("        <span>Cân nặng:</span>");
                    //        if (RoleID != 0 && RoleID != 2)
                    //            spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + s.Weight + "\" type=\"text\" placeholder=\"Cân nặng\" disabled onkeyup=\"gettotalweight2()\" />");
                    //        else
                    //            spsList.Append("        <input class=\"transactionWeight form-control\" value=\"" + s.Weight + "\" type=\"text\" placeholder=\"Cân nặng\" onkeyup=\"gettotalweight2()\" />");
                    //        spsList.Append("    </div>");
                    //        spsList.Append("    <div class=\"item-element\">");
                    //        spsList.Append("        <span>Trạng thái:</span>");
                    //        spsList.Append("        <select class=\"transactionCodeStatus form-control\">");
                    //        if (status == 1)
                    //            spsList.Append("            <option value=\"1\" selected>Chưa về kho TQ</option>");
                    //        else
                    //            spsList.Append("            <option value=\"1\">Chưa về kho TQ</option>");
                    //        if (status == 2)
                    //            spsList.Append("            <option value=\"2\" selected>Đã về kho TQ</option>");
                    //        else
                    //            spsList.Append("            <option value=\"2\">Đã về kho TQ</option>");
                    //        if (status == 3)
                    //            spsList.Append("            <option value=\"3\" selected>Đã về kho đích</option>");
                    //        else
                    //            spsList.Append("            <option value=\"3\">Đã về kho đích</option>");
                    //        if (status == 4)
                    //            spsList.Append("            <option value=\"4\" selected>Đã giao khách hàng</option>");
                    //        else
                    //            spsList.Append("            <option value=\"4\">Đã giao khách hàng</option>");
                    //        spsList.Append("        </select>");
                    //        spsList.Append("    </div>");
                    //        spsList.Append("    <div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>");
                    //        spsList.Append("</div>");
                    //    }
                    //    ltrCodeList.Text = spsList.ToString();
                    //}
                    #endregion
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);

            //Update lại giá sản phẩm
            var id = Request.QueryString["id"].ToInt(0);
            int MainOrderID = 0;
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    MainOrderID = Convert.ToInt32(o.MainOrderID);

                    double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);

                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

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
                    double quantity = 0;
                    if (status == 2)
                    {
                        price = 0;
                        quantity = 0;
                        var od = MainOrderController.GetAllByID(MainOrderID);
                        if (od != null)
                        {
                            int userdathangID = Convert.ToInt32(od.UID);
                            var userdathang = AccountController.GetByID(userdathangID);
                            if (userdathang != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, userdathang.ID, userdathang.Username, MainOrderID,
                                                       "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.", 0,
                                                       1, DateTime.Now, obj_user.Username);
                            }
                        }
                        if (price.ToString() != pProductPriceOriginal.Value.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                        }
                        if (o.quantity != pQuanity.Value.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + o.quantity + ", sang: "
                                            + pQuanity.Value + "", 1, currentDate);
                        }
                        OrderController.UpdateQuantity(id, quantity.ToString());
                        OrderController.UpdateProductStatus(id, status);
                        OrderController.UpdatePricePriceReal(id, "0", "0");
                        OrderController.UpdatePricePromotion(id, "0");
                    }
                    else
                    {
                        quantity = Convert.ToDouble(pQuanity.Value);
                        if (price.ToString() != pProductPriceOriginal.Value.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                            + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                        }
                        if (o.quantity != pQuanity.Value.ToString())
                        {
                            HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                            " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + o.quantity + ", sang: "
                                            + pQuanity.Value + "", 1, currentDate);
                        }
                        OrderController.UpdateQuantity(id, quantity.ToString());
                        OrderController.UpdateProductStatus(id, status);
                        OrderController.UpdatePricePriceReal(id, pProductPriceOriginal.Value.ToString(), pRealPrice.Value.ToString());
                        OrderController.UpdatePricePromotion(id, pProductPriceOriginal.Value.ToString());
                    }


                    OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());
                    OrderController.UpdateLinkLinkIMG(id, txtProductLink.Text, txtProductImgLink.Text);
                    //OrderController.UpdateOrderShopCode(id, txtOrderShopCode.Text.Trim());
                }
            }

            //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí
            var listorder = OrderController.GetByMainOrderID(MainOrderID);
            var mainorder = MainOrderController.GetAllByID(MainOrderID);

            if (mainorder != null)
            {
                bool isLocal = false;
                int UIDKhach = Convert.ToInt32(mainorder.UID);
                var accountKhach = AccountController.GetByID(UIDKhach);
                if (accountKhach != null)
                {
                    if (accountKhach.IsLocal != null)
                    {
                        isLocal = Convert.ToBoolean(accountKhach.IsLocal);
                    }
                }
                double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                #region cập nhật và tạo mới smallpackage
                //string tcl = hdfCodeTransactionList.Value;
                //if (!string.IsNullOrEmpty(tcl))
                //{
                //    string[] list = tcl.Split('|');
                //    for (int i = 0; i < list.Length - 1; i++)
                //    {
                //        string[] item = list[i].Split(',');
                //        int ID = item[0].ToInt(0);
                //        string code = item[1];
                //        string weight = item[2];
                //        int smallpackage_status = item[3].ToInt(1);
                //        if (ID > 0)
                //        {
                //            var smp = SmallPackageController.GetByID(ID);
                //            if (smp != null)
                //            {
                //                int bigpackageID = Convert.ToInt32(smp.BigPackageID);
                //                SmallPackageController.Update(ID, bigpackageID, code, smp.ProductType, Convert.ToDouble(smp.FeeShip),
                //                    Convert.ToDouble(weight), Convert.ToDouble(smp.Volume), smallpackage_status, currentDate, username);
                //                var bigpack = BigPackageController.GetByID(bigpackageID);
                //                if (bigpack != null)
                //                {
                //                    int TotalPackageWaiting = SmallPackageController.GetCountByBigPackageIDStatus(bigpackageID, 1, 2);
                //                    if (TotalPackageWaiting == 0)
                //                    {
                //                        BigPackageController.UpdateStatus(bigpackageID, 2, currentDate, username);
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                SmallPackageController.InsertWithOrderID(MainOrderID, id, 0, code, "", 0, Convert.ToDouble(weight), 0, smallpackage_status, currentDate, username);
                //            }
                //        }
                //        else
                //        {
                //            SmallPackageController.InsertWithOrderID(MainOrderID, id, 0, code, "", 0, Convert.ToDouble(weight), 0, smallpackage_status, currentDate, username);
                //        }
                //    }
                //}
                //var smallpackages = SmallPackageController.GetByMainOrderID(MainOrderID);
                //if (smallpackages.Count > 0)
                //{
                //    if (status < 5)
                //    {
                //        MainOrderController.UpdateStatus(id, Convert.ToInt32(mainorder.UID), 5);
                //    }
                //}
                #endregion
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

                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * Convert.ToDouble(item.CurrentCNYVN) + Convert.ToDouble(item.PriceChange);

                            //pricecyn += item.price_origin.ToFloat();
                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * current;
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
                        double FeeWeightCK = Convert.ToDouble(mainorder.FeeWeightCK);
                        //double FeeShipCNToVN = Convert.ToDouble(mainorder.FeeShipCNToVN);

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
                            //var count = listpro.Count;
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
                            //else if (counpros > 500)
                            //{
                            //    foreach (var item in listorder)
                            //    {
                            //        if (Convert.ToDouble(item.price_origin) < 10)
                            //            total = total + (700 * item.quantity.ToInt(1));
                            //        else if (Convert.ToDouble(item.price_origin) >= 10)
                            //            total = total + (1000 * item.quantity.ToInt(1));
                            //    }
                            //}
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
                        //if (ordertype != 2)
                        //{
                        //    var adminfeebuypro = FeeBuyProController.GetAll();
                        //    if (adminfeebuypro.Count > 0)
                        //    {
                        //        foreach (var item in adminfeebuypro)
                        //        {
                        //            if (pricecyn >= item.AmountFrom && pricecyn < item.AmountTo)
                        //            {
                        //                servicefee = Convert.ToDouble(item.FeePercent.ToString()) / 100;
                        //                servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                        //            }
                        //        }
                        //    }
                        //    feebpnotdc = (pricecyn * servicefee + servicefeeMoney) * current;
                        //}
                        //else
                        //{
                        //    feebpnotdc = (pricecyn * 3 / 100) * current;
                        //}
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


                        double FeeWeChatCYN = 0;
                        double FeeWeChatVND = 0;
                        if (mainorder.OrderType == 2)
                        {
                            double wechatFeeConfig = 0;
                            var config = ConfigurationController.GetByTop1();
                            if (config != null)
                            {
                                wechatFeeConfig = Convert.ToDouble(config.WeChatFee);
                            }
                            if (wechatFeeConfig > 0)
                            {
                                FeeWeChatCYN = (pricecyn * wechatFeeConfig) / 100;
                            }
                            FeeWeChatVND = FeeWeChatCYN * current;
                        }


                        //if (mainorder.WeChatFeeCYN.ToFloat(0) > 0)
                        //    FeeWeChatCYN = Convert.ToDouble(mainorder.WeChatFeeCYN);
                        //if (mainorder.WeChatFeeVND.ToFloat(0) > 0)
                        //    FeeWeChatVND = Convert.ToDouble(mainorder.WeChatFeeVND);

                        totalo = fastprice + pricepro + FeeCNShip + FeeBuyPros + FeeWeight + FeeCheck + IsPackedPrice
                            + IsFastDeliveryPrice + FeeWeChatVND;
                        double AmountDeposit = 0;
                        if (mainorder.OrderType == 2)
                        {
                            AmountDeposit = Math.Round(totalo, 0);
                        }
                        else
                        {
                            AmountDeposit = Math.Floor((totalo * LessDeposito) / 100);
                        }
                        
                        //double TotalPriceVND = FeeShipCN + FeeBuyPro
                        //                         + FeeWeight + IsCheckProductPrice
                        //                         + IsPackedPrice + IsFastDeliveryPrice
                        //                         + Convert.ToDouble(mainorder.IsFastPrice) + pricevnd;
                        double newdeposit = 0;

                        //cập nhật lại giá phải deposit của đơn hàng
                        MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString());

                        //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                        newdeposit = AmountDeposit;

                        //nếu đã đặt cọc rồi thì trả phí lại cho người ta
                        #region tính cách cũ
                        //if (Deposit > 0)
                        //{
                        //    if (Deposit > newdeposit)
                        //    {
                        //        double drefund = Deposit - newdeposit;
                        //        double userwallet = 0;
                        //        if (ui.Wallet.ToString() != null)
                        //            userwallet = Convert.ToDouble(ui.Wallet.ToString());

                        //        double wallet = userwallet + drefund;
                        //        AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                        //        PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                        //        // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                        //        //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
                        //        if (status == 2)
                        //            HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                        //        else
                        //            HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);
                        //        NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID),
                        //            AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, id, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                        //            1, currentDate, obj_user.Username);
                        //        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                        //        hubContext.Clients.All.addNewMessageToPage("", "");
                        //        try
                        //        {
                        //            PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                        //                "Thông báo tại 1688 Express", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                        //        }
                        //        catch
                        //        {

                        //        }
                        //        //newdeposit = Deposit;
                        //        MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                        //    }
                        //    else
                        //    {
                        //        if (Deposit < newdeposit)
                        //        {
                        //            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                        //        }
                        //        else if (Deposit == newdeposit)
                        //        {
                        //            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                        //        }
                        //        newdeposit = Deposit;

                        //    }
                        //}
                        //else
                        //{
                        //    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                        //    newdeposit = 0;
                        //}
                        #endregion
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
                                // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                                //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
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
                                //newdeposit = Deposit;
                                //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);

                            }
                            else
                            {
                                if (Deposit < totalo)
                                {
                                    //if(Deposit < newdeposit)
                                    //    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                }
                                else if (Deposit == newdeposit)
                                {
                                    if (mainorder.Status > 2)
                                    {

                                    }
                                    else
                                    {
                                        //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                    }
                                }
                                newdeposit = Deposit;
                            }
                        }
                        else
                        {
                            //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                            newdeposit = 0;
                        }
                        #endregion
                        if (totalo == 0)
                        {
                            //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                        }
                        //if (status == 2)
                        //{

                        //}
                        //else
                        //{
                        //    newdeposit = Deposit;
                        //}
                        #endregion


                        MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                            FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());
                        MainOrderController.UpdateFeeWeChat(MainOrderID, FeeWeChatCYN.ToString(), FeeWeChatVND.ToString());
                        MainOrderController.UpdateFeeweightCK(MainOrderID, FeeWeightCK.ToString());

                    }
                }
            }
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/Admin/OrderDetail.aspx?id=" + MainOrderID, Page);
        }

        #region Webservice
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
                    double sensorPrice = 0;
                    if (!string.IsNullOrEmpty(mainorder.AdditionFeeForSensorProduct))
                    {
                        sensorPrice = Convert.ToDouble(mainorder.AdditionFeeForSensorProduct);
                    }
                    double isfastprice = 0;
                    if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
                        isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
                    double pricenvd = 0;
                    if (!string.IsNullOrEmpty(mainorder.PriceVND))
                        pricenvd = Convert.ToDouble(mainorder.PriceVND);
                    double Deposit = Convert.ToDouble(mainorder.Deposit);

                    double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
                                                 + IsFastDeliveryPrice + isfastprice + pricenvd + sensorPrice;

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
        #endregion
    }
}