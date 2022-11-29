using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using MB.Extensions;


namespace NHST.Controllers
{
    public class AccountController
    {
        #region CRUD
        public static string Insert(string Username, string Email, string Password, int RoleID, int LevelID, int VIPLevel, int Status, int SaleID, int DathangID, DateTime CreatedDate, string CreatedBy, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities()) //now wrapping the context in a using to ensure it is disposed
            {

                tbl_Account user = new tbl_Account();
                user.Username = Username;
                user.Email = Email;
                user.Password = PJUtils.Encrypt("userpass", Password);
                user.RoleID = RoleID;
                user.LevelID = LevelID;
                user.VIPLevel = VIPLevel;
                user.Status = Status;
                user.Wallet = 0;
                user.SaleID = SaleID;
                user.DathangID = DathangID;
                user.CreatedDate = CreatedDate;
                user.CreatedBy = CreatedBy;
                user.ModifiedBy = ModifiedBy;
                user.ModifiedDate = ModifiedDate;
                dbe.tbl_Account.Add(user);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                string k = user.ID.ToString();
                return k;
            }

        }
        public static string updateEmail(int ID, string Email)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Email = Email;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateIsAgent(int ID, bool isAgent)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.IsAgent = isAgent;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateIsLocal(int ID, bool IsLocal)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.IsLocal = IsLocal;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateIsVip(int ID, bool IsVip)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.IsVip = IsVip;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateVipLevel(int ID, int VIPLevel, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.VIPLevel = VIPLevel;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateLevelID(int ID, int LevelID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.LevelID = LevelID;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateWallet(int ID, double Wallet, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Wallet = Wallet;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;

                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updatestatus(int ID, int status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Status = status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateRole(int ID, int roleid, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.RoleID = roleid;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateSaleID(int ID, int saleID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.SaleID = saleID;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdateDathangID(int ID, int DathangID, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {


                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.DathangID = DathangID;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string UpdatePassword(int ID, string Password)
        {
            using (var dbe = new NHSTEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Password = PJUtils.Encrypt("userpass", Password);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return null;
            }
        }
        public static string updateWalletCYN(int ID, double WalletCYN)
        {
            using (var dbe = new NHSTEntities())
            {
                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.WalletCYN = WalletCYN;
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
        public static List<tbl_Account> GetAllNotExcept()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserList> GetAll_View(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserList> las = new List<View_UserList>();
                las = dbe.View_UserList.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserList> GetAll_View_Customer(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserList> las = new List<View_UserList>();
                las = dbe.View_UserList.Where(a => a.Username.Contains(s) && a.RoleID == 1).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserListGetAll> GetAll_View_notexceptadmin()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListGetAll> las = new List<View_UserListGetAll>();
                las = dbe.View_UserListGetAll.Where(a =>a.Username != "phuongnguyen").ToList();
                return las;
            }
        }
        public static List<View_UserListGetAll> GetAll_View_notexceptadminwithrole(int RoleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListGetAll> las = new List<View_UserListGetAll>();
                las = dbe.View_UserListGetAll.Where(a =>a.RoleID == RoleID && a.Username != "phuongnguyen").ToList();
                return las;
            }
        }
        public static List<View_UserListGetAll> GetAll_View_notexceptadminwithagent()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListGetAll> las = new List<View_UserListGetAll>();
                las = dbe.View_UserListGetAll.Where(a =>a.IsAgent == true && a.Username != "phuongnguyen").ToList();
                return las;
            }
        }
        public static List<View_UserListWithWallet> GetAllWithWallet_View(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListWithWallet> las = new List<View_UserListWithWallet>();
                las = dbe.View_UserListWithWallet.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<View_UserListExcel> GetAll_ViewUserListExcel(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_UserListExcel> las = new List<View_UserListExcel>();
                las = dbe.View_UserListExcel.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.ID).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetUserAll(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID == 1).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllOrderDesc(string s)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.ID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllNotSearch()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleQuantri()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID != 1).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleID(int RoleID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleIDAndRoleID(int RoleID1, int RoleID2)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID1 || a.RoleID == RoleID2).OrderBy(a => a.RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleIDAndRoleIDNotOr(int RoleID1, int RoleID2)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID1 || a.RoleID == RoleID2).OrderBy(a => a.RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }

        public static tbl_Account GetByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.ID == ID).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static tbl_Account GetByUsername(string Username)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByEmail(string Email)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account Login(string Username, string Password)
        {
            using (var dbe = new NHSTEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username && a.Password == Password).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account LoginEmail(string Email, string Password)
        {
            using (var dbe = new NHSTEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email && a.Password == Password).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByPhone(string Phone)
        {
            using (var dbe = new NHSTEntities())
            {
                var ai = dbe.tbl_AccountInfo.Where(a => a.Phone == Phone).FirstOrDefault();
                if (ai != null)
                {
                    tbl_Account acc = dbe.tbl_Account.Where(a => a.ID == ai.UID).FirstOrDefault();
                    if (acc != null)
                        return acc;
                    else
                        return null;
                }
                else
                    return null;


            }
        }
        #endregion
    }
}