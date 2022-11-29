<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="chi-tiet-khieu-nai.aspx.cs" Inherits="NHST.chi_tiet_khieu_nai" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Thêm khiếu nại</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <div class="step-income">
                                <asp:Panel ID="pn" runat="server">
                                    <div class="form-row">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Mã đơn hàng: </div>
                                        <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control full-width"
                                             Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Mã vận đơn: </div>
                                        <asp:TextBox ID="txtOrderCode" runat="server" CssClass="form-control full-width"
                                             Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Yêu cầu của quý khách: </div>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Enabled="false">
                                            <asp:ListItem Value="1" Text="Bồi thường tiền"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Đổi trả hàng"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-full">Ảnh đối chiếu: </div>
                                        <asp:Literal ID="ltrIMG" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Nội dung khách khiếu nại: </div>
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Nội dung nhân viên phản hồi: </div>
                                        <asp:TextBox ID="txtStaffComment" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-row">
                                        <div class="lb width-not-full">Trạng thái: </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" Enabled="false">
                                            <asp:ListItem Value="0">Đã hủy</asp:ListItem>
                                            <asp:ListItem Value="1">Chưa Duyệt</asp:ListItem>
                                            <asp:ListItem Value="2">Đang xử lý</asp:ListItem>
                                            <asp:ListItem Value="3">Đã xử lý</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row btn-row">
                                        <a href="/khieu-nai" class="btn pill-btn primary-btn admin-btn mar-top3 main-btn hover">Trở về</a>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }

        .btn.pill-btn {
            font-weight: bold;
            text-transform: uppercase;
        }
    </style>
    <asp:HiddenField ID="hdfProductID" runat="server" />
    <asp:HiddenField ID="hdfOrderShopCode" runat="server" />
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading1">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NHST/loading1.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
    <!-- END CONTENT -->
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
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
    </telerik:RadCodeBlock>
    <style>
        .RadUpload_Metro .ruFakeInput {
            float: left;
            width: 60%;
        }

        .page.account-management .right-content .right-side {
            padding-left: 20px;
        }

        div.RadUploadSubmit, div.RadUpload_Metro .ruButton {
            padding: 0;
        }
    </style>
</asp:Content>
