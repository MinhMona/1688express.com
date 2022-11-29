<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="cong-cu.aspx.cs" Inherits="NHST.cong_cu1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Công cụ đặt hàng</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Công cụ đặt hàng</h2>
            <div class="order-tool clearfix">
                <div class="order-tool-left">
                    <img src="/App_Themes/pdv/assets/images/destop.png" alt="#">
                </div>
                <div class="order-tool-right">
                    <h3 class="order-title">ADDON 1688 Express SẼ GIÚP BẠN:</h3>
                    <p>1. Tiết kiệm thời gian và tăng cơ hội kinh doanh</p>
                    <p>2. Đặt hàng nhanh chóng, thuận tiện và chính xác</p>
                    <p>3. Form đặt hàng hiển thị sẵn khi vào trang chi tiết</p>
                    <p>4. Hỗ trợ dịch tự động từ tiếng Trung sang tiếng Việt</p>
                    <h3 class="order-title">SỬ DỤNG TRÊN TRÌNH DUYỆT CHROME & CỜ RÔM+ (CỐC CỐC)</h3>
                    <p>
                        Cài đặt nhanh chóng, hạn chế tối đa việc cài đặt lại
							Tự động cập nhật khi có phiên bản mới
                    </p>
                    <p><a href="#">Click để cài đặt</a></p>
                    <div class="tool-setting">
                        <a href="#">
                            <img src="/App_Themes/pdv/assets/images/chrom-set.png" alt="#"></a>
                        <a href="#">
                            <img src="/App_Themes/pdv/assets/images/cococ-set.png" alt="#"></a>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="tool-detail">
                    <p>Sau khi tạo được tài khoản trên Giao hàng Bảo Tín , khách hàng cần phải cài đặt <span>CÔNG CỤ MUA HÀNG </span>thì mới có thể mua được hàng được trên trang web thương mại điện tử của Trung Quốc. Công cụ này có tính năng ước lượng được mức giá của món hàng và giúp bạn thêm vào rỏ hàng.</p>
                    <h3 class="tool-detail-title">Bước 1</h3>
                    <p>Cài đặt công cụ đặt hàng tự động cho trình duyệt Google Chorme hoặc Cốc Cốc </p>
                    <h3 class="tool-detail-title">Bước 2</h3>
                    <p>Khách hàng click vào dòng chữ <span>“THÊM VÀO CHROME”</span> như trong hình để tải công cụ về máy.</p>
                    <img src="/App_Themes/pdv/assets/images/tool1.jpg" alt="#">
                    <h3 class="tool-detail-title">Bước 3</h3>
                    <p>Tiếp tục click vào chữ <span>“ THÊM TIỆN ÍCH ”</span> như trong hình Và đợi trong vòng 3 giây để việc cài đặt hoàn tất.</p>
                    <img src="/App_Themes/pdv/assets/images/tool2.jpg" alt="#">
                    <p>Khi biểu tượng này xuất hiện ở góc phải của giao diện hoặc trong phần tiện ích thì bạn đã cài đặt thành công công cụ</p>
                    <img src="/App_Themes/pdv/assets/images/tool3.jpg" alt="#">
                </div>
                <div class="cmt">
                    <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
