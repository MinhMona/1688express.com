<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="NHST.Admin.ProductEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Chỉnh sửa giá sản phẩm</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Tên sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblBrandname" runat="server" CssClass="form-control has-validate"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Link ảnh sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtProductImgLink" runat="server" CssClass="form-control has-validate"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Link sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtProductLink" runat="server" CssClass="form-control has-validate"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Giá sản phẩm CYN (<i class="fa fa-yen"></i>)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pProductPriceOriginal" NumberFormat-DecimalDigits="2"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pProductPriceOriginal" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Giá mua thực tế (<i class="fa fa-yen"></i>)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pRealPrice" NumberFormat-DecimalDigits="2"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pRealPrice" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Số lượng
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pQuanity" NumberFormat-DecimalDigits="0" MinValue="0"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pQuanity" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Ghi chú riêng sản phẩm
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtproducbrand" runat="server" CssClass="form-control width-notfull"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Trạng thái
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Còn hàng</asp:ListItem>
                                        <asp:ListItem Value="2">Hết hàng</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group marbot1" style="display:none">
                                    Mã đơn hàng
                                </div>
                                <div class="form-group marbot2" style="display: none">
                                    <asp:TextBox ID="txtOrderShopCode" runat="server" CssClass="form-control width-notfull"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1" style="display: none">
                                    <div class="order-panel " style="margin-left: 0;">
                                        <div class="title">Danh sách mã vận đơn</div>
                                        <div id="transactioncodeList" class="cont">
                                            <asp:Literal ID="ltrCodeList" runat="server"></asp:Literal>
                                        </div>
                                        <div class="ordercode addordercode"><a href="javascript:;" onclick="addCodeTransaction()">Thêm mã vận đơn</a></div>
                                    </div>
                                </div>
                                <div class="form-group no-margin">
                                    <a href="javascript:;" onclick="UpdateOrder()" class="btn btn-success btn-block small-btn">Cập nhật</a>
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btncreateuser_Click" Style="display: none" />
                                    <asp:Literal ID="ltrback" runat="server"></asp:Literal>
                                    <%--<a href="javascript:;" onclick="createuser()" class="btn btn-success btn-block small-btn right-btn">Tạo tài khoản</a>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfcurrent" runat="server" />
        <asp:HiddenField ID="hdfCodeTransactionList" runat="server" />
        <asp:HiddenField ID="hdfOrderType" runat="server" />
        <asp:HiddenField ID="hdfUserRole" runat="server" />
        <asp:HiddenField ID="hdfOrderID" runat="server" />        
        <asp:HiddenField ID="hdfFeeBuyProDiscount" runat="server" />
        <asp:HiddenField ID="hdfFeeWeightDiscount" runat="server" />
        <asp:HiddenField ID="hdfFeeweightPriceDiscount" runat="server" />
        <asp:HiddenField ID="hdfReceivePlace" runat="server" />
        <asp:HiddenField ID="hdfShippingType" runat="server" />
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="rc" runat="server">
        <script type="text/javascript">
            function keypress(e) {
                var keypressed = null;
                if (window.event) {
                    keypressed = window.event.keyCode; //IE
                }
                else {
                    keypressed = e.which; //NON-IE, Standard
                }
                if (keypressed < 48 || keypressed > 57) {
                    if (keypressed == 8 || keypressed == 127) {
                        return;
                    }
                    return false;
                }
            }
            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }
            var currency = $("#<%=hdfcurrent.ClientID%>").val();
            function price(type) {
                var shipfeendt = $("#<%= pProductPriceOriginal.ClientID%>").val();
                var shipfeevnd = $("#<%= pRealPrice.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= pProductPriceOriginal.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pProductPriceOriginal.ClientID%>").val(0);
                        $("#<%= pRealPrice.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= pRealPrice.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pProductPriceOriginal.ClientID%>").val(0);
                        $("#<%= pRealPrice.ClientID%>").val(0);
                    }
                }
            }
            function UpdateOrder() {
                //btnUpdate
                var list = "";
                $(".order-versionnew").each(function () {
                    var id = $(this).attr("data-packageID");
                    var code = $(this).find(".transactionCode").val();
                    var weight = $(this).find(".transactionWeight").val();
                    var status = $(this).find(".transactionCodeStatus").val();
                    list += id + "," + code + "," + weight + "," + status + "|";
                });
                $("#<%=hdfCodeTransactionList.ClientID%>").val(list);
                $("#<%=btncreateuser.ClientID%>").click();
            }
            function addCodeTransaction() {
                var orderType = $("#<%=hdfOrderType.ClientID%>").val();
                var role = parseFloat($("#<%=hdfUserRole.ClientID%>").val());
                var html = "";
                if (orderType == "2") {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/ProductEdit.aspx/getCode",
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
                            html += "<div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>";
                            html += "</div>";
                            $("#transactioncodeList").append(html);
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
                    html += "<div class=\"item-element\"><a href=\"javascript:;\" class=\"btn-not-radius\" onclick=\"deleteOrderCode($(this))\">Xóa</a></div>";
                    html += "</div>";
                    $("#transactioncodeList").append(html);
                }
            }
            function deleteOrderCode(obj) {
                var r = confirm("Bạn muốn xóa mã vận đơn này?");
                if (r == true) {
                    var id = obj.parent().parent().attr("data-packageID");
                    $.ajax({
                        type: "POST",
                        url: "/Admin/ProductEdit.aspx/DeleteSmallPackage",
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
            function gettotalweight2() {
                <%--addLoading();
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
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        removeLoading();
                        alert('Vui lòng nhập số vào cân nặng');
                    }
                });--%>

            }
        </script>
    </telerik:RadScriptBlock>
    <style>
        .order-panel {
            float: left;
            width: 100%;
            margin-bottom: 30px;
            padding: 10px;
            line-height: 1.6;
            box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.2);
        }

            .order-panel .cont {
                display: block;
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

        .order-panel .title {
            text-transform: uppercase;
            color: #2b2e4a;
            font-weight: bold;
            font-size: 16px;
            padding-bottom: 5px;
            border-bottom: solid 1px rgba(232, 69, 69, 0.3);
            margin-bottom: 10px;
        }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url(/App_Themes/NewUI/images/icon-plus.png) center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }
    </style>
</asp:Content>
