<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TQWareHouse.aspx.cs" Inherits="NHST.Admin.TQWareHouse" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Kiểm kho TQ</h3>
                    </div>
                    <div class="panel-body">
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
                        <div class="row">
                            <div class="col-md-12 mar-bot3">
                                <label>Barcode</label>
                                <input id="barcode-input" class="form-control barcode-area width-50-per" type="text" oninput="getCode($(this))" />
                                <a id="addPackage" style="width: auto;" href="javascript:;" onclick="addCodeTemp()" class="btn btn-success btn-block">Thêm kiện mới</a>
                                <a id="capnhatall" style="display: none;" href="javascript:;" onclick="updateAll()" id="outall-package" class="btn btn-success btn-block hidden-btn">Cập nhật tất cả</a>
                            </div>
                            <%--<div class="col-md-12 mar-bot3" style="display: none">
                                <label style="font-size: 18px;">Số lượng đơn: <span id="countorder">0</span></label>
                            </div>--%>
                        </div>
                        <div class="row error-outstock" style="margin-bottom: 30px;">
                        </div>
                        <div class="row listpack">
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();window.close()}\n" +
                    "</scri" + "pt></head><body onload='step1()'>\n" +
                    "<img src='" + source + "' /></body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
        function printBarcode(barcode) {
            //var barcode = "12341234-4123412342134";
            $.ajax({
                type: "POST",
                url: "/admin/TQWareHouse.aspx/PriceBarcode",
                data: "{barcode:'" + barcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    VoucherPrint(data);
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }
        function getCode_old(obj) {

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
                    url: "/admin/TQWareHouse.aspx/GetCode",
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
                            html += "           <span class=\"package-label\"><strong>Số loại sản phẩm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soloaisanpham + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Số lượng sản phẩm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soluongsanpham + "</strong></span>";
                            html += "       </div>";
                            if (data.Kiemdem == "Có") {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Kiểm đếm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Kiemdem + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                            }
                            else {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong >Kiểm đếm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                            }
                            if (data.Donggo == "Có") {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Đóng gỗ:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Donggo + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                            } else {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                            }
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                            html += "       </div>";
                            //html += "       <div class=\"row-package\">";
                            //html += "           <span class=\"package-label\">Mã Package:</span>";
                            //html += "           <span class=\"package-info packageCode\">" + data.TotalWeight + "</span>";
                            //html += "       </div>";
                            var status = data.Status;
                            if (status < 3) {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                html += "           <select class=\"package-status-select\">";
                                //if (status == 1)
                                //    html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
                                //else
                                //    html += "               <option value=\"1\">Chưa về kho TQ</option>";
                                //if (status == 2)
                                //    html += "               <option value=\"2\" selected>Đã về kho TQ</option>";
                                //else
                                //    html += "               <option value=\"2\">Đã về kho TQ</option>";
                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                html += "           </select>";

                                html += "       </div>";
                            }
                            else {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                if (data.Status == 1)
                                    html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                else if (data.Status == 2)
                                    html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                else if (data.Status == 3)
                                    html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                else
                                    html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                html += "       </div>";
                            }


                            //if (status < 3) {
                            //    html += "       <div class=\"row-package\">";
                            //    html += "           <span class=\"package-label\">Trọng lượng:</span>";
                            //    html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/> kg";
                            //    html += "       </div>";
                            //}
                            //else {
                            //    html += "       <div class=\"row-package\">";
                            //    html += "           <span class=\"package-label\">Trọng lượng:</span>";
                            //    html += "           <span class=\"package-info packageWeight\">" + data.TotalWeight + " kg</span>";
                            //    html += "       </div>";
                            //}

                            //html += "       <div class=\"row-package status-pack\">";
                            //html += "           <span class=\"package-label\">Bao hàng:</span>";
                            //var listb = data.ListBig;
                            //var BigPackageID = data.BigPackageID;
                            //html += "           <select class=\"package-bigpackage-select\">";
                            //if (listb.length > 0) {
                            //    for (var i = 0; i < listb.length; i++) {
                            //        var item = listb[i];
                            //        if (item.ID == BigPackageID) {
                            //            html += "               <option value=\"" + item.ID + "\" selected>" + item.PackageCode + "</option>";
                            //        }
                            //        else {
                            //            html += "               <option value=\"" + item.ID + "\">" + item.PackageCode + "</option>";
                            //        }
                            //    }
                            //}
                            //html += "           </select>";

                            //html += "       </div>";

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
                            if (status < 3) {
                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                            }
                            else
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";
                            html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn \" style=\"float:left;width:100%!important;clear:both;margin-top:5px;!important\">In barcode</a>";
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
                            $("#capnhatall").show();
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
        function getCode(obj) {
            var bc = obj.val();
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "/admin/TQWareHouse.aspx/GetCode",
                data: "{barcode:'" + bc + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var datas = JSON.parse(msg.d);
                    if (datas != "none") {
                        for (var i = 0; i < datas.length; i++) {
                            var data = datas[i];

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


                            var packageID = data.ID;
                            var isExist = false;
                            if ($(".package-detail").length > 0) {
                                $(".package-detail").each(function () {
                                    var dt_packageID = $(this).attr("data-packageID");
                                    if (packageID == dt_packageID) {
                                        isExist = true;
                                    }
                                });
                            }
                            if (isExist == false) {
                                var html = '';
                                var idpack = "bc-" + data.BarCode + "-" + packageID;
                                html += "<div class=\"col-md-4 package-item\" >";
                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                if (data.orderType == 1)
                                {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng mua hộ</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    html += "       </div>";
                                }
                                else if(data.orderType == 2){
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng VC hộ</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/admin/chi-tiet-vch.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    html += "       </div>";
                                }
                                else
                                {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Chưa xác định</strong></span>";
                                    html += "       </div>";
                                }
                                
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Mã kiện:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + packageID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Số loại sản phẩm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soloaisanpham + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Số lượng sản phẩm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soluongsanpham + "</strong></span>";
                                html += "       </div>";
                                if (data.Kiemdem == "Có") {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong style=\"color:red\">Kiểm đếm:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Kiemdem + "</strong></span>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong >Kiểm đếm:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                    html += "       </div>";
                                }
                                if (data.Donggo == "Có") {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong style=\"color:red\">Đóng gỗ:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Donggo + "</strong></span>";
                                    html += "       </div>";
                                } else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                    html += "       </div>";
                                }
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                                html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                                html += "       </div>";
                                var status = data.Status;
                                if (status < 3) {
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    html += "           <select class=\"package-status-select\">";
                                    html += "               <option value=\"2\">Đã về kho TQ</option>";
                                    html += "           </select>";

                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package status-pack\">";
                                    html += "           <span class=\"package-label\">Trạng thái:</span>";
                                    if (data.Status == 1)
                                        html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                    else if (data.Status == 2)
                                        html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                    else if (data.Status == 3)
                                        html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                    else
                                        html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                    html += "       </div>";
                                }

                                var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                html += "       <div class=\"row-package \">";
                                if (status < 3) {
                                    html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                }
                                else
                                    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";
                                html += "           <a href=\"javascript:;\" onclick=\"printBarcode(" + data.BarCode + ")\" class=\"printbarcode xuatkhobutton btn btn-success btn-block small-btn \" style=\"float:left;width:100%!important;clear:both;margin-top:5px!important;\">In barcode</a>";
                                html += "       </div>";
                                html += "       <div class=\"row-package-status \">";
                                html += "       </div>";
                                html += "   </div>";
                                html += "</div>";
                                listbarcode += strItem;

                                $(".listpack").append(html);
                                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);

                                $("#outall-package").show();
                                countOrder();
                                $("#capnhatall").show();
                            }
                        }
                        obj.val("");
                    }
                    else {
                        alert('Không tìm thấy');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
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
        function updateWeight(barcode, id, obj, packageID) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            //add_loading();
            $.ajax({
                type: "POST",
                url: "/admin/TQWareHouse.aspx/UpdateQuantity",
                data: "{barcode:'" + barcode + "',status:'" + status + "',packageID:'" + packageID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    //alert(id);
                    if (data == "1") {
                        $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                    }
                    //remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });
        }
        function add_loading() {
            $(".page-inner").append("<div class='loading_bg'></div>");
            var height = $(".page-inner").height();
            $(".loading_bg").css("height", height + "px");
        }
        function remove_loading()
        {
            $(".page-inner").remove(".loading_bg");
        }
        function updateWeight_Each_old(barcode, id, quantity, status, bigpackage, obj) {
            //var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            //var status = obj.parent().parent().find(".package-status-select").val();
            //var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/TQWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'" + bigpackage + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data == "1") {
                            $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                        }
                        //remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
            }
            else {
                //alert("Vui lòng nhập số Kg");
                obj.find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.attr("style", "border:solid 2px red;");
            }
        }
        function updateWeight_Each(barcode, id, status, packageID) {
            //var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            //var status = obj.parent().parent().find(".package-status-select").val();
            //var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            //if (quantity > 0) {
                
            //}
            //else {
            //    //alert("Vui lòng nhập số Kg");
            //    obj.find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
            //    obj.attr("style", "border:solid 2px red;");
            //}

            add_loading();
            $.ajax({
                type: "POST",
                url: "/admin/TQWareHouse.aspx/UpdateQuantity",
                data: "{barcode:'" + barcode + "',status:'" + status + "',packageID:'" + packageID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == "1") {
                        $("#" + id).find('.row-package-status').html('Cập nhật thành công.').attr("style", "color:blue");
                    }
                    //remove_loading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                }
            });

        }
        function updateAll() {
            $(".package-detail").each(function () {
                var barcode = $(this).attr("data-barcode");
                var id = $(this).attr("id");
                var packageID = $(this).attr("data-packageid");
                var quantity = $(this).find(".packageWeightUpdate").val();
                var status = $(this).find(".package-status-select").val();
                var bigpackage = $(this).find(".package-bigpackage-select").val();
                //alert(barcode + " - " + id + " - " + quantity + " - " + status + " - " + bigpackage);
                //alert(barcode + " - " + id + " - " + quantity + " - " + status + " - " + bigpackage);
                updateWeight_Each(barcode, id, status, packageID);
            });
        }
        function xuatkho(Barcode, id) {
            add_loading();
            //alert(Barcode);
            $.ajax({
                type: "POST",
                url: "/admin/OutStock.aspx/SetFinish",
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
                    //remove_loading();
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
                var id = barcode;
                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                obj.parent().parent().parent().remove();
                if ($(".package-item").length == 0) {
                    $("#outall-package").hide();
                    $("#capnhatall").hide();
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
                            url: "/admin/OutStock.aspx/GetWallet",
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
                                    //remove_loading();
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
                url: "/admin/OutStock.aspx/SetFinish",
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
        function addCodeTemp() {
            var obj = $('form');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup_home'></div>";
            var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
            fr += "<div class=\"popup_header\">";
            fr += "Thêm mã kiện mới";
            fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "</div>";
            fr += "     <div class=\"changeavatar\">";
            fr += "         <div class=\"content1\">";
            fr += "             <span class=\"package-label\"> Mã đơn hàng: </span>";
            fr += "             <span class=\"package-info\"><input id=\"ordercode-temp\" class=\"form-control\" /></span>";
            fr += "         </div>";
            fr += "         <div class=\"content1\" style=\"margin-top:10px;\">";
            fr += "             <span class=\"package-label\"> Mã vận đơn: </span>";
            fr += "             <span class=\"package-info\"><input id=\"orderpackagecode-temp\" class=\"form-control\" /></span>";
            fr += "         </div>";
            fr += "         <div class=\"content2\">";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
            fr += "             <a href=\"javascript:;\" class=\"btn btn-close\" onclick='CheckAddTempCode()'>Thêm</a>";
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
        function CheckAddTempCode() {
            var c = confirm("Bạn muốn tạo mã kiện mới?");
            if (c) {

                var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                var code = $("#ordercode-temp").val();
                var packagecode = $("#orderpackagecode-temp").val();
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/TQWareHouse.aspx/CheckOrderShopCode",
                    data: "{ordershopcode:'" + code + "',packagecode:'" + packagecode + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret != "none") {
                            var PackageAll = JSON.parse(ret);
                            var data = PackageAll[0];
                            var UID = data.UID;
                            var Wallet = data.Wallet;
                            var packageID = data.ID;
                            var idpack = "bc-" + data.BarCode + "-" + packageID;
                            var html = '';
                            html += "<div class=\"col-md-4 package-item\" >";
                            html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Mã kiện:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + packageID + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Số loại sản phẩm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soloaisanpham + "</strong></span>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\"><strong>Số lượng sản phẩm:</strong></span>";
                            html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Soluongsanpham + "</strong></span>";
                            html += "       </div>";
                            if (data.Kiemdem == "Có") {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Kiểm đếm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Kiemdem + "</strong></span>";
                                html += "       </div>";
                            }
                            else {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong >Kiểm đếm:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Kiemdem + "</strong></span>";
                                html += "       </div>";
                            }
                            if (data.Donggo == "Có") {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong style=\"color:red\">Đóng gỗ:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong style=\"color:red\">" + data.Donggo + "</strong></span>";
                                html += "       </div>";
                            } else {
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Đóng gỗ:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\"><strong>" + data.Donggo + "</strong></span>";
                                html += "       </div>";
                            }
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Mã vận đơn:</span>";
                            html += "           <span class=\"package-info packageCode\">" + data.BarCode + "</span>";
                            html += "       </div>";
                            var status = data.Status;
                            if (status < 3) {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                html += "           <select class=\"package-status-select\">";
                                html += "               <option value=\"2\">Đã về kho TQ</option>";
                                html += "           </select>";

                                html += "       </div>";
                            }
                            else {
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";
                                if (data.Status == 1)
                                    html += "       <span class=\"package-info packageStatus bg-red\">Chưa về kho TQ</span>";
                                else if (data.Status == 2)
                                    html += "       <span class=\"package-info packageStatus bg-yellow\">Đã về kho TQ</span>";
                                else if (data.Status == 3)
                                    html += "       <span class=\"package-info packageStatus bg-blue\">Đã về kho đích</span>";
                                else
                                    html += "       <span class=\"package-info packageStatus bg-green\">Đã giao khách</span>";
                                html += "       </div>";
                            }

                            var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                            html += "       <div class=\"row-package \">";
                            if (status < 3) {
                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                            }
                            else
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                            html += "       </div>";
                            html += "       <div class=\"row-package-status \">";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";
                            listbarcode += strItem;

                            $(".listpack").append(html);
                            $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                            $("#outall-package").show();
                            countOrder();
                            $("#capnhatall").show();
                            close_popup_ms();
                        }
                        //remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert(errorthrow);
                    }
                });
            }
        }
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
    </script>
    <style>
        #capnhatall {
            display: inline;
            margin-top: 20px;
            float: left;
            width: auto;
        }

        #addPackage {
            display: inline;
            margin-top: 20px;
            float: left;
            width: auto;
            margin-right: 10px;
        }
    </style>
    <style>
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
            background: #2154b0;
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
            background: #2154b0;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
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
    </style>
</asp:Content>
