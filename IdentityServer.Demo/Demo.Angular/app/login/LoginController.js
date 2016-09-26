
//var app = angular.module('testApp.Controller1', ["testApp.Service"]);
//app.controller('loginController', function ($scope, CRUDService) {
//    var vm = this;

//    vm.loginError = "";
//    vm.credentials = {
//        username: "",
//        password: ""
//    };

//    vm.submit = function () {
//        vm.loginError = "";
//        // get the token, using the resource owner password
//        // credentials flow 

//        var loginPromise = CRUDService.login(vm.credentials);

//        loginPromise.then(function (data) {
//            // set the access token
//            localStorage["access_token"] = data.access_token;

//            // clear un/pw
//            vm.credentials.username = "";
//            vm.credentials.password = "";

//            // redirect to root
//            window.location = window.location.protocol + "//" + window.location.host + "/";

//        },function (data) {
//            // show error on screen
//            vm.loginError = data.error;

//            // clear un/pw
//            vm.credentials.username = "";
//            vm.credentials.password = "";
//        });
//    }


//});

(function () {
    "use strict";
    angular.module("testApp")
    .controller("loginController", ["$scope", "CRUDService", LoginController]);
    function LoginController($scope, CRUDService) {
        var vm = this;

        vm.loginError = "";
        vm.credentials = {
            username: "",
            password: ""
        };

        vm.submit = function () {
            vm.loginError = "";
            // get the token, using the resource owner password
            // credentials flow 

            var loginPromise = CRUDService.login(vm.credentials);

            loginPromise.then(function (result) {
                // set the access token
                //localStorage["access_token"] = data.access_token;
                localStorage["access_token"] = result.data.access_token
                // clear un/pw
                vm.credentials.username = "";
                vm.credentials.password = "";

                // redirect to root
                window.location = window.location.protocol + "//" + window.location.host + "/";

            }, function (data) {
                // show error on screen
                vm.loginError = data.error;

                // clear un/pw
                vm.credentials.username = "";
                vm.credentials.password = "";
            });
        }


    }
}());