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
using NHST.Controllers;
using NHST.Models;

namespace NHST
{
    public partial class tao_ma_tth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "nhutsg8844";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadData()
        {
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int roleID = Convert.ToInt32(u.RoleID);
                if (roleID == 1)
                {
                    if (u.IsAgent != true)
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                ltrIfn.Text = username;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                double wallet = Convert.ToDouble(u.Wallet);
                double currencyAgent = 0;
                var config = ConfigurationController.GetByTop1();
                if (config != null)
                {
                    currencyAgent = Convert.ToDouble(config.AgentCurrency);
                }
                double totalPriceAllCYN = 0;
                double totalPriceAllVND = 0;
                string list = hdflist.Value;
                if (!string.IsNullOrEmpty(list))
                {
                    string[] items = list.Split('|');
                    if (items.Length - 1 > 0)
                    {
                        for (int i = 0; i < items.Length - 1; i++)
                        {
                            var item = items[i].Split(':');
                            double priceCYN = 0;
                            double priceVND = 0;
                            if (item[0].ToFloat(0) > 0)
                                priceCYN = Convert.ToDouble(item[0]);
                            string note = item[1];
                            priceVND = priceCYN * currencyAgent;
                            totalPriceAllCYN += priceCYN;

                            if (priceCYN > 0)
                            {
                                PayhelpController.Insert(UID, username, note, priceCYN.ToString(),
                                    priceVND.ToString(), currencyAgent.ToString(), "", "", 
                                    1, "", currentDate, username);

                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        NotificationController.Inser(UID, username, admin.ID,
                                                                           admin.Username, 0,
                                                                           "Có yêu cầu thanh toán hộ mới", 0, 7,
                                                                           currentDate, username);
                                    }
                                }

                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        NotificationController.Inser(UID, username, manager.ID,
                                                                          manager.Username, 0,
                                                                          "Có yêu cầu thanh toán hộ mới", 0, 7,
                                                                          currentDate, username);
                                    }
                                }
                            }
                        }
                    }
                }
                if (totalPriceAllCYN > 0)
                {
                    totalPriceAllVND = totalPriceAllCYN * currencyAgent;
                }
                if (wallet >= totalPriceAllVND)
                {
                    PJUtils.ShowMessageBoxSwAlert("Yêu cầu của bạn đã được gửi đến quản trị viên. Xin chân thành cám ơn", "s", false, Page);
                }
                else
                {
                    double rechar = totalPriceAllVND - wallet;
                    string html = "";
                    html += "Quý khách đã gửi yêu cầu Thanh toán hộ thành công.<br/>";
                    html += "Tổng số tiền yêu cầu thanh toán hộ: "+totalPriceAllCYN+" tệ, quy đổi: "+string.Format("{0:N0}",totalPriceAllVND)+" VNĐ.<br/>";
                    html += "Số tiền trong tài khoản quý khách: " + string.Format("{0:N0}", wallet) + " VNĐ.<br/>";
                    html += "Để yêu cầu thanh toán hộ được duyệt, quý khách vui lòng nạp thêm: " + string.Format("{0:N0}", rechar) + " VNĐ.<br/>";
                    html += "Để nạp tiền vào tài khoản vui lòng xem hướng dẫn <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong>TẠI ĐÂY</strong></a>.<br/>";
                    PJUtils.ShowMessageBoxHTMLSwAlert(html, "s", true, Page);
                }
            }
            else
                PJUtils.ShowMessageBoxSwAlert("Không tìm thấy user", "e", false, Page);
        }
    }
}