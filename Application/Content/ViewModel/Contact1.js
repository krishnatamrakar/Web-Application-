var dataFromServer;
$(document).ready(function () {

    function Item(ProfileId, FirstName, LastName, Email) {
        this.ProfileId = ko.observable(ProfileId)
        this.FirstName = ko.observable(FirstName);
        this.LastName = ko.observable(LastName);
        this.Email = ko.observable(Email);
    }

    var viewModel = {
        items: ko.observableArray([]),
        filter: ko.observable(""),
        search: ko.observable(""),

        createProfile: function () {
            alert("Create a new profile");
        },

        createProfile: function () {
            window.location.href = '/Contact/CreateEdit/0';
        },
        removeItem: function (item) {
            if (confirm("Are you sure you want to delete this profile?")) {
                var id = item.ProfileId();
                $.ajax({
                    type: 'DELETE',
                    url: 'DeleteProfile/',
                    data: { idTodelete: id },
                    success: function () {
                        viewModel.items.remove(item);
                    },
                    error: function (err) {
                        alert(err.responseText);
                    },
                    complete: function () { }
                });
            }
        },
        editProfile: function (Item) {
            var id = Item.ProfileId();
            window.location.href = '/Contact/CreateEdit/' + id;
        },
    };

    viewModel.load = function () {
        var filter = viewModel.filter().toLowerCase();
        $.ajax(
   {
       url: 'GetAllProfiles',
       type: 'GET',
       async: false,
       dataType: 'json',
       data: { searchString: filter },
       success: function (data) {
           // alert('success');
           dataFromServer = data;
       },
       error: function (xhr, status, error) {
           alert('error');
       }

   });
    }
    viewModel.load();
    viewModel.filteredItems = ko.dependentObservable(function () {
        var filter = this.filter().toLowerCase();
        if (!filter) {
            return this.items();
        } else {
            return ko.utils.arrayFilter(this.items(), function (item) {
                return ko.utils.stringStartsWith(item.FirstName().toLowerCase(), filter);
            });
        }
    }, viewModel);

    viewModel.mappedItems = ko.dependentObservable(function () {
        var items = ko.toJS(this.items);
        return ko.utils.arrayMap(items, function (item) {
            return item;
        });
    }, viewModel);

    var mappedData = ko.utils.arrayMap(dataFromServer, function (item) {

        return new Item(item.ProfileId, item.FirstName, item.LastName, item.Email);
    });

    viewModel.items(mappedData);
    ko.applyBindings(viewModel);
});


