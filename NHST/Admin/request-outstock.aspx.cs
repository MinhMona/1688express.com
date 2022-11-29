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
using System.Data;
using System.Text;
using System.Web.Services;

namespace NHST.Admin
{
    public partial class request_outstock : System.Web.UI.Page
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

                    if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8 && ac.RoleID != 2)
                    {

                    }
                    else
                    {
                        //ltraddminre.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/Add-smallpackage.aspx\">Thêm mã vận đơn</a>";
                    }
                }
            }
        }

        #region grid event
        //public bool ShouldApplySortFilterOrGroup()
        //{
        //    return gr.MasterTableView.FilterExpression != "" ||
        //        (gr.MasterTableView.GroupByExpressions.Count > 0 || isGrouping) ||
        //        gr.MasterTableView.SortExpressions.Count > 0;
        //}
        //bool isGrouping = false;
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            #region Tối ưu 1
            //int totalRow = RequestOutStockController.GetAll(tSearchName.Text.Trim()).Count;
            //int maximumRows = (ShouldApplySortFilterOrGroup()) ? totalRow : gr.PageSize;
            //gr.VirtualItemCount = totalRow;
            //int Page = (ShouldApplySortFilterOrGroup()) ? 0 : gr.CurrentPageIndex;
            //var lo = RequestOutStockController.GetAllSQLHelper(tSearchName.Text.Trim(), Page, maximumRows);
            //if(lo.Count>0)
            //{
            //    var list = new List<RequestOutStoc>();
            //    foreach (var item in lo)
            //    {
            //        RequestOutStoc r = new RequestOutStoc();
            //        r.ID = item.ID;
            //        string ordercode = "";
            //        string weight = "";
            //        string DateInLasteWareHouse = "";
            //        string DateOutWH = "";
            //        var smallpack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
            //        if (smallpack != null)
            //        {
            //            ordercode = smallpack.OrderTransactionCode;
            //            double w = 0;
            //            if (smallpack.Weight != null)
            //                w = Convert.ToDouble(smallpack.Weight);
            //            weight = Math.Round(w, 2).ToString() + " kg";

            //            if (smallpack.DateInLasteWareHouse != null)
            //                DateInLasteWareHouse = string.Format("{0:dd/MM/yyyy HH:mm}", smallpack.DateInLasteWareHouse);

            //            if (smallpack.DateOutWH != null)
            //                DateOutWH = string.Format("{0:dd/MM/yyyy HH:mm}", smallpack.DateOutWH);
            //        }
            //        r.OrderTransactionCode = ordercode;
            //        r.Weight = weight;
            //        r.MainOrderID = item.MainOrderID.ToString();
            //        r.TransportationID = item.TransportationID.ToString();
            //        r.UserNameCus = item.CreatedBy;
            //        r.DateInVNWarehouse = DateInLasteWareHouse;
            //        r.DateExWarehouse = DateOutWH;
            //        r.Status = Convert.ToInt32(item.Status);
            //        r.Statusstr = PJUtils.requestOutStockStatus(Convert.ToInt32(item.Status));
            //        r.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);

            //        string htvc = "";
            //        string note = "";
            //        if (item.ExportRequestTurnID != null)
            //        {
            //            var exp = ExportRequestTurnController.GetByID(Convert.ToInt32(item.ExportRequestTurnID));
            //            if (exp != null)
            //            {
            //                var shi = ShippingTypeVNController.GetByID(Convert.ToInt32(exp.ShippingTypeInVNID));
            //                if (shi != null)
            //                {
            //                    htvc = shi.ShippingTypeVNName;
            //                }
            //                note = exp.Note;
            //            }
            //        }
            //        r.HTVC = htvc;
            //        r.Note = note;
            //        list.Add(r);
            //    }
            //    gr.AllowCustomPaging = !ShouldApplySortFilterOrGroup();
            //    gr.DataSource = list;
            //}
            #endregion

            #region Tối ưu 2
            var la = RequestOutStockController.GetAllSQLHelper1(tSearchName.Text.Trim());
            if (la.Count > 0)
            {
                gr.DataSource = la;
            }
            #endregion

            #region Chưa tối ưu
            //var la = RequestOutStockController.GetAll(tSearchName.Text.Trim());
            //if (la != null)
            //{
            //    if (la.Count > 0)
            //    {
            //        var list = new List<RequestOutStoc>();
            //        foreach (var item in la)
            //        {
            //            RequestOutStoc r = new RequestOutStoc();
            //            r.ID = item.ID;
            //            string ordercode = "";
            //            string weight = "";
            //            string DateInLasteWareHouse = "";
            //            string DateOutWH = "";
            //            var smallpack = SmallPackageController.GetByID(Convert.ToInt32(item.SmallPackageID));
            //            if (smallpack != null)
            //            {
            //                ordercode = smallpack.OrderTransactionCode;
            //                double w = 0;
            //                if (smallpack.Weight != null)
            //                    w = Convert.ToDouble(smallpack.Weight);
            //                weight = Math.Round(w, 2).ToString() + " kg";

            //                if (smallpack.DateInLasteWareHouse != null)
            //                    DateInLasteWareHouse = string.Format("{0:dd/MM/yyyy HH:mm}", smallpack.DateInLasteWareHouse);

            //                if (smallpack.DateOutWH != null)
            //                    DateOutWH = string.Format("{0:dd/MM/yyyy HH:mm}", smallpack.DateOutWH);
            //            }
            //            r.OrderTransactionCode = ordercode;
            //            r.Weight = weight;
            //            r.MainOrderID = item.MainOrderID.ToString();
            //            r.TransportationID = item.TransportationID.ToString();
            //            r.UserNameCus = item.CreatedBy;
            //            r.DateInVNWarehouse = DateInLasteWareHouse;
            //            r.DateExWarehouse = DateOutWH;
            //            r.Status = Convert.ToInt32(item.Status);
            //            r.Statusstr = PJUtils.requestOutStockStatus(Convert.ToInt32(item.Status));
            //            r.CreatedDate = string.Format("{0:dd/MM/yyyy HH:mm}", item.CreatedDate);

            //            string htvc = "";
            //            string note = "";
            //            if (item.ExportRequestTurnID != null)
            //            {
            //                var exp = ExportRequestTurnController.GetByID(Convert.ToInt32(item.ExportRequestTurnID));
            //                if (exp != null)
            //                {
            //                    var shi = ShippingTypeVNController.GetByID(Convert.ToInt32(exp.ShippingTypeInVNID));
            //                    if (shi != null)
            //                    {
            //                        htvc = shi.ShippingTypeVNName;
            //                    }
            //                    note = exp.Note;
            //                }
            //            }
            //            r.HTVC = htvc;
            //            r.Note = note;
            //            list.Add(r);
            //        }
            //        gr.DataSource = list;
            //    }
            //}
            #endregion

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

        public class RequestOutStoc
        {
            public int ID { get; set; }
            public string OrderTransactionCode { get; set; }
            public string Weight { get; set; }
            public string MainOrderID { get; set; }
            public string TransportationID { get; set; }
            public string UserNameCus { get; set; }
            public string DateInVNWarehouse { get; set; }
            public string DateExWarehouse { get; set; }
            public string Statusstr { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string HTVC { get; set; }
            public string Note { get; set; }
        }

        [WebMethod]
        public static string updateStatus(int ID)
        {
            if (HttpContext.Current.Session["userLoginSystem"] != null)
            {
                string username_current = HttpContext.Current.Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                if (ac != null)
                {
                    if (ac.RoleID == 0 || ac.RoleID == 2)
                    {
                        var re = RequestOutStockController.GetByID(ID);
                        if (re != null)
                        {
                            RequestOutStockController.UpdateStatus(ID, 2, DateTime.Now, username_current);
                            return "1";
                        }
                    }
                }
            }
            return "none";

        }
    }
}