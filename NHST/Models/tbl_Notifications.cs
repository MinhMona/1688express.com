//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Notifications
    {
        public int ID { get; set; }
        public Nullable<int> SenderID { get; set; }
        public string SenderUsername { get; set; }
        public Nullable<int> ReceivedID { get; set; }
        public string ReceivedUsername { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string Message { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> NotifType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
