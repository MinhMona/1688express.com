using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.Admin
{
    public partial class report_income_userdathang : System.Web.UI.Page
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
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }

                }
                LoadData();
            }
        }
        public void LoadData()
        {

            var listStaff = AccountController.GetAllByRoleID(3);
            ddlUsername.Items.Clear();
            ddlUsername.Items.Insert(0, "Chọn nhân viên");
            if (listStaff.Count > 0)
            {
                ddlUsername.DataSource = listStaff;
                ddlUsername.DataBind();
            }
            int UID = Request.QueryString["uid"].ToInt(0);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];

            ddlUsername.SelectedValue = UID.ToString();
            if (!string.IsNullOrEmpty(fd))
                rdatefrom.SelectedDate = Convert.ToDateTime(fd);
            if (!string.IsNullOrEmpty(td))
                rdateto.SelectedDate = Convert.ToDateTime(td);


            var mainorders = MainOrderController.GetAllByDathangIDWithmoreStatus_SqlHelper1(UID, 5, fd, td);
            double totalOrder = 0;
            double totalOrderPriceVND = 0;
            double totalOrderPriceCYN = 0;

            double totalFeeBuyProVND = 0;
            double totalFeeBuyProCYN = 0;

            double totalFeeOptionVND = 0;
            double totalFeeOptionCYN = 0;

            double totalFeeWeight = 0;
            double totalFeeWeightCYN = 0;

            if (mainorders.Count > 0)
            {
                totalOrder = mainorders.Count;
                foreach (var m in mainorders)
                {
                    double currency = m.Currency;

                    double PriceVND = m.PriceVND;
                    double PriceCYN = PriceVND / currency;
                    totalOrderPriceVND += PriceVND;
                    totalOrderPriceCYN += PriceCYN;

                    double FeeBuyProVND = m.FeeBuyPro;
                    double FeeBuyProCYN = FeeBuyProVND / currency;

                    double FeeOptionVND = m.AdditionFeeForSensorProduct;
                    double FeeOptionCYN = FeeOptionVND / currency;

                    

                    totalFeeBuyProVND += FeeBuyProVND;
                    totalFeeBuyProCYN += FeeBuyProCYN;

                    totalFeeOptionVND += FeeOptionVND;
                    totalFeeOptionCYN += FeeOptionCYN;

                    if (m.Status == 10)
                    {
                        var ex = ExportRequestTurnController.GetByMainOrderID(m.ID);
                        if(ex.Count>0)
                        {
                            foreach (var item in ex)
                            {
                                totalFeeWeight += Convert.ToDouble(item.TotalPriceVND);
                                totalFeeWeightCYN += Convert.ToDouble(item.TotalPriceCYN);
                            }
                        }
                        totalFeeWeight += m.FeeWeight;
                    }
                }
            }

            lblOrderCount.Text = string.Format("{0:N0}", totalOrder);
            lblOrderPrice.Text = string.Format("{0:N0}", totalOrderPriceVND) + " VNĐ (" + "¥" + string.Format("{0:N0}", totalOrderPriceCYN) + ")";
            lblOrderFeeBuyProduct.Text = string.Format("{0:N0}", totalFeeBuyProVND) + " VNĐ (" + "¥" + string.Format("{0:N0}", totalFeeBuyProCYN) + ")";
            lblFeeOption.Text = string.Format("{0:N0}", totalFeeOptionVND) + " VNĐ (" + "¥" + string.Format("{0:N0}", totalFeeOptionCYN) + ")";
            lblFeeWeight.Text = string.Format("{0:N0}", totalFeeWeight) + " VNĐ (" + "¥" + string.Format("{0:N0}", totalFeeWeightCYN) + ")";
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string uid = ddlUsername.SelectedValue;
            string fd = rdatefrom.SelectedDate.ToString();
            string td = rdateto.SelectedDate.ToString();
            Response.Redirect("/admin/report-income-userdathang.aspx?uid=" + uid + "&fd=" + fd + "&td=" + td + "");
        }
    }
}