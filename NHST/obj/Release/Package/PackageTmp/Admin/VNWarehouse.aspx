<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="VNWarehouse.aspx.cs" Inherits="NHST.Admin.VNWarehouse" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .package-detail {
            float: left;
            border: dashed 1px #000;
            padding: 10px;
            margin-bottom: 30px;
            min-height: 580px;
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
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Kiểm hàng kho đích</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <%--<div class="col-md-12 mar-bot3">
                                <label>Mã bao lớn</label>
                                <input id="bigbarcode-input" class="form-control barcode-area width-50-per" type="text" oninput="getCodeBig($(this))" />                                
                            </div>--%>
                            <%--<div class="col-md-12 mar-bot3" style="display: none">
                                <label style="font-size: 18px;">Số lượng đơn: <span id="countorder">0</span></label>
                            </div>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mar-bot3">
                                <label>Barcode</label>
                                <%--<input id="barcode-input" class="form-control barcode-area width-50-per" type="text" oninput="getCode($(this))" />--%>
								<input id="barcode-input" class="form-control barcode-area width-50-per" type="text"/>
                                <a id="taokienmoi" style="float: left; clear: both; width: auto; margin: 10px 0;" href="javascript:;" onclick="createPackage()" class="btn btn-success btn-block">Tạo kiện</a>
                                <a id="capnhatall" style="display: none; float: left; clear: both; width: auto; margin: 10px 0;" href="javascript:;" onclick="updateAll()" class="btn btn-success btn-block hidden-btn">Cập nhật tất cả</a>
                                <%--<a href="javascript:;" onclick="xuatkhoall()" id="outall-package" class="btn btn-success btn-block hidden-btn">Xuất tất cả</a>--%>
                            </div>
                            <%--<div class="col-md-12 mar-bot3" style="display: none">
                                <label style="font-size: 18px;">Số lượng đơn: <span id="countorder">0</span></label>
                            </div>--%>
                        </div>
                        <div class="row error-outstock" style="margin-bottom: 30px;">
                        </div>
                        <div class="row listpack">
                            <%--<div class="col-md-3 package-detail">
                                <div class="row-package">
                                    <span class="package-label">Barcode:</span>
                                    <span class="package-info packageCode">123456789</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Package:</span>
                                    <span class="package-info packageCode">admin-AL-2017412-2-3-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Shop:</span>
                                    <span class="package-info packageShopCode">admin-AL-2017412-2-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trọng lượng:</span>
                                    <span class="package-info packageWeight">2 kg</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trạng thái:</span>
                                    <span class="package-info packageStatus">Chưa giao</span>
                                </div>
                                <div class="row-package">
                                    <a href="javascript:;" onclick="xuatkho('1')" class="btn btn-success btn-block large-btn">Xuất kho</a>
                                </div>
                            </div>
                            <div class="col-md-3 package-detail">
                                <div class="row-package">
                                    <span class="package-label">Barcode:</span>
                                    <span class="package-info packageCode">123456789</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Package:</span>
                                    <span class="package-info packageCode">admin-AL-2017412-2-3-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Shop:</span>
                                    <span class="package-info packageShopCode">admin-AL-2017412-2-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trọng lượng:</span>
                                    <span class="package-info packageWeight">2 kg</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trạng thái:</span>
                                    <span class="package-info packageStatus">Chưa giao</span>
                                </div>
                                <div class="row-package">
                                    <a href="javascript:;" onclick="xuatkho('1')" class="btn btn-success btn-block large-btn">Xuất kho</a>
                                </div>
                            </div>
                            <div class="col-md-3 package-detail">
                                <div class="row-package">
                                    <span class="package-label">Barcode:</span>
                                    <span class="package-info packageCode">123456789</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Package:</span>
                                    <span class="package-info packageCode">admin-AL-2017412-2-3-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Shop:</span>
                                    <span class="package-info packageShopCode">admin-AL-2017412-2-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trọng lượng:</span>
                                    <span class="package-info packageWeight">2 kg</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trạng thái:</span>
                                    <span class="package-info packageStatus">Chưa giao</span>
                                </div>
                                <div class="row-package">
                                    <a href="javascript:;" onclick="xuatkho('1')" class="btn btn-success btn-block large-btn">Xuất kho</a>
                                </div>
                            </div>
                            <div class="col-md-3 package-detail">
                                <div class="row-package">
                                    <span class="package-label">Barcode:</span>
                                    <span class="package-info packageCode">123456789</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Package:</span>
                                    <span class="package-info packageCode">admin-AL-2017412-2-3-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Mã Shop:</span>
                                    <span class="package-info packageShopCode">admin-AL-2017412-2-3</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trọng lượng:</span>
                                    <span class="package-info packageWeight">2 kg</span>
                                </div>
                                <div class="row-package">
                                    <span class="package-label">Trạng thái:</span>
                                    <span class="package-info packageStatus">Chưa giao</span>
                                </div>
                                <div class="row-package">
                                    <a href="javascript:;" onclick="xuatkho('1')" class="btn btn-success btn-block large-btn">Xuất kho</a>
                                </div>
                            </div>--%>
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
    <asp:HiddenField ID="hdfCurrency" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#barcode-input').focus();
			$('#barcode-input').keydown(function (e) {
                if (e.key === 'Enter') {
                    //getCodeNew
                    getCode($(this));
                    e.preventDefault();
                    return false;
                }
            });
        });
        function handleKeyPress(evt) {
            var nbr = (window.event) ? event.keyCode : evt.which;
            if (nbr == 13) {
                return false;
            }
        }
        document.onkeydown = handleKeyPress
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
                    url: "/admin/VNWareHouse.aspx/GetCodeInfo",
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
                            var status = data.Status;
                            html += "       <div class=\"row-package status-pack\">";
                            html += "           <span class=\"package-label\">Trạng thái:</span>";

                            html += "           <select class=\"package-status-select\">";
                            if (status < 2)
                                html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
                            else if (status > 1 && status < 4) {
                                html += "               <option value=\"3\">Đã về kho đích</option>";
                            }
                            else if (status == 4) {
                                html += "               <option value=\"4\" selected>Đã giao khách</option>";
                            }
                            html += "           </select>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                            html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                            html += "       </div>";
                            html += "       <div class=\"row-package\">";
                            html += "           <span class=\"package-label\">Quy đổi cm3 ( chiều dài * chiều rộng * chiều cao / 6000 ):</span>";
                            html += "           <input class=\"package-info packageWeight packageStaffNote\" style=\"width:40%\" value=\"" + data.StaffNote + "\"/>";
                            html += "       </div>";

                            html += "       <div class=\"row-package \">";
                            if (status > 1 && status < 4) {
                                html += "           <a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "','bc-" + data.BarCode + "',$(this))\" class=\"xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                            }
                            else
                                html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + data.BarCode + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                            html += "       </div>";
                            html += "       <div class=\"row-package-status \">";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";

                            listbarcode += data.BarCode + ",bc-" + data.BarCode + "|";

                            //$(".listpack").append(html);
                            $(".listpack").prepend(html);
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
        function createPackage() {
            var html = "";
            html += "<div class=\"col-md-4 package-item\" >";
            html += "   <div class=\"package-detail\">";
            html += "       <div class=\"row-package\">";
            html += "           <span class=\"package-label\" style=\"font-size:16px;\"><strong>Loại đơn hàng:</strong></span>";
            html += "           <span class=\"package-info packageShopCode\" style=\"font-size:16px;\"><select class=\"ordertype\" onchange=\"selectOrderType($(this))\"><option value=\"2\">Đơn hàng VC hộ</option><option value=\"1\">Đơn hàng mua hộ</option></select></span>";
            html += "       </div>";
            html += "       <div class=\"row-package ordertype-row\" style=\"display:none\">";
            html += "           <span class=\"package-label\"><strong>Mã đơn hàng hệ thống:</strong></span>";
            html += "           <span class=\"package-info packageShopCodeSystem\"><input class=\"packageOrderCode\" value=\"0\"/></span>";
            html += "       </div>";
            html += "       <div class=\"row-package\">";
            html += "           <span class=\"package-label\"><strong>Username khách:</strong></span>";
            html += "           <span class=\"package-info packageShopCode\"><input class=\"packageUsername\"/></span>";
            html += "       </div>";
            html += "       <div class=\"row-package ordertype-row\" style=\"display:none\">";
            html += "           <span class=\"package-label\"><strong>Mã đơn hàng taobao:</strong></span>";
            html += "           <span class=\"package-info packageShopCode\"><input class=\"packageOrderCodeTaobao\" value=\"\"/></span>";
            html += "       </div>";
            html += "       <div class=\"row-package\">";
            html += "           <span class=\"package-label\"><strong>Mã Vận đơn:</strong></span>";
            html += "           <span class=\"package-info packageShopCode\"><input class=\"packageCode\"/></span>";
            html += "       </div>";
            html += "       <div class=\"row-package ordertype-2-row\">";
            html += "           <span class=\"package-label\">Cước vật tư:</span>";
            html += "           <span class=\"package-info additionfee\">";
            html += "                <input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" style=\"width:20%\" value=\"0\"/>";
            html += "                <input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" style=\"width:20%\" value=\"0\"/>";
            html += "            </span>";
            html += "       </div>";
            html += "       <div class=\"row-package ordertype-2-row\">";
            html += "           <span class=\"package-label\">Phụ phí ĐB:</span>";
            html += "           <span class=\"package-info sensorfee\">";
            html += "                <input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" style=\"width:20%\" value=\"0\"/>";
            html += "                <input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" style=\"width:20%\" value=\"0\"/>";
            html += "            </span>";
            html += "       </div>";
            html += "       <div class=\"row-package\">";
            html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
            html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight\" style=\"width:40%\" value=\"0\"/>";
            html += "       </div>";
            html += "       <div class=\"row-package\">";
            html += "           <span class=\"package-label\">Quy đổi cm3 ( chiều dài * chiều rộng * chiều cao / 6000 ):</span>";
            html += "           <input class=\"package-info packageStaffNote\" style=\"width:40%\" value=\"\"/>";
            html += "       </div>";
            html += "       <div class=\"row-package\">";
            html += "           <a href=\"javascript:;\" onclick=\"createdPack($(this))\" class=\"updatepackageatkhodich xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Tạo mới</a>";
            html += "           <a href=\"javascript:;\" onclick=\"deletePackage($(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
            html += "       </div>";
            html += "       <div class=\"row-package-status \">";
            html += "       </div>";
            html += "   </div>";
            html += "</div>";
            $(".listpack").prepend(html);
        }

        function getPriceFee(rowname, obj, type) {
            var currency = parseFloat($("#<%=hdfCurrency.ClientID%>").val());
            var rowval = parseFloat(obj.val());
            
            if (type == "cyn") {
                var vndval = rowval * currency;
                if (rowname == 'packageadditionfeeCYN') {
                    obj.parent().find('.packageadditionfeeVND').val(vndval);
                }
                else if (rowname == 'packageSensorCYN') {
                    obj.parent().find('.packageSensorVND').val(vndval);
                }
            }
            else
            {
                var cynval = rowval / currency;
                if (rowname == 'packageadditionfeeVND') {
                    obj.parent().find('packageadditionfeeCYN').val(cynval);
                }
                else if (rowname == 'packageSensorVND') {
                    obj.parent().find('.packageSensorCYN').val(cynval);
                }
            }
        }

        function selectOrderType(obj) {
            var b = obj.val();
            if (b == 2) {
                obj.parent().parent().parent().find(".ordertype-row").hide();
                obj.parent().parent().parent().find(".ordertype-2-row").show();

            }
            else {
                obj.parent().parent().parent().find(".ordertype-row").show();
                obj.parent().parent().parent().find(".ordertype-2-row").hide();
            }
        }
        function createdPack(obj) {
            var root = obj.parent().parent();
            var ordertype = root.find(".ordertype").val();
            var packageOrderCode = root.find(".packageOrderCode").val();
            var packageUsername = root.find(".packageUsername").val();
            var packageOrderCodeTaobao = root.find(".packageOrderCodeTaobao").val();
            var packageCode = root.find(".packageCode").val();
            var packageWeight = root.find(".packageWeight").val();
            var packageStaffNote = root.find(".packageStaffNote").val();

            var packageadditionfeeCYN = root.find(".packageadditionfeeCYN").val();
            var packageadditionfeeVND = root.find(".packageadditionfeeVND").val();
            var packageSensorCYN = root.find(".packageSensorCYN").val();
            var packageSensorVND = root.find(".packageSensorVND").val();

            //alert(ordertype + " - " + packageOrderCode + " - " + packageUsername + " - " +
            //    packageCode + " - " + packageWeight + " - " + packageStaffNote
            //    + " - " + packageadditionfeeCYN + " - " + packageadditionfeeVND + " - " +
            //    packageSensorCYN + " - " + packageSensorVND);
            $.ajax({
                type: "POST",
                url: "/admin/VNWarehouse.aspx/addNewPackage",
                data: "{ordertype:'" + ordertype
                   + "',packageOrderCode:'" + packageOrderCode
                   + "',packageUsername:'" + packageUsername
                   + "',packageOrderCodeTaobao:'" + packageOrderCodeTaobao
                   + "',packageCode:'" + packageCode
                   + "',packageWeight:'" + packageWeight
                   + "',packageStaffNote:'" + packageStaffNote
                   + "',packageadditionfeeCYN:'" + packageadditionfeeCYN
                   + "',packageadditionfeeVND:'" + packageadditionfeeVND
                   + "',packageSensorCYN:'" + packageSensorCYN
                   + "',packageSensorVND:'" + packageSensorVND + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret == "vuilongnhapmadonhang") {
                        alert('Vui lòng nhập mã đơn hàng.');
                    }
                    else if (ret == "khongthetaomoi") {
                        alert('Có lỗi trong quá trình tạo mới, vui lòng thử lại sau');
                    }
                    else if (ret == "makiendatontai") {
                        alert('Mã kiện hàng đã tồn tại trong hệ thống.');
                    }
                    else if (ret == "khongdetrongmavandon") {
                        alert('Vui lòng không để trống mã vận đơn.');
                    }
                    else if (ret == "madonhangtaobaokhongthuocmadonhanghethong") {
                        alert('Mã đơn hàng taobao không thuộc mã đơn hàng hệ thống.');
                    }
                    else if (ret == "madonhangtaobaokhongtontai") {
                        alert('Mã đơn hàng taobao không tồn tại.');
                    }
                    else if (ret == "chuanhapmadonhangtaobao") {
                        alert('Vui lòng nhập mã đơn hàng taobao.');
                    }
                    else if (ret == "madonhangkhongthuocvekhach") {
                        alert('Sai mã đơn hàng hệ thống và khách.');
                    }
                    else if (ret == "donhangkhongtontai") {
                        alert('Đơn hàng không tồn tại.');
                    }
                    else if (ret == "usernamekhachkhongtontai") {
                        alert('Tài khoản khách không tồn tại.');
                    }
                    else {
                        obj.parent().parent().parent().remove();
                        getCodeWithValue(ret);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }
        function deletePackage(obj) {
            var c = confirm('Bạn muốn xóa kiện này?')
            if (c) {
                obj.parent().parent().parent().remove();
            }

        }
        function getCodeWithValue(bc) {
            var bc_e = bc + ",bc-" + bc;
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "/admin/VNWareHouse.aspx/GetCodeInfo",
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
                                var idpack = "bc-" + data.BarCode + "-" + packageID;
                                var html = '';
                                html += "<div class=\"col-md-4 package-item\" >";
                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\" style=\"font-size:16px;\"><strong>Username:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\" style=\"font-size:16px;\"><strong>" + data.Username + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Ngày đặt hàng:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\" ><strong>" + data.OrderDate + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                if (data.orderType == 1) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng mua hộ</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                }
                                else if (data.orderType == 2) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng VC hộ</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/admin/chi-tiet-vch.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package ordertype-2-row\">";
                                    html += "           <span class=\"package-label\">Cước vật tư:</span>";
                                    html += "           <span class=\"package-info additionfee\">";
                                    html += "                <input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" style=\"width:20%\" value=\"" + data.CuocvattuCYN + "\"/> tệ -";
                                    html += "                <input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" style=\"width:20%\" value=\"" + data.CuocvattuVND + "\"/> VNĐ";
                                    html += "            </span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package ordertype-2-row\">";
                                    html += "           <span class=\"package-label\">Phụ phí ĐB:</span>";
                                    html += "           <span class=\"package-info sensorfee\">";
                                    html += "                <input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" style=\"width:20%\" value=\"" + data.HangDBCYN + "\"/>tệ -";
                                    html += "                <input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" style=\"width:20%\" value=\"" + data.HangDBVND + "\"/> VNĐ";
                                    html += "            </span>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Chưa xác định</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                }
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
                                var status = data.Status;
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";

                                html += "           <select class=\"package-status-select\">";
                                //if (status < 2)
                                //html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
                                //else if (status > 1 && status < 4) {
                                //html += "               <option value=\"3\">Đã về kho đích</option>";
                                //}
                                //else if (status == 4) {
                                //   html += "               <option value=\"4\" selected>Đã giao khách</option>";
                                //}
                                if (status < 4) {
                                    html += "               <option value=\"3\">Đã về kho đích</option>";
                                }
                                else if (status == 4) {
                                    html += "               <option value=\"4\" selected>Đã giao khách</option>";
                                }
                                html += "           </select>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Quy đổi cm3 ( chiều dài * chiều rộng * chiều cao / 6000 ):</span>";
                                html += "           <input class=\"package-info packageStaffNote packageStaffNoteUpdate\" style=\"width:40%\" value=\"" + data.StaffNote + "\"/>";
                                html += "       </div>";
                                var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                html += "       <div class=\"row-package \">";
                                //if (status > 1 && status < 4) {
                                //    html += "           <a href=\"javascript:;\" data-barcode=\"" + data.BarCode + "\" data-idpack=\"" + idpack + "\" data-packageID=\"" + packageID + "\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatepackageatkhodich xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                //}
                                //else
                                //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                                if (status < 4) {
                                    html += "           <a href=\"javascript:;\" data-barcode=\"" + data.BarCode + "\" data-idpack=\"" + idpack + "\" data-packageID=\"" + packageID + "\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatepackageatkhodich xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
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

                                //$(".listpack").append(html);
                                $(".listpack").prepend(html);
                                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                                //obj.val("");
                                $("#outall-package").show();
                                countOrder();
                                $("#capnhatall").show();
                            }
                        }
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
        function getCode(obj) {
            var bc = obj.val();
            var bc_e = obj.val() + ",bc-" + obj.val();
            var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
            $.ajax({
                type: "POST",
                url: "/admin/VNWareHouse.aspx/GetCodeInfo",
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
                                var idpack = "bc-" + data.BarCode + "-" + packageID;
                                var html = '';
                                html += "<div class=\"col-md-4 package-item\" >";
                                html += "   <div id=\"" + idpack + "\" data-packageID=\"" + packageID + "\" class=\"package-detail\" data-barcode=\"" + data.BarCode + "\" data-uid=\"" + UID + "\" data-totalprice=\"" + data.TotalPriceVNDNum + "\" data-status=\"" + data.Status + "\">";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\" style=\"font-size:16px;\"><strong>Username:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\" style=\"font-size:16px;\"><strong>" + data.Username + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\"><strong>Ngày đặt hàng:</strong></span>";
                                html += "           <span class=\"package-info packageShopCode\" ><strong>" + data.OrderDate + "</strong></span>";
                                //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                html += "       </div>";
                                if (data.orderType == 1) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng mua hộ</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/Admin/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                }
                                else if (data.orderType == 2) {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Đơn hàng VC hộ</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Mã Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong><a href=\"/admin/chi-tiet-vch.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package ordertype-2-row\">";
                                    html += "           <span class=\"package-label\">Cước vật tư:</span>";
                                    html += "           <span class=\"package-info additionfee\">";
                                    html += "                <input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" style=\"width:20%\" value=\"" + data.CuocvattuCYN + "\"/> tệ - ";
                                    html += "                <input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" style=\"width:20%\" value=\"" + data.CuocvattuVND + "\"/> VNĐ";
                                    html += "            </span>";
                                    html += "       </div>";
                                    html += "       <div class=\"row-package ordertype-2-row\">";
                                    html += "           <span class=\"package-label\">Phụ phí ĐB:</span>";
                                    html += "           <span class=\"package-info sensorfee\">";
                                    html += "                <input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" style=\"width:20%\" value=\"" + data.HangDBCYN + "\"/> tệ - ";
                                    html += "                <input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" style=\"width:20%\" value=\"" + data.HangDBVND + "\"/> VNĐ";
                                    html += "            </span>";
                                    html += "       </div>";
                                }
                                else {
                                    html += "       <div class=\"row-package\">";
                                    html += "           <span class=\"package-label\"><strong>Loại Đơn Hàng:</strong></span>";
                                    html += "           <span class=\"package-info packageShopCode\"><strong>Chưa xác định</strong></span>";
                                    //html += "           <span class=\"package-info packageShopCode\"><strong>" + data.MainorderID + "</strong></span>";
                                    html += "       </div>";
                                }
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
                                var status = data.Status;
                                html += "       <div class=\"row-package status-pack\">";
                                html += "           <span class=\"package-label\">Trạng thái:</span>";

                                html += "           <select class=\"package-status-select\">";
                                //if (status < 2)
                                //html += "               <option value=\"1\" selected>Chưa về kho TQ</option>";
                                //else if (status > 1 && status < 4) {
                                //html += "               <option value=\"3\">Đã về kho đích</option>";
                                //}
                                //else if (status == 4) {
                                //   html += "               <option value=\"4\" selected>Đã giao khách</option>";
                                //}
                                if (status < 4) {
                                    html += "               <option value=\"3\">Đã về kho đích</option>";
                                }
                                else if (status == 4) {
                                    html += "               <option value=\"4\" selected>Đã giao khách</option>";
                                }
                                html += "           </select>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Cân nặng (kg):</span>";
                                html += "           <input type=\"Number\" min=\"0\" class=\"package-info packageWeight packageWeightUpdate\" style=\"width:40%\" value=\"" + data.TotalWeight + "\"/>";
                                html += "       </div>";
                                html += "       <div class=\"row-package\">";
                                html += "           <span class=\"package-label\">Quy đổi cm3 ( chiều dài * chiều rộng * chiều cao / 6000 ):</span>";
                                html += "           <input class=\"package-info packageStaffNote packageStaffNoteUpdate\" style=\"width:40%\" value=\"" + data.StaffNote + "\"/>";
                                html += "       </div>";
                                var strItem = data.BarCode + ",bc-" + data.BarCode + "," + packageID + "|";
                                html += "       <div class=\"row-package \">";
                                //if (status > 1 && status < 4) {
                                //    html += "           <a href=\"javascript:;\" data-barcode=\"" + data.BarCode + "\" data-idpack=\"" + idpack + "\" data-packageID=\"" + packageID + "\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatepackageatkhodich xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
                                //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block small-btn custom-small-button\">Hủy</a>";
                                //}
                                //else
                                //    html += "           <a href=\"javascript:;\" onclick=\"huyxuatkho('" + strItem + "',$(this))\" class=\"huyxuatkhobutton btn btn-danger btn-block\">Hủy</a>";

                                if (status < 4) {
                                    html += "           <a href=\"javascript:;\" data-barcode=\"" + data.BarCode + "\" data-idpack=\"" + idpack + "\" data-packageID=\"" + packageID + "\" onclick=\"updateWeight('" + data.BarCode + "','" + idpack + "',$(this),'" + packageID + "')\" class=\"updatepackageatkhodich xuatkhobutton btn btn-success btn-block small-btn custom-small-button\">Cập nhật</a>";
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

                                //$(".listpack").append(html);
                                $(".listpack").prepend(html);
                                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);
                                obj.val("");
                                $("#outall-package").show();
                                countOrder();
                                $("#capnhatall").show();
                            }
                        }
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
        function updateAll() {
            $(".updatepackageatkhodich").each(function () {
                var barcode = $(this).attr("data-barcode");
                var idpack = $(this).attr("data-idpack");
                var packageID = $(this).attr("data-packageid");
                updateWeight(barcode, idpack, $(this), packageID);
            });
        }
        function updateWeight(barcode, id, obj, packageID) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var StaffNote = obj.parent().parent().find(".packageStaffNoteUpdate").val();
            var packageadditionfeeCYN = obj.parent().parent().find(".packageadditionfeeCYN").val();
            var packageadditionfeeVND = obj.parent().parent().find(".packageadditionfeeVND").val();
            var packageSensorCYN = obj.parent().parent().find(".packageSensorCYN").val();
            var packageSensorVND = obj.parent().parent().find(".packageSensorVND").val();
            //alert(quantity + " - " + status);
            var bigpackage = "0";
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/VNWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status
                            + "',BigPackageID:'" + bigpackage + "',packageID:'" + packageID
                            + "',staffNote:'" + StaffNote
                            + "',packageadditionfeeCYN:'" + packageadditionfeeCYN
                            + "',packageadditionfeeVND:'" + packageadditionfeeVND
                            + "',packageSensorCYN:'" + packageSensorCYN
                            + "',packageSensorVND:'" + packageSensorVND + "'}",
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
            else {
                obj.parent().parent().find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.parent().parent().attr("style", "border:solid 2px red;");
            }
        }

        function getCode_finish(obj) {
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
                    url: "/admin/VNWareHouse.aspx/GetCode",
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
        function updateWeight_old(barcode, id, obj) {
            var quantity = obj.parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().find(".package-status-select").val();
            var bigpackage = "0";
            if (quantity > 0) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/TQWareHouse.aspx/UpdateQuantity",
                    data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'" + bigpackage + "'}",
                    //data: "{barcode:'" + barcode + "',status:'" + status + "'}",
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
            else {
                obj.parent().parent().find(".row-package-status").html("<span style=\"color:red\">Chưa nhập số kg</span>");
                obj.parent().parent().attr("style", "border:solid 2px red;");
            }
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
                    $("#capnhatall").hide();
                }
                countOrder();
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
    </script>
</asp:Content>
