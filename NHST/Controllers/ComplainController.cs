using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;

namespace NHST.Controllers
{
    public class ComplainController
    {
        #region CRUD
        public static string Insert(int UID, int OrderID, string Amount, string IMG, string ComplainText, int Status,
           int ProductID, string OrderCode, string OrderShopCode, int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Complain c = new tbl_Complain();
                c.UID = UID;
                c.OrderID = OrderID;
                c.Amount = Amount;
                c.IMG = IMG;
                c.ComplainText = ComplainText;
                c.Status = Status;

                c.ProductID = ProductID;
                c.OrderCode = OrderCode;
                c.OrderShopCode = OrderShopCode;
                c.Type = Type;

                c.CreatedDate = CreatedDate;
                c.CreatedBy = CreatedBy;
                dbe.tbl_Complain.Add(c);
                dbe.SaveChanges();
                string kq = c.ID.ToString();
                return kq;
            }
        }
        public static string Update(int ID, string Amount, string ComplainText, int Status, int Type, string StaffComment,
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    c.Amount = Amount;
                    c.ComplainText = ComplainText;
                    c.Status = Status;
                    c.Type = Type;
                    c.StaffComment = StaffComment;
                    c.ModifiedDate = ModifiedDate;
                    c.ModifiedBy = ModifiedBy;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Complain> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static List<tbl_Complain> GetAllWithStatus(string s, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                if (Status != -1)
                {
                    cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s) && c.Status == Status).OrderByDescending(c => c.ID).ToList();
                }
                else
                {
                    cs = dbe.tbl_Complain.Where(c => c.CreatedBy.Contains(s)).OrderByDescending(c => c.ID).ToList();
                }

                return cs;
            }
        }
        public static List<tbl_Complain> GetAllByOrderShopCodeAndUID(int UID, int OrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Complain> cs = new List<tbl_Complain>();
                cs = dbe.tbl_Complain.Where(c => c.UID == UID && c.OrderID == OrderID).OrderByDescending(c => c.ID).ToList();
                return cs;
            }
        }
        public static tbl_Complain GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var c = dbe.tbl_Complain.Where(p => p.ID == ID).FirstOrDefault();
                if (c != null)
                {
                    return c;
                }
                else
                    return null;
            }
        }
        #endregion
    }
}