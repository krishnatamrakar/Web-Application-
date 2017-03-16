var urlContact = "/Contact";
var AddressTypeData;
var PhoneTypeData;
var url = window.location.pathname;
var profileId = url.substring(url.lastIndexOf('/') + 1);

$.ajax({

    url: urlContact + '/returnAddressTYpe',
    dataType: 'json',
    type: 'GET',
    async: false,
    success: function (data) {
        AddressTypeData = data;

    },
    error: function () {
        alert('Error............');
    }

});
$.ajax({

    url: urlContact + '/returnPhoneType',
    dataType: 'json',
    type: 'GET',
    async: false,

    success: function (data) {
        PhoneTypeData = data;

    },
    error: function () {
        alert('Error............');
    }
});



var Profile = function (profile) {
    var self = this;
    self.ProfileId = ko.observable(profile ? profile.ProfileId : 0).extend({ required: true });
    self.FirstName = ko.observable(profile ? profile.FirstName : '').extend({ required: true, maxLength: 50 });
    self.LastName = ko.observable(profile ? profile.LastName : '').extend({ required: true, maxLength: 50 });
    self.Email = ko.observable(profile ? profile.Email : '').extend({ required: true, maxLength: 50, email: true });
    self.PhoneDTO = ko.observableArray(profile ? profile.PhoneDTO : []);
    self.AddressDTO = ko.observableArray(profile ? profile.AddressDTO : []);
};

var PhoneLine = function (phone) {
    var self = this;
    self.PhoneId = ko.observable(phone ? phone.PhoneId : 0);
    self.PhoneTypeId = ko.observable(phone ? phone.PhoneTypeId : undefined).extend({ required: true });
    self.Number = ko.observable(phone ? phone.Number : '').extend({ required: true, maxLength: 25, phoneUS: false });
};

var AddressLine = function (address) {
    var self = this;
    self.AddressId = ko.observable(address ? address.AddressId : 0);
    self.AddressTypeId = ko.observable(address ? address.AddressTypeId : undefined).extend({ required: true });
    self.AddressLine1 = ko.observable(address ? address.AddressLine1 : '').extend({ required: true, maxLength: 100 });
    self.AddressLine2 = ko.observable(address ? address.AddressLine2 : '').extend({ required: true, maxLength: 100 });
    self.Country = ko.observable(address ? address.Country : '').extend({ required: true, maxLength: 50 });
    self.State = ko.observable(address ? address.State : '').extend({ required: true, maxLength: 50 });
    self.City = ko.observable(address ? address.City : '').extend({ required: true, maxLength: 50 });
    self.ZipCode = ko.observable(address ? address.ZipCode : '').extend({ required: true, maxLength: 15 });

};

var ProfileCollection = function () {
    var self = this;
    self.profile = ko.observable();
    self.phoneNumbers = ko.observableArray();
    self.addresses = ko.observableArray();

    //if ProfileId is 0, It means Create new Profile
    if (profileId == 0) {
        self.profile(new Profile());
        self.phoneNumbers([new PhoneLine()]);
        self.addresses([new AddressLine()]);
    }
    else {


        $.ajax({
            url: urlContact + '/GetProfileById/',
            type: 'GET',
            dataType: 'json',
            data: { idOfSelectedContact: profileId },
            success: function (data) {
                //self.profile = ko.observable(new Profile(data));
                self.profile(new Profile(data));

            }
        });
        $.ajax({
            url: urlContact + '/GetPhoneById/',
            type: 'GET',
            dataType: 'json',
            data: { idOfSelectedContact: profileId },
            success: function (data) {

                self.phoneNumbers(ko.utils.arrayMap(data, function (phone) {
                    return phone;
                }));

            }
        });
        $.ajax({
            url: urlContact + '/GetAddressById/',
            type: 'GET',
            dataType: 'json',
            data: { idOfSelectedContact: profileId },
            success: function (data) {
                self.addresses(ko.utils.arrayMap(data, function (address) {
                    return address;
                }));
            }
        });
    }

    self.addPhone = function () { self.phoneNumbers.push(new PhoneLine()) };

    self.removePhone = function (phone) {

        if (confirm("Are you sure you want to delete this Phone?")) {
            var id = phone.PhoneId;
      
            $.ajax({
                type: 'DELETE',
                url: urlContact + '/DeletePhoneProfile/',
                data: { idtodelete: id },
                success: function () {
                    self.phoneNumbers.remove(phone)
                },
            });
        }


    };

    self.addAddress = function () { self.addresses.push(new AddressLine()) };

    self.removeAddress = function (address) {

        if (confirm("Are you sure you want to delete this Address?")) {
            var id = address.AddressId;
          
            $.ajax({
                type: 'DELETE',
                url: urlContact + '/DeleteAddressProfile/',
                data: { idtodelete: id },
                success: function () {
                    self.addresses.remove(address)
                },
            });
        }


    };

    self.backToProfileList = function () { window.location.href = '/Contact/Index'; };

    self.profileErrors = ko.validation.group(self.profile());
    self.phoneErrors = ko.validation.group(self.phoneNumbers(), { deep: true });
    self.addressErrors = ko.validation.group(self.addresses(), { deep: true });

    self.saveProfile = function () {
        var isValid = true;
        var errottype='';

        if (self.profileErrors().length != 0) {
            self.profileErrors.showAllMessages();
            errottype = 'profile';
            isValid = false;
        }

        if (self.phoneErrors().length != 0) {
            self.phoneErrors.showAllMessages();
            errottype = 'phone';
            isValid = false;
        }

        if (self.addressErrors().length != 0) {
            self.addressErrors.showAllMessages();
            errottype = 'address';
            isValid = false;
        }
        if (isValid) {
            self.profile().AddressDTO = self.addresses;
            self.profile().PhoneDTO = self.phoneNumbers;
            debugger;
            $.ajax({
                type: 'Post',
                cache: false,
                dataType: 'json',
                url: urlContact + (self.profile().ProfileId > 0 ? '/UpdateProfileInformation?id=' + self.profile().ProfileId : '/SaveProfileInformation'),
                data: JSON.stringify(ko.toJS(self.profile())),
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function () {
                    window.location.href = '/Contact/Index';
                },
                error: function (err) {

                },
                complete: function () {
                    window.location.href = '/Contact/Index';
                }
            });
        }
        else
        {
            alert('Please Provide all the feild required!!'+errottype+' detail is missing');
        }

    };
};

ko.applyBindings(new ProfileCollection());