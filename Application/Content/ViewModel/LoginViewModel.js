$(document).ready(function () {
    var self = this;

    var loginViewModel = {
        UserName: ko.observable(),
        Password: ko.observable(),
        RememberMe: ko.observable(),

        LoginUser: function () {
            // debugger;
            var dataSent =
               {
                   UserName: this.UserName(),
                   Password: this.Password(),

               };
            var baseurl = $("#GoToContactManager").val();
            $.ajax(
                     {
                         url: $('#GetLoginPath').val(),
                         type: "post",
                         data: ko.toJSON(dataSent),
                         contentType: "application/json",
                         error: function (xhr) { },
                         success: function (data) {
                             if (data) {
                                
                                 if (data.IsUserLogin) {
                                     var usermane = data.UserName;
                                     window.location = baseurl;
                                 }//if
                             }
                         }

                     });
        },
        Routefunction: function () {
        }
    };

    ko.applyBindings(loginViewModel, $('#Login').get(0));

    var validator = $("#Login").validate(
        {

            submitHandler: function () { loginViewModel.LoginUser() },

            rules: {

                txtUserName: {
                    required: {
                        required: true
                    },

                },
                txtPassword: {
                    required: true
                },

            },

            messages: {

                txtUserName: {
                    required: "Please Enter Username"

                },
                txtPassword:
                        {
                            required: "Please Enter Password",
                        },
            },

            highlight: function (element, errorClass) {
                $(element).closest("div").addClass("has-error");
            },

            unhighlight: function (element, errorClass) {
                $(element).closest("div").removeClass("has-error");
            }
        });

    //self.LoginClick = function () {
    //    var ajaxUrl
    //    alert('test');
    //};
});