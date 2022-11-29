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
    public partial class AddTafiffBuyPro : System.Web.UI.Page
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
                            Response.Redirect("/Admin/Tariff-Buypro.aspx");
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            double pAmountFrom = Convert.ToDouble(pPriceFrom.Value);
            double pAmountTo = Convert.ToDouble(pPriceTo.Value);
            double fee = Convert.ToDouble(pFeeservice.Value);

            var check = FeeBuyProController.GetByPriceFromAndPriceTo(pAmountFrom, pAmountTo);
            if (check != null)
            {
                PJUtils.ShowMessageBoxSwAlert("Chi phí đã tồn tại, vui lòng xem lại", "e", false, Page);
            }
            else
            {
                string kq = FeeBuyProController.Insert(pAmountFrom, pAmountTo, fee, Convert.ToDouble(rFeeMoney.Value), DateTime.Now, Username);
                if (kq.ToInt(0) > 0)
                    PJUtils.ShowMessageBoxSwAlert("Tạo mới chi phí thành công", "s", true, Page);
            }
        }
    }
}