<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="Thanh-toan.aspx.cs" Inherits="NHST.Thanh_toan1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .steps {
            float: left;
            width: 100%;
        }

            .steps .step {
                display: table-cell;
                width: 1%;
                text-align: center;
            }

                .steps .step .step-img {
                    position: relative;
                    margin-bottom: 25px;
                }

                .steps .step .title {
                    text-transform: uppercase;
                }

                .steps .step .step-img:before {
                    content: '';
                    background-color: #ebebeb;
                    width: 100%;
                    left: 0;
                    height: 2px;
                    margin-top: -1px;
                    top: 50%;
                    position: absolute;
                    z-index: 1;
                }

                .steps .step:first-child .step-img:before {
                    left: -80px;
                    width: calc(100% + 80px);
                }

                .steps .step img {
                    margin: 0 auto;
                    display: block;
                }

                .steps .step .step-img img {
                    position: relative;
                    z-index: 4;
                    border-radius: 50%;
                    -webkit-border-radius: 50%;
                }

                .steps .step:first-child .step-img:after {
                    content: '';
                    position: absolute;
                    z-index: 1;
                    left: -80px;
                    width: 20px;
                    height: 20px;
                    border-radius: 50%;
                    -webkit-border-radius: 50%;
                    margin-top: -10px;
                    background-color: #ebebeb;
                    top: 50%;
                }

                .steps .step.active .step-img:before {
                    background-color: #2178bf;
                }

        .checkout-sec .checkout-left {
            float: left;
            width: 50%;
            padding-right: 15px;
        }

        .checkout-sec .checkout-right {
            float: left;
            width: 50%;
            padding-left: 15px;
        }

        .policiy-check {
            float: left;
            text-align: left;
        }

            .policiy-check input[type=checkbox] {
                float: left;
                text-align: left;
                width: auto;
            }

        .feat-tt {
            float: left;
            width: 100%;
            font-size: 14px;
            margin-bottom: 15px;
            text-transform: uppercase;
            font-weight: bold;
        }

        .right {
            float: right;
        }


        .order-detail {
            float: left;
            width: 100%;
            background-color: #fafafa;
            padding: 30px;
            margin-bottom: 20px;
        }

            .order-detail table {
                float: left;
                width: 100%;
                border-collapse: collapse;
            }

                .order-detail table td {
                    vertical-align: middle;
                    padding: 5px 0;
                }

        .hl-txt {
            color: #2178bf;
        }

        .form-control {
            width: 100%;
        }

        .tool-detail {
            text-align: left;
        }

        .order-detail table td {
            vertical-align: middle;
            padding: 5px 0;
        }

        .order-detail table .borderbtm td {
            padding-bottom: 20px;
        }

        .order-detail table .borderbtm + tr td {
            padding-top: 20px;
        }

        .order-detail table .borderbtm {
            border-bottom: solid 1px #ebebeb;
        }

        .thumb-product .info {
            float: none;
            display: table-cell;
            vertical-align: middle;
            padding: 0 15px 0 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Thanh toán</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="tool-detail">
                            <div class="sec step-sec">
                                <div class="steps">
                                    <div class="step ">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-1.png" alt="">
                                        </div>
                                        <h4 class="title">Giỏ hàng</h4>
                                    </div>
                                    <div class="step active">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-2.png" alt="">
                                        </div>
                                        <h4 class="title">Chọn địa chỉ nhận hàng</h4>
                                    </div>
                                    <div class="step ">
                                        <div class="step-img">
                                            <img src="/App_Themes/NHST/images/order-step-3.png" alt="">
                                        </div>
                                        <h4 class="title">Đặt cọc và kết đơn</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="sec checkout-sec" style="display: inline-block; margin: 50px 0">
                                <div class="checkout-left">
                                    <h4 class="feat-tt">Thông tin tài khoản</h4>
                                    <div class="order-addinfo">
                                        <div>
                                            <div class="form-row">
                                                <div class="lb">Họ tên</div>
                                                <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Địa chỉ</div>
                                                <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Email</div>
                                                <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Số điện thoại</div>
                                                <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" CssClass="form-control">></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <h4 class="feat-tt">Địa chỉ giao hàng</h4>
                                    <div class="order-addinfo">
                                        <div>
                                            <div class="form-row">
                                                <div class="lb">Họ tên</div>
                                                <asp:TextBox ID="txt_DFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rq1" ControlToValidate="txt_DFullname" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Địa chỉ</div>
                                                <asp:TextBox ID="txt_DAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_DAddress" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Email</div>
                                                <asp:TextBox ID="txt_DEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_DEmail" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row">
                                                <div class="lb">Số điện thoại</div>
                                                <asp:TextBox ID="txt_DPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_DPhone" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-row btn-row">
                                            <asp:CheckBox ID="chk_DK" runat="server" />
                                            Tôi đồng ý với các <a href="/chuyen-muc/gioi-thieu/dieu-khoan-su-dung" style="color: blue;" target="_blank">điều khoản đặt hàng</a> của 1688 Express
                                            <%--<a href="#" class="right btn pill-btn primary-btn">HOÀN TẤT</a>--%>
                                        </div>
                                        <div class="form-row btn-row">
                                            <asp:Label ID="lblCheckckd" runat="server" Text="Vui lòng xác nhận trước khi hoàn tất đơn hàng." ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="form-row btn-row">
                                            <a href="/gio-hang" class="left hl-txt link"><i class="fa fa-long-arrow-alt-left"></i>Quay lại</a>
                                            <a href="javascript:;" id="finishorder" class="right btn pill-btn primary-btn  main-btn hover">HOÀN TẤT</a>
                                            <asp:Button ID="btn_saveOrder" runat="server" OnClick="btn_saveOrder_Click" Text="HOÀN TẤT" Style="display: none;"
                                                CssClass="right btn pill-btn primary-btn  main-btn hover" />
                                            <%--<a href="#" class="right btn pill-btn primary-btn">HOÀN TẤT</a>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="checkout-right">
                                    <asp:Literal ID="ltr_pro" runat="server"></asp:Literal>
                                    <%-- <div class="form-row">
                                        <div class="lb">Chuyển hàng về kho:</div>
                                        <asp:DropDownList ID="ddlWarehouseID" runat="server" CssClass="form-control" onchange="getWareHouse()"></asp:DropDownList>
                                    </div>
                                    <div class="form-row shippingtype">
                                        
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:HiddenField ID="hdfTeamWare" runat="server" />
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                
            </section>
        </div>
    </main>--%>
    <script type="text/javascript">
        function getWareHouse(obj) {
            var wa = obj.val();
            var shippinttype = obj.parent().parent().parent().find(".shippingtype");
            if (wa != "4") {

                shippinttype.show();
                shippinttype.find("select").val("1");
            }
            else {
                shippinttype.hide();
                shippinttype.find("select").val("1");
            }
        }
        $("#finishorder").click(function () {
            var html = "";
            $(".ordershoptem").each(function () {
                var obj = $(this);
                var id = obj.attr("data-id");
                var warehouseID = obj.find(".warehoseselect").val();
                var shippingtype = obj.find(".shippingtypesselect").val();
                html += id + ":" + warehouseID + "-" + shippingtype + "|";
            });
            $("#<%= hdfTeamWare.ClientID%>").val(html);
            $("#<%= btn_saveOrder.ClientID%>").click();
        });
        //Khu vực popup
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
        function showFinishOrder() {
            var text = "";
            text += "- Chúc mừng bạn đã tạo thành công đơn hàng trên hệ thống 1688Express.<br/>";
            text += "- Bạn có thể nạp tiền vào tài khoản: <br/><br/>";
            text += "Nội dung chuyển khoản:  NAP TIEN { tên tài khoản của bạn } + { SĐT } Trong đó bắt buộc có tên tài khoản đăng nhập hệ thống của quý khách, số điện thoại là số điện thoại quý khách đang sử dụng.<br/><br/>";
            text += "Số Tài Khoản: 002100 1605 135<br/>";
            text += "Chủ tài khoản: NGUYỄN QUANG KHẢI<br/>";
            text += "Ngân Hàng: VietComBank chi nhánh Hà Nội.<br/>";
            text += "-------------------------------------------------------<br/>";
            text += "Số Tài Khoản: 13032 0632 6666<br/>";
            text += "Chủ tài khoản: NGUYỄN QUANG KHẢI<br/>";
            text += "Ngân Hàng: AGRIBANK chi nhánh Hà Thành.<br/>";
            text += "-------------------------------------------------------<br/>";
            text += "Số Tài Khoản: 190 2044 3749 685<br/>";
            text += "Chủ tài khoản: NGUYỄN QUANG KHẢI<br/>";
            text += "Ngân Hàng: TECHCOMBANK chi nhánh Hà Nội.<br/>";
            text += "-------------------------------------------------------<br/><br/>";
            text += "- Sau khi hệ thống 1688Express update số dư tài khoản, bạn vui lòng chọn đơn hàng cần đặt cọc và bấm : ĐẶT CỌC<br/><br/>";
            text += "Mọi thắc mắc xin liên hệ :<br/>";
            text += "024.6326.5589 - 091.458.1688<br/>";
            text += "1688Express xin trân trọng cảm ơn.";
            var button = "<a href=\"/danh-sach-don-hang\" class=\"btn btn-close pill-btn primary-btn main-btn hover\">Đơn hàng của bạn</a>";
            showPopup("Đặt hàng thành công", text, button);
        }
        function showPopup(title, content, button) {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">";
            fr += title;
            //fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\">";
            fr += content;
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            //fr += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='close_popup_ms()'>Đóng</a>";
            //fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addOrderShopCode('" + shopID + "', '" + MainOrderID + "')\">Thêm</a>";
            fr += button;
            fr += "         </div>";
            fr += "     </div>";
            fr += "<div class=\"popup_footer\">";
            //fr += "<span class=\"float-right\">" + email + "</span>";
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
        //End khu vực popup
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
            background: url('/App_Themes/1688/images/close_button.png') no-repeat;
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
            padding: 0 20px;
            font-weight: bold;
            text-transform: uppercase;
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
        }

            .btn.btn-close-full:hover {
                background: #6692a5;
            }

        .label-popup {
            float: left;
            width: 100%;
            font-size: 14px;
            clear: both;
            margin-bottom: 10px;
            font-weight: bold;
        }
    </style>
</asp:Content>
