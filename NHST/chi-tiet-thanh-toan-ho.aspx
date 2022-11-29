<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="chi-tiet-thanh-toan-ho.aspx.cs" Inherits="NHST.chi_tiet_thanh_toan_ho" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .row-item {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
            padding: 0px 10%;
        }

        .row-left {
            float: left;
            width: 15%;
        }

        .row-right {
            float: left;
            width: 82%;
            margin-left: 20px;
        }

        .rowfull {
            float: left;
            margin-right: 20px;
            width: 100%;
            text-transform: uppercase;
            margin: 5px 0;
        }

        .left-rowfull {
            float: left;
            width: 20%;
            margin-right: 10px;
        }

        .right-rowfull {
            float: left;
            width: 75%;
        }

        .form-control {
            float: left;
            width: 100%;
        }

        textarea.form-control {
            min-height: 100px;
        }

        .text-align-center {
            text-align: center;
        }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url(/App_Themes/NewUI/images/icon-plus.png) center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .float-right {
            float: right;
        }

        .border-ra-5px {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
        }

        .width-custom {
            width: 850px;
            margin: 0 auto;
            float: none;
        }

        .border-top {
            border-top: solid 1px #333;
            padding-top: 10px;
        }

        .border-bottom {
            border-bottom: solid 1px #333;
            padding-bottom: 10px;
        }

        .itemaddmore {
            float: left;
            width: 100%;
            clear: both;
        }

        .border-ra-bg {
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            border: solid 1px #ddd;
            background: #fdfdfd;
            padding: 10px;
        }

        .lblstrongcolor {
            color: #043904;
        }

        .custom-border-padding {
            border-top: solid 1px #ccc;
            padding-top: 30px;
        }
        .text-uppercase
        {
            text-transform:uppercase;
        }
        
        .rowhalf {
            float: left;
            width: 45%;
        }

        .label-field {
            float: left;
            width: 35%;
        }

        .textbox-field {
            float: left;
            width: 60%;
        }

        .rowpart {
            float: left;
            width: 10%;
        }

        .nomin-width {
            min-width: unset !important;
            width: 100%;
            padding: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Chi tiết yêu cầu thanh toán hộ</h4>
                <div class="primary-form">
                    <div class="container">
                        <div class="main-content policy clear">
                            <div class="form-confirm-send width-custom">
                                <div class="form-group">
                                    <div class="row-item border-bottom">
                                        <div class="row-left">Tên đăng nhập:</div>
                                        <div class="row-right">
                                            <strong class="lblstrongcolor">
                                                <asp:Literal ID="ltrIfn" runat="server"></asp:Literal></strong>
                                        </div>
                                    </div>
                                    <div class="itemaddmore border-bottom">
                                        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                    </div>
                                    <div class="row-item mar-top-1">
                                        <div class="rowfull">
                                            Tổng tiền (đơn vị: tệ)
                                        </div>
                                        <div class="rowfull">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row-item ">
                                        <div class="rowfull">
                                            Tổng tiền (đơn vị: VNĐ)
                                        </div>
                                        <div class="rowfull">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                                ID="pVND" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row-item ">
                                        <div class="rowfull">
                                            Tỉ giá
                                        </div>
                                        <div class="rowfull">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control border-ra-bg" Skin="MetroTouch"
                                                ID="rTigia" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row-item">
                                        <div class="rowfull">Ghi chú: </div>
                                        <div class="rowfull">
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control border-ra-bg" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row-item">
                                        <div class="row-left text-uppercase">Trạng thái: </div>
                                        <div class="row-right">
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-item text-center mar-top4">
                                        <asp:Literal ID="ltrPay" runat="server" Visible="false"></asp:Literal>

                                        <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="pill-btn btn order-btn main-btn hover submit-btn" OnClick="btnUpdate_Click" Visible="false" />
                                        <asp:Button ID="btnSend" runat="server" Text="Thanh toán" CssClass="pill-btn btn order-btn main-btn hover submit-btn" OnClick="btnSend_Click" Style="display: none" />
                                        <a href="/thanh-toan-ho" class="pill-btn btn order-btn main-btn hover submit-btn border-ra-5px">Trở về</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <script type="text/javascript">       
        function paymoney()
        {
            var r = confirm("Bạn muốn thanh toán yêu cầu này?");
            if (r == true) {               
                $("#<%=btnSend.ClientID%>").click();
            }
            else {

            }
            
        }       
    </script>
</asp:Content>
