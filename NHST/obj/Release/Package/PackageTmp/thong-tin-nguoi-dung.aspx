<%@ Page Title="" Language="C#" MasterPageFile="~/1688MasterLogined.Master" AutoEventWireup="true" CodeBehind="thong-tin-nguoi-dung.aspx.cs" Inherits="NHST.thong_tin_nguoi_dung1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        p {
            text-align: initial;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Thông tin tài khoản</h4>
                <div class="primary-form">
                    <div class="form-row">
                        <div class="lb">
                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên đăng nhập</div>
                        </div>
                        <div class="form-row-right">
                            <strong>
                                <asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Họ của bạn</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate full-width" placeholder="Họ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Tên của bạn</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate full-width" placeholder="Tên"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                    ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Số điện thoại</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control full-width" placeholder="Số điện thoại" Enabled="false"
                                onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                MaxLength="11"></asp:TextBox>
                            <%--<div class="form-group-left">
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2 full-width"></asp:DropDownList>                                    
                                </div>
                                <div class="form-group-right">
                                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control full-width" placeholder="Số điện thoại" Enabled="false"
                                        onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                        MaxLength="11"></asp:TextBox>
                                </div>--%>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red"
                                    Display="Dynamic" ErrorMessage="Không được để trống số điện thoại."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Địa chỉ</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate full-width" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Email</div>
                        </div>
                        <div class="form-row-right">
                            <strong>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label></strong>
                            <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>--%>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Ngày sinh:</div>
                        </div>
                        <div class="form-row-right">
                            <div class="ip">
                                <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                    DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                    </DateInput>
                                </telerik:RadDateTimePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                    Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Giới tính</div>
                        </div>
                        <div class="form-row-right">
                            <div class="ip">
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Mật khẩu</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate full-width" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-row-left">
                            <div class="lb">Xác nhận mật khẩu</div>
                        </div>
                        <div class="form-row-right">
                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate full-width" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </span>
                        </div>
                    </div>
                    <div class="form-row btn-row">
                        <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
                            OnClick="btncreateuser_Click" />
                    </div>
                </div>
            </div>
        </section>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container container-800">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Thông tin tài khoản</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
    <script type="text/javascript">
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
    </script>
    <style>
        .form-row-right {
            line-height: 40px;
        }
    </style>
</asp:Content>
