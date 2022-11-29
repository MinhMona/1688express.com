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
    public partial class EditInWareHousePrice : System.Web.UI.Page
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
                var f = InWareHousePriceController.GetByID(ID);
                if (f != null)
                {
                    ViewState["FID"] = ID;                    
                    pWeightFrom.Value = f.WeightFrom;
                    pWeightTo.Value = f.WeightTo;
                    rMaxDate.Value = f.MaxDay;
                    pAmount.Value = f.PricePay;
                }
                else
                {
                    Response.Redirect("/Admin/InWareHousePriceList.aspx");
                }
            }
            else
                Response.Redirect("/Admin/InWareHousePriceList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int ID = ViewState["FID"].ToString().ToInt(0);
            var f = InWareHousePriceController.GetByID(ID);
            string BackLink = "/Admin/InWareHousePriceList.aspx";
            if (f != null)
            {
                string Username = Session["userLoginSystem"].ToString();
                
                double WeightFrom = Convert.ToDouble(pWeightFrom.Value);
                double WeightTo = Convert.ToDouble(pWeightTo.Value);
                double MaxDate = Convert.ToDouble(rMaxDate.Value);
                double Amount = Convert.ToDouble(pAmount.Value);

                string kq = InWareHousePriceController.Update(ID, WeightFrom, WeightTo, MaxDate, Amount, DateTime.Now, Username);
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