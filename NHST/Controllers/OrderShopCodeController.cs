using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;
using System.Text;

namespace NHST.Controllers
{
    public class OrderShopCodeController
    {
        #region CRUD
        public static string Insert(int MainOrderID, string OrderShopCode, string ShopID, string ShopName, DateTime CreatedDate,
            string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OrderShopCode p = new tbl_OrderShopCode();
                p.MainOrderID = MainOrderID;
                p.OrderShopCode = OrderShopCode;
                p.ShopID = ShopID;
                p.ShopName = ShopName;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_OrderShopCode.Add(p);
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, int MainOrderID, string OrderShopCode, string ShopID, string ShopName,
            string ModifiedBy, DateTime ModifiedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_OrderShopCode.Where(pa => pa.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    p.MainOrderID = MainOrderID;
                    p.OrderShopCode = OrderShopCode;
                    p.ShopID = ShopID;
                    p.ShopName = ShopName;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderShopCode(int ID, string OrderShopCode, string ModifiedBy, DateTime ModifiedDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_OrderShopCode.Where(pa => pa.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    p.OrderShopCode = OrderShopCode;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
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
                var p = dbe.tbl_OrderShopCode.Where(pa => pa.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    dbe.tbl_OrderShopCode.Remove(p);
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_OrderShopCode> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopCode> pages = new List<tbl_OrderShopCode>();
                pages = dbe.tbl_OrderShopCode.ToList();
                return pages;
            }
        }
        public static List<tbl_OrderShopCode> GetByShopID(string ShopID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopCode> pages = new List<tbl_OrderShopCode>();
                pages = dbe.tbl_OrderShopCode.Where(o => o.ShopID == ShopID).ToList();
                return pages;
            }
        }
        public static List<tbl_OrderShopCode> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopCode> pages = new List<tbl_OrderShopCode>();
                pages = dbe.tbl_OrderShopCode.Where(o => o.MainOrderID == MainOrderID).ToList();
                return pages;
            }
        }
        public static tbl_OrderShopCode GetByMainOrderIDAndOrderShopCode(int MainOrderID, string orderShopCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_OrderShopCode.Where(o => o.MainOrderID == MainOrderID && o.OrderShopCode == orderShopCode).FirstOrDefault();
                return pages;
            }
        }
        public static List<tbl_OrderShopCode> GetByShopIDAndMainOrderID(string ShopID, int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_OrderShopCode> pages = new List<tbl_OrderShopCode>();
                pages = dbe.tbl_OrderShopCode.Where(o => o.ShopID == ShopID && o.MainOrderID == MainOrderID).ToList();
                return pages;
            }
        }
        public static tbl_OrderShopCode GetByOrderShop(string OrderShopCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_OrderShopCode.Where(o => o.OrderShopCode == OrderShopCode).FirstOrDefault();
                return pages;
            }
        }
        public static tbl_OrderShopCode GetByOrderShopCodeAndShopIDAndMainOrderID(string OrderShopCode, string ShopID, int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                var pages = dbe.tbl_OrderShopCode.Where(o => o.OrderShopCode == OrderShopCode && o.ShopID == ShopID && o.MainOrderID == MainOrderID).FirstOrDefault();
                return pages;
            }
        }
        public static tbl_OrderShopCode GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_OrderShopCode page = dbe.tbl_OrderShopCode.Where(p => p.ID == ID).SingleOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static List<objGroupOshop> GetbyMainorderId(int mainOrderID)
        {
            var listO = new List<objGroup>();
            var listgroupO = new List<objGroupOshop>();
            var sql = @"select o.mainorderid, o.id, OrderShopCode, OrderTransactionCode from tbl_OrderShopCode as o left outer join ";
            sql += " (SELECT OrderShopCodeID, OrderTransactionCode FROM tbl_smallpackage) sm1 ON sm1.OrderShopCodeID = o.ID";
            sql += " where o.MainOrderID = " + mainOrderID + "";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new objGroup();
                if (reader["OrderShopCode"] != DBNull.Value)
                    entity.OrderShopCode = reader["OrderShopCode"].ToString();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["mainorderid"] != DBNull.Value)
                    entity.MainOrderID = reader["mainorderid"].ToString().ToInt(0);
                if (reader["OrderShopCode"] != DBNull.Value)
                    entity.OrderShopCode = reader["OrderShopCode"].ToString();
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                listO.Add(entity);
            }
            reader.Close();
            if (listO.Count > 0)
            {
                var resu = from p in listO
                           group p.OrderShopCode by p.OrderShopCode into g
                           select new { OrderShopCode = g.Key, Cars = g.ToList() };
                foreach (var item in resu)
                {
                    objGroupOshop oShopG = new objGroupOshop();
                    oShopG.oShopcode = item.OrderShopCode;
                    List<OSmall> losmall = new List<OSmall>();
                    var smalls = listO.Where(o => o.OrderShopCode == item.OrderShopCode).ToList();
                    if (smalls.Count > 0)
                    {
                        foreach (var ismalls in smalls)
                        {
                            OSmall sm = new OSmall();
                            sm.OrderTransactionCode = ismalls.OrderTransactionCode;
                            losmall.Add(sm);
                        }
                    }
                    oShopG.listSmallPackages = losmall;
                    listgroupO.Add(oShopG);
                }

            }
            return listgroupO;
            //return listO;
        }
        public static string GetbyMainorderIdRetStr(int mainOrderID)
        {
            var listO = new List<objGroup>();
            var listgroupO = new List<objGroupOshop>();
            var sql = @"select OrderShopCode, OrderTransactionCode from tbl_OrderShopCode as o left outer join ";
            sql += " (SELECT OrderShopCodeID, OrderTransactionCode FROM tbl_smallpackage) sm1 ON sm1.OrderShopCodeID = o.ID";
            sql += " where o.MainOrderID = " + mainOrderID + "";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new objGroup();           
                if (reader["OrderShopCode"] != DBNull.Value)
                    entity.OrderShopCode = reader["OrderShopCode"].ToString();
                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                listO.Add(entity);
            }
            reader.Close();
            StringBuilder htmlmadonhang = new StringBuilder();
            if (listO.Count > 0)
            {
                //var resu = from p in listO
                //           group p.OrderShopCode by p.OrderShopCode into g
                //           select new { OrderShopCode = g.Key, Cars = g.ToList() };
                var resu = listO.GroupBy(p => p.OrderShopCode);
                htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                foreach (var item in resu)
                {
                    string oschopcode = item.Key;
                    var smalls = listO.Where(o => o.OrderShopCode == oschopcode).ToList();
                    int scount = smalls.Count;
                    if (scount > 0)
                    {
                        int i = 0;
                        foreach (var items in smalls)
                        {
                            if (i == 0)
                            {
                                htmlmadonhang.Append("<tr>");
                                htmlmadonhang.Append("<td rowspan=\"" + scount + "\">" + oschopcode + "</td>");
                                htmlmadonhang.Append("<td>" + items.OrderTransactionCode + "</td>");
                                htmlmadonhang.Append("</tr>");
                            }
                            else
                            {
                                htmlmadonhang.Append("<tr>");
                                htmlmadonhang.Append("<td>" + items.OrderTransactionCode + "</td>");
                                htmlmadonhang.Append("</tr>");
                            }
                            i++;
                        }
                    }
                    else
                    {
                        htmlmadonhang.Append("<tr>");
                        htmlmadonhang.Append("<td >" + oschopcode + "</td>");
                        htmlmadonhang.Append("<td></td>");
                        htmlmadonhang.Append("</tr>");
                    }
                }
                htmlmadonhang.Append("</table>");
            }
            return htmlmadonhang.ToString();
        }
        public class objGroup
        {
            public int MainOrderID { get; set; }
            public int ID { get; set; }
            public string OrderShopCode { get; set; }
            public string OrderTransactionCode { get; set; }
        }
        public class objGroupOshop
        {
            public string oShopcode { get; set; }
            public List<OSmall> listSmallPackages { get; set; }
        }
        public class OSmall
        {
            public string OrderTransactionCode { get; set; }
        }
        #endregion
    }
}