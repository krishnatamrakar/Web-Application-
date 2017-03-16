using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Models;
using System.Web.Script.Serialization;

namespace Application
{
    public class InitializePageData
    {
        [ScriptIgnore]
        public List<AddressType> Addresstype { get; set; }
        [ScriptIgnore]
        public List<PhoneType> phonetype { get; set; }
    }

    public class ProfileDTO
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PhoneDTO> PhoneDTO { get; set; }
        public List<AddressDTO> AddressDTO { get; set; }

    }
}