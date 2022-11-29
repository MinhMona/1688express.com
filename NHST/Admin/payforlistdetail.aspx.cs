using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;
using NHST.Models;
using NHST.Controllers;
using System.Text;

namespace NHST.admin
{
    public partial class payforlistdetail : System.Web.UI.Page
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
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                }

                LoadData();

            }
        }

        public void LoadData()
        {
            var id = Request.QueryString["ID"].ToInt(0);
            if (id > 0)
            {
                var re = PayhelpController.GetByID(id);
                if (re != null)
                {
                    #region cách cũ
                    //ViewState["ID"] = id;
                    //lblUsername.Text = re.Username;
                    //pPriceCYN.Value = Convert.ToDouble(re.TotalPrice);
                    //pPriceVND.Value = Convert.ToDouble(re.TotalPriceVND);
                    //txtSummary.Text = re.Note;
                    //txtPhone.Text = re.Phone;
                    //if (re.IsNotComplete != null)
                    //    chkIsNotComplete.Checked = Convert.ToBoolean(re.IsNotComplete);
                    //ddlStatus.SelectedValue = re.Status.ToString();
                    //pCurrency.Value = Convert.ToDouble(re.Currency);
                    //var pd = PayhelpDetailController.GetByPayhelpID(id);
                    //StringBuilder html = new StringBuilder();
                    //if (pd.Count > 0)
                    //{
                    //    foreach (var item in pd)
                    //    {
                    //        html.Append("<div class=\"form-group itemyeuau\" data-id=\"" + item.ID + "\">");
                    //        html.Append("<label for=\"inputEmail3\" class=\"col-sm-2 control-label\">Giá tiền:</label>");
                    //        html.Append("<div class=\"col-sm-4\">");
                    //        html.Append("<input class=\"txtDesc2 form-control\" value=\"" + item.Desc2 + "\"/>");
                    //        html.Append("</div>");
                    //        html.Append("<label for=\"inputEmail3\" class=\"col-sm-2 control-label\">Nội dung:</label>");
                    //        html.Append("<div class=\"col-sm-4\">");
                    //        html.Append("<textarea class=\"txtDesc1 form-control\">" + item.Desc1 + "</textarea>");
                    //        html.Append("</div>");
                    //        html.Append("</div>");
                    //    }

                    //}
                    //ltrList.Text = html.ToString();
                    //var hist = HistoryServiceController.GetAllByPostIDAndType(id, 2);
                    //if (hist.Count > 0)
                    //{
                    //    rptPayment.DataSource = hist;
                    //    rptPayment.DataBind();
                    //}
                    #endregion
                    #region cách mới
                    ViewState["ID"] = id;
                    var acc = AccountController.GetByUsername(re.Username);
                    if (acc != null)
                    {
                        lblAccountWallet.Text = string.Format("{0:N0}", acc.Wallet);
                    }
                    double currency = 0;
                    var config = ConfigurationController.GetByTop1();
                    if (config != null)
                    {
                        currency = Convert.ToDouble(config.AgentCurrency);
                        hdfCurrency.Value = currency.ToString();
                        lblCurrency.Text = string.Format("{0:N0}", currency);
                    }

                    lblUsername.Text = re.Username;
                    pPriceCYN.Value = Convert.ToDouble(re.TotalPrice);


                    if (re.Status == 4)
                    {
                        rPriceVNDPayed.Value = Convert.ToDouble(re.TotalPriceVND);
                        pPriceVND.Value = Convert.ToDouble(re.TotalPriceVND);
                    }
                    else
                    {
                        rPriceVNDPayed.Value = 0;
                        pPriceVND.Value = Convert.ToDouble(re.TotalPrice) * currency;
                    }
                    txtSummary.Text = re.Note;
                    txtStaffNote.Text = re.StaffNote;
                    ddlStatus.SelectedValue = re.Status.ToString();
                    #endregion
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int ID = ViewState["ID"].ToString().ToInt(0);
            string list = hdfList.Value;
            int status = ddlStatus.SelectedValue.ToInt(0);
            var p = PayhelpController.GetByID(ID);
            var u_loginin = AccountController.GetByUsername(username_current);

            #region cách mới
            if (u_loginin != null)
            {
                if (p != null)
                {
                    double currency = 0;
                    var confi = ConfigurationController.GetByTop1();
                    if (confi != null)
                    {
                        currency = Convert.ToDouble(confi.AgentCurrency);
                    }

                    int UID = Convert.ToInt32(p.UID);
                    var accYeucau = AccountController.GetByID(UID);
                    if (accYeucau != null)
                    {
                        double wallet = Convert.ToDouble(accYeucau.Wallet);

                        //double oldPrice = Convert.ToDouble(p.TotalPrice);
                        //double newPrice = Convert.ToDouble(pPriceCYN.Value);



                        int statusOld = Convert.ToInt32(p.Status);
                        int statusNew = ddlStatus.SelectedValue.ToInt();
                        if (statusOld == 4)
                        {
                            if (statusNew == 4)
                            {
                                //Nếu là 4 thì kiểm tra số tiền có thay đổi hay k rồi cập nhật rồi lại tiếp tục trừ
                                //if (oldPrice > newPrice)
                                //{
                                //    //Hoàn tiền lại và update lại tiền
                                //    double refund = oldPrice - newPrice;
                                //    double refundVND = refund * currency;

                                //    double newPriceVND = newPrice * currency;
                                //    wallet = wallet + refundVND;

                                //    AccountController.updateWallet(accYeucau.ID, wallet, currentDate, username_current);
                                //    HistoryPayWalletController.Insert(accYeucau.ID, accYeucau.Username, 0, refundVND,
                                //        accYeucau.Username + " đã được hoàn tiền thanh toán hộ vào tài khoản.",
                                //        wallet, 2, 10, currentDate, username_current);
                                //    NotificationController.Inser(u_loginin.ID, username_current, accYeucau.ID, accYeucau.Username, 0,
                                //                           "Bạn đã được hoàn tiền thanh toán hộ: " + refundVND + " vào tài khoản.", 0,
                                //                           7, DateTime.Now, u_loginin.Username);
                                //    PayhelpController.UpdateNew(p.ID, txtSummary.Text, newPrice.ToString(),
                                //        newPriceVND.ToString(), currency.ToString(), statusNew, txtStaffNote.Text,
                                //        currentDate, u_loginin.Username);
                                //    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                                //}
                                //else if (oldPrice < newPrice)
                                //{
                                //    //Thu thêm tiền và update lại tiền
                                //    double recharge = newPrice - oldPrice;

                                //    double rechargeVND = recharge * currency;
                                //    if (wallet >= rechargeVND)
                                //    {
                                //        wallet = wallet - rechargeVND;
                                //        AccountController.updateWallet(accYeucau.ID, wallet, currentDate, 
                                //            username_current);
                                //        HistoryPayWalletController.Insert(accYeucau.ID, accYeucau.Username, 
                                //            0, rechargeVND,
                                //            accYeucau.Username + " đã thanh toán tiền của yêu cầu thanh toán hộ.",
                                //            wallet, 1, 9, currentDate, username_current);
                                //        NotificationController.Inser(u_loginin.ID, username_current, accYeucau.ID, accYeucau.Username, 0,
                                //                               "Bạn đã thanh toán tiền của yêu cầu thanh toán hộ: " + rechargeVND + " vnđ.", 0,
                                //                               7, DateTime.Now, u_loginin.Username);
                                //        PayhelpController.UpdateNew(p.ID, txtSummary.Text, newPrice.ToString(),
                                //            rechargeVND.ToString(), currency.ToString(), statusNew, txtStaffNote.Text,
                                //            currentDate, u_loginin.Username);
                                //        PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                                //    }
                                //    else
                                //    {
                                //        PJUtils.ShowMessageBoxSwAlert("Tài khoản không đủ tiền", "e", true, Page);
                                //    }
                                //}
                                PayhelpController.UpdateNew(p.ID, txtSummary.Text, p.TotalPrice.ToString(),
                                    p.TotalPriceVND.ToString(), currency.ToString(), 4, txtStaffNote.Text,
                                    currentDate, u_loginin.Username);
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }                            
                            else
                            {
                                double oldPriceVND = Convert.ToDouble(p.TotalPriceVND);
                                //Nếu không phải 4 thì trả tiền lại cho khách và đổi trạng thái
                                wallet = wallet + oldPriceVND;
                                AccountController.updateWallet(accYeucau.ID, wallet, currentDate,
                                    username_current);
                                HistoryPayWalletController.Insert(accYeucau.ID, accYeucau.Username, 0, oldPriceVND,
                                    accYeucau.Username + " đã được hoàn tiền thanh toán hộ vào tài khoản.",
                                    wallet, 2, 10, currentDate, username_current);
                                NotificationController.Inser(u_loginin.ID, username_current, accYeucau.ID, accYeucau.Username, 0,
                                                       "Bạn đã được hoàn tiền thanh toán hộ: " + oldPriceVND + " vào tài khoản.", 0,
                                                       7, DateTime.Now, u_loginin.Username);
                                PayhelpController.UpdateNew(p.ID, txtSummary.Text, p.TotalPrice.ToString(),
                                    "0", currency.ToString(), statusNew, txtStaffNote.Text,
                                    currentDate, u_loginin.Username);
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }
                        }
                        else
                        {
                            if (statusNew == 4)
                            {
                                //Thu tiền và update lại trạng thái và tiền
                                double totalPriceCYN = Convert.ToDouble(p.TotalPrice);
                                double totalPriceVND = totalPriceCYN * currency;
                                if (wallet >= totalPriceVND)
                                {
                                    wallet = wallet - totalPriceVND;
                                    AccountController.updateWallet(accYeucau.ID, wallet, currentDate, username_current);
                                    HistoryPayWalletController.Insert(accYeucau.ID, accYeucau.Username, 0,
                                        totalPriceVND,
                                    accYeucau.Username + " đã được duyệt thanh toán trong yêu cầu thanh toán hộ ID: " + p.ID + ".",
                                    wallet, 1, 9, currentDate, username_current);

                                    NotificationController.Inser(u_loginin.ID, username_current, accYeucau.ID, accYeucau.Username, 0,
                                                       "Bạn đã được duyệt thanh toán trong yêu cầu thanh toán hộ ID: " + p.ID + ".", 0,
                                                       7, DateTime.Now, u_loginin.Username);
                                    PayhelpController.UpdateNew(p.ID, txtSummary.Text, totalPriceCYN.ToString(),
                                                        totalPriceVND.ToString(), currency.ToString(), statusNew, txtStaffNote.Text,
                                                        currentDate, u_loginin.Username);
                                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlert("Tài khoản không đủ tiền", "e", true, Page);
                                }
                            }
                            else
                            {
                                //Chỉ cập nhật lại tiền và trạng thái
                                PayhelpController.UpdateNew(p.ID, txtSummary.Text, p.TotalPrice.ToString(),
                                    "0", currency.ToString(), statusNew, txtStaffNote.Text,
                                    currentDate, u_loginin.Username);
                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
                            }
                        }
                    }
                }
            }

            #endregion

            #region Cách cũ
            //var ac = AccountController.GetByUsername(username_current);
            //if (ac != null)
            //{
            //    if (p != null)
            //    {
            //        int status_old = Convert.ToInt32(p.Status);
            //        //if (status_old == 0 || status_old == 2)
            //        if (status_old == 0 || status_old == 2 || status_old == 4)
            //        {
            //            //if (status == 1 || status == 3 || status == 4)
            //            if (status == 1 || status == 3)
            //            {
            //                int UID = Convert.ToInt32(p.UID);
            //                int id = p.ID;
            //                var u = AccountController.GetByID(UID);
            //                if (u != null)
            //                {
            //                    string username = u.Username;
            //                    //var p = PayhelpController.GetByIDAndUID(id, UID);
            //                    if (p != null)
            //                    {
            //                        double wallet = Convert.ToDouble(u.Wallet);
            //                        double walletCYN = Convert.ToDouble(u.WalletCYN);

            //                        double Totalprice_left = 0;

            //                        double Currency = Convert.ToDouble(p.Currency);
            //                        double TotalPrice = Convert.ToDouble(p.TotalPrice);
            //                        double TotalPriceVND = Convert.ToDouble(p.TotalPriceVND);
            //                        if (walletCYN > 0)
            //                        {
            //                            if (walletCYN >= TotalPrice)
            //                            {
            //                                double walletCYN_left = walletCYN - TotalPrice;
            //                                AccountController.updateWalletCYN(UID, walletCYN_left);
            //                                HistoryPayWalletCYNController.Insert(UID, username, TotalPrice, walletCYN_left, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
            //                                    currentDate, username);

            //                                PayhelpController.UpdateStatus(id, 1, currentDate, username);
            //                                string statusText_odl = "";
            //                                if (status_old == 0)
            //                                    statusText_odl = "Chưa thanh toán";
            //                                else if (status_old == 1)
            //                                    statusText_odl = "Đã thanh toán";
            //                                else if (status_old == 2)
            //                                    statusText_odl = "Đã hủy";
            //                                else if (status_old == 3)
            //                                    statusText_odl = "Hoàn thành";
            //                                else statusText_odl = "Đã xác nhận";

            //                                if (status_old != status)
            //                                {
            //                                    HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                                    2, currentDate, username_current);
            //                                }

            //                                PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                                    chkIsNotComplete.Checked, currentDate, username_current);
            //                                string[] pds = list.Split('|');
            //                                for (int i = 0; i < pds.Length - 1; i++)
            //                                {
            //                                    string[] pd = pds[i].Split(',');
            //                                    int PDID = pd[0].ToInt();
            //                                    string des1 = pd[1];
            //                                    string des2 = pd[2];

            //                                    PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                                }
            //                                if (status == 3)
            //                                {
            //                                    //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                                    //if (usend != null)
            //                                    //{
            //                                    //    try
            //                                    //    {
            //                                    //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                    //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                    //    }
            //                                    //    catch { }
            //                                    //}

            //                                }
            //                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //                                //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //                            }
            //                            else
            //                            {
            //                                double walletCYN_left = TotalPrice - walletCYN;
            //                                double totalpricevndpay = walletCYN_left * Currency;
            //                                if (wallet >= totalpricevndpay)
            //                                {
            //                                    //double walletCYN_left = TotalPrice - walletCYN;
            //                                    AccountController.updateWalletCYN(UID, 0);
            //                                    HistoryPayWalletCYNController.Insert(UID, username, walletCYN, 0, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
            //                                        currentDate, username);

            //                                    //double totalpricevndpay = walletCYN_left * Currency;
            //                                    double walletleft = wallet - totalpricevndpay;
            //                                    AccountController.updateWallet(UID, walletleft, currentDate, username);
            //                                    HistoryPayWalletController.Insert(UID, username, 0, totalpricevndpay,
            //                                        username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);
            //                                    PayhelpController.UpdateStatus(id, 1, currentDate, username);

            //                                    string statusText_odl = "";
            //                                    if (status_old == 0)
            //                                        statusText_odl = "Chưa thanh toán";
            //                                    else if (status_old == 1)
            //                                        statusText_odl = "Đã thanh toán";
            //                                    else if (status_old == 2)
            //                                        statusText_odl = "Đã hủy";
            //                                    else if (status_old == 3)
            //                                        statusText_odl = "Hoàn thành";
            //                                    else statusText_odl = "Đã xác nhận";

            //                                    if (status_old != status)
            //                                    {
            //                                        HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                                        2, currentDate, username_current);
            //                                    }

            //                                    PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                                        chkIsNotComplete.Checked, currentDate, username_current);
            //                                    string[] pds = list.Split('|');
            //                                    for (int i = 0; i < pds.Length - 1; i++)
            //                                    {
            //                                        string[] pd = pds[i].Split(',');
            //                                        int PDID = pd[0].ToInt();
            //                                        string des1 = pd[1];
            //                                        string des2 = pd[2];

            //                                        PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                                    }
            //                                    if (status == 3)
            //                                    {
            //                                        //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                                        //if (usend != null)
            //                                        //{
            //                                        //    try
            //                                        //    {
            //                                        //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                        //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                        //    }
            //                                        //    catch { }
            //                                        //}

            //                                    }
            //                                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //                                    //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //                                }
            //                                else
            //                                {
            //                                    PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            if (wallet >= TotalPriceVND)
            //                            {
            //                                double walletleft = wallet - TotalPriceVND;
            //                                AccountController.updateWallet(UID, walletleft, currentDate, username);
            //                                HistoryPayWalletController.Insert(UID, username, 0, TotalPriceVND,
            //                                    username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, currentDate, username);
            //                                PayhelpController.UpdateStatus(id, 1, currentDate, username);

            //                                string statusText_odl = "";
            //                                if (status_old == 0)
            //                                    statusText_odl = "Chưa thanh toán";
            //                                else if (status_old == 1)
            //                                    statusText_odl = "Đã thanh toán";
            //                                else if (status_old == 2)
            //                                    statusText_odl = "Đã hủy";
            //                                else if (status_old == 3)
            //                                    statusText_odl = "Hoàn thành";
            //                                else statusText_odl = "Đã xác nhận";

            //                                if (status_old != status)
            //                                {
            //                                    HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                                    2, currentDate, username_current);
            //                                }

            //                                PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                                    chkIsNotComplete.Checked, currentDate, username_current);
            //                                string[] pds = list.Split('|');
            //                                for (int i = 0; i < pds.Length - 1; i++)
            //                                {
            //                                    string[] pd = pds[i].Split(',');
            //                                    int PDID = pd[0].ToInt();
            //                                    string des1 = pd[1];
            //                                    string des2 = pd[2];

            //                                    PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                                }
            //                                if (status == 3)
            //                                {
            //                                    //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                                    //if (usend != null)
            //                                    //{
            //                                    //    try
            //                                    //    {
            //                                    //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                    //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                                    //    }
            //                                    //    catch { }
            //                                    //}

            //                                }
            //                                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //                                //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //                            }
            //                            else
            //                            {
            //                                PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                string statusText_odl = "";
            //                if (status_old == 0)
            //                    statusText_odl = "Chưa thanh toán";
            //                else if (status_old == 1)
            //                    statusText_odl = "Đã thanh toán";
            //                else if (status_old == 2)
            //                    statusText_odl = "Đã hủy";
            //                else if (status_old == 3)
            //                    statusText_odl = "Hoàn thành";
            //                else statusText_odl = "Đã xác nhận";

            //                if (status_old != status)
            //                {
            //                    HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                    2, currentDate, username_current);
            //                }

            //                PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                    chkIsNotComplete.Checked, currentDate, username_current);
            //                string[] pds = list.Split('|');
            //                for (int i = 0; i < pds.Length - 1; i++)
            //                {
            //                    string[] pd = pds[i].Split(',');
            //                    int PDID = pd[0].ToInt();
            //                    string des1 = pd[1];
            //                    string des2 = pd[2];

            //                    PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                }
            //                if (status == 3)
            //                {
            //                    //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                    //if (usend != null)
            //                    //{
            //                    //    try
            //                    //    {
            //                    //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                    //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                    //    }
            //                    //    catch { }
            //                    //}

            //                }
            //                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            }
            //        }
            //        else if (status_old == 1)
            //        {
            //            if (status == 0 || status == 2)
            //            {
            //                double amountvnd = Convert.ToDouble(p.TotalPriceVND);
            //                string statusText_odl = "";
            //                if (status_old == 0)
            //                    statusText_odl = "Chưa thanh toán";
            //                else if (status_old == 1)
            //                    statusText_odl = "Đã thanh toán";
            //                else if (status_old == 2)
            //                    statusText_odl = "Đã hủy";
            //                else if (status_old == 3)
            //                    statusText_odl = "Hoàn thành";
            //                else statusText_odl = "Đã xác nhận";

            //                //Hoàn tiền cho user
            //                int UID = Convert.ToInt32(p.UID);
            //                int id = p.ID;
            //                var u = AccountController.GetByID(UID);
            //                if (u != null)
            //                {
            //                    string username = u.Username;
            //                    double wallet = Convert.ToDouble(u.Wallet);
            //                    double wallet_left = wallet + amountvnd;
            //                    AccountController.updateWallet(UID, wallet_left, currentDate, username);
            //                    HistoryPayWalletController.Insert(UID, username, 0, amountvnd,
            //                        "Hoàn tiền thanh toán hộ.", wallet_left, 2, 2, currentDate, username);
            //                }

            //                if (status_old != status)
            //                {
            //                    HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                    2, currentDate, username_current);
            //                }

            //                PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                    chkIsNotComplete.Checked, currentDate, username_current);
            //                string[] pds = list.Split('|');
            //                for (int i = 0; i < pds.Length - 1; i++)
            //                {
            //                    string[] pd = pds[i].Split(',');
            //                    int PDID = pd[0].ToInt();
            //                    string des1 = pd[1];
            //                    string des2 = pd[2];

            //                    PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                }
            //                //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                //if (usend != null)
            //                //{
            //                //    try
            //                //    {
            //                //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hủy thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã bị hủy tại vominhthien.com.", "");
            //                //    }
            //                //    catch { }
            //                //}
            //                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            }
            //            //else if (status == 1 || status == 3)
            //            //{
            //            //    int UID = Convert.ToInt32(p.UID);
            //            //    int id = p.ID;
            //            //    var u = AccountController.GetByID(UID);
            //            //    if (u != null)
            //            //    {
            //            //        string username = u.Username;
            //            //        //var p = PayhelpController.GetByIDAndUID(id, UID);
            //            //        if (p != null)
            //            //        {
            //            //            double wallet = Convert.ToDouble(u.Wallet);
            //            //            double walletCYN = Convert.ToDouble(u.WalletCYN);

            //            //            double Totalprice_left = 0;

            //            //            double Currency = Convert.ToDouble(p.Currency);
            //            //            double TotalPrice = Convert.ToDouble(p.TotalPrice);
            //            //            double TotalPriceVND = Convert.ToDouble(p.TotalPriceVND);
            //            //            if (walletCYN > 0)
            //            //            {
            //            //                if (walletCYN >= TotalPrice)
            //            //                {
            //            //                    double walletCYN_left = walletCYN - TotalPrice;
            //            //                    AccountController.updateWalletCYN(UID, walletCYN_left);
            //            //                    HistoryPayWalletCYNController.Insert(UID, username, TotalPrice, walletCYN_left, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
            //            //                        currentDate, username);

            //            //                    PayhelpController.UpdateStatus(id, 1, currentDate, username);
            //            //                    string statusText_odl = "";
            //            //                    if (status_old == 0)
            //            //                        statusText_odl = "Chưa thanh toán";
            //            //                    else if (status_old == 1)
            //            //                        statusText_odl = "Đã thanh toán";
            //            //                    else if (status_old == 2)
            //            //                        statusText_odl = "Đã hủy";
            //            //                    else if (status_old == 3)
            //            //                        statusText_odl = "Hoàn thành";
            //            //                    else statusText_odl = "Đã xác nhận";

            //            //                    if (status_old != status)
            //            //                    {
            //            //                        HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //            //                                        2, currentDate, username_current);
            //            //                    }

            //            //                    PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //            //                        currentDate, username_current);
            //            //                    string[] pds = list.Split('|');
            //            //                    for (int i = 0; i < pds.Length - 1; i++)
            //            //                    {
            //            //                        string[] pd = pds[i].Split(',');
            //            //                        int PDID = pd[0].ToInt();
            //            //                        string des1 = pd[1];
            //            //                        string des2 = pd[2];

            //            //                        PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //            //                    }
            //            //                    if (status == 3)
            //            //                    {
            //            //                        var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //            //                        if (usend != null)
            //            //                        {
            //            //                            try
            //            //                            {
            //            //                                //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                                PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                            }
            //            //                            catch { }
            //            //                        }

            //            //                    }
            //            //                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            //                    //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //            //                }
            //            //                else
            //            //                {
            //            //                    double walletCYN_left = TotalPrice - walletCYN;
            //            //                    double totalpricevndpay = walletCYN_left * Currency;
            //            //                    if (wallet >= totalpricevndpay)
            //            //                    {
            //            //                        //double walletCYN_left = TotalPrice - walletCYN;
            //            //                        AccountController.updateWalletCYN(UID, 0);
            //            //                        HistoryPayWalletCYNController.Insert(UID, username, walletCYN, 0, 1, 1, username + " đã trả tiền thanh toán tiền hộ.",
            //            //                            currentDate, username);

            //            //                        //double totalpricevndpay = walletCYN_left * Currency;
            //            //                        double walletleft = wallet - totalpricevndpay;
            //            //                        AccountController.updateWallet(UID, walletleft);
            //            //                        HistoryPayWalletController.Insert(UID, username, 0, "", totalpricevndpay,
            //            //                            username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, "", currentDate, username);
            //            //                        PayhelpController.UpdateStatus(id, 1, currentDate, username);
            //            //                        var adminlist = AccountController.GetAllByRoleID(0);
            //            //                        if (adminlist.Count > 0)
            //            //                        {
            //            //                            foreach (var a in adminlist)
            //            //                            {
            //            //                                NotificationController.InsertAdmin(UID, username, a.ID, a.Username, 0, "", username + " đã trả tiền thanh toán tiền hộ.", 0, true, 2, currentDate, username);
            //            //                            }
            //            //                        }
            //            //                        string statusText_odl = "";
            //            //                        if (status_old == 0)
            //            //                            statusText_odl = "Chưa thanh toán";
            //            //                        else if (status_old == 1)
            //            //                            statusText_odl = "Đã thanh toán";
            //            //                        else if (status_old == 2)
            //            //                            statusText_odl = "Đã hủy";
            //            //                        else if (status_old == 3)
            //            //                            statusText_odl = "Hoàn thành";
            //            //                        else statusText_odl = "Đã xác nhận";

            //            //                        if (status_old != status)
            //            //                        {
            //            //                            HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //            //                                            2, currentDate, username_current);
            //            //                        }

            //            //                        PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //            //                            currentDate, username_current);
            //            //                        string[] pds = list.Split('|');
            //            //                        for (int i = 0; i < pds.Length - 1; i++)
            //            //                        {
            //            //                            string[] pd = pds[i].Split(',');
            //            //                            int PDID = pd[0].ToInt();
            //            //                            string des1 = pd[1];
            //            //                            string des2 = pd[2];

            //            //                            PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //            //                        }
            //            //                        if (status == 3)
            //            //                        {
            //            //                            var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //            //                            if (usend != null)
            //            //                            {
            //            //                                try
            //            //                                {
            //            //                                    //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                                    PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                                }
            //            //                                catch { }
            //            //                            }

            //            //                        }
            //            //                        PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            //                        //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //            //                    }
            //            //                    else
            //            //                    {
            //            //                        PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
            //            //                    }
            //            //                }
            //            //            }
            //            //            else
            //            //            {
            //            //                if (wallet >= TotalPriceVND)
            //            //                {
            //            //                    double walletleft = wallet - TotalPriceVND;
            //            //                    AccountController.updateWallet(UID, walletleft);
            //            //                    HistoryPayWalletController.Insert(UID, username, 0, "", TotalPriceVND,
            //            //                        username + " đã trả tiền thanh toán tiền hộ.", walletleft, 1, 9, "", currentDate, username);
            //            //                    PayhelpController.UpdateStatus(id, 1, currentDate, username);
            //            //                    var adminlist = AccountController.GetAllByRoleID(0);
            //            //                    if (adminlist.Count > 0)
            //            //                    {
            //            //                        foreach (var a in adminlist)
            //            //                        {
            //            //                            NotificationController.InsertAdmin(UID, username, a.ID, a.Username, 0, "", username + " đã trả tiền thanh toán tiền hộ.", 0, true, 2, currentDate, username);
            //            //                        }
            //            //                    }
            //            //                    string statusText_odl = "";
            //            //                    if (status_old == 0)
            //            //                        statusText_odl = "Chưa thanh toán";
            //            //                    else if (status_old == 1)
            //            //                        statusText_odl = "Đã thanh toán";
            //            //                    else if (status_old == 2)
            //            //                        statusText_odl = "Đã hủy";
            //            //                    else if (status_old == 3)
            //            //                        statusText_odl = "Hoàn thành";
            //            //                    else statusText_odl = "Đã xác nhận";

            //            //                    if (status_old != status)
            //            //                    {
            //            //                        HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //            //                                        2, currentDate, username_current);
            //            //                    }

            //            //                    PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //            //                        currentDate, username_current);
            //            //                    string[] pds = list.Split('|');
            //            //                    for (int i = 0; i < pds.Length - 1; i++)
            //            //                    {
            //            //                        string[] pd = pds[i].Split(',');
            //            //                        int PDID = pd[0].ToInt();
            //            //                        string des1 = pd[1];
            //            //                        string des2 = pd[2];

            //            //                        PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //            //                    }
            //            //                    if (status == 3)
            //            //                    {
            //            //                        var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //            //                        if (usend != null)
            //            //                        {
            //            //                            try
            //            //                            {
            //            //                                //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                                PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //            //                            }
            //            //                            catch { }
            //            //                        }

            //            //                    }
            //            //                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            //                    //PJUtils.ShowMessageBoxSwAlert("Thanh toán thành công", "s", true, Page);
            //            //                }
            //            //                else
            //            //                {
            //            //                    PJUtils.ShowMessageBoxSwAlert("Bạn phải nạp thêm tiền vào để thanh toán", "e", true, Page);
            //            //                }
            //            //            }
            //            //        }
            //            //    }
            //            //}
            //            else
            //            {
            //                string statusText_odl = "";
            //                if (status_old == 0)
            //                    statusText_odl = "Chưa thanh toán";
            //                else if (status_old == 1)
            //                    statusText_odl = "Đã thanh toán";
            //                else if (status_old == 2)
            //                    statusText_odl = "Đã hủy";
            //                else if (status_old == 3)
            //                    statusText_odl = "Hoàn thành";
            //                else statusText_odl = "Đã xác nhận";


            //                if (status_old != status)
            //                {
            //                    HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                    2, currentDate, username_current);
            //                }

            //                PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                    chkIsNotComplete.Checked, currentDate, username_current);
            //                string[] pds = list.Split('|');
            //                for (int i = 0; i < pds.Length - 1; i++)
            //                {
            //                    string[] pd = pds[i].Split(',');
            //                    int PDID = pd[0].ToInt();
            //                    string des1 = pd[1];
            //                    string des2 = pd[2];

            //                    PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //                }
            //                if (status == 3)
            //                {
            //                    //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                    //if (usend != null)
            //                    //{
            //                    //    try
            //                    //    {
            //                    //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                    //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                    //    }
            //                    //    catch { }
            //                    //}

            //                }
            //                PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //            }
            //        }
            //        else
            //        {
            //            string statusText_odl = "";
            //            if (status_old == 0)
            //                statusText_odl = "Chưa thanh toán";
            //            else if (status_old == 1)
            //                statusText_odl = "Đã thanh toán";
            //            else if (status_old == 2)
            //                statusText_odl = "Đã hủy";
            //            else if (status_old == 3)
            //                statusText_odl = "Hoàn thành";
            //            else statusText_odl = "Đã xác nhận";


            //            if (status_old != status)
            //            {
            //                HistoryServiceController.Insert(ID, ac.ID, ac.Username, status_old, statusText_odl, status, ddlStatus.SelectedItem.ToString(),
            //                                2, currentDate, username_current);
            //            }

            //            PayhelpController.Update(ID, txtSummary.Text, pPriceCYN.Value.ToString(), pPriceVND.Value.ToString(), ddlStatus.SelectedValue.ToInt(), txtPhone.Text,
            //                chkIsNotComplete.Checked, currentDate, username_current);
            //            string[] pds = list.Split('|');
            //            for (int i = 0; i < pds.Length - 1; i++)
            //            {
            //                string[] pd = pds[i].Split(',');
            //                int PDID = pd[0].ToInt();
            //                string des1 = pd[1];
            //                string des2 = pd[2];

            //                PayhelpDetailController.Update(PDID, des1, des2, currentDate, username_current);
            //            }
            //            if (status == 3)
            //            {
            //                //var usend = AccountController.GetByID(Convert.ToInt32(p.UID));
            //                //if (usend != null)
            //                //{
            //                //    try
            //                //    {
            //                //        //PJUtils.SendMailGmail("vominhthien1688@gmail.com", "oiecjnsgwpsfgndz", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                //        PJUtils.SendMailDomain("lienhe@vominhthien.top", "0Chitaobiet", usend.Email, "Thông báo đã hoàn thành thanh toán hộ tại vominhthien.com", "Yêu cầu thanh toán hộ của bạn đã hoàn thành tại vominhthien.com.", "");
            //                //    }
            //                //    catch { }
            //                //}

            //            }
            //            PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công", "s", true, Page);
            //        }
            //    }
            //}
            #endregion

        }
    }
}