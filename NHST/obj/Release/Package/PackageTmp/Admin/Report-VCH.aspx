<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report-VCH.aspx.cs" Inherits="NHST.Admin.Report_VCH" %>

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
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">Thống kê cước Vận Chuyển Hộ
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                                            DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server" Width="100%">
                                            </DateInput>
                                        </telerik:RadDateTimePicker>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" Width="100%" Height="40px"
                                            Style="margin-top: 10px;" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="table-responsive">
                        <asp:Panel ID="pEdit" runat="server">
                            <div class="col-md-7" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng cân nặng</span>
                                        <span class="label-infor">
                                            <strong>
                                                <asp:Label ID="lblWeightAll" runat="server"></asp:Label>
                                                kg</strong></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px">
                                        <span class="label-title">Tổng cước (VNĐ)</span>
                                        <span class="label-infor">
                                            <strong>
                                                <asp:Label ID="lblPriceAllVND" runat="server"></asp:Label>
                                                VNĐ</strong></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px; margin-bottom: 50px;">
                                        <span class="label-title">Tổng cước (Tệ)</span>
                                        <span class="label-infor">
                                            <strong>
                                                <asp:Label ID="lblPriceAllCYN" runat="server"></asp:Label>
                                                Tệ</strong></span>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
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
                                    <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Tên tài khoản" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateRequest" HeaderText="Ngày YCXK" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateOutWH" HeaderText="Ngày XK" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalPackages" HeaderText="Tổng số kiện" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalWeight" HeaderText="Tổng số kg" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalPrice" HeaderText="Tổng cước" HeaderStyle-Width="10%" FilterControlWidth="50px"
                                        AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="HTVC" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="HTVC" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# ShippingTypeVNController.GetByID(Convert.ToInt32(Eval("ShippingTypeInVNID")))!= null?
                                                    ShippingTypeVNController.GetByID(Convert.ToInt32(Eval("ShippingTypeInVNID"))).ShippingTypeVNName : ""%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ghi Chú" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <textarea class="txtNote"><%#Eval("StaffNote") %></textarea>
                                            <a href="javascript:;" class="btn btn-info"
                                                onclick="updateNote($(this),'<%#Eval("ID") %>')">Cập nhật</a>
                                            <span class="update-info" style="width: 100%; clear: both; float: left; color: blue; display: none">Cập nhật thành công</span>
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
            <!-- Modal -->
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
            <%--<telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="p" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function updateNote(obj, ID) {
                var staffNote = obj.parent().find(".txtNote").val();
                $.ajax({
                    type: "POST",
                    url: "/admin/Report-VCH.aspx/UpdateStaffNote",
                    data: "{ID:'" + ID + "',staffNote:'" + staffNote + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var ret = msg.d;
                        if (ret == "ok") {
                            obj.parent().find(".update-info").show();
                        }
                        else {
                            obj.parent().find(".update-info").hide();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
