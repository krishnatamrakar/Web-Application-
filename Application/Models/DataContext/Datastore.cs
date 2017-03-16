using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Models;
using System.Web.Script.Serialization;
using System.Data.Entity.Validation;
using System.Diagnostics;


namespace Application.Models
{
    public class DataStore
    {
        ContactEntities db = new ContactEntities();
        public List<AddressType> returnAddressType()
        {
            List<AddressType> addressTypeList = db.AddressTypes.ToList();

            List<AddressType> returnAddressTypeList = new List<AddressType>();
            AddressType addresstype1 = new AddressType();
            foreach (var addresstype in addressTypeList)
            {
                addresstype1.AddressTypeId = addresstype.AddressTypeId;
                addresstype1.Name = addresstype.Name;
                addresstype1.ProfileAddresses = null;
                returnAddressTypeList.Add(addresstype1);
            }

            return returnAddressTypeList;
        }
        public List<PhoneType> returnPhoneType()
        {
            List<PhoneType> phoneTypeList = db.PhoneTypes.ToList();
            List<PhoneType> returnPhoneTypeList = new List<PhoneType>();
            PhoneType phoneType1 = new PhoneType();
            foreach (var phonetype in phoneTypeList)
            {
                phoneType1.PhoneTypeId = phonetype.PhoneTypeId;
                phoneType1.Name = phonetype.Name;
                phoneType1.ProfilePhones = null;
                returnPhoneTypeList.Add(phoneType1);
            }

            return returnPhoneTypeList;
        }

