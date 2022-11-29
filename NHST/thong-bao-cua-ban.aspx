<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="thong-bao-cua-ban.aspx.cs" Inherits="NHST.thong_bao_cua_ban" %>

<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Thông báo của bạn</h4>
                <div class="primary-form custom-width">
                    <asp:Button ID="btnUpdate" runat="server" Text="Đánh dấu tất cả đã đọc" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover" OnClick="btnUpdate_Click" />
                    <div class="step-income">
                        <table class="customer-table mar-top1 full-width">
                            <tr>
                                <th>Ngày</th>
                                <th>Mã đơn hàng</th>
                                <th>Nội dung</th>
                                <th>Trạng thái</th>
                            </tr>
                            <asp:Literal ID="ltr" runat="server"></asp:Literal>
                        </table>
                        <div class="pagination">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

   <%-- <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Thông báo của bạn</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    
                </div>
            </div>
        </div>
    </main>--%>

</asp:Content>
