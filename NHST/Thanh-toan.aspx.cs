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
    public partial class Thanh_toan1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    var rq = RouteData.Values["order"].ToString();
                    if (!string.IsNullOrEmpty(rq))
                    {
                        //string rq = Request.QueryString["order"];
                        UpdateCheck(rq);
                    }
                    else
                    {
                        Response.Redirect("/gio-hang");
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                //LoadReceiPlace();
            }
        }
        public void LoadReceiPlace()
        {
            //var dt = WarehouseController.GetAllWithIsHidden(false);
            //ddlWarehouseID.Items.Clear();
            //if (dt.Count > 0)
            //{
            //    foreach (var item in dt)
            //    {
            //        ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
            //        ddlWarehouseID.Items.Add(listitem);
            //    }
            //}
            //ddlWarehouseID.DataBind();
        }
        public void UpdateCheck(string rq)
        {
            double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
            //Load User Info
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                if (rq.Contains("all_"))
                {
                    string[] splitList = rq.Split('_');
                    string[] list = splitList[1].Split('|');
                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            int ID = list[i].ToInt(0);
                            if (ID > 0)
                            {
                                var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                                if (shop != null)
                                {
                                    if (shop.IsCheckProduct == true)
                                    {

                                        double total = 0;
                                        var listpro = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                        double counpros = 0;
                                        if (listpro.Count > 0)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                counpros += item.quantity.ToInt(1);
                                            }
                                        }
                                        //var count = listpro.Count;
                                        if (counpros >= 1 && counpros <= 2)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                double price_orig = Convert.ToDouble(item.price_origin);
                                                if (price_orig < 10)
                                                    total = total + (1500 * item.quantity.ToInt(1));
                                                else if (price_orig >= 10)
                                                    total = total + (5000 * item.quantity.ToInt(1));
                                            }
                                        }
                                        else if (counpros > 2 && counpros <= 10)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                double price_orig = Convert.ToDouble(item.price_origin);
                                                if (price_orig < 10)
                                                    total = total + (1000 * item.quantity.ToInt(1));
                                                else if (price_orig >= 10)
                                                    total = total + (3500 * item.quantity.ToInt(1));
                                            }
                                        }
                                        else if (counpros > 10 && counpros <= 100)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                double price_orig = Convert.ToDouble(item.price_origin);
                                                if (price_orig < 10)
                                                    total = total + (700 * item.quantity.ToInt(1));
                                                else if (price_orig >= 10)
                                                    total = total + (2000 * item.quantity.ToInt(1));
                                            }
                                        }
                                        else if (counpros > 100 && counpros <= 500)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                double price_orig = Convert.ToDouble(item.price_origin);
                                                if (price_orig < 10)
                                                    total = total + (700 * item.quantity.ToInt(1));
                                                else if (price_orig >= 10)
                                                    total = total + (1500 * item.quantity.ToInt(1));
                                            }
                                        }
                                        else if (counpros > 500)
                                        {
                                            foreach (var item in listpro)
                                            {
                                                double price_orig = Convert.ToDouble(item.price_origin);
                                                if (price_orig < 10)
                                                    total = total + (700 * item.quantity.ToInt(1));
                                                else if (price_orig >= 10)
                                                    total = total + (1000 * item.quantity.ToInt(1));
                                            }
                                        }
                                        OrderShopTempController.UpdateCheckProductPrice(shop.ID, total.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    int orderID = Convert.ToInt32(rq);
                    var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, orderID);
                    if (shop != null)
                    {
                        if (shop.IsCheckProduct == true)
                        {

                            double total = 0;
                            var listpro = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                            double counpros = 0;
                            if (listpro.Count > 0)
                            {
                                foreach (var item in listpro)
                                {
                                    counpros += item.quantity.ToInt(1);
                                }
                            }
                            //var count = listpro.Count;
                            if (counpros >= 1 && counpros <= 2)
                            {
                                foreach (var item in listpro)
                                {
                                    double price_orig = Convert.ToDouble(item.price_origin);
                                    if (price_orig < 10)
                                        total = total + (1500 * item.quantity.ToInt(1));
                                    else if (price_orig >= 10)
                                        total = total + (5000 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 2 && counpros <= 10)
                            {
                                foreach (var item in listpro)
                                {
                                    double price_orig = Convert.ToDouble(item.price_origin);
                                    if (price_orig < 10)
                                        total = total + (1000 * item.quantity.ToInt(1));
                                    else if (price_orig >= 10)
                                        total = total + (3500 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 10 && counpros <= 100)
                            {
                                foreach (var item in listpro)
                                {
                                    double price_orig = Convert.ToDouble(item.price_origin);
                                    if (price_orig < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (price_orig >= 10)
                                        total = total + (2000 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 100 && counpros <= 500)
                            {
                                foreach (var item in listpro)
                                {
                                    double price_orig = Convert.ToDouble(item.price_origin);
                                    if (price_orig < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (price_orig >= 10)
                                        total = total + (1500 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 500)
                            {
                                foreach (var item in listpro)
                                {
                                    double price_orig = Convert.ToDouble(item.price_origin);
                                    if (price_orig < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (price_orig >= 10)
                                        total = total + (1000 * item.quantity.ToInt(1));
                                }
                            }
                            OrderShopTempController.UpdateCheckProductPrice(shop.ID, total.ToString());
                        }
                    }
                }
            }
            LoadData(rq);
        }

        public void LoadData(string rq)
        {
            try
            {
                Session.Remove("orderitem");
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                //Load User Info
                string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                var warehouses = WarehouseController.GetAllWithIsHidden(false);
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
                    bool isLocal = false;
                    if (obj_user.IsLocal != null)
                    {
                        isLocal = Convert.ToBoolean(obj_user.IsLocal);
                    }
                    var ui = AccountInfoController.GetByUserID(obj_user.ID);
                    double FeeBuyProduct = 0;
                    double UL_CKFeeBuyPro = 0;
                    double UL_CKFeeWeight = 0;
                    if (ui != null)
                    {
                        txt_Fullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_DFullname.Text = ui.FirstName + " " + ui.LastName;
                        txt_Address.Text = ui.Address;
                        txt_DAddress.Text = ui.Address;
                        txt_Email.Text = ui.Email;
                        txt_DEmail.Text = ui.Email;
                        //txt_Phone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        //txt_DPhone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        txt_Phone.Text = ui.Phone;
                        txt_DPhone.Text = ui.Phone;
                        UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                    }
                    if (rq.Contains("all_"))
                    {
                        string[] splitList = rq.Split('_');
                        string[] list = splitList[1].Split('|');
                        if (list.Length - 1 > 0)
                        {
                            Session["orderitem"] = rq;
                            double totalfinal = 0;
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                int ID = list[i].ToInt(0);
                                var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                                if (shop != null)
                                {
                                    double fastprice = 0;
                                    double pricepro = Convert.ToDouble(shop.PriceVND);
                                    double priceproCYN = Convert.ToDouble(shop.PriceCNY);

                                    //if (shop.IsFast == true)
                                    //{
                                    //    fastprice = (pricepro * 5 / 100);
                                    //}

                                    double total = fastprice + pricepro;

                                    //double FeeCNShip = 10 * current;
                                    double FeeCNShip = 0;
                                    double FeeBuyPro = 0;
                                    double FeeCheck = 0;
                                    if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                                        FeeCheck = Convert.ToDouble(shop.IsCheckProductPrice);

                                    double totalFee_CountFee = total + FeeCNShip + FeeCheck;
                                    double servicefee = 0;
                                    double serviceFeeMoney = 0;

                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (priceproCYN >= item.AmountFrom && priceproCYN < item.AmountTo)
                                            {
                                                servicefee = Convert.ToDouble(item.FeePercent) / 100;
                                                serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                            }
                                        }
                                    }

                                    double feebpnotdc = (pricepro * servicefee + serviceFeeMoney) * current;
                                    double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                    double feebp = feebpnotdc - subfeebp;
                                    //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                                    FeeBuyPro = feebp;

                                    if (isLocal == true)
                                    {
                                        FeeBuyPro = 0;
                                        UL_CKFeeBuyPro = 0;
                                    }

                                    total = total + FeeCNShip + FeeBuyPro + FeeCheck;

                                    totalfinal += total;
                                    ltr_pro.Text += "<div class=\"order-detail\">";
                                    ltr_pro.Text += "   <table class=\"ordershoptem\" data-id=\"" + shop.ID + "\">";
                                    ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                    ltr_pro.Text += "           <td colspan=\"3\"><h4 class=\"title\">" + shop.ShopName + "</h4></td>";
                                    ltr_pro.Text += "       </tr>";
                                    var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);

                                    if (proOrdertemp != null)
                                    {
                                        if (proOrdertemp.Count > 0)
                                        {
                                            foreach (var item in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(item.quantity);
                                                double originprice = Convert.ToDouble(item.price_origin);
                                                double promotionprice = Convert.ToDouble(item.price_promotion);
                                                double u_pricecbuy = 0;
                                                double u_pricevn = 0;
                                                double e_pricebuy = 0;
                                                double e_pricevn = 0;
                                                double e_pricetemp = 0;
                                                double e_totalproduct = 0;
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                    u_pricevn = promotionprice * current;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }

                                                e_pricebuy = u_pricecbuy * quantity;
                                                e_pricevn = u_pricevn * quantity;
                                                string image = item.image_origin;
                                                if (image.Contains("%2F"))
                                                {
                                                    image = image.Replace("%2F", "/");
                                                }
                                                if (image.Contains("%3A"))
                                                {
                                                    image = image.Replace("%3A", ":");
                                                }
                                                ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                                ltr_pro.Text += "           <td colspan=\"2\">";
                                                ltr_pro.Text += "               <div class=\"thumb-product\">";
                                                ltr_pro.Text += "                   <div class=\"pd-img\">";
                                                ltr_pro.Text += "                       <img src=\"" + image + "\" alt=\"\"><span class=\"badge\">" + item.quantity + "</span>";
                                                ltr_pro.Text += "                   </div>";
                                                ltr_pro.Text += "                   <div class=\"info\"><a href=\"" + item.link_origin + "\">" + item.title_origin + "</a></div>";
                                                ltr_pro.Text += "               </div>";
                                                ltr_pro.Text += "           </td>";
                                                ltr_pro.Text += "           <td>";
                                                ltr_pro.Text += "               <strong>" + string.Format("{0:N0}", e_pricevn) + "vnđ</strong>";
                                                ltr_pro.Text += "           </td>";
                                                ltr_pro.Text += "       </tr>";
                                            }
                                        }
                                    }

                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    //ltr_pro.Text += "           <td style=\"width:20%;\"><strong>" + string.Format("{0:N0}", FeeCNShip) + " vnđ</strong></td>";
                                    ltr_pro.Text += "           <td style=\"width:20%;\"><strong>Chờ cập nhật</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    if (UL_CKFeeBuyPro > 0)
                                        ltr_pro.Text += "           <td>Phí hải quan cố định (CK: " + UL_CKFeeBuyPro + "%)</td>";
                                    else
                                        ltr_pro.Text += "           <td>Phí hải quan cố định</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeBuyPro) + " vnđ</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    //ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "       <tr>";
                                    if (UL_CKFeeWeight > 0)
                                        ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN (CK: " + UL_CKFeeWeight + "%)</td>";
                                    else
                                        ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td>Phí kiểm đếm</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    if (shop.IsCheckProduct == true)
                                        ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeCheck) + "</strong></td>";
                                    else
                                        ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td>Phí đóng gỗ</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    if (shop.IsPacked == true)
                                        ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                    else
                                        ltr_pro.Text += "           <td><strong>...</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td>Phí ship giao hàng tận nhà</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    if (shop.IsFastDelivery == true)
                                        ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                                    else
                                        ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    //ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                    //ltr_pro.Text += "           <td>Phí đơn hàng hỏa tốc</td>";
                                    //ltr_pro.Text += "           <td></td>";
                                    //if (shop.IsFast == true)
                                    //    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", fastprice) + "vnđ</strong></td>";
                                    //else
                                    //    ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                                    //ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng tiền</strong></td>";
                                    ltr_pro.Text += "           <td></td>";
                                    ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
                                    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Chuyển về kho</strong></td>";
                                    ltr_pro.Text += "           <td colspan=\"2\">";
                                    ltr_pro.Text += "               <select class=\"form-control warehoseselect\" onchange=\"getWareHouse($(this))\">";
                                    if (warehouses.Count > 0)
                                    {
                                        foreach (var w in warehouses)
                                        {
                                            ltr_pro.Text += "<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>";
                                        }
                                    }

                                    ltr_pro.Text += "                   ";
                                    ltr_pro.Text += "               </select>";
                                    ltr_pro.Text += "           </td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr class=\"shippingtype\" style=\"display:none\">";
                                    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Phương thức vận chuyển</strong></td>";
                                    ltr_pro.Text += "           <td colspan=\"2\">";
                                    ltr_pro.Text += "               <select class=\"form-control shippingtypesselect\">";
                                    ltr_pro.Text += "                   <option value=\"1\">Đi nhanh</option>";
                                    ltr_pro.Text += "                   <option value=\"2\">Đi thường</option>";
                                    ltr_pro.Text += "               </select>";
                                    ltr_pro.Text += "           </td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "   </table>";
                                    ltr_pro.Text += "</div>";
                                }
                            }
                            ltr_pro.Text += "<div class=\"order-detail\">";
                            ltr_pro.Text += "   <table>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng hóa đơn</strong></td>";
                            ltr_pro.Text += "           <td></td>";
                            ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", totalfinal) + "vnđ</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "   </table>";
                            ltr_pro.Text += "</div>";
                        }
                    }
                    else
                    {
                        int orderID = Convert.ToInt32(rq);
                        var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, orderID);
                        double total = 0;
                        if (shop != null)
                        {
                            Session["orderitem"] = orderID;
                            double fastprice = 0;
                            double pricepro = Convert.ToDouble(shop.PriceVND);
                            double priceproCYN = Convert.ToDouble(shop.PriceVND) / current;
                            //double servicefee = 0;
                            //var adminfeebuypro = FeeBuyProController.GetAll();
                            //if (adminfeebuypro.Count > 0)
                            //{
                            //    foreach (var item in adminfeebuypro)
                            //    {
                            //        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                            //        {
                            //            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                            //        }
                            //    }
                            //}

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

                            //double feebpnotdc = pricepro * servicefee;
                            //double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                            //double feebp = feebpnotdc - subfeebp;

                            //if (shop.IsFast == true)
                            //{
                            //    fastprice = (pricepro * 5 / 100);
                            //}
                            total = fastprice + pricepro;
                            ltr_pro.Text += "<div class=\"order-detail\">";
                            ltr_pro.Text += "   <table class=\"ordershoptem\" data-id=\"" + shop.ID + "\">";
                            ltr_pro.Text += "       <tr class=\"borderbtm\">";
                            ltr_pro.Text += "           <td colspan=\"3\"><h4 class=\"title\">" + shop.ShopName + "</h4></td>";
                            ltr_pro.Text += "       </tr>";
                            var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);

                            if (proOrdertemp != null)
                            {
                                if (proOrdertemp.Count > 0)
                                {
                                    foreach (var item in proOrdertemp)
                                    {
                                        int quantity = Convert.ToInt32(item.quantity);
                                        double originprice = Convert.ToDouble(item.price_origin);
                                        double promotionprice = Convert.ToDouble(item.price_promotion);
                                        double u_pricecbuy = 0;
                                        double u_pricevn = 0;
                                        double e_pricebuy = 0;
                                        double e_pricevn = 0;
                                        double e_pricetemp = 0;
                                        double e_totalproduct = 0;
                                        if (promotionprice < originprice)
                                        {
                                            u_pricecbuy = promotionprice;
                                            u_pricevn = promotionprice * current;
                                        }
                                        else
                                        {
                                            u_pricecbuy = originprice;
                                            u_pricevn = originprice * current;
                                        }

                                        e_pricebuy = u_pricecbuy * quantity;
                                        e_pricevn = u_pricevn * quantity;
                                        string image = item.image_origin;
                                        if (image.Contains("%2F"))
                                        {
                                            image = image.Replace("%2F", "/");
                                        }
                                        if (image.Contains("%3A"))
                                        {
                                            image = image.Replace("%3A", ":");
                                        }
                                        ltr_pro.Text += "       <tr class=\"borderbtm\">";
                                        ltr_pro.Text += "           <td colspan=\"2\">";
                                        ltr_pro.Text += "               <div class=\"thumb-product\">";
                                        ltr_pro.Text += "                   <div class=\"pd-img\">";
                                        ltr_pro.Text += "                       <img src=\"" + image + "\" alt=\"\"><span class=\"badge\">" + item.quantity + "</span>";
                                        ltr_pro.Text += "                   </div>";
                                        ltr_pro.Text += "                   <div class=\"info\"><a href=\"" + item.link_origin + "\">" + item.title_origin + "</a></div>";
                                        ltr_pro.Text += "               </div>";
                                        ltr_pro.Text += "           </td>";
                                        ltr_pro.Text += "           <td>";
                                        ltr_pro.Text += "               <strong>" + string.Format("{0:N0}", e_pricevn) + "vnđ</strong>";
                                        ltr_pro.Text += "           </td>";
                                        ltr_pro.Text += "       </tr>";
                                    }
                                }
                            }
                            //double FeeCNShip = 10 * current;
                            double FeeCNShip = 0 * current;
                            double FeeBuyPro = 0;
                            double FeeCheck = 0;
                            if (shop.IsCheckProductPrice.ToFloat(0) > 0)
                                FeeCheck = Convert.ToDouble(shop.IsCheckProductPrice);

                            double totalFee_CountFee = total + FeeCNShip + FeeCheck;
                            double servicefee = 0;
                            double servicefeeMoney = 0;

                            var adminfeebuypro = FeeBuyProController.GetAll();
                            if (adminfeebuypro.Count > 0)
                            {
                                foreach (var item in adminfeebuypro)
                                {
                                    if (priceproCYN >= item.AmountFrom && priceproCYN < item.AmountTo)
                                    {
                                        servicefee = Convert.ToDouble(item.FeePercent) / 100;
                                        servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                    }
                                }
                            }

                            double feebpnotdc = (pricepro * servicefee + servicefeeMoney) * current;
                            double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                            double feebp = feebpnotdc - subfeebp;
                            //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                            FeeBuyPro = feebp;

                            if (isLocal == true)
                            {
                                FeeBuyPro = 0;
                                UL_CKFeeBuyPro = 0;
                            }

                            total = total + FeeCNShip + FeeBuyPro + FeeCheck;
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                            ltr_pro.Text += "           <td></td>";
                            //ltr_pro.Text += "           <td style=\"width:20%;\"><strong>" + string.Format("{0:N0}", FeeCNShip) + " vnđ</strong></td>";
                            ltr_pro.Text += "           <td style=\"width:20%;\"><strong>Chờ cập nhật</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            if (UL_CKFeeBuyPro > 0)
                                ltr_pro.Text += "           <td>Phí hải quan cố định (Đã CK: " + UL_CKFeeBuyPro + "%)</td>";
                            else
                                ltr_pro.Text += "           <td>Phí hải quan cố định</td>";
                            ltr_pro.Text += "           <td></td>";
                            ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeBuyPro) + " vnđ</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            if (UL_CKFeeWeight > 0)
                                ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN (Đã CK: " + UL_CKFeeWeight + "%)</td>";
                            else
                                ltr_pro.Text += "           <td>Phí vận chuyển TQ - VN</td>";
                            ltr_pro.Text += "           <td></td>";
                            ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td>Phí kiểm đếm</td>";
                            ltr_pro.Text += "           <td></td>";
                            if (shop.IsCheckProduct == true)
                                ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeCheck) + "</strong></td>";
                            else
                                ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td>Phí đóng gỗ</td>";
                            ltr_pro.Text += "           <td></td>";
                            if (shop.IsPacked == true)
                                ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                            else
                                ltr_pro.Text += "           <td><strong>...</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td>Phí ship giao hàng tận nhà</td>";
                            ltr_pro.Text += "           <td></td>";
                            if (shop.IsFastDelivery == true)
                                ltr_pro.Text += "           <td><strong>Chờ cập nhật</strong></td>";
                            else
                                ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            //ltr_pro.Text += "       <tr class=\"borderbtm\">";
                            //ltr_pro.Text += "           <td>Phí đơn hàng hỏa tốc</td>";
                            //ltr_pro.Text += "           <td></td>";
                            //if (shop.IsFast == true)
                            //    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", fastprice) + "vnđ</strong></td>";
                            //else
                            //    ltr_pro.Text += "           <td><strong>Không yêu cầu</strong></td>";
                            //ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng tiền</strong></td>";
                            ltr_pro.Text += "           <td></td>";
                            ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Chuyển về kho</strong></td>";
                            ltr_pro.Text += "           <td colspan=\"2\">";
                            ltr_pro.Text += "               <select class=\"form-control warehoseselect\" onchange=\"getWareHouse($(this))\">";
                            if (warehouses.Count > 0)
                            {
                                foreach (var w in warehouses)
                                {
                                    ltr_pro.Text += "<option value=\"" + w.ID + "\">" + w.WareHouseName + "</option>";
                                }
                            }

                            ltr_pro.Text += "                   ";
                            ltr_pro.Text += "               </select>";
                            ltr_pro.Text += "           </td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr class=\"shippingtype\" style=\"display:none\">";
                            ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Phương thức vận chuyển</strong></td>";
                            ltr_pro.Text += "           <td colspan=\"2\">";
                            ltr_pro.Text += "               <select class=\"form-control shippingtypesselect\">";
                            ltr_pro.Text += "                   <option value=\"1\">Đi nhanh</option>";
                            ltr_pro.Text += "                   <option value=\"2\">Đi thường</option>";
                            ltr_pro.Text += "               </select>";
                            ltr_pro.Text += "           </td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "   </table>";
                            ltr_pro.Text += "</div>";

                            ltr_pro.Text += "<div class=\"order-detail\">";
                            ltr_pro.Text += "   <table>";
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng hóa đơn</strong></td>";
                            ltr_pro.Text += "           <td></td>";
                            ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "   </table>";
                            ltr_pro.Text += "</div>";
                        }
                        else
                        {
                            Response.Redirect("/gio-hang");
                        }
                        #region order old
                        //var order = OrderTempController.GetByUIDAndID(obj_user.ID, orderID);

                        //if (order != null)
                        //{
                        //    Session["orderitem"] = orderID;
                        //    double pricepro = Convert.ToInt32(order.quantity) * Convert.ToDouble(order.price_origin) * current;
                        //    double fastprice = 0;
                        //    if (order.IsFast == true)
                        //    {
                        //        fastprice = (pricepro / 2);
                        //    }
                        //    total = fastprice + pricepro;
                        //    string image = order.image_origin;
                        //    if (image.Contains("%2F"))
                        //    {
                        //        image = image.Replace("%2F", "/");
                        //    }
                        //    if (image.Contains("%3A"))
                        //    {
                        //        image = image.Replace("%3A", ":");
                        //    }
                        //    ltr_pro.Text += "<div class=\"order-detail\">";
                        //    ltr_pro.Text += "   <table>";
                        //    ltr_pro.Text += "       <tr class=\"borderbtm\">";
                        //    ltr_pro.Text += "           <td colspan=\"2\">";
                        //    ltr_pro.Text += "               <div class=\"thumb-product\">";
                        //    ltr_pro.Text += "                   <div class=\"pd-img\">";
                        //    ltr_pro.Text += "                       <img src=\"" + image + "\" alt=\"\"><span class=\"badge\">" + order.quantity + "</span>";
                        //    ltr_pro.Text += "                   </div>";
                        //    ltr_pro.Text += "                   <div class=\"info\"><a href=\"" + order.link_origin + "\">" + order.title_origin + "</a></div>";
                        //    ltr_pro.Text += "               </div>";
                        //    ltr_pro.Text += "           </td>";
                        //    ltr_pro.Text += "           <td>";
                        //    ltr_pro.Text += "               <strong>" + string.Format("{0:N0}", pricepro) + "vnđ</strong>";
                        //    ltr_pro.Text += "           </td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí mua hàng</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí cân nặng</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí kiểm đếm</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí đóng gỗ</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td>Phí ship giao hàng tận nhà</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr class=\"borderbtm\">";
                        //    ltr_pro.Text += "           <td>Phí đơn hàng hỏa tốc</td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", fastprice) + "vnđ</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng tiền</strong></td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "   </table>";
                        //    ltr_pro.Text += "</div>";

                        //    ltr_pro.Text += "<div class=\"order-detail\">";
                        //    ltr_pro.Text += "   <table>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng hóa đơn</strong></td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "   </table>";
                        //    ltr_pro.Text += "</div>";
                        //}
                        //else
                        //{
                        //    Response.Redirect("/gio-hang");
                        //}
                        #endregion
                    }
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
            catch
            {
                Response.Redirect("/gio-hang");
            }

        }

        protected void btn_saveOrder_Click(object sender, EventArgs e)
        {
            if (chk_DK.Checked)
            {
                double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
                if (Session["orderitem"] != null)
                {
                    string username = HttpContext.Current.Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(username);
                    string rq = Session["orderitem"].ToString();
                    if (obj_user != null)
                    {
                        bool isLocal = false;
                        if (obj_user.IsLocal != null)
                        {
                            isLocal = Convert.ToBoolean(obj_user.IsLocal);
                        }
                        int salerID = obj_user.SaleID.ToString().ToInt(0);

                        int dathangID = 0;
                        int dathangIDget = obj_user.DathangID.ToString().ToInt(0);
                        if (dathangIDget == 0)
                        {
                            var co = ConfigurationController.GetByTop1();
                            if (co != null)
                            {
                                if (co.AccountDathangID > 0)
                                {
                                    dathangID = Convert.ToInt32(co.AccountDathangID);
                                }
                            }
                        }
                        else
                        {
                            dathangID = dathangIDget;
                        }

                        int UID = obj_user.ID;

                        //double percent_User = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LevelPercent);
                        double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                        double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                        string wareship = hdfTeamWare.Value;
                        if (rq.Contains("all_"))
                        {
                            string[] splitList = rq.Split('_');
                            string[] list = splitList[1].Split('|');

                            if (list.Length - 1 > 0)
                            {
                                double totalfinal = 0;
                                for (int i = 0; i < list.Length - 1; i++)
                                {
                                    int ID = list[i].ToInt(0);

                                    var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, ID);
                                    if (shop != null)
                                    {
                                        int warehouseID = 0;
                                        int w_shippingType = 0;
                                        string[] w = wareship.Split('|');
                                        if (w.Length - 1 > 0)
                                        {
                                            for (int j = 0; j < w.Length - 1; j++)
                                            {
                                                int shoptempID = (w[j].Split(':')[0]).ToInt(0);
                                                string[] wsinfor = w[j].Split(':')[1].Split('-');
                                                int wareID = (wsinfor[0]).ToInt(1);
                                                int shippingtype = (wsinfor[1]).ToInt(1);
                                                if (ID == shoptempID)
                                                {
                                                    warehouseID = wareID;
                                                    w_shippingType = shippingtype;
                                                }
                                            }
                                        }

                                        double total = 0;
                                        double fastprice = 0;
                                        double pricepro = Convert.ToDouble(shop.PriceVND);
                                        double priceproCYN = Convert.ToDouble(shop.PriceCNY);
                                        //double servicefee = 0;
                                        //var adminfeebuypro = FeeBuyProController.GetAll();
                                        //if (adminfeebuypro.Count > 0)
                                        //{
                                        //    foreach (var item in adminfeebuypro)
                                        //    {
                                        //        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        //        {
                                        //            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                        //        }
                                        //    }
                                        //}
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

                                        //double feebpnotdc = pricepro * servicefee;
                                        //double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                        //double feebp = 0;
                                        //feebp = feebpnotdc - subfeebp;
                                        double feecnship = 0;
                                        //feecnship = 10 * current;

                                        if (shop.IsFast == true)
                                        {
                                            fastprice = (pricepro * 5 / 100);
                                        }
                                        //total = fastprice + pricepro + feebp + feecnship;
                                        string ShopID = shop.ShopID;
                                        string ShopName = shop.ShopName;
                                        string Site = shop.Site;
                                        bool IsForward = Convert.ToBoolean(shop.IsForward);
                                        string IsForwardPrice = shop.IsForwardPrice;
                                        bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                                        string IsFastDeliveryPrice = shop.IsFastDeliveryPrice;
                                        bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                                        string IsCheckProductPrice = shop.IsCheckProductPrice;
                                        bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                                        string IsPackedPrice = shop.IsPackedPrice;
                                        bool IsFast = Convert.ToBoolean(shop.IsFast);
                                        string IsFastPrice = fastprice.ToString();
                                        double pricecynallproduct = 0;

                                        double totalFee_CountFee = fastprice + pricepro + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                                        double servicefee = 0;
                                        double servicefeeMoney = 0;

                                        var adminfeebuypro = FeeBuyProController.GetAll();
                                        if (adminfeebuypro.Count > 0)
                                        {
                                            foreach (var item in adminfeebuypro)
                                            {
                                                if (priceproCYN >= item.AmountFrom && priceproCYN < item.AmountTo)
                                                {
                                                    servicefee = Convert.ToDouble(item.FeePercent) / 100;
                                                    servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                                }
                                            }
                                        }

                                        double feebpnotdc = (pricepro * servicefee + servicefeeMoney) * current;
                                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                        double feebp = feebpnotdc - subfeebp;
                                        if (isLocal == true)
                                        {
                                            feebp = 0;
                                            UL_CKFeeBuyPro = 0;
                                        }

                                        //double feebp = totalFee_CountFee * UL_CKFeeBuyPro / 100;
                                        total = fastprice + pricepro + feebp + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                                        //Lấy ra từng ordertemp trong shop
                                        var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                                        if (proOrdertemp != null)
                                        {
                                            if (proOrdertemp.Count > 0)
                                            {
                                                foreach (var item in proOrdertemp)
                                                {
                                                    int quantity = Convert.ToInt32(item.quantity);
                                                    double originprice = Convert.ToDouble(item.price_origin);
                                                    double promotionprice = Convert.ToDouble(item.price_promotion);

                                                    double u_pricecbuy = 0;
                                                    double u_pricevn = 0;
                                                    double e_pricebuy = 0;
                                                    double e_pricevn = 0;
                                                    if (promotionprice < originprice)
                                                    {
                                                        u_pricecbuy = promotionprice;
                                                        u_pricevn = promotionprice * current;
                                                    }
                                                    else
                                                    {
                                                        u_pricecbuy = originprice;
                                                        u_pricevn = originprice * current;
                                                    }

                                                    e_pricebuy = u_pricecbuy * quantity;
                                                    e_pricevn = u_pricevn * quantity;

                                                    pricecynallproduct += e_pricebuy;
                                                }
                                            }
                                        }
                                        string PriceVND = Math.Round(Convert.ToDouble(shop.PriceVND),0).ToString();
                                        string PriceCNY = pricecynallproduct.ToString();
                                        //string FeeShipCN = (10 * current).ToString();
                                        string FeeShipCN = feecnship.ToString();
                                        string FeeBuyPro = Math.Round(feebp,0).ToString();
                                        string FeeWeight = shop.FeeWeight;
                                        string Note = shop.Note;
                                        string FullName = txt_DFullname.Text.Trim();
                                        string Address = txt_DAddress.Text.Trim();
                                        string Email = txt_DEmail.Text.Trim();
                                        string Phone = txt_DPhone.Text.Trim();
                                        int Status = 0;
                                        string Deposit = "0";
                                        string CurrentCNYVN = current.ToString();
                                        string TotalPriceVND = Math.Round(total,0).ToString();
                                        string AmountDeposit = Math.Round((total * LessDeposit / 100),0).ToString();
                                        DateTime CreatedDate = DateTime.Now;
                                        string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice,
                                            IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN,
                                            TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1);
                                        int idkq = Convert.ToInt32(kq);
                                        if (idkq > 0)
                                        {
                                            foreach (var item in proOrdertemp)
                                            {
                                                int quantity = Convert.ToInt32(item.quantity);
                                                double originprice = Convert.ToDouble(item.price_origin);
                                                double promotionprice = Convert.ToDouble(item.price_promotion);
                                                double u_pricecbuy = 0;
                                                double u_pricevn = 0;
                                                double e_pricebuy = 0;
                                                double e_pricevn = 0;
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                    u_pricevn = promotionprice * current;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                    u_pricevn = originprice * current;
                                                }

                                                e_pricebuy = u_pricecbuy * quantity;
                                                e_pricevn = u_pricevn * quantity;

                                                pricecynallproduct += e_pricebuy;

                                                string image = item.image_origin;
                                                if (image.Contains("%2F"))
                                                {
                                                    image = image.Replace("%2F", "/");
                                                }
                                                if (image.Contains("%3A"))
                                                {
                                                    image = image.Replace("%3A", ":");
                                                }
                                                string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                                                item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                                                item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                                                item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                                                Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                                                fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                                                txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID);

                                                if (item.price_promotion.ToFloat(0) > 0)
                                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                                                else
                                                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                                            }
                                            MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);
                                            var admins = AccountController.GetAllByRoleID(0);
                                            if (admins.Count > 0)
                                            {
                                                foreach (var admin in admins)
                                                {
                                                    NotificationController.Inser(UID, username, admin.ID,
                                                                                       admin.Username, idkq,
                                                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                                                                                       1, CreatedDate, username);
                                                }
                                            }

                                            var managers = AccountController.GetAllByRoleID(2);
                                            if (managers.Count > 0)
                                            {
                                                foreach (var manager in managers)
                                                {
                                                    NotificationController.Inser(UID, username, manager.ID,
                                                                                       manager.Username, 0,
                                                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                                                                                       1, CreatedDate, username);
                                                }
                                            }
                                        }
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

                                        if (salerID > 0)
                                        {
                                            var sale = AccountController.GetByID(salerID);
                                            if (sale != null)
                                            {
                                                salerName = sale.Username;
                                                var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                                int d = CreatedDate.Subtract(createdDate).Days;
                                                if (d > 90)
                                                {
                                                    double per = feebp * salepercentaf3m / 100;
                                                    StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                    CreatedDate, CreatedDate, username);
                                                }
                                                else
                                                {
                                                    double per = feebp * salepercent / 100;
                                                    StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                    CreatedDate, CreatedDate, username);
                                                }
                                            }
                                        }
                                        if (dathangID > 0)
                                        {
                                            var dathang = AccountController.GetByID(dathangID);
                                            if (dathang != null)
                                            {
                                                dathangName = dathang.Username;
                                                StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                                                    CreatedDate, CreatedDate, username);
                                                NotificationController.Inser(UID, username, dathang.ID,
                                                                               dathang.Username, idkq,
                                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                                               1, CreatedDate, username);
                                            }
                                        }
                                        //Xóa Shop temp và order temp
                                        OrderShopTempController.Delete(shop.ID);
                                    }
                                }
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");

                                try
                                {
                                    string message = "";
                                    message += "Chào Qúy khách!<br/><br/>";
                                    message += "Đơn hàng của Qúy khách đã được tạo thành công trên hệ thống, và hiện đang trong trạng thái chờ đặt cọc.<br/><br/>";
                                    message += "Để tiến hành đặt cọc, Qúy khách vui lòng thanh toán tiền tạm ứng đơn hàng bằng cách Nạp tiền vào tài khoản ( xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a> ) và thực hiện theo đúng hướng dẫn, để 1688Express có thể xác nhận và nạp tiền nhanh nhất.<br/><br/>";
                                    message += "Qúy khách lưu ý: Số tiền đặt cọc phải lớn hơn hoặc bằng số tiền hiển thị trên hệ thống, nếu không, đơn hàng sẽ không được chuyển đến nhân viên đặt hàng.<br/><br/>";
                                    message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                    message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                    PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", obj_user.Email,
                                        "Tạo thành công đơn hàng chờ đặt cọc tại 1688 Express", message, "");
                                }
                                catch
                                {

                                }
                                //StringBuilder sb = new System.Text.StringBuilder();
                                //sb.Append(@"<script language='javascript'>");
                                //sb.Append(@"showFinishOrder();");                                
                                //sb.Append(@"</script>");

                                /////hàm để đăng ký javascript và thực thi đoạn script trên
                                //if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                                //{
                                //    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                                //}
                                Session["ordersucces"] = "ok";
                                Response.Redirect("/danh-sach-don-hang");
                            }
                        }
                        else
                        {
                            int IDshop = Convert.ToInt32(rq);
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, IDshop);
                            int warehouseID = 0;
                            int w_shippingType = 0;
                            double total = 0;
                            if (shop != null)
                            {
                                string[] w = wareship.Split('|');
                                if (w.Length - 1 > 0)
                                {
                                    for (int j = 0; j < w.Length - 1; j++)
                                    {
                                        int shoptempID = (w[j].Split(':')[0]).ToInt(0);
                                        string[] wsinfor = w[j].Split(':')[1].Split('-');
                                        int wareID = (wsinfor[0]).ToInt(1);
                                        int shippingtype = (wsinfor[1]).ToInt(1);
                                        if (IDshop == shoptempID)
                                        {
                                            warehouseID = wareID;
                                            w_shippingType = shippingtype;
                                        }
                                    }
                                }
                                double fastprice = 0;

                                //double priceproCYN = Convert.ToDouble(shop.PriceCNY);
                                double priceproCYN = 0;
                                List<tbl_OrderTemp> ors = OrderTempController.GetAllByOrderShopTempIDAndUID(UID, shop.ID);
                                if (ors != null)
                                {
                                    if (ors.Count > 0)
                                    {
                                        foreach (var item in ors)
                                        {
                                            int ID = item.ID;
                                            string linkproduct = item.link_origin;
                                            string productname = item.title_origin;
                                            string brand = item.brand;
                                            string image = item.image_origin;
                                            int quantity = Convert.ToInt32(item.quantity);
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);
                                            double u_pricecbuy = 0;
                                            double u_pricevn = 0;
                                            double e_pricebuy = 0;
                                            double e_pricevn = 0;
                                            double e_pricetemp = 0;
                                            double e_totalproduct = 0;
                                            if (promotionprice > 0)
                                            {
                                                if (promotionprice < originprice)
                                                {
                                                    u_pricecbuy = promotionprice;
                                                }
                                                else
                                                {
                                                    u_pricecbuy = originprice;
                                                }
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                            }
                                            e_pricebuy = u_pricecbuy * quantity;
                                            priceproCYN += e_pricebuy;
                                        }
                                    }
                                }
                                double pricepro = priceproCYN * current;

                                double servicefee = 0;
                                double servicefeeMoney = 0;
                                var adminfeebuypro = FeeBuyProController.GetAll();
                                if (adminfeebuypro.Count > 0)
                                {
                                    foreach (var item in adminfeebuypro)
                                    {
                                        if (priceproCYN >= item.AmountFrom && priceproCYN < item.AmountTo)
                                        {
                                            servicefee = Convert.ToDouble(item.FeePercent) / 100;
                                            servicefeeMoney = Convert.ToDouble(item.FeeMoney);
                                        }
                                    }
                                }

                                double feebpnotdc = (pricepro * servicefee + servicefeeMoney) * current;
                                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                double feebp = 0;
                                feebp = feebpnotdc - subfeebp;
                                if (isLocal == true)
                                {
                                    feebp = 0;
                                    UL_CKFeeBuyPro = 0;
                                }

                                //double feecnship = 10 * current;
                                double feecnship = 0;
                                if (shop.IsFast == true)
                                {
                                    fastprice = (pricepro * 5 / 100);
                                }

                                string ShopID = shop.ShopID;
                                string ShopName = shop.ShopName;
                                string Site = shop.Site;
                                bool IsForward = Convert.ToBoolean(shop.IsForward);
                                string IsForwardPrice = shop.IsForwardPrice;
                                bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                                string IsFastDeliveryPrice = shop.IsFastDeliveryPrice;
                                bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                                string IsCheckProductPrice = shop.IsCheckProductPrice;
                                bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                                string IsPackedPrice = shop.IsPackedPrice;
                                bool IsFast = Convert.ToBoolean(shop.IsFast);
                                string IsFastPrice = fastprice.ToString();
                                double pricecynallproduct = 0;
                                double totalQuantityProduct = 0;

                                //Lấy ra từng ordertemp trong shop
                                var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(IDshop);
                                if (proOrdertemp != null)
                                {
                                    if (proOrdertemp.Count > 0)
                                    {
                                        foreach (var item in proOrdertemp)
                                        {

                                            int quantity = Convert.ToInt32(item.quantity);
                                            totalQuantityProduct += quantity;
                                            double originprice = Convert.ToDouble(item.price_origin);
                                            double promotionprice = Convert.ToDouble(item.price_promotion);

                                            double u_pricecbuy = 0;
                                            double u_pricevn = 0;
                                            double e_pricebuy = 0;
                                            double e_pricevn = 0;
                                            if (promotionprice < originprice)
                                            {
                                                u_pricecbuy = promotionprice;
                                                u_pricevn = promotionprice * current;
                                            }
                                            else
                                            {
                                                u_pricecbuy = originprice;
                                                u_pricevn = originprice * current;
                                            }

                                            e_pricebuy = u_pricecbuy * quantity;
                                            e_pricevn = u_pricevn * quantity;

                                            pricecynallproduct += e_pricebuy;
                                        }
                                    }
                                }

                                double total1 = 0;
                                if (IsCheckProduct == true)
                                {

                                    if (totalQuantityProduct >= 1 && totalQuantityProduct <= 2)
                                    {
                                        total1 = total1 + (5000 * totalQuantityProduct);
                                    }
                                    else if (totalQuantityProduct > 2 && totalQuantityProduct <= 20)
                                    {
                                        total1 = total1 + (3500 * totalQuantityProduct);
                                    }
                                    else if (totalQuantityProduct > 20 && totalQuantityProduct <= 100)
                                    {
                                        total1 = total1 + (2000 * totalQuantityProduct);
                                    }
                                    else if (totalQuantityProduct > 100)
                                    {
                                        total1 = total1 + (1500 * totalQuantityProduct);
                                    }
                                    IsCheckProductPrice = total1.ToString();
                                }
                                total = fastprice + pricepro + feebp + feecnship + total1;
                                string PriceVND = Math.Round(Convert.ToDouble(shop.PriceVND), 0).ToString();
                                string PriceCNY = pricecynallproduct.ToString();
                                string FeeShipCN = feecnship.ToString();
                                string FeeBuyPro = Math.Round(feebp, 0).ToString();
                                string FeeWeight = shop.FeeWeight;
                                string Note = shop.Note;
                                string FullName = txt_DFullname.Text.Trim();
                                string Address = txt_DAddress.Text.Trim();
                                string Email = txt_DEmail.Text.Trim();
                                string Phone = txt_DPhone.Text.Trim();
                                int Status = 0;
                                string Deposit = "0";
                                string CurrentCNYVN = current.ToString();
                                string TotalPriceVND = Math.Round(total, 0).ToString();
                                string AmountDeposit = Math.Round((total * LessDeposit / 100), 0).ToString();
                                DateTime CreatedDate = DateTime.Now;
                                string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice,
                                    IsCheckProduct, IsCheckProductPrice, IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro,
                                    FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN, TotalPriceVND, salerID, dathangID,
                                    CreatedDate, UID, AmountDeposit, 1);
                                int idkq = Convert.ToInt32(kq);
                                if (idkq > 0)
                                {
                                    foreach (var item in proOrdertemp)
                                    {
                                        int quantity = Convert.ToInt32(item.quantity);
                                        double originprice = Convert.ToDouble(item.price_origin);
                                        double promotionprice = Convert.ToDouble(item.price_promotion);
                                        double u_pricecbuy = 0;
                                        double u_pricevn = 0;
                                        double e_pricebuy = 0;
                                        double e_pricevn = 0;
                                        if (promotionprice < originprice)
                                        {
                                            u_pricecbuy = promotionprice;
                                            u_pricevn = promotionprice * current;
                                        }
                                        else
                                        {
                                            u_pricecbuy = originprice;
                                            u_pricevn = originprice * current;
                                        }

                                        e_pricebuy = u_pricecbuy * quantity;
                                        e_pricevn = u_pricevn * quantity;

                                        pricecynallproduct += e_pricebuy;

                                        string image = item.image_origin;
                                        if (image.Contains("%2F"))
                                        {
                                            image = image.Replace("%2F", "/");
                                        }
                                        if (image.Contains("%3A"))
                                        {
                                            image = image.Replace("%3A", ":");
                                        }
                                        string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                                        item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                                        item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                                        item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                                        Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                                        fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                                        txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, CreatedDate, UID);

                                        if (item.price_promotion.ToFloat(0) > 0)
                                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                                        else
                                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                                    }
                                    MainOrderController.UpdateReceivePlace(idkq, UID, warehouseID.ToString(), w_shippingType);

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

                                    if (salerID > 0)
                                    {
                                        var sale = AccountController.GetByID(salerID);
                                        if (sale != null)
                                        {
                                            salerName = sale.Username;
                                            var createdDate = Convert.ToDateTime(sale.CreatedDate);
                                            int d = CreatedDate.Subtract(createdDate).Days;
                                            if (d > 90)
                                            {
                                                double per = feebp * salepercentaf3m / 100;
                                                StaffIncomeController.Insert(idkq, feebp.ToString(), salepercentaf3m.ToString(), salerID, salerName, 6, 1,
                                                    per.ToString(), false, CreatedDate, CreatedDate, username);
                                            }
                                            else
                                            {
                                                double per = feebp * salepercent / 100;
                                                StaffIncomeController.Insert(idkq, feebp.ToString(), salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                                                CreatedDate, CreatedDate, username);
                                            }
                                        }
                                    }
                                    if (dathangID > 0)
                                    {
                                        var dathang = AccountController.GetByID(dathangID);
                                        if (dathang != null)
                                        {
                                            dathangName = dathang.Username;
                                            StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                                                CreatedDate, CreatedDate, username);
                                            NotificationController.Inser(UID, username, dathang.ID,
                                                                               dathang.Username, idkq,
                                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                                               1, CreatedDate, username);
                                        }
                                    }

                                    //MainOrderController.UpdateReceivePlace(idkq, UID, ddlWarehouseID.SelectedValue);
                                    var admins = AccountController.GetAllByRoleID(0);
                                    if (admins.Count > 0)
                                    {
                                        foreach (var admin in admins)
                                        {
                                            NotificationController.Inser(UID, username, admin.ID,
                                                                               admin.Username, idkq,
                                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                                               1, CreatedDate, username);
                                        }
                                    }

                                    var managers = AccountController.GetAllByRoleID(2);
                                    if (managers.Count > 0)
                                    {
                                        foreach (var manager in managers)
                                        {
                                            NotificationController.Inser(UID, username, manager.ID,
                                                                               manager.Username, 0,
                                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                                                                               1, CreatedDate, username);
                                        }
                                    }
                                }
                                try
                                {
                                    string message = "";
                                    message += "Chào Qúy khách!<br/><br/>";
                                    message += "Đơn hàng của Qúy khách đã được tạo thành công trên hệ thống, và hiện đang trong trạng thái chờ đặt cọc.<br/><br/>";
                                    message += "Để tiến hành đặt cọc, Qúy khách vui lòng thanh toán tiền tạm ứng đơn hàng bằng cách Nạp tiền vào tài khoản ( xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" style=\"text-decoration:underline:\" target=\"_blank\">TẠI ĐÂY</a> ) và thực hiện theo đúng hướng dẫn, để 1688Express có thể xác nhận và nạp tiền nhanh nhất.<br/><br/>";
                                    message += "Qúy khách lưu ý: Số tiền đặt cọc phải lớn hơn hoặc bằng số tiền hiển thị trên hệ thống, nếu không, đơn hàng sẽ không được chuyển đến nhân viên đặt hàng.<br/><br/>";
                                    message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                    message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                    PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", obj_user.Email,
                                        "Tạo thành công đơn hàng chờ đặt cọc tại 1688 Express", message, "");
                                }
                                catch
                                {

                                }
                                //Xóa Shop temp và order temp
                                string kqdel = OrderShopTempController.Delete(shop.ID);
                                if (kqdel == "1")
                                {
                                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                    hubContext.Clients.All.addNewMessageToPage("", "");
                                    StringBuilder sb = new System.Text.StringBuilder();
                                    sb.Append(@"<script language='javascript'>");
                                    sb.Append(@"showFinishOrder();");
                                    sb.Append(@"</script>");

                                    ///hàm để đăng ký javascript và thực thi đoạn script trên
                                    //if (!ClientScript.IsStartupScriptRegistered("JSScript"))
                                    //{
                                    //    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                                    //}
                                    Session["ordersucces"] = "ok";
                                    Response.Redirect("/danh-sach-don-hang");
                                }
                            }
                        }
                        #region Code Cũ
                        //if (rq != "all")
                        //{
                        //    int IDshop = Convert.ToInt32(rq);
                        //    var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, IDshop);

                        //    double total = 0;
                        //    if (shop != null)
                        //    {
                        //        double fastprice = 0;
                        //        double pricepro = Convert.ToDouble(shop.PriceVND);
                        //        double servicefee = 0;
                        //        var adminfeebuypro = FeeBuyProController.GetAll();
                        //        if (adminfeebuypro.Count > 0)
                        //        {
                        //            foreach (var item in adminfeebuypro)
                        //            {
                        //                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                        //                {
                        //                    servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                        //                }
                        //            }
                        //        }

                        //        double feebpnotdc = pricepro * servicefee;
                        //        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        //        double feebp = 0;
                        //        feebp = feebpnotdc - subfeebp;
                        //        //double feecnship = 10 * current;
                        //        double feecnship = 0;
                        //        if (shop.IsFast == true)
                        //        {
                        //            fastprice = (pricepro * 5 / 100);
                        //        }

                        //        string ShopID = shop.ShopID;
                        //        string ShopName = shop.ShopName;
                        //        string Site = shop.Site;
                        //        bool IsForward = Convert.ToBoolean(shop.IsForward);
                        //        string IsForwardPrice = shop.IsForwardPrice;
                        //        bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                        //        string IsFastDeliveryPrice = shop.IsFastDeliveryPrice;
                        //        bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                        //        string IsCheckProductPrice = shop.IsCheckProductPrice;
                        //        bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                        //        string IsPackedPrice = shop.IsPackedPrice;
                        //        bool IsFast = Convert.ToBoolean(shop.IsFast);
                        //        string IsFastPrice = fastprice.ToString();
                        //        double pricecynallproduct = 0;
                        //        total = fastprice + pricepro + feebp + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                        //        //Lấy ra từng ordertemp trong shop
                        //        var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(IDshop);
                        //        if (proOrdertemp != null)
                        //        {
                        //            if (proOrdertemp.Count > 0)
                        //            {
                        //                foreach (var item in proOrdertemp)
                        //                {
                        //                    int quantity = Convert.ToInt32(item.quantity);
                        //                    double originprice = Convert.ToDouble(item.price_origin);
                        //                    double promotionprice = Convert.ToDouble(item.price_promotion);

                        //                    double u_pricecbuy = 0;
                        //                    double u_pricevn = 0;
                        //                    double e_pricebuy = 0;
                        //                    double e_pricevn = 0;
                        //                    if (promotionprice < originprice)
                        //                    {
                        //                        u_pricecbuy = promotionprice;
                        //                        u_pricevn = promotionprice * current;
                        //                    }
                        //                    else
                        //                    {
                        //                        u_pricecbuy = originprice;
                        //                        u_pricevn = originprice * current;
                        //                    }

                        //                    e_pricebuy = u_pricecbuy * quantity;
                        //                    e_pricevn = u_pricevn * quantity;

                        //                    pricecynallproduct += e_pricebuy;
                        //                }
                        //            }
                        //        }
                        //        string PriceVND = shop.PriceVND;
                        //        string PriceCNY = pricecynallproduct.ToString();
                        //        string FeeShipCN = feecnship.ToString();
                        //        string FeeBuyPro = feebp.ToString();
                        //        string FeeWeight = shop.FeeWeight;
                        //        string Note = shop.Note;
                        //        string FullName = txt_DFullname.Text.Trim();
                        //        string Address = txt_DAddress.Text.Trim();
                        //        string Email = txt_DEmail.Text.Trim();
                        //        string Phone = txt_DPhone.Text.Trim();
                        //        int Status = 0;
                        //        string Deposit = "0";
                        //        string CurrentCNYVN = current.ToString();
                        //        string TotalPriceVND = total.ToString();
                        //        string AmountDeposit = (total * LessDeposit / 100).ToString();
                        //        DateTime CreatedDate = DateTime.Now;
                        //        string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice,
                        //            IsCheckProduct, IsCheckProductPrice, IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro,
                        //            FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN, TotalPriceVND, salerID, dathangID,
                        //            CreatedDate, UID, AmountDeposit);
                        //        int idkq = Convert.ToInt32(kq);
                        //        if (idkq > 0)
                        //        {
                        //            foreach (var item in proOrdertemp)
                        //            {
                        //                int quantity = Convert.ToInt32(item.quantity);
                        //                double originprice = Convert.ToDouble(item.price_origin);
                        //                double promotionprice = Convert.ToDouble(item.price_promotion);
                        //                double u_pricecbuy = 0;
                        //                double u_pricevn = 0;
                        //                double e_pricebuy = 0;
                        //                double e_pricevn = 0;
                        //                if (promotionprice < originprice)
                        //                {
                        //                    u_pricecbuy = promotionprice;
                        //                    u_pricevn = promotionprice * current;
                        //                }
                        //                else
                        //                {
                        //                    u_pricecbuy = originprice;
                        //                    u_pricevn = originprice * current;
                        //                }

                        //                e_pricebuy = u_pricecbuy * quantity;
                        //                e_pricevn = u_pricevn * quantity;

                        //                pricecynallproduct += e_pricebuy;

                        //                string image = item.image_origin;
                        //                if (image.Contains("%2F"))
                        //                {
                        //                    image = image.Replace("%2F", "/");
                        //                }
                        //                if (image.Contains("%3A"))
                        //                {
                        //                    image = image.Replace("%3A", ":");
                        //                }
                        //                string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                        //                item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                        //                item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                        //                item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                        //                Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                        //                fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                        //                txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, CreatedDate, UID);

                        //                if (item.price_promotion.ToFloat(0) > 0)
                        //                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                        //                else
                        //                    OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                        //            }

                        //            double salepercent = 0;
                        //            double salepercentaf3m = 0;
                        //            double dathangpercent = 0;
                        //            var config = ConfigurationController.GetByTop1();
                        //            if (config != null)
                        //            {
                        //                salepercent = Convert.ToDouble(config.SalePercent);
                        //                salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                        //                dathangpercent = Convert.ToDouble(config.DathangPercent);
                        //            }
                        //            string salerName = "";
                        //            string dathangName = "";

                        //            if (salerID > 0)
                        //            {
                        //                var sale = AccountController.GetByID(salerID);
                        //                if (sale != null)
                        //                {
                        //                    salerName = sale.Username;
                        //                    var createdDate = Convert.ToDateTime(sale.CreatedDate);
                        //                    int d = CreatedDate.Subtract(createdDate).Days;
                        //                    if (d > 90)
                        //                    {
                        //                        double per = feebp * salepercentaf3m / 100;
                        //                        StaffIncomeController.Insert(idkq, feebp.ToString(), salepercentaf3m.ToString(), salerID, salerName, 6, 1,
                        //                            per.ToString(), false, CreatedDate, CreatedDate, username);
                        //                    }
                        //                    else
                        //                    {
                        //                        double per = feebp * salepercent / 100;
                        //                        StaffIncomeController.Insert(idkq, feebp.ToString(), salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                        //                        CreatedDate, CreatedDate, username);
                        //                    }
                        //                }
                        //            }
                        //            if (dathangID > 0)
                        //            {
                        //                var dathang = AccountController.GetByID(dathangID);
                        //                if (dathang != null)
                        //                {
                        //                    dathangName = dathang.Username;
                        //                    StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                        //                        CreatedDate, CreatedDate, username);
                        //                }
                        //            }

                        //            MainOrderController.UpdateReceivePlace(idkq, UID, ddlReceivePlace.SelectedValue);
                        //            var admins = AccountController.GetAllByRoleID(0);
                        //            if (admins.Count > 0)
                        //            {
                        //                foreach (var admin in admins)
                        //                {
                        //                    NotificationController.Inser(UID, username, admin.ID,
                        //                                                       admin.Username, idkq,
                        //                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                        //                                                       CreatedDate, username);
                        //                }
                        //            }

                        //            var managers = AccountController.GetAllByRoleID(2);
                        //            if (managers.Count > 0)
                        //            {
                        //                foreach (var manager in managers)
                        //                {
                        //                    NotificationController.Inser(UID, username, manager.ID,
                        //                                                       manager.Username, 0,
                        //                                                       "Có đơn hàng mới ID là: " + idkq, 0,
                        //                                                       CreatedDate, username);
                        //                }
                        //            }
                        //        }
                        //        //Xóa Shop temp và order temp
                        //        string kqdel = OrderShopTempController.Delete(shop.ID);
                        //        if (kqdel == "1")
                        //            Response.Redirect("/danh-sach-don-hang");
                        //    }
                        //}
                        //else
                        //{
                        //    var shops = OrderShopTempController.GetByUID(obj_user.ID);
                        //    if (shops != null)
                        //    {
                        //        if (shops.Count > 0)
                        //        {
                        //            Session["orderitem"] = "all";
                        //            foreach (var shop in shops)
                        //            {
                        //                double total = 0;
                        //                double fastprice = 0;
                        //                double pricepro = Convert.ToDouble(shop.PriceVND);
                        //                double servicefee = 0;
                        //                var adminfeebuypro = FeeBuyProController.GetAll();
                        //                if (adminfeebuypro.Count > 0)
                        //                {
                        //                    foreach (var item in adminfeebuypro)
                        //                    {
                        //                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                        //                        {
                        //                            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                        //                        }
                        //                    }
                        //                }

                        //                double feebpnotdc = pricepro * servicefee;
                        //                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        //                double feebp = 0;
                        //                feebp = feebpnotdc - subfeebp;
                        //                double feecnship = 0;
                        //                //feecnship = 10 * current;

                        //                if (shop.IsFast == true)
                        //                {
                        //                    fastprice = (pricepro * 5 / 100);
                        //                }
                        //                //total = fastprice + pricepro + feebp + feecnship;
                        //                string ShopID = shop.ShopID;
                        //                string ShopName = shop.ShopName;
                        //                string Site = shop.Site;
                        //                bool IsForward = Convert.ToBoolean(shop.IsForward);
                        //                string IsForwardPrice = shop.IsForwardPrice;
                        //                bool IsFastDelivery = Convert.ToBoolean(shop.IsFastDelivery);
                        //                string IsFastDeliveryPrice = shop.IsFastDeliveryPrice;
                        //                bool IsCheckProduct = Convert.ToBoolean(shop.IsCheckProduct);
                        //                string IsCheckProductPrice = shop.IsCheckProductPrice;
                        //                bool IsPacked = Convert.ToBoolean(shop.IsPacked);
                        //                string IsPackedPrice = shop.IsPackedPrice;
                        //                bool IsFast = Convert.ToBoolean(shop.IsFast);
                        //                string IsFastPrice = fastprice.ToString();
                        //                double pricecynallproduct = 0;
                        //                total = fastprice + pricepro + feebp + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                        //                //Lấy ra từng ordertemp trong shop
                        //                var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(shop.ID);
                        //                if (proOrdertemp != null)
                        //                {
                        //                    if (proOrdertemp.Count > 0)
                        //                    {
                        //                        foreach (var item in proOrdertemp)
                        //                        {
                        //                            int quantity = Convert.ToInt32(item.quantity);
                        //                            double originprice = Convert.ToDouble(item.price_origin);
                        //                            double promotionprice = Convert.ToDouble(item.price_promotion);

                        //                            double u_pricecbuy = 0;
                        //                            double u_pricevn = 0;
                        //                            double e_pricebuy = 0;
                        //                            double e_pricevn = 0;
                        //                            if (promotionprice < originprice)
                        //                            {
                        //                                u_pricecbuy = promotionprice;
                        //                                u_pricevn = promotionprice * current;
                        //                            }
                        //                            else
                        //                            {
                        //                                u_pricecbuy = originprice;
                        //                                u_pricevn = originprice * current;
                        //                            }

                        //                            e_pricebuy = u_pricecbuy * quantity;
                        //                            e_pricevn = u_pricevn * quantity;

                        //                            pricecynallproduct += e_pricebuy;
                        //                        }
                        //                    }
                        //                }
                        //                string PriceVND = shop.PriceVND;
                        //                string PriceCNY = pricecynallproduct.ToString();
                        //                //string FeeShipCN = (10 * current).ToString();
                        //                string FeeShipCN = feecnship.ToString();
                        //                string FeeBuyPro = feebp.ToString();
                        //                string FeeWeight = shop.FeeWeight;
                        //                string Note = shop.Note;
                        //                string FullName = txt_DFullname.Text.Trim();
                        //                string Address = txt_DAddress.Text.Trim();
                        //                string Email = txt_DEmail.Text.Trim();
                        //                string Phone = txt_DPhone.Text.Trim();
                        //                int Status = 0;
                        //                string Deposit = "0";
                        //                string CurrentCNYVN = current.ToString();
                        //                string TotalPriceVND = total.ToString();
                        //                string AmountDeposit = (total * LessDeposit / 100).ToString();
                        //                DateTime CreatedDate = DateTime.Now;
                        //                string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice, IsCheckProduct, IsCheckProductPrice,
                        //                    IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro, FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN,
                        //                    TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit);
                        //                int idkq = Convert.ToInt32(kq);
                        //                if (idkq > 0)
                        //                {
                        //                    foreach (var item in proOrdertemp)
                        //                    {
                        //                        int quantity = Convert.ToInt32(item.quantity);
                        //                        double originprice = Convert.ToDouble(item.price_origin);
                        //                        double promotionprice = Convert.ToDouble(item.price_promotion);
                        //                        double u_pricecbuy = 0;
                        //                        double u_pricevn = 0;
                        //                        double e_pricebuy = 0;
                        //                        double e_pricevn = 0;
                        //                        if (promotionprice < originprice)
                        //                        {
                        //                            u_pricecbuy = promotionprice;
                        //                            u_pricevn = promotionprice * current;
                        //                        }
                        //                        else
                        //                        {
                        //                            u_pricecbuy = originprice;
                        //                            u_pricevn = originprice * current;
                        //                        }

                        //                        e_pricebuy = u_pricecbuy * quantity;
                        //                        e_pricevn = u_pricevn * quantity;

                        //                        pricecynallproduct += e_pricebuy;

                        //                        string image = item.image_origin;
                        //                        if (image.Contains("%2F"))
                        //                        {
                        //                            image = image.Replace("%2F", "/");
                        //                        }
                        //                        if (image.Contains("%3A"))
                        //                        {
                        //                            image = image.Replace("%3A", ":");
                        //                        }
                        //                        string ret = OrderController.Insert(UID, item.title_origin, item.title_translated, item.price_origin, item.price_promotion, item.property_translated,
                        //                        item.property, item.data_value, image, image, item.shop_id, item.shop_name, item.seller_id, item.wangwang, item.quantity,
                        //                        item.stock, item.location_sale, item.site, item.comment, item.item_id, item.link_origin, item.outer_id, item.error, item.weight, item.step, item.stepprice, item.brand,
                        //                        item.category_name, item.category_id, item.tool, item.version, Convert.ToBoolean(item.is_translate), Convert.ToBoolean(item.IsForward), "0",
                        //                        Convert.ToBoolean(item.IsFastDelivery), "0", Convert.ToBoolean(item.IsCheckProduct), "0", Convert.ToBoolean(item.IsPacked), "0", Convert.ToBoolean(item.IsFast),
                        //                        fastprice.ToString(), pricepro.ToString(), PriceCNY, item.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                        //                        txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), idkq, DateTime.Now, UID);

                        //                        if (item.price_promotion.ToFloat(0) > 0)
                        //                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_promotion);
                        //                        else
                        //                            OrderController.UpdatePricePriceReal(ret.ToInt(0), item.price_origin, item.price_origin);
                        //                    }
                        //                    MainOrderController.UpdateReceivePlace(idkq, UID, ddlReceivePlace.SelectedValue);
                        //                    var admins = AccountController.GetAllByRoleID(0);
                        //                    if (admins.Count > 0)
                        //                    {
                        //                        foreach (var admin in admins)
                        //                        {
                        //                            NotificationController.Inser(UID, username, admin.ID,
                        //                                                               admin.Username, idkq,
                        //                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                        //                                                               CreatedDate, username);
                        //                        }
                        //                    }

                        //                    var managers = AccountController.GetAllByRoleID(2);
                        //                    if (managers.Count > 0)
                        //                    {
                        //                        foreach (var manager in managers)
                        //                        {
                        //                            NotificationController.Inser(UID, username, manager.ID,
                        //                                                               manager.Username, 0,
                        //                                                               "Có đơn hàng mới ID là: " + idkq, 0,
                        //                                                               CreatedDate, username);
                        //                        }
                        //                    }
                        //                }
                        //                double salepercent = 0;
                        //                double salepercentaf3m = 0;
                        //                double dathangpercent = 0;
                        //                var config = ConfigurationController.GetByTop1();
                        //                if (config != null)
                        //                {
                        //                    salepercent = Convert.ToDouble(config.SalePercent);
                        //                    salepercentaf3m = Convert.ToDouble(config.SalePercentAfter3Month);
                        //                    dathangpercent = Convert.ToDouble(config.DathangPercent);
                        //                }
                        //                string salerName = "";
                        //                string dathangName = "";

                        //                if (salerID > 0)
                        //                {
                        //                    var sale = AccountController.GetByID(salerID);
                        //                    if (sale != null)
                        //                    {
                        //                        salerName = sale.Username;
                        //                        var createdDate = Convert.ToDateTime(sale.CreatedDate);
                        //                        int d = CreatedDate.Subtract(createdDate).Days;
                        //                        if (d > 90)
                        //                        {
                        //                            double per = feebp * salepercentaf3m / 100;
                        //                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                        //                            CreatedDate, CreatedDate, username);
                        //                        }
                        //                        else
                        //                        {
                        //                            double per = feebp * salepercent / 100;
                        //                            StaffIncomeController.Insert(idkq, "0", salepercent.ToString(), salerID, salerName, 6, 1, per.ToString(), false,
                        //                            CreatedDate, CreatedDate, username);
                        //                        }
                        //                    }
                        //                }
                        //                if (dathangID > 0)
                        //                {
                        //                    var dathang = AccountController.GetByID(dathangID);
                        //                    if (dathang != null)
                        //                    {
                        //                        dathangName = dathang.Username;
                        //                        StaffIncomeController.Insert(idkq, "0", dathangpercent.ToString(), dathangID, dathangName, 3, 1, "0", false,
                        //                            CreatedDate, CreatedDate, username);
                        //                    }
                        //                }
                        //                //Xóa Shop temp và order temp
                        //                OrderShopTempController.Delete(shop.ID);
                        //            }

                        //            Response.Redirect("/danh-sach-don-hang");
                        //        }
                        //    }

                        //}
                        #endregion
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                else
                {
                    Response.Redirect("/gio-hang");
                }
            }
            else
            {
                lblCheckckd.Visible = true;
            }

        }
    }
}