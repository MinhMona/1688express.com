<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="WeigtPricehList.aspx.cs" Inherits="NHST.Admin.WeigtPricehList" %>
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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách giá cân nặng</h3>
                </div>
                <div class="panel-body">
                    <a type="button" class="btn btn-success m-b-sm" href="/Admin/AddWeightPrice.aspx">Thêm mới</a>
                    <div class="table-responsive">

                        <table class="table table-bordered">
                            <tr>
                                <td style="width: 10%">Giá tiền ở Hà Nội</td>
                                <td>
                                    <asp:Label ID="lblPriceHN" runat="server"></asp:Label>
                                    VNĐ
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 10%">Giá tiền ở Việt Trì</td>
                                <td>
                                    <asp:Label ID="lblPriceSG" runat="server"></asp:Label>
                                    VNĐ
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="table-responsive">
                        <telerik:radgrid runat="server" id="gr" onneeddatasource="r_NeedDataSource" autogeneratecolumns="False"
                            allowpaging="True" pagesize="20" enableembeddedskins="False" enableembeddedbasestylesheet="False"
                            allowautomaticupdates="True" onitemcommand="r_ItemCommand" onpageindexchanged="gr_PageIndexChanged"
                            allowsorting="True">
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="WeightFrom" HeaderText="Trọng lượng từ" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="WeightTo" HeaderText="Trọng lượng đến" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Khu vực">
                                        <ItemTemplate>
                                            <%#PJUtils.ReturnPlace(Convert.ToInt32(Eval("Place"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Loại">
                                        <ItemTemplate>
                                            <%#PJUtils.ReturnTypeFastSlow(Convert.ToInt32(Eval("TypeFastSlow"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 1">
                                        <ItemTemplate>
                                            <%#Eval("Vip1","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 2">
                                        <ItemTemplate>
                                            <%#Eval("Vip2","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 3">
                                        <ItemTemplate>
                                            <%#Eval("Vip3","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 4">
                                        <ItemTemplate>
                                            <%#Eval("Vip4","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 5">
                                        <ItemTemplate>
                                            <%#Eval("Vip5","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 6">
                                        <ItemTemplate>
                                            <%#Eval("Vip6","{0:N0}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" target="_blank" href='/Admin/WeightPriceInfo.aspx?ID=<%#Eval("ID") %>'>Sửa</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                    PrevPageText="← Previous" />
                            </MasterTableView>
                        </telerik:radgrid>
                    </div>
                </div>
            </div>
            <!-- Modal -->
        </div>
    </div>
    <telerik:radajaxmanager id="rAjax" runat="server">
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
    </telerik:radajaxmanager>


</asp:Content>