        public List<Profile> returnProfiles()
        {
            List<Profile> profileList = db.Profiles.ToList();
            return profileList;
        }
        public void SaveProfileInformation(ProfileDTO profileDTO)
        {
            try
            {
                Profile profile = new Profile
                {
                    FirstName = profileDTO.FirstName,
                    LastName = profileDTO.LastName,
                    Email = profileDTO.Email,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin",
                    Updated = DateTime.Now,
                    Created = DateTime.Now
                };
                db.Profiles.Add(profile);
                db.SaveChanges();
                var profileID = db.Profiles.Max(p => p.ProfileId);

                List<PhoneDTO> phoneList = profileDTO.PhoneDTO;

                if (phoneList != null)
                {
                    foreach (var item in phoneList)
                    {
                        Phone phone = new Phone()
                        {
                            Number = item.Number,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };
                        int PhoneID = savePhone(phone);
                        ProfilePhone profilePhone = new ProfilePhone()
                        {
                            ProfileId = profileID,
                            PhoneId = PhoneID,
                            PhoneTypeId = item.PhoneTypeId != null ? item.PhoneTypeId : 1,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };

                        db.ProfilePhones.Add(profilePhone);
                        db.SaveChanges();
                    }
                }

                List<AddressDTO> addressList = profileDTO.AddressDTO;
                if (addressList != null)
                {
                    foreach (var item in addressList)
                    {
                        Address address = new Address()
                        {
                            AddressLine1 = item.AddressLine1,
                            AddressLine2 = item.AddressLine2,
                            Country = item.Country,
                            State = item.State,
                            City = item.City,
                            ZipCode = item.ZipCode,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };

                        int addressId = saveAddress(address);

                        ProfileAddress profileAddress = new ProfileAddress()
                        {
                            ProfileId = profileID,
                            AddressId = addressId,
                            AddressTypeId = item.AddressTypeId != 0 ? item.AddressTypeId : 1,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };
                        db.ProfileAddresses.Add(profileAddress);
                        db.SaveChanges();
                    }


                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw ex;
            }

        }
        public void UpdateProfileInformation(int profileID, ProfileDTO profileDTO)
        {
            var profile = db.Profiles.Find(profileID);
            if (profile != null)
            {
                profile.FirstName = profileDTO.FirstName;
                profile.LastName = profileDTO.LastName;
                profile.Email = profileDTO.Email;
                profile.UpdatedBy = "Admin";
                profile.Updated = DateTime.Now;
                db.SaveChanges();
            }

            List<PhoneDTO> phoneList = profileDTO.PhoneDTO;
            if (phoneList != null)
            {
                foreach (var item in phoneList)
                {

                    if (item.PhoneId == 0)
                    {
                        Phone phone = new Phone()
                       {
                           Number = item.Number,
                           CreatedBy = "Admin",
                           UpdatedBy = "Admin",
                           Updated = DateTime.Now,
                           Created = DateTime.Now
                       };
                        int PhoneID = savePhone(phone);
                        ProfilePhone profilePhone = new ProfilePhone()
                         {
                             ProfileId = profileID,
                             PhoneId = PhoneID,
                             PhoneTypeId = item.PhoneTypeId != 0 ? item.PhoneTypeId : 1,
                             CreatedBy = "Admin",
                             UpdatedBy = "Admin",
                             Updated = DateTime.Now,
                             Created = DateTime.Now
                         };
                        db.ProfilePhones.Add(profilePhone);
                    }
                    else
                    {
                        var phone = db.Phones.Find(item.PhoneId);
                        if (phone != null)
                        {
                            phone.Number = item.Number;
                            phone.UpdatedBy = "Admin";
                            phone.Updated = DateTime.Now;
                        }
                    }
                }
                db.SaveChanges();
            }

            List<AddressDTO> addressList = profileDTO.AddressDTO;
            if (addressList != null)
            {
                foreach (var item1 in addressList)
                {
                    if (item1.AddressId == 0)
                    {
                        Address address = new Address()
                        {
                            AddressLine1 = item1.AddressLine1,
                            AddressLine2 = item1.AddressLine2,
                            Country = item1.Country,
                            State = item1.State,
                            City = item1.City,
                            ZipCode = item1.ZipCode,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };

                        int addressId = saveAddress(address);

                        ProfileAddress profileAddress = new ProfileAddress()
                        {
                            ProfileId = profileID,
                            AddressId = addressId,
                            AddressTypeId = item1.AddressTypeId != 0 ? item1.AddressTypeId : 1,
                            CreatedBy = "Admin",
                            UpdatedBy = "Admin",
                            Updated = DateTime.Now,
                            Created = DateTime.Now
                        };

                        db.ProfileAddresses.Add(profileAddress);
                    }
                    else
                    {
                        var address = db.Addresses.Find(item1.AddressId);
                        if (address != null)
                        {
                            address.AddressLine1 = item1.AddressLine1;
                            address.AddressLine2 = item1.AddressLine2;
                            address.Country = item1.Country;
                            address.State = item1.State;
                            address.City = item1.City;
                            address.ZipCode = item1.ZipCode;
                            address.UpdatedBy = "Admin";
                            address.Updated = DateTime.Now;
                        }
                    }
                }
                db.SaveChanges();

            }
        }
        public Int16 savePhone(Phone phone)
        {
            db.Phones.Add(phone);
            db.SaveChanges();
            var phoneID = db.Phones.Max(p => p.PhoneId);
            return Convert.ToInt16(phoneID);
        }
        public Int16 saveAddress(Address address)
        {
            db.Addresses.Add(address);
            db.SaveChanges();
            var addressId = db.Addresses.Max(p => p.AddressId);
            return Convert.ToInt16(addressId);
        }

        public void DeleteProfile(int id)
        {
            var profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }
        public void DeletePhone(int id)
        {
            var profilephone = db.ProfilePhones.Single(p => p.PhoneId == id);
            db.ProfilePhones.Remove(profilephone);
            var phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
        }
        public void Deleteaddress(int id)
        {
            var profileaddress = db.ProfileAddresses.Single(p => p.AddressId == id);
            db.ProfileAddresses.Remove(profileaddress);
            var address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
            db.SaveChanges();
        }
    }

}