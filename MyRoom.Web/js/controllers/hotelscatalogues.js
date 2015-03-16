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

        $scope.showHotelCataloguesPopup = function () {
            $('#popupHotelAssign').modal({
                show: 'true'
            });
        }

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
            if (!$scope.currentHotel)
            {
                $scope.toaster = { type: 'success', title: 'Info', text: 'Please, select a hotel and catalogues' };
                $scope.pop();
                return;
            }
            var hotelcatalogVm = createActiveHotelCataloguesViewModel();
            hotelService.assignCatalog(hotelcatalogVm).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Success', text: 'The hotel has been changed' };
                $scope.pop();
            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });

        }

        $scope.selectHotel = function (hotel)
        {
            $scope.currentHotel = hotel;
            hotelService.getCatalogAssignedByHotelId($scope.currentHotel.HotelId).then(function (response) {
                $scope.catalog = response.data;
                for (var i = 0 ; i < $scope.catalogues.length; i++) {
                    $scope.catalogues[i].checked = false;
                }
                for (var j = 0 ; j < $scope.catalog.length; j++) {
                    for (var i = 0 ; i < $scope.catalogues.length; i++) {
                        $scope.catalogues[i].checked = false;
                        if ($scope.catalogues[i].CatalogId == $scope.catalog[j].IdCatalogue) {
                            $scope.catalogues[i].checked = true;
                        }
                    }
                }

            },
            function (err) {
                $scope.toaster = { type: 'error', title: 'Error', text: err.error_description };
                $scope.pop();
            });
        };


        function createActiveHotelCataloguesViewModel() {
            var vm = { 'CataloguesIds': [] };
            vm.HotelId = $scope.currentHotel.HotelId;

            $scope.catalogues.filter(function (value) {
                if (value.checked == true) {
                    vm.CataloguesIds.push(value.CatalogId);
                }
            });
            return vm;
        }

        $scope.catalogSelect = function(currentCatalog)
        {
            angular.forEach($scope.catalogues, function (item) {
                if (item != currentCatalog) {
                    item.checked = false;
                }
            });
        }

        $scope.getAll();
  
    });
}]);