<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="payforlistdetail.aspx.cs" Inherits="NHST.admin.payforlistdetail" %>

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
                                        Số dư tài khoản                                        
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblAccountWallet" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tỉ giá hiện tại
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tổng tiền (tệ) <span>
                                            <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="pPriceCYN" ForeColor="Red"
                                                ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" ReadOnly="true" 
                                            Skin="MetroTouch" ID="pPriceCYN" NumberFormat-DecimalDigits="2" 
                                            Value="0" NumberFormat-GroupSizes="3" Width="100%" oninput="GetPrice()">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tổng tiền phải trả(VNĐ) <span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pPriceVND" ForeColor="Red"
                                                ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pPriceVND" NumberFormat-DecimalDigits="0" Value="0" ReadOnly="true"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tổng tiền đã trả(VNĐ) <span>                                            
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rPriceVNDPayed" NumberFormat-DecimalDigits="0" Value="0" ReadOnly="true"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Giá tệ (VNĐ) <span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pCurrency" ForeColor="Red"
                                                ErrorMessage="(*)" ValidationGroup="n" Display="Dynamic"></asp:RequiredFieldValidator></span>
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pCurrency" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Giá tệ thật
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rrealPriceCYN" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tỉ giá thật
                                    </label>
                                    <div class="col-sm-10">
                                        <span class="realCurrency form-control">0</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                       Tổng tiền thật
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rRealTotalPrice" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tổng tiền trả cuối
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="rFinalPayPrice" NumberFormat-DecimalDigits="0" Value="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Số đt
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ghi chú
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtSummary" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Ghi chú của nhân viên
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtStaffNote" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Chưa hoàn thiện
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:CheckBox ID="chkIsNotComplete" runat="server" />
                                    </div>
                                </div>--%>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Trạng thái
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Chờ thanh toán" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Đã hủy" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Đã hoàn tiền" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Đã thanh toán" Value="4"></asp:ListItem>
                                            <%--<asp:ListItem Text="Đã hủy" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Chưa thanh toán" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Đã xác nhận" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Đã thanh toán" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Hoàn thành" Value="3"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="panel panel-white">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <a href="javascript:;" class="btn btn-success" onclick="update()">Cập nhật</a>
                                    <%--<a href="/Admin/AddRequestRecharge.aspx" class="btn btn-success" target="_blank">Thêm mới nạp tiền</a>--%>
                                    <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click"
                                        Style="display: none;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-white" style="display:none">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Lịch sử thay đổi nơi đến</h3>
                    </div>
                    <div class="panel-body">
                        <div class="cont">
                            <div class="order-panel">
                                <table class="tb-product">
                                    <tr>
                                        <th class="pro">Ngày thay đổi</th>
                                        <th class="pro">Trạng thái cũ</th>
                                        <th class="pro">Trạng thái mới</th>
                                        <th class="qty">Người đổi</th>
                                    </tr>
                                    <asp:Repeater ID="rptPayment" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="pro">
                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td class="pro">
                                                    <%# Eval("OldeStatusText").ToString() %>
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("NewStatusText").ToString() %>
                                                </td>
                                                <td class="qty"><%#Eval("Username") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                </table>
                            </div>
                        </div>
                    </div>
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
            function update() {
                var list = "";
                $(".itemyeuau").each(function () {
                    var id = $(this).attr("data-id");
                    var des1 = $(this).find(".txtDesc1").val();
                    var des2 = $(this).find(".txtDesc2").val();
                    list += id + "," + des1 + "," + des2 + "|";
                });
                $("#<%=hdfList.ClientID%>").val(list);
                $("#<%=btnSave.ClientID%>").click();
            }
            function GetPrice()
            {
                var currency = parseFloat($("#<%=hdfCurrency.ClientID%>").val());
                var priceCYN = parseFloat($("#<%= pPriceCYN.ClientID%>").val());
                var priceVND = currency * priceCYN;
                $("#<%=pPriceVND.ClientID%>").val(priceVND);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
