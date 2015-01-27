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

        $scope.assignCatalog = function () {
            hotelService.assignCatalog().then(function (response) {
                $scope.toaster = { type: 'success', title: 'Success', text: 'Catalogues assigned to hotel' };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });

        }

        $scope.getAll();

        $scope.selectHotel = function (hotel)
        {
            $scope.currentHotel = hotel;
        };

        $scope.activeCatalog = function ()
        {
            if ($scope.currentHotel != undefined) {
                //Aca procedes a grabar, en currentHotel tienes todo el hotel seleccionado por si requieres otra informacion
                var activeHotelCatalog = [];
                $scope.catalogues.filter(function (value) {
                    if (value.checked == true) {
                        activeHotelCatalog.push({ IdHotel: $scope.currentHotel.HotelId, IdCatalogue: value.CatalogId });
                    }
                });
                if (activeHotelCatalog.length == 0) {
                    activeHotelCatalog.push({ IdHotel: $scope.currentHotel.HotelId });
                }
                hotelService.saveActiveHotelCatalog(activeHotelCatalog).then(function (response) {
                    $scope.toaster = { type: 'success', title: 'Info', text: 'The Hotel Catalogue has been activated' };
                    $scope.pop();
                },
                function (err) {
                    $scope.toaster = { type: 'success', title: 'Info', text: err.error_description };
                    $scope.pop();
                });
            }
            else {
                $scope.toaster = { type: 'error', title: 'Error', text: 'Select hotel' };
                $scope.pop();
            }
        }
        $scope.getAll();
    });
}]);