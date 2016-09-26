(function () {
    //"use strict";

    var app = angular.module("testApp",
                            ["ngRoute","testApp.Service"]);



    app.config(function ($routeProvider, $httpProvider) {

        $routeProvider
            .when("/details", {
                templateUrl: "/app/UserDetails/UserDetails.html",
                controller: "userDetailsController as ud"
            })

            .when("/login", {
                templateUrl: "/app/login/login.html",
                controller: "loginController as vm"
            })
           .otherwise({ redirectTo: "/" });
   


    $httpProvider.interceptors.push(function (tokenContainer) {
        return {
            'request': function (config) {

                // if it's a request to the API, we need to provide the
                // access token as bearer token.             
                if (config.url.indexOf('http://localhost:48412') === 0) {
                    config.headers.Authorization = 'Bearer ' + tokenContainer.getToken().token;
                }

                return config;
            }

        };
    });
    });

    //});


}());