//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YazilimYonetimAraci
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUserProcess
    {
        public int UserProcessID { get; set; }
        public Nullable<int> UsersID { get; set; }
        public Nullable<int> ProcessID { get; set; }
        public Nullable<int> UserRolesID { get; set; }
    
        public virtual tblProcess tblProcess { get; set; }
        public virtual tblUserRoles tblUserRoles { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }
}
