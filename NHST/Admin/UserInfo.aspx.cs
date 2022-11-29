using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;
using NHST.Controllers;
using NHST.Bussiness;
using NHST.Models;

namespace NHST.Admin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID == 1)
                        Response.Redirect("/trang-chu");
                }
                loadPrefix();
                LoadSaler();
                loaddata();
            }
        }
        public void loadPrefix()
        {
            //var listpre = PJUtils.loadprefix();
            //ddlPrefix.Items.Clear();
            //foreach (var item in listpre)
            //{
            //    ListItem listitem = new ListItem(item.dial_code, item.dial_code);
            //    ddlPrefix.Items.Add(listitem);
            //}
            //ddlPrefix.DataBind();
            var Levels = UserLevelController.GetAll("");
            if (Levels.Count > 0)
            {
                ddlLevelID.DataSource = Levels;
                ddlLevelID.DataBind();
            }
        }
        public void LoadSaler()
        {
            var salers = AccountController.GetAllByRoleID(6);
            ddlSale.Items.Clear();
            ddlSale.Items.Insert(0, "Chọn nhân viên kinh doanh");
            if (salers.Count > 0)
            {
                ddlSale.DataSource = salers;
                ddlSale.DataBind();
            }
            var dathangs = AccountController.GetAllByRoleID(3);
            ddlDathang.Items.Clear();
            ddlDathang.Items.Insert(0, "Chọn nhân viên đặt hàng");
            if (dathangs.Count > 0)
            {
                ddlDathang.DataSource = dathangs;
                ddlDathang.DataBind();
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                if (ac.RoleID == 0)
                {
                    pnAdmin.Visible = true;
                }
                else
                {
                    if (ac.ID != id)
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                ViewState["UID"] = id;
                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    lblTradeHistory.Text = "<a href=\"/admin/trade-history.aspx?ID=" + id + "\" target=\"_blank\">Lịch sử giao dịch</a>";
                    string pass = PJUtils.Decrypt("userpass", a.Password);
                    if(ac.RoleID == 0)
                        lblUsername.Text = a.Username + " - Password: " + pass;
                    else
                        lblUsername.Text = a.Username;
                    txtEmail.Text = a.Email;
                    var ai = AccountInfoController.GetByUserID(id);
                    if (ai != null)
                    {
                        txtFirstName.Text = ai.FirstName;
                        txtLastName.Text = ai.LastName;
                        //ddlPrefix.SelectedValue = ai.MobilePhonePrefix;
                        txtPhone.Text = ai.Phone;
                        txtAddress.Text = ai.Address;
                        //txtEmail.Text = ai.Email;
                        if (ai.BirthDay != null)
                            rBirthday.SelectedDate = ai.BirthDay;
                        if (ai.Gender != null)
                            ddlGender.SelectedValue = ai.Gender.ToString();
                    }
                    ddlRole.SelectedValue = a.RoleID.ToString();
                    ddlStatus.SelectedValue = a.Status.ToString();
                    ddlLevelID.SelectedValue = a.LevelID.ToString();
                    ddlVipLevel.SelectedValue = a.VIPLevel.ToString();
                    ddlSale.SelectedValue = a.SaleID.ToString();
                    ddlDathang.SelectedValue = a.DathangID.ToString();
                    bool isAgent = false;
                    if(a.IsAgent!=null)
                    {
                        isAgent = Convert.ToBoolean(a.IsAgent);
                    }
                    chkIsAgent.Checked = isAgent;

                    bool isLocal = false;
                    if (a.IsLocal != null)
                    {
                        isLocal = Convert.ToBoolean(a.IsLocal);
                    }
                    chkIsLocal.Checked = isLocal;

                    bool isVip = false;
                    if (a.IsVip != null)
                    {
                        isVip = Convert.ToBoolean(a.IsVip);
                    }
                    chkIsVip.Checked = isVip;
                }
                else
                {
                    Response.Redirect("/Admin/Home.aspx");
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int UID = ViewState["UID"].ToString().ToInt(0);
            string pass = txtpass.Text.Trim();
            int Status = ddlStatus.SelectedValue.ToString().ToInt();
            int RoleID = ddlRole.SelectedValue.ToString().ToInt();
            int LevelID = ddlLevelID.SelectedValue.ToString().ToInt();
            int SaleID = ddlSale.SelectedValue.ToString().ToInt(0);
            int VIPLevel = ddlVipLevel.SelectedValue.ToString().ToInt(1);
            int DathangID = ddlDathang.SelectedValue.ToString().ToInt(0);
            DateTime currentDate = DateTime.Now;
            string username_current = Session["userLoginSystem"].ToString();

            var acc = AccountController.GetByEmail(txtEmail.Text.Trim().ToLower());
            if (acc != null)
            {
                if (acc.ID == UID)
                {
                    if (!string.IsNullOrEmpty(pass))
                    {
                        string confirmpass = txtconfirmpass.Text;
                        if (!string.IsNullOrEmpty(confirmpass))
                        {
                            if (confirmpass == pass)
                            {
                                AccountController.updateLevelID(UID, LevelID, currentDate, username_current);                               
                                AccountController.updatestatus(UID, Status, currentDate, username_current);
                                AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                                AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                                AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                                AccountController.updateEmail(UID, txtEmail.Text);
                                AccountController.updateIsAgent(UID, chkIsAgent.Checked);
                                AccountController.updateIsLocal(UID, chkIsLocal.Checked);
                                AccountController.updateIsVip(UID, chkIsVip.Checked);

                                string rp = AccountController.UpdatePassword(UID, pass);
                                string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                                    txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", currentDate,
                                    username_current);
                                if (r == "1" && rp == "1")
                                {
                                    PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                                }
                                else if (r == "1" && rp == "0")
                                {
                                    lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                                    lblConfirmpass.Visible = true;
                                }
                                else
                                {
                                    PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                                }
                            }
                            else
                            {
                                lblConfirmpass.Text = "Xác nhận mật khẩu không trùng với mật khẩu.";
                                lblConfirmpass.Visible = true;
                            }
                        }
                        else
                        {
                            lblConfirmpass.Text = "Không để trống xác nhận mật khẩu";
                            lblConfirmpass.Visible = true;
                        }
                    }
                    else
                    {
                        AccountController.updateLevelID(UID, LevelID, currentDate, username_current);                      
                        AccountController.updatestatus(UID, Status, currentDate, username_current);
                        AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                        AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                        AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                        AccountController.updateEmail(UID, txtEmail.Text);
                        AccountController.updateIsAgent(UID, chkIsAgent.Checked);
                        AccountController.updateIsLocal(UID, chkIsLocal.Checked);
                        AccountController.updateIsVip(UID, chkIsVip.Checked);

                        string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                            txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now,
                            username_current);
                        if (r == "1")
                        {
                            PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                        }
                        else
                        {
                            PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                        }
                    }
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Email bị trùng vui lòng kiểm tra lại", "e", true, Page);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(pass))
                {
                    string confirmpass = txtconfirmpass.Text;
                    if (!string.IsNullOrEmpty(confirmpass))
                    {
                        if (confirmpass == pass)
                        {
                            AccountController.updateLevelID(UID, LevelID, currentDate, username_current);
                            //AccountController.updateVipLevel(UID, VIPLevel, currentDate, username_current);
                            AccountController.updatestatus(UID, Status, currentDate, username_current);
                            AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                            AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                            AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                            AccountController.updateEmail(UID, txtEmail.Text);
                            AccountController.updateIsAgent(UID, chkIsAgent.Checked);
                            AccountController.updateIsLocal(UID, chkIsLocal.Checked);
                            string rp = AccountController.UpdatePassword(UID, pass);

                            string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                                txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", currentDate,
                                username_current);
                            if (r == "1" && rp == "1")
                            {
                                PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                            }
                            else if (r == "1" && rp == "0")
                            {
                                lblConfirmpass.Text = "Mật khẩu mới trùng với mật khẩu cũ.";
                                lblConfirmpass.Visible = true;
                            }
                            else
                            {
                                PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                            }
                        }
                        else
                        {
                            lblConfirmpass.Text = "Xác nhận mật khẩu không trùng với mật khẩu.";
                            lblConfirmpass.Visible = true;
                        }
                    }
                    else
                    {
                        lblConfirmpass.Text = "Không để trống xác nhận mật khẩu";
                        lblConfirmpass.Visible = true;
                    }
                }
                else
                {
                    AccountController.updateLevelID(UID, LevelID, currentDate, username_current);
                    //AccountController.updateVipLevel(UID, VIPLevel, currentDate, username_current);
                    AccountController.updatestatus(UID, Status, currentDate, username_current);
                    AccountController.UpdateRole(UID, RoleID, currentDate, username_current);
                    AccountController.UpdateSaleID(UID, SaleID, currentDate, username_current);
                    AccountController.UpdateDathangID(UID, DathangID, currentDate, username_current);
                    AccountController.updateEmail(UID, txtEmail.Text);
                    AccountController.updateIsAgent(UID, chkIsAgent.Checked);
                    AccountController.updateIsLocal(UID, chkIsLocal.Checked);
                    string r = AccountInfoController.Update(UID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text, txtPhone.Text,
                        txtAddress.Text.Trim(), Convert.ToDateTime(rBirthday.SelectedDate), ddlGender.SelectedValue.ToInt(), "", "", DateTime.Now,
                        username_current);
                    if (r == "1")
                    {
                        PJUtils.ShowMsg("Cập nhật thành công", true, Page);
                    }
                    else
                    {
                        PJUtils.ShowMsg("Có lỗi trong quá trình cập nhật", true, Page);
                    }
                }
            }
        }
    }
}