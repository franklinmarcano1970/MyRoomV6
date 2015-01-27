'use strict';

/* Controllers */
// Hotels controller
app.controller('HotelsCataloguesController', ['$scope', '$http', '$state', 'catalogService', 'hotelService', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'toaster', function ($scope, $http, $state, catalogService, hotelService, DTOptionsBuilder, DTColumnDefBuilder, toaster) {
    $scope.catalogues = {};
    $scope.hotels = {};
    angular.element(document).ready(function () {
    
        $scope.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0),
            DTColumnDefBuilder.newColumnDef(1),
            DTColumnDefBuilder.newColumnDef(2).notSortable()
        ];

        $scope.pop = function () {
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
        };

        $scope.getAll = function () {
            catalogService.getAll().then(function (response) {
                $scope.catalogues = response.data;

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });

            hotelService.getAll().then(function (response) {
                $scope.hotels = response.data;

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        };

        $scope.selectHotel = function (hotel)
        {
            $scope.currentHotel = hotel;
        };

        $scope.activeCatalog = function ()
        {
            if ($scope.currentHotel != undefined) {
                //Aca procedes a grabar, en currentHotel tienes todo el hotel seleccionado por si requieres otra informacion
            }
            else {
                $scope.toaster = { type: 'error', title: 'Error', text: 'Select hotel' };
                $scope.pop();
            }
        }
        $scope.getAll();


    });
}]);