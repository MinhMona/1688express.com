﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditCommitment.aspx.cs" Inherits="NHST.Admin.EditCommitment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Chỉnh sửa</h3>
                    </div>
                    <asp:Panel runat="server" ID="TTCB">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Tên cam kết<span class="require">(*)</span>
                                        <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtCommitmentName"
                                            ValidationGroup="n" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtCommitmentName" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Sắp xếp
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pCommitmentIndex" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="10%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">
                                        Chi tiết
                                    </label>
                                    <div class="col-sm-10">
                                        <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                            Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                            DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                            <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                        </telerik:RadEditor>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="panel panel-white">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                    <a href="/Admin/CommitmentList.aspx" class="btn btn-success">Trở về</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlDVT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlDVT" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblUnit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery-1.11.0.min.js"></script>
        <script>

            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Admin/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
