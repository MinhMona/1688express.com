<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RequestShipDetail.aspx.cs" Inherits="NHST.Admin.RequestShipDetail" %>
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
                        <h3 class="panel-title semi-text text-uppercase">Thông tin yêu cầu ký gửi</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Username
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Phone
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Danh sách mã vận đơn
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtListOrderCode" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Ghi chú
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Trạng thái
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Đang chờ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Duyệt" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Hủy" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block large-btn"
                                        OnClick="btncreateuser_Click" />
                                    <a href="/admin/request-ship-list.aspx" class="btn btn-success btn-block large-btn">Trở về</a>
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
            <telerik:AjaxSetting AjaxControlID="btncreateuser">
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
    </script>
</asp:Content>
