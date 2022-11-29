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
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.Admin
{
    public partial class warehouse_fee : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                    LoadData();
                }
            }
        }
        public void LoadData()
        {            
            var dt = WarehouseController.GetAllWithIsHidden(false);
            ddlWarehouseID.Items.Clear();
            ddlWarehouseID.Items.Insert(0, "Tất cả");
            if (dt.Count>0)
            {
                foreach (var item in dt)
                {
                    ListItem listitem = new ListItem(item.WareHouseName, item.ID.ToString());
                    ddlWarehouseID.Items.Add(listitem);
                }
            }
            ddlWarehouseID.DataBind();
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<tbl_WarehouseFee> la = new List<tbl_WarehouseFee>();
            int warehouseID = ddlWarehouseID.SelectedValue.ToInt(0);
            if (warehouseID == 0)
            {
                la = WarehouseFeeController.GetAll();
            }
            else
            {
                la = WarehouseFeeController.GetAllWithWarehouseID(warehouseID);
            }
            if (la.Count > 0)
            {
                gr.DataSource = la;
            }

        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion

        #region button event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion
    }
}