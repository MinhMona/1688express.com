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
    public partial class transportationdetail : System.Web.UI.Page
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
                    else
                    {
                        if (RoleID == 4 || RoleID == 5 || RoleID == 8)
                        {
                            Response.Redirect("/admin/home.aspx");
                        }
                    }
                }
                LoadDDL();
                loaddata();
            }
        }
        public void LoadDDL()
        {
            var warehouse = WarehouseController.GetAllWithIsHidden(false);
            if (warehouse.Count > 0)
            {
                ddlReceivePlace.DataSource = warehouse;
                ddlReceivePlace.DataBind();
            }
        }
        public void loaddata()
        {
            var config = ConfigurationController.GetByTop1();
            double currency = 0;
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }
            var id = Convert.ToInt32(Request.QueryString["id"]);
            if (id > 0)
            {
                ViewState["ID"] = id;
                var t = TransportationOrderController.GetByID(id);
                if (t != null)
                {
                    int tID = t.ID;
                    #region lấy dữ liệu
                    int status = Convert.ToInt32(t.Status);
                    int warehouseFrom = Convert.ToInt32(t.WarehouseFromID);
                    int warehouseTo = Convert.ToInt32(t.WarehouseID);
                    int shippingType = Convert.ToInt32(t.ShippingTypeID);
                    double currency_addOrder = Convert.ToDouble(t.Currency);
                    double totalPriceVND = Convert.ToDouble(t.TotalPrice);
                    double totalPriceCYN = 0;
                    if (totalPriceVND > 0)
                        totalPriceCYN = totalPriceVND / currency_addOrder;
                    double deposited = Convert.ToDouble(t.Deposited);
                    double totalPackage = 0;
                    double totalWeight = 0;
                    StringBuilder htmlPackages = new StringBuilder();
                    var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                    if (smallpackages.Count > 0)
                    {
                        totalPackage = smallpackages.Count;
                        foreach (var p in smallpackages)
                        {
                            double weight = Convert.ToDouble(p.Weight);
                            htmlPackages.Append("<tr class=\"package-item\" data-code=\"" + p.OrderTransactionCode + "\" data-weight=\"" + weight + "\" data-package-id=\"" + p.ID + "\">");
                            htmlPackages.Append("   <td>" + p.ID + "</td>");
                            htmlPackages.Append("   <td><div class=\"product-infobox\">" + p.OrderTransactionCode + "</div></td>");
                            htmlPackages.Append("   <td>" + weight + " kg</td>");
                            if (status == 1)
                                htmlPackages.Append("   <td><span class=\"bg-red\">Chưa duyệt</span></td>");
                            else
                                htmlPackages.Append("   <td>" + PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(p.Status)) + "</td>");
                            //htmlPackages.Append("   <td class=\"hl-txt\"><a href=\"javascript:;\" class=\"left edit-btn\">Cập nhật</a></td>");
                            htmlPackages.Append("</tr>");
                            totalWeight += weight;
                        }
                    }
                    else
                    {
                        var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                        if (transportationDetail.Count > 0)
                        {
                            totalPackage = transportationDetail.Count;
                            foreach (var p in transportationDetail)
                            {
                                double weight = Convert.ToDouble(p.Weight);
                                htmlPackages.Append("<tr class=\"package-item\" data-code=\"" + p.TransportationOrderCode + "\" data-weight=\"" + weight + "\" data-package-id=\"" + p.ID + "\">");
                                htmlPackages.Append("   <td>" + p.ID + "</td>");
                                htmlPackages.Append("   <td><div class=\"product-infobox\">" + p.TransportationOrderCode + "</div></td>");
                                htmlPackages.Append("   <td>" + weight + " kg</td>");
                                htmlPackages.Append("   <td><span class=\"bg-red\">Chưa duyệt</span></td>");
                                //htmlPackages.Append("   <td class=\"hl-txt\"></td>");
                                htmlPackages.Append("</tr>");
                                totalWeight += weight;
                            }
                        }
                    }
                    ltrPackages.Text = htmlPackages.ToString();
                    #endregion

                    #region gán dữ liệu
                    hdfCurrency.Value = currency_addOrder.ToString();
                    hdfStatus.Value = status.ToString();
                    rTotalPrice.Value = totalPriceVND;
                    rTotalPriceCYN.Value = totalPriceCYN;
                    pDeposit.Value = deposited;
                    //ddlWarehouseFrom.SelectedValue = warehouseFrom.ToString();
                    ddlReceivePlace.SelectedValue = warehouseTo.ToString();
                    ddlShippingType.SelectedValue = shippingType.ToString();
                    ddlStatus.SelectedValue = status.ToString();
                    lblTotalPackage.Text = totalPackage.ToString();
                    lblTotalWeight.Text = string.Format("{0:N0}", totalWeight);
                    #endregion
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (ac != null)
            {
                if (ac.RoleID == 0)
                {
                    var id = Convert.ToInt32(ViewState["ID"]);
                    if (id > 0)
                    {
                        var t = TransportationOrderController.GetByID(id);
                        if (t != null)
                        {
                            int tID = t.ID;
                            double totalWeight = 0;
                            int warehouseFrom = 1;
                            int warehouseTo = ddlReceivePlace.SelectedValue.ToInt();
                            int shippingType = ddlShippingType.SelectedValue.ToInt();
                            int status = ddlStatus.SelectedValue.ToInt();
                            double currency = Convert.ToDouble(t.Currency);
                            int UID = Convert.ToInt32(t.UID);
                            double price = 0;
                            bool isExist = false;
                            double totalprice = 0;
                            var smallpackages = SmallPackageController.GetByTransportationOrderID(tID);
                            if (smallpackages.Count > 0)
                            {
                                foreach (var s in smallpackages)
                                {
                                    totalWeight += Convert.ToDouble(s.Weight);
                                }
                                isExist = true;
                            }
                            else
                            {
                                var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                                if (transportationDetail.Count > 0)
                                {
                                    foreach (var p in transportationDetail)
                                    {
                                        totalWeight += Convert.ToDouble(p.Weight);
                                    }
                                }
                            }

                            //var tf = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(warehouseFrom,
                            //            warehouseTo, shippingType, true);
                            //
                            //if (tf.Count > 0)
                            //{
                            //    foreach (var w in tf)
                            //    {
                            //        if (w.WeightFrom < totalWeight && totalWeight <= w.WeightTo)
                            //        {
                            //            price = Convert.ToDouble(w.Price);
                            //        }
                            //    }
                            //}
                            //totalprice = price * totalWeight * currency;
                            totalprice = Convert.ToDouble(rTotalPrice.Value);
                            TransportationOrderController.Update(tID, UID, t.Username, warehouseFrom, warehouseTo, shippingType,
                                    status, totalWeight, currency, totalprice, "", currentDate, username_current);
                            if (isExist == false)
                            {
                                var transportationDetail = TransportationOrderDetailController.GetByTransportationOrderID(tID);
                                if (transportationDetail.Count > 0)
                                {
                                    foreach (var p in transportationDetail)
                                    {
                                        SmallPackageController.InsertWithTransportationID(t.ID, 0, p.TransportationOrderCode, "",
                                            0, Convert.ToDouble(p.Weight), 0, 1, currentDate, username_current);
                                    }
                                }
                            }
                            if (status == 0)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.Delete(item.ID);
                                    }
                                }
                                double deposited = Convert.ToDouble(t.Deposited);
                                if (deposited > 0)
                                {
                                    var user_deposited = AccountController.GetByID(Convert.ToInt32(t.UID));
                                    if (user_deposited != null)
                                    {
                                        double wallet = Convert.ToDouble(user_deposited);
                                        double walletleft = wallet + deposited;
                                        AccountController.updateWallet(UID, walletleft, currentDate, username_current);
                                        HistoryPayWalletController.InsertTransportation(UID, username_current, 0, deposited,
                                        username_current + " nhận lại tiền của đơn hàng vận chuyển hộ: " + t.ID + ".",
                                        walletleft, 2, 11, currentDate, username_current, t.ID);
                                    }
                                }
                            }
                            else if (status == 1)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.Delete(item.ID);
                                    }
                                }
                            }
                            else if (status == 4)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 2, currentDate, username_current);
                                    }
                                }
                            }
                            else if (status == 5)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 3, currentDate, username_current);
                                    }
                                }
                            }
                            else if (status == 7)
                            {
                                var smallpacs = SmallPackageController.GetByTransportationOrderID(tID);
                                if (smallpacs.Count > 0)
                                {
                                    foreach (var item in smallpacs)
                                    {
                                        SmallPackageController.UpdateStatus(item.ID, 4, currentDate, username_current);
                                    }
                                }
                            }
                            PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                        }
                    }
                }
            }

        }

        #region webservice
        [WebMethod]
        public static string getPrice(double weight, int warehouseFrom, int warehouseTo, int shippingType)
        {
            var t = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(
                    warehouseTo, shippingType, true);
            double price = 0;
            double totalprice = 0;
            if (t.Count > 0)
            {
                foreach (var w in t)
                {
                    if (w.WeightFrom < weight && weight <= w.WeightTo)
                    {
                        price = Convert.ToDouble(w.Price);
                    }
                }
            }
            totalprice = price * weight;
            return totalprice.ToString();
        }
        #endregion
    }
}