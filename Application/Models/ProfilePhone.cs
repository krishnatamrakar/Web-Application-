//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProfilePhone
    {
        public int ProfilePhoneId { get; set; }
        public int ProfileId { get; set; }
        public int PhoneId { get; set; }
        public int PhoneTypeId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual Phone Phone { get; set; }
        public virtual PhoneType PhoneType { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
