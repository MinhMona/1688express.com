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

namespace NHST.manager
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
                    if (ac.RoleID != 0 && ac.RoleID != 5)
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
                    o.ID = package.ID;
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
                            MainOrderController.UpdateStatus(mainorder.ID, Convert.ToInt32(mainorder.UID), 10);
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
        }
    }
}