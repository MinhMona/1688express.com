using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLADIPJ.Business;

namespace NHST.Admin
{
    public partial class warehouse_fee_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0)
                        {
                            Response.Redirect("/Admin/Tariff-TQVN.aspx");
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                }
            }
        }
        public void LoadData()
        {
            int ID = Request.QueryString["ID"].ToString().ToInt(0);
            if (ID > 0)
            {
                var f = WarehouseFeeController.GetByID(ID);
                if (f != null)
                {
                    ViewState["FID"] = ID;
                    string wname = "";
                    var w = WarehouseController.GetByID(Convert.ToInt32(f.WarehouseID));
                    if (w != null)
                        wname = w.WareHouseName;
                    ltrWarehouseName.Text = wname;
                    pWeightFrom.Value = f.WeightFrom;
                    pWeightTo.Value = f.WeightTo;
                    pAmount.Value = f.Price;
                    ddlShippingType.SelectedValue = f.ShippingType.ToString();
                    chkIshidden.Checked = Convert.ToBoolean(f.IsHidden);
                    chkIsHelpMoving.Checked = Convert.ToBoolean(f.IsHelpMoving);
                }
                else
                {
                    Response.Redirect("/Admin/Tariff-TQVN.aspx");
                }
            }
            else
                Response.Redirect("/Admin/Tariff-TQVN.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["FID"].ToString().ToInt(0);
            var f = WarehouseFeeController.GetByID(ID);
            string BackLink = "/Admin/warehouse-fee.aspx";
            if (f != null)
            {
                string Username = Session["userLoginSystem"].ToString();

                int ReceivePlace = Convert.ToInt32(f.WarehouseID);
                double WeightFrom = Convert.ToDouble(pWeightFrom.Value);
                double WeightTo = Convert.ToDouble(pWeightTo.Value);
                double Amount = Convert.ToDouble(pAmount.Value);
                string kq = WarehouseFeeController.Update1(ID, ReceivePlace, WeightFrom, WeightTo, Amount, ddlShippingType.SelectedValue.ToInt(1), 
                    chkIshidden.Checked, chkIsHelpMoving.Checked, DateTime.Now, Username);
                if (kq.ToInt(0) > 0)
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Chỉnh sửa chi phí thành công", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Không tồn tại chi phí này.", "e", false, Page);
            }

        }
    }
}