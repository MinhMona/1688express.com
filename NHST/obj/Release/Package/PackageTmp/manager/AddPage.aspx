﻿<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="NHST.manager.AddPage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Tạo mới bài viết</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Tiêu đề <span class="require">(*)</span>
                                            <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtTitle"
                                                ValidationGroup="n" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Thuộc loại
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPageType" runat="server" DataTextField="PageTypeName" CssClass="form-control"
                                                DataValueField="ID">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-row marbot1">
                                            Ảnh đại diện
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                                Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                                MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                            </telerik:RadAsyncUpload>
                                            <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                            <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                        </div>

                                        <div class="form-row marbot1">
                                            Mô tả ngắn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtSummary" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Chi tiết
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                                Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                                DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                                <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                            </telerik:RadEditor>
                                        </div>

                                        <div class="form-row marbot1">
                                            Ẩn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:CheckBox runat="server" ID="isHidden" Checked="false" />
                                        </div>

                                        <div class="form-row marbot1">
                                            OG Title
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtOGTitle" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            OG Description
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtOGDescription" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            OG Image
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="rOGImage" ChunkSize="0"
                                                Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                                MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                            </telerik:RadAsyncUpload>
                                            <asp:Image runat="server" ID="Image1" Width="200" />
                                            <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                        </div>

                                        <div class="form-row marbot1">
                                            Meta Title
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtMetaTitle" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Meta Description
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtMetaDescription" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Meta Keyword (cách nhau bằng dấu: ,)
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtMetakeyword" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Tạo mới" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
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
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
        <script>
            function DelRow(that, link) {

                $(that).parent().parent().remove();
                var myHidden = $("#<%= listImg.ClientID %>");
                var tempF = myHidden.value;
                myHidden.value = tempF.replace(link, '');
            }
            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page
                function OnFileSelected(fileSelected) {
                    if (textBox) {
                        {
                            var myHidden = document.getElementById('<%= listImg.ClientID %>');
                            var tempF = myHidden.value;

                            tempF = tempF + '#' + fileSelected;
                            myHidden.value = tempF;

                            $('.hidImage').append('<tr><td><img height="100px" src="' + fileSelected + '"/></td><td style="text-align:center"><a class="btn btn-success" onclick="DelRow(this,\'' + fileSelected + '\')">Xóa</a></td></li>');
                            //alert(fileSelected);
                            textBox.set_value(fileSelected);
                        }
                    }
                }

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
