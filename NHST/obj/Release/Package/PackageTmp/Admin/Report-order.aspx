<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report-order.aspx.cs" Inherits="NHST.Admin.Report_order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">Thống kê đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="row m-b-lg">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="5" Text="Đã mua hàng"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Đang chuyển về kho đích"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Đã nhận hàng tại kho đích"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Khách đã thanh toán"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-success" Text="Xem" OnClick="btnFilter_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pninfo" runat="server" Visible="true">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <asp:Literal ID="ltrinf" runat="server"></asp:Literal>                               
                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="row">
                                    <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                        AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                        AllowSorting="True" AllowFilteringByColumn="True">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="STT" HeaderText="STT" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                                    CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ảnh sản phẩm" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <img src="<%#Eval("ProductImage") %>" width="100%" alt />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%-- <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên Shop" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> vnđ</p>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> vnđ</p>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="username" HeaderText="User đặt hàng" HeaderStyle-Width="15%"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="dathang" HeaderText="Nhân viên đặt hàng" HeaderStyle-Width="15%"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="kinhdoanh" HeaderText="Nhân viên kinh doanh" HeaderStyle-Width="15%"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridBoundColumn DataField="khotq" HeaderText="Nhân viên kho TQ" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="khovn" HeaderText="Nhân viên kho VN" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="Ngày đặt hàng" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                                    CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <%--<%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>--%>
                                                        <%#Eval("CreatedDate")!=null ? 
                                                            string.Format("{0:dd/MM/yyyy HH:mm}",Convert.ToDateTime(Eval("CreatedDate")))
                                                            : ""%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="statusstring" HeaderText="Trạng thái đơn hàng" HeaderStyle-Width="15%"
                                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                                    <ItemTemplate>
                                                        <a class="btn btn-info btn-sm" href='/Admin/OrderDetail.aspx?id=<%#Eval("ID") %>'>Xem</a>
                                                        <a class="btn btn-info btn-sm" href='/Admin/Pay-Order.aspx?id=<%#Eval("ID") %>'>Thanh toán</a>

                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                PrevPageText="← Previous" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- </asp:Panel>--%>
    <%-- <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadScriptBlock runat="server">
    </telerik:RadScriptBlock>

</asp:Content>
