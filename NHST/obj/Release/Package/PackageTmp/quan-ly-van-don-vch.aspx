<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="quan-ly-van-don-vch.aspx.cs" Inherits="NHST.quan_ly_van_don_vch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        .black {
            color: #2a363b;
        }

        ul {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        .m-color {
            color: #2772db;
        }

        b, strong, .b {
            font-weight: bold;
        }

        b, strong {
            font-weight: bolder;
        }

        .page.orders-list .statistics li:last-child {
            border: none;
        }

        .page.orders-list .statistics li {
            display: inline-block;
            padding-right: 10px;
            margin-right: 10px;
            border-right: 1px solid #2a363b;
            line-height: 1;
        }

        .page.orders-list .stat-detail {
            width: 100%;
            margin: 20px 0;
            border-top: 1px solid #e1e1e1;
            border-bottom: 1px solid #e1e1e1;
            display: block;
            padding: 5px 0;
        }

        table {
            border-collapse: collapse;
        }

        .page.orders-list .stat-detail th, .page.orders-list .stat-detail td {
            padding: 10px 0;
            vertical-align: top;
            text-align: left;
        }

        .page.orders-list .stat-detail th {
            padding-right: 35px;
        }

        .page.orders-list .stat-detail td {
            display: inline-block;
            width: 395px;
        }

        article, aside, details, figcaption, figure, footer, header, main, menu, nav, section {
            display: block;
        }

        .clear {
            zoom: 1;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        .RadPicker_Default .rcCalPopup, .RadPicker_Default .rcTimePopup {
            display: none;
        }

        html body .riSingle .riTextBox[type="text"] {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        .page .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

        .page.orders-list .filters .lbl {
            padding-right: 50px;
        }

        .page .filters ul li {
            display: inline-block;
            text-align: center;
            padding-right: 2px;
        }

        .page .filters ul li {
            padding-right: 4px;
        }

        .page .filters input {
            padding: 2px 10px;
        }

        .page.orders-list .filters input.order-id {
            width: 270px;
        }

        .page .status-list > li {
            display: block;
            float: left;
            margin: 0 1px 10px 0;
        }

        .page .status-list a {
            height: 40px;
            line-height: 40px;
            display: block;
            background: #f8f8f8;
            color: #959595;
            font-weight: bold;
            padding: 0 15px;
        }

            .page .status-list li.current > a, .page .status-list a:hover {
                background: #2772db;
                color: #fff;
            }

        .width-20-per {
            width: 20%;
        }

        .width-15-per {
            width: 15%;
        }

        .page.orders-list .tbl-subtotal {
            margin-bottom: 20px;
        }

            .page.orders-list .tbl-subtotal th {
                padding-right: 60px;
            }

            .page.orders-list .tbl-subtotal td {
                padding: 8px 30px 8px 0;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Danh sách kiện yêu cầu VCH</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="content-text">
                            <div id="primary" class="page orders-list">
                                <div class="container">
                                    <aside class="filters">
                                        <ul>
                                            <li class="lbl" style="width: 100%; text-align: left; margin-bottom: 10px;">Tìm kiếm</li>
                                            <li style="width: 35%; margin-bottom: 10px;">
                                                <asp:TextBox ID="txtOrderCode" runat="server" CssClass="form-control" placeholder="Nhập mã đơn hàng hoặc mã vận đơn để tìm"></asp:TextBox>
                                            </li>
                                            <li style="width: 35%; margin-bottom: 10px;">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="3" Text="Tìm theo Mã đơn hàng"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Tìm theo Mã vận đơn"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="width: 20%; margin-bottom: 10px;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-1" Text="Tất cả trạng thái"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Đơn hàng mới"></asp:ListItem>
                                                    <%--<asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>--%>
                                                    <asp:ListItem Value="3" Text="Đã về kho TQ"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Đã về kho đích"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Đã thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Đã nhận hàng"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Đơn hàng hủy"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="width: 37%; margin-bottom: 10px;">
                                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                    DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                                    </DateInput>
                                                </telerik:RadDateTimePicker>
                                            </li>
                                            <li style="width: 37%; margin-bottom: 10px;">
                                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                    DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                                    </DateInput>
                                                </telerik:RadDateTimePicker>
                                            </li>
                                            <li class="width-15-per">
                                                <asp:Button ID="btnSear" runat="server" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover" OnClick="btnSear_Click" Text="LỌC TÌM KIẾM" />
                                                <%--<a class="submit-btn" href="#">LỌC TÌM KIẾM</a>--%>
                                            </li>
                                        </ul>
                                    </aside>
                                </div>
                                <div class="container">
                                    <a style="clear: both; float: right; padding: 5px 40px; margin-bottom: 20px; display: none"
                                        id="btn-deposit-select" class="btn btn-success btn-block pill-btn primary-btn main-btn hover"
                                        onclick="depositOrderSelect();" href="javascript:;">Đặt cọc <span class="ordercountdeposit"></span>

                                    </a>
                                </div>
                            </div>
                            <div class="table-panel">
                                <div class="table-panel-main full-width">
                                    <a href="javascript:;" class="btn pill-btn primary-btn main-btn hover" id="outAll"
                                        style="float: right;" onclick="requestoutstockAll()">Yêu cầu xuất kho tất cả kiện</a>
                                    <a href="javascript:;" id="exportselected" class="btn pill-btn primary-btn main-btn hover" style="float: right; margin-right: 10px; background: #21bfa2; display: none" onclick="requestexportselect()">Yêu cầu xuất kho các kiện đã chọn</a>
                                    <table>
                                        <tr>
                                            <th class="id" style="width: 1%;"></th>
                                            <th class="id" style="width: 1%;">ID</th>
                                            <th class="pro" style="width: 1%;">Mã vận đơn</th>
                                            <th class="pro" style="width: 1%;">Cân nặng</th>
                                            <th class="date" style="width: 5%;">Ghi chú KH</th>
                                            <th class="date" style="width: 5%;">Cước vật tư</th>
                                            <th class="date" style="width: 5%;">PP hàng ĐB</th>
                                            <th class="date" style="width: 5%;">Ngày tạo</th>
                                            <th class="date" style="width: 5%;">Ngày YCXK</th>
                                            <th class="date" style="width: 5%;">Ngày XK</th>
                                            <th class="date" style="width: 5%;">HTVC</th>
                                            <th class="date" style="width: 5%;">Ghi chú</th>
                                            <th class="status" style="width: 5%;">Trạng thái</th>
                                            <th class="status" style="width: 5%;"></th>
                                        </tr>
                                        <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                    </table>
                                    <%--<div class="pagination">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <asp:HiddenField ID="hdfListShippingVN" runat="server" />
    <asp:HiddenField ID="hdfListID" runat="server" />
    <asp:HiddenField ID="hdfShippingType" runat="server" />
    <asp:HiddenField ID="hdfNote" runat="server" />
    <asp:Button ID="btnPayExport" runat="server" OnClick="btnPayExport_Click" Style="display: none" />
    <style>
        .table.table-bordered > thead > tr > th, .table.table-bordered > tbody > tr > th, .table.table-bordered > tfoot > tr > th, .table.table-bordered > thead > tr > td, .table.table-bordered > tbody > tr > td, .table.table-bordered > tfoot > tr > td {
            padding: 10px 0;
        }

        .rgPager table, .rgPager table:hover {
            border: none !important;
        }

            .rgPager table th {
                background: none;
            }


        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            background: #fff url(/App_Themes/NHST/images/icon-select.png) no-repeat right 15px center;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 40px;
        }

        .ycgh-chk {
            width: 13%;
            margin: 0 auto;
        }

        .btn.pill-btn {
            border-radius: 0px;
            -webkit-border-radius: 0px;
            font-size: 15px;
            font-weight: normal;
            text-transform: none;
            padding: 5px;
        }

        .table-panel .table-panel-main table tr:nth-child(odd) {
            background-color: #fafafa;
        }

        .viewmore-orderlist {
            background-color: #2178bf;
            color: #fff;
            border: 1px solid transparent;
            padding: 5px;
            float: left;
            width: 100%;
            margin-bottom: 5px;
            text-align: center;
        }

            .viewmore-orderlist:hover {
                background-color: #195f98;
                color: #fff;
            }

        .chk-deposit {
            width: 35%;
            display: inline-block;
        }

        .ordercountdeposit {
            float: left;
            margin-left: 5px;
        }

        .order-status {
            float: left;
            margin-right: 5px;
            width: auto;
            margin-bottom: 10px;
            text-align: center;
            border: solid 1px #ccc;
        }

            .order-status:hover {
                border: solid 1px #ccc;
            }
    </style>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }
            function rejectOrder(obj) {
                var c = confirm('Bạn muốn hủy yêu cầu này?');
                if (c) {
                    var id = obj.parent().parent().attr("data-id");
                    var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"agreeCancel($(this),'" + id + "')\">Xác nhận</a>";
                    var html = "";
                    html += "<div class=\"popup-row\">";
                    html += "   <span class=\"popuprow-left\">Lý do hủy đơn:</span>";
                    html += "   <input class=\"form-control requestcancelnote popuprow-right\" placeholder=\"Lý do hủy đơn\"/>";
                    html += "</div>";
                    html += "<div class=\"popup-row\">";
                    html += "   <span class=\"popuperror\" style=\"color:red\"></span>";
                    html += "</div>";
                    showPopup("Hủy đơn VCH", html, button);
                }
            }
            function agreeCancel(obj, id) {
                var note = $(".requestcancelnote").val();
                if (isEmpty(note) != true) {
                    obj.removeAttr("onclick");
                    $.ajax({
                        type: "POST",
                        url: "/quan-ly-van-don-vch.aspx/rejectOrder",
                        data: "{id:'" + id + "',cancelnote:'" + note + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == 1) {
                                close_popup_ms();
                                swal
                                (
                                    {
                                        title: 'Thông báo',
                                        text: 'Hủy đơn hàng: ' + id + ' thành công',
                                        type: 'success'
                                    },
                                    function () { window.location.replace(window.location.href); }
                                );
                            }
                            else {
                                swal
                                (
                                    {
                                        title: 'Thông báo',
                                        text: 'Có lỗi trong quá trình hủy đơn, vui lòng thử lại sau.',
                                        type: 'errpr'
                                    },
                                    function () { window.location.replace(window.location.href); }
                                );
                            }
                            $(".popuperror").html("");
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
                else {
                    $(".popuperror").html("Vui lòng nhập lý do hủy đơn.");
                }
            }
            function viewdetail(obj) {

            }

            function paytoexport(obj) {
                var shippingtype = parseFloat($(".shippingtypevn").val());
                var note = $(".requestnote").val();
                if (shippingtype > 0) {
                    obj.removeAttr("onclick");
                    $("#<%=hdfNote.ClientID%>").val(note);
                    $("#<%=hdfShippingType.ClientID%>").val(shippingtype);
                    $("#<%=btnPayExport.ClientID%>").click();
                    $(".popuperror").html("");
                }
                else {
                    $(".popuperror").html("Vui lòng chọn hình thức vận chuyển.");
                }
            }
            function selectdeposit() {
                var check = false;
                $(".chk-deposit").each(function () {
                    if ($(this).is(':checked')) {
                        check = true;
                    }
                });
                if (check == true) {
                    $("#exportselected").show();
                    $("#exportselected1").show();
                }
                else {
                    $("#exportselected").hide();
                    $("#exportselected1").hide();
                }

            }
            function requestexportselect() {
                var count = 0;
                var html = "";
                $(".chk-deposit").each(function () {
                    if ($(this).is(':checked')) {
                        html += $(this).attr("data-id") + "|";
                        count++;
                    }
                });
                if (count > 0) {
                    //$("#<%= hdfListID.ClientID%>").val(html);
                    $.ajax({
                        type: "POST",
                        url: "/quan-ly-van-don-vch.aspx/exportSelectedAll",
                        data: "{listID:'" + html + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret != "0") {
                                var c = confirm('Bạn muốn xuất kho tất cả các kiện đã chọn?');
                                if (c == true) {
                                    var ret = msg.d;
                                    var data = ret.split(':');
                                    var status = data[0];
                                    var wallet = data[1];
                                    var walletstr = formatThousands(wallet, 0);
                                    var totalCount = data[2];
                                    var totalWeight = data[3];
                                    var totalWeightPriceCYN = data[4];
                                    var totalWeightPriceVND = data[5];
                                    var totalWeightPriceVNDstr = formatThousands(totalWeightPriceVND, 0);
                                    var feeOutStockCYN = data[6]
                                    var feeOutStockVND = data[7];
                                    var feeOutStockVNDstr = formatThousands(feeOutStockVND, 0);
                                    var totalPriceCYN = data[8];
                                    var totalPriceVND = data[9];
                                    var totalPriceVNDstr = formatThousands(totalPriceVND, 0);
                                    var listID = data[10];
                                    if (status == 1) {
                                        $("#<%=hdfListID.ClientID%>").val(listID);
                                        var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                                        var html = "";
                                        html += "<div class=\"popup-row\">";
                                        html += "   <p>Tổng số mã xuất kho của qúy khách  : <strong>" + totalCount
                                            + "</strong></p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Tổng số kg xuất kho: " + totalWeight
                                            + " kg. Quy đổi tệ: <strong>¥" + totalWeightPriceCYN
                                            + "</strong> - Quy đổi VNĐ: <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Phí hải quan xuất kho cố định: " + feeOutStockCYN
                                            + " tệ. Quy đổi VNĐ: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";

                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                        var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                        var lists = s.split('|');
                                        if (lists.length - 1 > 0) {
                                            html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                            html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                            for (var i = 0; i < lists.length - 1; i++) {
                                                var item = lists[i].split(':');
                                                var sID = item[0];
                                                var sName = item[1];
                                                html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                            }
                                            html += "</select>";
                                        }
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                        html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                                        html += "</div>";
                                        showPopup("Thanh toán xuất kho", html, button);
                                    }
                                    else {
                                        var leftmoney = data[11];
                                        var leftmoneystr = formatThousands(leftmoney, 0);
                                        var button = "";
                                        var html = "";
                                        html += "<div class=\"popup-row\">";
                                        html += "   <p>Tổng số mã xuất kho của qúy khách  : <strong>" + totalCount
                                            + "</strong></p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Tổng số kg xuất kho: " + totalWeight
                                            + " kg. Quy đổi tệ: <strong>¥" + totalWeightPriceCYN
                                            + "</strong> - Quy đổi VNĐ: <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Phí hải quan xuất kho cố định: " + feeOutStockCYN
                                            + " tệ. Quy đổi VNĐ: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";

                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        //html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ.</p>";
                                        html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<p>Quý khách còn thiếu <strong>" + leftmoneystr + "</strong> VNĐ để xuất kho thành công.</p>";
                                        html += "</div>";
                                        html += "<div class=\"popup-row\">";
                                        html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                                        html += "</div>";
                                        showPopup("Thanh toán xuất kho", html, button);
                                    }
                                }
                            }
                            else {
                                alert('Hiện tại không đơn thích hợp để yêu cầu xuất kho.');
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
                else {
                    alert('Vui lòng chọn kiện bạn muốn yêu cầu xuất kho');
                }
            }
            function requestoutstockAll() {
                $.ajax({
                    type: "POST",
                    url: "/quan-ly-van-don-vch.aspx/exportAll",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret != "0") {
                            var c = confirm('Bạn muốn xuất kho tất cả các kiện?');
                            if (c == true) {
                                var ret = msg.d;
                                var data = ret.split(':');
                                var status = data[0];
                                var wallet = data[1];
                                var walletstr = formatThousands(wallet, 0);
                                var totalCount = data[2];
                                var totalWeight = data[3];
                                var totalWeightPriceCYN = data[4];
                                var totalWeightPriceVND = data[5];
                                var totalWeightPriceVNDstr = formatThousands(totalWeightPriceVND, 0);
                                var feeOutStockCYN = data[6]
                                var feeOutStockVND = data[7];
                                var feeOutStockVNDstr = formatThousands(feeOutStockVND, 0);
                                var totalPriceCYN = data[8];
                                var totalPriceVND = data[9];
                                var totalPriceVNDstr = formatThousands(totalPriceVND, 0);
                                var listID = data[10];
                                var totalAdditionFeeCYN = data[12];
                                var totalAdditionFeeVND = data[13];
                                var totalAdditionFeeVNDstr = formatThousands(totalAdditionFeeVND, 0);
                                if (status == 1) {
                                    $("#<%=hdfListID.ClientID%>").val(listID);
                                    var button = "<a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick=\"paytoexport($(this))\">Thanh toán</a>";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    html += "   <p>Tổng số mã xuất kho của qúy khách  : <strong>" + totalCount
                                        + "</strong></p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số kg xuất kho: " + totalWeight
                                        + " kg. Quy đổi tệ: <strong>¥" + totalWeightPriceCYN
                                        + "</strong> - Quy đổi VNĐ: <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Phí hải quan xuất kho cố định: " + feeOutStockCYN
                                        + " tệ. Quy đổi VNĐ: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Cước vật tư: " + totalAdditionFeeCYN
                                        + " tệ. Quy đổi VNĐ: <strong>" + totalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Hình thức vận chuyển trong nước:</span>";
                                    var s = $("#<%=hdfListShippingVN.ClientID%>").val();
                                    var lists = s.split('|');
                                    if (lists.length - 1 > 0) {
                                        html += "<select class=\"form-control popuprow-right shippingtypevn\">";
                                        html += "   <option value=\"0\">---Chọn phương thức vận chuyển---</option>";
                                        for (var i = 0; i < lists.length - 1; i++) {
                                            var item = lists[i].split(':');
                                            var sID = item[0];
                                            var sName = item[1];
                                            html += "<option value=\"" + sID + "\">" + sName + "</option>";
                                        }
                                        html += "</select>";
                                    }
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuprow-left\">Ghi chú:</span>";
                                    html += "<input class=\"form-control requestnote popuprow-right\" placeholder=\"Ghi chú\"/>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\"></span>";
                                    html += "</div>";
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                                else {
                                    var leftmoney = data[11];
                                    var leftmoneystr = formatThousands(leftmoney, 0);
                                    var button = "";
                                    var html = "";
                                    html += "<div class=\"popup-row\">";
                                    html += "   <p>Tổng số mã xuất kho của qúy khách  : <strong>" + totalCount
                                        + "</strong></p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số kg xuất kho: " + totalWeight
                                        + " kg. Quy đổi tệ: <strong>¥" + totalWeightPriceCYN
                                        + "</strong> - Quy đổi VNĐ: <strong>" + totalWeightPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Phí hải quan xuất kho cố định: " + feeOutStockCYN
                                        + " tệ. Quy đổi VNĐ: <strong>" + feeOutStockVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Cước vật tư: " + totalAdditionFeeCYN
                                        + " tệ. Quy đổi VNĐ: <strong>" + totalAdditionFeeVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Tổng số tiền xuất kho của quý khách: <strong>" + totalPriceVNDstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    //html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ. Bạn còn thiếu <strong>" + strrechargeMore + "</strong> VNĐ.</p>";
                                    html += "<p>Số tiền trong tài khoản của quý khách : <strong>" + walletstr + "</strong> VNĐ.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<p>Quý khách còn thiếu <strong>" + leftmoneystr + "</strong> VNĐ để xuất kho thành công.</p>";
                                    html += "</div>";
                                    html += "<div class=\"popup-row\">";
                                    html += "<span class=\"popuperror\" style=\"color:red\">Để nạp thêm tiền vào tài khoản, vui lòng truy cập <a href=\"/chuyen-muc/huong-dan/nap-tien-vao-tai-khoan\" target=\"_blank\"><strong style=\"font-size:20px;\">TẠI ĐÂY</strong></a>.</span>";
                                    html += "</div>";
                                    showPopup("Thanh toán xuất kho", html, button);
                                }
                            }
                        }
                        else {
                            alert('Hiện tại không đơn thích hợp để yêu cầu xuất kho.');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }

        </script>
        <script type="text/javascript">
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
                    $('form').css('overflow', 'auto');
                }, 500);
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
                fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                fr += "</div>";
                fr += "     <div class=\"changeavatar\">";
                fr += "         <div class=\"content1\">";
                fr += content;
                fr += "         </div>";
                fr += "         <div class=\"content2\">";
                fr += "             <a href=\"javascript:;\" class=\"btn btn-close pill-btn primary-btn main-btn hover\" onclick='close_popup_ms()'>Đóng</a>";
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
            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r +
                  (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };
        </script>
    </telerik:RadScriptBlock>
    <style>
        .vermid-tecenter {
            vertical-align: middle !important;
            text-align: center;
        }

        .popup-row {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
        }

        .popuprow-left {
            float: left;
            width: 30%;
        }

        .popuprow-right {
            float: left;
            width: 60%;
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

        .ordercodes {
            width: 100%;
        }

        .ordercode {
            float: left;
            width: 100%;
            clear: both;
            margin-bottom: 10px;
        }

            .ordercode .item-element {
                float: left;
                width: 33%;
                padding: 0 10px;
            }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
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

        .spackage-row {
            float: left;
            width: 100%;
        }
    </style>
</asp:Content>
