<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="chi-tiet-vch.aspx.cs" Inherits="NHST.Admin.chi_tiet_vch" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Chi tiết yêu cầu</h3>
                    </div>
                    <asp:Panel runat="server" ID="TTCB">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Username                                        
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Mã vận đơn <span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtBarcode" ForeColor="Red"
                                                ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtBarcode" runat="server" Enabled="false" CssClass="form-control" Width="40%" placeholder="Mã vận đơn"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Cân nặng
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control"
                                            Skin="MetroTouch" ID="rWeight" NumberFormat-DecimalDigits="2"
                                            Value="0" NumberFormat-GroupSizes="3" Width="40%" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Cước vật tư
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rAdditionFeeCYN" NumberFormat-DecimalDigits="2" Value="0" oninput="CountFee('ndt','feeadd',$(this))"
                                            NumberFormat-GroupSizes="3" Width="20%" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rAdditionFeeVND" NumberFormat-DecimalDigits="0" Value="0" oninput="CountFee('vnd','feeadd',$(this))"
                                            NumberFormat-GroupSizes="3" Width="20%" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Phụ phí hàng đặc biệt
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rSensorFeeCYN" NumberFormat-DecimalDigits="2" Value="0" oninput="CountFee('ndt','sensor',$(this))"
                                            NumberFormat-GroupSizes="3" Width="20%" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rSensorFeeeVND" NumberFormat-DecimalDigits="0" Value="0" oninput="CountFee('vnd','sensor',$(this))"
                                            NumberFormat-GroupSizes="3" Width="20%" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ghi chú
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtSummary" Width="40%" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ghi chú của nhân viên
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtStaffNote" Width="40%" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Trạng thái
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="40%" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Đơn hàng mới"></asp:ListItem>
                                            <%--<asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>--%>
                                            <asp:ListItem Value="3" Text="Đã về kho TQ"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Đã về kho đích"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Đã thanh toán"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Đã nhận hàng"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Đơn hàng hủy"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Hình thức vận chuyển
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlShippingType" runat="server" Enabled="false"
                                            Width="40%" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ghi chú hình thức vận chuyển
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtExportRequestNote" Width="40%" ReadOnly="true" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ngày yêu cầu xuất kho
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtDateExportRequest" ReadOnly="true" CssClass="form-control" Width="40%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ngày xuất kho
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtDateExport" ReadOnly="true" CssClass="form-control" Width="40%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Lý do hủy đơn
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtCancelReason" Width="40%" ReadOnly="true" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfList" runat="server" />
        <asp:HiddenField ID="hdfCurrency" runat="server" />
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlDVT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlDVT" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblUnit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery-1.11.0.min.js"></script>
        <script>

            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Admin/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
        <script type="text/javascript">
            var currency = parseFloat($("#<%= hdfCurrency.ClientID%>").val());
            function CountFee(currencyType, feild, obj) {
                var valu = parseFloat(obj.val());
                if(feild == "feeadd")
                {
                    if(currencyType == 'vnd')
                    {
                        var convertvalue = valu / currency;
                        $("#<%= rAdditionFeeCYN.ClientID%>").val(convertvalue);
                    }
                    else
                    {
                        var convertvalue = valu * currency;
                        $("#<%= rAdditionFeeVND.ClientID%>").val(convertvalue);
                    }
                }
                else
                {
                    if(currencyType == 'vnd')
                    {
                        var convertvalue = valu / currency;
                        $("#<%= rSensorFeeCYN.ClientID%>").val(convertvalue);
                    }
                    else
                    {
                        var convertvalue = valu * currency;
                        $("#<%= rSensorFeeeVND.ClientID%>").val(convertvalue);
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
