<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BigPackageList.aspx.cs" Inherits="NHST.Admin.BigPackageList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/fonticons.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">Danh sách đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <a type="button" class="btn btn-success m-b-sm" href="/Admin/AddBigPackage.aspx">Thêm kiện lớn</a>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã bao lớn để tìm"></asp:TextBox>
                                    </td>
                                    <td style="width: 50px">
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <th>ID
                                </th>
                                <th>Mã vận đơn
                                </th>
                                <th>User Phone
                                </th>
                                <th>Trọng lượng
                                </th>
                                <th>Kho
                                </th>
                                <th>Ghi chú
                                </th>
                                <th>Trạng thái
                                </th>
                                <th>Trạng thái nhận hàng
                                </th>
                                <th>Trạng thái thanh toán
                                </th>
                                <th></th>
                            </tr>
                            <asp:Literal ID="ltrorderlist" runat="server" EnableViewState="false"></asp:Literal>
                            <%-- <tr>
                                <td>1
                                </td>
                                <td colspan="3">Ngày gửi: 01/07/2017
                                </td>
                                <td>
                                    <a class="btn btn-info btn-sm" target="_blank" href='/Admin/BigPackageDetail.aspx?ID=<%#Eval("ID") %>'>Xem</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>123123123
                                </td>
                                <td>0934064443
                                </td>
                                <td>1.2
                                </td>
                                <td>Hà Nội
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>123123123
                                </td>
                                <td>0934064443
                                </td>
                                <td>1.2
                                </td>
                                <td>Hà Nội
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>123123123
                                </td>
                                <td>0934064443
                                </td>
                                <td>1.2
                                </td>
                                <td>Hà Nội
                                </td>
                            </tr>--%>
                        </table>
                        <div class="pagenavi fl">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                        <%--<telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                            AllowSorting="True" AllowFilteringByColumn="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <NestedViewTemplate>
                                    <asp:Literal ID="ltrListSmallpackage" runat="server"></asp:Literal>
                                </NestedViewTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PackageCode" HeaderText="Mã package" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Ngày gửi">
                                        <ItemTemplate>
                                            <%#Eval("SendDate","{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Ngày đến">
                                        <ItemTemplate>
                                            <%#Eval("ArrivedDate","{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Kho">
                                        <ItemTemplate>
                                            <%#PJUtils.ReturnPlace(Convert.ToInt32(Eval("Place"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" target="_blank" href='/Admin/BigPackageDetail.aspx?ID=<%#Eval("ID") %>'>Xem</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                    PrevPageText="← Previous" />
                            </MasterTableView>
                        </telerik:RadGrid>--%>
                    </div>
                </div>
            </div>
            <!-- Modal -->
        </div>
    </div>
    <style>
        .pagenavi {
            display: flex;
            float: right;
        }

            .pagenavi a,
            .pagenavi span {
                width: 40px;
                height: 40px;
                line-height: 40px;
                text-align: center;
                color: #fff;
                font-weight: bold;
                background: #22BAA0;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
                text-decoration: none;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #12AFCB;
                    color: #fff;
                }

        .table.table-bordered > thead > tr > th, .table.table-bordered > tbody > tr > th, .table.table-bordered > tfoot > tr > th, .table.table-bordered > thead > tr > td, .table.table-bordered > tbody > tr > td, .table.table-bordered > tfoot > tr > td {
            color: #000 !important;
        }
    </style>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

</asp:Content>
