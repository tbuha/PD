﻿'use strict';
app.controller('customersController', ['$scope', 'customersService', function ($scope, customersService) {

    $scope.customers = [];

    customersService.getCustomers().then(function (results) {

        $scope.customers = results.data;
        $scope.gridOptions = { data: customers };

    }, function (error) {
        //alert(error.data.message);
    });


}]);