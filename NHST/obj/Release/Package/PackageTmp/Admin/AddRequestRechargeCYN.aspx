<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddRequestRechargeCYN.aspx.cs" Inherits="NHSG.admin.AddRequestRechargeCYN" %>
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
                        <h3 class="panel-title semi-text text-uppercase">Yêu cầu nạp tiền</h3>
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
                                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="txtUsername" ErrorMessage="Không để rỗng"
                                            Display="Dynamic" ForeColor="Red" ValidationGroup="insert"></asp:RequiredFieldValidator>
                                    </div>                                   
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Số tiền
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="3" NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="pAmount" ErrorMessage="Không để rỗng"
                                            Display="Dynamic" ForeColor="Red" ValidationGroup="insert"></asp:RequiredFieldValidator>
                                    </div>                                    
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Ghi chú
                                        </label>
                                        <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Trạng thái</label>
                                        <br />
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="1">Chưa Duyệt</asp:ListItem>
                                            <asp:ListItem Value="2">Đã duyệt</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Button runat="server" ID="btnSave" Text="Tạo mới" CssClass="btn btn-success" ValidationGroup="insert" OnClick="btnSave_Click" />
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
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
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
