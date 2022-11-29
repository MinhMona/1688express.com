<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-hang.aspx.cs" Inherits="NHST.danh_sach_don_hang1" %>

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
                <h4 class="sec__title center-txt">Danh sách đơn hàng</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="content-text">
                            <div id="primary" class="page orders-list">
                                <div class="container">
                                    <asp:Literal ID="ltrSumOrder" runat="server"></asp:Literal>
                                    <ul class="statistics black">
                                        <li>Tổng số đơn hàng: <b class="m-color">
                                            <asp:Literal ID="ltrAllOrderCount" runat="server"></asp:Literal></b>
                                        </li>
                                        <li>Tổng trị giá: <b class="m-color">
                                            <asp:Literal ID="ltrAllOrderPrice" runat="server"></asp:Literal></b> VNĐ 
                                        </li>
                                        <li>Số tiền để lấy hàng trong kho: <b class="m-color">
                                            <asp:Literal ID="ltrTotalGetAllProduct" runat="server"></asp:Literal></b> VNĐ 
                                        </li>
                                    </ul>
                                    <table class="stat-detail black table-custom">
                                        <tr>
                                            <th rowspan="4">Trong đó:</th>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus0" runat="server"></asp:Literal></b> đơn hàng chưa đặt cọc.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus2" runat="server"></asp:Literal></b> đơn hàng đã đặt cọc.</td>
                                        </tr>
                                        <tr>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus5" runat="server"></asp:Literal></b> đơn hàng đã đặt hàng.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus6" runat="server"></asp:Literal></b> đơn hàng đã có hàng tại TQ.</td>
                                        </tr>

                                        <tr>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus7" runat="server"></asp:Literal></b> đơn hàng đã có hàng tại VN.</td>
                                            <td><b class="m-color">
                                                <asp:Literal ID="ltrOrderStatus10" runat="server"></asp:Literal></b> đơn hàng đã nhận hàng.</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="container">
                                    <table class="tbl-subtotal">
                                        <tbody>
                                            <tr class="black b">
                                                <th rowspan="7" valign="top">Thông tin tiền hàng:</th>
                                                <td>Tổng tiền hàng chưa giao:	</td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangchuagiao" runat="server"></asp:Literal>
                                                    VNĐ </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng cần đặt cọc: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangcandatcoc" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng chờ về kho TQ: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangchovekhotq" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng đã về kho TQ: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangdavekhotq" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng đang ở kho đích : </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangdangokhovn" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>Tổng tiền hàng cần thanh toán để lấy hàng đã nhận về kho đích: </td>
                                                <td>
                                                    <asp:Literal ID="ltrTongtienhangcanthanhtoandelayhang" runat="server"></asp:Literal>
                                                    VNĐ</td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
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
                                                    <asp:ListItem Value="0" Text="Chưa đặt cọc"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Hủy đơn hàng"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Đã đặt cọc"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Đã mua hàng"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Đang chuyển về kho đích"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="Đã nhận hàng tại kho đích"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="Đã hoàn thành"></asp:ListItem>
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
                                    <asp:Button ID="bttnAll" runat="server" CssClass="order-status bg-yellow" OnClick="bttnAll_Click" />
                                    <asp:Button ID="btn0" runat="server" CssClass="order-status bg-red" OnClick="btn0_Click" />
                                    <asp:Button ID="btn1" runat="server" CssClass="order-status bg-black" OnClick="btn1_Click" />
                                    <asp:Button ID="btn2" runat="server" CssClass="order-status bg-bronze" OnClick="btn2_Click" />
                                    <asp:Button ID="btn5" runat="server" CssClass="order-status bg-green" OnClick="btn5_Click" />
                                    <asp:Button ID="btn6" runat="server" CssClass="order-status bg-green" OnClick="btn6_Click" />
                                    <asp:Button ID="btn7" runat="server" CssClass="order-status bg-orange" OnClick="btn7_Click" />
                                    <asp:Button ID="btn9" runat="server" CssClass="order-status bg-blue" OnClick="btn9_Click" />
                                    <asp:Button ID="btn10" runat="server" CssClass="order-status bg-blue" OnClick="btn10_Click" />
                                    <%-- <ul class="status-list clear">
                                    <li><a href="/danh-sach-don-hang?status=-1">Tất cả</a></li>
                                    <li><a href="/danh-sach-don-hang?status=1">Chưa đặt cọc</a></li>
                                    <li><a href="/danh-sach-don-hang?status=2">Đã xác nhận </a></li>
                                    <li><a href="/danh-sach-don-hang?status=3">Đang đặt hàng</a></li>
                                    <li><a href="/danh-sach-don-hang?status=4">Đã đặt hàng </a></li>
                                    <li><a href="/danh-sach-don-hang?status=5">Đã có hàng tại VN</a></li>
                                    <li><a href="/danh-sach-don-hang?status=6">Đã thanh toán</a></li>
                                    <li><a href="/danh-sach-don-hang?status=7">Đã giao hàng</a></li>
                                    <li><a href="/danh-sach-don-hang?status=0">Đã hủy</a></li>
                                </ul>--%>
                                    <p style="margin: 10px 0; display: none;"><a class="btn btn-success btn-block pill-btn primary-btn main-btn hover" onclick="requestall();" href="javascript:;">Yêu cầu giao hàng</a></p>
                                    <asp:Button ID="btnAllrequest" runat="server" Style="display: none" OnClick="btnAllrequest_Click" UseSubmitBehavior="false" />
                                </div>
                                <div class="container">
                                    <a style="clear: both; float: right; padding: 5px 40px; margin-bottom: 20px; display: none"
                                        id="btn-deposit-select" class="btn btn-success btn-block pill-btn primary-btn main-btn hover"
                                        onclick="depositOrderSelect();" href="javascript:;">Đặt cọc <span class="ordercountdeposit"></span>

                                    </a>
                                </div>
                            </div>
                            <div class="table-panel">
                                <div class="mobile-show-scroll">
                                    <strong>Lưu ý: </strong>trượt ngang bảng bên dưới để có thể xem hết thông tin
                                </div>
                                <div class="table-panel-main full-width">
                                    <%--<telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                    AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                    AllowSorting="True" AllowFilteringByColumn="false">
                                    <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                                CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Ảnh sản phẩm" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <img src="<%#Eval("ProductImage") %>" width="100%" alt />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên Shop" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Site" HeaderText="Website" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Số Tiền phải cọc" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("AmountDeposit"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Ngày đặt hàng" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                                CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="statusstring" HeaderText="Trạng thái đơn hàng" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                                <ItemTemplate>
                                                    <a href="/chi-tiet-don-hang/<%#Eval("ID") %>" class="viewmore-orderlist">Chi tiết</a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                        <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                            PrevPageText="← Previous" />
                                    </MasterTableView>
                                </telerik:RadGrid>--%>
                                    <table>
                                        <tr>
                                            <th class="id" style="width: 1%;"></th>
                                            <th class="id" style="width: 1%;">ID</th>
                                            <th class="pro" style="width: 1%;">Ảnh sản phẩm</th>
                                            <th class="pro" style="width: 5%;">Tên Shop</th>
                                            <th class="qty" style="width: 5%;">Website</th>
                                            <th class="price" style="width: 5%;">Tổng tiền</th>
                                            <th class="price" style="width: 5%;">Số Tiền phải cọc</th>
                                            <th class="price" style="width: 5%;">Tiền đã cọc</th>
                                            <th class="date" style="width: 5%;">Ngày đặt hàng</th>
                                            <th class="date" style="width: 5%;">Ngày đặt cọc</th>
                                            <th class="status" style="width: 5%;">Trạng thái đơn hàng</th>
                                            <%--<th class="status" style="width: 5%;">Giao hàng</th>--%>
                                            <th class="status" style="width: 5%;"></th>
                                        </tr>
                                        <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                        <%--<asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="id"><%#Eval("ID") %></td>
                                                <td class="pro">
                                                    <img src='<%# PJUtils.GetFirstProductIMG(Eval("ID").ToString()) %>' width="100%" alt="" />
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("ShopName") %>
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("Site") %>
                                                </td>

                                                <td class="price">
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> đ</p>
                                                </td>
                                                <td class="price">
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("AmountDeposit"))) %> đ</p>
                                                </td>
                                                <td class="price">
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> đ</p>
                                                </td>
                                                <td class="date"><%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %></td>
                                                <td class="status"><%# NHST.Bussiness.PJUtils.IntToRequestClient(Convert.ToInt32(Eval("Status"))) %></td>
                                                <td class="status"><a href="/chi-tiet-don-hang/<%#Eval("ID") %>" class="viewmore">Xem chi tiết</a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>--%>
                                    </table>
                                    <div class="pagination">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Danh sách đơn hàng</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
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
    </style>
    <asp:HiddenField ID="hdfListOrder" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:HiddenField ID="hdfListOrderID" runat="server" />
    <asp:Button ID="btnDeposit" runat="server" OnClick="btnDeposit_Click" Style="display: none" />
    <asp:Button ID="btnDepositAll" runat="server" OnClick="btnDepositAll_Click" Style="display: none" />
    <%--<telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>    --%>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function depositOrderSelect() {
                var ishascheck = 0;
                var orderidlist = "";
                $(".chk-deposit").each(function () {
                    if ($(this).is(":checked")) {
                        ishascheck += 1;
                        orderidlist += $(this).attr("data-id") + "|";
                    }
                });
                if (ishascheck > 0) {
                    var c = confirm("Bạn muốn đặt cọc các đơn đã chọn?");
                    if (c) {
                        $("#<%= hdfListOrderID.ClientID%>").val(orderidlist);
                        $("#<%= btnDepositAll.ClientID%>").click();
                    }
                }
                else {
                    alert('Vui lòng chọn đơn hàng cần đặt cọc.');
                }
            }
            function selectdeposit() {
                var ishascheck = 0;
                $(".chk-deposit").each(function () {
                    if ($(this).is(":checked")) {
                        ishascheck += 1;
                    }
                });
                if (ishascheck > 0) {
                    $("#btn-deposit-select").show();
                    $(".ordercountdeposit").html(ishascheck + " đơn hàng");
                }
                else {
                    $("#btn-deposit-select").hide();
                    $(".ordercountdeposit").html("");
                }
            }
            function show_messageorder(content) {
                var obj = $('body');
                $(obj).css('overflow', 'hidden');
                $(obj).attr('onkeydown', 'keyclose_ms(event)');
                var bg = "<div id='bg_popup'></div>";
                var fr = "<div id='pupip' class=\"columns-container1\"><div class=\"container\" id=\"columns\"><div class='row'>" +
                         "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content\"><a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                fr += "     <div class=\"changeavatar\">";
                fr += content;
                fr += "         <label class=\"lbl\">Nhập địa chỉ giao hàng: </label>";
                fr += "         <div class=\"clearfix\"></div>";
                fr += "         <input class=\"addresscustomer\" type=\"text\" class=\"\" style=\"\margin:10px 0;\"/>";
                fr += "         <div class=\"clearfix\"></div>";
                fr += "         <a class=\"submit-btn\" onclick=\"acceptgiao()\" style=\"float:right;\">Xác nhận</a>";
                fr += "     </div>";
                fr += "   </div>";

                fr += "</div></div></div>";
                $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                $(fr).appendTo($(obj));
                setTimeout(function () {
                    $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                    $("#bg_popup").attr("onclick", "close_popup_ms()");
                }, 1000);
            }
            function keyclose_ms(e) {
                if (e.keyCode == 27) {
                    close_popup_ms();
                }
            }
            function close_popup_ms() {                
                $("#pupip").animate({ "opacity": 0 }, 400);
                $("#bg_popup").animate({ "opacity": 0 }, 400);
                setTimeout(function () {
                    $("#pupip").remove();
                    $(".zoomContainer").remove();
                    $("#bg_popup").remove();
                    $('body').css('overflow', 'auto').attr('onkeydown', '');
                    $('form').css('overflow', 'auto');
                }, 500);
            }
            function requestall() {
                var listOrder = '';
                var count = 0;
                $(".ycgh-chk").each(function () {
                    var status = $(this).attr("data-status");
                    var status1 = $(this).attr("data-sta");
                    var id = $(this).attr("data-id");
                    if (status == 9) {
                        if (status1 == 0) {
                            if ($(this).is(":checked")) {
                                listOrder += id + "|";
                                count++;
                            }
                        }
                    }
                });
                if (count > 0) {
                    var r = confirm("Bạn muốn yêu cầu giao " + count + " đơn hàng đã chọn!");
                    if (r == true) {
                        $("#<%=hdfListOrder.ClientID%>").val(listOrder);
                        $("#<%=hdfAmount.ClientID%>").val(count);
                        $("#<%=btnAllrequest.ClientID%>").click();
                    } else {
                    }
                }
                else {
                    alert("Vui lòng chọn đơn hàng mà bạn muốn giao.");
                }
            }
            function depositOrder(orderID) {
                var c = confirm('Bạn muốn đặt cọc đơn: ' + orderID);
                if (c == true) {
                    $("#<%=hdfOrderID.ClientID%>").val(orderID);
                    $("#<%=btnDeposit.ClientID%>").click();
                }
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
                //var button = "<a href=\"/danh-sach-don-hang\" class=\"btn btn-close pill-btn primary-btn main-btn hover\">Đơn hàng của bạn</a>";
                var button = "<a href=\"/danh-sach-don-hang\" class=\"btn btn-close pill-btn primary-btn main-btn hover\">Đóng</a>";
                showPopup("Đặt hàng thành công", text, button);
            }
            function showPopup(title, content, button) {
                var obj = $('form');
                $(obj).css('overflow', 'hidden');
                $(obj).attr('onkeydown', 'keyclose_ms(event)');
                var bg = "<div id='bg_popup'></div>";
                var fr = "<div id='pupip' class=\"columns-container1\">" +
                         "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\"><a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
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
        </script>
    </telerik:RadScriptBlock>
    <style>
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
</asp:Content>
