<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="NHST.Admin.OrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <link rel="stylesheet" href="/App_Themes/NHST/css/style.css" media="all">
    <link rel="stylesheet" href="/App_Themes/NHST/css/responsive.css" media="all">
    <link href="/App_Themes/NewUI/js/select2/select2.css" rel="stylesheet" />
    <link rel="stylesheet" href="/App_Themes/NHST/css/style-custom.css" media="all">
    <style>
        .sec {
            padding: 20px 0;
        }

        .main {
            width: 91%;
        }

        dl dd {
            display: block;
            padding-left: 0px;
            float: left;
            width: 50%;
            margin-bottom: 15px;
        }

        select.form-control {
            line-height: 25px;
        }

        .riSingle {
            width: 40% !important;
        }

        .admin-btn {
            float: right;
            clear: both;
            margin-top: 10px;
            line-height: 30px;
        }

        .order-panel dl dt {
            width: 40%;
            clear: both;
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
                width: 20%;
                padding: 0 10px;
            }

            .ordercode.smallpackage-item .item-element {
                float: left;
                width: 25%;
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

        .title-fee {
            float: left;
            width: 100%;
            border-bottom: solid 1px #ccc;
            font-size: 20px;
            margin: 20px 0;
            color: #000;
        }

        .bg-red-nhst {
            background: #ea2028;
            color: #fff;
        }

            .bg-red-nhst dt, .bg-red-nhst .title {
                color: #fff;
            }

        .order-panel .title {
            border-bottom: solid 1px #fff;
        }

        .addloading {
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: hidden;
            background: #fff url('/App_Themes/NewUI/images/loading.gif') center center no-repeat;
            z-index: 999999;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            opacity: 0.8;
        }

        .vermid-tecenter {
            vertical-align: middle !important;
            text-align: center;
        }
    </style>
    <%--<asp:Panel ID="pEdit" runat="server">--%>
    <div class="row">
        <div class="col-md-12">
            <div id="content-order-detail" class="panel panel-white" style="position: relative;">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase" style="padding: 30px;">Chi tiết đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <main id="main-wrap">
                            <div class="sec order-detail-sec">
                                <div class="all">
                                    <div class="main">
                                        <div class="order-panels mar-bot2">
                                            <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn">In đơn hàng</a>
                                            <asp:Literal ID="ltrPrint" runat="server"></asp:Literal>
                                        </div>
                                        <asp:Panel ID="pnordertype23" runat="server">
                                            <div class="order-panels">
                                                <div class="order-panel">
                                                    <div class="title">Mã đơn hàng</div>
                                                    <div class="cont">
                                                        <div class="item-element">
                                                            <span>Mã đơn hàng</span>
                                                            <asp:TextBox ID="txtMainOrderCode" runat="server" CssClass="form-control"
                                                                placeholder="Mã đơn hàng" onchange="loadOrderWebCode()"></asp:TextBox>
                                                            <a href="javascript:;" class="btn-not-radius" onclick="addOrderCode1()">Thêm</a>
                                                        </div>
                                                        <div class="item-element containlistcode" style="margin-top: 10px; float: left; clear: both; display: none">
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <td>Mã đơn hàng
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                </thead>
                                                                <tbody class="listordercodeadd">
                                                                    <asp:Literal ID="ltrListCodeadd" runat="server"></asp:Literal>
                                                                    <%--<tr class="ordercodemain" data-code="4123412343214">
                                                                    <td>4123412343214
                                                                    </td>
                                                                    <td>
                                                                        <a href="javascript:;" class="btn-not-radius" onclick="deleteordercode()">Xóa</a>
                                                                    </td>
                                                                </tr>--%>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-panel " style="margin-left: 0;">
                                                <div class="title">Danh sách mã vận đơn</div>
                                                <div id="transactioncodeList" class="cont">
                                                    <asp:Literal ID="ltrCodeList" runat="server"></asp:Literal>
                                                </div>
                                                <div class="ordercode addordercode"><a href="javascript:;" onclick="addCodeTransaction()">Thêm mã vận đơn</a></div>
                                            </div>
                                            <div class="order-panel " style="margin-left: 0;">
                                                <div class="title">Danh sách sản phẩm</div>
                                                <div class="cont" style="overflow-x: scroll">
                                                    <table class="tb-product">
                                                        <tr>
                                                            <th class="pro">Shop ID</th>
                                                            <th class="pro">Tên Shop</th>
                                                            <th class="pro">ID</th>
                                                            <th class="pro">Sản phẩm</th>
                                                            <th class="pro">Thuộc tính</th>
                                                            <th class="qty">Số lượng</th>
                                                            <th class="price">Đơn giá</th>
                                                            <th class="price">Giá sản phẩm CNY</th>
                                                            <%--<th class="price">Giá mua thực tế</th>
                                                            <th class="price">Hoa hồng đặt hàng</th>--%>
                                                            <th class="price">Ghi chú riêng sản phẩm</th>
                                                            <th class="price">Trạng thái</th>
                                                            <th class="tool"></th>
                                                        </tr>
                                                        <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnOrderType1" runat="server">
                                            <div class="order-panel">
                                                <div class="title">
                                                    ID đơn hàng:
                                                    <asp:Literal ID="ltrOrderID" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="order-panel" style="margin: 0; margin-bottom: 30px;">
                                                <div class="title">Danh sách sản phẩm</div>
                                                <asp:Literal ID="ltrProductOrderShopCode" runat="server"></asp:Literal>
                                                <%--<table class="tb-product">
                                                <tr>
                                                    <th>Tên Shop</th>
                                                    <th>Shop ID</th>
                                                    <th>Sản phẩm</th>
                                                    <th>Mã đơn hàng</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%;" class="middle-center">Shop 1</td>
                                                    <td style="width: 5%;" class="middle-center">taobao_1243213213</td>
                                                    <td style="width: 40%">
                                                        <table class="tb-product">
                                                            <tbody>
                                                                <tr>
                                                                    <th class="pro">ID</th>
                                                                    <th class="pro">Sản phẩm</th>
                                                                    <th class="pro">Thuộc tính</th>
                                                                    <th class="qty">Số lượng</th>
                                                                    <th class="price">Đơn giá</th>
                                                                    <th class="price">Giá sản phẩm CNY</th>

                                                                    <th class="price">Ghi chú riêng sản phẩm</th>
                                                                    <th class="price">Trạng thái</th>
                                                                    <th class="tool"></th>
                                                                </tr>
                                                                <tr>
                                                                    <td class="pro">1</td>
                                                                    <td class="pro">
                                                                        <div class="thumb-product">
                                                                            <div class="pd-img">
                                                                                <a href="https://item.taobao.com/item.htm?spm=a230r.1.998.26.3fc7523cm8PgVE&amp;scm=1007.11224.62891.0&amp;id=18217219693&amp;pvid=a8528844-645a-4f77-8b71-fab89cec4d1e" target="_blank">
                                                                                    <img src="https://gd1.alicdn.com/imgextra/i2/1093295465/TB2lcutswRkpuFjy1zeXXc.6FXa_!!1093295465.jpg_150x150.jpg" alt=""></a>
                                                                            </div>
                                                                            <div class="info"><a href="https://item.taobao.com/item.htm?spm=a230r.1.998.26.3fc7523cm8PgVE&amp;scm=1007.11224.62891.0&amp;id=18217219693&amp;pvid=a8528844-645a-4f77-8b71-fab89cec4d1e" target="_blank">智能无线红外探测器店铺家用防盗报警器抗误报无线广角红外探头</a></div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="pro">普通无线红外;</td>
                                                                    <td class="qty">2</td>
                                                                    <td class="price">
                                                                        <p class="">70,015 vnđ</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="">¥19</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="" style="color: orange; font-weight: bold"></p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="">Còn hàng</p>
                                                                    </td>
                                                                    <td class="price"><a class="btn btn-info btn-sm" href="/Admin/ProductEdit.aspx?id=69">Sửa</a></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="pro">2</td>
                                                                    <td class="pro">
                                                                        <div class="thumb-product">
                                                                            <div class="pd-img">
                                                                                <a href="https://item.taobao.com/item.htm?id=542941873605&amp;ali_refid=a3_420841_1006:1122356781:N:%E6%84%9F%E5%BA%94%E7%81%AF:97359c1d25283e03648980ee3ad6fff1&amp;ali_trackid=1_97359c1d25283e03648980ee3ad6fff1&amp;spm=a231k.8165028.0782702702.16" target="_blank">
                                                                                    <img src="https://gd2.alicdn.com/imgextra/i3/782128582/TB2vo6yjCFjpuFjSszhXXaBuVXa_!!782128582.jpg_150x150.jpg_.webp" alt=""></a>
                                                                            </div>
                                                                            <div class="info"><a href="https://item.taobao.com/item.htm?id=542941873605&amp;ali_refid=a3_420841_1006:1122356781:N:%E6%84%9F%E5%BA%94%E7%81%AF:97359c1d25283e03648980ee3ad6fff1&amp;ali_trackid=1_97359c1d25283e03648980ee3ad6fff1&amp;spm=a231k.8165028.0782702702.16" target="_blank">LED声光控吸顶灯LED声控灯 过道走廊楼梯楼道声控感应灯LED灯包邮</a></div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="pro">声控12w-21cm 限购一个;</td>
                                                                    <td class="qty">1</td>
                                                                    <td class="price">
                                                                        <p class="">33,165 vnđ</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="">¥9</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="" style="color: orange; font-weight: bold"></p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class="">Còn hàng</p>
                                                                    </td>
                                                                    <td class="price"><a class="btn btn-info btn-sm" href="/Admin/ProductEdit.aspx?id=70">Sửa</a></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <a href="javascript:;" class="btn-not-radius" style="margin: 0; margin-bottom: 20px;">Thêm mã đơn hàng</a>
                                                        <table class="tb-product" style="width: 100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Mã đơn hàng
                                                                    </th>
                                                                    <th>Mã vận đơn
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody class="shopOrderList">
                                                                <tr class="shoporder-item" data-ordershopcode="1231231231233">
                                                                    <td>
                                                                        <a href="javascript:;" class="custom-link">1231231231233</a>
                                                                        <a href="javascript:;" class="btn-not-radius" style="clear: both; width: 100%; text-align: center">Thêm mã vận đơn</a>
                                                                    </td>
                                                                    <td>
                                                                        <a href="javascript:;" class="custom-link">1231231231233</a>
                                                                        <a href="javascript:;" class="custom-link">1231231231233</a>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                            </div>
                                        </asp:Panel>
                                        <div class="order-panels">
                                            <asp:Panel ID="pnadminmanager" runat="server" Visible="false" CssClass="full-width">
                                                <div class="order-panel">
                                                    <div class="title">Nhân viên xử lý</div>
                                                    <div class="cont">
                                                        <dl>
                                                            <dt>Nhân viên saler</dt>
                                                            <dd>
                                                                <asp:DropDownList ID="ddlSaler" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    DataTextField="Username" DataValueField="ID">
                                                                </asp:DropDownList></dd>
                                                            <dt>Nhân viên đặt hàng</dt>
                                                            <dd>
                                                                <asp:DropDownList ID="ddlDatHang" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    DataTextField="Username" DataValueField="ID">
                                                                </asp:DropDownList></dd>
                                                            <dt style="display: none">Nhân viên kho TQ</dt>
                                                            <dd style="display: none">
                                                                <asp:DropDownList ID="ddlKhoTQ" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    DataTextField="Username" DataValueField="ID">
                                                                </asp:DropDownList></dd>
                                                            <dt style="display: none">Nhân viên kho đích</dt>
                                                            <dd style="display: none">
                                                                <asp:DropDownList ID="ddlKhoVN" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    DataTextField="Username" DataValueField="ID">
                                                                </asp:DropDownList></dd>
                                                            <dd>
                                                                <asp:Button ID="btnStaffUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn" Text="CẬP NHẬT" OnClick="btnStaffUpdate_Click" /></dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="order-panels print6" style="display: none;">
                                            <asp:Literal ID="ltr_OrderCode" runat="server"></asp:Literal>
                                        </div>
                                        <div class="order-panels">
                                            <asp:Literal ID="ltr_OrderFee_UserInfo1" runat="server"></asp:Literal>
                                        </div>
                                        <div class="order-panels">
                                            <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                                        </div>
                                        <div class="order-panels">
                                            <div class="order-panel">
                                                <div class="title">Đánh giá đơn hàng</div>
                                                <ul class="list-comment">
                                                    <asp:Literal ID="ltr_comment" runat="server"></asp:Literal>
                                                </ul>
                                                <asp:Panel ID="pn_sendcomment" runat="server">
                                                    <div class="bottom comment-bottom">
                                                        <div class="comment-input">
                                                            <div class="comment-input-left">
                                                                <asp:Literal ID="ltr_currentUserImg" runat="server"></asp:Literal>
                                                            </div>
                                                            <div class="comment-input-right">
                                                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rq" runat="server" ValidationGroup="n" ControlToValidate="txtComment" ForeColor="Red" ErrorMessage="Không để trống nội dung."></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="comment-input">
                                                            <asp:DropDownList ID="ddlTypeComment" runat="server" CssClass="form-control full-width">
                                                                <asp:ListItem Value="0" Text="Chọn khu vực"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Khách hàng"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Nội bộ"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:Button ID="btnSend" runat="server" Text="Gửi đánh giá" ValidationGroup="n" CssClass="btn pill-btn primary-btn admin-btn" OnClick="btnSend_Click" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <asp:Literal ID="ltr_AddressReceive" runat="server"></asp:Literal>
                                        </div>
                                        <div class="order-panel print3" style="display: none;">
                                            <div class="title">Danh sách sản phẩm</div>
                                            <div class="cont" style="overflow-x: scroll">
                                                <table class="tb-product">
                                                    <tr>
                                                        <th class="pro">ID</th>
                                                        <th class="pro">Sản phẩm</th>
                                                        <th class="pro">Thuộc tính</th>
                                                        <th class="qty">Số lượng</th>
                                                        <th class="price">Đơn giá</th>
                                                        <th class="price">Giá sản phẩm CNY</th>
                                                        <th class="price">Ghi chú riêng sản phẩm</th>
                                                        <th class="price">Trạng thái</th>
                                                    </tr>
                                                    <asp:Literal ID="ltrProductPrint" runat="server"></asp:Literal>
                                                </table>
                                            </div>
                                        </div>


                                        <div class="order-panels">
                                            <div class="order-panel">
                                                <div class="title">Chi phí của đơn hàng</div>
                                                <div class="cont">
                                                    <dl>
                                                        <dt class="full-width"><strong class="title-fee">Phí cố định</strong></dt>
                                                        <dt>Loại đơn hàng</dt>
                                                        <dd>
                                                            <asp:Literal ID="ltrOrderType" runat="server"></asp:Literal>
                                                        </dd>
                                                        <dt>Trạng thái đơn hàng</dt>
                                                        <dd>
                                                            <asp:Literal ID="ltrOrderStatus" runat="server"></asp:Literal>
                                                        </dd>
                                                        <asp:Panel ID="pnOrderType3" runat="server" Visible="false">
                                                            <dt>Chờ báo giá</dt>
                                                            <dd>
                                                                <asp:CheckBox ID="chkIsCheckNotiPrice" runat="server" />
                                                            </dd>
                                                        </asp:Panel>
                                                        <dt>Tiền hàng trên web</dt>
                                                        <dd>
                                                            <asp:Label ID="lblTotalMoney" runat="server"></asp:Label></dd>
                                                        <dt>Tổng Số Tiền mua thật</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="rTotalPriceRealCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountRealPrice()"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Tiền mua thật CNY">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="rTotalPriceReal" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountRealPrice1()"
                                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="true">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnd</span></dd>
                                                        <dt>Phí ship Trung Quốc</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pCNShipFeeNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountShipFee('ndt')"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Tiền ship Trung Quốc CNY" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pCNShipFee" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountShipFee('vnd')"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>Tiền HH</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull"
                                                                Skin="MetroTouch"
                                                                ID="pHHCYN" NumberFormat-DecimalDigits="2" Enabled="false"
                                                                NumberFormat-GroupSizes="3" Width="10%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pHHVND" NumberFormat-DecimalDigits="0" Enabled="false"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>Phí mua hàng
                                                                <br />
                                                            (CK:
                                                                <asp:Label ID="lblCKFeebuypro" runat="server"></asp:Label>
                                                            %)</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pBuyNDT" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeeBuyPro()"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí mua hàng CNY">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pBuy" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <asp:Panel ID="pnWechatFee" runat="server" Visible="false">
                                                            <dt>Phí mua hàng wechat</dt>
                                                            <dd>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="rFeeWeChatCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeeWeChatBuyPro()" 
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí mua hàng WeChat CNY">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull"
                                                                    Skin="MetroTouch" ID="rFeeWeChatVND" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0" placeholder="Phí mua hàng WeChat VNĐ">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>
                                                        </asp:Panel>
                                                        <dt style="display: block">Tổng cân nặng</dt>
                                                        <dd style="display: block">
                                                            <asp:Literal ID="ltrTotalWeight" runat="server"></asp:Literal>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="txtOrderWeight" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="gettotalweight2()"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0" Visible="false">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">KG</span>
                                                        </dd>
                                                        <dt>Phí vận chuyển TQ-VN
                                                                <br />
                                                            (CK
                                                                <asp:Label ID="lblCKFeeWeight" runat="server"></asp:Label>% : 
                                                                <asp:Label ID="lblCKFeeweightPrice" runat="server"></asp:Label>)</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pWeightNDT" MinValue="0" NumberFormat-DecimalDigits="2"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí cân nặng CNY" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pWeightNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="gettotalweight2()"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí cân nặng CNY" Value="0">
                                                            </telerik:RadNumericTextBox>--%>
                                                            <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pWeightNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeeWeight('kg')"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí cân nặng CNY" Value="0">
                                                            </telerik:RadNumericTextBox>--%>
                                                            <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup=""
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>--%>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="gettotalweight2()"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>

                                                        <dt>Trạng thái đơn hàng</dt>
                                                        <dd>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                                <%--<asp:ListItem Value="0">Chưa đặt cọc</asp:ListItem>
                                                                    <asp:ListItem Value="1">Hủy đơn hàng</asp:ListItem>
                                                                    <asp:ListItem Value="2">Đã đặt cọc</asp:ListItem>
                                                                    <asp:ListItem Value="3">Chờ duyệt đơn</asp:ListItem>
                                                                    <asp:ListItem Value="4">Đã duyệt đơn</asp:ListItem>
                                                                    <asp:ListItem Value="5">Đã đặt hàng</asp:ListItem>
                                                                    <asp:ListItem Value="6">Đã nhận hàng tại TQ</asp:ListItem>
                                                                    <asp:ListItem Value="7">Đã nhận hàng tại kho đích</asp:ListItem>
                                                                    <asp:ListItem Value="8">Chờ thanh toán</asp:ListItem>
                                                                    <asp:ListItem Value="9">Đã xong</asp:ListItem>--%>
                                                            </asp:DropDownList></dd>

                                                        <dd class="ordercodes width100" style="display: none;">
                                                            <div class="ordercode">
                                                                <div class="item-element">
                                                                    <span>Mã Vận đơn 1</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="item-element">
                                                                    <span>Cân nặng</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCodeWeight" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:Literal ID="ltraddordercode" runat="server"></asp:Literal>
                                                            <div id="oc2" class="ordercode" style="display: none;">
                                                                <div class="item-element">
                                                                    <span>Mã Vận đơn 2</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCode2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="item-element">
                                                                    <span>Cân nặng</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCodeWeight2" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div id="oc3" class="ordercode" style="display: none;">
                                                                <div class="item-element">
                                                                    <span>Mã Vận đơn 3</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCode3" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="item-element">
                                                                    <span>Cân nặng</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCodeWeight3" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div id="oc4" class="ordercode" style="display: none;">
                                                                <div class="item-element">
                                                                    <span>Mã Vận đơn 4</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCode4" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="item-element">
                                                                    <span>Cân nặng</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCodeWeight4" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div id="oc5" class="ordercode" style="display: none;">
                                                                <div class="item-element">
                                                                    <span>Mã Vận đơn 5</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCode5" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="item-element">
                                                                    <span>Cân nặng</span>
                                                                    <asp:TextBox ID="txtOrdertransactionCodeWeight5" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </dd>

                                                        <dt>Nhận hàng tại</dt>
                                                        <dd>
                                                            <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="getplace()" Enabled="false">
                                                            </asp:DropDownList>
                                                        </dd>
                                                        <asp:Panel ID="pnShippingType" runat="server" Style="display: none">
                                                            <dt>Phương thức vận chuyển</dt>
                                                            <dd>
                                                                <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control" onchange="getplace()" Enabled="false">
                                                                    <asp:ListItem Value="1" Text="Đi nhanh"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Đi thường"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </dd>
                                                        </asp:Panel>

                                                        <dt class="full-width"><strong class="title-fee">Phí tùy chọn</strong></dt>
                                                        <dt>Phụ phí mặt hàng đặc biệt 
                                                        </dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="rAdditionFeeForSensorProductCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeeCensor('ndt')"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí kiểm đếm CNY" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="rAdditionFeeForSensorProductVND" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeeCensor('vnd')"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>
                                                            <asp:CheckBox ID="chkCheck" runat="server" Enabled="false" />
                                                            Phí kiểm đếm
                                                        </dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pCheckNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountCheckPro('ndt')"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí kiểm đếm CNY" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pCheck" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountCheckPro('vnd')"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>
                                                            <asp:CheckBox ID="chkPackage" runat="server" Enabled="false" />
                                                            Phí đóng gỗ
                                                        </dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pPackedNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeePackage('ndt')"
                                                                NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí đóng gỗ CNY" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pPacked" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeePackage('vnd')"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>
                                                            <asp:CheckBox ID="chkShiphome" runat="server" Enabled="false" />
                                                            Phí ship giao hàng tận nhà
                                                        </dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pShipHome" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnđ</span></dd>
                                                        <dt>
                                                            <asp:CheckBox ID="chkIsGiaohang" runat="server" Enabled="false" />
                                                            Yêu cầu giao hàng</dt>
                                                        <dt class="full-width"><strong class="title-fee">Thanh toán</strong></dt>
                                                        <dt>Số Tiền phải cọc</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pAmountDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnd</span></dd>
                                                        <dt>Số tiền đã trả</dt>
                                                        <dd>
                                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                ID="pDeposit" MinValue="0" NumberFormat-DecimalDigits="0" Enabled="false"
                                                                NumberFormat-GroupSizes="3" Width="100%">
                                                            </telerik:RadNumericTextBox>
                                                            <span class="currency">vnd</span></dd>
                                                        <dt>
                                                            <asp:Literal ID="ltrBtnUpdate" runat="server"></asp:Literal>
                                                            <%--<a href="javascript:;" class="btn pill-btn primary-btn admin-btn full-width" onclick="UpdateOrder()">CẬP NHẬT</a>--%>
                                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Style="display: none" Text="CẬP NHẬT" OnClick="btnUpdate_Click" />
                                                        </dt>
                                                        <dd>
                                                            <asp:Button ID="btnThanhtoan" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Text="THANH TOÁN" OnClick="btnThanhtoan_Click" Visible="false" />
                                                        </dd>
                                                        <dd></dd>
                                                    </dl>
                                                </div>
                                            </div>
                                            <div class="order-panel bg-red-nhst print4">
                                                <div class="title">Tổng tiền hàng khách cần thanh toán</div>
                                                <div class="cont">
                                                    <dl>
                                                        <dt>Tiền hàng</dt>
                                                        <dd>
                                                            <asp:Label ID="lblMoneyNotFee" runat="server"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Ship nội địa</dt>
                                                        <dd>
                                                            <asp:Label ID="lblShipTQ" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Phí mua hàng</dt>
                                                        <dd>
                                                            <asp:Label ID="lblFeeBuyProduct" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <asp:Literal ID="ltrFeeWechat" runat="server"></asp:Literal>
                                                        <dt>Tiền hàng</dt>
                                                        <dd>
                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Phí tùy chọn</dt>
                                                        <dd>
                                                            <asp:Label ID="lblAllFee" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Phí vận chuyển TQ - VN</dt>
                                                        <dd>
                                                            <asp:Label ID="lblFeeTQVN" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Tổng chi phí</dt>
                                                        <dd>
                                                            <asp:Label ID="lblAllTotal" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Đã thanh toán</dt>
                                                        <dd>
                                                            <asp:Label ID="lblDeposit" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                        <dt>Cần thanh toán</dt>
                                                        <dd>
                                                            <asp:Label ID="lblLeftPay" runat="server" Text="0"></asp:Label>
                                                            vnđ</dd>
                                                    </dl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-panels">
                                            <div class="order-panel">
                                                <div class="title">Phí vận chuyển TQ - VN</div>
                                                <div class="cont">
                                                    <table class="tb-product">
                                                        <tr>
                                                            <th class="pro" style="vertical-align: middle; text-align: center;">Ngày yêu cầu xuất kho</th>
                                                            <th class="pro" style="vertical-align: middle; text-align: center;">Cân nặng (kg)</th>
                                                            <th class="pro" style="vertical-align: middle; text-align: center;">Số kiện</th>
                                                            <th class="pro" style="vertical-align: middle; text-align: center;">Tổng tiền (tệ)</th>
                                                            <th class="qty" style="vertical-align: middle; text-align: center;">Tổng tiền (VNĐ)</th>
                                                            <th class="qty" style="vertical-align: middle; text-align: center;">HTVC</th>
                                                            <th class="qty" style="vertical-align: middle; text-align: center;">Ghi chú</th>
                                                        </tr>
                                                        <asp:Literal ID="ltrExportHistory" runat="server"></asp:Literal>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-panel print5">
                                            <div class="title">Lịch sử thanh toán </div>
                                            <div class="cont">
                                                <table class="tb-product">
                                                    <tr>
                                                        <th class="pro">Ngày thanh toán</th>
                                                        <th class="pro">Loại thanh toán</th>
                                                        <th class="pro">Hình thức thanh toán</th>
                                                        <th class="qty">Số tiền</th>
                                                    </tr>
                                                    <asp:Repeater ID="rptPayment" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="pro">
                                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                                </td>
                                                                <td class="pro">
                                                                    <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                                                </td>
                                                                <td class="pro">
                                                                    <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                                                </td>
                                                                <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="order-panel print6" style="margin-left: 0;">
                                            <div class="title">Lịch sử thay đổi </div>
                                            <div class="cont">
                                                <div class="table-responsive">
                                                    <asp:Panel ID="pn" runat="server">
                                                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                                            AllowSorting="true" AllowFilteringByColumn="True">
                                                            <GroupingSettings CaseSensitive="false" />
                                                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false" Visible="false">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Date" HeaderText="Ngày thay đổi" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                                                        AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Username" HeaderText="Người thay đổi" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                                                        AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="RoleName" HeaderText="Quyền hạn" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                                                        AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Content" HeaderText="Nội dung" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                                                        AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>
                                                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                                    PrevPageText="← Previous" />
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </main>

                    </div>
                </div>
            </div>
            <!-- Modal -->
        </div>
    </div>
    <div id="printcontent" class="printdetail" style="display: none;">
        <div class="print-item">
            <div class="print-element-row">
                <div class="print-row-left">
                    ID Đơn hàng:
                </div>
                <div class="print-row-right">
                    <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                </div>
            </div>
            <div class="print-element-row">
                <div class="print-row-left">
                    User đặt hàng:
                </div>
                <div class="print-row-right">
                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                </div>
            </div>
            <div class="print-element-row">
                <div class="order-panel" style="margin: 0; margin-bottom: 30px;">
                    <asp:Literal ID="ltrProductOrderShopCode1" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="print-element-row">
                <div class="print-footer-row-left">
                    Đại diện công ty 1688express<br />
                    Ký tên

                </div>
                <div class="print-footer-row-right">
                    Khách hàng<br />
                    Ký tên (Ghi rõ họ tên)
                </div>
            </div>
            <style>
                .print-footer-row-left {
                    float: left;
                    width: 25%;
                    text-align: center;
                    font-size: 20px;
                }

                .print-footer-row-right {
                    float: right;
                    width: 25%;
                    text-align: center;
                    font-size: 20px;
                }

                .print-item {
                    float: left;
                    width: 100%;
                }

                .print-element-row {
                    float: left;
                    width: 100%;
                    clear: both;
                }

                .print-row-left {
                    float: left;
                    width: 10%;
                }

                .print-row-right {
                    float: left;
                    width: 65%;
                    font-weight: bold;
                }
            </style>
        </div>
    </div>

    <%--</asp:Panel>--%>

    <asp:HiddenField ID="hdfOrderID" runat="server" />
    <asp:HiddenField ID="hdfcurrent" runat="server" />
    <asp:HiddenField ID="hdfFeeBuyProDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeWeightDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeweightPriceDiscount" runat="server" />
    <asp:HiddenField ID="hdfUserRole" runat="server" />
    <asp:HiddenField ID="hdfListMainOrderCode" runat="server" />
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSend">
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
    </telerik:RadAjaxManager>

    <asp:HiddenField ID="hdforderamount" runat="server" />
    <asp:HiddenField ID="hdfoc2" runat="server" />
    <asp:HiddenField ID="hdfoc3" runat="server" />
    <asp:HiddenField ID="hdfoc4" runat="server" />
    <asp:HiddenField ID="hdfoc5" runat="server" />
    <asp:HiddenField ID="hdfReceivePlace" runat="server" />
    <asp:HiddenField ID="hdfShippingType" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHN" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHCM" runat="server" />
    <asp:HiddenField ID="hdfCodeTransactionList" runat="server" />
    <asp:HiddenField ID="hdfOrderType" runat="server" />
    <telerik:RadScriptBlock ID="rsc" runat="server">
        <script type="text/javascript">


            function deleteOrderCode(obj) {
                var r = confirm("Bạn muốn xóa mã vận đơn này?");
                if (r == true) {
                    var id = obj.parent().parent().attr("data-packageID");
                    $.ajax({
                        type: "POST",
                        url: "/Admin/OrderDetail.aspx/DeleteSmallPackage",
                        data: "{IDPackage:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            obj.parent().parent().remove();
                            swal
	                        (
		                        {
		                            title: 'Thông báo',
		                            text: 'Cập nhật đơn hàng: ' + id + ' thành công',
		                            type: 'success'
		                        },
		                        function () { window.location.replace(window.location.href); }
	                         );
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            alert('Lỗi');
                        }
                    });
                }
                else {

                }
            }
            function printDiv() {
                var html = "";

                //$('link').each(function () { // find all <link tags that have
                //    if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                //      html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                //    }
                //});
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/style.css" media="all">';
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/responsive.css" media="all">';
                html += '<link rel="stylesheet" href="/App_Themes/NHST/css/style-custom.css" media="all">';
                html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
                var w = window.open("", "print");
                if (w) { w.document.write(html); w.document.close() }
            }
            $(document).ready(function () {
                //$(".print6").clone().appendTo(".printdetail").show();
                //$(".print1").clone().appendTo(".printdetail");
                //$(".print2").clone().appendTo(".printdetail");
                //$(".print3").clone().appendTo(".printdetail").show();
                //$(".print4").clone().appendTo(".printdetail");
                //$(".print5").clone().appendTo(".printdetail");


                //var oc2 = $("#<%= hdfoc2.ClientID%>").val();
                //var oc3 = $("#<%= hdfoc3.ClientID%>").val();
                //var oc4 = $("#<%= hdfoc4.ClientID%>").val();
                //var oc5 = $("#<%= hdfoc5.ClientID%>").val();

                //if (oc2 == "1") {
                //    $("#oc2").show();
                //}
                //if (oc3 == "1") {
                //    $("#oc3").show();
                //}
                if (oc4 == "1") {
                    $("#oc4").show();
                }
                if (oc5 == "1") {
                    $("#oc5").show();
                }
            });
            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }
            var currency = $("#<%=hdfcurrent.ClientID%>").val();
            function CountShipFee(type) {
                var shipfeendt = $("#<%= pCNShipFeeNDT.ClientID%>").val();
                var shipfeevnd = $("#<%= pCNShipFee.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= pCNShipFee.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
            }
            function CountFeeCensor(type) {
                var shipfeendt = $("#<%= rAdditionFeeForSensorProductCYN.ClientID%>").val();
                var shipfeevnd = $("#<%= rAdditionFeeForSensorProductVND.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= rAdditionFeeForSensorProductCYN.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= rAdditionFeeForSensorProductVND.ClientID%>").val(0);
                        $("#<%= rAdditionFeeForSensorProductCYN.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= rAdditionFeeForSensorProductVND.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= rAdditionFeeForSensorProductVND.ClientID%>").val(0);
                        $("#<%= rAdditionFeeForSensorProductCYN.ClientID%>").val(0);
                    }
                }
            }
            function CountCheckPro(type) {
                var pCheckNDT = $("#<%= pCheckNDT.ClientID%>").val();
                var pCheckVND = $("#<%= pCheck.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pCheckVND)) {
                        var ndt = pCheckVND / currency;
                        $("#<%= pCheckNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pCheckNDT)) {
                        var vnd = pCheckNDT * currency;
                        $("#<%= pCheck.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
            }
            function CountFeePackage(type) {
                var pPackedNDT = $("#<%= pPackedNDT.ClientID%>").val();
                var pPackedVND = $("#<%= pPacked.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pPackedVND)) {
                        var ndt = pPackedVND / currency;
                        $("#<%= pPackedNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pPackedNDT)) {
                        var vnd = pPackedNDT * currency;
                        $("#<%= pPacked.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
            }
            function CountRealPrice() {
                var rTotalPriceRealCYN = $("#<%= rTotalPriceRealCYN.ClientID%>").val();
                var rTotalPriceReal = $("#<%= rTotalPriceReal.ClientID%>").val();
                var newpriuce = rTotalPriceRealCYN * currency;
                $("#<%= rTotalPriceReal.ClientID%>").val(newpriuce);

            }
            function CountRealPrice1() {
                var rTotalPriceRealCYN = $("#<%= rTotalPriceRealCYN.ClientID%>").val();
                var rTotalPriceReal = $("#<%= rTotalPriceReal.ClientID%>").val();
                var newpriuce = rTotalPriceReal / currency;
                $("#<%= rTotalPriceRealCYN.ClientID%>").val(newpriuce);

            }
            function CountFeeBuyPro() {
                var pBuyNotDC = $("#<%= pBuyNDT.ClientID%>").val();
                var pBuyDC = $("#<%= pBuy.ClientID%>").val();

                var discountper = $("#<%= hdfFeeBuyProDiscount.ClientID%>").val();
                var subfee = pBuyNotDC * discountper / 100;
                var vnd = (pBuyNotDC - subfee) * currency;
                $("#<%= pBuy.ClientID%>").val(vnd);
            }
            function CountFeeWeChatBuyPro()
            {
                var pBuyNotDC = $("#<%= rFeeWeChatCYN.ClientID%>").val();
                var vnd = pBuyNotDC * currency;
                $("#<%= rFeeWeChatVND.ClientID%>").val(vnd);
            }
            function addordercode() {
                var count = 0;
                count = parseInt($("#<%=hdforderamount.ClientID %>").val());
                if (count == 5) {
                    return;
                }
                else {
                    count = count + 1;
                    $("#<%=hdforderamount.ClientID %>").val(count);
                    var occ = "oc" + count;
                    $("#" + occ + "").show();

                }
            }

            function CountFeeWeight(type) {
                var pWeightNDT = $("#<%= pWeightNDT.ClientID%>").val();
                var pWeightVND = $("#<%= pWeight.ClientID%>").val();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var totalweight = parseFloat(pWeightNDT);

                if (totalweight > 0) {

                    //var leftweight = totalweight - 1;
                    var leftweight = totalweight;
                    var totalfeeweight = 0;
                    //var firstfeeweight = 100000;
                    var firstfeeweight = 0;
                    if (type == "kg") {
                        var steps = countfeearea.split('|');
                        if (steps.length > 0) {
                            for (var i = 0; i < steps.length - 1; i++) {
                                var step = steps[i];
                                var itemstep = step.split(',');
                                var wf = parseFloat(itemstep[1]);
                                var wt = parseFloat(itemstep[2]);
                                var amount = parseFloat(itemstep[3]);

                                if (totalweight >= wf && totalweight <= wt) {
                                    //totalfeeweight = firstfeeweight + (leftweight * amount);
                                    totalfeeweight = totalweight * amount;
                                }
                            }
                        }
                        var vnd = totalfeeweight;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(subfee));
                        $("#<%= pWeight.ClientID%>").val(vnd);
                    }
                }
                else {
                    $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(0));
                    $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(0));
                    $("#<%= pWeight.ClientID%>").val(0);
                }
            }
            function gettotalweight_old() {
                //txtOrderWeight, txtOrdertransactionCodeWeight
                var ocw = $("#<%= txtOrdertransactionCodeWeight.ClientID%>").val();
                var ocw2 = $("#<%= txtOrdertransactionCodeWeight2.ClientID%>").val();
                var ocw3 = $("#<%= txtOrdertransactionCodeWeight3.ClientID%>").val();
                var ocw4 = $("#<%= txtOrdertransactionCodeWeight4.ClientID%>").val();
                var ocw5 = $("#<%= txtOrdertransactionCodeWeight5.ClientID%>").val();
                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                //alert(countfeearea);

                if (isEmpty(ocw)) {
                    ocw = 0;
                }
                if (isEmpty(ocw2)) {
                    ocw2 = 0;
                }
                if (isEmpty(ocw3)) {
                    ocw3 = 0;
                }
                if (isEmpty(ocw4)) {
                    ocw4 = 0;
                }
                if (isEmpty(ocw5)) {
                    ocw5 = 0;
                }
                var totalweight = parseFloat(ocw) + parseFloat(ocw2) + parseFloat(ocw3) + parseFloat(ocw4) + parseFloat(ocw5);
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                //var firstfeeweight = 100000;
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                //var leftweight = totalweight - 1;


                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;

                //$("#<%= pPackedNDT.ClientID %>").val(feepackedndt);
                //$("#<%= pPacked.ClientID %>").val(feepackedvnd);
                //$("#<%= pWeight.ClientID %>").val(totalfeeweight);
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function gettotalweight() {
                //txtOrderWeight, txtOrdertransactionCodeWeight
                var totalweight = 0;
                $(".transactionWeight").each(function () {
                    totalweight += parseFloat($(this).val());
                });
                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function gettotalweight2_old() {

                var totalweight = $("#<%=txtOrderWeight.ClientID%>").val();

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }

            function gettotalweight2() {
                addLoading();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();
                var orderID = $("#<%= hdfOrderID.ClientID%>").val();
                var warehouse = $("#<%= hdfReceivePlace.ClientID%>").val();
                var shipping = $("#<%= hdfShippingType.ClientID%>").val();
                if (isEmpty(shipping))
                    shipping = 1;
                var currentweight = "";
                if ($(".transactionWeight").length > 0) {
                    $(".transactionWeight").each(function () {
                        currentweight += $(this).val() + "|";
                    });
                }
                //alert(orderID + "-" + warehouse + "-" + shipping + "-" + currentweight);
                $.ajax({
                    type: "POST",
                    url: "/Admin/OrderDetail.aspx/getWeightPrice",
                    data: "{orderID:'" + orderID + "',warehouse:'" + warehouse + "',shipping:'" + shipping + "',weightlist:'" + currentweight + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                       <%-- var totalfeeweight = parseFloat(msg.d);
                        var vnd = totalfeeweight * currency;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        var cyn = vnd / currency;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(subfee));
                        $("#<%= pWeightNDT.ClientID%>").val(cyn);
                        $("#<%= pWeight.ClientID%>").val(vnd);--%>
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                        alert('Vui lòng nhập số vào cân nặng');
                    }
                });

            }
            function gettotalweightgoupdate() {
                addLoading();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();
                var orderID = $("#<%= hdfOrderID.ClientID%>").val();
                var warehouse = $("#<%= hdfReceivePlace.ClientID%>").val();
                var shipping = $("#<%= hdfShippingType.ClientID%>").val();
                if (isEmpty(shipping))
                    shipping = 1;
                var currentweight = "";
                if ($(".transactionWeight").length > 0) {
                    $(".transactionWeight").each(function () {
                        currentweight += $(this).val() + "|";
                    });
                }
                //alert(orderID + "-" + warehouse + "-" + shipping + "-" + currentweight);
                $.ajax({
                    type: "POST",
                    url: "/Admin/OrderDetail.aspx/getWeightPrice",
                    data: "{orderID:'" + orderID + "',warehouse:'" + warehouse + "',shipping:'" + shipping + "',weightlist:'" + currentweight + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var totalfeeweight = parseFloat(msg.d);
                        var vnd = totalfeeweight * currency;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        var cyn = vnd / currency;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(subfee));
                        $("#<%= pWeightNDT.ClientID%>").val(cyn);
                        $("#<%= pWeight.ClientID%>").val(vnd);
                        removeLoading();
                        UpdateOrder();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                        alert('Vui lòng nhập số vào cân nặng');
                    }
                });

            }
            function addLoading() {
                $("#content-order-detail").append("<div class=\"addloading\"></div>");
            }
            function removeLoading() {
                $(".addloading").remove();
            }

            function getplace() {
                var value = $("#<%= ddlReceivePlace.ClientID%>").val();
                var shippingtype = $("#<%=ddlShippingType.ClientID%>").val();
                if (value != 4) {
                    $("#<%=pnShippingType.ClientID%>").show();
                }
                else {
                    $("#<%=pnShippingType.ClientID%>").hide();
                }
                $("#<%= hdfReceivePlace.ClientID%>").val(value);
                $("#<%= hdfShippingType.ClientID%>").val(shippingtype);

                gettotalweight2();
            }


            function getplace_old() {
                var value = $("#<%= ddlReceivePlace.ClientID%>").val();
                if (value == "Kho Hà Nội") {
                    $("#<%= hdfReceivePlace.ClientID%>").val(1);
                }
                else {
                    $("#<%= hdfReceivePlace.ClientID%>").val(2);
                }
                gettotalweight();
                CountFeeWeight('kg');
            }
            $(document).ready(function () {
                var re = $("#<%=hdfReceivePlace.ClientID%>").val();
                if (re == "4") {
                    $("#<%=pnShippingType.ClientID%>").hide();
                }
                else {
                    $("#<%=pnShippingType.ClientID%>").show();
                }

            })
        </script>
        <asp:HiddenField ID="hdfUpdateFeeShipVN" runat="server" />
        <script type="text/javascript">
            function updateTotalprice(obj, typemoney) {
                var root = obj.parent();
                var textval = parseFloat(obj.val());
                var mo = 0;
                var currency = $("#<%= hdfcurrent.ClientID%>").val();
                if (typemoney == 'cyn') {
                    mo = textval * currency;
                    root.find('.totalpricevndshopcode').val(mo);
                }
                else {
                    mo = textval / currency;
                    root.find('.totalpricecynshopcode').val(mo);
                }
            }
            function UpdateOrder() {
                var list = "";
                $(".order-versionnew").each(function () {
                    var id = $(this).attr("data-packageID");
                    var code = $(this).find(".transactionCode").val();
                    var weight = $(this).find(".transactionWeight").val();
                    var status = $(this).find(".transactionCodeStatus option:selected").val();
                    var orderwebcode = $(this).find(".transactionCodeOrderCodeString option:selected").val();
                    list += id + "," + code + "," + weight + "," + status + "," + orderwebcode + "|";
                });
                var html = "";
                $(".feeshipcnshopcode").each(function () {
                    var parent = $(this).parent();
                    //var totalpricecynshopcode = parent.find('.totalpricecynshopcode').val();
                    //var totalpricevndshopcode = parent.find('.totalpricevndshopcode').val();
                    //var totalquantityshopcode = parent.find('.totalquantityshopcode').val();
                    var price = $(this).val();
                    var mID = $(this).attr("data-mainorderid");
                    var shopID = $(this).attr("data-shopid");
                    html += price + "," + mID + "," + shopID + "|";
                    //html += price + "," + mID + "," + shopID + ","
                    //    + totalpricecynshopcode + "," + totalpricevndshopcode + ","
                    //    + totalquantityshopcode + "|";
                });
                $("#<%=hdfUpdateFeeShipVN.ClientID%>").val(html);
                $("#<%=hdfCodeTransactionList.ClientID%>").val(list);

                $("#<%=btnUpdate.ClientID%>").click();
            }
            function addCodeTransaction() {
                var orderType = $("#<%=hdfOrderType.ClientID%>").val();
                var role = parseFloat($("#<%=hdfUserRole.ClientID%>").val());
                var html = "";
                if (orderType == "2") {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/OrderDetail.aspx/getCode",
                        //data: "{IDPackage:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html += "<div class=\"ordercode order-versionnew\" data-packageID=\"0\">";
                            html += " <div class=\"item-element\">";
                            html += "     <span>Mã Vận đơn:</span>";
                            html += "     <input class=\"transactionCode form-control\" type=\"text\" value=\"" + msg.d + "\" disabled placeholder=\"Mã vận đơn\" />";
                            html += " </div>";
                            html += " <div class=\"item-element\">";
                            html += "     <span>Cân nặng:</span>";
                            if (role != 0 && role != 2) {
                                html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\" disabled onkeyup=\"gettotalweight2()\" />";
                            }
                            else {
                                html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\" onkeyup=\"gettotalweight2()\" />";
                            }
                            html += " </div>";
                            html += " <div class=\"item-element\">";
                            html += "     <span>Trạng thái:</span>";
                            html += "     <select class=\"transactionCodeStatus form-control\">";
                            html += "         <option value=\"1\">Chưa về kho TQ</option>";
                            html += "         <option value=\"2\">Đã về kho TQ</option>";
                            html += "         <option value=\"3\">Đã về kho đích</option>";
                            html += "         <option value=\"4\">Đã giao khách hàng</option>";
                            html += "     </select>";
                            html += " </div>";
                            html += " <div class=\"item-element\">";
                            html += "     <span>Mã đơn hàng:</span>";
                            html += "     <select class=\"transactionCodeOrderCodeString form-control\">";
                            html += "         <option value=\"\">--Chọn mã đơn hàng--</option>";
                            var code = $("#<%=txtMainOrderCode.ClientID%>").val();
                            var listcode = code.split(';');
                            if (listcode.length - 1 > 0) {
                                for (var i = 0; i < listcode.length - 1; i++) {
                                    html += "         <option value=\"" + listcode[i] + "\">" + listcode[i] + "</option>";
                                }
                            }
                            html += "     </select>";
                            html += " </div>";
                            html += "<div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteordercode($(this))\">Xóa</a></div>";
                            html += "</div>";
                            $("#transactioncodeList").append(html);
                            addListMainorderCode();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            alert('Lỗi');
                        }
                    });
                }
                else {
                    html += "<div class=\"ordercode order-versionnew\" data-packageID=\"0\">";
                    html += " <div class=\"item-element\">";
                    html += "     <span>Mã Vận đơn:</span>";

                    html += "     <input class=\"transactionCode form-control\" type=\"text\" placeholder=\"Mã vận đơn\" />";
                    html += " </div>";
                    html += " <div class=\"item-element\">";
                    html += "     <span>Cân nặng:</span>";
                    if (role != 0 && role != 2) {

                        html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\" disabled onkeyup=\"gettotalweight2()\" />";
                    }
                    else {
                        html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\" onkeyup=\"gettotalweight2()\" />";
                    }
                    //html += "     <input class=\"transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" disabled value=\"0\" onkeyup=\"gettotalweight2()\" />";
                    html += " </div>";
                    html += " <div class=\"item-element\">";
                    html += "     <span>Trạng thái:</span>";
                    html += "     <select class=\"transactionCodeStatus form-control\">";
                    html += "         <option value=\"1\">Chưa về kho TQ</option>";
                    html += "         <option value=\"2\">Đã về kho TQ</option>";
                    html += "         <option value=\"3\">Đã về kho đích</option>";
                    html += "         <option value=\"4\">Đã giao khách hàng</option>";
                    html += "     </select>";
                    html += " </div>";
                    html += " <div class=\"item-element\">";
                    html += "     <span>Mã đơn hàng:</span>";
                    html += "     <select class=\"transactionCodeOrderCodeString form-control\">";
                    html += "         <option value=\"\">--Chọn mã đơn hàng--</option>";
                    var code = $("#<%=txtMainOrderCode.ClientID%>").val();
                    var listcode = code.split(';');
                    if (listcode.length - 1 > 0) {
                        for (var i = 0; i < listcode.length - 1; i++) {
                            html += "         <option value=\"" + listcode[i] + "\">" + listcode[i] + "</option>";
                        }
                    }
                    html += "     </select>";
                    html += " </div>";
                    html += "<div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteordercode($(this))\">Xóa</a></div>";
                    html += "</div>";
                    $("#transactioncodeList").append(html);
                    addListMainorderCode();
                }
            }
            function loadOrderWebCode() {
                if ($(".transactionCodeOrderCodeString").length > 0) {
                    $(".transactionCodeOrderCodeString").each(function () {
                        var currentSelected = $(this).val();
                        $(this).empty().append("<option value=\"\">--Chọn mã đơn hàng--</option>");
                        var code = $("#<%=hdfListMainOrderCode.ClientID%>").val();
                        var listcode = code.split(';');
                        if (listcode.length - 1 > 0) {
                            for (var i = 0; i < listcode.length - 1; i++) {
                                var codeitem = listcode[i];
                                if (codeitem == currentSelected) {
                                    $(this).append("<option value=\"" + codeitem + "\" selected>" + codeitem + "</option>");
                                }
                                else {
                                    $(this).append("<option value=\"" + codeitem + "\">" + codeitem + "</option>");
                                }
                            }
                        }
                    })
                }
            }
            function addOrderCode1() {
                var code = $("#<%=txtMainOrderCode.ClientID%>").val();

                var check = false;
                if ($(".ordercodemain").length > 0) {
                    $(".ordercodemain").each(function () {
                        if ($(this).attr("data-code") == code) {
                            check = true;
                        }
                    });
                }
                if (check == false) {
                    var html = "";
                    html += "<tr class=\"ordercodemain\" data-code=\"" + code + "\">";
                    html += "<td>" + code + "</td>";
                    html += "<td>";
                    html += "<a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteordercode($(this))\">Xóa</a>";
                    html += "</td>";
                    html += "</tr>";
                    $(".listordercodeadd").append(html);
                    $(".containlistcode").show();
                }
                $("#<%=txtMainOrderCode.ClientID%>").val("");
                addListMainorderCode();

            }
            function deleteordercode(obj) {
                var c = confirm("Bạn muốn xóa mã đơn hàng này?");
                if (c) {
                    obj.parent().parent().remove();
                    if ($(".ordercodemain").length == 0) {
                        $(".containlistcode").hide();
                    }
                    addListMainorderCode();
                }
            }
            function addListMainorderCode() {
                var listmainordercode = "";
                $(".ordercodemain").each(function () {
                    listmainordercode += $(this).attr("data-code") + ";";
                });
                $("#<%=hdfListMainOrderCode.ClientID%>").val(listmainordercode);
                loadOrderWebCode();
            }
            $(document).ready(function () {
                var count = parseFloat($("#<%=hdfcount.ClientID%>").val());
                if (count > 0) {
                    $(".containlistcode").show();
                }
                else {
                    $(".containlistcode").hide();
                }
            });

        </script>
        <asp:HiddenField ID="hdfTotalWeight" runat="server" />
        <script type="text/javascript">
            //Gọi Popup
            function callpopupaddordershopcode(shopID, shopName, MainOrderID) {
                var content = "";
                content += "<span class=\"label-popup\">Nhập mã đơn hàng:</span>";
                content += "<input class=\"form-control\" id=\"ordershopcode-content\"/>";
                content += "<span id=\"error-txt\"></span>";
                var button = "<a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addOrderShopCode('" + shopID + "','" + shopName + "','" + MainOrderID + "')\">Thêm</a>";
                showPopup("Thêm mã đơn hàng cho shop: " + shopID + "", content, button);
            }
            function showordershopcodedetail(obj) {
                var parentCode = obj.parent().parent();
                var id = parentCode.attr("data-ordershopcodeid");
                var ordershopcode = parentCode.attr("data-ordershopcode");
                var shopID = parentCode.attr("data-shopid");
                var shopName = parentCode.attr("data-shopname");
                var MainOrderID = parentCode.attr("data-mainorderid");
                var content = "";
                content += "<span class=\"label-popup\">Mã đơn hàng:</span>";
                content += "<input class=\"form-control\" id=\"ordershopcode-content\" value=\"" + ordershopcode + "\"/>";
                content += "<span id=\"error-txt\"></span>";
                var button = "<a href=\"javascript:;\" class=\"btn btn-close btn-not-radius\" onclick=\"deleteOrderShopCode('" + id + "')\">Xóa</a>";
                button += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"editOrderShopCode('" + id + "','" + shopID + "','" + shopName + "','" + MainOrderID + "')\">Cập nhật</a>";

                showPopup("Mã đơn hàng của shop: " + shopID + "", content, button);
            }
            function callpopupaddsmallpackage(obj) {
                var parentCode = obj.parent().parent();
                var id = parentCode.attr("data-ordershopcodeid");
                var ordershopcode = parentCode.attr("data-ordershopcode");
                var shopID = parentCode.attr("data-shopid");
                var shopName = parentCode.attr("data-shopname");
                var MainOrderID = parentCode.attr("data-mainorderid");
                var content = "";
                content += "<div id=\"transactioncodeList1\" class=\"cont\"></div>";
                content += "<div class=\"ordercode addordercode\"><a href=\"javascript:;\" onclick=\"addsmallpackageitem()\">Thêm mã vận đơn</a></div>";
                content += "<span id=\"error-txt\"></span>";
                var button = "<a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addSmallpackage('" + id + "','" + MainOrderID + "')\">Thêm</a>";
                showPopup("Thêm mã vận đơn của mã đơn hàng của shop: " + ordershopcode + "", content, button);
            }
            function addsmallpackageitem() {
                var orderType = $("#<%=hdfOrderType.ClientID%>").val();
                var role = parseFloat($("#<%=hdfUserRole.ClientID%>").val());
                var html = "";
                html += "<div class=\"ordercode smallpackage-item\" data-packageID=\"0\">";
                html += " <div class=\"item-element\">";
                html += "     <span>Mã Vận đơn:</span>";
                html += "     <input class=\"sm-transactionCode form-control\" type=\"text\" placeholder=\"Mã vận đơn\" />";
                html += " </div>";
                html += " <div class=\"item-element\">";
                html += "     <span>Cân nặng:</span>";
                if (role != 0 && role != 2) {
                    html += "     <input class=\"sm-transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\" disabled/>";
                }
                else {
                    html += "     <input class=\"sm-transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"0\"/>";
                }
                html += " </div>";
                html += " <div class=\"item-element\">";
                html += "     <span>Trạng thái:</span>";
                html += "     <select class=\"sm-transactionCodeStatus form-control\">";
                html += "         <option value=\"1\">Chưa về kho TQ</option>";
                html += "         <option value=\"2\">Đã về kho TQ</option>";
                html += "         <option value=\"3\">Đã về kho đích</option>";
                html += "         <option value=\"4\">Đã giao khách hàng</option>";
                html += "     </select>";
                html += " </div>";
                html += " <div class=\"item-element\">";
                html += "     <span>Ghi chú:</span>";
                html += "     <input class=\"sm-smallstaffnote form-control\" type=\"text\" placeholder=\"Ghi chú\" />";
                html += " </div>";
                html += "<div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deletesmallpackage($(this))\">Xóa</a></div>";
                html += "</div>";
                $("#transactioncodeList1").append(html);
            }
            function editsmallpackage(obj) {
                var orderType = $("#<%=hdfOrderType.ClientID%>").val();
                var role = parseFloat($("#<%=hdfUserRole.ClientID%>").val());
                var id = obj.attr("data-smallpackageid");
                var status = parseFloat(obj.attr("data-smallpackagestatus"));
                var weight = obj.attr("data-smallpackageweight");
                var code = obj.attr("data-smallpackagecode");
                var staffnote = obj.attr("data-smallstaffnote");
                var html = "";
                html += "<div id=\"transactioncodeList1\" class=\"cont\">";
                html += "   <div class=\"ordercode smallpackage-item\" data-packageID=\"" + id + "\">";
                html += "       <div class=\"item-element\">";
                html += "           <span>Mã Vận đơn:</span>";
                html += "           <input class=\"sm-transactionCode form-control\" type=\"text\" placeholder=\"Mã vận đơn\" value=\"" + code + "\" />";
                html += "       </div>";
                html += "       <div class=\"item-element\">";
                html += "           <span>Cân nặng:</span>";
                if (role != 0 && role != 2) {
                    html += "           <input class=\"sm-transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"" + weight + "\" disabled/>";
                }
                else {
                    html += "           <input class=\"sm-transactionWeight form-control\" type=\"text\" placeholder=\"Cân nặng\" value=\"" + weight + "\"/>";
                }
                html += "       </div>";
                html += "       <div class=\"item-element\">";
                html += "           <span>Trạng thái:</span>";
                html += "           <select class=\"sm-transactionCodeStatus form-control\">";
                if (status == 1)
                    html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
                else
                    html += "               <option value=\"1\">Chưa về kho TQ</option>";
                if (status == 2)
                    html += "               <option value=\"2\" selected>Đã về kho TQ</option>";
                else
                    html += "               <option value=\"2\">Đã về kho TQ</option>";
                if (status == 3)
                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                else
                    html += "               <option value=\"3\">Đã về kho đích</option>";
                if (status == 4)
                    html += "               <option value=\"4\" selected>Đã giao khách hàng</option>";
                else
                    html += "               <option value=\"4\">Đã giao khách hàng</option>";

                html += "           </select>";
                html += "       </div>";
                html += "       <div class=\"item-element\">";
                html += "           <span>Ghi chú:</span>";
                html += "           <input class=\"sm-smallstaffnote form-control\" type=\"text\" placeholder=\"Ghi chú\" value=\"" + staffnote + "\" />";
                html += "       </div>";
                html += "       <div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deletesmallpackagefromDb($(this))\">Xóa</a></div>";
                html += "   </div>";
                html += "</div>";
                html += "<span id=\"error-txt\"></span>";
                var button = "<a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"updateSmallpackage()\">Cập nhật</a>";
                showPopup("Chỉnh sửa mã vận đơn", html, button);
            }
            //End Gọi Popup

            //Hàm xử lý chung tính lại số kg
            function GetWeigthNew() {
                addLoading();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();
                var orderID = $("#<%= hdfOrderID.ClientID%>").val();
                var warehouse = $("#<%= hdfReceivePlace.ClientID%>").val();
                var shipping = $("#<%= hdfShippingType.ClientID%>").val();
                if (isEmpty(shipping))
                    shipping = 1;
                var currentweight = "";
                if ($(".packageorderitem").length > 0) {
                    $(".packageorderitem").each(function () {
                        var weight = $(this).attr("data-smallpackageweight");
                        currentweight += weight + "|";
                    });
                }
                $.ajax({
                    type: "POST",
                    url: "/Admin/OrderDetail.aspx/getWeightPrice",
                    data: "{orderID:'" + orderID + "',warehouse:'" + warehouse + "',shipping:'" + shipping
                                       + "',weightlist:'" + currentweight + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var totalfeeweight = parseFloat(msg.d);
                        <%--var vnd = totalfeeweight * currency;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        var cyn = vnd / currency;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseFloat(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseFloat(subfee));
                        $("#<%= pWeightNDT.ClientID%>").val(cyn);
                        $("#<%= pWeight.ClientID%>").val(vnd);--%>
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                        alert('Vui lòng nhập số vào cân nặng');
                    }
                });

            }

            //Gọi hàm xử lý
            function updateSmallpackage(obj) {
                var root = $(".smallpackage-item");
                var id = root.attr("data-packageID");
                var code = root.find(".sm-transactionCode").val();
                var weight = root.find(".sm-transactionWeight").val();
                var status = root.find(".sm-transactionCodeStatus").val();
                var staffnote = root.find(".sm-smallstaffnote").val();
                if (isEmpty(code)) {
                    alert('Vui lòng điền mã vận đơn.');
                }
                else {
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/updateSmallPackage",
                        data: "{id:'" + id + "',code:'" + code + "',weight:'" + weight + "',status:'" + status + "',staffnote:'" + staffnote + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret != "none") {
                                $(".packageorderitem").each(function () {
                                    if ($(this).attr("data-smallpackageid") == id) {
                                        if (status == 1) {
                                            $(this).removeClass("bg-red").removeClass("bg-orange").removeClass("bg-green").removeClass("bg-blue")
                                                   .addClass("bg-red");
                                        }
                                        else if (status == 2) {
                                            $(this).removeClass("bg-red").removeClass("bg-orange").removeClass("bg-green").removeClass("bg-blue")
                                                   .addClass("bg-orange");
                                        }
                                        else if (status == 3) {
                                            $(this).removeClass("bg-red").removeClass("bg-orange").removeClass("bg-green").removeClass("bg-blue")
                                                   .addClass("bg-green");
                                        }
                                        else if (status == 4) {
                                            $(this).removeClass("bg-red").removeClass("bg-orange").removeClass("bg-green").removeClass("bg-blue")
                                                   .addClass("bg-blue");
                                        }
                                        $(this).attr("data-smallpackagestatus", status);
                                        $(this).attr("data-smallpackageweight", weight);
                                        $(this).attr("data-smallpackagecode", code);
                                        $(this).attr("data-smallstaffnote", staffnote);
                                        $(this).html(code);

                                    }
                                });
                                GetWeigthNew();
                                close_popup_ms();
                            }
                            else {
                                $("#error-txt").html("Có gián đoạn trong quá trình cập nhật, vui lòng thử lại sau.").show();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình cập nhật, vui lòng thử lại sau.").show();
                            removeLoading();

                            //alert('lỗi checkend');
                        }
                    });
                }
            }
            function deletesmallpackagefromDb(obj) {
                var c = confirm("Bạn muốn xóa mã vận đơn này?");
                if (c) {
                    var id = obj.parent().parent().attr("data-packageID");
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/deleteSmallPackage",
                        data: "{id:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret != "none") {
                                $(".packageorderitem").each(function () {
                                    if ($(this).attr("data-smallpackageid") == id) {
                                        $(this).remove();
                                    }
                                });
                                GetWeigthNew();
                                close_popup_ms();
                            }
                            else {
                                $("#error-txt").html("Có gián đoạn trong quá trình xóa mã vận đơn, vui lòng thử lại sau.").show();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            removeLoading();
                            $("#error-txt").html("Có gián đoạn trong quá trình xóa mã vận đơn, vui lòng thử lại sau.").show();
                            //alert('lỗi checkend');
                        }
                    });
                }
            }
            function addSmallpackage(orderShopCodeID, MainOrderID) {
                if ($(".smallpackage-item").length > 0) {
                    var data = "";
                    $(".smallpackage-item").each(function () {
                        var code = $(this).find(".sm-transactionCode").val();
                        if (!isEmpty(code)) {
                            var weight = $(this).find(".sm-transactionWeight").val();
                            var status = $(this).find(".sm-transactionCodeStatus").val();
                            var staffnote = $(this).find(".sm-smallstaffnote").val();
                            data += code + "," + weight + "," + status + "," + staffnote + "|";
                        }
                    });
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/addSmallPackage",
                        data: "{orderShopCodeID:'" + orderShopCodeID + "',MainOrderID:'" + MainOrderID + "',smItems:'" + data + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret != "none") {
                                $(".shoporder-item").each(function () {
                                    if ($(this).attr("data-ordershopcodeid") == orderShopCodeID) {
                                        var htmladd = "";
                                        var items = ret.split('|');
                                        if (items.length - 1 > 0) {
                                            for (var i = 0; i < items.length - 1; i++) {
                                                var item = items[i];
                                                var element = item.split(',');
                                                var id = element[0];
                                                var eCode = element[1];
                                                var eWeight = element[2];
                                                var eStatus = element[3];
                                                var eStaffNote = element[4];
                                                if (eStatus == 1) {
                                                    htmladd += "<a href=\"javascript:;\" class=\"packageorderitem custom-link bg-red\" data-smallpackageid=\"" + id
                                                       + "\" data-smallpackagestatus=\"" + eStatus + "\" data-smallpackageweight=\"" + eWeight
                                                       + "\" data-smallpackagecode=\"" + eCode + "\" data-smallstaffnote=\"" + eStaffNote + "\" onclick=\"editsmallpackage($(this))\">"
                                                       + eCode + "</a>";
                                                }
                                                else if (eStatus == 2) {
                                                    htmladd += "<a href=\"javascript:;\" class=\"packageorderitem custom-link bg-orange\" data-smallpackageid=\"" + id
                                                       + "\" data-smallpackagestatus=\"" + eStatus + "\" data-smallpackageweight=\"" + eWeight
                                                       + "\" data-smallpackagecode=\"" + eCode + "\" data-smallstaffnote=\"" + eStaffNote + "\" onclick=\"editsmallpackage($(this))\">"
                                                       + eCode + "</a>";
                                                }
                                                else if (eStatus == 3) {
                                                    htmladd += "<a href=\"javascript:;\" class=\"packageorderitem custom-link bg-green\" data-smallpackageid=\"" + id
                                                       + "\" data-smallpackagestatus=\"" + eStatus + "\" data-smallpackageweight=\"" + eWeight
                                                       + "\" data-smallpackagecode=\"" + eCode + "\" data-smallstaffnote=\"" + eStaffNote + "\" onclick=\"editsmallpackage($(this))\">"
                                                       + eCode + "</a>";
                                                }
                                                else if (eStatus == 4) {
                                                    htmladd += "<a href=\"javascript:;\" class=\"packageorderitem custom-link bg-blue\" data-smallpackageid=\"" + id
                                                       + "\" data-smallpackagestatus=\"" + eStatus + "\" data-smallpackageweight=\"" + eWeight
                                                       + "\" data-smallpackagecode=\"" + eCode + "\" data-smallstaffnote=\"" + eStaffNote + "\" onclick=\"editsmallpackage($(this))\">"
                                                       + eCode + "</a>";
                                                }
                                            }
                                        }
                                        $(this).find(".smallpackage-list-of-odsc").append(htmladd);
                                    }
                                });
                                GetWeigthNew();
                                close_popup_ms();
                            }
                            else {
                                $("#error-txt").html("Có gián đoạn trong quá trình thêm mã vận đơn, vui lòng thử lại sau.").show();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình thêm mã vận đơn, vui lòng thử lại sau.").show();
                            removeLoading();
                            //alert('lỗi checkend');
                        }
                    });

                }
                else {
                    alert("Vui lòng thêm mã vận đơn.");
                }
            }
            function addOrderShopCode(shopID, shopName, MainOrderID) {
                var codetext = $("#ordershopcode-content").val();
                if (isEmpty(codetext)) {
                    $("#error-txt").html("Vui lòng nhập mã đơn hàng.").show();
                }
                else {
                    $("#error-txt").html("").hide();
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/addOrderShopCode",
                        data: "{ordershopcode:'" + codetext + "',ShopID:'" + shopID + "',ShopName:'" + shopName + "',MainOrderID:'" + MainOrderID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "none") {
                                $("#error-txt").html("Có gián đoạn trong quá trình thêm mã đơn, vui lòng thử lại sau.").show();
                            }
                            else if (ret == "exist") {
                                $("#error-txt").html("Mã đơn hàng đã tồn tại vui lòng kiểm tra lại.").show();
                            }
                            else {
                                //Thêm vào danh sách hiện có
                                var htmladd = "";
                                htmladd += "<tr class=\"shoporder-item\" data-shopid=\"" + shopID + "\" data-shopname=\"" + shopName
                                        + "\" data-mainorderid=\"" + MainOrderID + "\" data-ordershopcode=\"" + codetext
                                        + "\" data-ordershopcodeid=\"" + ret + "\">";
                                htmladd += "    <td>";
                                htmladd += "        <a href=\"javascript:;\" class=\"custom-link ordershopcode-atag\" onclick=\"showordershopcodedetail($(this))\">" + codetext + "</a>";
                                htmladd += "        <a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"callpopupaddsmallpackage($(this))\"  style=\"clear: both; width: 100%; text-align: center\">Thêm mã vận đơn</a>";
                                htmladd += "    </td>";
                                htmladd += "    <td class=\"smallpackage-list-of-odsc\"></td>";
                                htmladd += "</tr>";
                                $("#" + shopID + "").find(".shopOrderList").append(htmladd);
                                //Tắt popup
                                close_popup_ms();
                            }
                            removeLoading();


                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình thêm mã đơn, vui lòng thử lại sau.").show();
                        }
                    });
                }
            }
            function editOrderShopCode(id, shopID, shopName, MainOrderID) {
                var codetext = $("#ordershopcode-content").val();
                if (isEmpty(codetext)) {
                    $("#error-txt").html("Vui lòng nhập mã đơn hàng.").show();
                }
                else {
                    $("#error-txt").html("").hide();
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/editOrderShopCode",
                        data: "{id:'" + id + "',ordershopcode:'" + codetext + "',ShopID:'" + shopID + "',ShopName:'" + shopName + "',MainOrderID:'" + MainOrderID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "none") {
                                $("#error-txt").html("Có gián đoạn trong quá trình chỉnh sửa mã đơn, vui lòng thử lại sau.").show();
                            }
                            else if (ret == "exist") {
                                $("#error-txt").html("Mã đơn hàng đã tồn tại vui lòng kiểm tra lại.").show();
                            }
                            else {
                                //Sửa lại mã hiện có
                                $(".shoporder-item").each(function () {
                                    var oscid = $(this).attr("data-ordershopcodeid");
                                    if (oscid == id) {
                                        $(this).attr("data-ordershopcode", codetext);
                                        $(this).find(".ordershopcode-atag").html(codetext);
                                    }
                                });
                                //Tắt popup
                                close_popup_ms();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình chỉnh sửa mã đơn, vui lòng thử lại sau.").show();
                        }
                    });
                }
            }
            function deleteOrderShopCode(id) {
                var c = confirm("Bạn muốn xóa mã đơn hàng này và tất cả các kiện của đơn này?");
                if (c) {
                    addLoading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/orderdetail.aspx/deleteOrderShopCode",
                        data: "{id:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "none") {
                                $("#error-txt").html("Có gián đoạn trong quá trình xóa mã đơn, vui lòng thử lại sau.").show();
                            }
                            else if (ret == "notexist") {
                                $("#error-txt").html("Không tồn tại mã đơn hàng, vui lòng kiểm tra lại.").show();
                            }
                            else {
                                //Sửa lại mã hiện có
                                $(".shoporder-item").each(function () {
                                    var oscid = $(this).attr("data-ordershopcodeid");
                                    if (oscid == id) {
                                        $(this).remove();
                                    }
                                });
                                //Tắt popup
                                GetWeigthNew();
                                close_popup_ms();
                            }
                            removeLoading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            $("#error-txt").html("Có gián đoạn trong quá trình xóa mã đơn, vui lòng thử lại sau.").show();
                        }
                    });
                }
            }
            function deletesmallpackage(obj) {
                var c = confirm("Bạn muốn xóa mã đơn hàng này?");
                if (c) {
                    obj.parent().parent().remove();
                }
            }
            //End Gọi hàm xử lý

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
                fr += "             <a href=\"javascript:;\" class=\"btn btn-close btn-not-radius\" onclick='close_popup_ms()'>Đóng</a>";
                //fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick=\"addOrderShopCode('" + shopID + "', '" + MainOrderID + "')\">Thêm</a>";
                fr += button;
                fr += "         </div>";
                fr += "     </div>";
                fr += "<div class=\"popup_footer\">";
                //fr += "<span class=\"float-right\">" + email + "</span>";
                fr += "</div>";
                fr += "   </div>";
                fr += "</div>";
                //alert(fr);
                $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                $(fr).appendTo($(obj));
                setTimeout(function () {
                    $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                    $("#bg_popup").attr("onclick", "close_popup_ms()");
                }, 1000);
            }
            //End khu vực popup
            function updateFeeShipCN() {
                addLoading();
                var totalFee = 0;
                var totalFeeNDT = 0;
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                $(".feeshipcnshopcode").each(function () {
                    var fee = parseFloat($(this).val());
                    totalFeeNDT += fee;
                });
                totalFee = totalFeeNDT * currency;
                $("#<%=pCNShipFeeNDT.ClientID%>").val(totalFeeNDT);
                $("#<%=pCNShipFee.ClientID%>").val(totalFee);
                //$(".totalfeeshipnoidia").val(totalFeeNDT);
                removeLoading();
            }
        </script>
        <script type="text/javascript">
            function countFeeAdd(obj) {
                var root = obj.parent().parent();
                var id = root.attr("data-id");
                var checkis = false;
                if (obj.is(":checked")) {
                    checkis = true;
                }
                addLoading();
                $.ajax({
                    type: "POST",
                    url: "/admin/OrderDetail.aspx/setIsCensor",
                    data: "{id:'" + id + "',check:'" + checkis + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != "none") {
                            var data = JSON.parse(msg.d);
                            var moneyCYN = parseFloat(data.moneyCYN);
                            var moneyVND = parseFloat(data.moneyVND);
                            $("#<%=rAdditionFeeForSensorProductCYN.ClientID%>").val(moneyCYN);
                            $("#<%=rAdditionFeeForSensorProductVND.ClientID%>").val(moneyVND);
                        }
                        removeLoading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                        removeLoading();
                    }
                });
            }
        </script>
    </telerik:RadScriptBlock>
    <asp:HiddenField ID="hdfcount" runat="server" />
</asp:Content>
