(function () {
    "use strict";

    var app = angular.module('testApp.Service', []);
    app.service("CRUDService", function ($http) {

        //Read all Employees
        this.getMembers = function () {
            //return 'test Service';
            return $http.get('http://localhost:48412/api/Home/GetAuthorize');
        };

        this.login = function (credentials) {
            // the message body
            var dataForBody = "grant_type=password&" +
                "username=" + encodeURI(credentials.username) + "&" +
                "password=" + encodeURI(credentials.password) + "&" +
               // "client_id=" + encodeURI("angularclientropc") + "&" +
                //"client_secret=" + encodeURI("secret2") + "&" +
                "scope=" + encodeURI("WebApi");

            // RFC requirements: when clientid/secret are provided,
            // they must be sent through the Authorization header.
            // cfr:https://tools.ietf.org/html/rfc6749#section-4.3

            // encode the client id & client secret (btoa = built-in function
            // for Base64 encoding)
            var encodedClientIdAndSecret = btoa("angularclientropc:secret2");

            // the header
            var messageHeaders = {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Basic ' + encodedClientIdAndSecret
            };

            return $http({
                method: 'POST',
                url: "http://localhost:33317/core/connect/token",
                headers: messageHeaders,
                data: dataForBody
            });
        };
    });

    

}());

