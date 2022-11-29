using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class ProductEdit : System.Web.UI.Page
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
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0 && ac.RoleID != 3)
                    //    Response.Redirect("/trang-chu");
                    Loaddata();
                }

            }

        }
        public void Loaddata()
        {
            int MainOrderID = 0;
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        hdfcurrent.Value = mainorder.CurrentCNYVN.ToString();
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 3 && ac.RoleID != 2)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }
                    else
                    {
                        if (ac.RoleID == 3)
                        {
                            if (mainorder.Status >= 5)
                                btncreateuser.Visible = false;
                            pProductPriceOriginal.Enabled = false;
                            pRealPrice.Enabled = true;
                        }
                    }
                    lblBrandname.Text = o.brand;
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);
                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

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
                    ViewState["productprice"] = price;
                    pProductPriceOriginal.Value = price;
                    if (!string.IsNullOrEmpty(o.quantity))
                        pQuanity.Value = Convert.ToDouble(o.quantity);
                    else pQuanity.Value = 0;
                    pRealPrice.Value = Convert.ToDouble(o.RealPrice);
                    txtproducbrand.Text = o.brand;
                    ltrback.Text += "<a href=\"/manager/OrderDetail.aspx?id=" + o.MainOrderID + "\" class=\"btn primary-btn\">Trở về</a>";
                    string productstatus = "";
                    if (!string.IsNullOrEmpty(o.ProductStatus.ToString()))
                        ddlStatus.SelectedValue = o.ProductStatus.ToString();
                    else
                        ddlStatus.SelectedValue = "1";

                }
            }

        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);

            //Update lại giá sản phẩm
            var id = Request.QueryString["id"].ToInt(0);
            int MainOrderID = 0;
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    MainOrderID = Convert.ToInt32(o.MainOrderID);

                    double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);

                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

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

                    if (price.ToString() != pProductPriceOriginal.Value.ToString())
                    {
                        HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                        + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                    }
                    if (o.quantity != pQuanity.Value.ToString())
                    {
                        HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                        " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                        + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                    }
                    OrderController.UpdateQuantity(id, pQuanity.Value.ToString());
                    OrderController.UpdateProductStatus(id, status);
                    OrderController.UpdatePricePriceReal(id, pProductPriceOriginal.Value.ToString(), pRealPrice.Value.ToString());
                    OrderController.UpdatePricePromotion(id, pProductPriceOriginal.Value.ToString());
                    OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());
                }
            }

            //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí
            var listorder = OrderController.GetByMainOrderID(MainOrderID);
            var mainorder = MainOrderController.GetAllByID(MainOrderID);

            if (mainorder != null)
            {
                double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                if (listorder != null)
                {
                    if (listorder.Count > 0)
                    {
                        double pricevnd = 0;
                        double pricecyn = 0;
                        foreach (var item in listorder)
                        {
                            double originprice = Convert.ToDouble(item.price_origin);
                            double promotionprice = Convert.ToDouble(item.price_promotion);
                            double oprice = 0;
                            if (promotionprice > 0)
                            {
                                if (promotionprice < originprice)
                                {
                                    pricecyn += promotionprice;
                                    oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                }
                                else
                                {
                                    pricecyn += originprice;
                                    oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                }
                            }
                            else
                            {
                                pricecyn += originprice;
                                oprice = originprice * Convert.ToDouble(item.quantity) * current;
                            }
                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * Convert.ToDouble(item.CurrentCNYVN) + Convert.ToDouble(item.PriceChange);

                            //pricecyn += item.price_origin.ToFloat();
                            //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * current;
                            pricevnd += oprice;
                        }
                        MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                        MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());
                        double Deposit = Convert.ToDouble(mainorder.Deposit);
                        double FeeShipCN = Convert.ToDouble(mainorder.FeeShipCN);
                        double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                        double FeeWeight = Convert.ToDouble(mainorder.FeeWeight);
                        //double FeeShipCNToVN = Convert.ToDouble(mainorder.FeeShipCNToVN);

                        double IsCheckProductPrice = 0;
                        if (mainorder.IsCheckProduct == true)
                        {
                            double total = 0;
                            double counpros = 0;
                            if (listorder.Count > 0)
                            {
                                foreach (var item in listorder)
                                {
                                    counpros += item.quantity.ToInt(1);
                                }
                            }
                            //var count = listpro.Count;
                            if (counpros >= 1 && counpros <= 2)
                            {
                                total = total + (5000 * counpros);                                
                            }
                            else if (counpros > 2 && counpros <= 20)
                            {
                                total = total + (3500 * counpros);                                
                            }
                            else if (counpros > 20 && counpros <= 100)
                            {
                                total = total + (2000 * counpros);
                            }
                            else if (counpros > 100)
                            {
                                total = total + (1500 * counpros);
                            }
                            //else if (counpros > 500)
                            //{
                            //    foreach (var item in listorder)
                            //    {
                            //        if (Convert.ToDouble(item.price_origin) < 10)
                            //            total = total + (700 * item.quantity.ToInt(1));
                            //        else if (Convert.ToDouble(item.price_origin) >= 10)
                            //            total = total + (1000 * item.quantity.ToInt(1));
                            //    }
                            //}
                            IsCheckProductPrice = total;
                        }
                        else
                            IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);

                        double IsPackedPrice = 0;
                        IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);

                        double IsFastDeliveryPrice = 0;
                        IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);

                        double TotalPriceVND = FeeShipCN + FeeBuyPro
                                                + FeeWeight + IsCheckProductPrice
                                                + IsPackedPrice + IsFastDeliveryPrice
                                                + Convert.ToDouble(mainorder.IsFastPrice) + pricevnd;
                        double newdeposit = 0;
                        #region phần chỉnh sửa giá
                        double totalo = 0;
                        var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                        double UL_CKFeeBuyPro = 0;
                        double UL_CKFeeWeight = 0;
                        double LessDeposito = 0;
                        if (ui != null)
                        {
                            UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                            UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                            LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                        }
                        double fastprice = 0;
                        double pricepro = pricevnd;
                        double servicefee = 0;
                        var adminfeebuypro = FeeBuyProController.GetAll();
                        if (adminfeebuypro.Count > 0)
                        {
                            foreach (var item in adminfeebuypro)
                            {
                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                {
                                    servicefee = Convert.ToDouble(item.FeePercent.ToString()) / 100;
                                }
                            }
                        }

                        double feebpnotdc = pricepro * servicefee;
                        double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                        double feebp = feebpnotdc - subfeebp;

                        if (mainorder.IsFast == true)
                        {
                            fastprice = (pricepro * 5 / 100);
                        }
                        totalo = fastprice + pricepro;
                        double FeeCNShip = FeeShipCN;
                        double FeeBuyPros = feebp;
                        double FeeCheck = IsCheckProductPrice;
                        totalo = totalo + FeeCNShip + FeeBuyPros + FeeCheck;
                        double AmountDeposit = Math.Floor((totalo * LessDeposito) / 100);


                        //cập nhật lại giá phải deposit của đơn hàng
                        MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString());

                        //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                        newdeposit = AmountDeposit;

                        //nếu đã đặt cọc rồi thì trả phí lại cho người ta
                        if (Deposit > 0)
                        {
                            if (Deposit > newdeposit)
                            {
                                double drefund = Deposit - newdeposit;
                                double userwallet = 0;
                                if (ui.Wallet.ToString() != null)
                                    userwallet = Convert.ToDouble(ui.Wallet.ToString());

                                double wallet = userwallet + drefund;
                                AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                                PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                                // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                                //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
                                if (status == 2)
                                    HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                                else
                                    HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);

                                NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID),
                                    AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, id, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + id + " của bạn. CLick vào để xem", 0,
                                    1, currentDate, obj_user.Username);
                                var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                hubContext.Clients.All.addNewMessageToPage("", "");
                                try
                                {
                                    PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                        "Thông báo tại 1688 Express", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                }
                                catch
                                {

                                }
                                //newdeposit = Deposit;
                                MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                            }
                            else
                            {
                                if (Deposit < newdeposit)
                                {
                                    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                }
                                else if (Deposit == newdeposit)
                                {
                                    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                }
                                newdeposit = Deposit;
                            }
                        }
                        else
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                            newdeposit = 0;
                        }
                        if (totalo == 0)
                        {
                            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                        }
                        //if (status == 2)
                        //{

                        //}
                        //else
                        //{
                        //    newdeposit = Deposit;
                        //}
                        #endregion
                        MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                            FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());


                    }
                }
            }
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/manager/OrderDetail.aspx?id=" + MainOrderID, Page);
        }
    }
}