<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PrintStamp.aspx.cs" Inherits="NHST.Admin.PrintStamp" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Thông tin Tem</h3>
                    </div>
                    <div class="panel-body">
                        <a href="javascript:;" onclick="printDiv()" class="btn btn-success">In tem</a>
                        <div class="row">
                            <div class="col-md-12">
                                <style>
                                    .stamp-row {
                                        float: left;
                                        width: 100%;
                                        margin: 5px 0;
                                        color: #e84545;
                                        font-size: 20px;
                                        font-weight: bold;
                                    }

                                    .label-print {
                                        float: left;
                                        width: 40%;
                                    }

                                    .label-text {
                                        float: left;
                                        width: 60%;
                                    }

                                    @media print {
                                        .stamp-row {
                                            font-size: 20px;
                                            font-weight: bold;
                                        }

                                        .label-print {
                                            float: left;
                                            width: 40%;
                                            font-size: 20px;
                                            font-weight: bold;
                                        }

                                        .label-text {
                                            float: left;
                                            width: 60%;
                                            font-size: 20px;
                                            font-weight: bold;
                                        }
                                        .font-size-25
                                        {
                                            font-size:25px;
                                            font-weight:bold;
                                        }
                                    }
                                </style>
                                <div style="width: 340px; height: 189px; margin-top: 20px;" id="printcontent">
                                    <div class="stamp-row">
                                        <span class="label-print font-size-25">Username: </span>
                                        <span class="label-text font-size-25">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="stamp-row">
                                        <span class="label-print font-size-25">Đơn hàng: </span>
                                        <span class="label-text font-size-25">
                                            <asp:Label ID="lblOrderID" runat="server"></asp:Label></span>
                                    </div>
                                    <div class="stamp-row">
                                        <span class="label-print">Số lượng: </span>
                                        <span class="label-text">
                                            <asp:Label ID="lblProductCount" runat="server"></asp:Label>
                                            sản phẩm</span>
                                    </div>
                                    <div class="stamp-row">
                                        <span class="label-print">Trọng lượng: </span>
                                        <span class="label-text">
                                            <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                            kg</span>
                                    </div>
                                    <div class="stamp-row">
                                        <span class="label-print">Mã vận đơn: </span>
                                        <span class="label-text">
                                            <asp:Label ID="lblOrderCodeTrans" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
        function printDiv() {
            var html = "";

            $('link').each(function () { // find all <link tags that have
                if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                    html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                }
            });
            html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
            var w = window.open("", "print");
            if (w) { w.document.write(html); w.document.close() }
        }
    </script>

</asp:Content>
