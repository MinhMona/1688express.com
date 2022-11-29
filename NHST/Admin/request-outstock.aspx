<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="request-outstock.aspx.cs" Inherits="NHST.Admin.request_outstock" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách yêu cầu xuất kho</h3>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltraddminre" runat="server"></asp:Literal>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã vận đơn để tìm"></asp:TextBox>
                                    </td>
                                    <td style="width: 50px">
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn btn-info" OnClick="btnSearch_Click" />
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
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridTemplateColumn HeaderText="Mã vận đơn" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="OrderTransactionCode" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID")))!=null ?
                                                SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID"))).OrderTransactionCode :
                                                ""
                                            %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Cân nặng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="Weight" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID")))!=null ?
                                                Math.Round(SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID"))).Weight.ToString().ToFloat(0),2).ToString() + " kg":
                                                "0"
                                            %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>                                    
                                    <telerik:GridBoundColumn DataField="OrderTransactionCode" HeaderText="Mã vận đơn" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Weight" HeaderText="Cân nặng" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MainOrderID" HeaderText="Mã đơn hàng Mua hộ" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TransportationID" HeaderText="Mã đơn hàng VC hộ" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UserNameCus" HeaderText="Tài khoản khách hàng" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateInVNWarehouse" HeaderText="Ngày về kho đích" HeaderStyle-Width="10%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateExWarehouse" HeaderText="Ngày xuất kho" HeaderStyle-Width="10%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Statusstr" HeaderText="Trạng thái" HeaderStyle-Width="5%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày yêu cầu XK" HeaderStyle-Width="10%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="HTVC" HeaderText="HTVC" HeaderStyle-Width="15%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Note" HeaderText="Ghi chú" HeaderStyle-Width="15%"
                                        CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridTemplateColumn HeaderText="Ngày về kho đích" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="FirstName" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID")))!=null ?
                                                string.Format("{0:dd/MM/yyyy}",SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID"))).DateInLasteWareHouse) :
                                                ""
                                            %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ngày xuất kho" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="FirstName" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID")))!=null ?
                                                SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID"))).DateOutWH !=null ? 
                                                string.Format("{0:dd/MM/yyyy}",SmallPackageController.GetByID(Convert.ToInt32(Eval("SmallPackageID"))).DateOutWH):"" 
                                                :
                                                ""
                                            %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="FirstName" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# PJUtils.requestOutStockStatus(Convert.ToInt32(Eval("Status"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="CreatedDate" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%#Eval("CreatedDate")!=null ? string.Format("{0:dd/MM/yyyy HH:mm}",Convert.ToDateTime(Eval("CreatedDate"))): ""%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="HTVC" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="HTVC" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# Eval("ExportRequestTurnID") != null ? 
                                                    ExportRequestTurnController.GetByID(Convert.ToInt32(Eval("ExportRequestTurnID"))) != null ?
                                                    ShippingTypeVNController.GetByID(Convert.ToInt32(ExportRequestTurnController.GetByID(Convert.ToInt32(Eval("ExportRequestTurnID"))).ShippingTypeInVNID)).ShippingTypeVNName : ""
                                                    : "" %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ghi chú" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="HTVC" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <%# Eval("ExportRequestTurnID") != null ? 
                                                    ExportRequestTurnController.GetByID(Convert.ToInt32(Eval("ExportRequestTurnID"))) != null ?
                                                    ExportRequestTurnController.GetByID(Convert.ToInt32(Eval("ExportRequestTurnID"))).Note : ""
                                                    : "" %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>

                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <%#Convert.ToInt32(Eval("Status")) == 1?
                                                "<a class=\"btn btn-info btn-sm\" href=\"javascript:;\" onclick=\"Updatestatus($(this),"+Eval("ID")+")\">Đã xuất</a>" : "" 
                                            %>
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
            function add_loading() {
                var html = "";
                html += "<div id=\"loadingUpdate\" style=\"position: absolute; width: 1098px; height: 2008px; left: 404px; top: 502px; text-align: center; z-index: 90000; opacity:0.9;background:#fff;\">";
                html += "   <div class=\"loading\">";
                html += "       <img src=\"/App_Themes/NewUI/images/loading.gif\" alt=\"loading\">";
                html += "   </div>";
                html += "</div>";
                $("body").append(html);
            }
            function remove_loading() {
                $("#loadingUpdate").remove();
            }

            function Updatestatus(obj, id) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/admin/request-outstock.aspx/updateStatus",
                    data: "{ID:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var parent = obj.parent().parent();
                        var stt = parent.find(".statusre").removeClass("bg-red").addClass("bg-blue").html("Đã xuất");
                        obj.remove();
                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
