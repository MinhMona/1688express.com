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
using NHST.Controllers;
using NHST.Models;
using Telerik.Web.UI;
using Microsoft.AspNet.SignalR;
using NHST.Hubs;

namespace NHST
{
    public partial class them_khieu_nai1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["userLoginSystem"] = "admin";
                if (Session["userLoginSystem"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
            }

        }
        public void LoadData()
        {
            if (RouteData.Values["id"] != null)
            {
                int MainOrderID = RouteData.Values["id"].ToString().ToInt(0);
                //int ProductID = RouteData.Values["productid"].ToString().ToInt(0);
                string OrderCode = RouteData.Values["ordercodetransaction"].ToString();

                if (MainOrderID > 0)
                {
                    string username = Session["userLoginSystem"].ToString();
                    var u = AccountController.GetByUsername(username);
                    if (u != null)
                    {
                        int UID = u.ID;

                        var mainorder = MainOrderController.GetAllByUIDAndID(UID, MainOrderID);
                        if (mainorder != null)
                        {
                            txtOrderID.Text = MainOrderID.ToString();
                            var small = SmallPackageController.GetByOrderTransactionCode(OrderCode);
                            if (small != null)
                            {
                                if (small.MainOrderID == mainorder.ID)
                                {
                                    txtOrderCode.Text = OrderCode;
                                }
                            }
                            else
                            {
                                Response.Redirect("/danh-sach-don-hang");
                            }
                            //hdfProductID.Value = ProductID.ToString();
                            //var order = OrderController.GetAllByID(ProductID);
                            //if (order != null)
                            //{
                            //    hdfOrderShopCode.Value = order.OrderShopCode;
                            //}
                        }
                    }
                }
            }
            else
                Response.Redirect("/danh-sach-don-hang");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int orderid = txtOrderID.Text.ToInt(0);
            DateTime currentDate = DateTime.Now;
            string username = Session["userLoginSystem"].ToString();
            var u = AccountController.GetByUsername(username);
            if (u != null)
            {
                int UID = u.ID;
                var shops = MainOrderController.GetAllByUIDAndID(UID, orderid);
                if (shops != null)
                {
                    string IMG = "";
                    string KhieuNaiIMG = "/Uploads/KhieuNaiIMG/";
                    if (hinhDaiDien.UploadedFiles.Count > 0)
                    {
                        foreach (UploadedFile f in hinhDaiDien.UploadedFiles)
                        {
                            var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                            try
                            {
                                f.SaveAs(Server.MapPath(o));
                                IMG += o + "|";
                            }
                            catch { }
                        }
                        string kq = ComplainController.Insert(UID, orderid, "0", IMG, txtNote.Text, 1,
                        hdfProductID.Value.ToInt(0), txtOrderCode.Text, hdfOrderShopCode.Value, ddlType.SelectedValue.ToInt(1), DateTime.Now, username);
                        if (kq.ToInt(0) > 0)
                        {
                            OrderCommentController.Insert(UID, "Bạn vừa tạo 1 khiếu nại", true, 1, DateTime.Now, u.ID);
                            var o = MainOrderController.GetAllByUIDAndID(UID, orderid);
                            if (o != null)
                            {
                                var admins = AccountController.GetAllByRoleID(0);
                                if (admins.Count > 0)
                                {
                                    foreach (var admin in admins)
                                    {
                                        NotificationController.Inser(u.ID, u.Username, admin.ID,
                                                                           admin.Username, orderid,
                                                                           "Đã có khiếu nại mới cho đơn hàng #" + orderid + ". CLick vào để xem", 0, 5,
                                                                           currentDate, u.Username);
                                    }
                                }

                                var managers = AccountController.GetAllByRoleID(2);
                                if (managers.Count > 0)
                                {
                                    foreach (var manager in managers)
                                    {
                                        NotificationController.Inser(u.ID, u.Username, manager.ID,
                                                                           manager.Username, orderid,
                                                                           "Đã có khiếu nại mới cho đơn hàng #" + orderid + ". CLick vào để xem", 0, 5,
                                                                           currentDate, u.Username);
                                    }
                                }

                                int nvdathang = Convert.ToInt32(o.DathangID);
                                if (nvdathang > 0)
                                {
                                    var accNVDathang = AccountController.GetByID(nvdathang);
                                    if (accNVDathang != null)
                                    {
                                        NotificationController.Inser(u.ID, u.Username, accNVDathang.ID,
                                                                           accNVDathang.Username, orderid,
                                                                           "Đã có khiếu nại mới cho đơn hàng #" + orderid + ". CLick vào để xem", 0, 5,
                                                                           currentDate, u.Username);
                                    }
                                }
                            }

                            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "signalRNow()", true);
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>signalRNow();</script>", false);

                            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hubContext.Clients.All.addNewMessageToPage("", "");

                            try
                            {
                                string message = "";
                                message += "Chào Qúy khách!<br/><br/>";
                                message += "Bộ phận xử lý khiếu nại đã nhận được thông tin khiếu nại của Qúy khách và sẽ kịp thời liên hệ với shop bán hàng để giải quyết.<br/><br/>";
                                message += "Thời gian quy định để xử lý khiếu nại là từ 2-7 ngày làm việc. Bộ phận xử lý khiếu nại sẽ cập nhật thông tin và kết quả khiếu nại ngay trên hệ thống.<br/>";
                                message += "Rất mong Qúy khách thông cảm!<br/><br/>";
                                message += "Nếu trong quá trình thao tác có bất cứ thắc mắc gì, quý khách vui lòng liên hệ : Hotline : <strong>024.6326.5589</strong> – <strong>091.458.1688</strong><br/>";
                                message += "Cảm ơn quý khách, nhóm hỗ trợ <span style=\"font-weight:bold; color:#0070c0\">1688Express</span>";
                                PJUtils.SendMailGmail("order1688express@gmail.com", "tkvrdkmkrzgbfjss", u.Email,
                                    "Tiếp nhận khiếu nại đơn hàng tại 1688 Express", message, "");
                            }
                            catch
                            {

                            }

                            PJUtils.ShowMessageBoxSwAlert("Tạo khiếu nại thành công", "s", true, Page);
                        }
                    }
                    else
                    {
                        lblError.Text = "Vui lòng upload hình ảnh";
                        lblError.Visible = true;
                    }


                }
                else
                {
                    lblError.Text = "Không tìm thấy đơn hàng";
                    lblError.Visible = true;

                    //PJUtils.ShowMessageBoxSwAlert("Không tìm thấy đơn hàng", "e", true, Page);
                }
            }
        }
    }
}