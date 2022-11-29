using NHST.Models;
using NHST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using Supremes;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NHST.Bussiness;
using MB.Extensions;

namespace NHST.manager
{
    public partial class SmallPackage_Detail : System.Web.UI.Page
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
                    if (ac != null)
                    {
                        //if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8)
                        //    Response.Redirect("/trang-chu");
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }
        public void LoadData()
        {
            var bp = BigPackageController.GetAll("");
            if (bp.Count > 0)
            {
                ddlPrefix.Items.Clear();
                ddlPrefix.Items.Insert(0, "Chọn bao hàng");
                foreach (var item in bp)
                {
                    ListItem listitem = new ListItem(item.PackageCode, item.ID.ToString());
                    ddlPrefix.Items.Add(listitem);
                }

                ddlPrefix.DataBind();
            }
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            if (ac != null)
            {
                int roleID = ac.RoleID.ToString().ToInt();
                int i = Request.QueryString["ID"].ToInt(0);
                if (i > 0)
                {
                    ViewState["ID"] = i;
                    var p = SmallPackageController.GetByID(i);
                    if (p != null)
                    {
                        int status = p.Status.ToString().ToInt();
                        if (roleID == 0)
                        {
                            txtOrderTransactionCode.Enabled = true;
                            txtProductType.Enabled = true;
                            pShip.Enabled = true;
                            pWeight.Enabled = true;
                            pVolume.Enabled = true;
                        }
                        txtOrderTransactionCode.Text = p.OrderTransactionCode;
                        txtProductType.Text = p.ProductType;
                        pShip.Value = p.FeeShip;
                        pWeight.Value = p.Weight;
                        pVolume.Value = p.Volume;
                        ddlStatus.SelectedValue = p.Status.ToString();
                        ddlPrefix.SelectedValue = p.BigPackageID.ToString();
                        if (roleID != 0 && roleID != 4 && roleID != 5)
                            btncreateuser.Enabled = false;
                    }
                    else
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
                else
                {
                    Response.Redirect("/trang-chu");
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username_current = Session["userLoginSystem"].ToString();
            DateTime currendDate = DateTime.Now;
            int id = ViewState["ID"].ToString().ToInt(0);
            var s = SmallPackageController.GetByID(id);
            if (s != null)
            {
                string current_ordertransactioncode = s.OrderTransactionCode;
                string current_producttype = s.ProductType;
                double current_ship = s.FeeShip.ToString().ToFloat();
                double current_weight = s.Weight.ToString().ToFloat();
                double current_volume = s.Volume.ToString().ToFloat();
                int current_status = s.Status.ToString().ToInt();
                int current_BigpackageID = s.BigPackageID.ToString().ToInt(0);

                string new_ordertransactionCode = txtOrderTransactionCode.Text.Trim();
                string new_producttype = txtProductType.Text.Trim();
                double new_ship = pShip.Value.ToString().ToFloat(0);
                double new_weight = pWeight.Value.ToString().ToFloat(0);
                double new_volume = pVolume.Value.ToString().ToFloat(0);
                int new_status = ddlStatus.SelectedValue.ToString().ToInt(1);
                int new_BigpackageID = ddlPrefix.SelectedValue.ToString().ToInt(0);


                string kq = SmallPackageController.Update(id, new_BigpackageID, new_ordertransactionCode, new_producttype, new_ship,
                   new_weight, new_volume, new_status, DateTime.Now, username_current);

                var allsmall = SmallPackageController.GetBuyBigPackageID(new_BigpackageID, "");
                if (allsmall.Count > 0)
                {
                    double totalweight = 0;
                    foreach (var item in allsmall)
                    {
                        totalweight += Convert.ToDouble(item.Weight);
                    }
                    BigPackageController.UpdateWeight(new_BigpackageID, totalweight);
                }

                if (current_ordertransactioncode != new_ordertransactionCode)
                {
                    BigPackageHistoryController.Insert(id, "OrderTransactionCode", current_ordertransactioncode, new_ordertransactionCode, 2, currendDate, username_current);
                }
                if (current_producttype != new_producttype)
                {
                    BigPackageHistoryController.Insert(id, "ProductType", current_producttype, new_producttype, 2, currendDate, username_current);
                }
                if (current_ship != new_ship)
                {
                    BigPackageHistoryController.Insert(id, "FeeShip", current_ship.ToString(), new_ship.ToString(), 2, currendDate, username_current);
                }
                if (current_weight != new_weight)
                {
                    BigPackageHistoryController.Insert(id, "Weight", current_weight.ToString(), new_weight.ToString(), 2, currendDate, username_current);
                }
                if (current_volume != new_volume)
                {
                    BigPackageHistoryController.Insert(id, "Volume", current_volume.ToString(), new_volume.ToString(), 2, currendDate, username_current);
                }
                if (current_status != new_status)
                {
                    BigPackageHistoryController.Insert(id, "Status", current_status.ToString(), new_status.ToString(), 2, currendDate, username_current);
                }
                if (current_BigpackageID != new_BigpackageID)
                {
                    BigPackageHistoryController.Insert(id, "BigpackageID", current_BigpackageID.ToString(), new_BigpackageID.ToString(), 2, currendDate, username_current);
                }

                if (kq.ToInt(0) > 0)
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thành công.", "s", true, Page);
                }
                else
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật thất bại.", "e", true, Page);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/manager/Order-Transaction-Code.aspx");
        }
    }
}