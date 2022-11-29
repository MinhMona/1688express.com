<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Withdraw-List.aspx.cs" Inherits="NHST.Admin.Withdraw_List" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách lệnh rút tiền</h3>
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltr" runat="server" Visible="false"></asp:Literal>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập Username để tìm kiếm"></asp:TextBox>
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
                            AllowSorting="True" AllowFilteringByColumn="false">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("Amount"))) %> vnđ</p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <p class=""><%# PJUtils.ReturnStatusWithdraw(Convert.ToInt32(Eval("Status"))) %></p>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
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
                                            <a class="btn btn-info btn-sm" href='/Admin/withdrawdetail.aspx?id=<%#Eval("ID") %>'>Xem</a>
                                            <a class="btn btn-info btn-sm" href='javascript:;' onclick="printPhieuchi('<%#Eval("ID") %>')">In phiếu chi</a>
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
    <div id="printcontent" style="display: none">
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
            function printPhieuchi(ID) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Withdraw-List.aspx/GetData",
                    data: "{ID:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data != "null") {
                            var ret = JSON.parse(data);
                            var html = "";
                            html += "<div class=\"print-bill\">";
                            html += "   <div class=\"top\">";
                            html += "       <div class=\"left\">";
                            html += "           <span class=\"company-info\">VITI EXPRESS</span>";
                            html += "           <span class=\"company-info\">Địa chỉ: T6/08 Phạm Văn Đồng Hà Nội</span>";
                            html += "       </div>";
                            html += "       <div class=\"right\">";
                            html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                            html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-title\">";
                            html += "       <h1>PHIẾU CHI</h1>";
                            html += "       <span class=\"bill-date\">" + ret.CreateDate + " </span>";
                            html += "   </div>";
                            html += "   <div class=\"bill-content\">";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Họ và tên người nhận tiền: </label>";
                            html += "           <label class=\"row-info\">" + ret.FullName + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Địa chỉ: </label>";
                            html += "           <label class=\"row-info\">" + ret.Address + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Lý do chi: </label>";
                            html += "           <label class=\"row-info\"></label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Số tiền: </label>";
                            html += "           <label class=\"row-info\">" + ret.Money + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Bằng chữ: </label>";
                            html += "           <label class=\"row-info\"></label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <div class=\"row-col\">";
                            html += "               <label class=\"row-name\">Kèm theo: </label>";
                            html += "               <label class=\"row-info\"></label>";
                            html += "           </div>";
                            html += "           <div class=\"row-col\">";
                            html += "               <label class=\"row-name\">Chứng từ gốc: </label>";
                            html += "               <label class=\"row-info\"></label>";
                            html += "           </div>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-footer\">";
                            html += "       <div class=\"bill-row-all right\">" + ret.CreateDate + "</div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-footer\">";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Giám đốc</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên, đóng dấu)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Kế toán trưởng</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Người nộp tiền</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Thủ quỹ</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";
                            $("#printcontent").html(html);

                            printDiv('printcontent');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                    }
                });

            }
            function printDiv(divid) {
                var divToPrint = document.getElementById('' + divid + '');
                var newWin = window.open('', 'Print-Window');
                newWin.document.open();
                newWin.document.write('<html><head><link rel="stylesheet" href="/App_Themes/NewUI/css/custom.css" type="text/css"/></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                newWin.document.close();
                setTimeout(function () { newWin.close(); }, 10);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
