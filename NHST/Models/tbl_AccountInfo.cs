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
    
    public partial class tbl_AccountInfo
    {
        public int ID { get; set; }
        public Nullable<int> UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhonePrefix { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public Nullable<int> Gender { get; set; }
    }
}
