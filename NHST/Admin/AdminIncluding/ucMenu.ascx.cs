using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;
using System.Text;

namespace NHST.Admin.AdminIncluding
{
    public partial class ucMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkRole();
            }
        }
        public void checkRole()
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account u = AccountController.GetByUsername(username_current);
            if (u != null)
            {
                int RoleID = u.RoleID.ToString().ToInt();
                if (RoleID != 1)
                {
                    if (u.RoleID == 0)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Configuration.aspx\"><span class=\"menu-icon glyphicon glyphicon-cog\"></span>";
                        ltrAdminMenu.Text += "<p>Thiết lập</p> <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Configuration.aspx\">Hệ thống</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/BenefitsList.aspx\">Danh sách lợi ích</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/CommitmentList.aspx\">Danh sách cam kết</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/PartnerList.aspx\">Danh sách đối tác</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/SupportBuyProductList.aspx\">Danh sách hỗ trợ</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/SocialSupportList.aspx\">Danh sách mạng hỗ trợ</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/StepList.aspx\">Danh sách bước đặt hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/WebChinaList.aspx\">Danh sách web Trung Quốc</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/MenuList.aspx\">Danh sách trang</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/WeigtPricehList.aspx\">Phí vận chuyển</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Tariff-TQVN.aspx\">Chi phí TQ-VN</a></li>";

                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/warehouse-fee.aspx\">Chi phí chuyển kho</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Tariff-Buypro.aspx\">Chi phí dịch vụ mua hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/PriceChangeList.aspx\">Phí mua hộ</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/InWareHousePriceList.aspx\">Phí lưu kho</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Page-SEO-List.aspx\">Quản lý SEO các trang</a></li>";
                        ltrAdminMenu.Text += "</ul>";
                        ltrAdminMenu.Text += "</li>";

                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/UserList.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        ltrAdminMenu.Text += "  <p>Người dùng</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/AddUser.aspx\">Thêm người dùng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/UserList.aspx\">Danh sách người dùng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/User-Level.aspx\">Danh sách cấp người dùng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/HistorySendWallet.aspx\">Lịch sử nạp tiền</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/RequestRechargeCYN.aspx\">Yêu cầu nạp tiền tệ</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/History-Pay-Order.aspx\">Lịch sử thanh toán đơn hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Withdraw-List.aspx\">Danh sách rút tiền</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/admin-staff-income.aspx\">Hoa hồng nhân viên</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        //ltrAdminMenu.Text += "  <p>Sản phẩm đặt hàng</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/ChinaSiteList.aspx\">Danh sách trang Trung Quốc</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/ProductCategoryList.aspx\">Danh sách danh mục sản phẩm</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/ProductLinkList.aspx\">Danh sách link sản phẩm</a></li>";                        
                        //ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a><span class=\"arrow\"></span>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=1\">Đơn hàng thường</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=2\">Đơn hàng wechat</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=3\">Đơn hàng từ link khác</a></li>";                        
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/danh-sach-vch.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Vận chuyển hộ</p></a><span class=\"arrow\"></span>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/danh-sach-vch.aspx\">DS vận chuyển hộ</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-VCH.aspx\">Thống kê cước VCH</a></li>";                        
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/danh-sach-vch.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p>Vận chuyển hộ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/payforlist.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Thanh toán hộ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/refund-cyn.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Hoàn tiền</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/ComplainList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Khiếu nại</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/shippingtypelistvn.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>PT Vận chuyển</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/SupportList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Hỗ trợ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Customer-Feedback-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Liên hệ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Page-Type-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Danh mục trang</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/PageList.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p> Quản lý trang</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Report-Income.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Thống kê</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user.aspx\">TK DS Tài khoản</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-income-userdathang.aspx\">TK DT tài khoản NVĐH</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-warehouse-staff.aspx\">TK DT tài khoản NV kho hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Income.aspx\">Báo cáo doanh thu</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Orders.aspx\">Báo cáo đơn hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-recharge.aspx\">Báo cáo tiền khách nạp</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user-wallet.aspx\">Báo cáo khách hàng có số dư</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-order.aspx\">Báo cáo đơn hàng đã mua, kho TQ, kho đích</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-User-Use-Wallet.aspx\">Báo cáo lịch sử giao dịch</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/TQWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng TQ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/VNWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng kho đích</p></a></li>";

                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p> Xuất kho</p></a></li>";

                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "  <p>Xuất kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/request-outstock.aspx\">Yêu cầu xuất kho</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/danh-sach-kh.aspx\">Danh sách khách hàng</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";


                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/request-ship-list.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p>Yêu cầu ký gửi</p></a>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/BigPackageList.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p>Quản lý ký gửi</p></a>";
                        //ltrAdminMenu.Text += "<ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/find-small-package.aspx\">Tìm kiếm theo SĐT</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/outstock-smallpackage.aspx\">Xuất kho cho khách</a></li>";
                        //ltrAdminMenu.Text += "</ul>";
                        //ltrAdminMenu.Text += "</li>";
                    }
                    else if (u.RoleID == 2)
                    {
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/UserList.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        ltrAdminMenu.Text += "  <p>Người dùng</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/AddUser.aspx\">Thêm người dùng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/UserList.aspx\">Danh sách người dùng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/User-Level.aspx\">Danh sách cấp người dùng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/HistorySendWallet.aspx\">Lịch sử nạp tiền</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/RequestRechargeCYN.aspx\">Yêu cầu nạp tiền tệ</a></li>";                        
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Withdraw-List.aspx\">Danh sách rút tiền</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/admin-staff-income.aspx\">Hoa hồng nhân viên</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a><span class=\"arrow\"></span>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=1\">Đơn hàng thường</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=2\">Đơn hàng wechat</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=3\">Đơn hàng từ link khác</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/danh-sach-vch.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Vận chuyển hộ</p></a><span class=\"arrow\"></span>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/danh-sach-vch.aspx\">DS vận chuyển hộ</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-VCH.aspx\">Thống kê cước VCH</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/danh-sach-vch.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p>Vận chuyển hộ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/payforlist.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Thanh toán hộ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/refund-cyn.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Hoàn tiền</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/ComplainList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Khiếu nại</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/SupportList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Hỗ trợ</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Customer-Feedback-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Liên hệ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Page-Type-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p>Danh mục trang</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/PageList.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p> Quản lý trang</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Report-Income.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Thống kê</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user.aspx\">TK DS Tài khoản</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-income-userdathang.aspx\">TK DT tài khoản NVĐH</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-warehouse-staff.aspx\">TK DT tài khoản NV kho hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Income.aspx\">Báo cáo doanh thu</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Orders.aspx\">Báo cáo đơn hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-recharge.aspx\">Báo cáo tiền khách nạp</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user-wallet.aspx\">Báo cáo khách hàng có số dư</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-order.aspx\">Báo cáo đơn hàng đã mua, kho TQ, kho đích</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-User-Use-Wallet.aspx\">Báo cáo lịch sử giao dịch</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/TQWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng TQ</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/VNWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng kho đích</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "  <p>Xuất kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/request-outstock.aspx\">Yêu cầu xuất kho</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/danh-sach-kh.aspx\">Danh sách khách hàng</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/UserList.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        //ltrAdminMenu.Text += "  <p>Người dùng</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/AddUser.aspx\">Thêm người dùng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/UserList.aspx\">Danh sách người dùng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/User-Level.aspx\">Danh sách cấp người dùng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/HistorySendWallet.aspx\">Lịch sử nạp tiền</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/RequestRechargeCYN.aspx\">Yêu cầu nạp tiền tệ</a></li>";                        
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Withdraw-List.aspx\">Danh sách rút tiền</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/admin-staff-income.aspx\">Hoa hồng nhân viên</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/History-Pay-Order.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Lịch sử thanh toán đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/ComplainList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Khiếu nại</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/SupportList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Hỗ trợ</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Report-Income.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Thống kê</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Orders.aspx\">Báo cáo đơn hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-recharge.aspx\">Báo cáo tiền khách nạp</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user-wallet.aspx\">Báo cáo khách hàng có số dư</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/TQWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Kiểm hàng TQ</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/VNWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Kiểm hàng kho đích</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "  <p>Xuất kho</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/request-outstock.aspx\">Yêu cầu xuất kho</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                    }
                    else if (u.RoleID == 7)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Withdraw-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        ltrAdminMenu.Text += "<p>Lịch sử rút tiền</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/historysendwalletaccountant.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        ltrAdminMenu.Text += "<p>Lịch sử nạp tiền</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Accountant-User-List.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        ltrAdminMenu.Text += "<p>Danh sách người dùng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Request-Send-Money.aspx\"><span class=\"menu-icon glyphicon glyphicon-user\"></span>";
                        //ltrAdminMenu.Text += "<p>Yêu cầu nạp tiền</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Report-Income.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Thống kê</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Income.aspx\">Báo cáo doanh thu</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-Orders.aspx\">Báo cáo đơn hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-recharge.aspx\">Báo cáo tiền khách nạp</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-user-wallet.aspx\">Báo cáo khách hàng có số dư</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-order.aspx\">Báo cáo đơn hàng đã mua, kho TQ, kho đích</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Report-User-Use-Wallet.aspx\">Báo cáo lịch sử giao dịch</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p> Xuất kho</p></a></li>";
                    }
                    else if (u.RoleID == 4)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/danh-sach-kh.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Danh sách khách hàng</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/TQWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Warehouse-Management.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                    }
                    else if (u.RoleID == 5)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/VNWareHouse.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Kiểm hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Warehouse-Management.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        //ltrAdminMenu.Text += "<p> Xuất kho</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "  <p>Xuất kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/request-outstock.aspx\">Yêu cầu xuất kho</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/danh-sach-kh.aspx\">Danh sách khách hàng</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                    }
                    else if (u.RoleID == 8)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"/Admin/Warehouse-Management.aspx\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        //ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        //ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        //ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        //ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OutStock.aspx\"><span class=\"menu-icon glyphicon glyphicon-duplicate\"></span>";
                        ltrAdminMenu.Text += "<p> Xuất kho</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pr\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-stats\"></span>";
                        ltrAdminMenu.Text += "  <p>Quản lý kho</p>";
                        ltrAdminMenu.Text += "  <span class=\"arrow\"></span></a>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Warehouse-Management.aspx\">Quản lý bao hàng</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/Order-Transaction-Code.aspx\">Quản lý mã vận đơn</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                    }
                    else if (u.RoleID == 3)
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/ComplainList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Khiếu nại</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"javascript:;\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a><span class=\"arrow\"></span>";
                        ltrAdminMenu.Text += "  <ul class=\"sub-menu\">";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=1\">Đơn hàng thường</a></li>";
                        ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=2\">Đơn hàng wechat</a></li>";
                        //ltrAdminMenu.Text += "      <li><a href=\"/Admin/OrderList.aspx?type=3\">Đơn hàng từ link khác</a></li>";
                        ltrAdminMenu.Text += "</ul></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/staff-income.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Hoa hồng</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/Warehouse-Management.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Quản lý bao hàng</p></a></li>";
                    }
                    else if (u.RoleID == 4)
                    {
                        
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/staff-income.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Hoa hồng</p></a></li>";
                    }
                    else
                    {
                        ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/OrderList.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        ltrAdminMenu.Text += "<p>Đơn hàng</p></a></li>";
                        //ltrAdminMenu.Text += "<li id=\"pp\" class=\"droplink\"><a href=\"/Admin/yeu-cau-giao.aspx\"><span class=\"menu-icon glyphicon glyphicon-shopping-cart\"></span>";
                        //ltrAdminMenu.Text += "<p>Đơn hàng yêu cầu giao</p></a></li>";
                    }
                }
            }

        }
    }
}