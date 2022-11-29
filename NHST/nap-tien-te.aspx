<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="nap-tien-te.aspx.cs" Inherits="NHST.nap_tien_te" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-nap-tien {
            float: left;
            width: 100%;
        }

            .page-nap-tien tr {
                float: left;
                width: 100%;
                margin-bottom: 10px;
            }

                .page-nap-tien tr th {
                    float: left;
                    width: 20%;
                    text-align: left;
                    vertical-align: middle;
                    min-height: 1px;
                    font-weight: bold;
                }

                .page-nap-tien tr td {
                    float: left;
                    width: 80%;
                    text-align: left;
                    margin-bottom: 10px;
                }

                    .page-nap-tien tr td textarea {
                        min-height: 150px;
                        width: 100%;
                        border: solid 1px #e1e1e1;
                        padding: 10px;
                    }
                    .table-panel-main table td
                    {
                        text-align:center;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Nạp tiền tệ</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <div class="main-content policy clear">
                                <div class="form-confirm-send">                                    
                                    <div class="form-group">
                                        <asp:Panel ID="Parent" runat="server">
                                            <table class="page-nap-tien">
                                                <tr>
                                                    <th>Tên đăng nhập:	</th>
                                                    <td>
                                                        <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Số tiền</th>
                                                    <td>
                                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                            ID="pAmount" NumberFormat-DecimalDigits="3" Value="0"
                                                            NumberFormat-GroupSizes="3" Width="100%">
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>Ghi chú</th>
                                                    <td>
                                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th></th>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnSend" runat="server" Text="GỬI YÊU CẦU" CssClass="pill-btn btn order-btn main-btn submit-btn" OnClick="btnSend_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="subcription">
                                    <asp:Literal ID="ltrPageNamtien" runat="server"></asp:Literal>
                                </div>
                                <div class="form-confirm-send">
                                    <h4 class="sec__title center-txt">Lịch sử nạp tiền tệ</h4>
                                    <div class="table-panel-main full-width">
                                        <table class="trans table">
                                            <thead>
                                                <tr>
                                                    <th>Ngày</th>
                                                    <th>Số tiền</th>
                                                    <th>Trạng thái</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="tbl-footer clear">
                                        <div class="subtotal fr">
                                            <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                                        </div>
                                        <div class="pagenavi fl">
                                            <%this.DisplayHtmlStringPaging1();%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Style="display: none;" />
    </main>
    <asp:HiddenField ID="hdfTradeID" runat="server" />


    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function deleteTrade(ID) {
                var r = confirm("Bạn muốn hủy yêu cầu?");
                if (r == true) {
                    $("#<%= hdfTradeID.ClientID %>").val(ID);
                    $("#<%= btnclear.ClientID %>").click();
                } else {
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
