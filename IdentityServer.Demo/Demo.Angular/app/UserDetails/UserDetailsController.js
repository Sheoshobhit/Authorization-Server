
//var app = angular.module('testApp.Controller', ["testApp.Service"]);
//app.controller('userDetailsController', function ($scope, CRUDService) {
//        $scope.getData = function () {
//            var getPromise = CRUDService.getMembers();
//            getPromise.then(function (response) {
//                $scope.user = response.data;
//            }, function (error) {
//                console.log(error);
//            });
//        }
//        //$scope.user = 'testing controller';


//});

(function () {
    "use strict";
    angular.module("testApp")
    .controller("userDetailsController", ["$scope", "CRUDService", UserDetailsController]);
    function UserDetailsController($scope, CRUDService) {
        $scope.getData = function () {
            var getPromise = CRUDService.getMembers();
            getPromise.then(function (response) {
                $scope.user = response.data;
            }, function (error) {
                console.log(error);
            });
        }
    }
}());
