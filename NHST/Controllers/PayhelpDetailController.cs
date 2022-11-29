using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class PayhelpDetailController
    {
        #region CRUD
        public static string Insert(int PayhelpID, string Desc1, string Desc2, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_PayhelpDetail o = new tbl_PayhelpDetail();
                o.PayhelpID = PayhelpID;
                o.Desc1 = Desc1;
                o.Desc2 = Desc2;              
                o.CreatedDate = CreatedDate;
                o.CreatedBy = CreatedBy;
                dbe.tbl_PayhelpDetail.Add(o);
                dbe.SaveChanges();
                int kq = o.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Desc1, string Desc2, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayhelpDetail.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    o.Desc1 = Desc1;
                    o.Desc2 = Desc2;
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
        public static tbl_PayhelpDetail GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var o = dbe.tbl_PayhelpDetail.Where(od => od.ID == ID).FirstOrDefault();
                if (o != null)
                {
                    return o;
                }
                else return null;
            }
        }
        public static List<tbl_PayhelpDetail> GetByPayhelpID(int PayhelpID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_PayhelpDetail> ps = new List<tbl_PayhelpDetail>();
                ps = dbe.tbl_PayhelpDetail.Where(p=>p.PayhelpID == PayhelpID).OrderByDescending(p => p.ID).ToList();
                return ps;
            }
        }
        #endregion
    }
}