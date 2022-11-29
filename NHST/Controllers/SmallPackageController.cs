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
    public class SmallPackageController
    {
        #region CRUD
        public static string Insert(int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertWithMainOrderID(int MainOrderID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertWithOrderWebCode(int MainOrderID, int BigPackageID,
            string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, string OrderWebCode, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.OrderWebCode = OrderWebCode;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertWithOrderID(int MainOrderID, int OrderID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.OrderID = OrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertWithMainOrderIDAndOrderShopCodeID(int MainOrderID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, int OrderShopCodeID, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.OrderShopCodeID = OrderShopCodeID;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }

        public static string InsertWithMainOrderIDAndOrderShopCodeIDNew(int MainOrderID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
           int Status, int OrderShopCodeID, string StaffNote, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.StaffNote = StaffNote;
                a.OrderShopCodeID = OrderShopCodeID;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertWithTransportationID(int TransportationOrderID, int BigPackageID,
           string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
           int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = 0;
                a.TransportationOrderID = TransportationOrderID;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.ProductType = ProductType;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.IsHelpMoving = true;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }

        public static string InsertInVNWithMainOrder(int MainOrderID, int BigPackageID,
            string OrderTransactionCode, double FeeShip, double Weight, double Volume, int Status,
            DateTime CreatedDate, string CreatedBy, DateTime DateInLasteWareHouse,
            int OrderShopCodeID, string StaffNote, int KhoVNID, DateTime DateCheckKhoVN)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = new tbl_SmallPackage();
                a.MainOrderID = MainOrderID;
                a.TransportationOrderID = 0;
                a.BigPackageID = BigPackageID;
                a.OrderTransactionCode = OrderTransactionCode;
                a.FeeShip = FeeShip;
                a.Weight = Weight;
                a.Volume = Volume;
                a.Status = Status;
                a.OrderShopCodeID = OrderShopCodeID;
                a.DateInLasteWareHouse = DateInLasteWareHouse;
                a.StaffNote = StaffNote;
                a.KhoVNID = KhoVNID;
                a.DateCheckKhoVN = DateCheckKhoVN;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_SmallPackage.Add(a);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.BigPackageID = BigPackageID;
                    a.OrderTransactionCode = OrderTransactionCode;
                    a.ProductType = ProductType;
                    a.FeeShip = FeeShip;
                    a.Weight = Weight;
                    a.Volume = Volume;
                    a.Status = Status;

                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateNew(int ID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, string StaffNote, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.BigPackageID = BigPackageID;
                    a.OrderTransactionCode = OrderTransactionCode;
                    a.ProductType = ProductType;
                    a.FeeShip = FeeShip;
                    a.Weight = Weight;
                    a.Volume = Volume;
                    a.Status = Status;
                    a.StaffNote = StaffNote;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWebCode(int ID, int BigPackageID, string OrderTransactionCode, string ProductType, double FeeShip, double Weight, double Volume,
            int Status, string OrderWebCode, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.BigPackageID = BigPackageID;
                    a.OrderTransactionCode = OrderTransactionCode;
                    a.ProductType = ProductType;
                    a.FeeShip = FeeShip;
                    a.Weight = Weight;
                    a.Volume = Volume;
                    a.Status = Status;
                    a.OrderWebCode = OrderWebCode;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateWeightStatus(int ID, double Weight, int Status, int BigPackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.BigPackageID = BigPackageID;
                    a.Weight = Weight;
                    a.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateKhoTQID(int ID, int khoTQID, string khoTQUsername, DateTime DateCheckKhoTQ)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.KhoTQID = khoTQID;
                    a.KhoTQUsername = khoTQUsername;
                    a.DateCheckKhoTQ = DateCheckKhoTQ;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateKhoVNID(int ID, int khoVNID, string khoVNUsername, DateTime DateCheckKhoVN)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.KhoVNID = khoVNID;
                    a.KhoVNUsername = khoVNUsername;
                    a.DateCheckKhoVN = DateCheckKhoVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateDateInLasteWareHouse(int ID, DateTime DateInLasteWareHouse)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.DateInLasteWareHouse = DateInLasteWareHouse;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStaffNoteInLasteWareHouse(int ID, string StaffNote)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.StaffNote = StaffNote;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    dbe.tbl_SmallPackage.Remove(a);
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.ModifiedDate = ModifiedDate;
                    a.ModifiedBy = ModifiedBy;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateDateOutWH(int ID, DateTime DateOutWH)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.DateOutWH = DateOutWH;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateBigPackageID(int ID, int BigPackageID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.BigPackageID = BigPackageID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_SmallPackage> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.OrderTransactionCode.Contains(s)).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByMainOrderIDAndCode(int MainOrderID, string TransactionCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.MainOrderID == MainOrderID && p.OrderTransactionCode == TransactionCode).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByOrderWebCode(string OrderWebCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.OrderWebCode == OrderWebCode).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByOrderShopCodeID(int OrderShopCodeID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.OrderShopCodeID == OrderShopCodeID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetBuyBigPackageID(int BigPackageID, string text)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.BigPackageID == BigPackageID && p.OrderTransactionCode.Contains(text)).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.MainOrderID == MainOrderID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByOrderID(int OrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.OrderID == OrderID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetAllWithoutAddtoBigpacage()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.BigPackageID == 0).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static tbl_SmallPackage GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_SmallPackage GetByIDAndMainOrderID(int ID, int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.ID == ID && ad.MainOrderID == MainOrderID).SingleOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static tbl_SmallPackage GetByOrderTransactionCode(string OrderTransactionCode)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.OrderTransactionCode == OrderTransactionCode).SingleOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_SmallPackage> GetPackagesByOrderTransactionCode(string OrderTransactionCode)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> sms = new List<tbl_SmallPackage>();
                sms = dbe.tbl_SmallPackage.Where(ad => ad.OrderTransactionCode == OrderTransactionCode).ToList();
                return sms;
            }
        }
        public static tbl_SmallPackage GetCodeWithdoutadd(string OrderTransactionCode)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_SmallPackage a = dbe.tbl_SmallPackage.Where(ad => ad.OrderTransactionCode == OrderTransactionCode && ad.BigPackageID == 0).SingleOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static int GetCountByBigPackageIDStatus(int BigPackageID, int statusf, int statust)
        {
            var sql = @"SELECT Count(*) as TotalPackages FROM dbo.tbl_SmallPackage Where BigPackageID = " + BigPackageID + " and status >= " + statusf + " and status <= " + statust + "";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int count = 0;
            while (reader.Read())
            {
                if (reader["TotalPackages"] != DBNull.Value)
                    count = reader["TotalPackages"].ToString().ToInt(0);
            }
            reader.Close();
            return count;
        }
        public static List<SmallPackageGet> GetAll_SqlHelper(string s, int status, string fd, string td)
        {
            var list = new List<SmallPackageGet>();
            var sql = @"SELECT * FROM tbl_SmallPackage";
            sql += " where 1=1 ";

            if (!string.IsNullOrEmpty(s))
            {
                sql += " AND OrderTransactionCode like N'%" + s + "%'";
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
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new SmallPackageGet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                int BigPackageID = 0;
                string bPackageCode = "";
                if (reader["BigPackageID"] != DBNull.Value)
                    BigPackageID = Convert.ToInt32(reader["BigPackageID"]);
                if (BigPackageID > 0)
                {
                    var bP = BigPackageController.GetByID(BigPackageID);
                    if (bP != null)
                    {
                        bPackageCode = bP.PackageCode;
                    }
                }
                entity.BigpackageCode = bPackageCode;
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);
                if (reader["ProductType"] != DBNull.Value)
                    entity.ProductType = reader["ProductType"].ToString();
                if (reader["FeeShip"] != DBNull.Value)
                    entity.FeeShip = Convert.ToDouble(reader["FeeShip"]);
                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = Convert.ToDouble(reader["Weight"]);
                if (reader["Volume"] != DBNull.Value)
                    entity.Volume = Convert.ToDouble(reader["Volume"]);
                int Status = 1;
                if (reader["Status"] != DBNull.Value)
                    Status = reader["Status"].ToString().ToInt(0);
                entity.Status = Status;
                entity.StatuStr = PJUtils.IntToStringStatusSmallPackage(Status);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public class SmallPackageGet
        {
            public int ID { get; set; }
            public int BigPackageID { get; set; }
            public string BigpackageCode { get; set; }
            public string OrderTransactionCode { get; set; }
            public string ProductType { get; set; }
            public int MainOrderID { get; set; }
            public double Weight { get; set; }
            public double FeeShip { get; set; }
            public double Volume { get; set; }
            public int KhoTQID { get; set; }
            public int KhoVNID { get; set; }
            public int Status { get; set; }
            public string StatuStr { get; set; }
            public string KhoTQUsername { get; set; }
            public string KhoVNUsername { get; set; }
            public DateTime DateCheckKhoTQ { get; set; }
            public DateTime DateCheckKhoVN { get; set; }
            public DateTime CreatedDate { get; set; }
        }
        public static List<SmallPackageGet> GetbyKhoTQIDOrKho(int KhoTQID, int KhoVNID, string fd, string td)
        {
            var list = new List<SmallPackageGet>();
            var sql = @"SELECT * FROM dbo.tbl_SmallPackage ";
            if (KhoTQID > 0)
            {
                sql += " where KhoTQID = " + KhoTQID + " ";
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND DateCheckKhoTQ >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND DateCheckKhoTQ <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                sql += " Order By ID desc";
            }
            else
            {
                sql += " where KhoVNID = " + KhoVNID + " ";
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND DateCheckKhoVN >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND DateCheckKhoVN <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                sql += " Order By ID desc";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new SmallPackageGet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["MainOrderID"] != DBNull.Value)
                    entity.MainOrderID = reader["MainOrderID"].ToString().ToInt(0);
                if (reader["KhoTQID"] != DBNull.Value)
                    entity.KhoTQID = reader["KhoTQID"].ToString().ToInt(0);
                if (reader["KhoVNID"] != DBNull.Value)
                    entity.KhoVNID = reader["KhoVNID"].ToString().ToInt(0);
                if (reader["KhoTQUsername"] != DBNull.Value)
                    entity.KhoTQUsername = reader["KhoTQUsername"].ToString();
                if (reader["KhoVNUsername"] != DBNull.Value)
                    entity.KhoVNUsername = reader["KhoVNUsername"].ToString();
                if (reader["DateCheckKhoTQ"] != DBNull.Value)
                    entity.DateCheckKhoTQ = Convert.ToDateTime(reader["DateCheckKhoTQ"].ToString());
                if (reader["DateCheckKhoVN"] != DBNull.Value)
                    entity.DateCheckKhoVN = Convert.ToDateTime(reader["DateCheckKhoVN"].ToString());
                if (reader["Weight"] != DBNull.Value)
                    entity.Weight = reader["Weight"].ToString().ToFloat(0);
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_SmallPackage> GetByTransportationOrderID(int TransportationOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.TransportationOrderID == TransportationOrderID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        public static List<tbl_SmallPackage> GetByTransportationOrderIDAndStatus(int TransportationOrderID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_SmallPackage> ps = new List<tbl_SmallPackage>();
                ps = dbe.tbl_SmallPackage.Where(p => p.TransportationOrderID == TransportationOrderID && p.Status == Status).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        #endregion
    }
}