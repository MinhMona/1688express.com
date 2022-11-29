<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="thong-ke-cuoc-vc-ho.aspx.cs" Inherits="NHST.thong_ke_cuoc_vc_ho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">Thống kê cước vận chuyển hộ</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <div class="step-income">
                                <table class="customer-table mar-top1 full-width">
                                    <tr>
                                        <th width="5%" style="text-align: center">ID</th>
                                        <th width="5%" style="text-align: center">Ngày YCXK</th>
                                        <th width="20%" style="text-align: center">Ngày XK</th>
                                        <th width="5%" style="text-align: center">Tổng số kiện</th>
                                        <th width="5%" style="text-align: center">Tổng số Kg</th>
                                        <th width="20%" style="text-align: center">Tổng cước</th>
                                        <th width="20%" style="text-align: center">HTVC</th>
                                        <th width="20%" style="text-align: center">Ghi chú</th>
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
