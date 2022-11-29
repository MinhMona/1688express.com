﻿<%@ Page Title="" Language="C#" MasterPageFile="~/1688Master.Master" AutoEventWireup="true" CodeBehind="chi-tiet-trang.aspx.cs" Inherits="NHST.chi_tiet_trang2" %>

<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p {
            text-align: initial;
        }

        .intro-page table {
            width: 100% !important;
        }
    </style>
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
                        <div class="services-content intro-align">
                            <div class="intro-page">
                                <asp:Literal ID="ltrDetail" runat="server"></asp:Literal>
                            </div>
                            <div class="cmt" style="display: none;">
                                <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                                <%--<img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">--%>
                            </div>
                            <div class="other-page">
                                <div class="line-head black-gray"></div>
                                <h2 class="other-header-title">Bài viết cùng chuyên mục</h2>
                                <ul class="list-other-news">
                                    <asp:Literal ID="ltrNewsOther" runat="server"></asp:Literal>
                                    <%--<li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>
                        <li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>
                        <li>
                            <a href="javascript:;">Bài viết cùng chuyên mục</a>
                        </li>--%>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>
    
</asp:Content>
