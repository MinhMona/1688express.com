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
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class chi_tiet_don_hang1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    loaddata();
                    LoadShippingTypeVN();
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }
        public void LoadShippingTypeVN()
        {
            string html = "";
            var s = ShippingTypeVNController.GetAllWithIsHidden("", false);
            if (s.Count > 0)
            {
                foreach (var item in s)
                {
                    html += item.ID + ":" + item.ShippingTypeVNName + "|";
                }
            }
            hdfListShippingVN.Value = html;


        }

        public void loaddata()
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            if (obj_user != null)
            {

                //if (obj_user.RoleID == 0)
                //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" />";
                //else
                //    ltr_currentUserImg.Text = "<img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" />";
                int uid = obj_user.ID;

                double UL_CKFeeBuyPro = 0;
                double UL_CKFeeWeight = 0;

                if (UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro.ToString().ToFloat(0) > 0)
                    UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                if (UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight.ToString().ToFloat(0) > 0)
                    UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);

                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
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
                    ViewState["ID"] = id;
                    Session["oID"] = id;
                    hdfmID.Value = id.ToString();
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        var config = ConfigurationController.GetByTop1();
                        double currency = 0;
                        double currency1 = 0;
                        if (config != null)
                        {
                            double currencyconfig = 0;
                            if (!string.IsNullOrEmpty(config.Currency))
                                currencyconfig = Convert.ToDouble(config.Currency);
                            currency = Math.Floor(currencyconfig);
                            currency1 = Math.Floor(currencyconfig);
                        }
                        ViewState["OID"] = id;
                        //if (o.IsHidden == true)
                        //{
                        //    Response.Redirect("/danh-sach-don-hang");
                        //}
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
                        double pricepro = Convert.ToDouble(o.PriceVND);
                        double servicefee = 0;
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item in adminfeebuypro)
                            {
                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                {
                                    servicefee = Convert.ToDouble(item.FeePercent) / 100;
                                }
                            }
                        }
                        ltrshopinfo.Text = "<span class=\"shop-info\">" + o.ShopName + " - Mã đơn hàng: " + o.ID + " </span>";
                        //if (pricepro < 1000000)
                        //{
                        //    servicefee = 0.05;
                        //}
                        //else if (pricepro >= 1000000 && pricepro < 30000000)
                        //{
                        //    servicefee = 0.04;
                        //}
                        //else if (pricepro >= 30000000 && pricepro < 50000000)
                        //{
                        //    servicefee = 0.035;
                        //}
                        //else if (pricepro >= 50000000 && pricepro < 100000000)
                        //{
                        //    servicefee = 0.03;
                        //}
                        //else if (pricepro >= 100000000 && pricepro < 200000000)
                        //{
                        //    servicefee = 0.025;
                        //}
                        //else if (pricepro >= 200000000)
                        //{
                        //    servicefee = 0.02;
                        //}
                        double feebpnotdc = pricepro * servicefee;
                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double userdadeposit = 0;
                        if (o.Deposit != null)
                            userdadeposit = Convert.ToDouble(o.Deposit);
                        //Kiểm tra đơn hàng hiển thị button hủy đơn hàng
                        int orderType = Convert.ToInt32(o.OrderType);
                        //if (orderType == 2 || orderType == 3)
                        if (orderType == 3)
                        {
                            pnOrderType23.Visible = true;
                            pnOrderType23_1.Visible = true;
                            ltrProductOrder.Visible = false;
                        }
                        else
                        {
                            pnOrderType23.Visible = false;
                            pnOrderType23_1.Visible = false;
                            ltrProductOrder.Visible = true;

                        }
                        int oStatus = Convert.ToInt32(o.Status);
                        if (oStatus == 0)
                        {
                            //if (o.OrderType == 1)
                            //{
                            //    pn.Visible = true;
                            //    ltrCancelorder.Text = "<a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"cancelOrder()\">Hủy đơn hàng</a>";
                            //}
                            pn.Visible = true;
                            if (o.OrderType != 2)
                            {
                                ltrCancelorder.Text = "<a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"cancelOrder()\">Hủy đơn hàng</a>";
                            }

                        }
                        else if (oStatus == 1)
                        {
                            //pn_sendcomment.Visible = false;
                        }
                        else if (oStatus == 7)
                        {
                            //if (o.OrderType == 3)
                            //{
                            //    if (o.IsCheckNotiPrice == true)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        if (obj_user.Wallet >= (Convert.ToDouble(o.TotalPriceVND) - userdadeposit))
                            //        {
                            //            pnthanhtoan.Visible = true;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (obj_user.Wallet >= (Convert.ToDouble(o.TotalPriceVND) - userdadeposit))
                            //    {
                            //        pnthanhtoan.Visible = true;
                            //    }
                            //}

                        }
                        double totalPrice = Convert.ToDouble(o.TotalPriceVND);

                        if (oStatus > 2)
                        {
                            if (o.OrderType == 3)
                            {
                                if (o.IsCheckNotiPrice == true)
                                {

                                }
                                else
                                {
                                    double leftmoney = totalPrice - userdadeposit;
                                    if (leftmoney > 100)
                                    {
                                        if (obj_user.Wallet >= (Convert.ToDouble(o.TotalPriceVND) - userdadeposit))
                                        {
                                            //pnthanhtoan.Visible = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                double leftmoney = totalPrice - userdadeposit;
                                if (leftmoney > 100)
                                {
                                    if (obj_user.Wallet >= (Convert.ToDouble(o.TotalPriceVND) - userdadeposit))
                                    {
                                        //pnthanhtoan.Visible = true;
                                    }
                                }
                            }
                        }
                        #region Generate Trạng thái
                        //<asp:ListItem Value="0" Text="Chưa đặt cọc"></asp:ListItem> bg-red
                        //<asp:ListItem Value="1" Text="Hủy đơn hàng"></asp:ListItem> bg-black
                        //<asp:ListItem Value="2" Text="Đã đặt cọc"></asp:ListItem> bg-bronze
                        //<asp:ListItem Value="5" Text="Đã mua hàng"></asp:ListItem> bg-green
                        //<asp:ListItem Value="6" Text="Đã nhận hàng tại TQ"></asp:ListItem> bg-green
                        //<asp:ListItem Value="7" Text="Đã nhận hàng tại kho đích"></asp:ListItem> bg-orange
                        //<asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem> bg-blue
                        //<asp:ListItem Value="10" Text="Đã hoàn thành"></asp:ListItem> bg-blue

                        StringBuilder htmlProcess = new StringBuilder();
                        if (oStatus == 0)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                            //htmlProcess.Append("<article class=\"step\">");
                            //htmlProcess.Append("    <h3 class=\"fz-24\">08</h3>");
                            //htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa gửi</h4></section>");                            
                            //htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 2)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 5)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 6)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 7)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 9)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        else if (oStatus == 10)
                        {
                            htmlProcess.Append("<article class=\"step active bg-red\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">01</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Chưa đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-bronze\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">02</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã đặt cọc</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">03</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã mua hàng</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-green\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">04</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đang chuyển về kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-orange\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">05</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã nhận hàng tại kho đích</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">06</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Khách đã thanh toán</h4></section>");
                            htmlProcess.Append("</article>");
                            htmlProcess.Append("<article class=\"step active bg-blue\">");
                            htmlProcess.Append("    <h3 class=\"fz-24\">07</h3>");
                            htmlProcess.Append("    <section class=\"status\"><h4 class=\"fz-14\">Đã hoàn thành</h4></section>");
                            htmlProcess.Append("</article>");
                        }
                        ltrstep.Text = htmlProcess.ToString();
                        #endregion

                        #region Lấy thông tin đơn hàng và thông tin người đặt                       
                        ltrOrderFee.Text += "<div class=\"order-panel\">";
                        ltrOrderFee.Text += " <div class=\"title\">Thông tin đơn hàng</div>";
                        ltrOrderFee.Text += " <div class=\"cont\">";
                        ltrOrderFee.Text += "     <dl>";
                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Phí cố định</strong></dt>";
                        if (o.OrderType == 3)
                        {
                            if (o.IsCheckNotiPrice == true)
                            {
                                ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd><span class=\"bg-yellow-gold\">Chờ báo giá</span></dd>";
                            }
                            else
                            {
                                ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</dd>";
                            }
                        }
                        else
                        {
                            ltrOrderFee.Text += "         <dt>Trạng thái đơn hàng</dt><dd>" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + "</dd>";
                        }

                        ltrOrderFee.Text += "         <dt>Tiền hàng trên web</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " VNĐ (<i class=\"fa fa-yen\"></i>" + Math.Round(Convert.ToDouble(o.PriceVND) / Convert.ToDouble(o.CurrentCNYVN), 2) + ")</dd>";
                        if (!string.IsNullOrEmpty(o.FeeShipCN))
                        {
                            double scn = Convert.ToDouble(o.FeeShipCN);
                            if (scn > 0)
                                ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>" + string.Format("{0:N0}", scn) + " VNĐ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", scn / Convert.ToDouble(o.CurrentCNYVN)) + ")</dd>";
                            else
                                ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí ship Trung Quốc</dt><dd>Đang cập nhật</dd>";

                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            double bp = Convert.ToDouble(o.FeeBuyPro);
                            double bpCYN = bp / currency;
                            if (bp > 0)
                            {
                                if (UL_CKFeeBuyPro > 0)
                                    ltrOrderFee.Text += "         <dt>Phí hải quan cố định (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " VNĐ)</dt><dd>" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>0</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>Đang cập nhật</dd>";


                        double orderWeight = 0;
                        double feeWeightCK = 0;
                        double feeWeight = 0;

                        if (o.OrderWeight.ToFloat(0) > 0)
                            orderWeight = Convert.ToDouble(o.OrderWeight);
                        if (o.FeeWeightCK.ToFloat(0) > 0)
                            feeWeightCK = Convert.ToDouble(o.FeeWeightCK);
                        if (o.FeeWeight.ToFloat(0) > 0)
                            feeWeight = Convert.ToDouble(o.FeeWeight);

                        ltrOrderFee.Text += "         <dt>Tổng cân nặng</dt><dd>" + orderWeight + " KG</dd>";
                        if (UL_CKFeeWeight > 0)
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", feeWeightCK) + " VNĐ)</dt><dd>" + string.Format("{0:N0}", feeWeight) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN</dt><dd>" + string.Format("{0:N0}", feeWeight) + " VNĐ</dd>";

                        //if (!string.IsNullOrEmpty(o.FeeWeight))
                        //{
                        //    double fw = Convert.ToDouble(o.FeeWeight);
                        //    if (fw > 0)
                        //        ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>" + string.Format("{0:N0}", fw) + " VNĐ</dd>";
                        //    else
                        //        ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>Đang cập nhật</dd>";
                        //}
                        //else
                        //    ltrOrderFee.Text += "         <dt>Phí cân nặng</dt><dd>Đang cập nhật</dd>";

                        double isCheckProductPrice = 0;
                        double isPackedPrice = 0;

                        if (o.IsCheckProductPrice.ToFloat(0) > 0)
                            isCheckProductPrice = Convert.ToDouble(o.IsCheckProductPrice);
                        if (o.IsPackedPrice.ToFloat(0) > 0)
                            isPackedPrice = Convert.ToDouble(o.IsPackedPrice);

                        ltrOrderFee.Text += "             <dt>Nhận hàng tại</dt><dd>" + o.ReceivePlace + "</dd>";
                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Phí tùy chọn</strong></dt>";
                        if (o.IsCheckProduct == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked  disabled=\"disabled\" /> Phí kiểm đếm</dt><dd>" + string.Format("{0:N0}", isCheckProductPrice) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\" /> Phí kiểm đếm</dt><dd>" + string.Format("{0:N0}", isCheckProductPrice) + " VNĐ</dd>";
                        if (o.IsPacked == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsPacked + "\" disabled=\"disabled\"  /> Phí đóng gỗ</dt><dd>" + string.Format("{0:N0}", isPackedPrice) + " VNĐ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", isPackedPrice / Convert.ToDouble(o.CurrentCNYVN)) + ")</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí đóng gỗ</dt><dd>" + string.Format("{0:N0}", isPackedPrice) + " VNĐ (<i class=\"fa fa-yen\"></i>" + string.Format("{0:#.##}", isPackedPrice / Convert.ToDouble(o.CurrentCNYVN)) + ")</dd>";
                        if (o.IsFastDelivery == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsFastDelivery + "\" disabled=\"disabled\"  /> Phí ship giao hàng tận nhà</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastDeliveryPrice)) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí ship giao hàng tận nhà</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastDeliveryPrice)) + " VNĐ</dd>";
                        if (o.IsFast == true)
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" checked=\"" + o.IsFast + "\" disabled=\"disabled\"  /> Phí đơn hàng hỏa tốc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice)) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt><input type=\"checkbox\" disabled=\"disabled\"  /> Phí đơn hàng hỏa tốc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.IsFastPrice)) + " VNĐ</dd>";


                        ltrOrderFee.Text += "         <dt class=\"full-width\"><strong class=\"title-fee\">Thanh toán</strong></dt>";
                        //if (obj_user.Wallet > 0 && o.Status == 0)
                        //{
                        //    ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                        //}


                        if (!string.IsNullOrEmpty(o.AmountDeposit))
                            ltrOrderFee.Text += "         <dt>Số tiền phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " VNĐ</dd>";
                        if (!string.IsNullOrEmpty(o.Deposit))
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.Deposit)) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Tiền đã đặt cọc</dt><dd>0 VNĐ</dd>";

                        ltrOrderFee.Text += "             <dt>Số tiền còn lại phải đặt cọc</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit) - Convert.ToDouble(o.Deposit)) + " VNĐ</dd>";


                        if (o.OrderType == 3)
                        {
                            if (o.IsCheckNotiPrice == true)
                            {

                            }
                            else
                            {
                                if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                                {
                                    //ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                                    ltrbtndeposit.Text += "         <a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a>";
                                }
                            }
                        }
                        else
                        {
                            if (o.Status == 0 && Convert.ToDouble(o.Deposit) < Convert.ToDouble(o.AmountDeposit) && Convert.ToDouble(o.TotalPriceVND) > 0)
                            {
                                //ltrOrderFee.Text += "         <dt><a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a></dt><dd></dd>";
                                ltrbtndeposit.Text += "         <a class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" onclick=\"depositOrder()\">Đặt cọc bằng số dư TK</a>";
                            }
                        }



                        ltrOrderFee.Text += "     </dl>";
                        ltrOrderFee.Text += " </div>";
                        ltrOrderFee.Text += "</div>";

                        ltrOrderFee.Text += "<div class=\"order-panel  bg-red-nhst print4\">";
                        ltrOrderFee.Text += "   <div class=\"title\">Tổng tiền khách hàng cần thanh toán</div>";
                        ltrOrderFee.Text += "   <div class=\"cont\">";
                        ltrOrderFee.Text += "     <dl>";
                        ltrOrderFee.Text += "         <dt>Tiền hàng</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " VNĐ</dd>";
                        ltrOrderFee.Text += "         <dt>Phí Ship nội địa</dt><dd>" + string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN)) + " VNĐ</dd>";
                        if (!string.IsNullOrEmpty(o.FeeBuyPro))
                        {
                            double bp = Convert.ToDouble(o.FeeBuyPro);
                            double bpCYN = bp / currency;
                            if (bp > 0)
                            {
                                if (UL_CKFeeBuyPro > 0)
                                    ltrOrderFee.Text += "         <dt>Phí hải quan cố định (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " VNĐ)</dt><dd>" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</dd>";
                                else
                                    ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</dd>";
                                //ltrOrderFee.Text += "         <dt>Phí mua hàng</dt><dd>" + string.Format("{0:N0}", bp) + " VNĐ</dd>";
                            }
                            else
                                ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>Đang cập nhật</dd>";
                        }
                        else
                            ltrOrderFee.Text += "         <dt>Phí hải quan cố định</dt><dd>Đang cập nhật</dd>";

                        double isFastDeliveryPrice = 0;
                        double isFastPrice = 0;
                        if (o.IsFastDeliveryPrice.ToFloat(0) > 0)
                            isFastDeliveryPrice = Convert.ToDouble(o.IsFastDeliveryPrice);
                        if (o.IsFastPrice.ToFloat(0) > 0)
                            isFastPrice = Convert.ToDouble(o.IsFastPrice);
                        double totalFee = isCheckProductPrice + isPackedPrice + isFastDeliveryPrice + isFastPrice;
                        ltrOrderFee.Text += "         <dt>Phí tùy chọn</dt><dd>" + string.Format("{0:N0}", totalFee) + " VNĐ</dd>";

                        double totalweight = 0;
                        var smallpackages1 = SmallPackageController.GetByMainOrderID(o.ID);
                        if (smallpackages1.Count > 0)
                        {
                            foreach (var s in smallpackages1)
                            {
                                totalweight += Convert.ToDouble(s.Weight);
                            }
                        }
                        if (UL_CKFeeWeight > 0)
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", totalweight) + " VNĐ)</dt><dd>" + string.Format("{0:N0}", feeWeight) + " VNĐ</dd>";
                        else
                            ltrOrderFee.Text += "         <dt>Phí vận chuyển TQ - VN</dt><dd>" + string.Format("{0:N0}", totalweight) + " VNĐ</dd>";

                        ltrOrderFee.Text += "         <dt class=\"full-width-sepearate\"></dt>";

                        double toptotalPriceVND = 0;
                        double topdeposit = 0;

                        if (o.TotalPriceVND.ToFloat(0) > 0)
                            toptotalPriceVND = Convert.ToDouble(o.TotalPriceVND);

                        if (o.Deposit.ToFloat(0) > 0)
                            topdeposit = Convert.ToDouble(o.Deposit);

                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Tổng chi phí</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", toptotalPriceVND) + " VNĐ</dd>";
                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Đã thanh toán</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", topdeposit) + " VNĐ</dd>";
                        ltrOrderFee.Text += "         <dt class=\"line-special\"><strong class=\"color-white\">Tiền còn thiếu</strong></dt><dd class=\"line-special\">" + string.Format("{0:N0}", toptotalPriceVND - topdeposit) + " VNĐ</dd>";
                        ltrOrderFee.Text += "     </dl>";
                        ltrOrderFee.Text += "   </div>";
                        ltrOrderFee.Text += "</div>";

                        var ui = AccountInfoController.GetByUserID(uid);
                        if (ui != null)
                        {
                            string phone = ui.MobilePhonePrefix + ui.MobilePhone;
                            txt_Fullname.Text = ui.FirstName + " " + ui.LastName;
                            txt_Address.Text = ui.Address;
                            txt_Email.Text = ui.Email;
                            txt_Phone.Text = phone;
                            //txt_DNote.Text = o.Note;
                        }
                        #endregion
                        #region Lấy các kiện nhỏ
                        var smallpackages = SmallPackageController.GetByMainOrderID(o.ID);
                        if (smallpackages.Count > 0)
                        {
                            hdfListPackage.Value = smallpackages.Count.ToString();
                            foreach (var s in smallpackages)
                            {
                                ltrOrderCodeList.Text += "<tr>";
                                ltrOrderCodeList.Text += "<td class=\"pro\">" + s.OrderWebCode + "</td>";
                                ltrOrderCodeList.Text += "<td class=\"pro\">" + s.OrderTransactionCode + "</td>";
                                ltrOrderCodeList.Text += "<td class=\"pro\">" + s.Weight + "</td>";
                                ltrOrderCodeList.Text += "<td class=\"pro\">" + s.StaffNote + "</td>";
                                ltrOrderCodeList.Text += "<td class=\"qty\">" + PJUtils.IntToStringStatusPackage(Convert.ToInt32(s.Status)) + "</td>";
                                ltrOrderCodeList.Text += "<td  colspan=\"2\"class=\"price\"><a target=\"_blank\" href=\"/them-khieu-nai/" + s.MainOrderID + "/" + s.OrderTransactionCode + "\" class=\"btn pill-btn primary-btn main-btn hover\">Khiếu nại</a></td>";
                                ltrOrderCodeList.Text += "</tr>";
                            }
                        }
                        else
                        {
                            hdfListPackage.Value = "0";
                        }
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
                                double pricepromotion = 0;
                                if (item.price_promotion.ToFloat(0) > 0)
                                    pricepromotion = Convert.ToDouble(item.price_promotion);

                                double priceorigin = 0;
                                if (item.price_origin.ToFloat(0) > 0)
                                    priceorigin = Convert.ToDouble(item.price_origin);

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
                                ltrProducts.Text += "<td class=\"pro\">" + stt + "</td>";
                                ltrProducts.Text += "<td class=\"pro\">";
                                ltrProducts.Text += "<div class=\"thumb-product\">";
                                ltrProducts.Text += "<div class=\"pd-img\"><a href=\"" + item.link_origin + "\" target=\"_blank\"><img src=\"" + item.image_origin + "\" alt=\"\"></a></div>";
                                ltrProducts.Text += "<div class=\"info\"><a href=\"" + item.link_origin + "\" target=\"_blank\">" + item.title_origin + "</a></div>";
                                ltrProducts.Text += "</div>";
                                ltrProducts.Text += "</td>";
                                ltrProducts.Text += "<td class=\"pro\">" + item.property + "</td>";
                                ltrProducts.Text += "<td class=\"qty\">" + item.quantity + "</td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">¥" + string.Format("{0:0.##}", price) + "</p></td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + string.Format("{0:N0}", vndprice) + " VNĐ</p></td>";
                                ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + item.brand + "</p></td>";
                                if (!string.IsNullOrEmpty(item.ProductStatus.ToString()))
                                {
                                    if (item.ProductStatus == 1)
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";
                                    else
                                        ltrProducts.Text += "<td class=\"price\"><p class=\"\">Hết hàng</p></td>";
                                }
                                else
                                    ltrProducts.Text += "<td class=\"price\"><p class=\"\">Còn hàng</p></td>";

                                //ltrProducts.Text += "<td class=\"price\"><p class=\"\">" + item.OrderShopCode + "</p></td>";
                                //var small = SmallPackageController.GetByOrderID(item.ID);
                                //if (small.Count > 0)
                                //{
                                //    ltrProducts.Text += "<td class=\"price\"><a href=\"javascript:;\" class=\"btn pill-btn primary-btn main-btn hover toggle-detail-row right\">Xem</a></td>";
                                //}
                                //else
                                //{
                                //    ltrProducts.Text += "<td class=\"price\"></td>";
                                //}
                                ltrProducts.Text += "</tr>";
                                //if (small.Count > 0)
                                //{
                                //    foreach (var sp in small)
                                //    {
                                //        ltrProducts.Text += "<tr class=\"detail-row\" style=\"background:#ff0;\">";
                                //        ltrProducts.Text += "<td colspan=\"2\" class=\"price\">Mã vận đơn:</td>";
                                //        ltrProducts.Text += "<td colspan=\"2\" class=\"price\">" + sp.OrderTransactionCode + "</td>";
                                //        ltrProducts.Text += "<td colspan=\"2\" class=\"price\">" + sp.Weight + " kg</td>";
                                //        int status = Convert.ToInt32(sp.Status);
                                //        string sttString = "";
                                //        if (status == 1)
                                //            sttString = "Chưa về kho TQ";
                                //        else if (status == 2)
                                //            sttString = "Đã về kho TQ";
                                //        else if (status == 3)
                                //            sttString = "Đã về kho đích";
                                //        else if (status == 4)
                                //            sttString = "Đã giao";

                                //        ltrProducts.Text += "<td colspan=\"2\" class=\"price\">" + sttString + "</td>";
                                //        ltrProducts.Text += "<td  colspan=\"2\"class=\"price\"><a target=\"_blank\" href=\"/them-khieu-nai/" + item.MainOrderID + "/" + item.ID + "/" + sp.OrderTransactionCode + "\" class=\"btn pill-btn primary-btn main-btn hover\">Khiếu nại</a></td>";
                                //        ltrProducts.Text += "</tr>";
                                //    }

                                //}
                                stt++;
                            }
                            ltrProducts1.Text += " <div class=\"rest-table\">";
                            ltrProducts1.Text += "<table class=\"tbl-subtotal full-width  mar-top2 mar-bot2\">";
                            ltrProducts1.Text += "     <tbody>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            if (o.OrderType == 3)
                            {
                                if (o.IsCheckNotiPrice == true)
                                {
                                    ltrProducts1.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                    ltrProducts1.Text += "             <td class=\"float-right\"><span class=\"bg-yellow-gold\">Chờ báo giá</span> </td>";
                                }
                                else
                                {
                                    ltrProducts1.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                    ltrProducts1.Text += "             <td class=\"float-right\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + " </td>";
                                }
                            }
                            else
                            {
                                ltrProducts1.Text += "             <td class=\"float-left\">Trạng thái đơn hàng:</td>";
                                ltrProducts1.Text += "             <td class=\"float-right\">" + PJUtils.IntToRequestClient(Convert.ToInt32(o.Status)) + " </td>";
                            }

                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Tiền hàng:</td>";
                            ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.PriceVND)) + " VNĐ (¥ " + o.PriceCNY + ")</td>";
                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";

                            if (!string.IsNullOrEmpty(o.FeeBuyPro))
                            {
                                double bp = Convert.ToDouble(o.FeeBuyPro);
                                double bpCYN = bp / currency;
                                if (bp > 0)
                                {
                                    if (UL_CKFeeBuyPro > 0)
                                    {
                                        ltrProducts1.Text += "             <td class=\"float-left\">Phí hải quan cố định (Đã CK " + UL_CKFeeBuyPro + "% : " + string.Format("{0:N0}", subfeebp) + " VNĐ):</td>";
                                        ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</td>";
                                    }
                                    else
                                    {
                                        ltrProducts1.Text += "             <td class=\"float-left\">Phí hải quan cố định:</td>";
                                        ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", bp) + " VNĐ (¥" + bpCYN + ")</td>";
                                    }
                                }
                                else
                                {
                                    ltrProducts1.Text += "             <td class=\"float-left\">Phí hải quan cố định:</td>";
                                    ltrProducts1.Text += "             <td class=\"float-right\">0</td>";
                                }
                            }
                            else
                            {
                                ltrProducts1.Text += "             <td class=\"float-left\">Phí hải quan cố định:</td>";
                                ltrProducts1.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                            }

                            ltrProducts1.Text += "         </tr>";
                            //ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            //ltrProducts1.Text += "             <td class=\"float-left\">Phí kiểm đếm:</td>";
                            //ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.IsCheckProductPrice.ToFloat(0)) + " VNĐ</td>";
                            //ltrProducts1.Text += "         </tr>";
                            if (o.OrderType == 2)
                            {

                                double wechatCYN = 0;
                                double wechatVND = 0;
                                if (o.WeChatFeeCYN.ToFloat(0) > 0)
                                    wechatCYN = Convert.ToDouble(o.WeChatFeeCYN);
                                if (o.WeChatFeeVND.ToFloat(0) > 0)
                                    wechatVND = Convert.ToDouble(o.WeChatFeeVND);

                                ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                                ltrProducts1.Text += "             <td class=\"float-left\">Phí mua hàng WeChat:</td>";
                                ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", wechatVND) + " VNĐ (¥ " + Math.Round(wechatCYN, 2) + ")</td>";
                                ltrProducts1.Text += "         </tr>";
                            }
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Phí ship nội địa TQ:</td>";
                            if (!string.IsNullOrEmpty(o.FeeShipCN))
                            {
                                double fscn = Math.Floor(Convert.ToDouble(o.FeeShipCN));
                                double phhinoidiate = fscn / currency1;
                                ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.FeeShipCN)) + " VNĐ (¥ " + phhinoidiate + ")</td>";
                            }
                            else
                                ltrProducts1.Text += "             <td class=\"float-right\">Đang cập nhật</td>";
                            ltrProducts1.Text += "         </tr>";

                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";

                            double feeweightVN = 0;
                            double feeweightCYN = 0;
                            double currencyOrder = Convert.ToDouble(o.CurrentCNYVN);
                            if (!string.IsNullOrEmpty(o.FeeWeight))
                            {
                                feeweightVN = Convert.ToDouble(o.FeeWeight);
                                feeweightCYN = feeweightVN / currencyOrder;
                                feeweightCYN = Math.Round(feeweightCYN, 2);
                            }

                            double oweight = 0;
                            var smp = SmallPackageController.GetByMainOrderID(o.ID);
                            if (smp.Count > 0)
                            {
                                foreach (var item in smp)
                                {
                                    oweight += Convert.ToDouble(item.Weight);
                                }
                            }
                            //var exportRe = ExportRequestTurnController.GetByMainOrderID(id);
                            //if (exportRe.Count > 0)
                            //{
                            //    foreach (var item in exportRe)
                            //    {
                            //        oweight += Convert.ToDouble(item.TotalWeight);
                            //    }
                            //}

                            ltrProducts1.Text += "             <td class=\"float-left\">Phí vận chuyển Trung Quốc - Việt Nam:</td>";
                            if (!string.IsNullOrEmpty(o.OrderWeight))
                                ltrProducts1.Text += "             <td class=\"float-right\">Trọng lượng: " + Math.Round(oweight, 2) + " kg - Quy đổi tệ: ¥" + string.Format("{0:N0}", feeweightCYN) + " - Quy đổi VNĐ: " + string.Format("{0:N0}", feeWeight) + " VNĐ</td>";
                            else
                                ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", feeWeight) + " VNĐ</td>";

                            //if (UL_CKFeeWeight > 0)
                            //{
                            //    ltrProducts1.Text += "             <td class=\"float-left\">Phí vận chuyển Trung Quốc - Việt Nam (Đã CK " + UL_CKFeeWeight + "% : " + string.Format("{0:N0}", o.FeeWeightCK.ToFloat(0)) + " VNĐ):</td>";
                            //    if (!string.IsNullOrEmpty(o.OrderWeight))
                            //        ltrProducts1.Text += "             <td class=\"float-right\">Trọng lượng: " + Math.Round(totalweight, 2) + " kg - Quy đổi tệ: ¥" + string.Format("{0:N0}", feeweightCYN) + " - Quy đổi VNĐ: " + string.Format("{0:N0}", feeweightVN) + " VNĐ</td>";
                            //    else
                            //        ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                            //}
                            //else
                            //{

                            //    ltrProducts1.Text += "             <td class=\"float-left\">Phí vận chuyển Trung Quốc - Việt Nam:</td>";
                            //    if (!string.IsNullOrEmpty(o.OrderWeight))
                            //        ltrProducts1.Text += "             <td class=\"float-right\">Trọng lượng: " + Math.Round(totalweight, 2) + " kg - Quy đổi tệ: ¥" + string.Format("{0:N0}", feeweightCYN) + " - Quy đổi VNĐ: " + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                            //    else
                            //        ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", o.FeeWeight.ToFloat(0)) + " VNĐ</td>";
                            //}

                            ltrProducts1.Text += "         </tr>";
                            //ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            //ltrProducts1.Text += "             <td class=\"float-left\">Phí đóng gỗ:</td>";
                            //ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.IsPackedPrice)) + " VNĐ</td>";
                            //ltrProducts1.Text += "         </tr>";

                            double phihangdacbiet_CYN = 0;
                            double phihangdacbiet_VND = 0;

                            var orders = OrderController.GetByMainOrderID(o.ID);
                            if (orders.Count > 0)
                            {
                                foreach (var item in orders)
                                {
                                    if (item.IsCensorProduct == true)
                                    {
                                        phihangdacbiet_CYN += Convert.ToDouble(item.PriceOfCensorCYN);
                                        phihangdacbiet_VND += Convert.ToDouble(item.PriceOfCensorVND);
                                    }
                                }

                            }
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Phụ phí hàng đặc biệt:</td>";
                            ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", phihangdacbiet_VND) + " VNĐ (¥ " + phihangdacbiet_CYN + ")</td>";
                            ltrProducts1.Text += "         </tr>";

                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Tổng cộng:</td>";
                            ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.TotalPriceVND)) + " VNĐ</td>";
                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Số tiền cần đặt cọc:</td>";
                            ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", Convert.ToDouble(o.AmountDeposit)) + " VNĐ</td>";
                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Đã thanh toán:</td>";
                            double deposit = 0;

                            if (!string.IsNullOrEmpty(o.Deposit))
                            {
                                deposit = Convert.ToDouble(o.Deposit);
                                ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", topdeposit) + " VNĐ</td>";
                            }
                            else
                                ltrProducts1.Text += "             <td class=\"float-right\">Chưa đặt cọc</td>";


                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "         <tr class=\"black b font-size-20 color-orange\">";
                            ltrProducts1.Text += "             <td class=\"float-left\">Cần thanh toán:</td>";
                            ltrProducts1.Text += "             <td class=\"float-right\">" + string.Format("{0:N0}", toptotalPriceVND - topdeposit) + " VNĐ</td>";
                            ltrProducts1.Text += "         </tr>";
                            ltrProducts1.Text += "     </tbody>";
                            ltrProducts1.Text += "</table>";
                            ltrProducts1.Text += " </div>";
                        }
                        #endregion
                        #region Lấy sản phẩm có group
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

                                    pos.FeeShipVN = string.Format("{0:N0}", FeeShipVN);
                                    double TotalPriceShopCYN = 0;
                                    double TotalPriceShopVND = 0;
                                    double TotalShopQuantity = 0;

                                    pos.shopName = productlist[0].shop_name;
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

                                        double totalPriceVND = vndprice * Convert.ToDouble(item.quantity);
                                        double totalPriceCYN = price * Convert.ToDouble(item.quantity);

                                        sp.priceVND = string.Format("{0:N0}", vndprice);
                                        sp.priceCYN = "¥" + string.Format("{0:0.##}", price);

                                        sp.totalPriceVND = string.Format("{0:N0}", totalPriceVND);
                                        sp.totalPriceCYN = "¥" + string.Format("{0:0.##}", totalPriceCYN);

                                        sp.note = item.brand;
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
                                        bool iscen = false;
                                        if (item.IsCensorProduct != null)
                                        {
                                            iscen = Convert.ToBoolean(item.IsCensorProduct);
                                        }
                                        sp.isCensoreProduct = iscen;
                                        double pricecCYN = 0;
                                        double pricecVND = 0;
                                        if (!string.IsNullOrEmpty(item.PriceOfCensorCYN))
                                        {
                                            pricecCYN = Convert.ToDouble(item.PriceOfCensorCYN);
                                        }
                                        if (!string.IsNullOrEmpty(item.PriceOfCensorVND))
                                        {
                                            pricecVND = Convert.ToDouble(item.PriceOfCensorVND);
                                        }

                                        sp.priceCensorCYN = pricecCYN;
                                        sp.priceCensorVND = pricecVND;
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
                            //htmlOSD.Append("        <th>Shop ID</th>");
                            htmlOSD.Append("        <th>Sản phẩm</th>");
                            htmlOSD.Append("        <th>Mã đơn hàng</th>");
                            htmlOSD.Append("    </tr>");
                            foreach (var item in poss)
                            {
                                htmlOSD.Append("    <tr id=\"" + item.shopID + "\" data-shopid=\"" + item.shopID + "\">");
                                htmlOSD.Append("        <td style=\"width: 5%;\" class=\"middle-center\">");
                                htmlOSD.Append("" + item.shopName + "<br/>");
                                htmlOSD.Append("        <br/>Phí ship nội địa: <strong>¥" + item.FeeShipVN + "</strong>");
                                htmlOSD.Append("        <br/><br/>Tổng tệ mua tại SHOP: <strong>¥" + item.TotalPriceShopCYN + "</strong>");
                                htmlOSD.Append("        <br/><br/>Tổng tiền VNĐ mua tại SHOP: <strong>" + string.Format("{0:N0}", item.TotalPriceShopVND) + " VNĐ</strong>");
                                htmlOSD.Append("        <br/><br/>Tổng số lượng mua tại SHOP: <strong>" + item.TotalShopQuantity + "</strong>");
                                htmlOSD.Append("        </td>");

                                //htmlOSD.Append("        <td style=\"width: 5%;\" class=\"middle-center\">" + item.shopID + "");
                                //htmlOSD.Append("            <a href=\"javascript:;\" class=\"btn-not-radius\" style=\"margin: 0; margin-bottom: 20px; width: 100%; margin-top: 10px;\" onclick=\"callpopupaddordershopcode('" + item.shopID + "','" + item.shopName + "','" + o.ID + "')\">Thêm mã đơn</a>");
                                //htmlOSD.Append("        </td>");
                                htmlOSD.Append("        <td style=\"width: 40%\">");
                                htmlOSD.Append("            <table class=\"tb-product\">");
                                htmlOSD.Append("                <tbody>");
                                htmlOSD.Append("                    <tr>");
                                htmlOSD.Append("                        <th class=\"pro\">ID</th>");
                                htmlOSD.Append("                        <th class=\"pro\">Sản phẩm</th>");
                                htmlOSD.Append("                        <th class=\"pro\">Thuộc tính</th>");
                                htmlOSD.Append("                        <th class=\"qty\">Số lượng</th>");
                                htmlOSD.Append("                        <th class=\"price\">Đơn giá VND</th>");
                                htmlOSD.Append("                        <th class=\"price\">Tổng giá VND</th>");
                                htmlOSD.Append("                        <th class=\"price\">Đơn giá CYN</th>");
                                htmlOSD.Append("                        <th class=\"price\">Tổng giá CYN</th>");
                                htmlOSD.Append("                        <th class=\"price\">Ghi chú riêng sản phẩm</th>");
                                htmlOSD.Append("                        <th class=\"price\">Trạng thái</th>");
                                htmlOSD.Append("                        <th class=\"price\">Hàng đặc biệt</th>");
                                htmlOSD.Append("                        <th class=\"price\">Phí đặc biệt</th>");
                                htmlOSD.Append("                    </tr>");
                                var products = item.sp;
                                if (products.Count > 0)
                                {
                                    foreach (var p in products)
                                    {
                                        htmlOSD.Append("                    <tr>");
                                        htmlOSD.Append("                        <td class=\"pro\">" + p.ID + "</td>");
                                        htmlOSD.Append("                        <td class=\"pro\">");
                                        htmlOSD.Append("                            <div class=\"thumb-product\">");
                                        htmlOSD.Append("                                <div class=\"pd-img\" style=\"float:left;\">");
                                        htmlOSD.Append("                                    <a href=\"" + p.productLink + "\" target=\"_blank\">");
                                        htmlOSD.Append("                                        <img src=\"" + p.IMG + "\" alt=\"\"></a>");
                                        htmlOSD.Append("                                </div><br/>");
                                        htmlOSD.Append("                                <div class=\"info\" style=\"float:left;clear:both;\"><a href=\"" + p.productLink + "\" target=\"_blank\">" + p.productName + "</a></div>");
                                        htmlOSD.Append("                            </div>");
                                        htmlOSD.Append("                        </td>");
                                        htmlOSD.Append("                        <td class=\"pro\">" + p.variable + "</td>");
                                        htmlOSD.Append("                        <td class=\"qty\">" + p.quantity + "</td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.priceVND + " VNĐ</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.totalPriceVND + " VNĐ</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.priceCYN + "</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.totalPriceCYN + " </p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\" style=\"color: orange; font-weight: bold\">" + p.note + "</p></td>");
                                        htmlOSD.Append("                        <td class=\"price\"><p class=\"\">" + p.status + "</p></td>");

                                        if (p.isCensoreProduct == true)
                                            htmlOSD.Append("                        <td class=\"price\"><input type=\"checkbox\" checked disabled/></td>");
                                        else
                                            htmlOSD.Append("                        <td class=\"price\"><input type=\"checkbox\" disabled/></td>");

                                        htmlOSD.Append("                        <td class=\"price\">" + string.Format("{0:N0}", p.priceCensorVND) + " VNĐ (¥" + p.priceCensorCYN + ")</td>");
                                        //htmlOSD.Append("                        <td class=\"price\"><a class=\"btn btn-info btn-sm\" href=\"/Admin/ProductEdit.aspx?id=" + p.ID + "\">Sửa</a></td>");
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
                                        //htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link ordershopcode-atag\" onclick=\"showordershopcodedetail($(this))\">" + ordershopcodes.OrderShopCode + "</a>");
                                        //htmlOSD.Append("                            <a href=\"javascript:;\" class=\"btn-not-radius\" style=\"clear: both; width: 100%; text-align: center\" onclick=\"callpopupaddsmallpackage($(this))\">Thêm mã vận đơn</a>");
                                        htmlOSD.Append("                            <a href=\"javascript:;\" class=\"custom-link ordershopcode-atag\">" + ordershopcodes.OrderShopCode + "</a>");
                                        htmlOSD.Append("                        </td>");
                                        htmlOSD.Append("                        <td class=\"smallpackage-list-of-odsc\">");
                                        var smlpacks = osc.smallPackage;
                                        if (smlpacks.Count > 0)
                                        {
                                            foreach (var smlo in smlpacks)
                                            {
                                                double weight = Math.Round(Convert.ToDouble(smlo.Weight), 1);
                                                var check = RequestOutStockController.GetBySmallpackageID(smlo.ID);
                                                if (check == null)
                                                {
                                                    if (smlo.Status == 1)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-red\" data-issendoutstock=\"0\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 2)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-orange\" data-issendoutstock=\"0\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 3)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><input class=\"chk-export\" type=\"checkbox\" onchange=\"selectExport()\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" /> <a href=\"javascript:;\" class=\"custom-link packageorderitem bg-green isxuatkhook\" data-issendoutstock=\"0\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 4)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-blue\" data-issendoutstock=\"0\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                }
                                                else
                                                {
                                                    if (smlo.Status == 1)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-red\" data-issendoutstock=\"1\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 2)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-orange\" data-issendoutstock=\"1\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 3)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-green\" data-issendoutstock=\"1\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                    else if (smlo.Status == 4)
                                                        htmlOSD.Append("                            <div class=\"spackage-row\"><a href=\"javascript:;\" class=\"custom-link packageorderitem bg-blue\" data-issendoutstock=\"1\" data-mainorderid=\"" + o.ID + "\" data-ordershopcodeid=\"" + smlo.OrderShopCodeID + "\" data-smallpackageid=\"" + smlo.ID + "\" data-smallstaffnote=\"" + smlo.StaffNote + "\" data-smallpackagestatus=\"" + smlo.Status + "\" data-smallpackageweight=\"" + weight + "\" data-smallpackagecode=\"" + smlo.OrderTransactionCode + "\" onclick=\"showsmallpackagedetail($(this))\">" + smlo.OrderTransactionCode + "</a></div>");
                                                }
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
                            ltrProductOrder.Text = htmlOSD.ToString();
                            #endregion
                            #region Lấy bình luận
                            ltrComment.Text += "<div class=\"comment mar-bot2\">";
                            ltrComment.Text += "     <div class=\"comment_content\" seller=\"" + o.ShopID + "\" order=\"" + o.ID + "\" >";
                            var shopcomments = OrderCommentController.GetByOrderIDAndType(o.ID, 1);
                            if (shopcomments.Count > 0)
                            {
                                foreach (var item in shopcomments)
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
                                    if (role == 1)
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : " + item.Comment + "<br>";
                                    }
                                    else
                                    {
                                        ltrComment.Text += "         <span class=\"user-comment green\">" + fullname + "</span>&nbsp;&nbsp;<b class=\"font-size-10\">[" + string.Format("{0:HH:mm:ss dd/MM/yyyy}", item.CreatedDate) + "]</b> : <span class=\"green\">" + item.Comment + "</span><br>";

                                    }
                                }
                            }
                            else
                            {
                                ltrComment.Text += "         <span class=\"user-comment\">Chưa có ghi chú.</span>";
                            }
                            ltrComment.Text += "     </div>";
                            ltrComment.Text += "     <div class=\"comment_action\" style=\"padding-bottom: 4px; padding-top: 4px;\">";
                            ltrComment.Text += "         <input shop_code=\"" + o.ID + "\" type=\"text\" class=\"comment-text\" order=\"188083\" seller=\"" + o.ShopID + "\" placeholder=\"Nội dung\">";
                            //ltrComment.Text += "         <a id=\"sendnotecomment\" onclick=\"postcomment($(this))\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                            ltrComment.Text += "         <a id=\"sendnotecomment\" order=\"" + o.ID + "\" class=\"btn pill-btn primary-btn main-btn hover\" href=\"javascript:;\" style=\"min-width:10px;\">Gửi</a>";
                            ltrComment.Text += "     </div>";
                            ltrComment.Text += "</div>";

                            //var cs = OrderCommentController.GetByOrderIDAndType(o.ID, 1);
                            //if (cs != null)
                            //{
                            //    if (cs.Count > 0)
                            //    {
                            //        foreach (var item in cs)
                            //        {
                            //            string fullname = "";
                            //            int role = 0;
                            //            var user = AccountController.GetByID(Convert.ToInt32(item.CreatedBy));
                            //            if (user != null)
                            //            {
                            //                role = Convert.ToInt32(user.RoleID);
                            //                var userinfo = AccountInfoController.GetByUserID(user.ID);
                            //                if (userinfo != null)
                            //                {
                            //                    fullname = userinfo.FirstName + " " + userinfo.LastName;
                            //                }
                            //            }
                            //            ltr_comment.Text += "<li class=\"item\">";
                            //            ltr_comment.Text += "   <div class=\"item-left\">";
                            //            if (role == 0)
                            //            {
                            //                ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/icon.png\" width=\"100%\" /></span>";
                            //            }
                            //            else
                            //            {
                            //                ltr_comment.Text += "       <span class=\"avata circle\"><img src=\"/App_Themes/NHST/images/user-icon.png\" width=\"100%\" /></span>";
                            //            }
                            //            ltr_comment.Text += "   </div>";
                            //            ltr_comment.Text += "   <div class=\"item-right\">";
                            //            ltr_comment.Text += "       <strong class=\"item-username\">" + fullname + "</strong>";
                            //            ltr_comment.Text += "       <span class=\"item-date\">" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate) + "</span>";
                            //            ltr_comment.Text += "       <p class=\"item-comment\">";
                            //            ltr_comment.Text += item.Comment;
                            //            ltr_comment.Text += "       </p>";
                            //            ltr_comment.Text += "   </div>";
                            //            ltr_comment.Text += "</li>";
                            //        }
                            //    }
                            //    else
                            //    {
                            //        ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                            //    }
                            //}
                            //else
                            //{
                            //    ltr_comment.Text += "Hiện chưa có đánh giá nào.";
                            //}
                            #endregion
                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double wallet = Convert.ToDouble(obj_user.Wallet);
                        wallet = wallet + Convert.ToDouble(o.Deposit);
                        AccountController.updateWallet(obj_user.ID, wallet, DateTime.Now, obj_user.Username);
                        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, "0");
                        string kq = MainOrderController.UpdateStatus(id, uid, 1);
                        if (kq == "ok")
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = RouteData.Values["id"].ToString().ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        //string kq = OrderCommentController.Insert(id, txtComment.Text, true, 1, DateTime.Now, uid);
                        //if (Convert.ToInt32(kq) > 0)
                        //    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }
                }
            }
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            string username_current = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username_current);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int OID = ViewState["OID"].ToString().ToInt(0);
                if (OID > 0)
                {
                    var o = MainOrderController.GetAllByID(OID);
                    if (o != null)
                    {
                        double orderdeposited = 0;
                        if (!string.IsNullOrEmpty(o.Deposit))
                            orderdeposited = Convert.ToDouble(o.Deposit);
                        double amountdeposit = Convert.ToDouble(o.AmountDeposit);
                        double userwallet = Convert.ToDouble(obj_user.Wallet);
                        if (userwallet > 0)
                        {
                            if (orderdeposited > 0)
                            {
                                double depleft = amountdeposit - orderdeposited;
                                if (userwallet >= depleft)
                                {
                                    double wallet = userwallet - depleft;
                                    //Cập nhật lại MainOrder                                
                                    MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, depleft,
                                        obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                    AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);
                                    PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, depleft, 2, currentDate, obj_user.Username);
                                    try
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "Đơn hàng của Qúy khách đã được đặt cọc thành công và chuyển tới nhân viên đặt hàng. Đơn hàng sẽ được xử lý và đặt hàng trong 24h. Mọi cập nhật về đơn hàng đều sẽ hiển thị trên hệ thống và thông báo qua email.<br/><br/>";
                                        message += "Để kiểm tra tình trạng đơn hàng, Qúy khách xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-kiem-tra-trang-thai-don-hang\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a>.<br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", obj_user.Email,
                                            "Đặt cọc thành công đơn hàng tại 1688 Express", message, "");
                                    }
                                    catch
                                    {

                                    }
                                    PJUtils.ShowMessageBoxSwAlert("Đặt cọc đơn hàng thành công.", "s", true, Page);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxHTMLSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Click <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong>VÀO ĐÂY</strong> để xem hướng dẫn.</a>.",
                                "e", true, Page);
                                }
                            }
                            else
                            {
                                if (userwallet >= amountdeposit)
                                {
                                    //Cập nhật lại Wallet User
                                    double wallet = userwallet - amountdeposit;
                                    AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                                    //Cập nhật lại MainOrder
                                    MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                                    MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                                    HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                                    PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);

                                    var admins = AccountController.GetAllByRoleID(0);
                                    if (admins.Count > 0)
                                    {
                                        foreach (var admin in admins)
                                        {
                                            NotificationController.Inser(obj_user.ID, obj_user.Username, admin.ID,
                                                                               admin.Username, o.ID,
                                                                               "Có đơn hàng mới: #" + o.ID, 0, 1,
                                                                               currentDate, obj_user.Username);
                                        }
                                    }

                                    var managers = AccountController.GetAllByRoleID(2);
                                    if (managers.Count > 0)
                                    {
                                        foreach (var manager in managers)
                                        {
                                            NotificationController.Inser(obj_user.ID, obj_user.Username, manager.ID,
                                                                               manager.Username, o.ID,
                                                                                "Có đơn hàng mới: #" + o.ID, 0, 1,
                                                                               currentDate, obj_user.Username);
                                        }
                                    }

                                    try
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "Đơn hàng của Qúy khách đã được đặt cọc thành công và chuyển tới nhân viên đặt hàng. Đơn hàng sẽ được xử lý và đặt hàng trong 24h. Mọi cập nhật về đơn hàng đều sẽ hiển thị trên hệ thống và thông báo qua email.<br/><br/>";
                                        message += "Để kiểm tra tình trạng đơn hàng, Qúy khách xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-kiem-tra-trang-thai-don-hang\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a>.<br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", obj_user.Email,
                                            "Đặt cọc thành công đơn hàng tại 1688 Express", message, "");
                                    }
                                    catch
                                    {

                                    }
                                    PJUtils.ShowMessageBoxSwAlert("Đặt cọc thành công", "s", true, Page);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxHTMLSwAlert("Số dư trong tài khoản của quý khách không đủ để đặt cọc đơn hàng này. Click <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong>VÀO ĐÂY</strong> để xem hướng dẫn.</a>.", "e", true, Page);
                                }
                            }
                            #region code cũ
                            //if (orderdeposited > 0)
                            //{
                            //    double depleft = amountdeposit - orderdeposited;
                            //    if (userwallet >= depleft)
                            //    {
                            //        double wallet = userwallet - depleft;
                            //        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                            //        //Cập nhật lại MainOrder                                
                            //        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                            //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                            //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, depleft, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                            //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, depleft, 2, currentDate, obj_user.Username);
                            //    }
                            //    else
                            //    {
                            //        double walletleft = depleft - userwallet;
                            //        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                            //        double newpay = orderdeposited + userwallet;
                            //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, newpay.ToString());
                            //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                            //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                            //    }
                            //}
                            //else
                            //{
                            //    if (userwallet >= amountdeposit)
                            //    {
                            //        //Cập nhật lại Wallet User
                            //        double wallet = userwallet - amountdeposit;
                            //        AccountController.updateWallet(obj_user.ID, wallet, currentDate, obj_user.Username);

                            //        //Cập nhật lại MainOrder                                
                            //        MainOrderController.UpdateStatus(o.ID, obj_user.ID, 2);
                            //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, amountdeposit.ToString());
                            //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, amountdeposit, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", wallet, 1, 1, currentDate, obj_user.Username);
                            //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, amountdeposit, 2, currentDate, obj_user.Username);

                            //        var admins = AccountController.GetAllByRoleID(0);
                            //        if (admins.Count > 0)
                            //        {
                            //            foreach (var admin in admins)
                            //            {
                            //                NotificationController.Inser(obj_user.ID, obj_user.Username, admin.ID,
                            //                                                   admin.Username, o.ID,
                            //                                                   "Có đơn hàng mới: #" + o.ID, 0, 1,
                            //                                                   currentDate, obj_user.Username);
                            //            }
                            //        }

                            //        var managers = AccountController.GetAllByRoleID(2);
                            //        if (managers.Count > 0)
                            //        {
                            //            foreach (var manager in managers)
                            //            {
                            //                NotificationController.Inser(obj_user.ID, obj_user.Username, manager.ID,
                            //                                                   manager.Username, o.ID,
                            //                                                    "Có đơn hàng mới: #" + o.ID, 0, 1,
                            //                                                   currentDate, obj_user.Username);
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        double paid = amountdeposit - userwallet;
                            //        //Cập nhật lại Wallet User
                            //        AccountController.updateWallet(obj_user.ID, 0, currentDate, obj_user.Username);
                            //        //Cập nhật lại MainOrder
                            //        MainOrderController.UpdateDeposit(o.ID, obj_user.ID, userwallet.ToString());
                            //        HistoryPayWalletController.Insert(obj_user.ID, obj_user.Username, o.ID, userwallet, obj_user.Username + " đã đặt cọc đơn hàng: " + o.ID + ".", 0, 1, 1, currentDate, obj_user.Username);
                            //        PayOrderHistoryController.Insert(o.ID, obj_user.ID, 2, userwallet, 2, currentDate, obj_user.Username);
                            //    }
                            //}

                            #endregion

                            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        }
                    }
                }
            }
        }

        protected void btnPayAll_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                var id = ViewState["OID"].ToString().ToInt(0);
                DateTime currentDate = DateTime.Now;
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        double deposit = Convert.ToDouble(o.Deposit);
                        double wallet = Convert.ToDouble(obj_user.Wallet);
                        double moneyleft = Convert.ToDouble(o.TotalPriceVND) - deposit;

                        if (wallet >= moneyleft)
                        {
                            double walletLeft = wallet - moneyleft;
                            double payalll = deposit + moneyleft;
                            MainOrderController.UpdateStatus(o.ID, uid, 9);
                            AccountController.updateWallet(uid, walletLeft, currentDate, username);

                            HistoryOrderChangeController.Insert(o.ID, uid, username, username +
                                        " đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                            HistoryPayWalletController.Insert(uid, username, o.ID, moneyleft, username + " đã thanh toán đơn hàng: " + o.ID + ".", walletLeft, 1, 3, currentDate, username);
                            MainOrderController.UpdateDeposit(id, uid, payalll.ToString());
                            PayOrderHistoryController.Insert(id, uid, 9, moneyleft, 2, currentDate, username);
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        }
                        else
                        {
                            ltr_info.Text = "Số tiền trong tài khoản của bạn không đủ để thanh toán đơn hàng.";
                            ltr_info.ForeColor = System.Drawing.Color.Red;
                            ltr_info.Visible = true;
                        }
                    }
                }
            }

        }

        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {
                int uid = obj_user.ID;
                //var id = RouteData.Values["id"].ToString().ToInt(0);
                int id = hdfShopID.Value.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        int salerID = Convert.ToInt32(o.SalerID);
                        int dathangID = Convert.ToInt32(o.DathangID);
                        int khotqID = Convert.ToInt32(o.KhoTQID);
                        int khovnID = Convert.ToInt32(o.KhoVNID);

                        if (salerID > 0)
                        {
                            var saler = AccountController.GetByID(salerID);
                            if (saler != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, salerID,
                                    saler.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (dathangID > 0)
                        {
                            var dathang = AccountController.GetByID(dathangID);
                            if (dathang != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, dathangID,
                                    dathang.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (khotqID > 0)
                        {
                            var khotq = AccountController.GetByID(khotqID);
                            if (khotq != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, khotqID,
                                    khotq.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (khovnID > 0)
                        {
                            var khovn = AccountController.GetByID(khovnID);
                            if (khovn != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, khovnID,
                                    khovn.Username, id,
                                    "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }

                        var admins = AccountController.GetAllByRoleID(0);
                        if (admins.Count > 0)
                        {
                            foreach (var admin in admins)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, admin.ID,
                                                                   admin.Username, id,
                                                                   "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                                                   currentDate, obj_user.Username);
                            }
                        }

                        var managers = AccountController.GetAllByRoleID(2);
                        if (managers.Count > 0)
                        {
                            foreach (var manager in managers)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, manager.ID,
                                                                   manager.Username, id,
                                                                   "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem", 0, 1,
                                                                   currentDate, obj_user.Username);
                            }
                        }


                        string comment = hdfCommentText.Value;
                        string kq = OrderCommentController.Insert(id, comment, true, 1, currentDate, uid);
                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                        hubContext.Clients.All.addNewMessageToPage("", "");
                        PJUtils.ShowMessageBoxSwAlert("Gửi nội dung thành công", "s", true, Page);
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (u != null)
            {
                int UID = u.ID;
                int ID = ViewState["OID"].ToString().ToInt(0);
                string orderCodeshop = Request.QueryString["ordershopcode"];
                var s = MainOrderController.GetAllByUIDAndID(UID, ID);
                if (s != null)
                {
                    //MainOrderController.UpdateNote(s.ID, txt_DNote.Text);
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật ghi chú thành công", "s", true, Page);
                }
            }
        }

        #region webservice
        [WebMethod]
        public static string PostComment(string commentext, string shopid)
        {
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            if (obj_user != null)
            {

                int uid = obj_user.ID;
                //var id = RouteData.Values["id"].ToString().ToInt(0);
                int id = shopid.ToInt(0);
                if (id > 0)
                {
                    var o = MainOrderController.GetAllByUIDAndID(uid, id);
                    if (o != null)
                    {
                        string message = "Đã có đánh giá mới cho đơn hàng #" + id + ". CLick vào để xem";
                        int salerID = Convert.ToInt32(o.SalerID);
                        int dathangID = Convert.ToInt32(o.DathangID);
                        int khotqID = Convert.ToInt32(o.KhoTQID);
                        int khovnID = Convert.ToInt32(o.KhoVNID);

                        if (salerID > 0)
                        {
                            var saler = AccountController.GetByID(salerID);
                            if (saler != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, salerID,
                                    saler.Username, id,
                                    message, 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (dathangID > 0)
                        {
                            var dathang = AccountController.GetByID(dathangID);
                            if (dathang != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, dathangID,
                                    dathang.Username, id,
                                    message, 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (khotqID > 0)
                        {
                            var khotq = AccountController.GetByID(khotqID);
                            if (khotq != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, khotqID,
                                    khotq.Username, id,
                                    message, 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }
                        if (khovnID > 0)
                        {
                            var khovn = AccountController.GetByID(khovnID);
                            if (khovn != null)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, khovnID,
                                    khovn.Username, id,
                                    message, 0, 1,
                                    currentDate, obj_user.Username);
                            }
                        }

                        var admins = AccountController.GetAllByRoleID(0);
                        if (admins.Count > 0)
                        {
                            foreach (var admin in admins)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, admin.ID,
                                                                   admin.Username, id,
                                                                   message, 0, 1,
                                                                   currentDate, obj_user.Username);
                            }
                        }

                        var managers = AccountController.GetAllByRoleID(2);
                        if (managers.Count > 0)
                        {
                            foreach (var manager in managers)
                            {
                                NotificationController.Inser(obj_user.ID, obj_user.Username, manager.ID,
                                                                   manager.Username, id,
                                                                   message, 0, 1,
                                                                   currentDate, obj_user.Username);
                            }
                        }

                        string kq = OrderCommentController.Insert(id, commentext, true, 1, currentDate, uid);
                        if (kq.ToInt(0) > 0)
                        {
                            return kq + "|" + message;
                        }
                        else
                            return "0";

                    }
                    else
                        return "0";
                }
                else
                    return "0";
            }
            else return "0";
        }
        [WebMethod]
        public static string requestoutstockall(string listID, int mID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    if (!string.IsNullOrEmpty(listID))
                    {
                        double leftMoney = 0;
                        var mO = MainOrderController.GetAllByUIDAndID(acc.ID, mID);
                        if (mO != null)
                        {
                            double totalprice = Convert.ToDouble(mO.TotalPriceVND);
                            double deposited = Convert.ToDouble(mO.Deposit);
                            leftMoney = totalprice - deposited;
                        }
                        if (leftMoney > 100)
                        {
                            return "0";
                        }
                        else
                        {
                            string[] IDs = listID.Split('|');
                            if (IDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < IDs.Length - 1; i++)
                                {
                                    int id = IDs[i].ToInt(0);
                                    var check = RequestOutStockController.GetBySmallpackageID(id);
                                    if (check == null)
                                    {
                                        var s = SmallPackageController.GetByID(id);
                                        if (s.Status == 3)
                                        {
                                            if (s.MainOrderID == mID)
                                            {
                                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(s.MainOrderID));
                                                if (mainorder != null)
                                                {
                                                    int UID = Convert.ToInt32(mainorder.UID);
                                                    if (UID == acc.ID)
                                                    {
                                                        //string kq = RequestOutStockController.Insert(id, s.OrderTransactionCode, Convert.ToInt32(s.MainOrderID),
                                                        //    Convert.ToInt32(s.OrderShopCodeID), 1, DateTime.Now, username);
                                                        //var quantri = AccountController.GetAllByRoleQuantri();
                                                        //if (quantri.Count > 0)
                                                        //{
                                                        //    foreach (var admin in quantri)
                                                        //    {
                                                        //        NotificationController.Inser(acc.ID, acc.Username, admin.ID,
                                                        //                                           admin.Username, id,
                                                        //                                           "Có yêu cầu xuất kho mới", 0, 7,
                                                        //                                           currentDate, acc.Username);
                                                        //    }
                                                        //}
                                                    }
                                                }
                                            }
                                            else
                                                return "none";
                                        }
                                    }
                                }
                                return "ok";
                            }
                        }

                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string requestoutstock(int id, int mID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                DateTime currentDate = DateTime.Now;
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var acc = AccountController.GetByUsername(username);
                if (acc != null)
                {
                    var check = RequestOutStockController.GetBySmallpackageID(id);
                    if (check == null)
                    {
                        double leftMoney = 0;
                        var mO = MainOrderController.GetAllByUIDAndID(acc.ID, mID);
                        if (mO != null)
                        {
                            double totalprice = Convert.ToDouble(mO.TotalPriceVND);
                            double deposited = Convert.ToDouble(mO.Deposit);
                            leftMoney = totalprice - deposited;
                        }
                        if (leftMoney > 100)
                        {
                            return "0";
                        }
                        else
                        {
                            var s = SmallPackageController.GetByID(id);
                            if (s.Status == 3)
                            {
                                var mainorder = MainOrderController.GetAllByID(Convert.ToInt32(s.MainOrderID));
                                if (mainorder != null)
                                {
                                    double totalprice = Convert.ToDouble(mainorder.TotalPriceVND);
                                    double deposited = Convert.ToDouble(mainorder.Deposit);
                                    double left = totalprice - deposited;
                                    if (left > 0)
                                    {
                                        return "0";
                                    }
                                    else
                                    {
                                        int UID = Convert.ToInt32(mainorder.UID);
                                        if (UID == acc.ID)
                                        {
                                            //string kq = RequestOutStockController.Insert(id, s.OrderTransactionCode, Convert.ToInt32(s.MainOrderID),
                                            //    Convert.ToInt32(s.OrderShopCodeID), 1, DateTime.Now, username);
                                            //var quantri = AccountController.GetAllByRoleQuantri();
                                            //if (quantri.Count > 0)
                                            //{
                                            //    foreach (var admin in quantri)
                                            //    {
                                            //        NotificationController.Inser(acc.ID, acc.Username, admin.ID,
                                            //                                           admin.Username, id,
                                            //                                           "Có yêu cầu xuất kho mới", 0, 10,
                                            //                                           currentDate, acc.Username);
                                            //    }
                                            //}

                                            //if (kq.ToInt(0) > 0)
                                            //    return "ok";
                                            //else return "none";
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return "none";
        }
        [WebMethod]
        public static string GetPrice(string listID)
        {
            double totalPrice = 0;
            if (!string.IsNullOrEmpty(listID))
            {
                if (HttpContext.Current.Session["userLoginSystem"] != null)
                {
                    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                    var u = AccountController.GetByUsername(username);
                    if (u != null)
                    {
                        double currency = 0;
                        var confi = ConfigurationController.GetByTop1();
                        if (confi != null)
                        {
                            currency = Convert.ToDouble(confi.Currency);
                        }
                        bool isLocal = false;
                        bool isAgent = false;
                        bool isVip = false;
                        if (u.IsLocal != null)
                            isLocal = Convert.ToBoolean(u.IsLocal);
                        if (u.IsAgent != null)
                            isAgent = Convert.ToBoolean(u.IsAgent);
                        if (u.IsVip != null)
                            isVip = Convert.ToBoolean(u.IsVip);
                        double totalWeight = 0;
                        int mID = HttpContext.Current.Session["oID"].ToString().ToInt(0);
                        var mainOrder = MainOrderController.GetAllByUIDAndID(u.ID, mID);
                        if (mainOrder != null)
                        {
                            //double currency = Convert.ToDouble(mainOrder.CurrentCNYVN);

                            string[] IDs = listID.Split('|');
                            if (IDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < IDs.Length - 1; i++)
                                {
                                    int ID = IDs[i].ToInt(0);
                                    var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                                    if (smallpackage != null)
                                    {
                                        if (smallpackage.Status == 3)
                                        {
                                            var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                                            if (check == null)
                                            {
                                                totalWeight += Convert.ToDouble(smallpackage.Weight);
                                            }
                                        }
                                    }
                                }
                            }
                            int warehouse = mainOrder.ReceivePlace.ToInt(1);
                            int shipping = Convert.ToInt32(mainOrder.ShippingType);

                            double returnprice = 0;
                            if (isLocal == true)
                            {
                                double feeweightlocal = 0;
                                if (confi != null)
                                {
                                    if (confi.AccountLocalWeightPrice != null)
                                    {
                                        feeweightlocal = Convert.ToDouble(confi.AccountLocalWeightPrice);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightlocal;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightlocal;
                                }
                            }
                            else if (isVip == true)
                            {
                                double feeweightVip = 0;
                                if (confi != null)
                                {
                                    if (confi.WeightPriceVip != null)
                                    {
                                        feeweightVip = Convert.ToDouble(confi.WeightPriceVip);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightVip;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightVip;
                                }
                            }
                            else if (isAgent == true)
                            {
                                double feeweightAgent = 0;
                                if (confi != null)
                                {
                                    if (confi.WeightPriceAgent != null)
                                    {
                                        feeweightAgent = Convert.ToDouble(confi.WeightPriceAgent);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightAgent;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightAgent;
                                }
                            }
                            else
                            {
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = 16;
                                }
                                else
                                {
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
                                        var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(4, 1, false);
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
                                }
                            }
                            returnprice = Math.Round(returnprice, 2);

                            double totalPriceWeightVND = 0;
                            double totalPriceWeightYNC = 0;
                            if (isLocal || isVip || isAgent)
                            {
                                totalPriceWeightVND = returnprice;
                                totalPriceWeightYNC = Math.Round(Convert.ToDouble(totalPriceWeightVND / currency), 2);
                            }
                            else
                            {
                                totalPriceWeightVND = Convert.ToDouble(returnprice * currency);
                                totalPriceWeightYNC = currency;
                            }
                            totalPriceWeightVND = Math.Round(totalPriceWeightVND, 0);
                            double mTotalPrice = Convert.ToDouble(mainOrder.TotalPriceVND);
                            double mDeposite = Convert.ToDouble(mainOrder.Deposit);
                            double mLeftPayNotWeight = mTotalPrice - mDeposite;
                            mLeftPayNotWeight = Math.Round(mLeftPayNotWeight, 0);
                            double mLeftPay = mTotalPrice + totalPriceWeightVND - mDeposite;
                            mLeftPay = Math.Round(mLeftPay, 0);
                            double mLeftPayCYN = mLeftPay / currency;
                            mLeftPayCYN = Math.Round(mLeftPayCYN, 2);
                            //totalPrice = Math.Round(mLeftPay,0);
                            totalPrice = mLeftPay;
                            double wallet = Math.Round(Convert.ToDouble(u.Wallet), 0);
                            if (wallet > 0)
                            {
                                if (wallet >= mLeftPay)
                                {
                                    string ret = mLeftPay.ToString() + ":1:" + Math.Round(totalWeight, 1) + ":" + mLeftPayCYN + ":" + mLeftPayNotWeight + ":" + totalPriceWeightVND + ":" + totalPriceWeightYNC;
                                    return ret;
                                }
                                else
                                {

                                    double rechargeMore = mLeftPay - wallet;
                                    rechargeMore = Math.Round(rechargeMore, 0);
                                    if (rechargeMore < 200)
                                    {
                                        string ret = mLeftPay.ToString() + ":1:" + Math.Round(totalWeight, 1) + ":" + mLeftPayCYN + ":" + mLeftPayNotWeight + ":" + totalPriceWeightVND + ":" + returnprice;
                                        return ret;
                                    }
                                    else
                                    {
                                        string ret = mLeftPay.ToString() + ":0:" + Math.Round(totalWeight, 1) + ":" + mLeftPayCYN + ":" + mLeftPayNotWeight + ":" + totalPriceWeightVND + ":" + returnprice + ":" + rechargeMore + ":" + wallet;
                                        return ret;
                                    }
                                }
                            }
                            else
                            {
                                double rechargeMore = mLeftPay - wallet;
                                rechargeMore = Math.Round(rechargeMore, 0);
                                if (rechargeMore < 200)
                                {
                                    string ret = mLeftPay.ToString() + ":1:" + Math.Round(totalWeight, 1) + ":" + mLeftPayCYN + ":" + mLeftPayNotWeight + ":" + totalPriceWeightVND + ":" + returnprice;
                                    return ret;
                                }
                                else
                                {
                                    string ret = mLeftPay.ToString() + ":0:" + Math.Round(totalWeight, 1) + ":" + mLeftPayCYN + ":" + mLeftPayNotWeight + ":" + totalPriceWeightVND + ":" + returnprice + ":" + rechargeMore + ":" + wallet;
                                    return ret;
                                }
                            }
                        }
                    }
                }
            }
            return totalPrice.ToString() + ":0";
        }
        [WebMethod]
        public static string PayOrderPrice(string listID, int shippingtype, string note)
        {
            double totalPrice = 0;
            DateTime currentDate = DateTime.Now;
            if (!string.IsNullOrEmpty(listID))
            {
                if (HttpContext.Current.Session["userLoginSystem"] != null)
                {
                    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                    var u = AccountController.GetByUsername(username);
                    if (u != null)
                    {
                        int uid = u.ID;
                        double totalWeight = 0;
                        int mID = HttpContext.Current.Session["oID"].ToString().ToInt(0);
                        var mainOrder = MainOrderController.GetAllByUIDAndID(uid, mID);
                        if (mainOrder != null)
                        {
                            double currency = Convert.ToDouble(mainOrder.CurrentCNYVN);
                            string[] IDs = listID.Split('|');
                            if (IDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < IDs.Length - 1; i++)
                                {
                                    int ID = IDs[i].ToInt(0);
                                    var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                                    if (smallpackage != null)
                                    {
                                        if (smallpackage.Status == 3)
                                        {
                                            var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                                            if (check == null)
                                            {
                                                totalWeight += Convert.ToDouble(smallpackage.Weight);
                                            }
                                        }
                                    }
                                }
                            }
                            int warehouse = mainOrder.ReceivePlace.ToInt(1);
                            int shipping = Convert.ToInt32(mainOrder.ShippingType);

                            double returnprice = 0;
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
                                var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(4, 1, false);
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
                            double totalPriceWeight = 0;
                            totalPriceWeight = returnprice * currency;
                            double mTotalPrice = Convert.ToDouble(mainOrder.TotalPriceVND);
                            double mDeposite = Convert.ToDouble(mainOrder.Deposit);
                            double mLeftPay = mTotalPrice + totalPriceWeight - mDeposite;

                            #region Tạo lượt xuất kho
                            string kq = ExportRequestTurnController.Insert(mID, currentDate, totalPriceWeight, returnprice,
                                totalWeight, note, shippingtype, currentDate, username);
                            int eID = kq.ToInt(0);
                            if (eID > 0)
                            {

                            }
                            #endregion

                            #region trừ tiền user
                            double wallet = Convert.ToDouble(u.Wallet);
                            if (wallet > 0)
                            {
                                if (wallet > mLeftPay)
                                {
                                    double walletLeft = wallet - mLeftPay;
                                    double payalll = mDeposite + mLeftPay;
                                    MainOrderController.UpdateStatus(mID, uid, 9);
                                    AccountController.updateWallet(uid, walletLeft, currentDate, username);

                                    HistoryOrderChangeController.Insert(mID, uid, username, username +
                                                " đã đổi trạng thái của đơn hàng ID là: " + mID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                    HistoryPayWalletController.Insert(uid, username, mID, mLeftPay, username + " đã thanh toán đơn hàng: " + mID + ".", walletLeft, 1, 3, currentDate, username);
                                    MainOrderController.UpdateDeposit(mID, uid, payalll.ToString());
                                    PayOrderHistoryController.Insert(mID, uid, 9, mLeftPay, 2, currentDate, username);
                                }
                            }
                            #endregion

                        }
                    }
                }
            }
            return totalPrice.ToString();
        }
        #endregion

        #region Class
        public class objProductOfShop
        {
            public string shopName { get; set; }
            public string shopID { get; set; }
            public string FeeShipVN { get; set; }
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
            public string totalPriceVND { get; set; }
            public string totalPriceCYN { get; set; }
            public string note { get; set; }
            public string status { get; set; }
            public bool isCensoreProduct { get; set; }
            public double priceCensorCYN { get; set; }
            public double priceCensorVND { get; set; }
        }
        public class OrderShopCode
        {
            public tbl_OrderShopCode ordershopCode { get; set; }
            public List<tbl_SmallPackage> smallPackage { get; set; }
        }
        #endregion

        protected void btnyeucau_Click(object sender, EventArgs e)
        {
            string listID = hdfListID.Value;
            int shippingtype = hdfShippingType.Value.ToInt(1);
            string note = hdfNote.Value;
            double totalPrice = 0;
            DateTime currentDate = DateTime.Now;

            if (!string.IsNullOrEmpty(listID))
            {
                if (HttpContext.Current.Session["userLoginSystem"] != null)
                {
                    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                    var u = AccountController.GetByUsername(username);
                    if (u != null)
                    {
                        double currency = 0;
                        var confi = ConfigurationController.GetByTop1();
                        if (confi != null)
                        {
                            currency = Convert.ToDouble(confi.Currency);
                        }
                        bool isLocal = false;
                        bool isAgent = false;
                        bool isVip = false;
                        if (u.IsLocal != null)
                            isLocal = Convert.ToBoolean(u.IsLocal);
                        if (u.IsAgent != null)
                            isAgent = Convert.ToBoolean(u.IsAgent);
                        if (u.IsVip != null)
                            isVip = Convert.ToBoolean(u.IsVip);

                        int uid = u.ID;
                        //double totalWeight = 0;
                        double totalWeight = 0;
                        int mID = HttpContext.Current.Session["oID"].ToString().ToInt(0);
                        var mainOrder = MainOrderController.GetAllByUIDAndID(uid, mID);
                        #region cách tính theo Float
                        if (mainOrder != null)
                        {
                            //double currency = Convert.ToDouble(mainOrder.CurrentCNYVN);
                            string[] IDs = listID.Split('|');
                            if (IDs.Length - 1 > 0)
                            {
                                for (int i = 0; i < IDs.Length - 1; i++)
                                {
                                    int ID = IDs[i].ToInt(0);
                                    var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                                    if (smallpackage != null)
                                    {
                                        if (smallpackage.Status == 3)
                                        {
                                            var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                                            if (check == null)
                                            {
                                                totalWeight += Convert.ToDouble(smallpackage.Weight);
                                                //totalWeight += smallpackage.Weight.ToString().ToFloat(0);
                                            }
                                        }
                                    }
                                }
                            }
                            totalWeight = Math.Round(totalWeight, 1);
                            int warehouse = mainOrder.ReceivePlace.ToInt(1);
                            int shipping = Convert.ToInt32(mainOrder.ShippingType);

                            //float returnprice = 0;
                            double returnprice = 0;
                            if (isLocal == true)
                            {
                                double feeweightlocal = 0;
                                if (confi != null)
                                {
                                    if (confi.AccountLocalWeightPrice != null)
                                    {
                                        feeweightlocal = Convert.ToDouble(confi.AccountLocalWeightPrice);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightlocal;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightlocal;
                                }
                            }
                            else if (isVip == true)
                            {
                                double feeweightVip = 0;
                                if (confi != null)
                                {
                                    if (confi.WeightPriceVip != null)
                                    {
                                        feeweightVip = Convert.ToDouble(confi.WeightPriceVip);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightVip;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightVip;
                                }
                            }
                            else if (isAgent == true)
                            {
                                double feeweightAgent = 0;
                                if (confi != null)
                                {
                                    if (confi.WeightPriceAgent != null)
                                    {
                                        feeweightAgent = Convert.ToDouble(confi.WeightPriceAgent);
                                    }
                                }
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = feeweightAgent;
                                }
                                else
                                {
                                    returnprice = totalWeight * feeweightAgent;
                                }
                            }
                            else
                            {
                                if (totalWeight > 0 && totalWeight <= 1)
                                {
                                    returnprice = 16;
                                }
                                else
                                {
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
                                                    //returnprice = totalWeight * f.Price.ToString().ToFloat();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(4, 1, false);
                                        if (fee.Count > 0)
                                        {
                                            foreach (var f in fee)
                                            {
                                                if (totalWeight > f.WeightFrom && totalWeight <= f.WeightTo)
                                                {
                                                    //returnprice = totalWeight * Convert.ToDouble(f.Price);
                                                    returnprice = totalWeight * Convert.ToDouble(f.Price);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            returnprice = Math.Round(returnprice, 2);
                            double totalPriceWeightVND = 0;
                            double totalPriceWeightYNC = 0;
                            if (isLocal || isVip || isAgent)
                            {
                                totalPriceWeightVND = returnprice;
                                totalPriceWeightYNC = Math.Round(Convert.ToDouble(totalPriceWeightVND / currency), 2);
                            }
                            else
                            {
                                totalPriceWeightVND = Convert.ToDouble(returnprice * currency);
                                totalPriceWeightYNC = currency;
                            }
                            //float totalPriceWeight = 0;
                            //double totalPriceWeight = 0;
                            //totalPriceWeight = Math.Round(returnprice, 2) * currency;
                            //float mTotalPrice = Convert.ToDouble(mainOrder.TotalPriceVND);
                            //float mDeposite = Convert.ToDouble(mainOrder.Deposit);
                            //float mTotalPrice = mainOrder.TotalPriceVND.ToFloat(0);
                            //float mDeposite = mainOrder.Deposit.ToFloat(0);

                            //float totalPriceNew = mTotalPrice + totalPriceWeight;
                            //float mLeftPay = totalPriceNew - mDeposite;

                            //float wallet = u.Wallet.ToString().ToFloat(0);
                            totalPriceWeightVND = Math.Round(totalPriceWeightVND, 0);
                            double mTotalPrice = Math.Round(Convert.ToDouble(mainOrder.TotalPriceVND), 0);
                            double mDeposite = Math.Round(Convert.ToDouble(mainOrder.Deposit), 0);

                            double totalPriceNew = mTotalPrice + Math.Round(totalPriceWeightVND, 0);
                            double mLeftPay = totalPriceNew - mDeposite;
                            double wallet = Math.Round(Convert.ToDouble(u.Wallet), 0);
                            mLeftPay = Math.Round(mLeftPay, 2);
                            if (wallet > 0)
                            {
                                if (wallet >= mLeftPay)
                                {
                                    MainOrderController.UpdateStatus(mID, uid, 9);
                                    #region Tạo lượt xuất kho
                                    string kq = ExportRequestTurnController.Insert(mID, currentDate, totalPriceWeightVND, totalPriceWeightYNC,
                                        totalWeight, note, shippingtype, currentDate, username);
                                    int eID = kq.ToInt(0);
                                    if (eID > 0)
                                    {
                                        if (IDs.Length - 1 > 0)
                                        {
                                            for (int i = 0; i < IDs.Length - 1; i++)
                                            {
                                                int ID = IDs[i].ToInt(0);
                                                var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                                                if (smallpackage != null)
                                                {
                                                    if (smallpackage.Status == 3)
                                                    {
                                                        var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                                                        if (check == null)
                                                        {
                                                            RequestOutStockController.Insert(ID, smallpackage.OrderTransactionCode,
                                                                Convert.ToInt32(smallpackage.MainOrderID),
                                                                Convert.ToInt32(smallpackage.OrderShopCodeID), 1, DateTime.Now, username, eID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Tính lại tiền cân nặng và tổng tiền cho đơn hàng
                                    double feeWeightOld = 0;
                                    double feeWeightNew = 0;
                                    if (!string.IsNullOrEmpty(mainOrder.FeeWeight))
                                    {
                                        feeWeightOld = Math.Round(Convert.ToDouble(mainOrder.FeeWeight), 0);
                                    }
                                    feeWeightNew = feeWeightOld + totalPriceWeightVND;
                                    feeWeightNew = Math.Round(feeWeightNew, 0);
                                    double orderWeight = 0;
                                    double orderWeightNew = 0;
                                    if (!string.IsNullOrEmpty(mainOrder.OrderWeight))
                                    {
                                        orderWeight = Math.Round(Convert.ToDouble(mainOrder.OrderWeight), 1);
                                    }
                                    orderWeightNew = orderWeight + totalWeight;
                                    orderWeightNew = Math.Round(orderWeightNew, 1);
                                    MainOrderController.UpdateOrderWeightFeeWeightTotalPrice(mID, orderWeightNew.ToString(),
                                        feeWeightNew.ToString(), totalPriceNew.ToString());
                                    #endregion
                                    #region Trừ tiền user
                                    double walletLeft = wallet - mLeftPay;
                                    double payalll = mDeposite + mLeftPay;
                                    walletLeft = Math.Round(walletLeft, 0);
                                    payalll = Math.Round(payalll, 0);
                                    AccountController.updateWallet(uid, walletLeft, currentDate, username);

                                    HistoryOrderChangeController.Insert(mID, uid, username, username +
                                                " đã đổi trạng thái của đơn hàng ID là: " + mID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                    HistoryPayWalletController.Insert(uid, username, mID, mLeftPay, username + " đã thanh toán đơn hàng: " + mID + ".", walletLeft, 1, 3, currentDate, username);
                                    MainOrderController.UpdateDeposit(mID, uid, payalll.ToString());
                                    PayOrderHistoryController.Insert(mID, uid, 9, mLeftPay, 2, currentDate, username);
                                    #endregion
                                    #region Gửi thông báo
                                    var admins = AccountController.GetAllByRoleID(0);
                                    if (admins.Count > 0)
                                    {
                                        foreach (var admin in admins)
                                        {
                                            NotificationController.Inser(uid, username, admin.ID,
                                                                               admin.Username, mID,
                                                                               "Có yêu cầu xuất kho của user: " + username, 0,
                                                                               10, currentDate, username);
                                        }
                                    }

                                    var managers = AccountController.GetAllByRoleID(2);
                                    if (managers.Count > 0)
                                    {
                                        foreach (var manager in managers)
                                        {
                                            NotificationController.Inser(uid, username, manager.ID,
                                                                               manager.Username, mID,
                                                                               "Có yêu cầu xuất kho của user: " + username, 0,
                                                                               10, currentDate, username);
                                        }
                                    }
                                    var khoVNs = AccountController.GetAllByRoleID(5);
                                    if (khoVNs.Count > 0)
                                    {
                                        foreach (var khoVN in khoVNs)
                                        {
                                            NotificationController.Inser(uid, username, khoVN.ID,
                                                                               khoVN.Username, mID,
                                                                               "Có yêu cầu xuất kho của user: " + username, 0,
                                                                               10, currentDate, username);
                                        }
                                    }
                                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                    hubContext.Clients.All.addNewMessageToPage("", "");
                                    #endregion
                                    try
                                    {
                                        string message = "";
                                        message += "Chào Qúy khách!<br/><br/>";
                                        message += "1688Express đã nhận được yêu cầu xuất kho của mã đơn hàng: #" + mID + " của Qúy khách.<br/><br/>";
                                        message += "Chúng tôi sẽ sắp xếp gửi hàng sớm nhất.<br/>";
                                        message += "Lưu ý: 1. Trường hợp gọi shipper, Qúy khách vui lòng để ý điện thoại khi 1688 thông báo gửi hàng, và chuẩn bị tiền mặt tương ứng để thuận tiện thanh toán.<br/>";
                                        message += "2. Trường hợp dùng chuyển phát Viettel post: <br/>";
                                        message += "Khi nhận hàng, Qúy khách vui lòng kiểm hàng và giữ lại hóa đơn, hộp sản phẩm. Nếu hàng hóa có bất cứ nhầm lẫn, sai sót gì từ phía shop bán, chúng tôi sẽ hỗ trợ khách hàng khiếu nại với shop bán. Để 1688Express có thể hỗ trợ khách hàng tốt nhất trong quá trình khiếu nại đơn hàng, Qúy khách vui lòng làm theo <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-khieu-nai-don-hang\" style=\"text-decoration:underline:\" target=\"_blank\">hướng dẫn</a><br/><br/>";
                                        message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                        message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                        PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", u.Email,
                                            "Yêu cầu xuất kho của bạn đã được chấp nhận tại 1688 Express", message, "");
                                    }
                                    catch
                                    {

                                    }
                                    MainOrderController.UpdateStatus(mID, uid, 9);
                                    PJUtils.ShowMessageBoxSwAlert("Gửi yêu cầu xuất kho thành công", "s", true, Page);
                                }
                                else
                                {
                                    double rechare = mLeftPay - wallet;
                                    if (rechare < 200)
                                    //if (rechare == 0)
                                    {
                                        MainOrderController.UpdateStatus(mID, uid, 9);
                                        #region Tạo lượt xuất kho
                                        string kq = ExportRequestTurnController.Insert(mID, currentDate, totalPriceWeightVND, returnprice,
                                            totalWeight, note, shippingtype, currentDate, username);
                                        int eID = kq.ToInt(0);
                                        if (eID > 0)
                                        {
                                            if (IDs.Length - 1 > 0)
                                            {
                                                for (int i = 0; i < IDs.Length - 1; i++)
                                                {
                                                    int ID = IDs[i].ToInt(0);
                                                    var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                                                    if (smallpackage != null)
                                                    {
                                                        if (smallpackage.Status == 3)
                                                        {
                                                            var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                                                            if (check == null)
                                                            {
                                                                RequestOutStockController.Insert(ID, smallpackage.OrderTransactionCode,
                                                                    Convert.ToInt32(smallpackage.MainOrderID),
                                                                    Convert.ToInt32(smallpackage.OrderShopCodeID), 1, DateTime.Now, username, eID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region Tính lại tiền cân nặng và tổng tiền cho đơn hàng
                                        double feeWeightOld = 0;
                                        double feeWeightNew = 0;
                                        if (!string.IsNullOrEmpty(mainOrder.FeeWeight))
                                        {
                                            feeWeightOld = Math.Round(Convert.ToDouble(mainOrder.FeeWeight), 0);
                                        }
                                        feeWeightNew = feeWeightOld + totalPriceWeightVND;
                                        feeWeightNew = Math.Round(feeWeightNew, 0);
                                        double orderWeight = 0;
                                        double orderWeightNew = 0;
                                        if (!string.IsNullOrEmpty(mainOrder.OrderWeight))
                                        {
                                            orderWeight = Math.Round(Convert.ToDouble(mainOrder.OrderWeight), 1);
                                        }
                                        orderWeightNew = orderWeight + totalWeight;
                                        orderWeightNew = Math.Round(orderWeightNew, 1);
                                        MainOrderController.UpdateOrderWeightFeeWeightTotalPrice(mID, orderWeightNew.ToString(),
                                            feeWeightNew.ToString(), totalPriceNew.ToString());

                                        #endregion
                                        #region Trừ tiền user
                                        //double walletLeft = wallet - mLeftPay;
                                        mLeftPay = Math.Round(mLeftPay, 0);
                                        double walletLeft = 0;
                                        double payalll = mDeposite + mLeftPay;
                                        payalll = Math.Round(payalll, 0);
                                        AccountController.updateWallet(uid, walletLeft, currentDate, username);

                                        HistoryOrderChangeController.Insert(mID, uid, username, username +
                                                    " đã đổi trạng thái của đơn hàng ID là: " + mID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                                        HistoryPayWalletController.Insert(uid, username, mID, mLeftPay, username + " đã thanh toán đơn hàng: " + mID + ".", walletLeft, 1, 3, currentDate, username);
                                        MainOrderController.UpdateDeposit(mID, uid, payalll.ToString());
                                        PayOrderHistoryController.Insert(mID, uid, 9, mLeftPay, 2, currentDate, username);
                                        #endregion
                                        #region Gửi thông báo
                                        var admins = AccountController.GetAllByRoleID(0);
                                        if (admins.Count > 0)
                                        {
                                            foreach (var admin in admins)
                                            {
                                                NotificationController.Inser(uid, username, admin.ID,
                                                                                   admin.Username, 0,
                                                                                   "Có yêu cầu xuất kho của user: " + username, 0,
                                                                                   10, currentDate, username);
                                            }
                                        }

                                        var managers = AccountController.GetAllByRoleID(2);
                                        if (managers.Count > 0)
                                        {
                                            foreach (var manager in managers)
                                            {
                                                NotificationController.Inser(uid, username, manager.ID,
                                                                                   manager.Username, 0,
                                                                                   "Có yêu cầu xuất kho của user: " + username, 0,
                                                                                   10, currentDate, username);
                                            }
                                        }
                                        var khoVNs = AccountController.GetAllByRoleID(5);
                                        if (khoVNs.Count > 0)
                                        {
                                            foreach (var khoVN in khoVNs)
                                            {
                                                NotificationController.Inser(uid, username, khoVN.ID,
                                                                                   khoVN.Username, 0,
                                                                                   "Có yêu cầu xuất kho của user: " + username, 0,
                                                                                   10, currentDate, username);
                                            }
                                        }
                                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                        hubContext.Clients.All.addNewMessageToPage("", "");
                                        #endregion
                                        try
                                        {
                                            string message = "";
                                            message += "Chào Qúy khách!<br/><br/>";
                                            message += "1688Express đã nhận được yêu cầu xuất kho của mã đơn hàng: #" + mID + " của Qúy khách.<br/><br/>";
                                            message += "Chúng tôi sẽ sắp xếp gửi hàng sớm nhất.<br/>";
                                            message += "Lưu ý: 1. Trường hợp gọi shipper, Qúy khách vui lòng để ý điện thoại khi 1688 thông báo gửi hàng, và chuẩn bị tiền mặt tương ứng để thuận tiện thanh toán.<br/>";
                                            message += "2. Trường hợp dùng chuyển phát Viettel post: <br/>";
                                            message += "Khi nhận hàng, Qúy khách vui lòng kiểm hàng và giữ lại hóa đơn, hộp sản phẩm. Nếu hàng hóa có bất cứ nhầm lẫn, sai sót gì từ phía shop bán, chúng tôi sẽ hỗ trợ khách hàng khiếu nại với shop bán. Để 1688Express có thể hỗ trợ khách hàng tốt nhất trong quá trình khiếu nại đơn hàng, Qúy khách vui lòng làm theo <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-khieu-nai-don-hang\" style=\"text-decoration:underline:\" target=\"_blank\">hướng dẫn</a><br/><br/>";
                                            message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                            message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                            PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", u.Email,
                                                "Yêu cầu xuất kho của bạn đã được chấp nhận tại 1688 Express", message, "");
                                        }
                                        catch
                                        {

                                        }
                                        PJUtils.ShowMessageBoxSwAlert("Gửi yêu cầu xuất kho thành công", "s", true, Page);
                                    }
                                    else
                                    {
                                        PJUtils.ShowMessageBoxSwAlert("Tài khoản của bạn không đủ để thanh toán", "e", true, Page);
                                    }

                                }
                            }
                        }
                        #endregion
                        #region Cách tính theo double
                        //if (mainOrder != null)
                        //{
                        //    double currency = Convert.ToDouble(mainOrder.CurrentCNYVN);
                        //    string[] IDs = listID.Split('|');
                        //    if (IDs.Length - 1 > 0)
                        //    {
                        //        for (int i = 0; i < IDs.Length - 1; i++)
                        //        {
                        //            int ID = IDs[i].ToInt(0);
                        //            var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                        //            if (smallpackage != null)
                        //            {
                        //                if (smallpackage.Status == 3)
                        //                {
                        //                    var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                        //                    if (check == null)
                        //                    {
                        //                        totalWeight += Convert.ToDouble(smallpackage.Weight);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    int warehouse = mainOrder.ReceivePlace.ToInt(1);
                        //    int shipping = Convert.ToInt32(mainOrder.ShippingType);                        
                        //    double returnprice = 0;
                        //    if (totalWeight <= 0.5)
                        //    {
                        //        returnprice = 11;
                        //    }
                        //    else
                        //    {
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
                        //            var fee = WarehouseFeeController.GetByAndWarehouseFromAndToWarehouseAndShippingTypeAndAndHelpMoving(4, 1, false);
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
                        //    }

                        //    double totalPriceWeight = 0;
                        //    totalPriceWeight = returnprice * currency;
                        //    double mTotalPrice = Convert.ToDouble(mainOrder.TotalPriceVND);
                        //    double mDeposite = Convert.ToDouble(mainOrder.Deposit);
                        //    double totalPriceNew = mTotalPrice + totalPriceWeight;
                        //    double mLeftPay = totalPriceNew - mDeposite;

                        //    double wallet = Convert.ToDouble(u.Wallet);
                        //    if (wallet > 0)
                        //    {
                        //        if (wallet >= mLeftPay)
                        //        {
                        //            #region Tạo lượt xuất kho
                        //            string kq = ExportRequestTurnController.Insert(mID, currentDate, totalPriceWeight, returnprice,
                        //                totalWeight, note, shippingtype, currentDate, username);
                        //            int eID = kq.ToInt(0);
                        //            if (eID > 0)
                        //            {
                        //                if (IDs.Length - 1 > 0)
                        //                {
                        //                    for (int i = 0; i < IDs.Length - 1; i++)
                        //                    {
                        //                        int ID = IDs[i].ToInt(0);
                        //                        var smallpackage = SmallPackageController.GetByIDAndMainOrderID(ID, mID);
                        //                        if (smallpackage != null)
                        //                        {
                        //                            if (smallpackage.Status == 3)
                        //                            {
                        //                                var check = RequestOutStockController.GetBySmallpackageID(smallpackage.ID);
                        //                                if (check == null)
                        //                                {
                        //                                    RequestOutStockController.Insert(ID, smallpackage.OrderTransactionCode,
                        //                                        Convert.ToInt32(smallpackage.MainOrderID),
                        //                                        Convert.ToInt32(smallpackage.OrderShopCodeID), 1, DateTime.Now, username, eID);
                        //                                }
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            #endregion
                        //            #region Tính lại tiền cân nặng và tổng tiền cho đơn hàng
                        //            double feeWeightOld = 0;
                        //            double feeWeightNew = 0;
                        //            if (!string.IsNullOrEmpty(mainOrder.FeeWeight))
                        //            {
                        //                feeWeightOld = Convert.ToDouble(mainOrder.FeeWeight);
                        //            }
                        //            feeWeightNew = feeWeightOld + totalPriceWeight;

                        //            double orderWeight = 0;
                        //            double orderWeightNew = 0;
                        //            if (!string.IsNullOrEmpty(mainOrder.OrderWeight))
                        //            {
                        //                orderWeight = Convert.ToDouble(mainOrder.OrderWeight);
                        //            }
                        //            orderWeightNew = orderWeight + totalWeight;

                        //            MainOrderController.UpdateOrderWeightFeeWeightTotalPrice(mID, orderWeightNew.ToString(),
                        //                feeWeightNew.ToString(), totalPriceNew.ToString());
                        //            #endregion
                        //            #region Trừ tiền user
                        //            double walletLeft = wallet - mLeftPay;
                        //            double payalll = mDeposite + mLeftPay;
                        //            MainOrderController.UpdateStatus(mID, uid, 9);
                        //            AccountController.updateWallet(uid, walletLeft, currentDate, username);

                        //            HistoryOrderChangeController.Insert(mID, uid, username, username +
                        //                        " đã đổi trạng thái của đơn hàng ID là: " + mID + ", từ: Chờ thanh toán, sang: Khách đã thanh toán.", 1, currentDate);

                        //            HistoryPayWalletController.Insert(uid, username, mID, mLeftPay, username + " đã thanh toán đơn hàng: " + mID + ".", walletLeft, 1, 3, currentDate, username);
                        //            MainOrderController.UpdateDeposit(mID, uid, payalll.ToString());
                        //            PayOrderHistoryController.Insert(mID, uid, 9, mLeftPay, 2, currentDate, username);
                        //            #endregion
                        //            PJUtils.ShowMessageBoxSwAlert("Gửi yêu cầu xuất kho thành công", "s", true, Page);
                        //        }
                        //        else
                        //        {
                        //            PJUtils.ShowMessageBoxSwAlert("Tài khoản của bạn không đủ để thanh toán", "e", true, Page);
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                }
            }
        }
    }
}