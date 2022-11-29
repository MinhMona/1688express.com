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
    public partial class OutStock : System.Web.UI.Page
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
                    if (ac.RoleID != 0 && ac.RoleID != 5 && ac.RoleID != 2)
                        Response.Redirect("/trang-chu");
                }
            }
        }

        [WebMethod]
        public static string GetCode(string barcode)
        {
            var package = SmallPackageController.GetByOrderTransactionCode(barcode.Trim());
            if (package != null)
            {
                var reou = RequestOutStockController.GetBySmallpackageID(package.ID);
                if (reou != null)
                {
                    var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(package.MainOrderID));
                    if (mainorder != null)
                    {
                        //if (mainorder.Status == 9)
                        //{
                        //    OrderGet o = new OrderGet();
                        //    o.ID = package.ID;
                        //    o.BarCode = package.OrderTransactionCode;
                        //    o.TotalWeight = package.Weight.ToString();
                        //    o.Status = Convert.ToInt32(package.Status);
                        //    o.MainorderID = Convert.ToInt32(package.MainOrderID);
                        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
                        //    return serializer.Serialize(o);
                        //}
                        //else return "none";

                        OrderGet o = new OrderGet();
                        string username = "";
                        var account = AccountController.GetByID(Convert.ToInt32(mainorder.UID));
                        if(account!=null)
                        {
                            username = account.Username;
                        }
                        o.ID = package.ID;
                        o.Username = username;
                        o.BarCode = package.OrderTransactionCode;
                        o.TotalWeight = package.Weight.ToString();
                        o.Status = Convert.ToInt32(package.Status);
                        o.MainorderID = Convert.ToInt32(package.MainOrderID);
                        o.MainOrderStatus = Convert.ToInt32(mainorder.Status);
                        if (mainorder.IsCheckProduct == true)
                            o.Kiemdem = "Có";
                        else
                            o.Kiemdem = "Không";
                        if (mainorder.IsPacked == true)
                            o.Donggo = "Có";
                        else
                            o.Donggo = "Không";
                        o.OrderType = 1;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        return serializer.Serialize(o);
                    }
                    else
                    {
                        var t = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                        if (t != null)
                        {
                            OrderGet o = new OrderGet();
                            o.ID = package.ID;
                            o.Username = t.Username;
                            o.BarCode = package.OrderTransactionCode;
                            o.TotalWeight = package.Weight.ToString();
                            o.Status = Convert.ToInt32(package.Status);
                            o.MainorderID = Convert.ToInt32(t.ID);
                            o.MainOrderStatus = Convert.ToInt32(t.Status);
                            o.Kiemdem = "Không";
                            o.Donggo = "Không";
                            o.OrderType = 2;

                            double additionFeeCYN = 0;
                            double additionFeeVND = 0;
                            double sensorFeeCYN = 0;
                            double sensorFeeVND = 0;
                            if (t.AdditionFeeCYN.ToFloat(0) > 0)
                            {
                                additionFeeCYN = Convert.ToDouble(t.AdditionFeeCYN);
                            }
                            if (t.AdditionFeeVND.ToFloat(0) > 0)
                            {
                                additionFeeVND = Convert.ToDouble(t.AdditionFeeVND);
                            }
                            if (t.SensorFeeCYN.ToFloat(0) > 0)
                            {
                                sensorFeeCYN = Convert.ToDouble(t.SensorFeeCYN);
                            }
                            if (t.SensorFeeeVND.ToFloat(0) > 0)
                            {
                                sensorFeeVND = Convert.ToDouble(t.SensorFeeeVND);
                            }
                            o.AdditionFeeCYN = additionFeeCYN.ToString();
                            o.AdditionFeeVND = additionFeeVND.ToString();
                            o.SensorFeeCYN = sensorFeeCYN.ToString();
                            o.SensorFeeVND = sensorFeeVND.ToString();
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            return serializer.Serialize(o);
                        }
                        else return "none";
                    }
                }
                else
                {
                    return "chuathanhtoan";
                }
                //else return "none";
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
                    #region Lấy tiền của user
                    double wallet = 0;
                    var obj_user = AccountController.GetByID(Convert.ToInt32(mainorder.UID));
                    if (obj_user != null)
                    {
                        wallet = Convert.ToDouble(obj_user.Wallet);
                    }
                    #endregion
                    DateTime ngayvekho = Convert.ToDateTime(package.DateInLasteWareHouse);
                    TimeSpan ts = currentDate.Subtract(ngayvekho);
                    double weight = Convert.ToDouble(package.Weight);
                    double totalDays = ts.TotalDays;
                    double price = 0;
                    double maxDay = 0;
                    double priceluukho = 0;
                    var getPrice = InWareHousePriceController.GetAll("");
                    #region Tính phí lưu kho
                    //if (getPrice.Count > 0)
                    //{
                    //    foreach (var item in getPrice)
                    //    {
                    //        if (item.WeightFrom < weight && weight <= item.WeightTo)
                    //        {
                    //            price = Convert.ToDouble(item.PricePay);
                    //            maxDay = Convert.ToDouble(item.MaxDay);
                    //        }
                    //    }
                    //}
                    //if (totalDays > maxDay)
                    //{
                    //    double minusday = totalDays - maxDay;
                    //    priceluukho = price * weight * minusday;
                    //}
                    //if (priceluukho > 0)
                    //{
                    //    if (priceluukho <= wallet)
                    //    {
                    //        if (mainorder.Status == 9)
                    //        {
                    //            double walletleft = wallet - priceluukho;
                    //            AccountController.updateWallet(obj_user.ID, walletleft, currentDate, obj_user.Username);

                    //            //Cập nhật lại MainOrder                                                                
                    //            HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, mainorder.ID, priceluukho,
                    //                obj_user.Username + " thanh toán tiền lưu kho của đơn hàng: " + mainorder.ID + ".", walletleft,
                    //                1, 1, currentDate, obj_user.Username);

                    //            SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                    //            SmallPackageController.UpdateDateOutWH(package.ID, currentDate);

                    //            bool checkIsChinaCome = true;
                    //            var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    //            if (packages.Count > 0)
                    //            {
                    //                foreach (var p in packages)
                    //                {
                    //                    if (p.Status < 4)
                    //                        checkIsChinaCome = false;
                    //                }
                    //            }
                    //            if (checkIsChinaCome == true)
                    //                MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 10);


                    //            return "ok";
                    //        }
                    //        else return "none";
                    //    }
                    //    else
                    //    {
                    //        return priceluukho.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    if (mainorder.Status == 9)
                    //    {
                    //        SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                    //        SmallPackageController.UpdateDateOutWH(package.ID, currentDate);
                    //        bool checkIsChinaCome = true;
                    //        var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    //        if (packages.Count > 0)
                    //        {
                    //            foreach (var p in packages)
                    //            {
                    //                if (p.Status < 4)
                    //                    checkIsChinaCome = false;
                    //            }
                    //        }
                    //        if (checkIsChinaCome == true)
                    //            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 10);

                    //        var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                    //        if (check != null)
                    //        {
                    //            RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                    //        }
                    //        return "ok";
                    //        //int bigbackageID = Convert.ToInt32(package.BigPackageID);                        
                    //    }
                    //    else return "none";
                    //}
                    #endregion
                    #region Không tính phí lưu kho
                    #region có kiểm tra trạng thái
                    //if (mainorder.Status == 9)
                    //{
                    //    SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                    //    SmallPackageController.UpdateDateOutWH(package.ID, currentDate);
                    //    bool checkIsChinaCome = true;
                    //    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    //    if (packages.Count > 0)
                    //    {
                    //        foreach (var p in packages)
                    //        {
                    //            if (p.Status < 4)
                    //                checkIsChinaCome = false;
                    //        }
                    //    }
                    //    if (checkIsChinaCome == true)
                    //        MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 10);

                    //    var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                    //    if (check != null)
                    //    {
                    //        RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                    //    }
                    //    return "ok";
                    //    //int bigbackageID = Convert.ToInt32(package.BigPackageID);                        
                    //}
                    //else return "none";
                    #endregion
                    #region không kiểm tra trạng thái
                    SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                    SmallPackageController.UpdateDateOutWH(package.ID, currentDate);
                    bool checkIsChinaCome = true;
                    var packages = SmallPackageController.GetByMainOrderID(mainorder.ID);
                    if (packages.Count > 0)
                    {
                        foreach (var p in packages)
                        {
                            if (p.Status < 4)
                                checkIsChinaCome = false;
                        }
                    }
                    if (checkIsChinaCome == true)
                    {
                        MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 10);
                        var cust = AccountController.GetByID(Convert.ToInt32(mainorder.UID));
                        if (cust != null)
                        {
                            int cusID = cust.ID;
                            var cus_orders = MainOrderController.GetSuccessByCustomer(mainorder.ID);
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

                    var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                    if (check != null)
                    {
                        RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                    }
                    return "ok";
                    #endregion


                    #endregion
                }
                else
                {
                    var t = TransportationOrderNewController.GetByID(Convert.ToInt32(package.TransportationOrderID));
                    if (t != null)
                    {
                        #region có kiểm tra trạng thái
                        //if (t.Status == 5)
                        //{
                        //    SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                        //    SmallPackageController.UpdateDateOutWH(package.ID, currentDate);
                        //    TransportationOrderNewController.UpdateStatus(t.ID, 6, currentDate, username_current);
                        //    TransportationOrderNewController.UpdateDateExport(t.ID, currentDate, currentDate, username_current);

                        //    var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                        //    if (check != null)
                        //    {
                        //        RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                        //    }
                        //    return "ok";
                        //    //int bigbackageID = Convert.ToInt32(package.BigPackageID);                        
                        //}
                        //else return "none";
                        #endregion
                        #region Không kiểm tra trạng thái
                        SmallPackageController.UpdateStatus(package.ID, 4, currentDate, username_current);
                        SmallPackageController.UpdateDateOutWH(package.ID, currentDate);
                        TransportationOrderNewController.UpdateStatus(t.ID, 6, currentDate, username_current);
                        TransportationOrderNewController.UpdateDateExport(t.ID, currentDate, currentDate, username_current);

                        var check = RequestOutStockController.GetBySmallpackageID(package.ID);
                        if (check != null)
                        {
                            RequestOutStockController.UpdateStatus(check.ID, 2, currentDate, username_current);
                        }
                        return "ok";
                        #endregion

                    }
                    else
                        return "none";
                }
                //else return "none";
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
            public int MainOrderStatus { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
            public int OrderType { get; set; }
            public string AdditionFeeCYN { get; set; }
            public string AdditionFeeVND { get; set; }
            public string SensorFeeCYN { get; set; }
            public string SensorFeeVND { get; set; }
        }
    }
}