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
    using System.Web.Script.Serialization;
    
    public partial class Profile
    {
        public Profile()
        {
            this.ProfileAddresses = new HashSet<ProfileAddress>();
            this.ProfilePhones = new HashSet<ProfilePhone>();
        }
    
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public System.DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
          [ScriptIgnore]
        public virtual ICollection<ProfileAddress> ProfileAddresses { get; set; }
          [ScriptIgnore]
        public virtual ICollection<ProfilePhone> ProfilePhones { get; set; }
    }
}
