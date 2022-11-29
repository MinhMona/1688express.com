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

namespace NHST
{
    public partial class Thanh_toan : System.Web.UI.Page
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
            }
        }
        public void UpdateCheck(string rq)
        {
            double current = Convert.ToDouble(ConfigurationController.GetByTop1().Currency);
            //Load User Info
            string username = HttpContext.Current.Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                if (rq == "all")
                {
                    var shops = OrderShopTempController.GetByUID(obj_user.ID);
                    if (shops != null)
                    {
                        if (shops.Count > 0)
                        {
                            foreach (var shop in shops)
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
                                            if (item.price_origin.ToFloat() < 10)
                                                total = total + (1500 * item.quantity.ToInt(1));
                                            else if (item.price_origin.ToFloat() >= 10)
                                                total = total + (5000 * item.quantity.ToInt(1));
                                        }
                                    }
                                    else if (counpros > 2 && counpros <= 10)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            if (item.price_origin.ToFloat() < 10)
                                                total = total + (1000 * item.quantity.ToInt(1));
                                            else if (item.price_origin.ToFloat() >= 10)
                                                total = total + (3500 * item.quantity.ToInt(1));
                                        }
                                    }
                                    else if (counpros > 10 && counpros <= 100)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            if (item.price_origin.ToFloat() < 10)
                                                total = total + (700 * item.quantity.ToInt(1));
                                            else if (item.price_origin.ToFloat() >= 10)
                                                total = total + (2000 * item.quantity.ToInt(1));
                                        }
                                    }
                                    else if (counpros > 100 && counpros <= 500)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            if (item.price_origin.ToFloat() < 10)
                                                total = total + (700 * item.quantity.ToInt(1));
                                            else if (item.price_origin.ToFloat() >= 10)
                                                total = total + (1500 * item.quantity.ToInt(1));
                                        }
                                    }
                                    else if (counpros > 500)
                                    {
                                        foreach (var item in listpro)
                                        {
                                            if (item.price_origin.ToFloat() < 10)
                                                total = total + (700 * item.quantity.ToInt(1));
                                            else if (item.price_origin.ToFloat() >= 10)
                                                total = total + (1000 * item.quantity.ToInt(1));
                                        }
                                    }
                                    OrderShopTempController.UpdateCheckProductPrice(shop.ID, total.ToString());
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
                                    if (item.price_origin.ToFloat() < 10)
                                        total = total + (1500 * item.quantity.ToInt(1));
                                    else if (item.price_origin.ToFloat() >= 10)
                                        total = total + (5000 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 2 && counpros <= 10)
                            {
                                foreach (var item in listpro)
                                {
                                    if (item.price_origin.ToFloat() < 10)
                                        total = total + (1000 * item.quantity.ToInt(1));
                                    else if (item.price_origin.ToFloat() >= 10)
                                        total = total + (3500 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 10 && counpros <= 100)
                            {
                                foreach (var item in listpro)
                                {
                                    if (item.price_origin.ToFloat() < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (item.price_origin.ToFloat() >= 10)
                                        total = total + (2000 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 100 && counpros <= 500)
                            {
                                foreach (var item in listpro)
                                {
                                    if (item.price_origin.ToFloat() < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (item.price_origin.ToFloat() >= 10)
                                        total = total + (1500 * item.quantity.ToInt(1));
                                }
                            }
                            else if (counpros > 500)
                            {
                                foreach (var item in listpro)
                                {
                                    if (item.price_origin.ToFloat() < 10)
                                        total = total + (700 * item.quantity.ToInt(1));
                                    else if (item.price_origin.ToFloat() >= 10)
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
                var obj_user = AccountController.GetByUsername(username);
                if (obj_user != null)
                {
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
                        txt_Phone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        txt_DPhone.Text = ui.MobilePhonePrefix + ui.MobilePhone;
                        UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);

                    }
                    if (rq != "all")
                    {
                        int orderID = Convert.ToInt32(rq);
                        var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, orderID);
                        double total = 0;
                        if (shop != null)
                        {
                            Session["orderitem"] = orderID;
                            double fastprice = 0;
                            double pricepro = Convert.ToDouble(shop.PriceVND);
                            double servicefee = 0;

                            var adminfeebuypro = FeeBuyProController.GetAll();
                            if (adminfeebuypro.Count > 0)
                            {
                                foreach (var item in adminfeebuypro)
                                {
                                    if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                    {
                                        servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                    }
                                }
                            }

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
                            double feebp = feebpnotdc - subfeebp;

                            if (shop.IsFast == true)
                            {
                                fastprice = (pricepro * 5 / 100);
                            }
                            total = fastprice + pricepro;
                            ltr_pro.Text += "<div class=\"order-detail\">";
                            ltr_pro.Text += "   <table>";
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
                            double FeeBuyPro = feebp;
                            double FeeCheck = shop.IsCheckProductPrice.ToFloat(0);
                            total = total + FeeCNShip + FeeBuyPro + FeeCheck;
                            ltr_pro.Text += "       <tr>";
                            ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                            ltr_pro.Text += "           <td></td>";
                            //ltr_pro.Text += "           <td style=\"width:20%;\"><strong>" + string.Format("{0:N0}", FeeCNShip) + " vnđ</strong></td>";
                            ltr_pro.Text += "           <td style=\"width:20%;\"><strong>Chờ cập nhật</strong></td>";
                            ltr_pro.Text += "       </tr>";
                            ltr_pro.Text += "       <tr>";
                            if (UL_CKFeeBuyPro > 0)
                                ltr_pro.Text += "           <td>Phí mua hàng (Đã CK: " + UL_CKFeeBuyPro + "%)</td>";
                            else
                                ltr_pro.Text += "           <td>Phí mua hàng</td>";
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
                    else
                    {
                        //var orders = OrderTempController.GetAllByUID(obj_user.ID);
                        var shops = OrderShopTempController.GetByUID(obj_user.ID);
                        if (shops != null)
                        {
                            if (shops.Count > 0)
                            {
                                Session["orderitem"] = "all";
                                double totalfinal = 0;

                                foreach (var shop in shops)
                                {
                                    double fastprice = 0;
                                    double pricepro = Convert.ToDouble(shop.PriceVND);
                                    double servicefee = 0;

                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                            {
                                                servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                            }
                                        }
                                    }
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
                                    double feebp = feebpnotdc - subfeebp;

                                    if (shop.IsFast == true)
                                    {
                                        fastprice = (pricepro * 5 / 100);
                                    }
                                    double total = fastprice + pricepro;
                                    //double FeeCNShip = 10 * current;
                                    double FeeCNShip = 0;
                                    double FeeBuyPro = feebp;
                                    double FeeCheck = shop.IsCheckProductPrice.ToFloat(0);
                                    total = total + FeeCNShip + FeeBuyPro + FeeCheck;
                                    totalfinal += total;
                                    ltr_pro.Text += "<div class=\"order-detail\">";
                                    ltr_pro.Text += "   <table>";
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
                                        ltr_pro.Text += "           <td>Phí mua hàng (CK: " + UL_CKFeeBuyPro + "%)</td>";
                                    else
                                        ltr_pro.Text += "           <td>Phí mua hàng</td>";
                                    ltr_pro.Text += "           <td></td>";
                                    ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", FeeBuyPro) + " vnđ</strong></td>";
                                    ltr_pro.Text += "       </tr>";
                                    ltr_pro.Text += "       <tr>";
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
                                    ltr_pro.Text += "   </table>";
                                    ltr_pro.Text += "</div>";


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
                        #region order_old
                        //if (orders.Count > 0)
                        //{
                        //    Session["orderitem"] = "all";
                        //    double totalfinal = 0;
                        //    foreach (var order in orders)
                        //    {
                        //        double pricepro = Convert.ToInt32(order.quantity) * Convert.ToDouble(order.price_origin) * current;
                        //        double fastprice = 0;
                        //        if (order.IsFast == true)
                        //        {
                        //            fastprice = (pricepro / 2);
                        //        }
                        //        double total = fastprice + pricepro;
                        //        string image = order.image_origin;
                        //        if (image.Contains("%2F"))
                        //        {
                        //            image = image.Replace("%2F", "/");
                        //        }
                        //        if (image.Contains("%3A"))
                        //        {
                        //            image = image.Replace("%3A", ":");
                        //        }
                        //        totalfinal += total;
                        //        ltr_pro.Text += "<div class=\"order-detail\">";
                        //        ltr_pro.Text += "   <table>";
                        //        ltr_pro.Text += "       <tr class=\"borderbtm\">";
                        //        ltr_pro.Text += "           <td colspan=\"2\">";
                        //        ltr_pro.Text += "               <div class=\"thumb-product\">";
                        //        ltr_pro.Text += "                   <div class=\"pd-img\">";
                        //        ltr_pro.Text += "                       <img src=\"" + image + "\" alt=\"\"><span class=\"badge\">" + order.quantity + "</span>";
                        //        ltr_pro.Text += "                   </div>";
                        //        ltr_pro.Text += "                   <div class=\"info\"><a href=\"" + order.link_origin + "\">" + order.title_origin + "</a></div>";
                        //        ltr_pro.Text += "               </div>";
                        //        ltr_pro.Text += "           </td>";
                        //        ltr_pro.Text += "           <td>";
                        //        ltr_pro.Text += "               <strong>" + string.Format("{0:N0}", pricepro) + "vnđ</strong>";
                        //        ltr_pro.Text += "           </td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí ship Trung Quốc</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí mua hàng</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí cân nặng</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí kiểm đếm</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí đóng gỗ</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td>Phí ship giao hàng tận nhà</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>Updating...</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr class=\"borderbtm\">";
                        //        ltr_pro.Text += "           <td>Phí đơn hàng hỏa tốc</td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong>" + string.Format("{0:N0}", fastprice) + "vnđ</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "       <tr>";
                        //        ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng tiền</strong></td>";
                        //        ltr_pro.Text += "           <td></td>";
                        //        ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", total) + "vnđ</strong></td>";
                        //        ltr_pro.Text += "       </tr>";
                        //        ltr_pro.Text += "   </table>";
                        //        ltr_pro.Text += "</div>";
                        //    }
                        //    ltr_pro.Text += "<div class=\"order-detail\">";
                        //    ltr_pro.Text += "   <table>";
                        //    ltr_pro.Text += "       <tr>";
                        //    ltr_pro.Text += "           <td style=\"color: #959595; text-transform: uppercase\"><strong>Tổng hóa đơn</strong></td>";
                        //    ltr_pro.Text += "           <td></td>";
                        //    ltr_pro.Text += "           <td><strong class=\"hl-txt\">" + string.Format("{0:N0}", totalfinal) + "vnđ</strong></td>";
                        //    ltr_pro.Text += "       </tr>";
                        //    ltr_pro.Text += "   </table>";
                        //    ltr_pro.Text += "</div>";
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
                        int salerID = obj_user.SaleID.ToString().ToInt(0);
                        int dathangID = obj_user.DathangID.ToString().ToInt(0);
                        int UID = obj_user.ID;
                        //double percent_User = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LevelPercent);
                        double UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeBuyPro);
                        double UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).FeeWeight);
                        double LessDeposit = Convert.ToDouble(UserLevelController.GetByID(obj_user.LevelID.ToString().ToInt()).LessDeposit);
                        if (rq != "all")
                        {
                            int IDshop = Convert.ToInt32(rq);
                            var shop = OrderShopTempController.GetByUIDAndID(obj_user.ID, IDshop);

                            double total = 0;
                            if (shop != null)
                            {
                                double fastprice = 0;
                                double pricepro = Convert.ToDouble(shop.PriceVND);
                                double servicefee = 0;
                                var adminfeebuypro = FeeBuyProController.GetAll();
                                if (adminfeebuypro.Count > 0)
                                {
                                    foreach (var item in adminfeebuypro)
                                    {
                                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        {
                                            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                        }
                                    }
                                }
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
                                double feebp = 0;
                                feebp = feebpnotdc - subfeebp;
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
                                total = fastprice + pricepro + feebp + feecnship + shop.IsCheckProductPrice.ToFloat(0);
                                //Lấy ra từng ordertemp trong shop
                                var proOrdertemp = OrderTempController.GetAllByOrderShopTempID(IDshop);
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
                                string PriceVND = shop.PriceVND;
                                string PriceCNY = pricecynallproduct.ToString();
                                string FeeShipCN = feecnship.ToString();
                                string FeeBuyPro = feebp.ToString();
                                string FeeWeight = shop.FeeWeight;
                                string Note = shop.Note;
                                string FullName = txt_DFullname.Text.Trim();
                                string Address = txt_DAddress.Text.Trim();
                                string Email = txt_DEmail.Text.Trim();
                                string Phone = txt_DPhone.Text.Trim();
                                int Status = 0;
                                string Deposit = "0";
                                string CurrentCNYVN = current.ToString();
                                string TotalPriceVND = total.ToString();
                                string AmountDeposit = (total * LessDeposit / 100).ToString();
                                DateTime CreatedDate = DateTime.Now;
                                string kq = MainOrderController.Insert(UID, ShopID, ShopName, Site, IsForward, IsForwardPrice, IsFastDelivery, IsFastDeliveryPrice,
                                    IsCheckProduct, IsCheckProductPrice, IsPacked, IsPackedPrice, IsFast, IsFastPrice, PriceVND, PriceCNY, FeeShipCN, FeeBuyPro,
                                    FeeWeight, Note, FullName, Address, Email, Phone, Status, Deposit, CurrentCNYVN, TotalPriceVND, salerID, dathangID, CreatedDate, UID, AmountDeposit, 1);
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
                                    //MainOrderController.UpdateReceivePlace(idkq, UID, ddlReceivePlace.SelectedValue);
                                }
                                //Xóa Shop temp và order temp
                                string kqdel = OrderShopTempController.Delete(shop.ID);
                                if (kqdel == "1")
                                    Response.Redirect("/danh-sach-don-hang");
                            }
                        }
                        else
                        {
                            var shops = OrderShopTempController.GetByUID(obj_user.ID);
                            if (shops != null)
                            {
                                if (shops.Count > 0)
                                {
                                    Session["orderitem"] = "all";
                                    foreach (var shop in shops)
                                    {
                                        double total = 0;
                                        double fastprice = 0;
                                        double pricepro = Convert.ToDouble(shop.PriceVND);
                                        double servicefee = 0;
                                        var adminfeebuypro = FeeBuyProController.GetAll();
                                        if (adminfeebuypro.Count > 0)
                                        {
                                            foreach (var item in adminfeebuypro)
                                            {
                                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                                {
                                                    servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                                }
                                            }
                                        }
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
                                        double feebp = 0;
                                        feebp = feebpnotdc - subfeebp;
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
                                        string PriceVND = shop.PriceVND;
                                        string PriceCNY = pricecynallproduct.ToString();
                                        //string FeeShipCN = (10 * current).ToString();
                                        string FeeShipCN = feecnship.ToString();
                                        string FeeBuyPro = feebp.ToString();
                                        string FeeWeight = shop.FeeWeight;
                                        string Note = shop.Note;
                                        string FullName = txt_DFullname.Text.Trim();
                                        string Address = txt_DAddress.Text.Trim();
                                        string Email = txt_DEmail.Text.Trim();
                                        string Phone = txt_DPhone.Text.Trim();
                                        int Status = 0;
                                        string Deposit = "0";
                                        string CurrentCNYVN = current.ToString();
                                        string TotalPriceVND = total.ToString();
                                        string AmountDeposit = (total * LessDeposit / 100).ToString();
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
                                            //MainOrderController.UpdateReceivePlace(idkq, UID, ddlReceivePlace.SelectedValue);
                                        }
                                        //Xóa Shop temp và order temp
                                        OrderShopTempController.Delete(shop.ID);
                                    }

                                    Response.Redirect("/danh-sach-don-hang");
                                }
                            }
                            //var orders = OrderTempController.GetAllByUID(obj_user.ID);
                            //if (orders.Count > 0)
                            //{
                            //    Session["orderitem"] = "all";
                            //    foreach (var order in orders)
                            //    {
                            //        double pricepro = Convert.ToInt32(order.quantity) * Convert.ToDouble(order.price_origin) * current;
                            //        double priceprocyn = Convert.ToInt32(order.quantity) * Convert.ToDouble(order.price_origin);

                            //        double fastprice = 0;
                            //        if (order.IsFast == true)
                            //        {
                            //            fastprice = (pricepro / 2);
                            //        }
                            //        double total = fastprice + pricepro;
                            //        string image = order.image_origin;
                            //        if (image.Contains("%2F"))
                            //        {
                            //            image = image.Replace("%2F", "/");
                            //        }
                            //        if (image.Contains("%3A"))
                            //        {
                            //            image = image.Replace("%3A", ":");
                            //        }
                            //        //string kq = OrderController.Insert(UID, order.title_origin, order.title_translated, order.price_origin, order.price_promotion, order.property_translated,
                            //        //order.property, order.data_value, image, image, order.shop_id, order.shop_name, order.seller_id, order.wangwang, order.quantity,
                            //        //order.stock, order.location_sale, order.site, order.comment, order.item_id, order.link_origin, order.outer_id, order.error, order.weight, order.step, order.brand,
                            //        //order.category_name, order.category_id, order.tool, order.version, Convert.ToBoolean(order.is_translate), Convert.ToBoolean(order.IsForward), "0",
                            //        //Convert.ToBoolean(order.IsFastDelivery), "0", Convert.ToBoolean(order.IsCheckProduct), "0", Convert.ToBoolean(order.IsPacked), "0", Convert.ToBoolean(order.IsFast),
                            //        //fastprice.ToString(), pricepro.ToString(), priceprocyn.ToString(), order.Note, txt_DFullname.Text.Trim(), txt_DAddress.Text.Trim(), txt_DEmail.Text.Trim(),
                            //        //txt_DPhone.Text.Trim(), 0, "0", current.ToString(), total.ToString(), DateTime.Now, UID);
                            //        //if (Convert.ToInt32(kq) > 0)
                            //        //{
                            //        //    //Xóa order temp ID
                            //        //    OrderTempController.Delete(order.ID);
                            //        //}
                            //    }
                            //    Session.Remove("orderitem");
                            //    //Session["ordersuccess"] = "ok";
                            //    Response.Redirect("/danh-sach-don-hang");
                            //}
                        }
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