<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="transportationdetail.aspx.cs" Inherits="NHST.Admin.transportationdetail" %>

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
    </style>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">
                        <span id="main_lblOrderType" style="margin-left: 20px;">Chi tiết đơn hàng</span>
                    </h3>
                </div>
                <div class="panel-body">
                    <a href="javascript:;" onclick="printDiv()" class="btn primary-btn" style="display: none">In đơn hàng</a>
                    <asp:Literal ID="ltrPrint" runat="server"></asp:Literal>
                    <div class="order-panels">
                        <div class="order-panel">
                            <div class="title">Danh sách mã</div>
                            <div class="cont">
                                <div class="table-responsive">
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th style="">ID</th>
                                                <th style="width: 300px;">Mã vận đơn</th>
                                                <th>Cân nặng</th>
                                                <th>Trạng thái</th>
                                                <%--<th style="width: 150px"></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal ID="ltrPackages" runat="server"></asp:Literal>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="order-panels">
                        <div class="order-panel">
                            <div class="title">Thông tin đơn hàng</div>
                            <dl style="width:50%;">
                                <dt>Tổng số kiện</dt>
                                <dd>
                                    <asp:Label ID="lblTotalPackage" runat="server"></asp:Label></dd>
                                <dt>Tổng cân nặng</dt>
                                <dd>
                                    <asp:Label ID="lblTotalWeight" runat="server"></asp:Label>
                                    kg</dd>
                                <dt>Tổng tiền</dt>
                                <dd>
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                        ID="rTotalPriceCYN" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="countPrice('cyn')"
                                        NumberFormat-GroupSizes="3">
                                    </telerik:RadNumericTextBox>
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                        ID="rTotalPrice" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="countPrice('vnd')"
                                        NumberFormat-GroupSizes="3" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                    vnđ
                                </dd>
                                <dd>
                                    <asp:Label ID="Label1" runat="server"></asp:Label></dd>
                                <dt>Trạng thái đơn hàng</dt>
                                <dd>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đang xử lý"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Đang vận chuyển về kho đích"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Đã về kho đích"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Đã thanh toán"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Đã hoàn thành"></asp:ListItem>
                                    </asp:DropDownList>
                                </dd>
                                <dt>Nhận hàng tại</dt>
                                <dd>
                                    <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="returnWeightFee()"
                                        DataValueField="ID" DataTextField="WareHouseName">
                                    </asp:DropDownList>
                                </dd>
                                <dt>Phương thức vận chuyển</dt>
                                <dd>
                                    <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control"
                                        onchange="returnWeightFee()"
                                        DataValueField="ID" DataTextField="ShippingTypeName">
                                        <asp:ListItem Value="1" Text="Đi nhanh"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đi thường"></asp:ListItem>
                                    </asp:DropDownList>
                                </dd>
                                <dt><strong>Thanh toán</strong></dt>
                                <dd></dd>
                                <dt>Số tiền đã trả</dt>
                                <dd>
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control chiphi-it" Skin="MetroTouch"
                                        ID="pDeposit" MinValue="0" NumberFormat-DecimalDigits="0" Enabled="false"
                                        NumberFormat-GroupSizes="3">
                                    </telerik:RadNumericTextBox>
                                    vnd    
                                </dd>
                                <dt>
                                    <asp:Literal ID="ltrBtnUpdate" runat="server"></asp:Literal>

                                </dt>
                                <dd>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn" Text="CẬP NHẬT" OnClick="btnUpdate_Click" style="float:left;"/>
                                    <a href="/manager/transportation-list" class="btn pill-btn primary-btn admin-btn" style="float: left; clear: none; margin-left: 20px;">Trở về</a>
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="printcontent" class="printdetail" style="display: none;">
    </div>
    <asp:HiddenField ID="hdfStatus" runat="server" />
    <asp:HiddenField ID="hdfCurrency" runat="server" />

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
            margin-bottom: 15px;
        }

        select.form-control {
            line-height: 25px;
        }

        /*.riSingle {
            width: 40% !important;
        }*/

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
                width: 25%;
                padding: 0 10px;
            }

        .addordercode {
            /*padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;*/
        }

            .addordercode a {
                padding-left: 10px;
            }

        .title-fee {
            float: left;
            width: 100;
            er-bott m: sli f 2 margin colo : #000;
            .bg- kground: #ea028 color nhst .title r: #fff;
        }

        r title {
            border-bottom: solid 1px #fff;
        }
    </style>

    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function add_loading() {
                $(".page-inner").append("<div class='loading_bg'></div>");
                var height = $(".page-inner").height();
                $(".loading_bg").css("height", height + "px");
            }
            function remove_loading() {
                $(".loading_bg").remove();
            }
            function returnWeightFee() {
                var status = parseFloat($("#<%=ddlStatus.ClientID%>").val());
                if (status > 1) {

                    var totalweight = 0;
                    var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                    var warehouseFrom = "1";
                    var warehouseTo = $("#<%= ddlReceivePlace.ClientID%>").val();
                    var shippingType = $("#<%=ddlShippingType.ClientID%>").val();
                    if ($(".package-item").length > 0) {
                        $(".package-item").each(function () {
                            totalweight += parseFloat($(this).attr("data-weight"));
                        });
                    }
                    add_loading();
                    $.ajax({
                        type: "POST",
                        url: "/admin/transportationdetail.aspx/getPrice",
                        data: "{weight:'" + totalweight + "',warehouseFrom:'" + warehouseFrom + "',warehouseTo:'" + warehouseTo + "',shippingType:'" + shippingType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = parseFloat(msg.d);
                            var totalpriceVND = data * currency;
                            $("#<%= rTotalPriceCYN.ClientID%>").val(data);
                            $("#<%= rTotalPrice.ClientID%>").val(totalpriceVND);
                            remove_loading();
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
            }
            function countPrice(type) {
                var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
                if (type == "cyn") {
                    var totalprice = parseFloat($("#<%= rTotalPriceCYN.ClientID%>").val());
                    var totalpriceVND = totalprice * currency;
                    $("#<%= rTotalPrice.ClientID%>").val(totalpriceVND);
                }
                else {
                    var totalpriceVND = parseFloat($("#<%= rTotalPrice.ClientID%>").val());
                    var totalpriceCYN = totalpriceVND / currency;
                    $("#<%= rTotalPriceCYN.ClientID%>").val(totalpriceCYN);
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
