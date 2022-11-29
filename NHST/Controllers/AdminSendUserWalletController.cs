using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
namespace NHST.Controllers
{
    public class AdminSendUserWalletController
    {
        
        #region CRUD
        public static string Insert(int UID, string Username, double Amount, int Status, string Content, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = new tbl_AdminSendUserWallet();
                a.UID = UID;
                a.Username = Username;
                a.Amount = Amount;
                a.Status = Status;
                a.TradeContent = Content;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_AdminSendUserWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string UpdateStatus(int ID, int Status, string Content, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Status = Status;
                    a.TradeContent = Content;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_AdminSendUserWallet GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_AdminSendUserWallet a = dbe.tbl_AdminSendUserWallet.Where(ad => ad.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    return a;
                }
                else
                    return null;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> lo = new List<tbl_AdminSendUserWallet>();

                var alllist = dbe.tbl_AdminSendUserWallet.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                }
                else
                {
                    lo = alllist;
                }
                if (lo.Count > 0)
                    return lo.Where(l=>l.Status == 2).ToList();
                else return lo;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByCreatedBy(string s, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.Username.Contains(s) && a.CreatedBy == CreatedBy).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_AdminSendUserWallet> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_AdminSendUserWallet> aus = new List<tbl_AdminSendUserWallet>();
                aus = dbe.tbl_AdminSendUserWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        #endregion
    }
}