<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="khieu-nai.aspx.cs" Inherits="NHST.khieu_nai1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Khiếu nại</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <%--<a href="/them-khieu-nai" class="btn pill-btn primary-btn admin-btn main-btn hover">Thêm mới</a>--%>
                            <div class="step-income">
                                <table class="customer-table mar-top1 full-width">
                                    <tr>
                                        <th>Ngày</th>
                                        <th>Mã Shop</th>
                                        <%--<th>Tiền bồi thường</th>--%>
                                        <th>Nội dung</th>
                                        <%--<th>NV ghi chú</th>--%>
                                        <th>Trạng thái</th>
                                        <th>Thao tác</th>
                                    </tr>
                                    <tbody>
                                        <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                    </tbody>
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
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Nạp tiền</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
</asp:Content>
