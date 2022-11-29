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

namespace NHST.Admin
{
    public partial class VNWarehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "khovn";
            if (!IsPostBack)
            {
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    hdfCurrency.Value = config.AgentCurrency.ToString();
                }
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 5 && ac.RoleID != 0 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        [WebMethod]
        public static string GetCode(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, Convert.ToDouble(package.Weight), 3, Convert.ToInt32(package.BigPackageID));
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    OrderGet o = new OrderGet();
                    o.ID = package.ID;
                    o.BarCode = package.OrderTransactionCode;
                    o.TotalWeight = package.Weight.ToString();
                    o.Status = Convert.ToInt32(package.Status);
                    o.MainorderID = Convert.ToInt32(package.MainOrderID);
                    o.Fullname = mainorder.FullName;
                    o.Phone = mainorder.Phone;
                    o.Email = mainorder.Email;
                    o.Address = mainorder.Address;
                    if (mainorder.IsCheckProduct == true)
                        o.Kiemdem = "Có";
                    else
                        o.Kiemdem = "Không";
                    if (mainorder.IsPacked == true)
                        o.Donggo = "Có";
                    else
                        o.Donggo = "Không";

                    bool checkIsChinaCome = true;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 3)
                                checkIsChinaCome = false;
                        }
                    }
                    bool checkIsAllVN = true;
                    int bigpackid = Convert.ToInt32(package.BigPackageID);
                    var bigpacage = SmallPackageController.GetBuyBigPackageID(bigpackid, "");
                    if (bigpacage.Count > 0)
                    {
                        foreach (var item in bigpacage)
                        {
                            if (item.Status < 3)
                                checkIsAllVN = false;
                        }
                    }

                    DateTime currentDate = DateTime.Now;
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode + "</strong> của đơn hàng ID là: " + mainorder.ID
                                               + ", là: Đã về kho đích", 8, currentDate);

                        if (checkIsAllVN == true)
                        {
                            BigPackageController.UpdateStatus(bigpackid, 2, DateTime.Now, username_current);
                        }
                        if (checkIsChinaCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
                        }
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o);
                }
                else return "none";
            }
            else return "none";
        }
        [WebMethod]
        public static string GetCodeInfo_old(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    OrderGet o = new OrderGet();
                    o.ID = package.ID;
                    o.BarCode = package.OrderTransactionCode;
                    o.TotalWeight = package.Weight.ToString();
                    o.Status = Convert.ToInt32(package.Status);
                    o.MainorderID = Convert.ToInt32(package.MainOrderID);
                    o.Fullname = mainorder.FullName;
                    o.Phone = mainorder.Phone;
                    o.Email = mainorder.Email;
                    o.Address = mainorder.Address;
                    if (mainorder.IsCheckProduct == true)
                        o.Kiemdem = "Có";
                    else
                        o.Kiemdem = "Không";
                    if (mainorder.IsPacked == true)
                        o.Donggo = "Có";
                    else
                        o.Donggo = "Không";

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o);
                }
                else return "none";
            }
            else return "none";
        }
        [WebMethod]
        public static string GetCodeInfo(string barcode)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
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
                        int UID = Convert.ToInt32(mainorder.UID);
                        o.UID = UID;
                        var u = AccountController.GetByID(UID);
                        if (u != null)
                        {
                            o.Username = u.Username;
                        }
                        o.OrderDate = string.Format("{0:dd/MM/yyyy}", mainorder.CreatedDate);
                        o.BarCode = package.OrderTransactionCode;
                        o.TotalWeight = package.Weight.ToString();
                        o.Status = Convert.ToInt32(package.Status);
                        int mainOrderID = Convert.ToInt32(package.MainOrderID);
                        o.MainorderID = mainOrderID;
                        o.Fullname = mainorder.FullName;
                        o.Phone = mainorder.Phone;
                        o.Email = mainorder.Email;
                        o.Address = mainorder.Address;
                        if (mainorder.IsCheckProduct == true)
                            o.Kiemdem = "Có";
                        else
                            o.Kiemdem = "Không";
                        if (mainorder.IsPacked == true)
                            o.Donggo = "Có";
                        else
                            o.Donggo = "Không";
                        o.orderType = 1;
                        o.StaffNote = package.StaffNote;
                        o.CuocvattuCYN = 0;
                        o.CuocvattuVND = 0;
                        o.HangDBCYN = 0;
                        o.HangDBVND = 0;
                        ogs.Add(o);
                    }
                    else
                    {
                        var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (transportation != null)
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            int UID = Convert.ToInt32(transportation.UID);
                            o.UID = UID;
                            var u = AccountController.GetByID(UID);
                            if (u != null)
                            {
                                o.Username = u.Username;
                            }
                            o.OrderDate = string.Format("{0:dd/MM/yyyy}", transportation.CreatedDate);
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            int mainOrderID = Convert.ToInt32(package.TransportationOrderID);
                            o.MainorderID = transportation.ID;
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";

                            double CuocvattuCYN = 0;
                            double CuocvattuVND = 0;
                            double HangDBCYN = 0;
                            double HangDBVND = 0;

                            if (transportation.AdditionFeeCYN.ToFloat(0) > 0)
                            {
                                CuocvattuCYN = Convert.ToDouble(transportation.AdditionFeeCYN);
                            }
                            if (transportation.AdditionFeeVND.ToFloat(0) > 0)
                            {
                                CuocvattuVND = Convert.ToDouble(transportation.AdditionFeeVND);
                            }
                            if (transportation.SensorFeeCYN.ToFloat(0) > 0)
                            {
                                HangDBCYN = Convert.ToDouble(transportation.SensorFeeCYN);
                            }
                            if (transportation.SensorFeeeVND.ToFloat(0) > 0)
                            {
                                HangDBVND = Convert.ToDouble(transportation.SensorFeeeVND);
                            }

                            o.CuocvattuCYN = CuocvattuCYN;
                            o.CuocvattuVND = CuocvattuVND;
                            o.HangDBCYN = HangDBCYN;
                            o.HangDBVND = HangDBVND;
                            o.orderType = 2;
                            o.StaffNote = package.StaffNote;
                            var listb = BigPackageController.GetAllNotHuy();
                            ogs.Add(o);
                        }
                        else
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            o.UID = 0;
                            o.Username = "Chưa xác định";
                            o.OrderDate = "Chưa xác định";
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            int mainOrderID = Convert.ToInt32(package.MainOrderID);
                            o.MainorderID = 0;
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";
                            o.orderType = 3;
                            o.StaffNote = package.StaffNote;
                            o.CuocvattuCYN = 0;
                            o.CuocvattuVND = 0;
                            o.HangDBCYN = 0;
                            o.HangDBVND = 0;
                            var listb = BigPackageController.GetAllNotHuy();
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
        public static string UpdateStatus_NotWeight(string barcode, int status)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                SmallPackageController.UpdateWeightStatus(package.ID, Convert.ToDouble(package.Weight), status, Convert.ToInt32(package.BigPackageID));
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    bool checkIsChinaCome = true;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 3)
                                checkIsChinaCome = false;

                        }
                    }
                    if (checkIsChinaCome == true)
                        MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                    return "1";
                }
                else return "none";
            }

            return "none";
        }
        [WebMethod]
        public static string UpdateQuantity(string barcode, string quantity, int status,
            int BigPackageID, int packageID, string staffNote, string packageadditionfeeCYN,
            string packageadditionfeeVND, string packageSensorCYN, string packageSensorVND)
        {
            double additionfeeCYN = 0;
            double additionfeeVND = 0;
            double sensorCYN = 0;
            double sensorVND = 0;

            if (packageadditionfeeCYN.ToFloat(0) > 0)
            {
                additionfeeCYN = Convert.ToDouble(packageadditionfeeCYN);
            }
            if (packageadditionfeeVND.ToFloat(0) > 0)
            {
                additionfeeVND = Convert.ToDouble(packageadditionfeeVND);
            }
            if (packageSensorCYN.ToFloat(0) > 0)
            {
                sensorCYN = Convert.ToDouble(packageSensorCYN);
            }
            if (packageSensorVND.ToFloat(0) > 0)
            {
                sensorVND = Convert.ToDouble(packageSensorVND);
            }


            #region Cách cũ có tính cân nặng
            //string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            //DateTime currentDate = DateTime.Now;
            //var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            //if (package != null)
            //{
            //    SmallPackageController.UpdateWeightStatus(package.ID, quantity.ToFloat(0), status, BigPackageID);
            //    SmallPackageController.UpdateDateInLasteWareHouse(package.ID, currentDate);
            //    SmallPackageController.UpdateStaffNoteInLasteWareHouse(package.ID, staffNote);
            //    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
            //    if (mainorder != null)
            //    {
            //        int orderID = mainorder.ID;
            //        int warehouse = mainorder.ReceivePlace.ToInt(1);
            //        int shipping = Convert.ToInt32(mainorder.ShippingType);

            //        bool checkIsWarehouseCome = false;
            //        double totalweight = Convert.ToDouble(quantity);
            //        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
            //        if (packages.Count > 0)
            //        {
            //            foreach (var p in packages)
            //            {
            //                if (p.Status == 3)
            //                    checkIsWarehouseCome = true;
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
            //                if (checkoutweight == false)
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

            //        MainOrderController.UpdateFee(mainorder.ID, Deposit.ToString(), FeeShipCN.ToString(),
            //            FeeBuyPro.ToString(), FeeWeight.ToString(),
            //            IsCheckProductPrice.ToString(),
            //            IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), TotalPriceVND.ToString());
            //        MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
            //        var accChangeData = AccountController.GetByUsername(username_current);
            //        if (accChangeData != null)
            //        {
            //            if (status == 3)
            //            {

            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                               " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
            //                               + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
            //            }
            //            if (checkIsWarehouseCome == true)
            //            {
            //                MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
            //                HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
            //                                   " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);

            //                NotificationController.Inser(accChangeData.ID, accChangeData.Username, usercreate.ID,
            //                                            usercreate.Username, mainorder.ID,
            //                                            "Đơn hàng: " + mainorder.ID + " đã về kho bạn yêu cầu nhận.", 0,
            //                                           1, currentDate, accChangeData.Username);
            //            }
            //        }

            //        return "1";
            //    }
            //    else
            //    {
            //        SmallPackageController.UpdateWeightStatus(package.ID, quantity.ToFloat(0), status, BigPackageID);
            //        SmallPackageController.UpdateDateInLasteWareHouse(package.ID, currentDate);
            //        SmallPackageController.UpdateStaffNoteInLasteWareHouse(package.ID, staffNote);
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
            #region Cách mới không có tính cân nặng
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            double quantityin = 0;
            if (quantity.ToFloat(0) > 0)
                quantityin = Convert.ToDouble(quantity);
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                int khoVNID = 0;
                var acckiemkho = AccountController.GetByUsername(username_current);
                if (acckiemkho != null)
                {
                    khoVNID = acckiemkho.ID;
                }
                SmallPackageController.UpdateKhoVNID(package.ID, khoVNID, username_current, currentDate);
                SmallPackageController.UpdateWeightStatus(package.ID, quantityin, status, BigPackageID);
                SmallPackageController.UpdateDateInLasteWareHouse(package.ID, currentDate);
                SmallPackageController.UpdateStaffNoteInLasteWareHouse(package.ID, staffNote);
                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                if (mainorder != null)
                {
                    int orderID = mainorder.ID;
                    bool checkIsWarehouseCome = false;
                    double totalweight = Convert.ToDouble(quantity);
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status == 3)
                                checkIsWarehouseCome = true;
                            if (p.OrderTransactionCode != barcode)
                            {
                                totalweight += Convert.ToDouble(p.Weight);
                            }
                        }
                    }
                    var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));
                    MainOrderController.UpdateTotalWeight(mainorder.ID, totalweight.ToString(), totalweight.ToString());
                    var accChangeData = AccountController.GetByUsername(username_current);
                    if (accChangeData != null)
                    {
                        if (status == 3)
                        {
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                           " đã đổi trạng thái của mã vận đơn: <strong>" + barcode
                                           + "</strong> của đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);
                        }
                        if (checkIsWarehouseCome == true)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 7);
                            HistoryOrderChangeController.Insert(mainorder.ID, accChangeData.ID, accChangeData.Username, accChangeData.Username +
                                               " đã đổi trạng thái đơn hàng ID là: " + mainorder.ID + ", là: Đã về kho đích", 8, currentDate);

                            NotificationController.Inser(accChangeData.ID, accChangeData.Username, usercreate.ID,
                                                        usercreate.Username, mainorder.ID,
                                                        "Đơn hàng: " + mainorder.ID + " của quý khách đã có 1 kiện hàng về kho. Để biết chi tiết quý khách vui lòng Click vào đây.", 0,
                                                       1, currentDate, accChangeData.Username);
                        }
                    }

                    return "1";
                }
                else
                {
                    SmallPackageController.UpdateWeightStatus(package.ID, quantityin, status, BigPackageID);
                    SmallPackageController.UpdateDateInLasteWareHouse(package.ID, currentDate);
                    SmallPackageController.UpdateStaffNoteInLasteWareHouse(package.ID, staffNote);
                    #region Cách cũ
                    //var transportation = TransportationOrderController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                    //if (transportation != null)
                    //{
                    //    int tID = transportation.ID;
                    //    int warehouseFrom = Convert.ToInt32(transportation.WarehouseFromID);
                    //    int warehouse = Convert.ToInt32(transportation.WarehouseID);
                    //    int shipping = Convert.ToInt32(transportation.ShippingTypeID);

                    //    bool checkIsChinaCome = true;
                    //    double totalweight = 0;
                    //    double returnprice = 0;
                    //    double totalWeight = 0;
                    //    bool checkoutweight = false;
                    //    var smallpackage = SmallPackageController.GetByMainOrderID(tID);
                    //    if (smallpackage.Count > 0)
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
                    //        if (warehouse != 4)
                    //        {
                    //            var feet = WarehouseFeeController.GetAllWithWarehouseIDAndTypeAndIsHidden(warehouse, shipping, false);
                    //            if (feet.Count > 0)
                    //            {
                    //                foreach (var f in feet)
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
                    //    var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));



                    //    double currency = Convert.ToDouble(transportation.Currency);
                    //    double totalPriceVND = returnprice;
                    //    double totalPriceCYN = 0;
                    //    totalPriceCYN = returnprice / currency;

                    //    var accChangeData = AccountController.GetByUsername(username_current);
                    //    if (accChangeData != null)
                    //    {

                    //        if (checkIsChinaCome == true)
                    //        {
                    //            //MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 6);
                    //            var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                    //            if (smallpackages.Count > 0)
                    //            {
                    //                bool isChuaVekhoTQ = true;
                    //                var sp_main = smallpackages.Where(s => s.IsTemp != true).ToList();
                    //                var sp_support_isvekhotq = smallpackages.Where(s => s.IsTemp == true && s.Status > 1).ToList();
                    //                var sp_main_isvekhotq = smallpackages.Where(s => s.IsTemp != true && s.Status > 1).ToList();
                    //                double che = sp_support_isvekhotq.Count + sp_main_isvekhotq.Count;
                    //                if (che >= sp_main.Count)
                    //                {
                    //                    isChuaVekhoTQ = false;
                    //                }
                    //                if (isChuaVekhoTQ == false)
                    //                {
                    //                    TransportationOrderController.UpdateStatus(tID, 4, currentDate, username_current);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    TransportationOrderController.UpdateTotalWeightTotalPrice(tID, totalweight, totalPriceVND, currentDate, username_current);
                    //    return "1";
                    //}
                    //else
                    //{
                    //    return "1";
                    //}
                    #endregion
                    #region Cách mới
                    var transportation = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                    if (transportation != null)
                    {
                        var accChangeData = AccountController.GetByUsername(username_current);
                        if (accChangeData != null)
                        {
                            int tID = transportation.ID;
                            var usercreate = AccountController.GetByID(Convert.ToInt32(transportation.UID));
                            TransportationOrderNewController.UpdateStatus(tID, 4, currentDate, username_current);
                            TransportationOrderNewController.UpdateWeight(tID, quantityin.ToString(), currentDate, username_current);
                            TransportationOrderNewController.UpdateFee(tID, additionfeeCYN.ToString(), additionfeeVND.ToString(),
                                sensorCYN.ToString(), sensorVND.ToString());
                            TransportationOrderNewController.UpdateDateInVNWareHouse(tID, currentDate);

                            NotificationController.Inser(accChangeData.ID, accChangeData.Username, usercreate.ID,
                                                                       usercreate.Username, 0,
                                                                       "Đơn hàng VCH ID: " + tID + " đã có kiện hàng về đến kho VN.", 0,
                                                                      9, currentDate, accChangeData.Username);
                        }

                        return "1";
                    }
                    else
                    {
                        return "1";
                    }
                    #endregion

                }
            }
            return "none";
            #endregion

        }
        [WebMethod]
        public static string addNewPackage(int ordertype, string packageOrderCode, string packageUsername,
            string packageOrderCodeTaobao, string packageCode, string packageWeight, string packageStaffNote,
            string packageadditionfeeCYN, string packageadditionfeeVND, string packageSensorCYN,
            string packageSensorVND)
        {
            string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var account = AccountController.GetByUsername(username_current);
            if (account != null)
            {
                if (account.RoleID == 5 || account.RoleID == 0 || account.RoleID == 2)
                {
                    var account_Khach = AccountController.GetByUsername(packageUsername);
                    if (account_Khach != null)
                    {
                        int UIDKhach = account_Khach.ID;
                        if (ordertype == 1)
                        {
                            int mOID = 0;
                            if (packageOrderCode.ToInt(0) > 0)
                                mOID = packageOrderCode.ToInt(0);
                            else
                                return "vuilongnhapmadonhang";

                            var mainO = MainOrderController.GetAllByID(mOID);
                            if (mainO != null)
                            {
                                if (mainO.UID == UIDKhach)
                                {
                                    if (!string.IsNullOrEmpty(packageOrderCodeTaobao))
                                    {
                                        var ordershopcode = OrderShopCodeController.GetByOrderShop(packageOrderCodeTaobao);
                                        if (ordershopcode != null)
                                        {
                                            if (ordershopcode.MainOrderID == mOID)
                                            {
                                                if (!string.IsNullOrEmpty(packageCode))
                                                {
                                                    var smallpackage = SmallPackageController.GetByOrderTransactionCode(packageCode);
                                                    if (smallpackage == null)
                                                    {
                                                        double weight = 0;
                                                        if (packageWeight.ToFloat(0) > 0)
                                                            weight = Convert.ToDouble(packageWeight);
                                                        string kq = SmallPackageController.InsertInVNWithMainOrder(mOID, 0, packageCode, 0, weight,
                                                            0, 3, currentDate, username_current, currentDate, ordershopcode.ID, packageStaffNote,
                                                            account.ID, currentDate);
                                                        if (kq.ToInt(0) > 0)
                                                        {
                                                            NotificationController.Inser(account.ID, account.Username, account_Khach.ID,
                                                                                        account_Khach.Username, 0,
                                                                                        "Đơn hàng mua hộ ID: " + mOID + " đã có kiện hàng về đến kho VN.", 0,
                                                                                       9, currentDate, account.Username);
                                                            return packageCode;
                                                        }
                                                        else
                                                        {
                                                            return "khongthetaomoi";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return "makiendatontai";
                                                    }
                                                }
                                                else
                                                {
                                                    return "khongdetrongmavandon";
                                                }
                                            }
                                            else
                                            {
                                                return "madonhangtaobaokhongthuocmadonhanghethong";
                                            }
                                        }
                                        else
                                        {
                                            return "madonhangtaobaokhongtontai";
                                        }
                                    }
                                    else
                                    {

                                        return "chuanhapmadonhangtaobao";
                                    }
                                }
                                else
                                {
                                    return "madonhangkhongthuocvekhach";
                                }
                            }
                            else return "donhangkhongtontai";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(packageCode))
                            {
                                var smallpackage = SmallPackageController.GetByOrderTransactionCode(packageCode);
                                if (smallpackage == null)
                                {
                                    double weight = 0;
                                    if (packageWeight.ToFloat(0) > 0)
                                        weight = Convert.ToDouble(packageWeight);

                                    double additionfeeCYN = 0;
                                    double additionfeeVND = 0;
                                    double sensorCYN = 0;
                                    double sensorVND = 0;

                                    if (packageadditionfeeCYN.ToFloat(0) > 0)
                                    {
                                        additionfeeCYN = Convert.ToDouble(packageadditionfeeCYN);
                                    }
                                    if (packageadditionfeeVND.ToFloat(0) > 0)
                                    {
                                        additionfeeVND = Convert.ToDouble(packageadditionfeeVND);
                                    }
                                    if (packageSensorCYN.ToFloat(0) > 0)
                                    {
                                        sensorCYN = Convert.ToDouble(packageSensorCYN);
                                    }
                                    if (packageSensorVND.ToFloat(0) > 0)
                                    {
                                        sensorVND = Convert.ToDouble(packageSensorVND);
                                    }

                                    string tID = TransportationOrderNewController.Insert(UIDKhach, packageUsername, weight.ToString(), "0",
                                        additionfeeCYN.ToString(), additionfeeVND.ToString(), "0", "0", "0",
                                        "0", sensorCYN.ToString(), sensorVND.ToString(), 0, packageCode, 1, "", "", "0", "0", currentDate, username_current);
                                    int packageID = 0;
                                    string kq = SmallPackageController.InsertWithTransportationID(tID.ToInt(0), 0, packageCode, "",
                                    0, weight, 0, 1, currentDate, username_current);
                                    packageID = kq.ToInt(0);
                                    if (packageID > 0)
                                    {
                                        TransportationOrderNewController.UpdateSmallPackageID(tID.ToInt(0), packageID);
                                        NotificationController.Inser(account.ID, account.Username, account_Khach.ID,
                                                                    account_Khach.Username, 0,
                                                                    "Đơn hàng VCH ID: " + tID + " đã có kiện hàng về đến kho VN.", 0,
                                                                   9, currentDate, account.Username);
                                        return packageCode;
                                    }
                                    else
                                    {
                                        return "khongthetaomoi";
                                    }
                                }
                                else
                                {
                                    return "makiendatontai";
                                }
                            }
                            else
                            {
                                return "khongdetrongmavandon";
                            }

                        }
                    }
                    else
                    {
                        return "usernamekhachkhongtontai";
                    }
                }
                else
                {
                    return "khongthetaomoi";
                }
            }
            return "khongthetaomoi";
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
            public int Status { get; set; }
            public string OrderDate { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public int orderType { get; set; }
            public string StaffNote { get; set; }
            public double CuocvattuCYN { get; set; }
            public double CuocvattuVND { get; set; }
            public double HangDBCYN { get; set; }
            public double HangDBVND { get; set; }
        }
    }
}