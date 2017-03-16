using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;
using Newtonsoft.Json;

namespace Application.Controllers
{
    public class ContactController : Controller
    {

        DataStore db = new DataStore();
        ContactEntities contactContext = new ContactEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateEdit()
        {
            return View();
        }

        public JsonResult GetAllProfiles(string searchString = "")
        {
            try
            {
                var profiles = (from p in db.returnProfiles()
                                where (p.FirstName == searchString || searchString == "")
                                select new { p.ProfileId, p.FirstName, p.LastName, p.Email }).ToList();

                return this.Json(profiles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public JsonResult GetProfileById(int idOfSelectedContact)
        {
            try
            {
                if (idOfSelectedContact != 0)
                {
                    contactContext.Configuration.ProxyCreationEnabled = false;
                    var profile = contactContext.Profiles.Single(p => p.ProfileId == idOfSelectedContact);
                    return this.Json(profile, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return this.Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPhoneById(int idOfSelectedContact)
        {
            try
            {
                if (idOfSelectedContact != 0)
                {
                    var phoneDTO = (from profilePhone in contactContext.ProfilePhones.Where(p => p.ProfileId == idOfSelectedContact)
                                    join phone in contactContext.Phones on profilePhone.PhoneId equals phone.PhoneId
                                    select new
                                    {
                                        profilePhone.PhoneTypeId,
                                        phone.PhoneId,
                                        phone.Number
                                    }).ToList();
                    return this.Json(phoneDTO, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return this.Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetAddressById(int idOfSelectedContact)
        {
            try
            {
                if (idOfSelectedContact != 0)
                {
                    var addrressDTO = (from profileAddress in
                                           contactContext.ProfileAddresses.Where(p => p.ProfileId == idOfSelectedContact)
                                       join address in contactContext.Addresses
                                       on profileAddress.AddressId equals address.AddressId
                                       select new
                                       {
                                           profileAddress.AddressTypeId,
                                           address.AddressId,
                                           address.AddressLine1,
                                           address.AddressLine2,
                                           address.City,
                                           address.Country,
                                           address.ZipCode,
                                           address.State,
                                       }).ToList();
                    return this.Json(addrressDTO, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return this.Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult returnAddressTYpe()
        {
            contactContext.Configuration.ProxyCreationEnabled = false;
            var Addresstype = (from p in contactContext.AddressTypes select new { p.AddressTypeId, p.Name }).ToList();
            return this.Json(Addresstype, JsonRequestBehavior.AllowGet);
        }
        public JsonResult returnPhoneType()
        {
            contactContext.Configuration.ProxyCreationEnabled = false;
            var Phonnetype = (from p in contactContext.PhoneTypes select new { p.PhoneTypeId, p.Name }).ToList();
            return this.Json(Phonnetype, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult SaveProfileInformation(ProfileDTO profileDTO)
        {
            if (profileDTO.ProfileId == 0)
            {
                db.SaveProfileInformation(profileDTO);
            }
            else
            {
                db.UpdateProfileInformation(profileDTO.ProfileId, profileDTO);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpStatusCodeResult UpdateProfileInformation(int id, ProfileDTO profileDTO)
        {
            db.UpdateProfileInformation(id, profileDTO);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpDelete]
        public void DeleteProfile(int idTodelete)
        {
            try
            {
                if (idTodelete != 0)
                {
                    db.DeleteProfile(idTodelete);
                }
            }
            catch
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        public void DeletePhoneProfile(int idTodelete)
        {
            try
            {
                if (idTodelete != 0)
                {
                    db.DeletePhone(idTodelete);
                }
            }
            catch
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        public void DeleteAddressProfile(int idtodelete)
        {
            try
            {
                if (idtodelete != 0)
                {
                    db.Deleteaddress(idtodelete);
                }
            }
            catch
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}