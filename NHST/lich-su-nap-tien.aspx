<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="lich-su-nap-tien.aspx.cs" Inherits="NHST.lich_su_nap_tien1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Lịch sử giao dịch</h4>
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
                                <table class="customer-table mar-top1 full-width">
                                    <tr>
                                        <th width="20%" style="text-align: center">Ngày giờ</th>
                                        <th width="20%" style="text-align: center">Nội dung</th>
                                        <th width="20%" style="text-align: center">Số tiền</th>
                                        <th width="20%" style="text-align: center">Loại giao dịch</th>
                                        <th width="20%" style="text-align: center">Số dư</th>
                                    </tr>
                                    <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                </table>
                                <div class="pagination">
                                    <%this.DisplayHtmlStringPaging1();%>
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
                <div class="container text-center container-800">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Lịch sử giao dịch</span></h3>
                   
                </div>
            </section>
        </div>
    </main>--%>
</asp:Content>
