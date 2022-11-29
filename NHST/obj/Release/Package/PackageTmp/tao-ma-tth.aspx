<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="tao-ma-tth.aspx.cs" Inherits="NHST.tao_ma_tth" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .row-item {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
            padding: 0px 10%;
        }

        .row-left {
            float: left;
            width: 15%;
        }

        .row-right {
            float: left;
            width: 82%;
            margin-left: 20px;
        }

        .rowfull {
            float: left;
            margin-right: 20px;
            width: 100%;
            text-transform: uppercase;
            margin: 5px 0;
        }

        .left-rowfull {
            float: left;
            width: 20%;
            margin-right: 10px;
        }

        .right-rowfull {
            float: left;
            width: 75%;
        }

        .form-control {
            float: left;
            width: 100%;
        }

        textarea.form-control {
            min-height: 100px;
        }

        .text-align-center {
            text-align: center;
        }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url(/App_Themes/NewUI/images/icon-plus.png) center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .float-right {
            float: right;
        }

        .border-ra-5px {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
        }

        .width-custom {
            width: 850px;
            margin: 0 auto;
            float: none;
        }

        .border-top {
            border-top: solid 1px #333;
            padding-top: 10px;
        }

        .border-bottom {
            border-bottom: solid 1px #333;
            padding-bottom: 10px;
        }

        .itemaddmore {
            float: left;
            width: 100%;
            clear: both;
        }

        .border-ra-bg {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            border: solid 1px #ddd;
            background: #fdfdfd;
            padding: 10px;
        }

        .lblstrongcolor {
            color: #2178bf;
        }

        .custom-border-padding {
            border-top: solid 1px #ccc;
            padding-top: 30px;
        }

        .rowhalf {
            float: left;
            width: 45%;
        }

        .label-field {
            float: left;
            width: 35%;
        }

        .textbox-field {
            float: left;
            width: 60%;
        }

        .rowpart {
            float: left;
            width: 10%;
        }

        .nomin-width {
            min-width: unset !important;
            width: 100%;
            padding: 0 !important;
        }

        .form-confirm-send {
            float: left;
            width: 100%;
            margin: 50px 0;
        }

        .table-panel-main table td {
            text-align: center;
        }

        .btn.pill-btn {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Gửi yêu cầu thanh toán hộ</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <div class="container">
                                <div class="main-content policy clear">
                                    <div class="form-confirm-send width-custom">
                                        <div class="form-group">
                                            <%--<div class="row-item">
                                                <p class="red" style="color: red; font-weight: bold; font-size: 15px;">
                                                    Lưu ý:<br />
                                                    Yêu cầu không đúng thực tế, yêu cầu sẽ bị huỷ và hoàn tiền.
                                                    <br />
                                                    Yêu cầu sẽ tính theo tỉ giá lúc thực hiện giao dịch.
                                                </p>
                                            </div>--%>

                                            <div class="row-item border-bottom">
                                                <div class="row-left">Tên đăng nhập:</div>
                                                <div class="row-right">
                                                    <strong class="lblstrongcolor">
                                                        <asp:Literal ID="ltrIfn" runat="server"></asp:Literal></strong>
                                                    <a href="javascript:;" onclick="addMoreRequest()" class="pill-btn btn order-btn main-btn hover submit-btn float-right border-ra-5px">+ Thêm đơn</a>
                                                </div>
                                            </div>
                                            <%--<div class="row-item">
                                                <div class="ordercode addordercode"></div>
                                                <a href="javascript:;" onclick="addMoreRequest()" class="submit-btn">Thêm yêu cầu</a>
                                            </div>--%>
                                            <div class="itemaddmore border-bottom">
                                                <div class="itemyeuau row-item">
                                                    <div class="rowhalf">
                                                        <div class="label-field">Giá tiền (Tệ):</div>
                                                        <div class="textbox-field">
                                                            <input class="txtDesc2 form-control border-ra-bg" oninput="sumtotalprice()" value="0" />
                                                        </div>
                                                    </div>
                                                    <div class="rowhalf">
                                                        <div class="label-field">Nội dung:</div>
                                                        <div class="textbox-field">
                                                            <input class="txtDesc1 form-control border-ra-bg" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row-item text-align-center">
                                                <asp:Button ID="btnSend" runat="server" Text="GỬI" CssClass="submit-btn " OnClick="btnSend_Click" Style="display: none" />
                                                <a href="javascript:;" class="pill-btn btn order-btn main-btn hover submit-btn border-ra-5px" onclick="sendRequest()">Gửi yêu cầu</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:HiddenField ID="hdfTradeID" runat="server" />
        <asp:HiddenField ID="hdflist" runat="server" />
        <asp:HiddenField ID="hdfAmount" runat="server" />
    </main>
    <script type="text/javascript">
        function addMoreRequest() {
            var html = "";
            html += "<div class=\"itemyeuau row-item custom-border-padding\">";
            html += "<div class=\"rowhalf \"> <div class=\"label-field\">Giá tiền (Tệ):</div><div class=\"textbox-field\"><input class=\"txtDesc2 form-control border-ra-bg\" oninput=\"sumtotalprice()\" value=\"0\"/></div></div>";
            html += "<div class=\"rowhalf\"> <div class=\"label-field\">Nội dung:</div><div class=\"textbox-field\"><input class=\"txtDesc1 form-control border-ra-bg\"/></div></div>";
            html += "<div class=\"rowpart\"> <a href=\"javascript:;\" class=\"pill-btn btn order-btn main-btn hover submit-btn float-right border-ra-5px nomin-width\" onclick=\"deleteitem($(this))\">Xóa</a></div>";
            html += "</div>";
            $(".itemaddmore").append(html);
        }

        function deleteitem(obj) {
            obj.parent().parent().remove();
            sumtotalprice();
        }

        function sendRequest() {
            var html = "";
            $(".itemyeuau").each(function () {
                var money = $(this).find(".txtDesc2").val();
                var note = $(this).find(".txtDesc1").val();
                html += money + ":" + note + "|";
            });
            $("#<%=hdflist.ClientID%>").val(html);
            $("#<%=btnSend.ClientID%>").click();
        }

        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }
        var formatThousands = function (n, dp) {
            var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
            while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
            return s.substr(0, i + 3) + r +
              (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
        };
    </script>
</asp:Content>
