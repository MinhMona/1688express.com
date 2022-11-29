<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="danh-muc-trang.aspx.cs" Inherits="NHST.danh_muc_trang1" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <h4 class="sec__title center-txt">
                    <asp:Label ID="lblTitle" runat="server" EnableViewState="false"></asp:Label></h4>
                <div class="primary-form">
                    <div class="services-page clearfix">
                        <uc:SideBar ID="SideBar1" runat="server" />
                        <div class="services-content">
                            <asp:Literal ID="ltrSummary" runat="server" EnableViewState="false"></asp:Literal>
                            <div class="services-list clearfix">
                                <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                <div class="pagination">
                                    <%this.DisplayHtmlStringPaging1();%>
                                </div>
                            </div>
                            <div class="cmt" style="display: none;">
                                <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                                <%--<img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>
</asp:Content>
