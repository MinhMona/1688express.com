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
    public class MainOrderController
    {
        #region CRUD
        public static string Insert(int UID, string ShopID, string ShopName, string Site, bool IsForward, string IsForwardPrice, bool IsFastDelivery, string IsFastDeliveryPrice, bool IsCheckProduct,
            string IsCheckProductPrice, bool IsPacked, string IsPackedPrice, bool IsFast, string IsFastPrice, string PriceVND, string PriceCNY, string FeeShipCN, string FeeBuyPro, string FeeWeight,
            string Note, string FullName, string Address, string Email, string Phone, int Status, string Deposit, string CurrentCNYVN, string TotalPriceVND,
            int SalerID, int DathangID, DateTime CreatedDate, int CreatedBy, string AmountDeposit, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {

                tbl_MainOder o = new tbl_MainOder();
                o.UID = UID;
                o.ShopID = ShopID;
                o.ShopName = ShopName;
                o.Site = Site;
                o.IsForward = IsForward;
                o.IsForwardPrice = IsForwardPrice;
                o.IsFastDelivery = IsFastDelivery;
                o.IsFastDeliveryPrice = IsFastDeliveryPrice;
                o.IsCheckProduct = IsCheckProduct;
                o.IsCheckProductPrice = IsCheckProductPrice;
                o.IsPacked = IsPacked;
                o.IsPackedPrice = IsPackedPrice;
                o.IsFast = IsFast;
                o.IsFastPrice = IsFastPrice;
                o.PriceVND = PriceVND;
                o.PriceCNY = PriceCNY;
                o.FeeShipCN = FeeShipCN;
                o.FeeBuyPro = FeeBuyPro;
                o.FeeWeight = FeeWeight;
                o.Note = Note;
                o.FullName = FullName;
                o.Address = Address;
                o.Email = Email;
                o.Phone = Phone;
                o.Status = Status;
                o.Deposit = Deposit;
                o.CurrentCNYVN = CurrentCNYVN;
                o.TotalPriceVND = TotalPriceVND;
                o.SalerID = SalerID;
                o.DathangID = DathangID;
                o.KhoTQID = 0;
                o.KhoVNID = 0;
                o.FeeShipCNToVN = "0";
                o.CreatedDate = CreatedDate;
                o.IsHidden = false;
                o.AmountDeposit = AmountDeposit;
                o.OrderType = OrderType;
                dbe.tbl_MainOder.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }
        public static string InsertWeChat(int UID, string ShopID, string ShopName, string Site, bool IsForward, string IsForwardPrice, bool IsFastDelivery, string IsFastDeliveryPrice, bool IsCheckProduct,
            string IsCheckProductPrice, bool IsPacked, string IsPackedPrice, bool IsFast, string IsFastPrice, string PriceVND, string PriceCNY, string FeeShipCN, string FeeBuyPro, string FeeWeight,
            string Note, string FullName, string Address, string Email, string Phone, int Status, string Deposit, string CurrentCNYVN, string TotalPriceVND,
            int SalerID, int DathangID, DateTime CreatedDate, int CreatedBy, string AmountDeposit, int OrderType,
            string WeChatFeeCYN, string WeChatFeeVND)
        {
            using (var dbe = new NHSTEntities())
            {

                tbl_MainOder o = new tbl_MainOder();
                o.UID = UID;
                o.ShopID = ShopID;
                o.ShopName = ShopName;
                o.Site = Site;
                o.IsForward = IsForward;
                o.IsForwardPrice = IsForwardPrice;
                o.IsFastDelivery = IsFastDelivery;
                o.IsFastDeliveryPrice = IsFastDeliveryPrice;
                o.IsCheckProduct = IsCheckProduct;
                o.IsCheckProductPrice = IsCheckProductPrice;
                o.IsPacked = IsPacked;
                o.IsPackedPrice = IsPackedPrice;
                o.IsFast = IsFast;
                o.IsFastPrice = IsFastPrice;
                o.PriceVND = PriceVND;
                o.PriceCNY = PriceCNY;
                o.FeeShipCN = FeeShipCN;
                o.FeeBuyPro = FeeBuyPro;
                o.FeeWeight = FeeWeight;
                o.Note = Note;
                o.FullName = FullName;
                o.Address = Address;
                o.Email = Email;
                o.Phone = Phone;
                o.Status = Status;
                o.Deposit = Deposit;
                o.CurrentCNYVN = CurrentCNYVN;
                o.TotalPriceVND = TotalPriceVND;
                o.SalerID = SalerID;
                o.DathangID = DathangID;
                o.KhoTQID = 0;
                o.KhoVNID = 0;
                o.FeeShipCNToVN = "0";
                o.CreatedDate = CreatedDate;
                o.IsHidden = false;
                o.AmountDeposit = AmountDeposit;
                o.OrderType = OrderType;
                o.WeChatFeeCYN = WeChatFeeCYN;
                o.WeChatFeeVND = WeChatFeeVND;
                dbe.tbl_MainOder.Add(o);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                dbe.SaveChanges();
                string k = o.ID.ToString();
                return k;
            }
        }
        public static string UpdateStaff(int ID, int SalerID, int DathangID, int KhoTQID, int KhoVNID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.SalerID = SalerID;
                    or.DathangID = DathangID;
                    or.KhoTQID = KhoTQID;
                    or.KhoVNID = KhoVNID;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatus(int ID, int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateStatusByID(int ID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.Status = Status;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeight(int ID, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeightFeeWeightTotalPrice(int ID, string OrderWeight, string FeeWeight, string TotalPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderWeight = OrderWeight;
                    or.TQVNWeight = OrderWeight;
                    or.FeeWeight = FeeWeight;
                    or.TotalPriceVND = TotalPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateAmountDeposit(int ID, string AmountDeposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.AmountDeposit = AmountDeposit;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateNote(int ID, string Note)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.Note = Note;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateCheckPro(int ID, bool IsCheckProduct)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsCheckProduct = IsCheckProduct;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsGiaohang(int ID, bool IsGiaohang)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsGiaohang = IsGiaohang;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsPacked(int ID, bool IsPacked)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsPacked = IsPacked;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeightDC(int ID, string Feeweightdc)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = Feeweightdc;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsFastDelivery(int ID, bool IsFastDelivery)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsFastDelivery = IsFastDelivery;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderWeightCK(int ID, string OrderWeightCKS)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = OrderWeightCKS;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode(int ID, string OrderTransactionCode, string OrderTransactionCodeweight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode = OrderTransactionCode;
                    or.OrderTransactionCodeWeight = OrderTransactionCodeweight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode2(int ID, string OrderTransactionCode2, string OrderTransactionCodeweight2)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode2 = OrderTransactionCode2;
                    or.OrderTransactionCodeWeight2 = OrderTransactionCodeweight2;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode3(int ID, string OrderTransactionCode3, string OrderTransactionCodeweight3)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode3 = OrderTransactionCode3;
                    or.OrderTransactionCodeWeight3 = OrderTransactionCodeweight3;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode4(int ID, string OrderTransactionCode4, string OrderTransactionCodeweight4)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode4 = OrderTransactionCode4;
                    or.OrderTransactionCodeWeight4 = OrderTransactionCodeweight4;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderTransactionCode5(int ID, string OrderTransactionCode5, string OrderTransactionCodeweight5)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderTransactionCode5 = OrderTransactionCode5;
                    or.OrderTransactionCodeWeight5 = OrderTransactionCodeweight5;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDeposit(int ID, int UID, string Deposit)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateReceivePlace(int ID, int UID, string ReceivePlace, int ShippingType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.ReceivePlace = ReceivePlace;
                    or.ShippingType = ShippingType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateIsCheckNotiPrice(int ID, int UID, bool IsCheckNotiPrice)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsCheckNotiPrice = IsCheckNotiPrice;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateOrderType(int ID, int UID, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.OrderType = OrderType;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTQVNWeight(int ID, int UID, string TQVNWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TQVNWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateMainOrderCode(int ID, int UID, string MainOrderCode)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.MainOrderCode = MainOrderCode;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalPriceReal(int ID, string TotalPriceReal, string TotalPriceRealCYN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.TotalPriceReal = TotalPriceReal;
                    or.TotalPriceRealCYN = TotalPriceRealCYN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateDepositDate(int ID, DateTime DepositDate)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.DepostiDate = DepositDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFee(int ID, string Deposit, string FeeShipCN, string FeeBuyPro, string FeeWeight,
           string IsCheckProductPrice, string IsPackedPrice, string IsFastDeliveryPrice, string TotalPriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.Deposit = Deposit;
                    or.FeeShipCN = FeeShipCN;
                    or.FeeBuyPro = FeeBuyPro;
                    or.FeeWeight = FeeWeight;
                    or.IsCheckProductPrice = IsCheckProductPrice;
                    or.IsPackedPrice = IsPackedPrice;
                    or.IsFastDeliveryPrice = IsFastDeliveryPrice;
                    or.TotalPriceVND = TotalPriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeSensor(int ID, string AdditionFeeForSensorProduct)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.AdditionFeeForSensorProduct = AdditionFeeForSensorProduct;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeWeChat(int ID, string WeChatFeeCYN, string WeChatFeeVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.WeChatFeeCYN = WeChatFeeCYN;
                    or.WeChatFeeVND = WeChatFeeVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateTotalWeight(int ID, string TotalWeight, string OrderWeight)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.TQVNWeight = TotalWeight;
                    or.OrderWeight = OrderWeight;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeShipTQVN(int ID, string FeeShipTQVN)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.FeeShipCN = FeeShipTQVN;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceNotFee(int ID, string PriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.PriceVND = PriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceCYN(int ID, string PriceCNY)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.PriceCNY = PriceCNY;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdatePriceVND(int ID, string PriceVND)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.PriceVND = PriceVND;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        public static string UpdateFeeweightCK(int ID, string FeeWeightCK)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.FeeWeightCK = FeeWeightCK;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }

        public static string UpdateIsHiddenTrue(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var or = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (or != null)
                {
                    or.IsHidden = true;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    dbe.SaveChanges();
                    return "ok";
                }
                else
                    return null;
            }
        }
        #endregion

        #region GetAll
        public static List<View_OrderListFilterWithStatusString> GetByUserInViewFilterWithStatusString(int RoleID, int StaffID, string searchtext, int Type, int OrderType)
        {
            using (var dbe = new NHSTEntities())
            {
                if (RoleID != 1)
                {
                    List<View_OrderListFilterWithStatusString> lo = new List<View_OrderListFilterWithStatusString>();
                    List<View_OrderListFilterWithStatusString> losearch = new List<View_OrderListFilterWithStatusString>();
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(o => o.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(o => o.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.DathangID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status < 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 5 && l.Status <= 7 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status != 1 && l.SalerID == StaffID && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 9 && l.Status < 10 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterWithStatusString.Where(l => l.Status >= 2 && l.OrderType == OrderType).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {

                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            if (Type == 1)
                            {
                                var listos = GetMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else if (Type == 2)
                            {
                                var listos = GetSmallPackageMainOrderIDBySearch(searchtext);
                                if (listos.Count > 0)
                                {
                                    foreach (var id in listos)
                                    {
                                        var a = lo.Where(o => o.ID == id.ID).FirstOrDefault();
                                        if (a != null)
                                        {
                                            losearch.Add(a);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in lo)
                                {
                                    if (item.OrderType == 1)
                                    {
                                        var ordershopcode = OrderShopCodeController.GetByMainOrderIDAndOrderShopCode(item.ID, searchtext);
                                        if (ordershopcode != null)
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else
                                    {
                                        bool check = false;
                                        var listOrder = OrderController.GetByMainOrderID(item.ID);
                                        if (listOrder.Count > 0)
                                        {
                                            foreach (var o in listOrder)
                                            {
                                                if (o.OrderShopCode.Contains(searchtext))
                                                {
                                                    check = true;
                                                }
                                            }
                                        }
                                        if (check == true)
                                        {
                                            losearch.Add(item);
                                        }
                                    }

                                }
                                //losearch = lo.Where(o => o.MainOrderCode == searchtext).ToList();
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                    return losearch;
                }
                return null;
            }
        }

        public static string SelectUIDByIDOrder(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var ot = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (ot != null)
                {
                    return ot.UID.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<tbl_MainOder> GetAll()
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.OrderByDescending(o => o.ID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetByRoleID(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 3)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.Status < 5 && l.DathangID == StaffID).ToList();
                    else if (RoleID == 4)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5 && (l.KhoTQID == StaffID || l.KhoTQID == 0)).ToList();
                    else if (RoleID == 5)
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 6 && (l.KhoVNID == StaffID || l.KhoVNID == 0)).ToList();
                    else if (RoleID == 6)
                        lo = dbe.tbl_MainOder.Where(l => l.SalerID == StaffID).ToList();
                    else if (RoleID == 7)
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 7).ToList();
                    else
                    {
                        lo = dbe.tbl_MainOder.ToList();
                    }
                }
                return lo;
            }
        }
        public static List<View_OrderList> GetByUserInView(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderList> lo = new List<View_OrderList>();
                List<View_OrderList> losearch = new List<View_OrderList>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderList.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderList.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter(int RoleID, int StaffID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }

                }
                return lo;
            }
        }
        public static List<View_OrderListFilter> GetByUserInViewFilter2(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilter> lo = new List<View_OrderListFilter>();
                List<View_OrderListFilter> losearch = new List<View_OrderListFilter>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilter.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilter.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    var findpackage = SmallPackageController.GetByMainOrderIDAndCode(item.ID, searchtext);
                                    if (findpackage.Count > 0)
                                    {
                                        losearch.Add(item);
                                    }
                                    //if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    //{
                                    //    if (item.OrderTransactionCode.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    //{
                                    //    if (item.OrderTransactionCode2.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    //{
                                    //    if (item.OrderTransactionCode3.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    //{
                                    //    if (item.OrderTransactionCode4.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                    //else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    //{
                                    //    if (item.OrderTransactionCode5.Contains(searchtext))
                                    //    {
                                    //        losearch.Add(item);
                                    //    }
                                    //}
                                }
                            }
                        }
                        else if (Type == 3)
                        {
                            foreach (var item in lo)
                            {
                                var findpackage = SmallPackageController.GetByMainOrderID(item.ID);
                                if (findpackage.Count == 0)
                                {
                                    losearch.Add(item);
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<View_OrderListDamuahang> GetByUserInViewFilterStatus5()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListDamuahang> lo = new List<View_OrderListDamuahang>();
                lo = dbe.View_OrderListDamuahang.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoTQ> GetByUserInViewFilterStatus6()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoTQ> lo = new List<View_OrderListKhoTQ>();
                lo = dbe.View_OrderListKhoTQ.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListKhoVN> GetByUserInViewFilterStatus7()
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListKhoVN> lo = new List<View_OrderListKhoVN>();
                lo = dbe.View_OrderListKhoVN.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_Orderlistwithstatus> GetByUserInViewFilterStatus(int status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_Orderlistwithstatus> lo = new List<View_Orderlistwithstatus>();
                lo = dbe.View_Orderlistwithstatus.Where(l => l.Status == status).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                return lo;
            }
        }
        public static List<View_OrderListFilterYCGiao> GetByUserInViewFilterYCG(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<View_OrderListFilterYCGiao> lo = new List<View_OrderListFilterYCGiao>();
                List<View_OrderListFilterYCGiao> losearch = new List<View_OrderListFilterYCGiao>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 5 && l.Status <= 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.View_OrderListFilterYCGiao.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }
        public static List<tbl_MainOder> GetByUser(int RoleID, int StaffID, string searchtext, int Type)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                List<tbl_MainOder> losearch = new List<tbl_MainOder>();
                if (RoleID != 1)
                {
                    if (RoleID == 0)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 2)
                    {
                        lo = dbe.tbl_MainOder.OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 3)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2 && l.DathangID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 4)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status == 5).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 5)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 6 && l.Status < 7).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 6)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status != 1 && l.SalerID == StaffID).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else if (RoleID == 8)
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 9 && l.Status < 10).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    else
                    {
                        lo = dbe.tbl_MainOder.Where(l => l.Status >= 2).OrderByDescending(l => l.ID).ThenByDescending(l => l.Status).ToList();
                    }
                    if (lo.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(searchtext))
                        {
                            foreach (var item in lo)
                            {
                                if (Type == 1)
                                {
                                    var pros = OrderController.GetByMainOrderID(item.ID);
                                    if (pros.Count > 0)
                                    {
                                        foreach (var p in pros)
                                        {
                                            if (p.title_origin.Contains(searchtext))
                                            {
                                                losearch.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        losearch = lo;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.OrderTransactionCode))
                                    {
                                        if (item.OrderTransactionCode.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode2))
                                    {
                                        if (item.OrderTransactionCode2.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode3))
                                    {
                                        if (item.OrderTransactionCode3.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode4))
                                    {
                                        if (item.OrderTransactionCode4.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(item.OrderTransactionCode5))
                                    {
                                        if (item.OrderTransactionCode5.Contains(searchtext))
                                        {
                                            losearch.Add(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            losearch = lo;
                        }
                    }
                }
                return losearch;
            }
        }

        public static List<tbl_MainOder> GetSuccessByCustomer(int customerID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(l => l.Status == 10 && l.UID == customerID).ToList();
                return lo;
            }
        }
        public static List<tbl_MainOder> GetFromDateToDate(DateTime from, DateTime to)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();

                var alllist = dbe.tbl_MainOder.OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();

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
                    return lo;
                else return lo;
            }
        }
        public static List<tbl_MainOder> GetAllByUID(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<tbl_MainOder> GetAllByUIDAndStatus(int UID, int Status)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.Status == Status).ToList();
                return lo;
            }
        }
        public static List<tbl_MainOder> GetAllByUIDNotHidden(int UID)
        {
            using (var dbe = new NHSTEntities())
            {
                List<tbl_MainOder> lo = new List<tbl_MainOder>();
                lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.IsHidden == false).OrderByDescending(t => t.CreatedDate).ThenBy(t => t.Status).ToList();
                if (lo.Count > 0)
                    return lo;
                else
                    return null;
            }
        }
        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper(int UID, int status, string fd, string td)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit, mo.AmountDeposit, mo.CreatedDate, mo.DepostiDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, mo.MainOrderCode ";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
            sql += " where UID = " + UID + " ";

            if (status >= 0)
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
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepositDate = Convert.ToDateTime(reader["DepostiDate"].ToString());
                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepositDateStr = reader["DepostiDate"].ToString();

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;

                if (reader["MainOrderCode"] != DBNull.Value)
                    entity.MainOrderCode = reader["MainOrderCode"].ToString();

                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<mainorder> GetAllByUIDOrderCodeNotHidden_SqlHelper(int UID)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.DepostiDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, mo.OrderTransactionCode,mo.OrderTransactionCode2,mo.OrderTransactionCode3,mo.OrderTransactionCode4,mo.OrderTransactionCode5, mo.OrderType, mo.IsCheckNotiPrice, o.anhsanpham, o.quantityPro";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham, sum(CONVERT(float, quantity)) as quantityPro FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where UID = " + UID + " ";
            //sql += " where UID = " + UID + " and IsHidden = 0 ";
            sql += " Order By ID desc";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["DepostiDate"] != DBNull.Value)
                    entity.DepositDate = Convert.ToDateTime(reader["DepostiDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();

                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();

                if (reader["OrderTransactionCode"] != DBNull.Value)
                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                if (reader["OrderTransactionCode2"] != DBNull.Value)
                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                if (reader["OrderTransactionCode3"] != DBNull.Value)
                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                if (reader["OrderTransactionCode4"] != DBNull.Value)
                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                if (reader["OrderTransactionCode5"] != DBNull.Value)
                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();
                if (reader["quantityPro"] != DBNull.Value)
                    entity.quantityPro = reader["quantityPro"].ToString();

                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                if (reader["OrderType"] != DBNull.Value)
                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);
                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"].ToString());
                else
                    entity.IsCheckNotiPrice = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<OrderGetSQL> GetByUserInSQLHelper_nottextnottypeWithstatus(int RoleID, int OrderType, int StaffID, int page, int maxrows)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, mo.MainOrderCode, mo.DepostiDate, ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đang chuyển về kho đích</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã nhận hàng tại kho đích</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM      dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
                sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages=1 LEFT OUTER JOIN";
                sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

                sql += "        Where UID > 0";
                sql += "    AND mo.OrderType  = " + OrderType + "";
                if (RoleID == 3)
                {
                    sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
                }
                else if (RoleID == 4)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status < 7";
                }
                else if (RoleID == 5)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status <= 7";
                }
                else if (RoleID == 6)
                {
                    sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
                }
                else if (RoleID == 8)
                {
                    sql += "    AND mo.Status >= 9 and mo.Status < 10";
                }
                else if (RoleID == 7)
                {
                    sql += "    AND mo.Status >= 2";
                }
                //sql += " ORDER BY mo.ID DESC";
                sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    int MainOrderID = reader["ID"].ToString().ToInt(0);
                    int orderType = reader["OrderType"].ToString().ToInt(0);
                    var entity = new OrderGetSQL();
                    if (reader["ID"] != DBNull.Value)
                        entity.ID = MainOrderID;
                    if (reader["TotalPriceVND"] != DBNull.Value)
                        entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                    if (reader["Deposit"] != DBNull.Value)
                        entity.Deposit = reader["Deposit"].ToString();
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = reader["CreatedDate"].ToString();
                    if (reader["DepostiDate"] != DBNull.Value)
                        entity.DepostiDate = reader["DepostiDate"].ToString();


                    if (reader["Status"] != DBNull.Value)
                    {
                        entity.Status = Convert.ToInt32(reader["Status"].ToString());
                    }
                    if (reader["statusstring"] != DBNull.Value)
                    {
                        entity.statusstring = reader["statusstring"].ToString();
                    }
                    if (reader["OrderTransactionCode"] != DBNull.Value)
                        entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                    if (reader["OrderTransactionCode2"] != DBNull.Value)
                        entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                    if (reader["OrderTransactionCode3"] != DBNull.Value)
                        entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                    if (reader["OrderTransactionCode4"] != DBNull.Value)
                        entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                    if (reader["OrderTransactionCode5"] != DBNull.Value)
                        entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                    if (reader["Uname"] != DBNull.Value)
                        entity.Uname = reader["Uname"].ToString();
                    if (reader["saler"] != DBNull.Value)
                        entity.saler = reader["saler"].ToString();
                    if (reader["dathang"] != DBNull.Value)
                        entity.dathang = reader["dathang"].ToString();
                    if (reader["anhsanpham"] != DBNull.Value)
                        entity.anhsanpham = reader["anhsanpham"].ToString();
                    if (reader["OrderType"] != DBNull.Value)
                        entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                    if (reader["IsCheckNotiPrice"] != DBNull.Value)
                        entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                    else
                        entity.IsCheckNotiPrice = false;
                    int totalSmallPackages = 0;
                    if (reader["totalSmallPackages"] != DBNull.Value)
                        totalSmallPackages = reader["totalSmallPackages"].ToString().ToInt(0);
                    if (totalSmallPackages > 0)
                    {
                        entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                    }
                    else
                    {
                        entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                    }
                    //var smallpackage = SmallPackageController.GetByMainOrderID(MainOrderID);
                    //if (smallpackage.Count > 0)
                    //{
                    //    entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                    //}
                    //else
                    //{
                    //    entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                    //}

                    StringBuilder htmlmadonhang = new StringBuilder();

                    #region Cách cũ
                    //string htmlmadonhang = "";
                    //if (orderType != 3)
                    //{
                    //    var ordershopcodes = OrderShopCodeController.GetByMainOrderID(MainOrderID);
                    //    if (ordershopcodes.Count > 0)
                    //    {
                    //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                    //        foreach (var code in ordershopcodes)
                    //        {
                    //            var sps = SmallPackageController.GetByOrderShopCodeID(code.ID);
                    //            if (sps.Count > 0)
                    //            {
                    //                for (int j = 0; j < sps.Count; j++)
                    //                {
                    //                    var item1 = sps[j];
                    //                    if (j == 0)
                    //                    {
                    //                        htmlmadonhang += "<tr>";
                    //                        htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code.OrderShopCode + "</td>";
                    //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                    //                        htmlmadonhang += "</tr>";
                    //                    }
                    //                    else
                    //                    {
                    //                        htmlmadonhang += "<tr>";
                    //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                    //                        htmlmadonhang += "</tr>";
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                htmlmadonhang += "<tr>";
                    //                htmlmadonhang += "<td >" + code.OrderShopCode + "</td>";
                    //                htmlmadonhang += "<td></td>";
                    //                htmlmadonhang += "</tr>";
                    //            }
                    //        }
                    //        htmlmadonhang += "</table>";
                    //    }
                    //}
                    //else
                    //{
                    //    string madonhang = "";
                    //    if (reader["MainOrderCode"] != DBNull.Value)
                    //        madonhang = reader["MainOrderCode"].ToString();
                    //    if (!string.IsNullOrEmpty(madonhang))
                    //    {
                    //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                    //        string[] codes = madonhang.Split(';');
                    //        if (codes.Length - 1 > 0)
                    //        {
                    //            for (int i = 0; i < codes.Length - 1; i++)
                    //            {

                    //                string code = codes[i];
                    //                var sps = SmallPackageController.GetByOrderWebCode(code);
                    //                if (sps.Count > 0)
                    //                {
                    //                    for (int j = 0; j < sps.Count; j++)
                    //                    {
                    //                        var item = sps[j];
                    //                        if (j == 0)
                    //                        {
                    //                            htmlmadonhang += "<tr>";
                    //                            htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code + "</td>";
                    //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                    //                            htmlmadonhang += "</tr>";
                    //                        }
                    //                        else
                    //                        {
                    //                            htmlmadonhang += "<tr>";
                    //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                    //                            htmlmadonhang += "</tr>";
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    htmlmadonhang += "<tr>";
                    //                    htmlmadonhang += "<td >" + code + "</td>";
                    //                    htmlmadonhang += "<td></td>";
                    //                    htmlmadonhang += "</tr>";
                    //                }
                    //            }
                    //        }
                    //        htmlmadonhang += "</table>";
                    //    }
                    //}
                    #endregion
                    #region Cách mới 1
                    //if (orderType != 3)
                    //{
                    //    var ordershopcodes = OrderShopCodeController.GetbyMainorderId(MainOrderID);
                    //    if (ordershopcodes.Count > 0)
                    //    {
                    //        htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                    //        foreach (var code in ordershopcodes)
                    //        {
                    //            var sps = code.listSmallPackages;
                    //            if (sps.Count > 0)
                    //            {
                    //                int i = 0;
                    //                foreach (var item in sps)
                    //                {
                    //                    if (i == 0)
                    //                    {
                    //                        htmlmadonhang.Append("<tr>");
                    //                        htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code.oShopcode + "</td>");
                    //                        htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                    //                        htmlmadonhang.Append("</tr>");
                    //                    }
                    //                    else
                    //                    {
                    //                        htmlmadonhang.Append("<tr>");
                    //                        htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                    //                        htmlmadonhang.Append("</tr>");
                    //                    }
                    //                    i++;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                htmlmadonhang.Append("<tr>");
                    //                htmlmadonhang.Append("<td >" + code.oShopcode + "</td>");
                    //                htmlmadonhang.Append("<td></td>");
                    //                htmlmadonhang.Append("</tr>");
                    //            }
                    //        }
                    //        htmlmadonhang.Append("</table>");
                    //    }
                    //}
                    //else
                    //{
                    //    string madonhang = "";
                    //    if (reader["MainOrderCode"] != DBNull.Value)
                    //        madonhang = reader["MainOrderCode"].ToString();
                    //    if (!string.IsNullOrEmpty(madonhang))
                    //    {
                    //        htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                    //        string[] codes = madonhang.Split(';');
                    //        if (codes.Length - 1 > 0)
                    //        {
                    //            for (int i = 0; i < codes.Length - 1; i++)
                    //            {

                    //                string code = codes[i];
                    //                var sps = SmallPackageController.GetByOrderWebCode(code);
                    //                if (sps.Count > 0)
                    //                {
                    //                    for (int j = 0; j < sps.Count; j++)
                    //                    {
                    //                        var item = sps[j];
                    //                        if (j == 0)
                    //                        {
                    //                            htmlmadonhang.Append("<tr>");
                    //                            htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                    //                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                    //                            htmlmadonhang.Append("</tr>");
                    //                        }
                    //                        else
                    //                        {
                    //                            htmlmadonhang.Append("<tr>");
                    //                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                    //                            htmlmadonhang.Append("</tr>");
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    htmlmadonhang.Append("<tr>");
                    //                    htmlmadonhang.Append("<td >" + code + "</td>");
                    //                    htmlmadonhang.Append("<td></td>");
                    //                    htmlmadonhang.Append("</tr>");
                    //                }
                    //            }
                    //        }
                    //        htmlmadonhang.Append("</table>");
                    //    }
                    //}
                    #endregion
                    #region Cách mới 2
                    if (orderType != 3)
                    {
                        string html = "";
                        html = OrderShopCodeController.GetbyMainorderIdRetStr(MainOrderID);
                        htmlmadonhang.Append(html);
                    }
                    else
                    {
                        string madonhang = "";
                        if (reader["MainOrderCode"] != DBNull.Value)
                            madonhang = reader["MainOrderCode"].ToString();
                        if (!string.IsNullOrEmpty(madonhang))
                        {
                            htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                            string[] codes = madonhang.Split(';');
                            if (codes.Length - 1 > 0)
                            {
                                for (int i = 0; i < codes.Length - 1; i++)
                                {

                                    string code = codes[i];
                                    var sps = SmallPackageController.GetByOrderWebCode(code);
                                    if (sps.Count > 0)
                                    {
                                        for (int j = 0; j < sps.Count; j++)
                                        {
                                            var item = sps[j];
                                            if (j == 0)
                                            {
                                                htmlmadonhang.Append("<tr>");
                                                htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                                                htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                htmlmadonhang.Append("</tr>");
                                            }
                                            else
                                            {
                                                htmlmadonhang.Append("<tr>");
                                                htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                htmlmadonhang.Append("</tr>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        htmlmadonhang.Append("<tr>");
                                        htmlmadonhang.Append("<td >" + code + "</td>");
                                        htmlmadonhang.Append("<td></td>");
                                        htmlmadonhang.Append("</tr>");
                                    }
                                }
                            }
                            htmlmadonhang.Append("</table>");
                        }
                    }
                    #endregion
                    entity.MainOrderCode = htmlmadonhang.ToString();                    
                    list.Add(entity);
                }
                reader.Close();
            }
            return list;
        }
        public static List<OrderGetSQL> GetByUserInSQLHelperWithFilter(int RoleID, int OrderType, int StaffID,
            string searchtext, int Type, string fd, string td, double priceFrom, double priceTo,
            bool isNotCode)
        {
            var list = new List<OrderGetSQL>();
            if (RoleID != 1)
            {
                var sql = @"SELECT  mo.ID, mo.TotalPriceVND, mo.Deposit, mo.CreatedDate, mo.DepostiDate, mo.Status, mo.OrderType, mo.IsCheckNotiPrice, ";
                sql += " CASE mo.Status WHEN 0 THEN N'<span class=\"bg-red\">Chờ đặt cọc</span>' ";
                sql += "                WHEN 1 THEN N'<span class=\"bg-black\">Hủy đơn hàng</span>' ";
                sql += "WHEN 2 THEN N'<span class=\"bg-bronze\">Khách đã đặt cọc</span>' ";
                sql += "WHEN 3 THEN N'<span class=\"bg-green\">Chờ duyệt đơn</span>'";
                sql += "WHEN 4 THEN N'<span class=\"bg-green\">Đã duyệt đơn</span>' ";
                sql += "WHEN 5 THEN N'<span class=\"bg-green\">Đã mua hàng</span>' ";
                sql += "WHEN 6 THEN N'<span class=\"bg-green\">Đã về kho TQ</span>' ";
                sql += "WHEN 7 THEN N'<span class=\"bg-orange\">Đã về kho VN</span>'";
                sql += "WHEN 8 THEN N'<span class=\"bg-yellow\">Chờ thanh toán</span>' ";
                sql += "WHEN 9 THEN N'<span class=\"bg-blue\">Khách đã thanh toán</span>' ";
                sql += "ELSE N'<span class=\"bg-blue\">Đã hoàn thành</span>' ";
                sql += "        END AS statusstring, mo.DathangID, ";
                sql += " mo.SalerID, mo.OrderTransactionCode, mo.OrderTransactionCode2, mo.OrderTransactionCode3, mo.OrderTransactionCode4, ";
                sql += " mo.OrderTransactionCode5, u.Username AS Uname, s.Username AS saler, d.Username AS dathang, sm.totalSmallPackages, sm1.totalSmallPackagesWithSearchText, ofi.totalOrderSearch, ";
                sql += " CASE WHEN o.anhsanpham IS NULL THEN '' ELSE '<img alt=\"\" src=\"' + o.anhsanpham + '\" width=\"100%\">' END AS anhsanpham";

                sql += " FROM    dbo.tbl_MainOder AS mo LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS u ON mo.UID = u.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS s ON mo.SalerID = s.ID LEFT OUTER JOIN";
                sql += " dbo.tbl_Account AS d ON mo.DathangID = d.ID LEFT OUTER JOIN";
                //sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM      dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID";
                //if (!string.IsNullOrEmpty(searchtext))
                //{
                //    if (Type == 1)
                //    {
                //        //sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%14%') sm1 ON sm1.MainOrderID = mo.ID LEFT OUTER JOIN";                        
                //        sql += "(SELECT  MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%" + searchtext + "%') ofi ON ofi.MainOrderID = mo.ID LEFT OUTER JOIN";
                //        sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID LEFT OUTER JOIN";
                //        sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
                //    }
                //    else if (Type == 2)
                //    {
                //        sql += "(SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%" + searchtext + "%') sm1 ON sm1.MainOrderID = mo.ID LEFT OUTER JOIN";
                //        //sql += "(SELECT  MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%a%') ofi ON ofi.MainOrderID = mo.ID LEFT OUTER JOIN";
                //        sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID LEFT OUTER JOIN";
                //sql += " (SELECT  image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";
                //    }
                //}
                sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackagesWithSearchText FROM tbl_smallpackage where OrderTransactionCode like N'%" + searchtext + "%') sm1 ON sm1.MainOrderID = mo.ID and totalSmallPackagesWithSearchText = 1 LEFT OUTER JOIN";
                sql += " (SELECT MainOrderID, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalOrderSearch FROM tbl_Order where title_origin like N'%" + searchtext + "%') ofi ON ofi.MainOrderID = mo.ID and totalOrderSearch = 1 LEFT OUTER JOIN";
                sql += " (SELECT MainOrderID, OrderTransactionCode, ROW_NUMBER() OVER(PARTITION BY MainOrderID ORDER BY(SELECT NULL)) AS totalSmallPackages FROM tbl_smallpackage) sm ON sm.MainOrderID = mo.ID and totalSmallPackages=1 LEFT OUTER JOIN";
                sql += " (SELECT image_origin as anhsanpham, MainOrderID, ROW_NUMBER() OVER (PARTITION BY MainOrderID ORDER BY (SELECT NULL)) AS RowNum FROM tbl_Order) o ON o.MainOrderID = mo.ID And RowNum = 1";

                sql += "        Where UID > 0 ";
                sql += "    AND mo.OrderType  = " + OrderType + "";
                if (!string.IsNullOrEmpty(searchtext))
                {
                    if (Type == 3)
                    {
                        sql += "  AND mo.OrderTransactionCode like N'%" + OrderType + "%'";
                    }
                }
                if (RoleID == 3)
                {
                    sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
                }
                else if (RoleID == 4)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status < 7";
                }
                else if (RoleID == 5)
                {
                    sql += "    AND mo.Status >= 5 and mo.Status <= 7";
                }
                else if (RoleID == 6)
                {
                    sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
                }
                else if (RoleID == 8)
                {
                    sql += "    AND mo.Status >= 9 and mo.Status < 10";
                }
                else if (RoleID == 7)
                {
                    sql += "    AND mo.Status >= 2";
                }
                if (!string.IsNullOrEmpty(fd))
                {
                    var df = Convert.ToDateTime(fd).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate >= CONVERT(VARCHAR(24),'" + df + "',113)";
                }
                if (!string.IsNullOrEmpty(td))
                {
                    var dt = Convert.ToDateTime(td).Date.ToString("yyyy-MM-dd HH:mm:ss");
                    sql += " AND mo.CreatedDate <= CONVERT(VARCHAR(24),'" + dt + "',113)";
                }
                if (priceFrom > 0)
                {
                    sql += " AND CAST(mo.TotalPriceVND AS float)  >= " + priceFrom;
                }
                if (priceTo > 0)
                {
                    sql += " AND CAST(mo.TotalPriceVND AS float)  <= " + priceTo;
                }
                if (isNotCode == true)
                {
                    sql += " AND totalSmallPackages is null";
                }
                sql += " ORDER BY mo.ID DESC";
                //sql += " ORDER BY mo.ID DESC OFFSET (" + page + " * " + maxrows + ") ROWS FETCH NEXT " + maxrows + " ROWS ONLY";
                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(searchtext))
                    {
                        int totalOrderSearch = 0;
                        if (reader["totalOrderSearch"] != DBNull.Value)
                            totalOrderSearch = reader["totalOrderSearch"].ToString().ToInt(0);

                        int totalSmallPackagesWithSearchText = 0;
                        if (reader["totalSmallPackagesWithSearchText"] != DBNull.Value)
                            totalSmallPackagesWithSearchText = reader["totalSmallPackagesWithSearchText"].ToString().ToInt(0);

                        int totalSmallPackages = 0;
                        if (reader["totalSmallPackages"] != DBNull.Value)
                            totalSmallPackages = reader["totalSmallPackages"].ToString().ToInt(0);
                        if (Type == 1)
                        {
                            if (totalOrderSearch > 0)
                            {
                                int MainOrderID = reader["ID"].ToString().ToInt(0);
                                var entity = new OrderGetSQL();
                                if (reader["ID"] != DBNull.Value)
                                    entity.ID = MainOrderID;
                                if (reader["TotalPriceVND"] != DBNull.Value)
                                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                                if (reader["Deposit"] != DBNull.Value)
                                    entity.Deposit = reader["Deposit"].ToString();
                                if (reader["CreatedDate"] != DBNull.Value)
                                    entity.CreatedDate = reader["CreatedDate"].ToString();

                                if(reader["DepostiDate"] != DBNull.Value)
                                    entity.DepostiDate = reader["DepostiDate"].ToString();
                                if (reader["Status"] != DBNull.Value)
                                {
                                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                                }
                                if (reader["statusstring"] != DBNull.Value)
                                {
                                    entity.statusstring = reader["statusstring"].ToString();
                                }
                                if (reader["OrderTransactionCode"] != DBNull.Value)
                                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                                if (reader["OrderTransactionCode2"] != DBNull.Value)
                                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                                if (reader["OrderTransactionCode3"] != DBNull.Value)
                                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                                if (reader["OrderTransactionCode4"] != DBNull.Value)
                                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                                if (reader["OrderTransactionCode5"] != DBNull.Value)
                                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                                if (reader["Uname"] != DBNull.Value)
                                    entity.Uname = reader["Uname"].ToString();
                                if (reader["saler"] != DBNull.Value)
                                    entity.saler = reader["saler"].ToString();
                                if (reader["dathang"] != DBNull.Value)
                                    entity.dathang = reader["dathang"].ToString();
                                if (reader["anhsanpham"] != DBNull.Value)
                                    entity.anhsanpham = reader["anhsanpham"].ToString();
                                if (reader["OrderType"] != DBNull.Value)
                                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                                else
                                    entity.IsCheckNotiPrice = false;

                                if (totalSmallPackages > 0)
                                {
                                    entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                                }
                                else
                                {
                                    entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                                }
                                #region Cách cũ
                                //string htmlmadonhang = "";
                                //int rowspan = 0;
                                //if (OrderType != 3)
                                //{
                                //    var ordershopcodes = OrderShopCodeController.GetByMainOrderID(MainOrderID);
                                //    if (ordershopcodes.Count > 0)
                                //    {
                                //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                                //        foreach (var code in ordershopcodes)
                                //        {
                                //            var sps = SmallPackageController.GetByOrderShopCodeID(code.ID);
                                //            if (sps.Count > 0)
                                //            {
                                //                for (int j = 0; j < sps.Count; j++)
                                //                {
                                //                    var item1 = sps[j];
                                //                    if (j == 0)
                                //                    {
                                //                        htmlmadonhang += "<tr>";
                                //                        htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code.OrderShopCode + "</td>";
                                //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                                //                        htmlmadonhang += "</tr>";
                                //                    }
                                //                    else
                                //                    {
                                //                        htmlmadonhang += "<tr>";
                                //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                                //                        htmlmadonhang += "</tr>";
                                //                    }
                                //                }
                                //            }
                                //            else
                                //            {
                                //                htmlmadonhang += "<tr>";
                                //                htmlmadonhang += "<td >" + code.OrderShopCode + "</td>";
                                //                htmlmadonhang += "<td></td>";
                                //                htmlmadonhang += "</tr>";
                                //            }
                                //        }
                                //        htmlmadonhang += "</table>";
                                //    }
                                //}
                                //else
                                //{
                                //    string madonhang = "";
                                //    if (reader["MainOrderCode"] != DBNull.Value)
                                //        madonhang = reader["MainOrderCode"].ToString();
                                //    if (!string.IsNullOrEmpty(madonhang))
                                //    {
                                //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                                //        string[] codes = madonhang.Split(';');
                                //        if (codes.Length - 1 > 0)
                                //        {
                                //            for (int i = 0; i < codes.Length - 1; i++)
                                //            {

                                //                string code = codes[i];
                                //                var sps = SmallPackageController.GetByOrderWebCode(code);
                                //                if (sps.Count > 0)
                                //                {
                                //                    for (int j = 0; j < sps.Count; j++)
                                //                    {
                                //                        var item = sps[j];
                                //                        if (j == 0)
                                //                        {
                                //                            htmlmadonhang += "<tr>";
                                //                            htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code + "</td>";
                                //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                                //                            htmlmadonhang += "</tr>";
                                //                        }
                                //                        else
                                //                        {
                                //                            htmlmadonhang += "<tr>";
                                //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                                //                            htmlmadonhang += "</tr>";
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    htmlmadonhang += "<tr>";
                                //                    htmlmadonhang += "<td >" + code + "</td>";
                                //                    htmlmadonhang += "<td></td>";
                                //                    htmlmadonhang += "</tr>";
                                //                }
                                //            }
                                //        }
                                //        htmlmadonhang += "</table>";
                                //    }
                                //}
                                #endregion
                                #region Cách mới
                                StringBuilder htmlmadonhang = new StringBuilder();
                                if (OrderType != 3)
                                {
                                    string html = "";
                                    html = OrderShopCodeController.GetbyMainorderIdRetStr(MainOrderID);
                                    htmlmadonhang.Append(html);
                                }
                                else
                                {
                                    string madonhang = "";
                                    if (reader["MainOrderCode"] != DBNull.Value)
                                        madonhang = reader["MainOrderCode"].ToString();
                                    if (!string.IsNullOrEmpty(madonhang))
                                    {
                                        htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                                        string[] codes = madonhang.Split(';');
                                        if (codes.Length - 1 > 0)
                                        {
                                            for (int i = 0; i < codes.Length - 1; i++)
                                            {

                                                string code = codes[i];
                                                var sps = SmallPackageController.GetByOrderWebCode(code);
                                                if (sps.Count > 0)
                                                {
                                                    for (int j = 0; j < sps.Count; j++)
                                                    {
                                                        var item = sps[j];
                                                        if (j == 0)
                                                        {
                                                            htmlmadonhang.Append("<tr>");
                                                            htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                                                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                            htmlmadonhang.Append("</tr>");
                                                        }
                                                        else
                                                        {
                                                            htmlmadonhang.Append("<tr>");
                                                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                            htmlmadonhang.Append("</tr>");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    htmlmadonhang.Append("<tr>");
                                                    htmlmadonhang.Append("<td >" + code + "</td>");
                                                    htmlmadonhang.Append("<td></td>");
                                                    htmlmadonhang.Append("</tr>");
                                                }
                                            }
                                        }
                                        htmlmadonhang.Append("</table>");
                                    }
                                }
                                #endregion
                                entity.MainOrderCode = htmlmadonhang.ToString();
                                list.Add(entity);
                            }
                        }
                        else if (Type == 2)
                        {
                            if (totalSmallPackagesWithSearchText > 0)
                            {
                                int MainOrderID = reader["ID"].ToString().ToInt(0);
                                var entity = new OrderGetSQL();
                                if (reader["ID"] != DBNull.Value)
                                    entity.ID = MainOrderID;
                                if (reader["TotalPriceVND"] != DBNull.Value)
                                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                                if (reader["Deposit"] != DBNull.Value)
                                    entity.Deposit = reader["Deposit"].ToString();
                                if (reader["CreatedDate"] != DBNull.Value)
                                    entity.CreatedDate = reader["CreatedDate"].ToString();
                                if (reader["Status"] != DBNull.Value)
                                {
                                    entity.Status = Convert.ToInt32(reader["Status"].ToString());
                                }
                                if (reader["statusstring"] != DBNull.Value)
                                {
                                    entity.statusstring = reader["statusstring"].ToString();
                                }
                                if (reader["OrderTransactionCode"] != DBNull.Value)
                                    entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                                if (reader["OrderTransactionCode2"] != DBNull.Value)
                                    entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                                if (reader["OrderTransactionCode3"] != DBNull.Value)
                                    entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                                if (reader["OrderTransactionCode4"] != DBNull.Value)
                                    entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                                if (reader["OrderTransactionCode5"] != DBNull.Value)
                                    entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                                if (reader["Uname"] != DBNull.Value)
                                    entity.Uname = reader["Uname"].ToString();
                                if (reader["saler"] != DBNull.Value)
                                    entity.saler = reader["saler"].ToString();
                                if (reader["dathang"] != DBNull.Value)
                                    entity.dathang = reader["dathang"].ToString();
                                if (reader["anhsanpham"] != DBNull.Value)
                                    entity.anhsanpham = reader["anhsanpham"].ToString();
                                if (reader["OrderType"] != DBNull.Value)
                                    entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                                if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                    entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                                else
                                    entity.IsCheckNotiPrice = false;
                                if (totalSmallPackages > 0)
                                {
                                    entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                                }
                                else
                                {
                                    entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                                }
                                #region Cách cũ
                                //string htmlmadonhang = "";
                                //int rowspan = 0;
                                //if (OrderType != 3)
                                //{
                                //    var ordershopcodes = OrderShopCodeController.GetByMainOrderID(MainOrderID);
                                //    if (ordershopcodes.Count > 0)
                                //    {
                                //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                                //        foreach (var code in ordershopcodes)
                                //        {
                                //            var sps = SmallPackageController.GetByOrderShopCodeID(code.ID);
                                //            if (sps.Count > 0)
                                //            {
                                //                for (int j = 0; j < sps.Count; j++)
                                //                {
                                //                    var item1 = sps[j];
                                //                    if (j == 0)
                                //                    {
                                //                        htmlmadonhang += "<tr>";
                                //                        htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code.OrderShopCode + "</td>";
                                //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                                //                        htmlmadonhang += "</tr>";
                                //                    }
                                //                    else
                                //                    {
                                //                        htmlmadonhang += "<tr>";
                                //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                                //                        htmlmadonhang += "</tr>";
                                //                    }
                                //                }
                                //            }
                                //            else
                                //            {
                                //                htmlmadonhang += "<tr>";
                                //                htmlmadonhang += "<td >" + code.OrderShopCode + "</td>";
                                //                htmlmadonhang += "<td></td>";
                                //                htmlmadonhang += "</tr>";
                                //            }
                                //        }
                                //        htmlmadonhang += "</table>";
                                //    }
                                //}
                                //else
                                //{
                                //    string madonhang = "";
                                //    if (reader["MainOrderCode"] != DBNull.Value)
                                //        madonhang = reader["MainOrderCode"].ToString();
                                //    if (!string.IsNullOrEmpty(madonhang))
                                //    {
                                //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                                //        string[] codes = madonhang.Split(';');
                                //        if (codes.Length - 1 > 0)
                                //        {
                                //            for (int i = 0; i < codes.Length - 1; i++)
                                //            {

                                //                string code = codes[i];
                                //                var sps = SmallPackageController.GetByOrderWebCode(code);
                                //                if (sps.Count > 0)
                                //                {
                                //                    for (int j = 0; j < sps.Count; j++)
                                //                    {
                                //                        var item = sps[j];
                                //                        if (j == 0)
                                //                        {
                                //                            htmlmadonhang += "<tr>";
                                //                            htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code + "</td>";
                                //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                                //                            htmlmadonhang += "</tr>";
                                //                        }
                                //                        else
                                //                        {
                                //                            htmlmadonhang += "<tr>";
                                //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                                //                            htmlmadonhang += "</tr>";
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    htmlmadonhang += "<tr>";
                                //                    htmlmadonhang += "<td >" + code + "</td>";
                                //                    htmlmadonhang += "<td></td>";
                                //                    htmlmadonhang += "</tr>";
                                //                }
                                //            }
                                //        }
                                //        htmlmadonhang += "</table>";
                                //    }
                                //}
                                #endregion
                                #region Cách mới
                                StringBuilder htmlmadonhang = new StringBuilder();
                                if (OrderType != 3)
                                {
                                    string html = "";
                                    html = OrderShopCodeController.GetbyMainorderIdRetStr(MainOrderID);
                                    htmlmadonhang.Append(html);
                                }
                                else
                                {
                                    string madonhang = "";
                                    if (reader["MainOrderCode"] != DBNull.Value)
                                        madonhang = reader["MainOrderCode"].ToString();
                                    if (!string.IsNullOrEmpty(madonhang))
                                    {
                                        htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                                        string[] codes = madonhang.Split(';');
                                        if (codes.Length - 1 > 0)
                                        {
                                            for (int i = 0; i < codes.Length - 1; i++)
                                            {

                                                string code = codes[i];
                                                var sps = SmallPackageController.GetByOrderWebCode(code);
                                                if (sps.Count > 0)
                                                {
                                                    for (int j = 0; j < sps.Count; j++)
                                                    {
                                                        var item = sps[j];
                                                        if (j == 0)
                                                        {
                                                            htmlmadonhang.Append("<tr>");
                                                            htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                                                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                            htmlmadonhang.Append("</tr>");
                                                        }
                                                        else
                                                        {
                                                            htmlmadonhang.Append("<tr>");
                                                            htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                            htmlmadonhang.Append("</tr>");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    htmlmadonhang.Append("<tr>");
                                                    htmlmadonhang.Append("<td >" + code + "</td>");
                                                    htmlmadonhang.Append("<td></td>");
                                                    htmlmadonhang.Append("</tr>");
                                                }
                                            }
                                        }
                                        htmlmadonhang.Append("</table>");
                                    }
                                }
                                #endregion
                                entity.MainOrderCode = htmlmadonhang.ToString();
                                list.Add(entity);
                            }
                        }
                        else
                        {
                            int MainOrderID = reader["ID"].ToString().ToInt(0);
                            var entity = new OrderGetSQL();
                            if (reader["ID"] != DBNull.Value)
                                entity.ID = MainOrderID;
                            if (reader["TotalPriceVND"] != DBNull.Value)
                                entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                            if (reader["Deposit"] != DBNull.Value)
                                entity.Deposit = reader["Deposit"].ToString();
                            if (reader["CreatedDate"] != DBNull.Value)
                                entity.CreatedDate = reader["CreatedDate"].ToString();
                            if (reader["Status"] != DBNull.Value)
                            {
                                entity.Status = Convert.ToInt32(reader["Status"].ToString());
                            }
                            if (reader["statusstring"] != DBNull.Value)
                            {
                                entity.statusstring = reader["statusstring"].ToString();
                            }
                            if (reader["OrderTransactionCode"] != DBNull.Value)
                                entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                            if (reader["OrderTransactionCode2"] != DBNull.Value)
                                entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                            if (reader["OrderTransactionCode3"] != DBNull.Value)
                                entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                            if (reader["OrderTransactionCode4"] != DBNull.Value)
                                entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                            if (reader["OrderTransactionCode5"] != DBNull.Value)
                                entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                            if (reader["Uname"] != DBNull.Value)
                                entity.Uname = reader["Uname"].ToString();
                            if (reader["saler"] != DBNull.Value)
                                entity.saler = reader["saler"].ToString();
                            if (reader["dathang"] != DBNull.Value)
                                entity.dathang = reader["dathang"].ToString();
                            if (reader["anhsanpham"] != DBNull.Value)
                                entity.anhsanpham = reader["anhsanpham"].ToString();
                            if (reader["OrderType"] != DBNull.Value)
                                entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                            if (reader["IsCheckNotiPrice"] != DBNull.Value)
                                entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                            else
                                entity.IsCheckNotiPrice = false;
                            if (totalSmallPackages > 0)
                            {
                                entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                            }
                            else
                            {
                                entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                            }
                            #region Cách cũ
                            //string htmlmadonhang = "";
                            //int rowspan = 0;
                            //if (OrderType != 3)
                            //{
                            //    var ordershopcodes = OrderShopCodeController.GetByMainOrderID(MainOrderID);
                            //    if (ordershopcodes.Count > 0)
                            //    {
                            //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                            //        foreach (var code in ordershopcodes)
                            //        {
                            //            var sps = SmallPackageController.GetByOrderShopCodeID(code.ID);
                            //            if (sps.Count > 0)
                            //            {
                            //                for (int j = 0; j < sps.Count; j++)
                            //                {
                            //                    var item1 = sps[j];
                            //                    if (j == 0)
                            //                    {
                            //                        htmlmadonhang += "<tr>";
                            //                        htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code.OrderShopCode + "</td>";
                            //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                            //                        htmlmadonhang += "</tr>";
                            //                    }
                            //                    else
                            //                    {
                            //                        htmlmadonhang += "<tr>";
                            //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                            //                        htmlmadonhang += "</tr>";
                            //                    }
                            //                }
                            //            }
                            //            else
                            //            {
                            //                htmlmadonhang += "<tr>";
                            //                htmlmadonhang += "<td >" + code.OrderShopCode + "</td>";
                            //                htmlmadonhang += "<td></td>";
                            //                htmlmadonhang += "</tr>";
                            //            }
                            //        }
                            //        htmlmadonhang += "</table>";
                            //    }
                            //}
                            //else
                            //{
                            //    string madonhang = "";
                            //    if (reader["MainOrderCode"] != DBNull.Value)
                            //        madonhang = reader["MainOrderCode"].ToString();
                            //    if (!string.IsNullOrEmpty(madonhang))
                            //    {
                            //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                            //        string[] codes = madonhang.Split(';');
                            //        if (codes.Length - 1 > 0)
                            //        {
                            //            for (int i = 0; i < codes.Length - 1; i++)
                            //            {

                            //                string code = codes[i];
                            //                var sps = SmallPackageController.GetByOrderWebCode(code);
                            //                if (sps.Count > 0)
                            //                {
                            //                    for (int j = 0; j < sps.Count; j++)
                            //                    {
                            //                        var item = sps[j];
                            //                        if (j == 0)
                            //                        {
                            //                            htmlmadonhang += "<tr>";
                            //                            htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code + "</td>";
                            //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                            //                            htmlmadonhang += "</tr>";
                            //                        }
                            //                        else
                            //                        {
                            //                            htmlmadonhang += "<tr>";
                            //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                            //                            htmlmadonhang += "</tr>";
                            //                        }
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    htmlmadonhang += "<tr>";
                            //                    htmlmadonhang += "<td >" + code + "</td>";
                            //                    htmlmadonhang += "<td></td>";
                            //                    htmlmadonhang += "</tr>";
                            //                }
                            //            }
                            //        }
                            //        htmlmadonhang += "</table>";
                            //    }
                            //}
                            #endregion
                            #region Cách mới
                            StringBuilder htmlmadonhang = new StringBuilder();
                            if (OrderType != 3)
                            {
                                string html = "";
                                html = OrderShopCodeController.GetbyMainorderIdRetStr(MainOrderID);
                                htmlmadonhang.Append(html);
                            }
                            else
                            {
                                string madonhang = "";
                                if (reader["MainOrderCode"] != DBNull.Value)
                                    madonhang = reader["MainOrderCode"].ToString();
                                if (!string.IsNullOrEmpty(madonhang))
                                {
                                    htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                                    string[] codes = madonhang.Split(';');
                                    if (codes.Length - 1 > 0)
                                    {
                                        for (int i = 0; i < codes.Length - 1; i++)
                                        {

                                            string code = codes[i];
                                            var sps = SmallPackageController.GetByOrderWebCode(code);
                                            if (sps.Count > 0)
                                            {
                                                for (int j = 0; j < sps.Count; j++)
                                                {
                                                    var item = sps[j];
                                                    if (j == 0)
                                                    {
                                                        htmlmadonhang.Append("<tr>");
                                                        htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                                                        htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                        htmlmadonhang.Append("</tr>");
                                                    }
                                                    else
                                                    {
                                                        htmlmadonhang.Append("<tr>");
                                                        htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                        htmlmadonhang.Append("</tr>");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                htmlmadonhang.Append("<tr>");
                                                htmlmadonhang.Append("<td >" + code + "</td>");
                                                htmlmadonhang.Append("<td></td>");
                                                htmlmadonhang.Append("</tr>");
                                            }
                                        }
                                    }
                                    htmlmadonhang.Append("</table>");
                                }
                            }
                            #endregion
                            entity.MainOrderCode = htmlmadonhang.ToString();
                            list.Add(entity);
                        }
                    }
                    else
                    {
                        int MainOrderID = reader["ID"].ToString().ToInt(0);
                        int totalSmallPackages = 0;
                        if (reader["totalSmallPackages"] != DBNull.Value)
                            totalSmallPackages = reader["totalSmallPackages"].ToString().ToInt(0);
                        var entity = new OrderGetSQL();
                        if (reader["ID"] != DBNull.Value)
                            entity.ID = MainOrderID;
                        if (reader["TotalPriceVND"] != DBNull.Value)
                            entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                        if (reader["Deposit"] != DBNull.Value)
                            entity.Deposit = reader["Deposit"].ToString();
                        if (reader["CreatedDate"] != DBNull.Value)
                            entity.CreatedDate = reader["CreatedDate"].ToString();
                        if (reader["Status"] != DBNull.Value)
                        {
                            entity.Status = Convert.ToInt32(reader["Status"].ToString());
                        }
                        if (reader["statusstring"] != DBNull.Value)
                        {
                            entity.statusstring = reader["statusstring"].ToString();
                        }
                        if (reader["OrderTransactionCode"] != DBNull.Value)
                            entity.OrderTransactionCode = reader["OrderTransactionCode"].ToString();
                        if (reader["OrderTransactionCode2"] != DBNull.Value)
                            entity.OrderTransactionCode2 = reader["OrderTransactionCode2"].ToString();
                        if (reader["OrderTransactionCode3"] != DBNull.Value)
                            entity.OrderTransactionCode3 = reader["OrderTransactionCode3"].ToString();
                        if (reader["OrderTransactionCode4"] != DBNull.Value)
                            entity.OrderTransactionCode4 = reader["OrderTransactionCode4"].ToString();
                        if (reader["OrderTransactionCode5"] != DBNull.Value)
                            entity.OrderTransactionCode5 = reader["OrderTransactionCode5"].ToString();

                        if (reader["Uname"] != DBNull.Value)
                            entity.Uname = reader["Uname"].ToString();
                        if (reader["saler"] != DBNull.Value)
                            entity.saler = reader["saler"].ToString();
                        if (reader["dathang"] != DBNull.Value)
                            entity.dathang = reader["dathang"].ToString();
                        if (reader["anhsanpham"] != DBNull.Value)
                            entity.anhsanpham = reader["anhsanpham"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            entity.OrderType = reader["OrderType"].ToString().ToInt(1);

                        if (reader["IsCheckNotiPrice"] != DBNull.Value)
                            entity.IsCheckNotiPrice = Convert.ToBoolean(reader["IsCheckNotiPrice"]);
                        else
                            entity.IsCheckNotiPrice = false;
                        if (totalSmallPackages > 0)
                        {
                            entity.hasSmallpackage = "<span class=\"bg-blue\">Đã có</span>";
                        }
                        else
                        {
                            entity.hasSmallpackage = "<span class=\"bg-red\">Chưa có</span>";
                        }
                        #region Cách cũ
                        //string htmlmadonhang = "";
                        //int rowspan = 0;
                        //if (OrderType != 3)
                        //{
                        //    var ordershopcodes = OrderShopCodeController.GetByMainOrderID(MainOrderID);
                        //    if (ordershopcodes.Count > 0)
                        //    {
                        //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                        //        foreach (var code in ordershopcodes)
                        //        {
                        //            var sps = SmallPackageController.GetByOrderShopCodeID(code.ID);
                        //            if (sps.Count > 0)
                        //            {
                        //                for (int j = 0; j < sps.Count; j++)
                        //                {
                        //                    var item1 = sps[j];
                        //                    if (j == 0)
                        //                    {
                        //                        htmlmadonhang += "<tr>";
                        //                        htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code.OrderShopCode + "</td>";
                        //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                        //                        htmlmadonhang += "</tr>";
                        //                    }
                        //                    else
                        //                    {
                        //                        htmlmadonhang += "<tr>";
                        //                        htmlmadonhang += "<td>" + item1.OrderTransactionCode + "</td>";
                        //                        htmlmadonhang += "</tr>";
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                htmlmadonhang += "<tr>";
                        //                htmlmadonhang += "<td >" + code.OrderShopCode + "</td>";
                        //                htmlmadonhang += "<td></td>";
                        //                htmlmadonhang += "</tr>";
                        //            }
                        //        }
                        //        htmlmadonhang += "</table>";
                        //    }
                        //}
                        //else
                        //{
                        //    string madonhang = "";
                        //    if (reader["MainOrderCode"] != DBNull.Value)
                        //        madonhang = reader["MainOrderCode"].ToString();
                        //    if (!string.IsNullOrEmpty(madonhang))
                        //    {
                        //        htmlmadonhang += "<table class=\"table table-bordered table-hover\">";
                        //        string[] codes = madonhang.Split(';');
                        //        if (codes.Length - 1 > 0)
                        //        {
                        //            for (int i = 0; i < codes.Length - 1; i++)
                        //            {

                        //                string code = codes[i];
                        //                var sps = SmallPackageController.GetByOrderWebCode(code);
                        //                if (sps.Count > 0)
                        //                {
                        //                    for (int j = 0; j < sps.Count; j++)
                        //                    {
                        //                        var item = sps[j];
                        //                        if (j == 0)
                        //                        {
                        //                            htmlmadonhang += "<tr>";
                        //                            htmlmadonhang += "<td rowspan=\"" + sps.Count + "\">" + code + "</td>";
                        //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                        //                            htmlmadonhang += "</tr>";
                        //                        }
                        //                        else
                        //                        {
                        //                            htmlmadonhang += "<tr>";
                        //                            htmlmadonhang += "<td>" + item.OrderTransactionCode + "</td>";
                        //                            htmlmadonhang += "</tr>";
                        //                        }
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    htmlmadonhang += "<tr>";
                        //                    htmlmadonhang += "<td >" + code + "</td>";
                        //                    htmlmadonhang += "<td></td>";
                        //                    htmlmadonhang += "</tr>";
                        //                }
                        //            }
                        //        }
                        //        htmlmadonhang += "</table>";
                        //    }
                        //}
                        #endregion
                        #region Cách mới
                        StringBuilder htmlmadonhang = new StringBuilder();
                        if (OrderType != 3)
                        {
                            string html = "";
                            html = OrderShopCodeController.GetbyMainorderIdRetStr(MainOrderID);
                            htmlmadonhang.Append(html);
                        }
                        else
                        {
                            string madonhang = "";
                            if (reader["MainOrderCode"] != DBNull.Value)
                                madonhang = reader["MainOrderCode"].ToString();
                            if (!string.IsNullOrEmpty(madonhang))
                            {
                                htmlmadonhang.Append("<table class=\"table table-bordered table-hover\">");
                                string[] codes = madonhang.Split(';');
                                if (codes.Length - 1 > 0)
                                {
                                    for (int i = 0; i < codes.Length - 1; i++)
                                    {

                                        string code = codes[i];
                                        var sps = SmallPackageController.GetByOrderWebCode(code);
                                        if (sps.Count > 0)
                                        {
                                            for (int j = 0; j < sps.Count; j++)
                                            {
                                                var item = sps[j];
                                                if (j == 0)
                                                {
                                                    htmlmadonhang.Append("<tr>");
                                                    htmlmadonhang.Append("<td rowspan=\"" + sps.Count + "\">" + code + "</td>");
                                                    htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                    htmlmadonhang.Append("</tr>");
                                                }
                                                else
                                                {
                                                    htmlmadonhang.Append("<tr>");
                                                    htmlmadonhang.Append("<td>" + item.OrderTransactionCode + "</td>");
                                                    htmlmadonhang.Append("</tr>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            htmlmadonhang.Append("<tr>");
                                            htmlmadonhang.Append("<td >" + code + "</td>");
                                            htmlmadonhang.Append("<td></td>");
                                            htmlmadonhang.Append("</tr>");
                                        }
                                    }
                                }
                                htmlmadonhang.Append("</table>");
                            }
                        }
                        #endregion
                        entity.MainOrderCode = htmlmadonhang.ToString();
                        list.Add(entity);
                    }
                }
                reader.Close();
            }
            return list;
        }

        public static List<mainorder> GetAllByUIDNotHidden_SqlHelper1(int UID, int status, string fd, string td)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.ID, mo.TotalPriceVND, mo.Deposit,mo.AmountDeposit, mo.CreatedDate, mo.Status, mo.shopname, mo.site, mo.IsGiaohang, o.anhsanpham";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where UID = " + UID + " and IsHidden = 0 ";

            if (status >= 0)
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
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();
                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<mainorder> GetAllByDathangIDWithmoreStatus_SqlHelper1(int DathangID, int status, string fd, string td)
        {
            var list = new List<mainorder>();
            var sql = @"SELECT mo.*, o.anhsanpham ";
            sql += " FROM dbo.tbl_MainOder AS mo LEFT OUTER JOIN ";
            sql += " (SELECT MainOrderID, MIN(image_origin) AS anhsanpham FROM dbo.tbl_Order GROUP BY MainOrderID) AS o ON mo.ID = o.MainOrderID ";
            sql += " where DathangID = " + DathangID + " ";

            if (status >= 0)
                sql += " AND Status >= " + status;

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
                var entity = new mainorder();
                entity.STT = i;
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["TotalPriceVND"] != DBNull.Value)
                    entity.TotalPriceVND = reader["TotalPriceVND"].ToString();
                if (reader["Deposit"] != DBNull.Value)
                    entity.Deposit = reader["Deposit"].ToString();

                double currency = 0;
                if (reader["CurrentCNYVN"] != DBNull.Value)
                    currency = Convert.ToDouble(reader["CurrentCNYVN"].ToString());
                entity.Currency = currency;

                double PriceVND = 0;
                if (reader["PriceVND"] != DBNull.Value)
                {
                    PriceVND = Convert.ToDouble(reader["PriceVND"].ToString());
                }
                entity.PriceVND = PriceVND;

                double feeBuypro = 0;
                if (reader["FeeBuyPro"] != DBNull.Value)
                    feeBuypro = Convert.ToDouble(reader["FeeBuyPro"].ToString());
                entity.FeeBuyPro = feeBuypro;

                double feeWeight = 0;
                if (reader["FeeWeight"] != DBNull.Value)
                    feeWeight = Convert.ToDouble(reader["FeeWeight"].ToString());
                entity.FeeWeight = feeWeight;

                double additionFeeForSensorProduct = 0;
                if (reader["AdditionFeeForSensorProduct"] != DBNull.Value)
                    additionFeeForSensorProduct = Convert.ToDouble(reader["AdditionFeeForSensorProduct"].ToString());
                entity.AdditionFeeForSensorProduct = additionFeeForSensorProduct;

                if (reader["AmountDeposit"] != DBNull.Value)
                    entity.AmountDeposit = reader["AmountDeposit"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt();
                if (reader["shopname"] != DBNull.Value)
                    entity.ShopName = reader["shopname"].ToString();
                if (reader["site"] != DBNull.Value)
                    entity.Site = reader["site"].ToString();
                if (reader["anhsanpham"] != DBNull.Value)
                    entity.anhsanpham = reader["anhsanpham"].ToString();
                if (reader["IsGiaohang"] != DBNull.Value)
                    entity.IsGiaohang = Convert.ToBoolean(reader["IsGiaohang"].ToString());
                else
                    entity.IsGiaohang = false;
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static tbl_MainOder GetAllByUIDAndID(int UID, int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.UID == UID && o.ID == ID).SingleOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static tbl_MainOder GetAllByID(int ID)
        {
            using (var dbe = new NHSTEntities())
            {
                var lo = dbe.tbl_MainOder.Where(o => o.ID == ID).SingleOrDefault();
                if (lo != null)
                    return lo;
                else
                    return null;
            }
        }
        public static int getOrderByRoleIDStaffID_SQL(int RoleID, int StaffID, int orderType)
        {
            int Count = 0;
            var sql = @"SELECT COUNT(*) as Total from tbl_MainOder as mo";
            sql += "        Where UID > 0 and mo.ordertype=" + orderType + " ";
            if (RoleID == 3)
            {
                sql += "    AND mo.Status >= 2 and mo.DathangID = " + StaffID + "";
            }
            else if (RoleID == 4)
            {
                sql += "    AND mo.Status >= 5 and mo.Status < 7";
            }
            else if (RoleID == 5)
            {
                sql += "    AND mo.Status >= 5 and mo.Status <= 7";
            }
            else if (RoleID == 6)
            {
                sql += "    AND mo.Status != 1 and mo.SalerID = " + StaffID + "";
            }
            else if (RoleID == 8)
            {
                sql += "    AND mo.Status >= 9 and mo.Status < 10";
            }
            else if (RoleID == 7)
            {
                sql += "    AND mo.Status >= 2";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                Count = reader["Total"].ToString().ToInt();
            }
            reader.Close();
            return Count;
        }
        public static List<MainOrderID> GetMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_order where title_origin like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public static List<MainOrderID> GetSmallPackageMainOrderIDBySearch(string search)
        {
            List<MainOrderID> ods = new List<MainOrderID>();
            var sql = @"Select MainOrderID from tbl_SmallPackage where OrderTransactionCode like N'%" + search + "%' GROUP BY MainorderID";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                MainOrderID os = new MainOrderID();
                if (reader["MainOrderID"] != DBNull.Value)
                    os.ID = reader["MainOrderID"].ToString().ToInt(0);
                ods.Add(os);
            }
            reader.Close();
            return ods;
        }
        public class MainOrderID
        {
            public int ID { get; set; }
        }
        public class mainorder
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public string AmountDeposit { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime DepositDate { get; set; }
            public string DepositDateStr { get; set; }
            public double Currency { get; set; }
            public double PriceVND { get; set; }
            public double FeeBuyPro { get; set; }
            public double FeeWeight { get; set; }
            public double AdditionFeeForSensorProduct { get; set; }
            public int Status { get; set; }
            public string ShopName { get; set; }
            public string Site { get; set; }
            public string anhsanpham { get; set; }
            public bool IsGiaohang { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public int OrderType { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }
            public string quantityPro { get; set; }
            public string MainOrderCode { get; set; }
        }
        public class OrderGetSQL
        {
            public int ID { get; set; }
            public int STT { get; set; }
            public string anhsanpham { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string TotalPriceVND { get; set; }
            public string Deposit { get; set; }
            public int UID { get; set; }
            public int Status { get; set; }
            public string CreatedDate { get; set; }
            public string DepostiDate { get; set; }
            public string statusstring { get; set; }
            public int OrderType { get; set; }
            public bool IsCheckNotiPrice { get; set; }
            public string OrderTransactionCode { get; set; }
            public string OrderTransactionCode2 { get; set; }
            public string OrderTransactionCode3 { get; set; }
            public string OrderTransactionCode4 { get; set; }
            public string OrderTransactionCode5 { get; set; }

            public string Uname { get; set; }
            public string dathang { get; set; }
            public string saler { get; set; }
            public string khotq { get; set; }
            public string khovn { get; set; }
            public string hasSmallpackage { get; set; }
            public string MainOrderCode { get; set; }
        }
        #endregion
    }
}