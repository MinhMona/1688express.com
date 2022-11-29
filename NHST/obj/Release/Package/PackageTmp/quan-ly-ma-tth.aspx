<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="quan-ly-ma-tth.aspx.cs" Inherits="NHST.quan_ly_ma_tth" %>

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

        .trans.table tr td {
            text-align: center;
        }

        select.form-control {
            -webkit-appearance: none;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 40px;
            background:#fff url('/App_Themes/1688/images/icon-select.png') right 15px center no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Quản lý mã thanh toán hộ</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="content-text">
                            <div id="primary" class="page orders-list">
                                <div class="container">
                                    <aside class="filters">
                                        <ul>
                                            <li class="lbl" style="width: 100%; text-align: left; margin-bottom: 10px;">Tìm kiếm</li>
                                            <li style="width: 35%; margin-bottom: 10px;">
                                                <asp:TextBox ID="txtOrderCode" runat="server" CssClass="form-control" placeholder="Nhập mã yêu cầu hoặc số tiền để tìm"></asp:TextBox>
                                            </li>
                                            <li style="width: 35%; margin-bottom: 10px;">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Tìm theo Mã yêu cầu"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Tìm theo số tiền yêu cầu"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="width: 20%; margin-bottom: 10px;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Tất cả trạng thái"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Chờ thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Đã thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Đã hủy"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Đã hoàn tiền"></asp:ListItem>
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
                            </div>
                        </div>
                        <div class="primary-form custom-width">
                            <div class="container">
                                <div class="main-content policy clear">
                                    <div class="form-confirm-send">
                                        <div class="table-panel-main full-width">
                                            <table class="trans table">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Ngày gửi</th>
                                                        <th>Tổng tiền (¥)</th>
                                                        <th>Tổng tiền (VNĐ)</th>
                                                        <th>Ghi chú</th>
                                                        <th>NV Ghi chú</th>
                                                        <th>Trạng thái</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="tbl-footer clear">
                                            <div class="pagenavi fl">
                                                <%this.DisplayHtmlStringPaging1();%>
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
    </main>
</asp:Content>
