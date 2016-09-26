//(function () {
//    'use strict';

//    angular
//        .module('testApp', ['CRUDService'])
//        .controller('Main', main);

//    function main() {
//        var vm = this;
//        vm.food = 'pizza';
//    }

//})();
//(function () {
//    var app = angular.module('testApp', ['CRUDService']);
//    app.controller('MainController', ['$scope', 'CRUDService', function ($scope, CRUDService) {
//        var vm = this;
//        vm.food = 'pizza';
//        //$scope.getData = function () {
//        //    var getPromise = CRUDService.getMembers();
//        //    getPromise.then(function (response) {
//        //        $scope.user = response.data;
//        //    }, function (error) {
//        //        console.log(error);
//        //    });
//        //}
       


//    }]);
//})();
(function (){
    var app = angular.module('testApp');
    app.controller('MainController', ['$scope', function ($scope) {
        $scope.name = "Sheoshobhit";
        $scope.email = "sheo_87@yahoo.com";
        $scope.marks1 = 0;
        $scope.marks2 = 0;
        $scope.marks3 = 0;
        $scope.totalMarks = 2;
        $scope.Percentage = 0;
        $scope.food = 'pizza';
    } ])
})();