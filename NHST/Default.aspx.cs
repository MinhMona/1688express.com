using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Write(PJUtils.Decrypt("userpass", "h8OqGM6Rd1PDFLn2njgtCHtKFZlGMXLV"));
                //PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", "phuong@mona.media",
                //                            "Yêu cầu xuất kho của bạn đã được chấp nhận tại 1688 Express", "abc", "");
                //string message = "";
                //message += "Chào Qúy khách!<br/><br/>";
                //message += "Đơn hàng của Qúy khách đã có kiện hàng vê đến kho Hà Nội..<br/><br/>";
                //message += "Để nhận hàng, quý khách vui lòng thanh toán số tiền còn thiếu bằng cách nạp tiền vào tài khoản và gửi yêu cầu xuất kho ( xem hướng dẫn <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" style=\"text-decoration:underline:\" target=\"_blank\">nạp tiền</a> và <a href=\"https://1688express.com.vn/chuyen-muc/huong-dan/huong-dan-gui-yeu-cau-xuat-kho\" style=\"text-decoration:underline:\" target=\"_blank\">gửi yêu cầu xuất kho</a> ).<br/><br/>";
                //message += "Lưu ý: Sau 3 ngày kể từ khi đơn hàng về đủ, nếu khách hàng không nhận hàng, mọi yêu cầu xử lý khiếu nại được chấp nhận giải quyết là rất thấp do quá thời hạn xử lý của shop bán. Vì vậy 1688Express mong quý khách sắp xếp nhận hàng sớm, đặc biệt đối với khách hàng sử dụng dịch vụ chuyển phát chậm.<br/><br/>";
                //message += "- <strong>Khách ở HN</strong> : đến trực tiếp Vp 1688Express để lấy hàng , hoặc 1688Express sẽ hỗ trợ tìm Shipper chuyển hàng đến tận nơi cho khách <br/><i style=\"font-style: italic;\">( khách hàng thanh toán phí vận chuyển với shipper khi nhận hàng)</i><br/><br/>";
                //message += "<span style=\"width:100%;text-align:center;font-style:italic\"><span style=\"color:red\">THỜI GIAN NHẬN HÀNG TẠI VĂN PHÒNG HÀ NỘI:</span><br/>"
                //        + ">>> Sáng : Từ 9h-12h từ thứ 2 đến thứ 7<br/>"
                //        + ">>> Chiều : Từ 14h-17h  từ thứ 2 đến thứ 7<br/>"
                //        + "  </span><br/><br/>";
                //message += "- <strong>Khách ở khu vực khác</strong> : 1688Express sẽ chuyển hàng đến tận nơi cho khách  thông qua Viettel post hoặc hình thức vận chuyển khách hàng đề xuất ( <span style=\"color:red\">XEM HƯỚNG DẪN CHI TIẾT BÊN DƯỚI</span>)<br/><br/>";
                //message += "<span style=\"font-weight:bold; color:#0070c0\">>>> CHUYỂN HÀNG BẰNG VIETTEL POST :</span><br/>";
                //message += "<span style=\"font-weight:bold; color:#0070c0\">Qúy khách vui lòng chọn hình thức <strong>Chuyển phát Nhanh hoặc Chậm</strong> của Viettel post để 1688Express chủ động gửi hàng cho bạn.</span><br/>";
                //message += "Hàng sẽ được vận chuyển đến Địa Chỉ của bạn. Phí ship bạn sẽ thanh toán với nhân viên Viettel khi nhận hàng. Thông tin về từng hình thức:<br/><br/>";
                //message += "   • Chuyển phát nhanh: 1-2 ngày nhận hàng, phí 30.000- 40.000 VND/kg.<br/>";
                //message += "   • Chuyển phát chậm: 3-6 ngày nhận hàng, phí 15.000- 25.000 VND/kg.<br/><br/>";

                //message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                //message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";                
                //PJUtils.SendMailGmail("no-reply@mona-media.com", "demonhunter", "phuong@mona.media", "Khởi tạo tài khoản tại 1688 Express", message, "");
                LoadData();
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
              
                ltrTimework.Text = confi.TimeWork;
                ltrEmail.Text += "<p><a href=\"mailto:" + email + "\">" + email + "</a></p>";
                ltrHotline.Text += "<p><a href=\"tel:" + hotline+ "\">" + hotline + "</a></p>";

                //ltrTopLeft.Text += "<p>Tỷ giá ¥: <span>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>"
                //                + "  <a href=\"\"><i class=\"fas fa-phone\"></i>" + hotline + "</a>"
                //                + "  <p>(Thời gian làm việc: " + confi.TimeWork + ")</p>";
                //ltrWebname.Text = confi.Websitename;
                //ltrHotline2.Text = "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                //ltrEmail2.Text = "<a href=\"mailto:" + email + "\">" + email + "</a>";
                //ltrAddress.Text = confi.Address; ;
                //ltrSocial.Text += "<div class=\"social__item fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-facebook-f\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-twitter\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item ins\"><a href=\"" + confi.Instagram + "\" target=\"_blank\"><i class=\"fab fa-instagram\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item sky\"><a href=\"" + confi.Skype + "\" target=\"_blank\"><i class=\"fab fa-skype\"></i></a></div>";

                //ltrConfig.Text += "<p class=\"info\">Tỷ giá: <span class=\"hl-txt\"> " + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                //ltrConfig.Text += "<p class=\"info\"><i class=\"fas fa fa-clock-o\"></i> Giờ làm việc: " + confi.TimeWork + "</p>";
                //ltrConfig.Text += "<a href=\"mailto:" + email + "\" class=\"info\"><i class=\"fas fa-fw fa-envelope\"></i> Email: " + email + "</a>";
                //ltrConfig.Text += "<a href=\"tel:" + hotline + "\" class=\"info\"><i class=\"fa fa-phone-square\"></i> " + hotline + "</a>";

                //ltrSocial.Text += "<div class=\"icon-fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-fw fa-facebook-f\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-fw fa-twitter\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-gg\"><a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fab fa-fw fa-google-plus-g\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-pi\"><a href=\"" + confi.Pinterest + "\" target=\"_blank\"><i class=\"fab fa-fw fa-pinterest-p\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-ib\"><a href=\"mailto:" + email + "\"><i class=\"fas fa-fw fa-envelope\"></i></a></div>";               
            }
        }
        [WebMethod]
        public static string closewebinfo()
        {
            HttpContext.Current.Session["infoclose"] = "ok";
            return "ok";
        }
        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopup;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n);
                }
                else
                    return "null";
            }
            else
                return null;

        }
        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }
    }
}