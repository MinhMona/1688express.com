<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="addOrderWechat.aspx.cs" Inherits="NHST.Admin.addOrderWechat" %>

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
            <div id="content-order-detail" class="panel panel-white" style="position: relative;">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase" style="padding: 30px;">Tạo mới đơn hàng Wechat</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <main id="main-wrap">
                            <div class="sec order-detail-sec">
                                <div class="all">
                                    <div class="main">
                                        <div class="order-panels">
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
                                                    </dl>
                                                </div>
                                            </div>
                                            <div class="order-panel print2">
                                                <div class="title">Thông tin người nhận hàng</div>
                                                <div class="cont">
                                                    <dl>
                                                        <dt>Username</dt>
                                                        <dd>
                                                            <strong>
                                                                <asp:Label ID="lblUsername" runat="server" CssClass="form-control"></asp:Label>
                                                            </strong>
                                                        </dd>
                                                        <dt>Họ tên</dt>
                                                        <dd>
                                                            <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </dd>
                                                        <dt>Địa chỉ</dt>
                                                        <dd>
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </dd>
                                                        <dt>Email</dt>
                                                        <dd>
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </dd>
                                                        <dt>Số đt</dt>
                                                        <dd>
                                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </dd>
                                                        <dt>Ghi chú</dt>
                                                        <dd>
                                                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </dd>
                                                    </dl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-panel">
                                            <div class="form-row">
                                                <div class="table-rps table-responsive">
                                                    <table class="customer-table mar-top1 full-width normal-table">
                                                        <thead>
                                                            <tr>
                                                                <th style="width:10%;">Ảnh sản phẩm
                                                                </th>
                                                                <th>Link sản phẩm
                                                                </th>
                                                                <th>Tên sản phẩm
                                                                </th>
                                                                <th>Màu sắc, kích thước
                                                                </th>
                                                                <th>Giá
                                                                </th>
                                                                <th>Số lượng
                                                                </th>
                                                                <th>Ghi chú
                                                                </th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="product-list">
                                                            <tr class="product-item">
                                                                <td>
                                                                    <input type='file' class="productimage" onchange="imagepreview(this,$(this));" name="productimage1" />
                                                                    <img class="imgpreview" style="width: 40%;" alt="" />
                                                                    <a href="javascript:;" class="remove" style="display: none">Xóa</a>
                                                                </td>
                                                                <td>
                                                                    <input class="product-link form-control margin-custom-5" placeholder="Nhập link sản phẩm" /></td>
                                                                <td>
                                                                    <input class="product-name form-control margin-custom-5" placeholder="Nhập tên sản phẩm" /></td>
                                                                <td>
                                                                    <input class="product-colorsize form-control margin-custom-5" placeholder="Nhập màu sắc, kích thước" /></td>
                                                                <td>
                                                                    <input class="product-price form-control margin-custom-5" placeholder="Giá sản phẩm" /></td>
                                                                <td>
                                                                    <input class="product-quantity form-control margin-custom-5" placeholder="Số lượng" type="number" value="1" min="0" /></td>
                                                                <td>
                                                                    <input class="product-request form-control margin-custom-5" placeholder="Ghi chú" /></td>
                                                                <td>
                                                                    <%--<a href="javascript:;" onclick="deleteProduct($(this))" class="btn btn-bg-close"><i class="fa fa-close"></i></a>--%></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <a href="javascript:;" onclick="addProductRow()" class="btn btn-bg-submit" style="float: right; margin-right: 5px; width: auto;">
                                                    <i class="fa fa-plus"></i>Thêm sản phẩm</a>
                                            </div>
                                            <%--<div class="title">
                                                Thêm sản phẩm
                                                
                                            </div>
                                            <div class="cont" style="overflow-x: scroll">
                                                <dl class="form-insert">
                                                    <dt>Link ảnh sản phẩm</dt>
                                                    <dd>
                                                        <input id="txtProductIMGLink" class="form-control" />
                                                    </dd>
                                                    <dt>Link sản phẩm</dt>
                                                    <dd>
                                                        <input id="txtProductLink" class="form-control" />
                                                    </dd>
                                                    <dt>Tên sản phẩm</dt>
                                                    <dd>
                                                        <input id="txtProductName" class="form-control" />
                                                    </dd>
                                                    <dt>Thuộc tính sản phẩm</dt>
                                                    <dd>
                                                        <input id="txtProductVariable" class="form-control" />
                                                    </dd>
                                                    <dt>Giá mặc định (tệ)</dt>
                                                    <dd>
                                                        <input id="txtPrice" class="form-control" />
                                                    </dd>
                                                    <dt>Giá khuyến mãi (tệ)</dt>
                                                    <dd>
                                                        <input id="txtPricePromotion" class="form-control" />
                                                    </dd>
                                                    <dt>Số lượng</dt>
                                                    <dd>
                                                        <input id="txtQuantity" class="form-control" />
                                                    </dd>
                                                    <dt>Ghi chú</dt>
                                                    <dd>
                                                        <input id="txtProductNote" class="form-control" />
                                                    </dd>
                                                    <dt></dt>
                                                    <dd>
                                                        <a href="javascript:;" onclick="addProduct()" id="btnAddProduct" class="btn pill-btn primary-btn admin-btn" style="margin-bottom: 10px; float: left">Thêm sản phẩm</a>
                                                        <a href="javascript:;" onclick="editProduct($(this))" id="btnEditProduct" class="btn pill-btn primary-btn " style="display: none; margin-bottom: 10px; margin-right: 10px; float: left; padding: 0 20px;">Cập nhật</a>
                                                        <a href="javascript:;" onclick="cancelEditProduct($(this))" id="btnCancelEditProduct" class="btn pill-btn primary-btn " style="display: none; margin-bottom: 10px; float: left; padding: 0 20px;">Xong</a>
                                                    </dd>
                                                </dl>
                                                <table class="tb-product">
                                                    <thead>
                                                        <tr>
                                                            <th class="pro">Sản phẩm</th>
                                                            <th class="pro">Thuộc tính</th>
                                                            <th class="qty">Số lượng</th>
                                                            <th class="price">Giá bán thật</th>
                                                            <th class="price">Giá bán khuyến mãi</th>
                                                            <th class="price">Ghi chú</th>
                                                            <th class="tool"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="product-list">
                                                    </tbody>
                                                </table>
                                            </div>--%>
                                        </div>
                                        <div class="order-panels">
                                            <a href="javascript:;" onclick="AddOrder()" class="btn pill-btn primary-btn admin-btn" style="margin: 0 auto;">Tạo mới đơn hàng</a>
                                            <%--<a href="javascript:;" onclick="createOrder()" class="btn pill-btn primary-btn admin-btn" style="margin: 0 auto;">Tạo mới đơn hàng</a>--%>
                                            <asp:Button ID="btnCreate" runat="server" CssClass="btn pill-btn primary-btn admin-btn" Text="Tạo mới đơn hàng"
                                                OnClick="btnCreate_Click" Style="display: none;" />
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
    <asp:HiddenField ID="hdfListProduct" runat="server" />
    <asp:HiddenField ID="hdfCurrency" runat="server" />
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <asp:HiddenField ID="hdfcountimage" runat="server" Value="1" />
    <script type="text/javascript">
        function AddOrder()
        {
            if ($(".product-item").length > 0) {
                var html = "";
                var check = false;
                $(".product-item").each(function () {
                    var item = $(this);
                    var productlink_obj = item.find(".product-link");
                    var productlink = item.find(".product-link").val();

                    var productname_obj = item.find(".product-name");
                    var productname = item.find(".product-name").val();

                    var productsizecolor_obj = item.find(".product-colorsize");
                    var productsizecolor = item.find(".product-colorsize").val();

                    var productprice_obj = item.find(".product-price");
                    var productprice = item.find(".product-price").val();

                    var productquantity_obj = item.find(".product-quantity");
                    var productquantity = item.find(".product-quantity").val();
                    var productquantityfloat = parseFloat(item.find(".product-quantity").val());

                    var productrequest = item.find(".product-request").val();
                    //if (isBlank(productlink)) {
                    //    check = true;
                    //}
                    if (isBlank(productname)) {
                        check = true;
                    }
                    //if (isBlank(productsizecolor)) {
                    //    check = true;
                    //}
                    if (isBlank(productprice)) {
                        check = true;
                    }
                    if (isBlank(productquantity)) {
                        check = true;
                    }
                    else if (productquantityfloat <= 0) {
                        check = true;
                    }

                    //validateText(productlink_obj);
                    validateText(productname_obj);
                    //validateText(productsizecolor_obj);
                    validateText(productquantity_obj);
                    validateText(productprice_obj);
                    validateNumber(productquantity_obj);
                });
                if (check == true) {
                    alert('Vui lòng điền đầy đủ thông tin từng sản phẩm');
                }
                else {
                    $(".product-item").each(function () {
                        var item = $(this);
                        var productlink = item.find(".product-link").val();
                        var productname = item.find(".product-name").val();
                        var productsizecolor = item.find(".product-colorsize").val();
                        var productprice = item.find(".product-price").val();
                        var productquantity = item.find(".product-quantity").val();
                        var productrequest = item.find(".product-request").val();
                        var image = item.find(".productimage").attr("name");
                        
                        html += productlink + "]" + productname + "]" + productsizecolor
                            + "]" + productprice + "]" + productquantity
                            + "]" + productrequest + "]" + image + "|";
                    });
                    $("#<%=hdfProductList.ClientID%>").val(html);
                    $("#<%=btnCreate.ClientID%>").click();
                }
            }
            else {
                alert('Vui lòng nhập sản phẩm');
            }
        }
        function addProductRow() {
            var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
            countimage += 1;
            $("#<%= hdfcountimage.ClientID %>").val(countimage);
            
            var html = "";
            html += "<tr class=\"product-item\">";
            html += "   <td><input type='file' class=\"productimage\" onchange=\"imagepreview(this,$(this));\" name=\"productimage" + countimage + "\"/>"
                + "         <img class=\"imgpreview\" style=\"width: 40%;\"/>"
                + "         <a href=\"javascript:;\" class=\"remove\" style=\"display:none\">Xóa</a>"
                + "     </td>";
            html += "   <td><input class=\"product-link form-control margin-custom-5\" placeholder=\"Nhập link sản phẩm\"/></td>";
            html += "   <td><input class=\"product-name form-control margin-custom-5\" placeholder=\"Nhập tên sản phẩm\"/></td>";
            html += "   <td><input class=\"product-colorsize form-control margin-custom-5\" placeholder=\"Nhập màu sắc, kích thước\"/></td>";
            html += "   <td><input class=\"product-price form-control margin-custom-5\" placeholder=\"Giá sản phẩm\"/></td>";
            html += "   <td><input class=\"product-quantity form-control margin-custom-5\" placeholder=\"Số lượng\" type=\"number\" value=\"1\" min=\"0\"/></td>";
            html += "   <td><input class=\"product-request form-control margin-custom-5\" placeholder=\"Ghi chú\"/></td>";
            html += "   <td><a href=\"javascript:;\" onclick=\"deleteProduct($(this))\" class=\"btn btn-bg-close\"><i class=\"fa fa-close\"></i></a></td>";
            html += "</tr>";
            $(".product-list").append(html);
            checkShowButton();
        }
        function imagepreview(input, obj) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    obj.parent().find('.imgpreview').attr('src', e.target.result);
                    //$('.imgpreview').attr('src', e.target.result);

                    $(".remove").show();
                    obj.parent().find(".remove").click(function () {
                        obj.parent().find('.imgpreview').attr('src', "");
                        obj.parent().find('input:file').val("");
                        $(this).hide();
                    });
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        function checkShowButton() {
            if ($(".product-item").length > 0) {
                $("#deleteAllProduct").show();
            }
            else {
                $("#deleteAllProduct").hide();
            }
        }
        function deleteProduct(obj) {
            var c = confirm('Bạn muốn xóa sản phẩm này?');
            if (c == true) {
                var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
                if (countimage > 0)
                {
                    countimage = countimage - 1;
                    $("#<%=hdfcountimage.ClientID%>").val(countimage);
                }
                obj.parent().parent().remove();
            }
            checkShowButton();
        }
        function deleteAllProduct() {
            var c = confirm('Bạn muốn xóa tất cả sản phẩm?');
            if (c == true) {
                $(".product-item").remove();
                $("#<%=hdfcountimage.ClientID%>").val("0");
            }
            checkShowButton();
        }
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
        function validateText(obj) {
            var value = obj.val();
            if (isBlank(value)) {
                obj.addClass("border-select");
            }
            else {
                obj.removeClass("border-select");
            }
        }
        function validateNumber(obj) {
            var value = parseFloat(obj.val());
            if (value <= 0)
                obj.addClass("border-select");
            else
                obj.removeClass("border-select");
        }
        function isBlank(str) {
            return (!str || /^\s*$/.test(str));
        }
        function generate() {
            var text = "";
            var possible = "0123456789";
            for (var i = 0; i < 5; i++)
                text += possible.charAt(Math.floor(Math.random() * possible.length));

            return text;
        }
    </script>
    <script type="text/javascript">
        <%--function createOrder() {
            if ($(".product-item-add").length > 0) {
                var html = "";
                $(".product-item-add").each(function () {
                    var root = $(this);
                    var productlink = root.find(".linkproduct-img").attr("href");
                    var productimage = root.find(".productimg").attr("src");
                    var productname = root.find(".linkproduct-name").html();
                    var productvariable = root.find(".productvariable").html();
                    var productquantity = root.find(".productquantity").html();
                    var productprice = root.find(".productprice").html();
                    var productpromotionprice = root.find(".productpromotionprice").html();
                    var productnote = root.find(".productnote").html();

                    html += productlink + "]" + productimage + "]" + productname + "]" + productvariable + "]" + productquantity + "]" + productprice + "]"
                         + productpromotionprice + "]" + productnote + "|";
                });
                $("#<%=hdfListProduct.ClientID%>").val(html);
                $("#<%=btnCreate.ClientID%>").click();
            }
            else {
                alert("Vui lòng thêm sản phẩm vào đơn hàng");
            }
        }--%>

        //function isEmpty(str) {
        //    return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        //}
        //var idcount = 1;
        //function addProduct() {
        //    var imglink = $("#txtProductIMGLink").val();
        //    var productlink = $("#txtProductLink").val();
        //    var productname = $("#txtProductName").val();
        //    var productvariable = $("#txtProductVariable").val();
        //    var price = $("#txtPrice").val();
        //    var promotionprice = $("#txtPricePromotion").val();
        //    var quantity = $("#txtQuantity").val();
        //    var note = $("#txtProductNote").val();
        //    var check = true;
        //    if (isBlank(imglink)) {
        //        check = false;
        //        alert('Nhập link ảnh sản phẩm');
        //    }
        //    else if (isBlank(productlink)) {
        //        check = false;
        //        alert('Nhập link sản phẩm');
        //    }
        //    else if (isBlank(productname)) {
        //        check = false;
        //        alert('Nhập tên sản phẩm');
        //    }
        //    else if (isBlank(price)) {
        //        check = false;
        //        alert('Nhập giá sản phẩm');
        //    }
        //    else if (isBlank(promotionprice)) {
        //        check = false;
        //        alert('Nhập giá khuyến mãi sản phẩm');
        //    }
        //    else if (isBlank(quantity)) {
        //        check = false;
        //        alert('Nhập số lượng sản phẩm');
        //    }

        //    if (check == true) {
        //        var html = "";
        //        html += "<tr id=\"product_" + idcount + "\" class=\"product-item-add\">";
        //        html += "   <td class=\"pro\">";
        //        html += "       <div class=\"thumb-product\">";
        //        html += "           <div class=\"pd-img\">";
        //        html += "               <a href=\"" + productlink + "\" class=\"linkproduct-img\" target=\"_blank\">";
        //        html += "                   <img src=\"" + imglink + "\" alt class=\"productimg\"/>";
        //        html += "               </a>";
        //        html += "           </div>";
        //        html += "           <div class=\"info\">";
        //        html += "               <a href=\"" + productlink + "\" target=\"_blank\" class=\"linkproduct-name\">";
        //        html += productname;
        //        html += "               </a>";
        //        html += "           </div>";
        //        html += "       </div>";
        //        html += "     </td>";
        //        html += "   <td class=\"productvariable\">" + productvariable + "</td>";
        //        html += "   <td class=\"productquantity\">" + quantity + "</td>";
        //        html += "   <td class=\"productprice\">" + price + "</td>";
        //        html += "   <td class=\"productpromotionprice\">" + promotionprice + "</td>";
        //        html += "   <td class=\"productnote\">" + note + "</td>";
        //        html += "   <td class=\"pro\">"
        //             + "        <a href=\"javascript:;\" class=\"btn primary-btn\" style=\"padding-top:0;\" onclick=\"deleteRow($(this))\">Xóa</a><br/><br/>"
        //             + "        <a href=\"javascript:;\" class=\"btn primary-btn\" style=\"padding-top:0;\" onclick=\"editRow($(this))\">Sửa</a>"
        //             + "    </td>";

        //        html += "</tr>";
        //        $(".product-list").append(html);
        //        resetForm();
        //        idcount++;
        //    }
        //}
        //function editRow(obj) {
        //    var root = obj.parent().parent();
        //    var productlink = root.find(".linkproduct-img").attr("href");
        //    var productimage = root.find(".productimg").attr("src");
        //    var productname = root.find(".linkproduct-name").html();
        //    var productvariable = root.find(".productvariable").html();
        //    var productquantity = root.find(".productquantity").html();
        //    var productprice = root.find(".productprice").html();
        //    var productpromotionprice = root.find(".productpromotionprice").html();
        //    var productnote = root.find(".productnote").html();

        //    $("#txtProductIMGLink").val(productimage);
        //    $("#txtProductLink").val(productlink);
        //    $("#txtProductName").val(productname);
        //    $("#txtProductVariable").val(productvariable);
        //    $("#txtPrice").val(productprice);
        //    $("#txtPricePromotion").val(productpromotionprice);
        //    $("#txtQuantity").val(productquantity);
        //    $("#txtProductNote").val(productnote);


        //    var currentID = root.attr("id");
        //    $(".product-item-add").attr("style", "background-color:none");
        //    root.attr("style", "background-color:#4f9eff");


        //    $(".form-insert").attr("data-id", currentID);
        //    $(".form-insert").find("#btnEditProduct").show();
        //    $(".form-insert").find("#btnCancelEditProduct").show();
        //    $(".form-insert").find("#btnAddProduct").hide();
        //}
        //function editProduct(obj) {
        //    var productID = $(".form-insert").attr("data-id");
        //    var imglink = $("#txtProductIMGLink").val();
        //    var productlink = $("#txtProductLink").val();
        //    var productname = $("#txtProductName").val();
        //    var productvariable = $("#txtProductVariable").val();
        //    var price = $("#txtPrice").val();
        //    var promotionprice = $("#txtPricePromotion").val();
        //    var quantity = $("#txtQuantity").val();
        //    var note = $("#txtProductNote").val();
        //    var check = true;
        //    if (isBlank(imglink)) {
        //        check = false;
        //        alert('Nhập link ảnh sản phẩm');
        //    }
        //    else if (isBlank(productlink)) {
        //        check = false;
        //        alert('Nhập link sản phẩm');
        //    }
        //    else if (isBlank(productname)) {
        //        check = false;
        //        alert('Nhập tên sản phẩm');
        //    }
        //    else if (isBlank(price)) {
        //        check = false;
        //        alert('Nhập giá sản phẩm');
        //    }
        //    else if (isBlank(promotionprice)) {
        //        check = false;
        //        alert('Nhập giá khuyến mãi sản phẩm');
        //    }
        //    else if (isBlank(quantity)) {
        //        check = false;
        //        alert('Nhập số lượng sản phẩm');
        //    }

        //    if (check == true) {
        //        var root = $("#" + productID);
        //        root.find(".linkproduct-img").attr("href", productlink);
        //        root.find(".productimg").attr("src", imglink);
        //        root.find(".linkproduct-name").html(productname);
        //        root.find(".productvariable").html();
        //        root.find(".productquantity").html(quantity);
        //        root.find(".productprice").html(price);
        //        root.find(".productpromotionprice").html(promotionprice);
        //        root.find(".productnote").html(note);
        //    }
        //}
        //function cancelEditProduct(obj) {
        //    var productid = $(".form-insert").attr("data-id");
        //    $(".form-insert").removeAttr("data-id");
        //    $(".form-insert").find("#btnEditProduct").hide();
        //    $(".form-insert").find("#btnCancelEditProduct").hide();
        //    $(".form-insert").find("#btnAddProduct").show();
        //    resetForm();
        //    $("#" + productid).attr("style", "background-color:none");
        //}
        //function deleteRow(obj) {
        //    var c = confirm('Bạn muốn xóa sản phẩm này?');
        //    if (c) {
        //        obj.parent().parent().remove();
        //    }
        //}
        //function resetForm() {
        //    $("#txtProductIMGLink").val("");
        //    $("#txtProductLink").val("");
        //    $("#txtProductName").val("");
        //    $("#txtProductVariable").val("");
        //    $("#txtPrice").val("");
        //    $("#txtPricePromotion").val("");
        //    $("#txtQuantity").val("");
        //    $("#txtProductNote").val("");
        //}
        
    </script>
</asp:Content>
