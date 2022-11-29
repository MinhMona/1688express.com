﻿<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="VNWarehouse.aspx.cs" Inherits="NHST.manager.VNWarehouse" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .package-detail {
            float: left;
            border: dashed 1px #000;
            padding: 10px;
            margin-bottom: 30px;
            min-height: 240px;
        }

        .row-package {
            float: left;
            width: 100%;
            margin-bottom: 10px;
        }

        .package-label {
            float: left;
            width: 45%;
        }

        .width-50-per {
            width: 100%;
        }

        .custom-small-button {
            width: 45% !important;
        }

        .hidden-btn {
            display: none;
        }

        #outall-package {
            margin: 20px 0;
            float: left;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-100 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Kiểm kho Việt Nam</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            <label>Barcode</label>

                                        </div>
                                        <div class="form-row marbot1">
                                            <input id="barcode-input" class="form-control barcode-area width-50-per" type="text" oninput="getCode($(this))" />
                                        </div>
                                        <div class="form-row marbot1">
                                            <div class="row error-outstock" style="margin-bottom: 30px;">
                                            </div>
                                            <div class="row listpack">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <asp:HiddenField ID="hdfTotalPrice" runat="server" />
        <div class="userlist-outstock" style="display: none">
        </div>
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
    <asp:HiddenField ID="hdfListBarcode" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#barcode-input').focus();
        });
        function handleKeyPress(evt) {
            var nbr = (window.event) ? event.keyCode : evt.which;
            if (nbr == 13) {
                return false;
            }
        }
        document.onkeydown = handleKeyPress
        function getCode(obj) {

            var bc = obj.val();
            var bc_e = obj.val() + ",bc-" + obj.val();
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();


            var bs = listbarcode.split('|');
            var check = false;
            for (var i = 0; i < bs.length - 1; i++) {
                if (bc_e == bs[i]) {
                    check = true;
                }
            }
            if (check == false) {
                $.ajax({
                    type: "POST",
                    url: "/manager/VNWareHouse.aspx/GetCode",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);

                        if (data != "none") {
                            var UID = data.UID;
                            var Wallet = data.Wallet;
                            var check = false;
                            $(".user-out").each(function () {
                                if ($(this).attr("data-uid") == UID) {
                                    check = true;
                                }
                            })
                            if (check == false) {
                                var uhtml = "<div class=\"user-out\" data-uid=\"" + UID + "\" data-username=\"" + data.Username + "\" data-wallet=\"" + Wallet + "\" ></div>";
                                $(".userlist-outstock").append(uhtml);
                            }

                            var html = '';
                            html += "<div class=\"col-md-4 package-item\" >";
                            html += "   <div id=\"bc-" + data.BarCode + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\"><strong>Username:</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Username + "</strong></span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Kiểm đếm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                            //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Tên khách:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Fullname + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Email:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Email + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Phone:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Phone + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Địa chỉ:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.Address + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package status-pack\">";
                            html += "           <span class=\"package-label\">Trạng thái:</span>";
                            html += "           <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                            html += "       </div>";

                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Trọng lượng:</span>";
                            html += "           <span class=\"package-info packageWeight\">" + data.TotalWeight + " kg</span>";
                            html += "       </div>";

                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\">Tổng tiền:</span>";
                            //html += "           <span class=\"package-info packageWeight\">" + data.TotalPriceVND + " vnđ</span>";
                            //html += "       </div>";
                            //html += "       <div class=\"row-package status-pack\">";
                            //html += "           <span class=\"package-label\">Trạng thái:</span>";
                            //if (data.Status < 3)
                            //    html += "       <span class=\"package-info packageStatus bg-red\">Chưa về</span>";
                            //else if (data.Status == 3)
                            //    html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về</span>";
                            //else if (data.Status == 4)
                            //    html += "       <span class=\"package-info packageStatus bg-blue\">Đã giao</span>";
                            //html += "       </div>";
                            html += "       <div class=\"row-package \">";
                            //if (status > 1 && status < 4) {
                            //    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                            //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                            //}
                            //else
                            //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                            html += "       </div>";
                            html += "       <div class=\"row-package-status \">";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";

                            listbarcode += data.BarCode + ",bc-" + data.BarCode + "|";

                            $(".listpack").append(html);
                            $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                            obj.val("");
                            $("#outall-package").show();
                            countOrder();
                        }
                        else {
                            alert('Không tìm thấy');

                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            } else {
                obj.val("");
            }

        }
        function countOrder() {
            //countorder
            var count = $(".package-detail").length;
            $("#countorder").html(count);
        }
        function add_loading() {
            $(".page-inner").append("<div class='loading_bg'></div>");
            var height = $(".page-inner").height();
            $(".loading_bg").css("height", height + "px");
        }
        function updateWeight(barcode, id, obj) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            add_loading();
            $.ajax({
                type: "POST",
                url: "/manager/VNWareHouse.aspx/UpdateStatus",
                data: "{barcode:'" + barcode + "',status:'" + status + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == "1") {
                        $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                    }
                    remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }
        function xuatkho(Barcode, id) {
            add_loading();
            //alert(Barcode);
            $.ajax({
                type: "POST",
                url: "/manager/OutStock.aspx/SetFinish",
                data: "{barcode:'" + Barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == "ok") {
                        //obj.parent().parent().find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                        //obj.parent().parent().find(".xuatkhobutton").remove();
                        //obj.parent().parent().find(".huyxuatkhobutton").remove();

                        //obj.parent().find(".status-pack").html("<span class=\"package-info packageStatus\">Đã giao</span>");

                        $("#" + id).find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                        $("#" + id).find(".xuatkhobutton").remove();
                        $("#" + id).find(".huyxuatkhobutton").remove();
                        $("#" + id).find('.row-package-status').html('Xuất kho thành công.').attr("style", "color:blue");
                    }
                    else if (data != "ok" && data != "none") {

                        $("#" + id).find('.row-package-status').html('Đơn hàng barcode: ' + Barcode + '<br/> Còn thiếu: ' + data + ' VNĐ để nhận hàng.');

                        //swal
                        //(
                        //    {
                        //        title: 'Thông báo',
                        //        text: 'Đơn hàng barcode: ' + Barcode + ', còn thiếu: ' + data + ' VNĐ để nhận hàng',
                        //        type: 'error'
                        //    }
                        //    //function () { window.location.replace(window.location.href); }
                        //);
                    }
                    remove_loading();
                    countOrder();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

        }
        function huyxuatkho(barcode, obj) {
            var r = confirm("Bạn muốn tắt package này?");
            if (r == true) {
                var id = barcode + "|";
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                obj.parent().parent().parent().remove();
                if ($(".package-item").length == 0) {
                    $("#outall-package").hide();
                }
                countOrder();
            } else {

            }

        }
        function xuatkhoall() {
            var r = confirm("Bạn muốn xuất kho tất cả đơn hàng này?");
            if (r == true) {
                $(".error-outstock").html("");
                add_loading();
                if ($(".user-out").length > 0) {
                    $(".user-out").each(function () {
                        var UID = $(this).attr("data-uid");
                        var Username = $(this).attr("data-username");
                        $.ajax({
                            type: "POST",
                            url: "/manager/OutStock.aspx/GetWallet",
                            data: "{UID:'" + UID + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = msg.d;
                                var userWallet = parseFloat(data);
                                var totalpriceorder = 0;
                                $(".package-detail").each(function () {
                                    if ($(this).attr("data-uid") == UID) {
                                        if ($(this).attr("data-status") == 5)
                                            totalpriceorder += parseFloat($(this).attr("data-totalprice"));
                                    }
                                });
                                if (userWallet >= totalpriceorder) {
                                    $(".package-detail").each(function () {
                                        if ($(this).attr("data-uid") == UID) {
                                            var barcode = $(this).attr("data-barcode");
                                            var id = $(this).attr("ID");
                                            xuatkho(barcode, id);
                                        }
                                    });
                                }
                                else {
                                    var moneyneed = numberWithCommas(totalpriceorder - userWallet);
                                    var error = "";
                                    error += "<div class=\"col-md-12\" style=\"color:red\">";
                                    error += "Username: <strong>" + Username + "</strong> còn thiếu: <strong>" + moneyneed + "</strong> vnđ để xuất kho <br/>";
                                    error += "</div>";
                                    //$(".error-outstock").append("Username: " + Username + " còn thiếu: " + moneyneed + " vnđ để xuất kho <br/>");
                                    $(".error-outstock").append(error);
                                    remove_loading();
                                }
                                countOrder();
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                //alert('lỗi checkend');
                            }
                        });
                    });
                }

               <%-- var listbarcode1 = $("#<%=hdfListBarcode.ClientID%>").val();
                var listbarcode = listbarcode1.split('|');
                for (var i = 0; i < listbarcode.length - 1; i++) {
                    var b = listbarcode[i];
                    var bcid = b.split(',');
                    xuatkho(bcid[0], bcid[1]);
                }--%>
            } else {

            }

        }
        function xuatkhoForall(Barcode) {
            $.ajax({
                type: "POST",
                url: "/manager/OutStock.aspx/SetFinish",
                data: "{barcode:'" + Barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;

                    //if (data == "ok") {
                    //    obj.parent().parent().find(".status-pack").html("<span class=\"package-label\">Trạng thái:</span><span class=\"package-info packageStatus bg-blue\">Đã giao</span>");
                    //    obj.parent().parent().find(".xuatkhobutton").remove();
                    //    //obj.parent().find(".status-pack").html("<span class=\"package-info packageStatus\">Đã giao</span>");

                    //}
                    countOrder();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

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
        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            return parts;
            //return parts.join(".");
        }
    </script>
</asp:Content>
