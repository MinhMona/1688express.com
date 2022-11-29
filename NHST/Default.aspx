<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHST.Default7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <div class="banner">
            <div class="banner__top">
                <div class="all">
                    <div class="wrap-bg">
                    </div>
                </div>
            </div>
            <div class="banner_bot">
                <div class="all">
                    <div class="wrap">
                        <div>
                            <span style="font-family: Arial; font-size: 28px;">ĐẶT HÀNG CHỈ TRONG 30S</span><br />
                            <span style="font-family: Arial; font-size: 21px;">CÀI ĐẶT CÔNG CỤ ĐẶT HÀNG CHO TRÌNH DUYỆT NGAY
                            </span>
                        </div>
                        <div class="button">
                            <a href="https://chrome.google.com/webstore/detail/công-cụ-đặt-hàng-của-1688/dpgofcjebnjdhnijckcpeomncmbeilkj" target="_blank" class="btn btn-2"><i class="fab fa-chrome"></i><span class="text">Chrome</span></a>
                            <a href="https://chrome.google.com/webstore/detail/công-cụ-đặt-hàng-của-1688/dpgofcjebnjdhnijckcpeomncmbeilkj" target="_blank" class="btn btn-3"><i class="fab fa-chrome"></i><span class="text">Cốc cốc</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <style>
            table.bank-info {
                margin: 0 auto;
                width: 70%;
            }

                table.bank-info th {
                    border-bottom: 1px solid #fff;
                    padding: 5px 10px;
                    min-width: 120px;
                    color: #fff;
                    font-size: 13px;
                    text-transform: uppercase;
                    font-weight: bold;
                    font-size: 20px;
                    text-align: left;
                }

                table.bank-info td {
                    vertical-align: top;
                    padding: 15px 3px;
                    border-bottom: 1px solid #aaa;
                    color: #fff;
                    font-size: 15px;
                    line-height: 20px;
                }

                    table.bank-info td strong {
                        color: #f9a924;
                    }

            .midlecontent span {
                font-size: 20px !important;
            }

            .midlecontent table.bank-info {
                margin: 0 auto;
            }

                .midlecontent table.bank-info th {
                    border-bottom: 1px solid #4a4a4a;
                    padding: 5px 10px;
                    min-width: 120px;
                    color: #f79646;
                    font-size: 18px;
                    text-transform: uppercase;
                    font-weight: bold;
                }

                .midlecontent table.bank-info td {
                    vertical-align: top;
                    padding: 15px 3px;
                    border-bottom: 1px solid #aaa;
                    color: #1b75be;
                    font-size: 15px;
                    line-height: 20px;
                }

                    .midlecontent table.bank-info td strong {
                        color: #f79646;
                    }
        </style>
        <div class="quytrinhdathang sec-pad">
            <div class="all">
                <div class="sec-title fz-36">
                    <h1>Quy trình đặt hàng</h1>
                    <span class="border"></span>
                </div>
                <ul class="list-step">
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/cach-dang-ki-tai-khoan">
                            <div class="img"><i class="fas fa-clipboard-list"></i></div>
                            <div class="text semibold"><span class="color">1.</span> Đăng ký tài khoản</div>
                        </a>
                    </li>
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/huong-dan-cai-dat-addon-mua-hang-1688express">
                            <div class="img"><i class="fas fa-cog"></i></div>
                            <div class="text semibold">
                                <span class="color">2.</span> Cài đặt công cụ mua hàng
                            </div>
                        </a>
                    </li>
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/huong-dan-tao-don-va-mua-hang">
                            <div class="img"><i class="fas fa-cart-plus"></i></div>
                            <div class="text semibold"><span class="color">3.</span> Chọn hàng và thêm hàng vào giỏ</div>
                        </a>
                    </li>
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/huong-dan-tao-don-va-mua-hang">
                            <div class="img"><i class="fas fa-share-square"></i></div>
                            <div class="text semibold"><span class="color">4.</span> Gửi đơn đặt hàng</div>
                        </a>
                    </li>
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/huong-dan-tao-don-va-mua-hang">
                            <div class="img"><i class="fas fa-credit-card"></i></div>
                            <div class="text semibold"><span class="color">5.</span> Đặt cọc tiền hàng</div>
                        </a>
                    </li>
                    <li class="item">
                        <a href="/chuyen-muc/huong-dan/huong-dan-gui-yeu-cau-xuat-kho">
                            <div class="img"><i class="fas fa-box-open"></i></div>
                            <div class="text semibold"><span class="color">6.</span>Nhận hàng và thanh toán</div>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="chinhsach sec-pad">
            <div class="all">
                <div class="sec-title fz-36">
                    <h1>Chính sách</h1>
                    <span class="border"></span>
                </div>
                <div class="wrap">
                    <div class="child hover-zoomin">
                        <div class="p-pad img icon">
                            <img src="/App_Themes/1688/images/chinhsach-icon-1.png" alt="">
                        </div>
                        <h4 class="p-pad fz-18 color2">Chính sách kiểm hàng</h4>
                        <div class="text">
                            <p>Khách hàng có thể lựa chọn hoặc không lựa chọn sử dụng dịch vụ kiểm hàng</p>
                        </div>
                    </div>
                    <div class="child hover-zoomin">
                        <div class="img p-pad icon">
                            <img src="/App_Themes/1688/images/chinhsach-icon-2.png" alt="">
                        </div>
                        <h4 class="fz-18 p-pad color2">Chính sách khiếu nại</h4>
                        <div class="text">
                            <p>Khách hàng có thể lựa chọn hoặc không lựa chọn sử dụng dịch vụ kiểm hàng</p>
                        </div>
                    </div>
                    <div class="child hover-zoomin">
                        <div class="img p-pad icon">
                            <img src="/App_Themes/1688/images/chinhsach-icon-3.png" alt="">
                        </div>
                        <h4 class="fz-18 p-pad color2">Hướng dẫn tổng hợp</h4>
                        <div class="text">
                            <p>Khách hàng có thể lựa chọn hoặc không lựa chọn sử dụng dịch vụ kiểm hàng</p>
                        </div>
                    </div>
                    <div class="child hover-zoomin">
                        <div class="img p-pad icon">
                            <img src="/App_Themes/1688/images/chinhsach-icon-4.png" alt="">
                        </div>
                        <h4 class="fz-18 p-pad color2">Thông tin thanh toán</h4>
                        <div class="text">
                            <p>Khách hàng có thể lựa chọn hoặc không lựa chọn sử dụng dịch vụ kiểm hàng</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="brand sec-pad">
            <div class="all">
                <div class="wrap">
                    <div class="child">
                        <div class="img">
                            <img src="/App_Themes/1688/images/tmall-brand.png" alt="">
                        </div>
                        <div class="text">
                            <p>
                                TaoBao là một hệ thống Website của tập đoàn Alibaba bán hàng dạng thương mại điện tử, 
                mọi quá trình mua và bán đều thông qua mang Internet kể cả thanh toán và chọn hàng.
                            </p>
                        </div>
                    </div>
                    <div class="child">
                        <div class="img">
                            <img src="/App_Themes/1688/images/1688-brand.png" alt="">
                        </div>
                        <div class="text">
                            <p>
                                TaoBao là một hệ thống Website của tập đoàn Alibaba bán hàng dạng thương mại điện tử, 
                mọi quá trình mua và bán đều thông qua mang Internet kể cả thanh toán và chọn hàng.
                            </p>
                        </div>
                    </div>
                    <div class="child">
                        <div class="img">
                            <img src="/App_Themes/1688/images/taobao-brand.png" alt="">
                        </div>
                        <div class="text">
                            <p>
                                TaoBao là một hệ thống Website của tập đoàn Alibaba bán hàng dạng thương mại điện tử, 
                mọi quá trình mua và bán đều thông qua mang Internet kể cả thanh toán và chọn hàng.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="chungtoicamket sec-pad">
            <div class="all">
                <div class="sec-title fz-36">
                    <h1>Chúng tôi cam kết</h1>
                    <span class="border"></span>
                </div>
                <div class="wrap">
                    <div class="img">
                        <img src="/App_Themes/1688/images/chungtoicamket-bg.png" alt="">
                    </div>
                    <div class="content">
                        <div class="child hover-zoomin">
                            <h4 class="fz-18">KHÔNG CÓ THỜI GIAN TRỄ KHI ĐẶT HÀNG</h4>
                            <div class="text">
                                <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                            </div>
                        </div>
                        <div class="child hover-zoomin">
                            <h4 class="fz-18">Cam kết mua hàng trong 24h</h4>
                            <div class="text">
                                <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                            </div>
                        </div>
                        <div class="child hover-zoomin">
                            <h4 class="fz-18">Tiết kiệm thời gian quản lý</h4>
                            <div class="text">
                                <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                            </div>
                        </div>
                        <div class="child hover-zoomin">
                            <h4 class="fz-18">Hổ trợ trực tuyến 24/7</h4>
                            <div class="text">
                                <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="quyenloi sec-pad2">
            <div class="all">
                <div class="sec-title fz-36">
                    <h1>Quyền lợi khách hàng</h1>
                    <span class="border"></span>
                </div>
                <div class="wrap">
                    <div class="child hover-zoomin">
                        <div class="p-pad img icon">
                            <img src="/App_Themes/1688/images/quyenloi-icon-1.png" alt="">
                        </div>
                        <h4 class="p-pad fz-18 color2">Ưu đãi theo<br>
                            cấp độ thành viên</h4>
                        <div class="text">
                            <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                        </div>
                    </div>
                    <div class="child hover-zoomin">
                        <div class="p-pad img icon">
                            <img src="/App_Themes/1688/images/quyenloi-icon-2.png" alt="">
                        </div>
                        <h4 class="p-pad fz-18 color2">Ưu đãi theo<br>
                            doanh thu tháng</h4>
                        <div class="text">
                            <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                        </div>
                    </div>
                    <div class="child hover-zoomin">
                        <div class="p-pad img icon">
                            <img src="/App_Themes/1688/images/quyenloi-icon-3.png" alt="">
                        </div>
                        <h4 class="p-pad fz-18 color2">Ưu đãi theo<br>
                            sản lượng tháng</h4>
                        <div class="text">
                            <p>Quý khách chủ động với toàn bộ quy trình nạp tiền, thanh toán và đặt hàng tự động</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="connect">
            <div class="contact sec-pad">
                <div class="all">
                    <div class="wrap">
                        <div class="child">
                            <div class="img icon p-pad">
                                <img src="/App_Themes/1688/images/contact-icon-1.png" alt="">
                            </div>
                            <h4 class="fz-18 white p-pad">Giờ hoạt động</h4>
                            <div class="text p-pad">
                                <asp:Literal ID="ltrTimework" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="child">
                            <div class="img icon p-pad">
                                <img src="/App_Themes/1688/images/contact-icon-2.png" alt="">
                            </div>
                            <h4 class="fz-18 white p-pad">Email contact:</h4>
                            <div class="text p-pad">
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="child">
                            <div class="img icon p-pad">
                                <img src="/App_Themes/1688/images/contact-icon-3.png" alt="">
                            </div>
                            <h4 class="fz-18 white p-pad">Hotline</h4>
                            <div class="text">
                                <asp:Literal ID="ltrHotline" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="all">
                    <div class="sec-title fz-36" style="margin-top: 50px;">
                        <h1 style="color: #fff;">Thông tin tài khoản</h1>
                        <span class="border custom-border"></span>
                    </div>
                    <table class="bank-info">
                        <thead>
                            <tr>
                                <th>Số tài khoản</th>
                                <th>Thông tin</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><strong>002100 1605 135</strong></td>
                                <td><strong>VietComBank</strong> chi nhánh Hà Nội.<br />
                                    <strong>Chủ TK: NGUYỄN QUANG KHẢI</strong></td>
                            </tr>
                            <tr>
                                <td><strong>13032 0632 6666</strong></td>
                                <td><strong>AGRIBANK</strong> chi nhánh Hà Thành.<br />
                                    <strong>Chủ TK:NGUYỄN QUANG KHẢI</strong></td>
                            </tr>
                            <tr>
                                <td><strong>190 2044 3749 685</strong></td>
                                <td><strong>TECHCOMBANK</strong> chi nhánh Hà Nội.<br />
                                    <strong>Chủ TK: NGUYỄN QUANG KHẢI</strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="contact-map">
                <div class="all">
                    <div class="google-map">
                        <%--<iframe src="https://www.google.com/maps/d/embed?mid=1af27yG8ie60W2aTa7_m9zwW0ICnmTbTb" width="100%" height="330"></iframe>--%>
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1862.4958302493399!2d105.77727974557023!3d20.992971407133997!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3134538e6913db27%3A0x4c4c89251e475778!2s1688Express!5e0!3m2!1svi!2s!4v1635144448871!5m2!1svi!2s" width="100%" height="330"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }
        function closeandnotshow() {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/setNotshow",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    close_popup_ms();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });

        }
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/getPopup",
                //data: "{ID:'" + id + "',UserName:'" + username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "null") {
                        var data = JSON.parse(msg.d);
                        var title = data.NotiTitle;
                        var content = data.NotiContent;
                        var email = data.NotiEmail;
                        var obj = $('form');
                        $(obj).css('overflow', 'hidden');
                        $(obj).attr('onkeydown', 'keyclose_ms(event)');
                        var bg = "<div id='bg_popup_home'></div>";
                        var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                                 "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
                        fr += "<div class=\"popup_header\">";
                        fr += title;
                        fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                        fr += "</div>";
                        fr += "     <div class=\"changeavatar\">";
                        fr += "         <div class=\"content1\" style=\"height:480px;overflow-y:scroll;overflow-x:hidden;\">";
                        fr += content;
                        fr += "         </div>";
                        fr += "         <div class=\"content2\">";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close-full\" onclick='closeandnotshow()'>Đóng & không hiện thông báo</a>";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
                        fr += "         </div>";
                        fr += "     </div>";
                        fr += "<div class=\"popup_footer\">";
                        fr += "<span class=\"float-right\">" + email + "</span>";
                        fr += "</div>";
                        fr += "   </div>";
                        fr += "</div>";
                        $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                        $(fr).appendTo($(obj));
                        setTimeout(function () {
                            $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                            $("#bg_popup").attr("onclick", "close_popup_ms()");
                        }, 1000);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        });
    </script>
    <style>
        #bg_popup_home {
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: #333;
            opacity: 0.7;
            filter: alpha(opacity=70);
            left: 0px;
            top: 0px;
            z-index: 999999999;
            opacity: 0;
            filter: alpha(opacity=0);
        }

        #popup_ms_home {
            background: #fff;
            border-radius: 0px;
            box-shadow: 0px 2px 10px #fff;
            float: left;
            position: fixed;
            width: 735px;
            z-index: 10000;
            left: 50%;
            margin-left: -370px;
            top: 200px;
            opacity: 0;
            filter: alpha(opacity=0);
            height: 360px;
        }

            #popup_ms_home .popup_body {
                border-radius: 0px;
                float: left;
                position: relative;
                width: 735px;
            }

            #popup_ms_home .content {
                /*background-color: #487175;     border-radius: 10px;*/
                margin: 12px;
                padding: 15px;
                float: left;
            }

            #popup_ms_home .title_popup {
                /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                color: #ffffff;
                font-family: Arial;
                font-size: 24px;
                font-weight: bold;
                height: 35px;
                margin-left: 0;
                margin-top: -5px;
                padding-left: 40px;
                padding-top: 0;
                text-align: center;
            }

            #popup_ms_home .text_popup {
                color: #fff;
                font-size: 14px;
                margin-top: 20px;
                margin-bottom: 20px;
                line-height: 20px;
            }

                #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                    color: #FFFFFF;
                    display: block;
                    float: left;
                    font-style: italic;
                    list-style: -moz-hangul outside none;
                    margin-bottom: 5px;
                    margin-left: 110px;
                    -webkit-transition-duration: 0.3s;
                    -moz-transition-duration: 0.3s;
                    transition-duration: 0.3s;
                }

                    #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                        color: #8cd8fd;
                    }

            #popup_ms_home .close_popup {
                background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                display: block;
                height: 28px;
                position: absolute;
                right: 0px;
                top: 5px;
                width: 26px;
                cursor: pointer;
                z-index: 10;
            }

        #popup_content_home {
            height: auto;
            position: fixed;
            background-color: #fff;
            top: 15%;
            z-index: 999999999;
            left: 25%;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 50%;
            padding: 20px;
        }

        #popup_content_home {
            padding: 0;
        }

        .popup_header, .popup_footer {
            float: left;
            width: 100%;
            background: #29aae1;
            padding: 10px;
            position: relative;
            color: #fff;
        }

        .popup_header {
            font-weight: bold;
            font-size: 16px;
            text-transform: uppercase;
        }

        .close_message {
            top: 10px;
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
        }

        .float-right {
            float: right;
        }

        .content1 {
            float: left;
            width: 100%;
        }

        .content2 {
            float: left;
            width: 100%;
            border-top: 1px solid #eee;
            clear: both;
            margin-top: 10px;
        }

        .btn.btn-close {
            float: right;
            background: #29aae1;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
        }

            .btn.btn-close:hover {
                background: #1f85b1;
            }

        .btn.btn-close-full {
            float: right;
            background: #7bb1c7;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }
    </style>
</asp:Content>
