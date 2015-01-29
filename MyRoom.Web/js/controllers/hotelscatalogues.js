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
            if (!$scope.currentHotel)
            {
                $scope.toaster = { type: 'success', title: 'Info', text: 'Please, select a hotel and catalogues' };
                $scope.pop();
                return;
            }
            var hotelcatalogVm = createActiveHotelCataloguesViewModel();
            hotelService.assignCatalog(hotelcatalogVm).then(function (response) {
                $scope.toaster = { type: 'success', title: 'Success', text: 'The catalogues has been assigned to hotel' };
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

        $scope.getAll();
  
    });
}]);