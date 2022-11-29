using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using WebUI.Business;
using System.Data;
using MB.Extensions;

namespace NHST.Controllers
{
    public class ExportRequestTurnController
    {
        #region CRUD
        public static string Insert(int MainOrderID, DateTime DateExport, double TotalPriceVND, double TotalPriceCYN,
            double TotalWeight, string Note, int ShippingTypeInVNID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ExportRequestTurn p = new tbl_ExportRequestTurn();
                p.MainOrderID = MainOrderID;
                p.DateExport = DateExport;
                p.TotalPriceVND = TotalPriceVND;
                p.TotalPriceCYN = TotalPriceCYN;
                p.TotalWeight = TotalWeight;
                p.Note = Note;
                p.ShippingTypeInVNID = ShippingTypeInVNID;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ExportRequestTurn.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string InsertWithUID(int UID, string Username, int MainOrderID, DateTime DateExport, double TotalPriceVND, double TotalPriceCYN,
           double TotalWeight, string Note, int ShippingTypeInVNID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_ExportRequestTurn p = new tbl_ExportRequestTurn();
                p.UID = UID;
                p.Username = Username;
                p.MainOrderID = MainOrderID;
                p.DateExport = DateExport;
                p.TotalPriceVND = TotalPriceVND;
                p.TotalPriceCYN = TotalPriceCYN;
                p.TotalWeight = TotalWeight;
                p.Note = Note;
                p.ShippingTypeInVNID = ShippingTypeInVNID;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_ExportRequestTurn.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string UpdateStaffNote(int ID, string StaffNote)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_ExportRequestTurn.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.StaffNote = StaffNote;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_ExportRequestTurn GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pa = dbe.tbl_ExportRequestTurn.Where(p => p.ID == ID).FirstOrDefault();
                if (pa != null)
                    return pa;
                else
                    return null;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == MainOrderID).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByCreatedBy(string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.CreatedBy == CreatedBy).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByCreatedByAndVCH(string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.CreatedBy == CreatedBy && p.MainOrderID == 0).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByVCH()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == 0).ToList();
                return pages;
            }
        }
        public static List<tbl_ExportRequestTurn> GetByFilter_SqlHelper(string fd, string td)
        {
            var list = new List<tbl_ExportRequestTurn>();
            var sql = @"SELECT * FROM dbo.tbl_ExportRequestTurn where MainOrderID = 0 ";
            
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
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new tbl_ExportRequestTurn();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = Convert.ToDouble(reader["TotalPriceVND"]);
                if (reader["TotalPriceCYN"] != DBNull.Value)
                    entity.TotalPriceCYN = Convert.ToDouble(reader["TotalPriceCYN"]);
                if (reader["TotalWeight"] != DBNull.Value)
                    entity.TotalWeight = Convert.ToDouble(reader["TotalWeight"]);
                if (reader["StaffNote"] != DBNull.Value)
                    entity.StaffNote = reader["StaffNote"].ToString();
                if (reader["ShippingTypeInVNID"] != DBNull.Value)
                    entity.ShippingTypeInVNID = reader["ShippingTypeInVNID"].ToString().ToInt();
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }


        public static List<tbl_ExportRequestTurn> GetByMainOrderIDAndFTTD(int MainOrderID, DateTime FD, DateTime TD)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_ExportRequestTurn> pages = new List<tbl_ExportRequestTurn>();
                pages = dbe.tbl_ExportRequestTurn.Where(p => p.MainOrderID == MainOrderID && p.DateExport >= FD && p.DateExport < TD).ToList();
                return pages;
            }
        }
        public static List<ListDateExport> GetAllExportByMainOrderID(int MainOrderID)
        {
            var list = new List<ListDateExport>();
            var sql = @"SELECT CAST(DateExport AS DATE) AS DateExport, COUNT(*) as TotalRows ";
            sql += " FROM tbl_ExportRequestTurn ";
            sql += " WHERE MainOrderID = " + MainOrderID + " ";
            sql += " GROUP BY CAST(DateExport AS DATE)";
            sql += " ORDER BY 1";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new ListDateExport();
                if (reader["DateExport"] != DBNull.Value)
                    entity.DateExport = Convert.ToDateTime(reader["DateExport"]);
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        #endregion
        public class ListDateExport
        {
            public DateTime DateExport { get; set; }
        }
    }
}