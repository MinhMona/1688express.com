<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report-recharge.aspx.cs" Inherits="NHST.Admin.Report_recharge" %>
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
                    <h3 class="panel-title semi-text text-uppercase">Thống kê thanh toán</h3>
                </div>
                <div class="panel-body">
                    <div class="row m-b-lg">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5">
                                    <label for="exampleInputEmail">
                                        Từ ngày
                                    </label>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdatefrom" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate">
                                        <TimeView TimeFormat="HH:mm" runat="server">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>
                                <div class="col-md-5">
                                    <label for="exampleInputEmail">
                                        Đến ngày
                                    </label>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdateto" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate">
                                        <TimeView TimeFormat="HH:mm" runat="server">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-success" Text="Xem" OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <style>
                                
                            </style>
                        <asp:Panel ID="pninfo" runat="server" Visible="true">
                            <div class="col-md-12" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền đã nạp</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblDeposit" runat="server"></asp:Label></span>
                                    </div>
                                </div>                               
                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">
                                
                                <div class="row">
                                    <telerik:RadGrid runat="server" ID="RadGrid1" OnNeedDataSource="RadGrid1_NeedDataSource" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                        AllowAutomaticUpdates="True" OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                                        AllowSorting="True" OnPageSizeChanged="RadGrid1_PageSizeChanged">
                                        <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Status">
                                                    <ItemTemplate>
                                                        <%#Eval("Amount","{0:N0}") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Status">
                                                    <ItemTemplate>
                                                        <%#PJUtils.ReturnStatusWithdraw(Convert.ToInt32(Eval("Status"))) %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="CreatedDate">
                                                    <ItemTemplate>
                                                        <%--<%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>--%>
                                                        <%#Eval("CreatedDate")!=null ? 
                                                string.Format("{0:dd/MM/yyyy HH:mm}",Convert.ToDateTime(Eval("CreatedDate")))
                                                : ""%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Người tạo" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
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
        <script type="text/javascript">
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
