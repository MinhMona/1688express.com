using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class HistoryPayWalletController
    {       
        #region CRUD
        public static string Insert(int UID, string UserName, int MainOrderID, double Amount, string HContent,double MoneyLeft, int Type,int TradeType,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = MainOrderID;
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertTransportation(int UID, string UserName, int MainOrderID, double Amount, string HContent, double MoneyLeft, int Type, int TradeType,
            DateTime CreatedDate, string CreatedBy, int TransportationOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = MainOrderID;
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = Type;
                a.TradeType = TradeType;
                a.TransportationOrderID = TransportationOrderID;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                string k = a.ID.ToString();
                return k;
            }
        }
        public static string InsertTransport(int UID, string UserName, double Amount, string HContent, double MoneyLeft, DateTime DateSend, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_HistoryPayWallet a = new tbl_HistoryPayWallet();
                a.UID = UID;
                a.UserName = UserName;
                a.MainOrderID = 0;                
                a.Amount = Amount;
                a.HContent = HContent;
                a.MoneyLeft = MoneyLeft;
                a.Type = 1;
                a.TradeType = 8;
                a.DateSend = DateSend;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_HistoryPayWallet.Add(a);
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = a.ID.ToString();
                return k;
            }
        }
        #endregion
        #region Select
        public static List<tbl_HistoryPayWallet> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UserName.Contains(s)).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UID == UID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByMainOrderID(int MainOrderID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.MainOrderID == MainOrderID).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
       
        public static List<tbl_HistoryPayWallet> GetFromDateTodate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> lo = new List<tbl_HistoryPayWallet>();

                var alllist = dbe.tbl_HistoryPayWallet.OrderByDescending(t => t.CreatedDate).ToList();

                if (!string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from && t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else if (!string.IsNullOrEmpty(from.ToString()) && string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate >= from).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else if (string.IsNullOrEmpty(from.ToString()) && !string.IsNullOrEmpty(to.ToString()))
                {
                    lo = alllist.Where(t => t.CreatedDate <= to).OrderByDescending(t => t.CreatedDate).ToList();
                }
                else
                {
                    lo = alllist;
                }
               return lo;
            }
        }
        public static List<tbl_HistoryPayWallet> GetByUIDTradeTypeDateSend(int UID, int TradeType, DateTime DateSend)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_HistoryPayWallet> aus = new List<tbl_HistoryPayWallet>();
                aus = dbe.tbl_HistoryPayWallet.Where(a => a.UID == UID && a.TradeType == TradeType && a.DateSend == DateSend).OrderByDescending(a => a.ID).ToList();
                return aus;
            }
        }
        #endregion
    }
}
