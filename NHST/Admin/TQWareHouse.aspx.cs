using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using MB.Extensions;
using System.Drawing;
using System.Drawing.Imaging;

namespace NHST.Admin
{
    public partial class TQWareHouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "khotq";
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
                    if (ac.RoleID != 4 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        [WebMethod]
        public static string GetCode_old(string barcode)
        {
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    OrderGet o = new OrderGet();
                    o.ID = package.ID;
                    o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                    o.BarCode = package.OrderTransactionCode;
                    o.TotalWeight = package.Weight.ToString();
                    o.Status = Convert.ToInt32(package.Status);
                    int mainOrderID = Convert.ToInt32(package.MainOrderID);
                    o.MainorderID = mainOrderID;
                    var orders = OrderController.GetByMainOrderID(mainOrderID);
                    o.Soloaisanpham = orders.Count.ToString();
                    double totalProductQuantity = 0;
                    if (orders.Count > 0)
                    {
                        foreach (var p in orders)
                        {
                            totalProductQuantity += Convert.ToDouble(p.quantity);
                        }
                    }
                    o.Soluongsanpham = totalProductQuantity.ToString();
                    if (mainorder.IsCheckProduct == true)
                        o.Kiemdem = "Có";
                    else
                        o.Kiemdem = "Không";
                    if (mainorder.IsPacked == true)
                        o.Donggo = "Có";
                    else
                        o.Donggo = "Không";

                    var listb = BigPackageController.GetAllNotHuy();
                    if (listb.Count > 0)
                    {
                        o.ListBig = listb;
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o);
                }
                else return "none";
            }
            //var p = OrderShopController.GetByBarCode(barcode);
            //if (p != null)
            //{
            //    OrderGet o = new OrderGet();
            //    o.ID = p.ID;
            //    int UID = Convert.ToInt32(p.UID);
            //    o.UID = UID;
            //    double wallet = 0;
            //    string Username = "";
            //    var user = AccountController.GetByID(UID);
            //    if (user != null)
            //    {
            //        wallet = Convert.ToDouble(user.Wallet);
            //        Username = user.Username;
            //    }
            //    o.Username = Username;
            //    o.Wallet = wallet;
            //    o.OrderShopCode = p.OrderShopCode;
            //    o.BarCode = p.Barcode;
            //    o.TotalWeight = p.TotalWeight;
            //    o.TotalPriceVND = string.Format("{0:N0}", Convert.ToDouble(p.TotalPriceVND)).Replace(",", ".");
            //    o.TotalPriceVNDNum = Convert.ToDouble(p.TotalPriceVND) - Convert.ToDouble(p.Deposit);
            //    o.Status = Convert.ToInt32(p.Status);
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    return serializer.Serialize(o);
            //}
            else return "none";
        }
        [WebMethod]
        public static string GetCode(string barcode)
        {
            var packages = SmallPackageController.GetPackagesByOrderTransactionCode(barcode.Trim());
            if (packages.Count > 0)
            {
                List<OrderGet> ogs = new List<OrderGet>();
                foreach (var package in packages)
                {
                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                    if (mainorder != null)
                    {
                        OrderGet o = new OrderGet();
                        o.ID = package.ID;
                        o.UID = Convert.ToInt32(mainorder.UID);
                        o.BigPackageID = Convert.ToInt32(package.BigPackageID);
                        o.BarCode = package.OrderTransactionCode;
                        o.TotalWeight = package.Weight.ToString();
                        o.Status = Convert.ToInt32(package.Status);
                        int mainOrderID = Convert.ToInt32(package.MainOrderID);
                        o.MainorderID = mainOrderID;
                        var orders = OrderController.GetByMainOrderID(mainOrderID);
                        o.Soloaisanpham = orders.Count.ToString();
                        double totalProductQuantity = 0;
                        if (orders.Count > 0)
                        {
                            foreach (var p in orders)
                            {
                                totalProductQuantity += Convert.ToDouble(p.quantity);
                            }
                        }
                        o.Soluongsanpham = totalProductQuantity.ToString();
                        if (mainorder.IsCheckProduct == true)
                            o.Kiemdem = "Có";
                        else
                            o.Kiemdem = "Không";
                        if (mainorder.IsPacked == true)
                            o.Donggo = "Có";
                        else
                            o.Donggo = "Không";
                        o.orderType = 1;
                        var listb = BigPackageController.GetAllNotHuy();
                        if (listb.Count > 0)
                        {
                            o.ListBig = listb;
                        }
                        ogs.Add(o);
                    }
                    else
                    {
                        var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (transportation != null)
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            o.UID = 0;
                            o.BigPackageID = 0;
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            int mainOrderID = Convert.ToInt32(package.MainOrderID);
                            o.MainorderID = transportation.ID;
                            o.Soloaisanpham = "0";
                            o.Soluongsanpham = "0";
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";

                            o.orderType = 2;
                            var listb = BigPackageController.GetAllNotHuy();
                            if (listb.Count > 0)
                            {
                                o.ListBig = listb;
                            }
                            ogs.Add(o);
                        }
                        else
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            o.UID = 0;
                            o.BigPackageID = 0;
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            int mainOrderID = Convert.ToInt32(package.MainOrderID);
                            o.MainorderID = 0;
                            o.Soloaisanpham = "0";
                            o.Soluongsanpham = "0";
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";
                            o.orderType = 3;
                            var listb = BigPackageController.GetAllNotHuy();
                            if (listb.Count > 0)
                            {
                                o.ListBig = listb;
                            }
                            ogs.Add(o);
                        }

                    }

                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(ogs);
            }
            else return "none";
        }
        [WebMethod]
        public static string GetWallet(int UID)
        {

            var user = AccountController.GetByID(UID);
            if (user != null)
                return user.Wallet.ToString();
            else return "0";


        }
        [WebMethod]
        public static string SetFinish(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    if (mainorder.Status == 9)
                    {
                        SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                        return "ok";
                        //int bigbackageID = Convert.ToInt32(package.BigPackageID);                        
                    }
                    else return "none";
                }
                else return "none";
            }
            //var p = OrderShopController.GetByBarCode(barcode);
            //if (p != null)
            //{
            //    if (p.Status < 7)
            //    {
            //        double deposit = Convert.ToDouble(p.Deposit);
            //        double totalprice = Convert.ToDouble(p.TotalPriceVND);
            //        if (deposit < totalprice)
            //        {
            //            int UID = Convert.ToInt32(p.UID);
            //            var u = AccountController.GetByID(Convert.ToInt32(UID));
            //            if (u != null)
            //            {
            //                double sotienphaitra = totalprice - deposit;
            //                double wallet = Convert.ToDouble(u.Wallet);
            //                if (wallet >= sotienphaitra)
            //                {
            //                    double wallet_conlai = wallet - sotienphaitra;
            //                    AccountController.updateWallet(UID, wallet_conlai);
            //                    HistoryPayWalletController.Insert(UID, u.Username, p.ID, p.OrderShopCode, sotienphaitra,
            //                        "Xác nhận đã giao đơn hàng: " + p.OrderShopCode + ".", wallet_conlai, 1, 3, "", currentDate, username_current);
            //                    OrderShopController.UpdateDeposit(p.ID, totalprice.ToString());
            //                    OrderShopController.UpdateStatus(p.ID, 7);
            //                    OrderShopController.UpdateOrderDateExport(p.ID, DateTime.Now);
            //                    return "ok";
            //                }
            //                else
            //                {
            //                    double wallet_conlai = sotienphaitra - wallet;
            //                    return string.Format("{0:N0}", wallet_conlai).Replace(",", ".");
            //                }
            //            }
            //            else
            //            {
            //                return "none";
            //            }
            //        }
            //        else
            //        {
            //            OrderShopController.UpdateStatus(p.ID, 7);
            //            OrderShopController.UpdateOrderDateExport(p.ID, DateTime.Now);
            //            return "ok";
            //        }
            //    }
            //    else return "ok";
            //}
            //else return "none";
            return "none";
        }
        [WebMethod]
        public static string UpdateQuantity_old(string barcode, string quantity, int status, int BigPackageID)
        {
            #region cách cũ tính cân nặng
            //string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            //DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            //if (package != null)
            //{
            //    SmallPackageController.UpdateWeightStatus(package.ID, quantity.ToFloat(0), status, BigPackageID);
            //    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
            //    if (mainorder != null)
            //    {
            //        int orderID = mainorder.ID;
            //        int warehouse = mainorder.ReceivePlace.ToInt(1);
            //        int shipping = Convert.ToInt32(mainorder.ShippingType);

            //        bool checkIsChinaCome = true;
            //        double totalweight = quantity.ToFloat(0);
            //        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
            //        if (packages.Count > 0)
            //        {
            //            foreach (var p in packages)
            //            {
            //                if (p.Status < 2)
            //                    checkIsChinaCome = false;
            //                if (p.OrderTransactionCode != barcode)
            //                {
            //                    totalweight += Convert.ToDouble(p.Weight);
            //                }
            //            }
            //        }
            //        var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

            //        double FeeWeight = 0;
            //        double FeeWeightDiscount = 0;
            //        double ckFeeWeight = 0;
            //        ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
            //        double returnprice = 0;
            //        var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
            //        if (smallpackage.Count > 0)
            //        {
            //            double totalWeight = 0;
            //            bool checkoutweight = false;
            //            foreach (var item in smallpackage)
            //            {
            //                double weight = Convert.ToDouble(item.Weight);
            //                if (weight >= 20)
            //                {
            //                    checkoutweight = true;
            //                }
            //                totalWeight += Convert.ToDouble(item.Weight);
            //            }
            //            if (warehouse != 4)
            //            {
            //                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
            //                if (fee.Count > 0)
            //                {
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (checkoutweight == true)
            //                {
            //                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
            //        FeeWeight = returnprice * currency;
            //        FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
            //        FeeWeight = FeeWeight - FeeWeightDiscount;

            //        double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
            //        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
            //        double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
            //        double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
            //        double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
            //        double isfastprice = 0;
            //        if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
            //            isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
            //        double pricenvd = 0;
            //        if (!string.IsNullOrEmpty(mainorder.PriceVND))
            //            pricenvd = Convert.ToDouble(mainorder.PriceVND);
            //        double Deposit = Convert.ToDouble(mainorder.Deposit);

            //        double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
            //                                     + IsFastDeliveryPrice + isfastprice + pricenvd;

            //        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
            //            IsCheckProductPrice.ToString(),
            //            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
            //        MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
            //        var accChangeData = AccountController.GetByUsername(username_current);
            //        if (accChangeData != null)
            //        {
            //            if (status == 2)
            //            {

            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
            //                               + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);


            //            }
            //            if (checkIsChinaCome == true)
            //            {
            //                MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
            //            }
            //        }

            //        return "1";
            //    }
            //    else return "none";
            //}

            //return "none";
            #endregion
            #region cách mới không tính cân nặng
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, quantity.ToFloat(0), status, BigPackageID);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    bool checkIsChinaCome = true;
                    double totalweight = quantity.ToFloat(0);
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 2)
                                checkIsChinaCome = false;
                            if (p.OrderTransactionCode != barcode)
                            {
                                totalweight += Convert.ToDouble(p.Weight);
                            }
                        }
                    }
                    MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        if (status == 2)
                        {
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                        }
                        if (checkIsChinaCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                        }
                    }

                    return "1";
                }
                else return "none";
            }
            return "none";
            #endregion
        }
        [WebMethod]
        public static string UpdateQuantity(string barcode, int status, int packageID)
        {
            #region Cách cũ có tính cân nặng
            //string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            //DateTime currentDate = DateTime.Now;
            ////var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            //var package = SmallPackageController.GetByID(packageID);
            //if (package != null)
            //{
            //    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, 0);
            //    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
            //    if (mainorder != null)
            //    {
            //        int orderID = mainorder.ID;
            //        int warehouse = mainorder.ReceivePlace.ToInt(1);
            //        int shipping = Convert.ToInt32(mainorder.ShippingType);

            //        bool checkIsChinaCome = true;
            //        double totalweight = 0;
            //        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
            //        if (packages.Count > 0)
            //        {
            //            foreach (var p in packages)
            //            {
            //                if (p.Status < 2)
            //                    checkIsChinaCome = false;
            //                if (p.OrderTransactionCode != barcode)
            //                {
            //                    totalweight += Convert.ToDouble(p.Weight);
            //                }
            //            }
            //        }
            //        var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));

            //        double FeeWeight = 0;
            //        double FeeWeightDiscount = 0;
            //        double ckFeeWeight = 0;
            //        ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
            //        double returnprice = 0;
            //        var smallpackage = SmallPackageController.GetByMainOrderID(orderID);
            //        if (smallpackage.Count > 0)
            //        {
            //            double totalWeight = 0;
            //            bool checkoutweight = false;
            //            foreach (var item in smallpackage)
            //            {
            //                double weight = Convert.ToDouble(item.Weight);
            //                if (weight >= 20)
            //                {
            //                    checkoutweight = true;
            //                }
            //                totalWeight += Convert.ToDouble(item.Weight);
            //            }
            //            if (warehouse != 4)
            //            {
            //                var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
            //                if (fee.Count > 0)
            //                {
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (checkoutweight == true)
            //                {
            //                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
            //                    foreach (var f in fee)
            //                    {
            //                        if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                        {
            //                            returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        double currency = Convert.ToDouble(mainorder.CurrentCNYVN);
            //        FeeWeight = returnprice * currency;
            //        FeeWeightDiscount = FeeWeight * ckFeeWeight / 100;
            //        FeeWeight = FeeWeight - FeeWeightDiscount;

            //        double FeeShipCN = Math.Floor(Convert.ToDouble(mainorder.FeeShipCN));
            //        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
            //        double IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);
            //        double IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);
            //        double IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);
            //        double isfastprice = 0;
            //        if (!string.IsNullOrEmpty(mainorder.IsFastPrice))
            //            isfastprice = Convert.ToDouble(mainorder.IsFastPrice);
            //        double pricenvd = 0;
            //        if (!string.IsNullOrEmpty(mainorder.PriceVND))
            //            pricenvd = Convert.ToDouble(mainorder.PriceVND);
            //        double Deposit = Convert.ToDouble(mainorder.Deposit);

            //        double TotalPriceVND = FeeShipCN + FeeBuyPro + FeeWeight + IsCheckProductPrice + IsPackedPrice
            //                                     + IsFastDeliveryPrice + isfastprice + pricenvd;

            //        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(), FeeBuyPro.ToString(), FeeWeight.ToString(),
            //            IsCheckProductPrice.ToString(),
            //            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
            //        MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
            //        var accChangeData = AccountController.GetByUsername(username_current);
            //        if (accChangeData != null)
            //        {
            //            if (status == 2)
            //            {

            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
            //                               + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);


            //            }
            //            if (checkIsChinaCome == true)
            //            {
            //                MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
            //            }
            //        }

            //        return "1";
            //    }
            //    else
            //    {
            //        SmallPackageController.UpdateWeightStatus(package.ID, 0, status, 0);
            //        var transportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
            //        if (transportation != null)
            //        {
            //            int tID = transportation.ID;
            //            int warehouseFrom = Convert.ToInt32(transportation.WarehouseFromID);
            //            int warehouse = Convert.ToInt32(transportation.WarehouseID);
            //            int shipping = Convert.ToInt32(transportation.ShippingTypeID);

            //            bool checkIsChinaCome = true;
            //            double totalweight = 0;
            //            double returnprice = 0;
            //            //var packages = SmallPackageController.GetByTransportationOrderID(tID);
            //            //if (packages.Count > 0)
            //            //{
            //            //    foreach (var p in packages)
            //            //    {
            //            //        if (p.Status < 2)
            //            //            checkIsChinaCome = false;

            //            //        totalweight += Convert.ToDouble(p.Weight);

            //            //    }
            //            //}
            //            //double returnprice = 0;
            //            //double pricePerWeight = 0;
            //            //double finalPriceOfPackage = 0;
            //            //var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
            //            // warehouse, shipping, true);
            //            //if (fee.Count > 0)
            //            //{
            //            //    foreach (var f in fee)
            //            //    {
            //            //        if (totalweight > f.WeightFrom && totalweight <= f.WeightTo)
            //            //        {
            //            //            pricePerWeight = Convert.ToDouble(f.Price);
            //            //            returnprice = totalweight * Convert.ToDouble(f.Price);
            //            //            break;
            //            //        }
            //            //    }

            //            //}
            //            //foreach (var item in packages)
            //            //{
            //            //    double compareweight = 0;
            //            //    double weight = Convert.ToDouble(item.Weight);
            //            //    compareweight = weight * pricePerWeight;
            //            //    finalPriceOfPackage += compareweight;
            //            //}
            //            double totalWeight = 0;
            //            bool checkoutweight = false;
            //            var smallpackage = SmallPackageController.GetByMainOrderID(tID);
            //            if (smallpackage.Count > 0)
            //            {

            //                foreach (var item in smallpackage)
            //                {
            //                    double weight = Convert.ToDouble(item.Weight);
            //                    if (weight >= 20)
            //                    {
            //                        checkoutweight = true;
            //                    }
            //                    totalWeight += Convert.ToDouble(item.Weight);
            //                }
            //                if (warehouse != 4)
            //                {
            //                    var feet = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
            //                    if (feet.Count > 0)
            //                    {
            //                        foreach (var f in feet)
            //                        {
            //                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                            {
            //                                returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if (checkoutweight == false)
            //                    {
            //                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
            //                        foreach (var f in fee)
            //                        {
            //                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                            {
            //                                returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
            //                        foreach (var f in fee)
            //                        {
            //                            if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
            //                            {
            //                                returnprice = totalWeight * Convert.ToDouble(f.Price);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));



            //            double currency = Convert.ToDouble(transportation.Currency);
            //            double totalPriceVND = returnprice;
            //            double totalPriceCYN = 0;
            //            totalPriceCYN = returnprice / currency;

            //            var accChangeData = AccountController.GetByUsername(username_current);
            //            if (accChangeData != null)
            //            {

            //                if (checkIsChinaCome == true)
            //                {
            //                    //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
            //                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
            //                    if (smallpackages.Count > 0)
            //                    {
            //                        bool isChuaVekhoTQ = true;
            //                        var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
            //                        var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
            //                        var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
            //                        double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
            //                        if (che >= sp_main.Count)
            //                        {
            //                            isChuaVekhoTQ = false;
            //                        }
            //                        if (isChuaVekhoTQ == false)
            //                        {
            //                            TransportationOrderController.UpdateStatus(tID, 4, currentDate, username_current);
            //                        }
            //                    }
            //                }
            //            }
            //            TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
            //            return "1";
            //        }
            //        else
            //        {
            //            return "1";
            //        }
            //        return "1";
            //    }
            //}
            //return "none";
            #endregion
            #region Cách mới không tính cân nặng
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByID(packageID);
            if (package != null)
            {
                int khotqID = 0;
                var acckiemkho = AccountController.GetByUsername(username_current);
                if (acckiemkho != null)
                {
                    khotqID = acckiemkho.ID;
                }
                SmallPackageController.UpdateKhoTQID(package.ID, khotqID, username_current, currentDate);
                SmallPackageController.UpdateWeightStatus(package.ID, 0, status, 0);

                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    int orderID = mainorder.ID;
                    int warehouse = mainorder.ReceivePlace.ToInt(1);
                    int shipping = Convert.ToInt32(mainorder.ShippingType);

                    bool checkIsChinaCome = true;
                    double totalweight = 0;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 2)
                                checkIsChinaCome = false;
                            if (p.OrderTransactionCode != barcode)
                            {
                                totalweight += Convert.ToDouble(p.Weight);
                            }
                        }
                    }
                    MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        if (status == 2)
                        {

                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);


                        }
                        if (checkIsChinaCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho TQ", 8, currentDate);
                        }
                    }

                    return "1";
                }
                else
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, 0, status, 0);
                    var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                    if (transportation != null)
                    {
                        #region Cách cũ
                        //int tID = transportation.ID;
                        //int warehouseFrom = Convert.ToInt32(transportation.WarehouseFromID);
                        //int warehouse = Convert.ToInt32(transportation.WarehouseID);
                        //int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                        //bool checkIsChinaCome = true;
                        //double totalweight = 0;
                        //double returnprice = 0;
                        //double totalWeight = 0;
                        //bool checkoutweight = false;
                        //var smallpackage = SmallPackageController.GetByMainOrderID(tID);
                        //if (smallpackage.Count > 0)
                        //{

                        //    foreach (var item in smallpackage)
                        //    {
                        //        double weight = Convert.ToDouble(item.Weight);
                        //        if (weight >= 20)
                        //        {
                        //            checkoutweight = true;
                        //        }
                        //        totalWeight += Convert.ToDouble(item.Weight);
                        //    }
                        //    if (warehouse != 4)
                        //    {
                        //        var feet = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                        //        if (feet.Count > 0)
                        //        {
                        //            foreach (var f in feet)
                        //            {
                        //                if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                        //                {
                        //                    returnprice = totalWeight * Convert.ToDouble(f.Price);
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (checkoutweight == false)
                        //        {
                        //            var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 1, false);
                        //            foreach (var f in fee)
                        //            {
                        //                if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                        //                {
                        //                    returnprice = totalWeight * Convert.ToDouble(f.Price);
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            var fee = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(4, 2, false);
                        //            foreach (var f in fee)
                        //            {
                        //                if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                        //                {
                        //                    returnprice = totalWeight * Convert.ToDouble(f.Price);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));



                        //double currency = Convert.ToDouble(transportation.Currency);
                        //double totalPriceVND = returnprice;
                        //double totalPriceCYN = 0;
                        //totalPriceCYN = returnprice / currency;

                        //var accChangeData = AccountController.GetByUsername(username_current);
                        //if (accChangeData != null)
                        //{

                        //    if (checkIsChinaCome == true)
                        //    {
                        //        //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                        //        var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                        //        if (smallpackages.Count > 0)
                        //        {
                        //            bool isChuaVekhoTQ = true;
                        //            var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                        //            var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                        //            var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                        //            double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                        //            if (che >= sp_main.Count)
                        //            {
                        //                isChuaVekhoTQ = false;
                        //            }
                        //            if (isChuaVekhoTQ == false)
                        //            {
                        //                TransportationOrderController.UpdateStatus(tID, 4, currentDate, username_current);
                        //            }
                        //        }
                        //    }
                        //}
                        //TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
                        #endregion
                        #region Cách mới
                        int tID = transportation.ID;
                        TransportationOrderNewController.UpdateStatus(tID, 3, currentDate, username_current);
                        #endregion

                        return "1";
                    }
                    else
                    {
                        return "1";
                    }
                }
            }
            return "none";
            #endregion

        }
        [WebMethod]
        public static string CheckOrderShopCode(string ordershopcode, string packagecode)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_check = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user_check = AccountController.GetByUsername(username_check);
                if (user_check != null)
                {
                    int userRole_check = Convert.ToInt32(user_check.RoleID);
                    if (userRole_check == 0 || userRole_check == 2 || userRole_check == 4)
                    {
                        if (string.IsNullOrEmpty(packagecode))
                        {
                            packagecode = "00-" + PJUtils.GetRandomStringByDateTime();
                        }

                        string kq = SmallPackageController.InsertWithOrderWebCode(0, 0, packagecode, "", 0, 0, 0, 2, ordershopcode,
                            DateTime.Now, username_check);
                        if (kq.ToInt(0) > 0)
                        {
                            int packageID = kq.ToInt(0);
                            List<OrderGet> ogs = new List<OrderGet>();
                            OrderGet o = new OrderGet();
                            o.ID = packageID;
                            o.UID = 0;
                            o.BigPackageID = 0;
                            o.BarCode = packagecode;
                            o.TotalWeight = "0";
                            o.Status = 2;
                            int mainOrderID = 0;
                            o.MainorderID = mainOrderID;
                            o.Soloaisanpham = "0";
                            double totalProductQuantity = 0;
                            o.Soluongsanpham = totalProductQuantity.ToString();
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";

                            var listb = BigPackageController.GetAllNotHuy();
                            if (listb.Count > 0)
                            {
                                o.ListBig = listb;
                            }
                            ogs.Add(o);
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(ogs);
                        }

                    }
                }

            }
            return "none";
        }
        [WebMethod]
        public static string PriceBarcode(string barcode)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var user = AccountController.GetByUsername(username);
                if (user != null)
                {
                    int userRole = Convert.ToInt32(user.RoleID);

                    if (userRole == 0 || userRole == 2 || userRole == 4)
                    {
                        if (!string.IsNullOrEmpty(barcode))
                        {
                            string barcodeIMG = "/Uploads/smallpackagebarcode/" + barcode + ".gif";
                            Bitmap barCode = PJUtils.CreateBarcode1(barcode);
                            barCode.Save(HttpContext.Current.Server.MapPath("~" + barcodeIMG + ""), ImageFormat.Gif);
                            return barcodeIMG;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                        return "none";

                }
                else
                {
                    return "none";
                }

            }
            else
            {
                return "none";
            }

        }
        public class OrderGet
        {
            public int ID { get; set; }
            public int MainorderID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public double Wallet { get; set; }
            public string OrderShopCode { get; set; }
            public string BarCode { get; set; }
            public string TotalWeight { get; set; }
            public string TotalPriceVND { get; set; }
            public double TotalPriceVNDNum { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public string Soloaisanpham { get; set; }
            public string Soluongsanpham { get; set; }
            public int Status { get; set; }
            public int BigPackageID { get; set; }
            public List<tbl_BigPackage> ListBig { get; set; }
            public int orderType { get; set; }
        }
    }
}