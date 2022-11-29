<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="transportation-list.aspx.cs" Inherits="NHST.Admin.transportation_list" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
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
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã vận đơn"></asp:TextBox>
                                    </td>
                                    <td>
                                        Kho đích:
                                        <asp:DropDownList ID="ddlWarehouseTo" runat="server" CssClass="form-control"
                                            DataTextField="WareHouseName" DataValueField="ID">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Phương thức vận chuyển: 
                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Đi nhanh"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Đi thường"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Giá từ: 
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rPriceFrom" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Giá từ" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>Giá đến:
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rPriceTo" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Giá đến" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Từ ngày: 
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td>Đến ngày: 
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" multiple>
                                            <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Đang xử lý"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Đang về kho đích"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Đã nhận hàng tại kho đích"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Khách đã thanh toán"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Đã hoàn thành"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <a href="javascript:;" class="btn btn-info" onclick="fulterGet()">Tìm kiếm</a>
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="table-responsive">
                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                            AllowSorting="true" AllowFilteringByColumn="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Username" HeaderText="User đặt hàng" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPrice"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposited"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Tổng cân nặng (kg)" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("TotalWeight"))) %> kg</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Kho đích" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class="">
                                                <%#WarehouseController.GetByID(Convert.ToInt32(Eval("WarehouseID")))!=null
                                            ? WarehouseController.GetByID(Convert.ToInt32(Eval("WarehouseID"))).WareHouseName
                                            : ""%>
                                            </p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Kho đích" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class="">
                                                <%#PJUtils.generateTransportationStatus(Convert.ToInt32(Eval("status")))%>
                                            </p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
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

                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" href='/admin/transportationdetail.aspx?id=<%#Eval("ID") %>'>Chi tiết</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                    PrevPageText="← Previous" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfStatus" runat="server" Value="-1" />
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NewUI/images/loading.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
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
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function fulterGet() {
                var st = $("#<%=ddlStatus.ClientID%>").val();
                $("#<%=hdfStatus.ClientID%>").val(st);
                $("#<%=btnSearch.ClientID%>").click();
            }
            $(document).ready(function () {
                $("#tag").select2({
                    tags: true,
                    maximumInputLength: 10,

                    templateSelection: function (selection) {
                        if (!selection.id) {
                            return selection.text;
                        }
                        return $('<span class="' + selection.id + '">' + selection.text + '</span>');
                    }
                });
            });
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
