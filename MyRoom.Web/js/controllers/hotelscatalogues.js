'use strict';

/* Controllers */
// Hotels controller
app.controller('HotelsCataloguesController', ['$scope', '$http', '$state', 'catalogService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, catalogService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.catalogues = {};
    
    angular.element(document).ready(function () {
        $scope.dtOptions = DTOptionsBuilder.newOptions().withPaginationType('full_numbers');
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1),
            DTColumnDefBuilder.newColumnDef(2).notSortable()
        ];

        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };

        $scope.getAll = function () {
            debugger
            catalogService.getAll().then(function (response) {
                $scope.catalogues = response.data;

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        };

       
        $scope.getAll();
    });
}]);