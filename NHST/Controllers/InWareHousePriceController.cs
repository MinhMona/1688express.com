using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class InWareHousePriceController
    {
        #region CRUD
        public static string Insert(double WeightFrom, double WeightTo, double MaxDay, double PricePay, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_InWareHousePrice p = new tbl_InWareHousePrice();
                p.WeightFrom = WeightFrom;
                p.WeightTo = WeightTo;
                p.MaxDay = MaxDay;
                p.PricePay = PricePay;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_InWareHousePrice.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = p.ID.ToString();
                return k;
            }
        }
        public static string Update(int ID, double WeightFrom, double WeightTo, double MaxDay, double PricePay, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_InWareHousePrice.Where(pa => pa.ID == ID).SingleOrDefault();
                if (p != null)
                {
                    p.WeightFrom = WeightFrom;
                    p.WeightTo = WeightTo;
                    p.MaxDay = MaxDay;
                    p.PricePay = PricePay;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        #endregion
        #region Select
        public static List<tbl_InWareHousePrice> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_InWareHousePrice> pages = new List<tbl_InWareHousePrice>();
                pages = dbe.tbl_InWareHousePrice.OrderBy(a => a.ID).ToList();
                return pages;
            }
        }
        
        public static tbl_InWareHousePrice GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_InWareHousePrice page = dbe.tbl_InWareHousePrice.Where(p => p.ID == ID).SingleOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        public static tbl_InWareHousePrice GetByWFWT(double WeightFrom, double WeightTo)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_InWareHousePrice page = dbe.tbl_InWareHousePrice.Where(p => p.WeightFrom > WeightFrom && p.WeightTo <= WeightTo).SingleOrDefault();
                if (page != null)
                    return page;
                else
                    return null;
            }
        }
        #endregion
    }
}