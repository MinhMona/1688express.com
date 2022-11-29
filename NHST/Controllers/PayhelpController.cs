using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;

namespace NHST.Controllers
{
    public class PayhelpController
    {
        #region CRUD
        public static string Insert(int UID, string Username, string Note, string TotalPrice, string TotalPriceVND, string Currency,
            string CurrencyGiagoc, string TotalPriceVNDGiagoc, int Status, string Phone, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayHelp o = new tbl_PayHelp();
                o.UID = UID;
                o.Username = Username;
                o.Note = Note;
                o.TotalPrice = TotalPrice;
                o.TotalPriceVND = TotalPriceVND;
                o.Currency = Currency;
                o.CurrencyGiagoc = CurrencyGiagoc;
                o.TotalPriceVNDGiagoc = TotalPriceVNDGiagoc;
                o.Status = Status;
                o.Phone = Phone;
                o.IsNotComplete = false;
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_PayHelp.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Note, string TotalPrice, string TotalPriceVND,
            int Status, string Phone, bool IsNotComplete, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Note = Note;
                    o.TotalPrice = TotalPrice;
                    o.TotalPriceVND = TotalPriceVND;
                    o.Status = Status;
                    o.Phone = Phone;
                    o.IsNotComplete = IsNotComplete;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateNew(int ID, string Note, string TotalPrice, string TotalPriceVND,
           string currency, int Status, string staffNote, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Note = Note;
                    o.TotalPrice = TotalPrice;
                    o.TotalPriceVND = TotalPriceVND;
                    o.Status = Status;
                    o.StaffNote = staffNote;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Status = Status;
                    o.ModifiedDate = ModifiedDate;
                    o.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_PayHelp GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static tbl_PayHelp GetByIDAndUID(int ID, int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayHelp.Where(od => od.ID == ID && od.UID == UID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static List<tbl_PayHelp> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> ps = new List<tbl_PayHelp>();
                ps = dbe.tbl_PayHelp.Where(p => p.UID == UID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_PayHelp> GetAllByFromStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status >= status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        public static List<tbl_PayHelp> GetAllByStatusFromdateToDate(int status, DateTime fromdate, DateTime todate)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayHelp> os = new List<tbl_PayHelp>();
                os = dbe.tbl_PayHelp.Where(od => od.Status == status && od.CreatedDate >= fromdate && od.CreatedDate < todate).OrderByDescending(od => od.ID).ToList();
                return os;
            }
        }
        public static List<payhelpCustom> GetAllByUIDWithSearchTextStatusFromDateToDate_SqlHelper1(int UID, string searchText, int searchType, int status, string fd, string td)
        {
            var list = new List<payhelpCustom>();
            var sql = @"SELECT * FROM dbo.tbl_Payhelp ";
            sql += " where UID = " + UID + " ";

            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchType == 1)
                {
                    sql += " AND ID = " + searchText;
                }
                else
                {
                    sql += " AND TotalPrice = '" + searchText +"'";
                }
            }

            if (status > 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By Status, CreatedDate desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new payhelpCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.DateSend = Convert.ToDateTime(reader["CreatedDate"]);

                if (reader["TotalPrice"] != DBNull.Value)
                {
                    if (reader["TotalPrice"].ToString().ToFloat(0) > 0)
                        entity.TotalPriceCYN = Convert.ToDouble(reader["TotalPrice"]);
                }

                if (reader["TotalPriceVND"] != DBNull.Value)
                {
                    if (reader["TotalPriceVND"].ToString().ToFloat(0) > 0)
                        entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);
                }

                if (reader["Note"] != DBNull.Value)
                {
                    entity.Note = reader["Note"].ToString();
                }

                if (reader["StaffNote"] != DBNull.Value)
                {
                    entity.StaffNote = reader["StaffNote"].ToString();
                }

                int statusInt = 1;

                if (reader["Status"] != DBNull.Value)
                {
                    statusInt = reader["Status"].ToString().ToInt(1);
                }
                entity.Status = statusInt;
                entity.strStatus = PJUtils.ReturnStatusPayHelp(statusInt);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<payhelpCustom> GetAllWithSearchTextStatusFromPriceToPriceFromDateToDate_SqlHelper1(string searchText, int searchType,
            int status, string fromPrice, string toPrice, string fd, string td)
        {
            var list = new List<payhelpCustom>();
            var sql = @"SELECT * FROM dbo.tbl_Payhelp ";
            sql += " where 1 = 1 ";

            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchType == 1)
                {
                    sql += " AND ID = " + searchText;
                }
                else
                {
                    sql += " AND TotalPrice = " + searchText;
                }
            }

            if (status > 0)
                sql += " AND Status = " + status;

            if (!string.IsNullOrEmpty(fromPrice))
            {
                if (fromPrice.ToFloat(0) > 0)
                    sql += " AND TotalPrice >= " + fromPrice + "";
            }
            if (!string.IsNullOrEmpty(toPrice))
            {
                if (toPrice.ToFloat(0) > 0)
                    sql += " AND TotalPrice <= " + fromPrice + "";
            }

            if (!string.IsNullOrEmpty(fd))
            {
                var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
            }
            if (!string.IsNullOrEmpty(td))
            {
                var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                sql += " AND CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
            }
            sql += " Order By Status, CreatedDate desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new payhelpCustom();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["UID"] != DBNull.Value)
                    entity.UID = reader["UID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.DateSend = Convert.ToDateTime(reader["CreatedDate"]);

                if (reader["TotalPrice"] != DBNull.Value)
                {
                    if (reader["TotalPrice"].ToString().ToFloat(0) > 0)
                        entity.TotalPriceCYN = Convert.ToDouble(reader["TotalPrice"]);
                }

                if (reader["TotalPriceVND"] != DBNull.Value)
                {
                    if (reader["TotalPriceVND"].ToString().ToFloat(0) > 0)
                        entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);
                }

                if (reader["Note"] != DBNull.Value)
                {
                    entity.Note = reader["Note"].ToString();
                }

                int statusInt = 1;

                if (reader["Status"] != DBNull.Value)
                {
                    statusInt = reader["Status"].ToString().ToInt(1);
                }
                entity.Status = statusInt;
                entity.strStatus = PJUtils.ReturnStatusPayHelp(statusInt);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public class payhelpCustom
        {
            public int ID { get; set; }
            public int UID { get; set; }
            public string Username { get; set; }
            public DateTime DateSend { get; set; }
            public double TotalPriceCYN { get; set; }
            public double TotalPriceVND { get; set; }
            public string Note { get; set; }
            public string StaffNote { get; set; }
            public int Status { get; set; }
            public string strStatus { get; set; }
        }
        #endregion
    }
}