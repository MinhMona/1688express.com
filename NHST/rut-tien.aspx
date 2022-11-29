<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="rut-tien.aspx.cs" Inherits="NHST.rut_tien1" %>

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
                <h4 class="sec__title center-txt">Rút tiền</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <table class="customer-table mar-bot3 full-width font-size-16">
                                <tr style="font-weight: bold">
                                    <td>Số dư tài khoản
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="step-income">
                                <asp:Panel ID="pn" runat="server">
                                    <h2 class="content-title">Tạo lệnh rút tiền</h2>
                                    <div class="form-row">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row mar-top2">
                                        <div class="lb width-not-full">Số tiền cần rút: </div>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="100000"
                                            NumberFormat-GroupSizes="3" Width="80%" placeholder="Số tiền muốn rút" Value="100000">
                                        </telerik:RadNumericTextBox>
                                        <%--<a href="javascript:;" onclick="confirmrutien()" class="btn btn-success btn-block pill-btn primary-btn">Tạo lệnh rút tiền</a>--%>
                                    </div>
                                    <div class="form-row mar-top2">
                                        <div class="lb width-not-full">Nội dung: </div>
                                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                        
                                    </div>
                                    <div class="form-row btn-row">
                                        <a href="javascript:;" onclick="confirmrutien()" style="line-height: 40px;" class="pill-btn btn order-btn main-btn hover">Tạo lệnh rút tiền</a>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="step-income">
                                <h2 class="content-title">Danh sách rút tiền</h2>
                                <div class="step-income">
                                    <table class="customer-table mar-top1 full-width center-data">
                                        <tr>
                                            <th width="20%" style="text-align: center">Ngày giờ</th>
                                            <th width="20%" style="text-align: center">Số tiền</th>
                                            <th width="20%" style="text-align: center">Trạng thái</th>
                                            <th width="20%" style="text-align: center"></th>
                                        </tr>
                                        <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Rút tiền</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
    <asp:Button ID="btnCreate" runat="server" Text="Tạo lệnh rút tiền" CssClass="btn btn-success btn-block pill-btn primary-btn"
        OnClick="btnCreate_Click" Style="display: none" />
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }

        .center-data th, .center-data td {
            text-align: center;
        }
    </style>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCreate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pn" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="s" runat="server">
        <script type="text/javascript">
            function confirmrutien() {
                var r = confirm("Bạn muốn tạo lệnh rút tiền?");
                if (r == true) {
                    $("#<%=btnCreate.ClientID%>").click();
                }
                else {
                }
            }
            function cancelwithdraw(id) {
                var r = confirm("Bạn muốn hủy lệnh rút tiền?");
                if (r == true) {
                    $.ajax({
                        type: "POST",
                        url: "rut-tien.aspx/cancelwithdraw",
                        data: "{ID:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "1") {
                                location.reload();
                            }
                            else {
                                alert('Có lỗi trong quá trình xử lý, vui lòng thử lại sau');
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi');
                        }
                    });
                }
                else {
                }

            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
