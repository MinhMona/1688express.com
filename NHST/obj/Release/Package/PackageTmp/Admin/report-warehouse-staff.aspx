<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="report-warehouse-staff.aspx.cs" Inherits="NHST.Admin.report_warehouse_staff" %>
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
                    <h3 class="panel-title semi-text text-uppercase">Thống kê nhân viên kiểm hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="row m-b-lg">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="exampleInputEmail">
                                        Username
                                    </label>
                                    <asp:DropDownList ID="ddlUsername" runat="server" CssClass="form-control select2" AppendDataBoundItems="true"
                                        DataValueField="ID" DataTextField="Username">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
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
                                <div class="col-md-3">
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
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-success" Text="Xem"
                                        OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 20px;">
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng kiện đã kiểm</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblPackageCount" runat="server" Text="0"></asp:Label>
                                                kiện</span>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng kg đã kiểm</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblPackageWeight" runat="server" Text="0"></asp:Label>
                                                kg</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
            function thanhtoanHoahong(ID, obj) {
                var money = parseFloat(obj.parent().parent().find(".moneyrose").attr("data-value"));
                if(money>0)
                {
                    $.ajax({
                        type: "POST",
                        url: "/admin/admin-staff-income.aspx/UpdateStatus",
                        data: "{ID:'" + ID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == 1) {
                                var status = obj.parent().parent().find(".statusThanhtoan");
                                status.html("<span class=\"bg-blue\">Đã thanh toán</span>");
                                obj.remove();
                            }
                            else {
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert(errorthrow);
                        }
                    });
                }
                else
                {
                    alert('Hoa hồng chưa có');
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
