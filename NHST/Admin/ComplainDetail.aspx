<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ComplainDetail.aspx.cs" Inherits="NHST.Admin.ComplainDetail" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Chi tiết khiếu nại</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row m-b-lg">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Username                                            
                                        </label>
                                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Mã đơn
                                        </label>
                                        <asp:TextBox runat="server" ID="txtMainOrderID" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Mã đơn hàng (từ shop)
                                        </label>
                                        <asp:TextBox runat="server" ID="txtOrderShopCode" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Mã vận đơn
                                        </label>
                                        <asp:TextBox runat="server" ID="txtOrderCode" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Yêu cầu của khách</label>
                                        <br />
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="1">Bồi thường tiền</asp:ListItem>
                                            <asp:ListItem Value="2">Đổi trả hàng</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12" style="display: none">
                                        <label for="exampleInputEmail">
                                            Tỷ giá
                                        </label>
                                        <asp:Label ID="lblCurrence" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-12" style="display: none">
                                        <label for="exampleInputEmail">
                                            Số tiền (VNĐ)
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch" oninput="countte($(this))"
                                            ID="pBuyNDT" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pBuyNDT" ErrorMessage="Không để rỗng"
                                            Display="Dynamic" ForeColor="Red" ValidationGroup="n"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12" style="display: none">
                                        <label for="exampleInputEmail">
                                            Số tiền (¥)
                                        </label>
                                        <asp:Label ID="lblAmountCYN" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Ảnh
                                        </label>
                                        <br />
                                        <asp:Literal ID="ltrImage" runat="server"></asp:Literal>
                                        <%--<asp:Image runat="server" ID="imgDaiDien" Width="200" />--%>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Nội dung
                                        </label>
                                        <asp:TextBox runat="server" ID="txtComplainText" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Nhân viên đặt hàng ghi chú
                                        </label>
                                        <asp:TextBox runat="server" ID="txtStaffComment" CssClass="form-control" TextMode="MultiLine" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Trạng thái</label>
                                        <br />
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                            <%--<asp:ListItem Value="0">Đã hủy</asp:ListItem>
                                            <asp:ListItem Value="1">Chưa Duyệt</asp:ListItem>
                                            <asp:ListItem Value="2">Đang xử lý</asp:ListItem>
                                            <asp:ListItem Value="3">Đã xử lý</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdfCurrency" runat="server" />
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function countte(obj) {
                var currency = parseFloat($("#<%=hdfCurrency.ClientID %>").val());
                var value = obj.val();
                var total = value / currency;
                $("#<%= lblAmountCYN.ClientID%>").text(total);
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
        </script>
    </telerik:RadScriptBlock>


</asp:Content>
