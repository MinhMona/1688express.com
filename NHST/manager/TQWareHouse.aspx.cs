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

namespace NHST.manager
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
                    if (ac.RoleID != 4 && ac.RoleID != 0)
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
        public static string UpdateQuantity(string barcode, string quantity, int status, int BigPackageID)
        {
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
                    var usercreate = AccountController.GetByID(Convert.ToInt32(mainorder.UID));
                    int PlaceReceive = 1;
                    if (!string.IsNullOrEmpty(mainorder.ReceivePlace))
                    {
                        if (mainorder.ReceivePlace == "Kho Hà Nội")
                        {
                            PlaceReceive = 1;
                        }
                        else
                        {
                            PlaceReceive = 2;
                        }
                    }
                    else
                    {
                        PlaceReceive = 1;
                    }
                    double FeeWeight = 0;
                    double FeeWeightDiscount = 0;
                    double ckFeeWeight = 0;
                    ckFeeWeight = Convert.ToDouble(UserLevelController.GetByID(usercreate.LevelID.ToString().ToInt()).FeeWeight.ToString());
                    List<tbl_FeeWeightTQVN> listfee = new List<tbl_FeeWeightTQVN>();
                    if (PlaceReceive == 1)
                    {
                        listfee = FeeWeightTQVNController.GetByReceivePlace("Kho Hà Nội");

                    }
                    else
                    {
                        listfee = FeeWeightTQVNController.GetByReceivePlace("Kho Việt Trì");
                    }
                    if (listfee.Count > 0)
                    {
                        foreach (var item in listfee)
                        {
                            if (totalweight >= item.WeightFrom && totalweight <= item.WeightTo)
                            {
                                FeeWeight = totalweight * Convert.ToDouble(item.Amount);
                            }
                        }
                    }
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
        }
    }
}