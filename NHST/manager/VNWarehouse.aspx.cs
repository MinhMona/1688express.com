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
    public partial class VNWarehouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userLoginSystem"] = "khovn";
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
                    if (ac.RoleID != 5 && ac.RoleID != 0)
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
        public static string UpdateStatus(string barcode, int status)
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
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Kiemdem { get; set; }
            public string Donggo { get; set; }
        }
    }
}