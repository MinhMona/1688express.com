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
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;
using System.Web.Script.Serialization;

namespace NHST
{
    public partial class tao_don_hang_van_chuyen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] != null)
                {
                    loaddata();
                    LoadReceiPlace();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }
        }
        public void LoadReceiPlace()
        {
            var dt = WarehouseController.GetAllWithIsHidden(false);
            ddlReceivePlace.Items.Clear();
            if (dt.Count > 0)
            {
                foreach (var item in dt)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlReceivePlace.Items.Add(listitem);
                }
            }
            ddlReceivePlace.DataBind();
        }

        public void loaddata()
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                int id = obj_user.ID;
                ViewState["UID"] = id;
                lblUsername.Text = username;
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            double currency = 0;
            var config = ConfigurationController.GetByTop1();
            if (config != null)
            {
                currency = Convert.ToDouble(config.Currency);
            }
            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            if (obj_user != null)
            {
                string listPackage = hdfProductList.Value;
                if (!string.IsNullOrEmpty(listPackage))
                {
                    double totalWeight = 0;
                    string[] list = listPackage.Split('|');
                    if (list.Length - 1 > 0)
                    {
                        for (int i = 0; i < list.Length - 1; i++)
                        {
                            string items = list[i];
                            string[] item = items.Split(']');
                            double weight = Convert.ToDouble(item[1].ToString());
                            totalWeight += weight;
                        }
                    }
                    string kq = TransportationOrderController.Insert(obj_user.ID, username, 1,
                        ddlReceivePlace.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1), 1, totalWeight,
                       currency, 0, txtNote.Text,
                        currentDate, username);
                    if (kq.ToInt(0) > 0)
                    {
                        if (list.Length - 1 > 0)
                        {
                            for (int i = 0; i < list.Length - 1; i++)
                            {
                                var items = list[i];
                                string[] item = items.Split(']');
                                string orderCode = item[0].ToString();
                                double weight = Convert.ToDouble(item[1].ToString());
                                TransportationOrderDetailController.Insert(kq.ToInt(0), orderCode, weight, currentDate, username);
                            }
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                }
                else
                {
                    string kq = TransportationOrderController.Insert(obj_user.ID, username, 1,
                       ddlReceivePlace.SelectedValue.ToInt(1), ddlShippingType.SelectedValue.ToInt(1), 1, 0,
                       currency, 0, txtNote.Text,
                       currentDate, username);
                    PJUtils.ShowMessageBoxSwAlert("Tạo đơn hàng thành công", "s", true, Page);
                }
            }
        }
    }
}